using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp{
    [ApiController]
    [Route("api/weather")]
    public class WeatherApiController : ControllerBase
{
    private readonly Db _db;

    public WeatherApiController(Db db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetWeatherSqlData(
        string station,
        DateTime startDatum,
        DateTime? eindDatum)
    {
        if (string.IsNullOrEmpty(station))
        {
            return BadRequest("Station is verplicht.");
        }
        var eind = eindDatum ?? startDatum.AddDays(7);
        
        var results = new List<StationRecord>();

        using var connection = await _db.GetOpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = @"
        SELECT * FROM stationgegevens
        WHERE station = @station
        AND datum >= @startDatum
        AND datum <= @eindDatum
        ORDER BY datum";

        command.Parameters.AddWithValue("@station", station);
        command.Parameters.AddWithValue("@startDatum", startDatum);
        command.Parameters.AddWithValue("@eindDatum", eind);

        using var reader = await command.ExecuteReaderAsync();
        while(await reader.ReadAsync())
            {
                results.Add(new StationRecord
                {
                    Station = reader.GetString("station"),
                    Datum = reader.GetDateTime("datum"),
                    ActueleTemperatuur = reader.GetFloat("actuele_temperatuur"),
                    Zonnekracht = reader.GetFloat("zonnekracht"),
                    Gevoelstemperatuur = reader.GetFloat("gevoelstemperatuur"),
                    RegenLaatsteUur = reader.GetFloat("regen_laatste_uur"),
                    GrondTemperatuur = reader.GetFloat("grond_temperatuur"),
                    Windrichting = reader.GetString("windrichting")
                });
            }




            return Ok(results);
    }
}
}