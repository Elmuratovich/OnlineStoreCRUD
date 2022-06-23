using Microsoft.EntityFrameworkCore;
using CodeSimpleCRUD.Models;

namespace CodeSimpleCRUD.Data
{
    public class CodeSimpleContext:DbContext
    {
        public CodeSimpleContext(DbContextOptions<CodeSimpleContext> options): base(options) { }

        public virtual DbSet<Product> Products { get; set; }

    }

}
