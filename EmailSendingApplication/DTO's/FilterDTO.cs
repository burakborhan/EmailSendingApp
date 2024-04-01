using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplication.DTO_s
{
    public class FilterDTO
    {
        public string? Name { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? HomePhoneNo { get; set; }
        public string? CellPhoneNo { get; set; }
        public string? Title { get; set; }
        public string? WorkPlace { get; set; }
    }
}
