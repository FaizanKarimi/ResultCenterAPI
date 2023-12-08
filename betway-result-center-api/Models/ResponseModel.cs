namespace betway_result_center_api.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            this.message = "Data returned from Database";
            this.status = "success";
        }

        public string status { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}