using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class ReportDtoResponse
    {
        public ICollection<PieDataDto> PieData { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class PieDataDto
    {
        public int Day { get; set; }
        public decimal TotalSales { get; set; }
    }
}