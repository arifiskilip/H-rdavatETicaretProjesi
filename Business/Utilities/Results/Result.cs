namespace Business.Utilities.Results
{
    public class Result : IResult
    {
        public bool Succes { get; }

        public string Message { get; }
        public Result(bool succes, string message) : this(succes)
        {
            Message = message;
        }
        public Result(bool succes)
        {
            Succes = succes;
        }
    }
}
