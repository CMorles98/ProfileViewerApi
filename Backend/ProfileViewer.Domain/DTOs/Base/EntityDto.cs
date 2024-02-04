namespace ProfileViewer.Domain.DTOs.Base
{
    public class EntityDto
    {
        public Guid Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; } = DateTime.Now;

    }
}
