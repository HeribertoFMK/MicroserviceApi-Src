using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var ConnerctionString = "Server=localhost;port=3306;Database=Api;Uid=root;Pwd=190981ju";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(ConnerctionString);
            return new MyContext(optionsBuilder.Options);

        }
    }
}
