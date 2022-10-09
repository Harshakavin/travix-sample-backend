namespace TravixBackend.API.Dtos.V1.Response
{
    public class ErrorResponse : IResponse
    {
        public bool Success { get; set; } = false;
        public string Code { get; set; }
        public ErrorMessage Message { get; set; } = new ErrorMessage();

        public class ErrorMessage
        {
            public string En { get; set; }
        }
    }
}
