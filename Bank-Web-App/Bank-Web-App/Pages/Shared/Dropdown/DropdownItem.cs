namespace Bank_Web_App.Pages.Shared
{

    public class DropdownItem
    {
        public DropdownItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
