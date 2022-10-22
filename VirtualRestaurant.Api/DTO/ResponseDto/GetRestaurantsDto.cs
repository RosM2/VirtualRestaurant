namespace VirtualRestaurant.Api.DTO.ResponseDto
{
    public class GetRestaurantsDto
    {
        public string Name { get; set; }

        public int TotalTablesCount { get; set; }

        public int FreeTablesCount { get; set; }
    }
}
