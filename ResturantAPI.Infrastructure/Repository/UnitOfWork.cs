 
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
 
 


namespace ResturantAPI.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;


        private ICustomerRepository _customer;
        //private IUserRepository _user;
        //private IRepository<ServiceCategory, int> _serviceCategories;
        //private IRepository<SubCategory, int> _subCategories;

        public UnitOfWork(DatabaseContext context)
        {

            _context = context;

        }
       

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if(_customer is null)
                {
                    _customer = new CustomerRepository(_context);
                }

                return _customer;
            }
        }
        private IGeneralRepository<Restaurant, int> restaurant;
        public IGeneralRepository<Restaurant, int> RestaurantRepository
        {
            get
            {
                if (restaurant is null)
                {
                    restaurant = new GeneralRepository<Restaurant, int>(_context);
                }
                return restaurant;
            }
        }
        
        private IGeneralRepository<Delivery, int> delivery;
        public IGeneralRepository<Delivery, int> DeliveryRepository
        {
            get
            {
                if (delivery is null)
                {
                    delivery = new GeneralRepository<Delivery, int>(_context);
                }
                return delivery;
            }
        }

        private IGeneralRepository<Address, int> address;
        public IGeneralRepository<Address, int> AddressRepository
        {
            get
            {
                if (address is null)
                {
                    address = new GeneralRepository<Address, int>(_context);
                }
                return address;
            }
        }


        private IOrderRepository Order;

        public IOrderRepository OrderRepository
        {
            get
            {
                if (Order is null)
                {
                    Order = new OrderRepository(_context);
                }
                return Order;
            }
        }

       

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
