using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Dtos
{
    public class DailyIncomeDto
    {
        public int Id { get; set; }
        public string BaseAmount { get; set; }
        public string DailyYeld { get; set; }
        public string Date { get; set; }
    }
}
