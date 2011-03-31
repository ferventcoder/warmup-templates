namespace __NAME__.infrastructure.persistence.listeners
{
    using System;
    using model.auditing;
    using NHibernate.Event;
    using NHibernate.Persister.Entity;

    public class AuditEventListener : IPreInsertEventListener, IPreUpdateEventListener
    {
        public string get_identity()
        {
            return context.information.VersionInformation.get_version();

            return context.information.UserInformation.get_identity();
        }

        //http://ayende.com/Blog/archive/2009/04/29/nhibernate-ipreupdateeventlistener-amp-ipreinserteventlistener.aspx
        public bool OnPreInsert(PreInsertEvent event_item)
        {
            Auditable audit = event_item.Entity as Auditable;
            if (audit == null)
            {
                return false;
            }

            DateTime? entered_date = DateTime.Now;
            DateTime? modified_date = DateTime.Now;
            string identity_of_updater = get_identity();

            store(event_item.Persister, event_item.State, "entered_date", entered_date);
            store(event_item.Persister, event_item.State, "modified_date", modified_date);
            store(event_item.Persister, event_item.State, "updating_user", identity_of_updater);
            audit.entered_date = entered_date;
            audit.modified_date = modified_date;
            audit.updating_user = identity_of_updater;

            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent event_item)
        {
            Auditable audit = event_item.Entity as Auditable;
            if (audit == null)
            {
                return false;
            }

            DateTime? modified_date = DateTime.Now;
            string identity_of_updater = get_identity();

            store(event_item.Persister, event_item.State, "modified_date", modified_date);
            store(event_item.Persister, event_item.State, "updating_user", identity_of_updater);
            audit.modified_date = modified_date;
            audit.updating_user = identity_of_updater;

            //insert auditing object here

            return false;
        }

        public void store(IEntityPersister persister, object[] state, string property_name, object value)
        {
            int index = Array.IndexOf(persister.PropertyNames, property_name);
            if (index == -1)
            {
                return;
            }
            state[index] = value;
        }
    }
}