using FlexiCart.Model.Entities;
using FlexiCart.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FlexiCart.Service
{
    public class FakeDbContext
    {
        private List<Promotion> _promotions = new List<Promotion>();
        private List<Product> _products = new List<Product>();


        public FakeDbContext()
        {
            LoadFakeData();
        }
        private void LoadFakeData()
        {
            LoadProducts();
            LoadPromotions();
        }

        private void LoadPromotions()
        {
            _promotions = new List<Promotion>()
            {
                new SimplePromotion () {
                    RuleID =1 , RuleName ="3 Of A's" , Description = "3 Of A's for 130 " , IsActive =true ,
                    Group =   new Criteria () { ProductName ="A" , Qty =3 , OfferedPrice =130}
                },
                new SimplePromotion () {
                    RuleID =2 , RuleName ="2 Of B's" , Description = "2 Of B's for 45 " , IsActive =true  ,
                     Group =   new Criteria () { ProductName ="B" , Qty =2 , OfferedPrice =45}
                },
                 new GroupedPromotion () {
                    RuleID =3 , RuleName =" C & D " , Description = "C & D for 30 " , IsActive =true ,
                     Groups = new List<Criteria>()
                     {
                         new Criteria () { ProductName ="C" , Qty = 1  ,OfferedPrice =0},
                         new Criteria () { ProductName ="D" , Qty = 1 , OfferedPrice =  30 }

                     }
                 }
            };
        }

        private void LoadProducts()
        {
            _products = new List<Product>()
            {
                new Product () { SKU ="A" , UnitPrice =50 } ,
                new Product () { SKU ="B" , UnitPrice =30 } ,
                new Product () { SKU ="C" , UnitPrice =20 } ,
                new Product () { SKU ="D" , UnitPrice =15 } ,
            };
        }


        public List<Product> Products { get { return _products; } set { _products = value; } }

        public List<Promotion> Promotions { get { return _promotions; } set { _promotions = value; } }




    }

    public class ProductRepository : IRepository<Product>
    {
        protected readonly FakeDbContext Context;

        public ProductRepository(FakeDbContext context)
        {
            Context = context;
        }

        public void Add(Product entity)
        {
            Context.Products.Add(entity);
        }

        public void AddRange(IEnumerable<Product> entities)
        {
            Context.Products.AddRange(entities);
        }

        public Product Get(int id)
        {
            return Context.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public void Remove(Product entity)
        {
            Context.Products.Remove(entity);
        }
    }


    public class PromotionRepository : IPromotionRepository<Promotion>
    {
        protected readonly FakeDbContext Context;

        public PromotionRepository(FakeDbContext context)
        {
            Context = context;
        }

        public void Add(Promotion entity)
        {
            Context.Promotions.Add(entity);
        }

        public void AddRange(IEnumerable<Promotion> entities)
        {
            Context.Promotions.AddRange(entities);
        }

        public Promotion Get(int id)
        {
            return Context.Promotions.FirstOrDefault(p => p.RuleID == id);
        }

        public IEnumerable<Promotion> GetAll()
        {
            return Context.Promotions.ToList();
        }

        public void Remove(Promotion entity)
        {
            Context.Promotions.Remove(entity);
        }

        public IEnumerable<Promotion> GetActiveRules()
        {
            var filteredRules = Context.Promotions.Where(r => r.IsActive == true).ToList();
            return filteredRules;
        }

        public IEnumerable<Promotion> GetFestivalPromotions()
        {
            throw new System.NotImplementedException();
        }
    }




}
