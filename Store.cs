namespace NapilnikTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            warehouse.Show();

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); 
            cart.Show();

            Console.WriteLine(cart.Order());

            cart.Add(iPhone12, 9); 
        }
    }

    class Good
    {
        public string Name { get; private set; }

        public Good(string name)
        {
            Name = name;
        }
    }

    class Warehouse : IShowGoodsproduct, IProductAvailability
    {
        private readonly Dictionary<Good, int> _goods;

        public IReadOnlyDictionary<Good, int> Goods => _goods;

        public Warehouse()
        {
            _goods = new Dictionary<Good, int>();
        }

        public void Delive(Good good, int count)
        {
            _goods.Add(good, count);
        }

        public void Show()
        {
            foreach (var good in _goods)
            {
                Console.WriteLine($"Название = { good.Key.Name }, количество {good.Value}");
            }
        }

        public bool ProductAvailability(Good good, int countInOrder)
        {
            if (Goods.TryGetValue(good, out int countInWarehouse) == false)
                throw new ArgumentOutOfRangeException(nameof(good));

            if (countInWarehouse < countInOrder)
                throw new ArgumentOutOfRangeException(nameof(countInOrder));

            return true;
        }

    }

    class Shop : IProductAvailability
    {
        private Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart Cart()
        {
            return new Cart(this);
        }

        public bool ProductAvailability(Good good, int countInOrder)
        {
            return _warehouse.ProductAvailability(good, countInOrder);
        }
    }

    class Cart : IShowGoodsproduct 
    {
        private readonly  Dictionary<Good, int> _goods;
        private string _paylink;
        private Shop _shop;

        public IReadOnlyDictionary<Good, int> Goods => _goods;
        public Cart(Shop shop)
        {
            _goods = new Dictionary<Good, int>();
            _paylink = "оплата";
            _shop = shop;
        }

        public void Add(Good good, int count)
        {
            if (_shop.ProductAvailability(good, count) == false)
                throw new ArgumentOutOfRangeException(nameof(count));

            _goods.Add(good, count);
            
        }

        public string Order()
        {
            string Paylink = _paylink;

            return Paylink;
        }

        public void Show()
        {
            foreach (var good in _goods)
            {
                Console.WriteLine($"Название = {good.Key.Name}, количество {good.Value}");
            }
        }
    }

    interface IShowGoodsproduct
    {
        public void Show();
    }

    interface IProductAvailability
    {
        public bool ProductAvailability(Good good, int countInOrder);
    }
}

Good iPhone12 = new Good("IPhone 12");
Good iPhone11 = new Good("IPhone 11");

Warehouse warehouse = new Warehouse();

Shop shop = new Shop(warehouse);

warehouse.Delive(iPhone12, 10);
warehouse.Delive(iPhone11, 1);

//Вывод всех товаров на складе с их остатком

Cart cart = shop.Cart();
cart.Add(iPhone12, 4);
cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

//Вывод всех товаров в корзине

Console.WriteLine(cart.Order().Paylink);

cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
