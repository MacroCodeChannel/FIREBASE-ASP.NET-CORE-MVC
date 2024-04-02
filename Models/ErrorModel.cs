namespace FIREBASEASPMVC.Models
{
    public class ErrorModel
    {
        public int  code { get; set; }

        public string message { get; set; } 

        public List<ErrorModel> errors { get; set; }    
    }
}
