using ProductMangement;

class Program{
    public static void Main(string[] args)
    {
        IProductRepository productRepository = new ProductService();
        Menu(productRepository);
    }
    static void Menu(IProductRepository productRepository){
        while(true){
            Console.WriteLine("Product Management");
            Console.WriteLine("1. Create");
            Console.WriteLine("2.Create Product");
            Console.WriteLine("3. Read Product");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Select an option: ");

            string choice = Console.ReadLine();
            switch(choice){
                case"1"://goi ham list all
                ListAllProduct(productRepository);
                break;
                case"2"://goi ham create
                CreateProduct(productRepository);
                break;
                case"3":
                ReadProduct(productRepository);
                break;
                case"4":
                UpdateProduct(productRepository);
                break;
                case"5":                
                return;
                default:
                Console.WriteLine("Invalid choice. Psl try again");
                break;
            }
        }
    }
    static void ListAllProduct(IProductRepository productRepository){
        List<Product> products = productRepository.GetAll();
        foreach(var product in products){
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price} , Description: {product.Description}");
        }
    }
    static void CreateProduct(IProductRepository productRepository){
        Console.WriteLine("Enter Product name:");
        string name = Console.ReadLine();
        Console.WriteLine("Enter Product price:");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.WriteLine("Enter Product description:");
        string description = Console.ReadLine();

        Product newProduct = new Product{
            Name = name,
            Price = price,
            Description = description
        };
        productRepository.Create(newProduct);
        Console.WriteLine("Product create success!");
    }
    static void ReadProduct(IProductRepository productRepository){
        Console.WriteLine("Enter Product Id:");
        int id = int.Parse(Console.ReadLine());

        Product product = productRepository.Read(id);
        if(product != null){
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
        }
        else{
            Console.WriteLine("Product not found!");
        }
    }
    static void UpdateProduct(IProductRepository productRepository){
        Console.WriteLine("Enter Product Id:");
        int id = int.Parse(Console.ReadLine());
        Product existingProduct = productRepository.Read(id);
        if(existingProduct != null){
            Console.WriteLine("Enter Product name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Product price:");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Product description:");
            string description = Console.ReadLine();

            if (!string.IsNullOrEmpty(name)){
                existingProduct.Name = name;
            }
            if (string.IsNullOrEmpty(price) && decimal.TryParse(price, out decimal price)){
                existingProduct.Price = price;
            }
            if (!string.IsNullOrEmpty(description)){
                existingProduct.Description = description;
            }

            productRepository.Update(existingProduct);
            Console.WriteLine("Product update success!");
        }
        else{
            Console.WriteLine("Product not found!");
        }
    }
}        