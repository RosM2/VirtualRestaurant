namespace VirtualRestaurant.Api.DTO.ResponseDto
{
    public class GetRestaurantsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FreeTablesCount { get; set; }
    }
}
