/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: User shall specify a maximum size for the queue. If the size is invalid then default should be 10. The queue shall be created with that size.
        
        var test1 = new CustomerService(0);
        // Expected Result: 
        Console.WriteLine("Test 1");
        Console.WriteLine(test1);

        // Defect(s) Found: None--it worked as expected.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: AddNewCustomer method shall enqueue a new customer into the queue. The ServeCustomer function shall dequeue the next customer and display the information.
        // Expected Result: 
        Console.WriteLine("Test 2");

        //Create new instance of CustomerService with a max size of 2
        var test2 = new CustomerService(2);
        //Add a new customer to the queue
        test2.AddNewCustomer();
        //Dequeue the customer to test if teh AddNewCustomer method worked correctly       
        test2.ServeCustomer();
        
        // Defect(s) Found: Dequee method was removing the customer from the queue before displaying the information.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: If the queue is full when trying to add a customer then an error message will be displayed.
        // Expected Result: 
        Console.WriteLine("Test 3");

        //Create new instance of CustomerService with a max size of 2
        var test3 = new CustomerService(2);
        //Add a new customer to the queue
        test3.AddNewCustomer();
        test3.AddNewCustomer();
        test3.AddNewCustomer(); //This should display an error message since the queue is full     


        // Defect(s) Found: Error message is not displayed. Changed > to >= in the if statement in the AddNewCustomer method to fix this issue.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: If the queue is empty when trying to serve a customer then an error message will be displayed.
        // Expected Result: 
        Console.WriteLine("Test 4");

        //Create new instance of CustomerService with a max size of 2
        var test4 = new CustomerService(2);
        //Attempt to serve a customer when the queue is empty
        test4.ServeCustomer(); 

        // Defect(s) Found: Error message is displayed. Changed to if statement to give more clear reasoning.

        Console.WriteLine("=================");

    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) { //Added = sign so that it will not allow more customers to be added than the max size of the queue
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {        
        if (_queue.Count == 0) { //Added if statement to make error message more descriptive and clear.
            Console.WriteLine("No customers in queue.");
            return;
        }
        var customer = _queue[0];
        _queue.RemoveAt(0); //Switched order so customer is defined before being removed from the queue
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}