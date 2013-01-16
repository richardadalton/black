namespace com.devjoy.black
{
    public class TableCell
    {
        private string _text;

        public TableCell(string createFrom)
        {
            _text = createFrom;
        }

        public void Pass() { _text = "pass"; }
        public void Pass(string message) { _text = string.Format("pass: {0}", message); }
        public void Fail() { _text = "fail"; }
        public void Fail(string message) { _text = string.Format("fail: {0}", message); }
        public void Ignore() { _text = "ignore"; }
        public void Ignore(string message) { _text = string.Format("ignore: {0}", message); }
        public void Report(string message) { _text = string.Format("report: {0}", message); }
        public void Empty() { _text = string.Empty; }
        public void Error(string message) { _text = string.Format("error: {0}", message); }
        public void SetText(string message) { _text = message; }

        public string AsSlimCell() { return _text; }

        public void CompareWith(string value)
        {
            if (AsSlimCell() == value)
                Pass();
            else
                Fail(string.Format("Expected:{0} Actual:{1}",AsSlimCell(), value));
        }
    }
}
