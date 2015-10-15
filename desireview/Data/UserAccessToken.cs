using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desireview.Data
{
    public class UserAccessToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        [NotMapped]
        public bool ErrorFlag { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }

    }
}
