using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp
{
    [Table("stationgegevens")]
    public class StationRecord
    {
        [Key]
        public int Id {get; set;}

        [Column("station")]
        public string? Station{get;set;}

        [Column("datum")]
        public DateTime Datum {get;set;}

        [Column("actuele_temperatuur")]
        public float ActueleTemperatuur { get; set; }

        [Column("zonnekracht")]
        public float Zonnekracht { get; set; }

        [Column("gevoelstemperatuur")]
        public float Gevoelstemperatuur { get; set; }

        [Column("regen_laatste_uur")]
        public float RegenLaatsteUur { get; set; }

        [Column("grond_temperatuur")]
        public float GrondTemperatuur { get; set; }

        [Column("windrichting")]
        public string? Windrichting { get; set; }
        
    }

    
    
}