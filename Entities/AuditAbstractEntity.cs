namespace CMC.TS.FT.Api.Entities
{
    public abstract class AuditAbstractEntity
    {
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid CreateBy { get; set; }
        public Guid UpdateBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
