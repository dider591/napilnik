class Program
{
    static void Main(string[] args)
    {
        //Выведите платёжные ссылки для трёх разных систем платежа: 
        //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
        //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
        //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}
        Order order = new Order(6, 700);
        List<OrderHash> orderHashes = new List<OrderHash> ();

        orderHashes.Add (new OrderHash(new PaymentSystem1(), order));
        orderHashes.Add(new OrderHash(new PaymentSystem2(), order));
        orderHashes.Add(new OrderHash(new PaymentSystem3(), order));

        foreach (var hash in orderHashes)
        {
            hash.FindHash();
        }
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

class OrderHash
{
    private IPaymentSystem _paymentSystem;
    private Order _order;
    private string _hash;

    public OrderHash(IPaymentSystem paymentSystem, Order order)
    {
        _paymentSystem = paymentSystem;
        _order = order;
        _hash = "";
    }

    public void FindHash()
    {
        _hash = _paymentSystem.GetPayingLink(_order);

        Console.WriteLine(_hash);
    }
}

interface IPaymentSystem
{
    string GetPayingLink(Order order);
}

public class PaymentSystem1 : IPaymentSystem
{
    private string _hash;

    public string GetPayingLink(Order order)
    {
        _hash = "MD5" + order.Id.ToString();
        return $"pay.system1.ru/order?amount=12000RUB&hash={_hash}";
    }
}

public class PaymentSystem2 : IPaymentSystem
{
    private string _hash;

    public string GetPayingLink(Order order)
    {
        _hash = "MD5" + order.Id.ToString() + order.Amount;
        return $"order.system2.ru/pay?hash={_hash}";
    }
}

public class PaymentSystem3 : IPaymentSystem
{
    private string _hash;
    private int _secretKey = 456546;

    public string GetPayingLink(Order order)
    {
        _hash = "SHA-1" + order.Id.ToString() + _secretKey.ToString();
        return $"system3.com/pay?amount=12000&curency=RUB&hash={_hash}";
    }
}