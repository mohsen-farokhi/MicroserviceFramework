using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public class Result
    {
        private readonly FluentResults.Result _result;
        public Result() : base()
        {
            _result = new FluentResults.Result();
        }

        public bool IsFailed
        {
            get
            {
                return _result.IsFailed;
            }
        }

        public bool IsSuccess
        {
            get
            {
                return _result.IsSuccess;
            }
        }

        private List<string> _errors;
        public IReadOnlyList<string> Errors
        {
            get
            {
                _errors =
                    _result.Errors
                    .Select(c => c.Message)
                    .ToList();

                return _errors;
            }
        }

        private List<string> _successes;
        public IReadOnlyList<string> Successes
        {
            get
            {
                _successes =
                    _result.Successes
                    .Select(c => c.Message)
                    .ToList();

                return _successes;
            }
        }

        public void WithError(string errorMessage)
        {
            errorMessage =
                String.Fix(text: errorMessage);

            if (errorMessage == null)
            {
                return;
            }

            if (_result.Errors.Select(c => c.Message).Contains(errorMessage))
            {
                return;
            }

            _result.WithError(errorMessage: errorMessage);
        }

        public void ClearErrorMessages()
        {
            _result.Errors.Clear();
        }

        public void WithSuccess(string successMessage)
        {
            successMessage =
                String.Fix(text: successMessage);

            if (successMessage == null)
            {
                return;
            }

            if (_result.Successes.Select(c => c.Message).Contains(successMessage))
            {
                return;
            }

            _result.WithSuccess(successMessage: successMessage);
        }

        public void ClearSuccessMessages()
        {
            _result.Successes.Clear();
        }

        public void WithErrors(IReadOnlyList<string> errors)
        {
            foreach (var message in errors)
            {
                WithError(errorMessage: message);
            }
        }

    }

    public class Result<T> : Result
    {
        private readonly FluentResults.Result<T> _result;
        public Result()
        {
            _result = new FluentResults.Result<T>();
        }

        public T Value
        {
            get
            {
                return _result.Value;
            }
        }

        public void WithValue(T value)
        {
            _result.WithValue(value: value);
        }

        public Result ToResult()
        {
            var result = new Result();

            foreach (var message in Errors)
            {
                result.WithError(message);
            }

            foreach (var message in Successes)
            {
                result.WithSuccess(message);
            }

            return result;
        }
    }
}
