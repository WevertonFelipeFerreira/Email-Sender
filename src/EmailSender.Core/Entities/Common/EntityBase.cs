using Flunt.Notifications;

namespace EmailSender.Core.Entities.Common
{
    public abstract class EntityBase : Notifiable<Notification>
    {
        protected EntityBase(Guid id)
        {
            SetId(id);
            SetCreatedDate();
        }

        protected EntityBase() :
            this(Guid.NewGuid())
        {

        }

        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset ModifiedAt { get; protected set; }
        public DateTimeOffset? DeletedAt { get; protected set; }

        protected void SetId(Guid id) => Id = id;
        private void SetCreatedDate()
        {
            CreatedAt = DateTimeOffset.Now;
            ModifiedAt = DateTimeOffset.Now;
        }

        public void SetModifiedDate()
        {
            ModifiedAt = DateTimeOffset.Now;
        }

        public void SetDeletedDate()
        {
            DeletedAt = DateTimeOffset.Now;
        }

        public abstract void Validate();
    }
}
