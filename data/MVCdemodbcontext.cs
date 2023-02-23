using Microsoft.EntityFrameworkCore;
using student_data_management.Models.Domain;

namespace student_data_management.data
{
    public class MVCdemodbcontext : DbContext
    {
        public MVCdemodbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

    }
}
