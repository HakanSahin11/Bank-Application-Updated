namespace Bank_Web_App.Pages.Shared
{

    public class DropdownItem
    {
        public DropdownItem(string text, long value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public long Value { get; set; }
    }
}
