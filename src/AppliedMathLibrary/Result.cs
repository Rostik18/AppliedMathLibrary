namespace AppliedMathLibrary
{
    /// <summary> Result of action with T type value </summary>
    public readonly struct Result<T>
    {
        /// <summary> Value of the result. Default if result is failed </summary>
        public T Value { get; }

        /// <summary> The result is failed </summary>
        public bool IsFailure { get; }

        /// <summary> The result is success </summary>
        public bool IsSuccess => !IsFailure;

        /// <summary> Reason why the result is failed </summary>
        public string Error { get; }

        internal Result(T value, bool isFailure, string error)
        {
            Value = value;
            IsFailure = isFailure;
            Error = error;
        }

        /// <summary> Convert T value to success result </summary>
        public static implicit operator Result<T>(T value) => new(value, false, string.Empty);
    }

    /// <summary> Result of action </summary>
    public readonly struct Result
    {
        /// <summary> The result is failed </summary>
        public bool IsFailure { get; }

        /// <summary> The result is success </summary>
        public bool IsSuccess => !IsFailure;

        /// <summary> Reason why the result is failed </summary>
        public string Error { get; }

        internal Result(bool isFailure, string error)
        {
            IsFailure = isFailure;
            Error = error;
        }

        /// <summary> Creates success result of action </summary>
        /// <returns> Success result </returns>
        public static Result Success() => new(false, string.Empty);

        /// <summary> Creates success result of action with T type value </summary>
        /// <param name="value"> Value of success result </param>
        /// <returns> Success result with T type value </returns>
        public static Result<T> Success<T>(T value) => new(value, false, string.Empty);

        /// <summary> Creates failed result of action </summary>
        /// <param name="error"> Reason why the result is failed </param>
        /// <returns> Failed result </returns>
        public static Result Failure(string error) => new(true, error);

        /// <summary> Creates failed result of action with T type default value </summary>
        /// <param name="error"> Reason why the result is failed </param>
        /// <returns> Failed result </returns>
        public static Result<T> Failure<T>(string error) => new(default, true, error);
    }
}
