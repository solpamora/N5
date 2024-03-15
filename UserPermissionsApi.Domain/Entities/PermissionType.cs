using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserPermissionsApi.Domain.Entities
{
    [Table("PermissionTypes")]
    public class PermissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PermissionTypeId")]
        public int PermissionTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        public ICollection<EmployeePermissions> EmployeePermissions { get; set; }
    }
}
