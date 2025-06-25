namespace EmailSender.Core.Entities.Common
{
    public abstract class Entity
    {
        protected Entity()
        {
            SetCreatedDate();
        }
        protected Guid Guid { get; set; }
        protected DateTimeOffset CreatedAt { get; set; }
        protected DateTimeOffset ModifiedAt { get; set; }

        private void SetCreatedDate()
        {
            CreatedAt = DateTimeOffset.Now;
            ModifiedAt = DateTimeOffset.Now;
        }

        public void SetModifiedDate()
        {
            ModifiedAt = DateTimeOffset.Now;
        }
    }
}
