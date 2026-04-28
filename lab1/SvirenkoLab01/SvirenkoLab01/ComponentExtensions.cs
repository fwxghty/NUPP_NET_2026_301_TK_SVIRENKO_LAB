namespace SvirenkoLab01
{
    public static class ComponentExtensions
    {
        // Метод розширення
        public static void ApplyDiscount(this Component component, decimal discountPercentage)
        {
            component.Price -= component.Price * (discountPercentage / 100);
        }
    }
}