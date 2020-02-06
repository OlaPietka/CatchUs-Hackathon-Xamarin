using SQLite;
using System;

namespace CatchUs.Model
{
    [Table("PasswordRecovery")]
    public class PasswordRecovery
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime Time { get; set; }
    }
}
