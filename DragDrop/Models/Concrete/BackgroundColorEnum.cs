namespace DragDrop.Models
{
    public static class Helpers
    {
        public static string ToBS(this BackgroundColorEnum e)
            => e switch
            {
                BackgroundColorEnum.Red => "bg-danger-30",
                BackgroundColorEnum.Green => "bg-success-30",
                BackgroundColorEnum.Grey => "bg-secondary-30",
                BackgroundColorEnum.Blue => "bg-primary-30",
                BackgroundColorEnum.Transparent => "bg-transparent",
                BackgroundColorEnum.White => "bg-light",
                BackgroundColorEnum.Orange => "bg-warning",
                BackgroundColorEnum.Dark => "bg-dark",
                _ => "bg-transparent"
            };
    }
    public enum BackgroundColorEnum
    {
        Transparent,
        White,
        Red,
        Green,
        Orange,
        Grey,
        Blue,
        Dark
    }
}
