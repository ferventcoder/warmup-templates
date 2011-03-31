namespace __NAME__.model.auditing
{
    using System;

    public interface Auditable
    {
        DateTime? entered_date { get; set; }
        DateTime? modified_date { get; set; }
        string updating_user { get; set; }
    }
}