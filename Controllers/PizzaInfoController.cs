using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaInfoController : ControllerBase
    {
        private static readonly PizzaInfo[] TheMenu = new[]
        {
            new PizzaInfo { PizzaName = "The Mighty Meatball11111", Ingredients = "Meatballs and cheese", Cost = 40, InStock = "yes"},
            new PizzaInfo { PizzaName = "Crab Apple", Ingredients = "Dungeness crab and apples", Cost = 35, InStock = "no"},
            new PizzaInfo { PizzaName = "Forest Floor", Ingredients = "Mushrooms, rutabagas, and walnuts", Cost = 20, InStock = "yes"},
            new PizzaInfo { PizzaName = "Don't At Me", Ingredients = "Pineapple, Canadian bacon, jalapeños", Cost = 25, InStock = "yes"},
            new PizzaInfo { PizzaName = "Vanilla", Ingredients = "Sausage and pepperoni", Cost = 15, InStock = "no"},
            new PizzaInfo { PizzaName = "Spice Coming At Ya", Ingredients = "Peppers, chili sauce, spicy andouille", Cost = 50, InStock = "yes"}
        };

        private readonly ILogger<PizzaInfoController> _logger;
        private readonly MyDbContext myDbContext;

        public PizzaInfoController(ILogger<PizzaInfoController> logger, MyDbContext myDbContext)
        {
            _logger = logger;
            this.myDbContext = myDbContext;
        }

         [HttpGet]
         public IEnumerable<PizzaInfo> Get()
         {
             return TheMenu;
         }

        [HttpGet("st")]
        public IActionResult GetST()
        {
            var count = 100;
            using (var context = this.myDbContext)
            {
                // Check if the database exists and migrate if needed
                context.Database.Migrate();

                // Seed data if the database was created or didn't exist
                SeedData(context);

                count = this.myDbContext.Users.Count();


            }


            return Ok(count);
        }
        [HttpPost]
        public IEnumerable<PizzaInfo> Post()
        {
            this.myDbContext.Users.Add(new User() { Name = "sth" });
            this.myDbContext.SaveChanges();
            return TheMenu;
        }

        private static void SeedData(MyDbContext context)
        {
            var entity1 = new User { Name = "asd" };
            var entity2 = new User { Name = "asd" };

            // Add entities to the context
            context.Users.Add(entity1);
            context.Users.Add(entity2);

            // Save changes to persist the data
            context.SaveChanges();
        }
    }
}
