using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: The Enqueue method shall add an item (which contains both a value and a priority) to the queue.
    // Expected Result: The item shall be added to the queue.
    // Defect(s) Found: None
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: The Dequeue method shall remove and return the item with the highest priority.
    // Expected Result: The item with the highest priority shall be removed and returned.
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        var priorityQueue2 = new PriorityQueue();
        priorityQueue2.Enqueue("A", 2);
        priorityQueue2.Enqueue("B", 3);
        priorityQueue2.Enqueue("C", 1);
        Assert.AreEqual("B", priorityQueue2.Dequeue());

    }

    [TestMethod]
    // Scenario: If there is more than one item with the highest priority, then the item closest to the front of the queue
    //will be removed and it's value returned.
    // Expected Result: The item with the highest priority closest to the front of the queue shall be removed and returned.
    // Defect(s) Found: None, it worked perfectly. I believe this was accounted for when I updated the dequeue method for the last test.
    public void TestPriorityQueue_3()
    {
        var priorityQueue3 = new PriorityQueue();
        priorityQueue3.Enqueue("A", 2);
        priorityQueue3.Enqueue("B", 3);
        priorityQueue3.Enqueue("C", 1);
        priorityQueue3.Enqueue("D", 3);
        Assert.AreEqual("B", priorityQueue3.Dequeue());

    }

    [TestMethod]
    // Scenario: If the queue is empty then an error exception shall be thrown. This exception should be an InvalidOPerationException with a message of "The queue is empty."
    // Expected Result: The message will appear.
    // Defect(s) Found: None, it worked perfectly. If we hadn't needed to check the text of the message we could have just done Assert.ThrowsException<InvalidOperationException>.
    public void TestPriorityQueue_4()
    {
        var priorityQueue4 = new PriorityQueue();
        //Assign message returned when queue is empty to a variable so we can check the content of the message.
        var exception = Assert.ThrowsException<InvalidOperationException>(() =>
            priorityQueue4.Dequeue()
        );
        
        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}