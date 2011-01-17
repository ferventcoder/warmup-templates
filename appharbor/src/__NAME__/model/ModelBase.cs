namespace __NAME__.model
{
    using System;

    public class ModelBase : IModel
    {
        public long id { get; set; }

        // auditing
        public DateTime? entered_date { get; set; }
        public DateTime? modified_date { get; set; }
        public string updating_user { get; set; }

    }
}