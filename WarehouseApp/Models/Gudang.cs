using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseApp.Models;

[Table("m_gudang")]
public class Gudang
{
    [Key, Column("kode_gudang")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int KodeGudang { get; set; }

    [Column("nama_gudang", TypeName = "NVarchar(255)")]
    [DisplayName("Nama Gudang")]
    [Required]
    public string NamaGudang { get; set; }
    
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Column("updated_at")] 
    public DateTime? UpdatedAt { get; set; }

    [Column("is_active")] 
    public bool IsActive { get; set; } = true;
}