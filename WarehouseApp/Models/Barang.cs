using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseApp.Models;

[Table("m_barang")]
public class Barang
{
    [Key]
    [Column("kode_barang")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int KodeBarang { get; set; }

    [Column("nama_barang", TypeName = "NVarchar(255)")]
    [DisplayName("Nama Barang")]
    [Required]
    public string NamaBarang { get; set; }
    
    [Column("harga_barang")]
    [DisplayName("Harga Barang")]
    [Required]
    public long HargaBarang { get; set; }

    [Column("jumlah_barang")]
    [DisplayName("Jumlah Barang")]
    [Required]
    public int JumlahBarang { get; set; }
    
    [Column("expired_barang")] 
    [DisplayName("Expired Barang")]
    [Required]
    public DateTime ExpiredBarang { get; set; }
    
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Column("updated_at")] 
    public DateTime? UpdatedAt { get; set; }

    [Column("is_active")] 
    public bool IsActive { get; set; } = true;

    [ForeignKey("Gudang")]
    [Column("kode_gudang")]
    public int KodeGudang { get; set; } 
    public virtual Gudang? Gudang { get; set; }
}