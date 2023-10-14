

public class Shop
{
    private List<Customer> customers;
    private Customer CurrentCustomer { get; set; }
    private List<Product> Products { get; set; }

    public Shop()
    {
        customers = new List<Customer>
            {
                new Customer("Knatte", "123"),
                new Customer("Fnatte", "321"),
                new Customer("Tjatte", "213"),
                new Customer("Saher",  "050")

            };
        Products = new List<Product>
            {
                new Product { Name = "Apple", Price = 10.0 },
                new Product { Name = "Sausage", Price = 20.0 },
                new Product { Name = "Drink", Price = 15.0 },
                new Product { Name = "Banana", Price = 9.0 },
                new Product { Name = "Bread", Price = 11.0 },
                new Product { Name = "Beef Burger", Price = 38.0 }

            };
    }

    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Log In");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void Login()
    {
        //Enter Name and password
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        //Check if the customer already existes
        Customer customer = customers.Find(cust => cust.Name == name);

        if (customer == null)
        {
            Console.WriteLine("Customer does not exist.");
            Console.Write("Do you want to register a new customer? (yes/no): ");
            string registerChoice = Console.ReadLine();

            if (registerChoice == "yes")
            {
                Register();
            }
        }
        else
        {
            if (customer.CheckPassword(password))
            {
                Console.WriteLine("\n############## W E L C O M E ############");
                Console.WriteLine("Hello " + customer.Name);
                CurrentCustomer = customer;
                ShopMenu();
            }
            else
            {
                Console.WriteLine("Incorrect password. Please try again.");
            }
        }
    }

    private void Register()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        Customer newCustomer = new Customer(name, password);
        customers.Add(newCustomer);
        Console.WriteLine("Customer registered successfully.");
    }

    private void ShopMenu()
    {
        while (true)
        {
            Console.WriteLine("\nShop Menu:");
            Console.WriteLine("1. Products to buy");
            Console.WriteLine("2. View Shopping Cart");
            Console.WriteLine("3. Checkout");
            Console.WriteLine("4. Log Out");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayProducts();
                    break;
                case "2":
                    ViewCart();
                    break;
                case "3":
                    Checkout();
                    break;
                case "4":
                    CurrentCustomer = null;
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void DisplayProducts()
    {
        Console.WriteLine("\nAvailable Products:");
        foreach (var product in Products)
        {
            Console.WriteLine(product);
        }

        Console.Write("Enter the product name to add to your cart: ");
        string productName = Console.ReadLine();
        Product selectedProduct = Products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (selectedProduct != null)
        {
            CurrentCustomer.AddToCart(selectedProduct);
            Console.WriteLine(selectedProduct.Name + " added to the cart.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    private void ViewCart()
    {
        Console.WriteLine("\nShopping Cart:");
        List<string> productsName = new List<string>();
        foreach (var product in CurrentCustomer.Cart)
        {
            if (productsName.Contains(product.Name)) continue;
            int quantity = CurrentCustomer.Cart.Count(p => p.Name == product.Name);
            double totalItemPrice = quantity * product.Price;
            Console.WriteLine($"{product.Name} {quantity} pcs SEK {product.Price}/pc = SEK {totalItemPrice}");
            productsName.Add(product.Name);
        }

        double totalCartPrice = CurrentCustomer.CartTotal();
        Console.WriteLine($"Total: SEK {totalCartPrice}");
    }

    private void Checkout()
    {
        foreach (var product in CurrentCustomer.Cart)
        {
            int quantity = CurrentCustomer.Cart.Count(p => p.Name == product.Name);
            double totalItemPrice = quantity * product.Price;
            
            double totalCartPrice = CurrentCustomer.CartTotal();
            Console.WriteLine("Total price is: " + totalCartPrice + " SEK");

            Console.WriteLine("Enter the cash money: ");

            double cash = double.Parse(Console.ReadLine());

            if (cash >= totalCartPrice)
            {
                cash -= totalCartPrice;

                Console.WriteLine("Change = " + cash + " SEK");
                break;
            }
            else
            {
                Console.WriteLine("You need to pay the exact amount of money or more! ");
            }
        }

       
        Console.WriteLine("****** Thanks for your visit *******");

    }

}
