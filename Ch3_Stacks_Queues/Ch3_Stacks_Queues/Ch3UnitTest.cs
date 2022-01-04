using NUnit.Framework;

namespace Ch3_Stacks_Queues
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UnitTestforStack()
        {
            var stack = new MyStack<int>();
            
            for(int i = 1; i <= 10; i++)
                stack.Push(i);

            while(!stack.IsEmpty())
            {
                System.Console.WriteLine(stack.Pop());
            }
        }

        [Test]
        public void UnitTestforQueue()
        {
            var queue = new MyQueue<int>();

            for(int i = 1; i <= 10; i++)
                queue.Enqueue(i);
            
            while(!queue.IsEmpty()) 
            {
                System.Console.WriteLine(queue.Dequeue());
            }
        }

        /// <summary>
        /// Interview Question 3.2
        /// </summary>
        [Test]
        public void StackMinMax()
        {
            var stack = new MyStackMinMax<int>();

            int[] data = { 10, 5, 3, 2, 6, 1, 15, 20, 8, 4 };
            stack.LoadArray(data);

            System.Console.WriteLine($"Min : {stack.PeekMin()}");
            System.Console.WriteLine($"Max : {stack.PeekMax()}");

            for(int i = 1; i <= 6; i++) stack.Pop();

            System.Console.WriteLine($"Min : {stack.PeekMin()}");
            System.Console.WriteLine($"Max : {stack.PeekMax()}");
        }

        /// <summary>
        /// Interview Question 3.3
        /// </summary>
        [Test]
        public void StackOfPlates()
        {
            var stack = new SetOfStacks<int>();

            for (int i = 0; i < 32; i++)
            {
                stack.Push(i);
            }

            System.Console.WriteLine($"Poping two elements from the very first stack: ");
            System.Console.WriteLine(stack.PopAt(0));
            System.Console.WriteLine(stack.PopAt(0));

            while(!stack.IsEmpty())
            {
                System.Console.WriteLine(stack.Pop());
            }
        }

        /// <summary>
        /// Interview Question 3.4
        /// </summary>
        [Test]
        public void QueueViaStack()
        {
            var queue = new StackQueue<int>();

            for (var i = 1; i <= 10; i++)
            {
                queue.Enqueue(i);
            }

            while(!queue.IsEmpty())
            {
                System.Console.WriteLine(queue.Dequeue());
            }
        }

        /// <summary>
        /// Interview Question 3.5
        /// </summary>
        [Test]
        public void SortStack()
        {
            var stack = new MyStack<int>();

            int[] data = { 5, 6, 3, 1, 4, 2, 7 };

            stack.LoadArray(data);
            stack.Sort();

            while (!stack.IsEmpty())
            {
                System.Console.WriteLine(stack.Pop());
            }
        }

        /// <summary>
        /// Interview Question 3.6
        /// </summary>
        [Test]
        public void AnimalShelter()
        {
            AnimalShelter SPCA = new AnimalShelter();
            SPCA.Enqueue(new Cat("Tiger", "Persian Cat"));
            SPCA.Enqueue(new Cat("Roma", "Bobcat"));
            SPCA.Enqueue(new Dog("Poo", "Golden Retriver"));
            SPCA.Enqueue(new Dog("Digger", "Presa Canary"));


            var adopt = SPCA.DequeueAny();
            PrintPet(adopt);

            adopt = SPCA.DequeueDog();
            PrintPet(adopt);

            adopt = SPCA.DequeueAny();
            PrintPet(adopt);

            void PrintPet(Pet p)
            {
                System.Console.WriteLine($"Name: {p.Name}, Desc: {p.Description}, Type: {p.Type()}");
            }
        }
    }
}