using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easy_master_api.Models
{
  public class MasterModel
  {
    [Key]
    public Guid id { get; set; }
    public string hn { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string tel { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public bool is_active { get; set; }

  }
}
