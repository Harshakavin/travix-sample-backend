namespace TravixBackend.API.Dtos.V1.Response
{
    public class SuccessResponse : IResponse
    {
        public bool Success { get; set; } = true;
        public object Response { get; set; }

        public SuccessResponse(object response)
        {
            Response = response;
        }
    }
}
