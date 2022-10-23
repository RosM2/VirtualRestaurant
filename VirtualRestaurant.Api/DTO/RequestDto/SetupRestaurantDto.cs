namespace VirtualRestaurant.Api.DTO.RequestDto
{
    public class SetupRestaurantDto
    {
        public int RestaurantId { get; set; }

        public List<TableSetupDto> Tables { get; set; }
    }
}
