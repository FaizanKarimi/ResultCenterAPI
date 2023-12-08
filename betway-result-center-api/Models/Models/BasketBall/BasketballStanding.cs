using System;

namespace betway_result_center_api.Models.Models.BasketBall
{
    public class BasketballStanding
    {
        public string TeamName { get; set; }
        public Int16? Won { get; set; }
        public Int16? Lost { get; set; }
        public double Pct { get; set; }
        public string GB { get; set; }
        public decimal PSG { get; set; }
        public decimal PAG { get; set; }
        public Int16? Place { get; set; }
    }
}