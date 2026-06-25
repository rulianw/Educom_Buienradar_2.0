using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WeatherApp;

namespace weerbericht_casus_2._0.Models
{
    public class BackgroundFetcher : BackgroundService
    {
        private readonly BuienradarClient _client;
        private readonly Db _db;

        public BackgroundFetcher(BuienradarClient client, Db db)
        {
            _client = client;
            _db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var feed = await _client.FetchAsync();

                using var connection = await _db.GetOpenConnectionAsync();

                foreach (var station in feed.Actual.StationMeasurements)
                {
                    using var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO stationgegevens 
                            (station, datum, actuele_temperatuur, zonnekracht, 
                             gevoelstemperatuur, regen_laatste_uur, grond_temperatuur, windrichting) 
                        VALUES (@station, @datum, @actueleTemperatuur, @zonnekracht, 
                                @gevoelstemperatuur, @regenLaatsteUur, @grondTemperatuur, @windrichting) 
                        ON DUPLICATE KEY UPDATE 
                            actuele_temperatuur = VALUES(actuele_temperatuur),
                            zonnekracht = VALUES(zonnekracht),
                            gevoelstemperatuur = VALUES(gevoelstemperatuur),
                            regen_laatste_uur = VALUES(regen_laatste_uur),
                            grond_temperatuur = VALUES(grond_temperatuur),
                            windrichting = VALUES(windrichting)";

                    command.Parameters.AddWithValue("@station", station.StationName);
                    command.Parameters.AddWithValue("@datum", DateTime.Now);
                    command.Parameters.AddWithValue("@actueleTemperatuur", station.Temperature);
                    command.Parameters.AddWithValue("@zonnekracht", station.SunPower);
                    command.Parameters.AddWithValue("@gevoelstemperatuur", station.FeelTemperature);
                    command.Parameters.AddWithValue("@regenLaatsteUur", station.RainFallLastHour);
                    command.Parameters.AddWithValue("@grondTemperatuur", station.GroundTemperature);
                    command.Parameters.AddWithValue("@windrichting", station.WindDirection);

                    await command.ExecuteNonQueryAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // ← runs every 10 minutes
            }
        }
    }
}