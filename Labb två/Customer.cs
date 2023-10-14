public class Customer
{
    public string Name { get; private set; }
    private string Password { get; set; }
    private List<Product> _cart;

    public List<Product> Cart
    {
        get { return _cart; }
    }

    public Customer(string name, string password)
    {
        Name = name;
        Password = password;
        this._cart = new List<Product>();
    }

    public bool CheckPassword(string password)
    {
        return Password == password;
    }

    public void AddToCart(Product product)
    {
        _cart.Add(product);
    }

    public void RemoveFromCart(Product product)
    {
        _cart.Remove(product);
    }

    public double CartTotal()
    {
        double total = 0.0;
        foreach (var product in _cart)
        {
            total += product.Price;
        }

        return total;
    }

    public override string ToString()
    {
        return "Customer: " + Name + "\nPassword: " + Password + "\nShopping Cart : " + CartTotal();
    }
}