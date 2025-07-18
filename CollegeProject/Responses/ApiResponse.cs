namespace CollegeProject.Responses
{
    public class ApiResponse
    {
            public string Message { get; set; }
            public bool Success { get; set; }

    }
    public class ErroResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

    }
}
