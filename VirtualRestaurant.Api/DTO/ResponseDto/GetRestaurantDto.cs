namespace VirtualRestaurant.Api.DTO.ResponseDto
{
    public class GetRestaurantDto
    {
        public int Id{ get; set; }

        public string Name { get; set; }

        public int TotalTablesCount { get; set; }

        public int FreeTablesCount { get; set; }
    }
}
