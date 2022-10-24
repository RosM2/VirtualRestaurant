namespace VirtualRestaurant.BusinessLogic
{
    public class Result
    {
        protected Result()
        {
            Successful = true;
        }

        protected Result(string error)
        {
            Successful = false;
            Error = error;
        }

        public bool Successful { get; protected set; }

        public string Error { get; protected set; }

        public static Result Ok() => new Result();

        public static Result Fail(string error) => new Result(error);
    }

    public sealed class Result<T> : Result
    {
        private readonly T _value;

        private Result(T value)
        {
            _value = value;
        }

        private Result(string error) : base(error)
        {
        }

        public T Value
        {
            get
            {
                if (!Successful)
                {
                    throw new NotSupportedException("Accessed result value of unsuccessful operation.");
                }
                return _value;
            }
        }

        public static Result<T> Ok(T value) => new Result<T>(value);

        public new static Result<T> Fail(string error) => new Result<T>(error);
    }
}
