namespace BlogWebApi.Helpers
{
    public class ResultHelper
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public static ResultHelper Success(string message, object? data = null)
        {
            return new ResultHelper
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static ResultHelper Failure(string message, object? data = null)
        {
            return new ResultHelper
            {
                Status = false,
                Message = message,
                Data = data
            };
        }
    }
}
