namespace AppliedMathLibrary
{
    public struct Result<T>
    {
        public T Value { get; }
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        public string Error { get; }

        internal Result(T value, bool isFailure, string error)
        {
            Value = value;
            IsFailure = isFailure;
            Error = error;
        }

        public static implicit operator Result<T>(T value) => new(value, false, string.Empty);
        public static implicit operator bool(Result<T> result) => result.IsSuccess;
    }

    public struct Result
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        public string Error { get; }

        internal Result(bool isFailure, string error)
        {
            IsFailure = isFailure;
            Error = error;
        }

        public static implicit operator bool(Result result) => result.IsSuccess;

        public static Result Success() => new(false, string.Empty);
        public static Result<T> Success<T>(T value) => new(value, false, string.Empty);

        public static Result Failure(string error) => new(true, error);
        public static Result<T> Failure<T>(string error) => new(default, true, error);
    }
}
