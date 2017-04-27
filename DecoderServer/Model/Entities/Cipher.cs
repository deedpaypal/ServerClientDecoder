using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecoderServer.Model.Entities
{
  
    [Table("testschema.cipher")]
    public partial class Cipher
    {
        [Key]
        [StringLength(1)]
        public string From { get; set; }

        [StringLength(1)]
        public string To { get; set; }
    }
}
