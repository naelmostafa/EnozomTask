
using Data;
using Models;

namespace Enozom.Seeder
{
    public class DataSeeder
    {
        private readonly DataContext _dataContext;

        public DataSeeder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedData()
        {
            if (!_dataContext.Hotels.Any())
            {
                var hotels = new List<Hotel>
                {
                    
                    new Hotel
                    {
                        Name = "Sheraton Hotel",
                        Rooms = new List<Room>
                        {
                            new Room
                            {
                                Name = "Double Room See View",
                                Price = 200
                            },
                            new Room
                            {
                                Name = "Single Room See View",
                                Price = 150
                            },
                            new Room
                            {
                                Name = "Double Room City View",
                                Price = 170
                            },
                            new Room
                            {
                                Name = "Single Room City View",
                                Price = 120
                            }
                        }
                    },
                    new Hotel
                    {
                        Name = "Helnan Hotel",
                        Rooms = new List<Room>
                        {
                            new Room
                            {
                                Name = "Double Room Garden View",
                                Price = 100
                            },
                            new Room
                            {
                                Name = "Single Room Garden View",
                                Price = 90
                            },
                            new Room
                            {
                                Name = "Double Room Pool View",
                                Price = 120
                            },
                            new Room
                            {
                                Name = "Single Room Pool View",
                                Price = 110
                            }
                        }
                    },
                    new Hotel
                    {
                        Name = "Tolip Hotel",
                        Rooms = new List<Room>
                        {
                            new Room
                            {
                                Name = "Double Room See Standard",
                                Price = 80
                            },
                            new Room
                            {
                                Name = "Single Room See Standard",
                                Price = 70
                            },
                            new Room
                            {
                                Name = "Double Room City Deluxe",
                                Price = 100
                            },
                            new Room
                            {
                                Name = "Single Room City Deluxe",
                                Price = 150
                            }
                        }
                    }
               
                };
                _dataContext.Hotels.AddRange(hotels);
                _dataContext.SaveChanges();
            }
        }
    }
}