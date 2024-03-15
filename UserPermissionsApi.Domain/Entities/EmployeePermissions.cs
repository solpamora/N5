using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserPermissionsApi.Domain.Entities
{
    [Table("EmployeePermissions")]
    public class EmployeePermissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EmployeePermissionId")]
        public int EmployeePermissionId { get; set; }

        [Required]
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("PermissionTypeId")]
        public int PermissionTypeId { get; set; }
        
        public PermissionType PermissionType { get; set; }
    }
}
