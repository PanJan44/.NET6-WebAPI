using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using web_api_net5.Entities;

namespace web_api_net5
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigarations = _dbContext.Database.GetPendingMigrations();

                if (pendingMigarations != null && pendingMigarations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }


                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },

                new Role()
                {
                    Name="Manager"
                },

                new Role()
                {
                    Name= "Admin"
                }
            };

            return roles;
        }


        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken",
                    ContactEmail = "kfc@example.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.20M,
                        }
                    },
                    Address = new Address()
                    {
                        City = "Bielsko-biała",
                        Street = "Krakowska 3",
                        PostalCode = "43‑300"
                    }
                },
                new Restaurant()
                {
                    Name="McDonald",
                    Category = "Fast Food",
                    Description ="This is mcdonald",
                    ContactEmail="mc@donald.com",
                    HasDelivery=true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Two4You",
                            Price = 4.80M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Bielsko-biała",
                        Street = "Krakowska 44",
                        PostalCode = "43‑300"
                    }
                }
            };

            return restaurants;
        }
    }
}
