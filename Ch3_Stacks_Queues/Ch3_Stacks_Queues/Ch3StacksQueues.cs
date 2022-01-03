using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch3_Stacks_Queues
{
    /// <summary>
    /// Type: SinglyNode
    /// Singly Linked List Node Data Stucture
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SinglyNode<T>
    {
        /// <summary>
        /// Getters & Setters
        /// </summary>
        public T Data { get; set; }

        public SinglyNode<T>? Next { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public SinglyNode(T data) 
        { 
            this.Data = data;
            this.Next = null;
        }
    }

    /// <summary>
    /// Type: SinglyLinkedList
    /// Standard Singly Linked List Data Structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SinglyLinkedList<T>
    {
        /// <summary>
        /// Node Pointers: head & tail.
        /// </summary>
        protected SinglyNode<T>? head = null;
        protected SinglyNode<T>? tail = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SinglyLinkedList() { }

        /// <summary>
        /// Method: Link
        /// Helper Method which links two nodes.
        /// </summary>
        /// <param name="Prev"></param>
        /// <param name="Next"></param>
        public static void Link(SinglyNode<T> Prev, SinglyNode<T> Next)
        {
            /// Special case where Prev pointer is null.
            /// One such circumstance is when the list is empty.
            if (Prev == null)
            {
                Prev = Next;
            } 
            else
            {
                Prev.Next = Next;
            }
        }

        /// <summary>
        /// Method: CompareTValues
        /// A helper method to compare two generic T values
        /// This method converts T values into Int64 values for
        /// the comparison.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        protected static int CompareTValues(T first, T second)
        {
            long a = Convert.ToInt64(first);
            long b = Convert.ToInt64(second);

            return a.CompareTo(b);
        }

        /// <summary>
        /// Method: Append
        /// Appends a new node with data at the end of the list
        /// Then move the tail pointer to the end of the list.
        /// It is declared as virtual since it should be overriden
        /// in different data structures inheriting this class to avoid 
        /// side effects when using this method.
        /// </summary>
        /// <param name="data"></param>
        public virtual void Append(T data)
        {
            /// Special Case, when the list is empty.
            if(head == null || tail == null)
            {
                head = new SinglyNode<T>(data);
                tail = head;
            }
            /// Default Case, ...
            else
            {
                tail.Next = new SinglyNode<T>(data);
                tail = tail.Next;
            }
        }

        /// <summary>
        /// Mehtod: FindNode
        /// Finds a node containing the value, then returns the node pointer.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SinglyNode<T>? FindNode(T value)
        {
            var node = head;
            while (node != null)
            {
                /// When the node is found with the value...
                if(node.Data.Equals(value)) return node;

                node = node.Next;
            }
            return null; // Node with the value not found.
        }

        /// <summary>
        /// Method: FindNodeAt
        /// Finds a node at the position, index from the head.
        /// A helper method to retrieve a desired node in the singly linked list and is
        /// very helpful method; nonetheless, this method was created for the follow up
        /// question of interview quesiton 3.3, Stack of Plates, which requres to pop an
        /// element from nth stack, where this method would find the desired stack.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SinglyNode<T>? FindNodeAt(int index)
        {
            var node = head;
            while(node != null && index-- > 0) { node = node.Next; }
            return node;
        }

        /// <summary>
        /// Method: IsEmpty
        /// Checks whether the list is empty or not.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return head == null;
        }

        /// <summary>
        /// Method: Count
        /// Returns the number of nodes in the list.
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            int count = 0;
            var node = head;
            while(node!= null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }

        /// <summary>
        /// Method: LoadArray
        /// Loads array data into the linked list structure.
        /// </summary>
        /// <param name="data"></param>
        public void LoadArray(T[] data)
        {
            foreach(var item in data)
            {
                Append(item);
            }
        }
    }

    /// <summary>
    /// Type: MyStack
    /// My implementation of a stack data structure based on SinglyLinkedList structure.
    /// This data structure uses LIFO.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MyStack<T> : SinglyLinkedList<T>
    {
        /// <summary>
        /// Top and Bottom pointers
        /// Top -> head.
        /// Bottom -> tail.
        /// </summary>
        public SinglyNode<T> Top
        {
            get { return head; }
            set { head = value; }
        }

        public SinglyNode<T> Bottom
        {
            get { return tail; }
            set { tail = value; }
        }

        /// <summary>
        /// Method: Append
        /// Override the SinglyLinkedList Append method.
        /// To avoid side effects of the method Append being used in
        /// this Stack data structure.
        /// Base Class:LoadArray method also uses Append; thus,
        /// in order to make LoadArray compatible with MyStack data structure,
        /// this method must be overriden.
        /// 
        /// This method is basically pushes an element in the stack.
        /// </summary>
        /// <param name="data"></param>
        public override void Append(T data)
        {
            Push(data);
        }

        /// <summary>
        /// Method: Push
        /// This method pushes an element in the stack.
        /// </summary>
        /// <param name="data"></param>
        public void Push(T data)
        {
            // Create a new Node with the value.
            var node = new SinglyNode<T>(data);

            /// Link node -> Top
            Link(node, Top);

            // Top moves its position to the newly created node.
            Top = node;

            if (Bottom == null) Bottom = Top;
                    /// Special Case, if it is the very first element.
        }

        /// <summary>
        /// Method: Pop
        /// This method retrives the data from the top while removing the node
        /// from the stack.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack Empty");

            T data = Top.Data;
            Top = Top.Next;

            if (Top == null) Bottom = null;
                    /// Special case, when the stack is emptied.
            return data;
        }

        /// <summary>
        /// Method: Peek
        /// This method retrieves the data from the top without removing the node
        /// from the stack.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack Empty");

            return Top.Data;
        }
    }

    /// <summary>
    /// Type: MyQueue
    /// My implementation of Queue data structure based on Singly Linked List.
    /// This data structure uses FIFO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MyQueue<T> : SinglyLinkedList<T>
    {
        /// <summary>
        /// First -> head
        /// Last -> tail
        /// </summary>
        public SinglyNode<T> First
        {
            get { return head; }
            set { head = value; }
        }

        public SinglyNode<T> Last
        {
            get { return tail; }
            set { tail = value; }
        }

        /// <summary>
        /// Method: Append
        /// Override for Queue structure
        /// </summary>
        /// <param name="data"></param>
        public override void Append(T data)
        {
            Enqueue(data);
        }

        /// <summary>
        /// Method: Enqueue
        /// Basically performs the same functionality as the linked list Append method.
        /// </summary>
        /// <param name="data"></param>
        public void Enqueue(T data)
        {
            // Create new node
            var node = new SinglyNode<T>(data);

            if (IsEmpty()) First = Last = node;
            else
            {
                Link(Last, node);
                Last = Last.Next;
            }
        }

        /// <summary>
        /// Method: Dequeue
        /// Basically retrives a data from the head of the list then move
        /// the head to the next node.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue Empty");

            var data = First.Data;
            First = First.Next;
            return data;
        }

        /// <summary>
        /// Method: Peek
        /// Retrieves the data without modifying the head of the list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue Empty");

            return First.Data;
        }
    }

    /// <summary>
    /// Type: MyStackMinMax
    /// Solution to Interview Questions 3.2
    /// This data structure keeps track of min and max values in the stack.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MyStackMinMax<T> : MyStack<T>
    {
        /// <summary>
        /// Stack for Min and Max values.
        /// </summary>
        protected MyStack<T> maxValues = new MyStack<T>();
        protected MyStack<T> minValues = new MyStack<T>();

        /// <summary>
        /// Must be overriden to allow LoadArray method to work correctly.
        /// </summary>
        /// <param name="data"></param>
        public override void Append(T data)
        {
            Push(data);
        }

        /// <summary>
        /// Method: Push
        /// Push a value in the stack while also keeps track of min and max values.
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value)
        {
            if(IsEmpty())
            {
                maxValues.Push(value);
                minValues.Push(value);
            } 
            else
            {
                if(CompareTValues(value, maxValues.Peek())>0) { maxValues.Push(value); }
                if(CompareTValues(value, minValues.Peek())<0) { minValues.Push(value); }
            }
            base.Push(value);
        }

        /// <summary>
        /// Method: Pop
        /// Pop a value in the stack while also maintains the min and max stack values.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T value = base.Pop();

            if(CompareTValues(value, maxValues.Peek()) == 0) maxValues.Pop();
            if(CompareTValues(value, minValues.Peek()) == 0) minValues.Pop();

            return value;
        }

        /// <summary>
        /// Method: PeekMax and PeekMin
        /// Peeks into the min and max stacks.
        /// </summary>
        /// <returns></returns>
        public T PeekMax()
        {
            return maxValues.Peek();
        }

        public T PeekMin()
        {
            return minValues.Peek();
        }
    }

    /// <summary>
    /// Type: SetOfStacks
    /// Answer to the interview question 3.3.
    /// This data structure manages multiple stacks as if it is one.
    /// Each stack size is defined in the constructor.
    /// Also provides a method to pop at a specific stack with PopAt() method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SetOfStacks<T>
    {
        protected int nStacks = 0;
                // the number of stacks. Stack is empty when it is at 0.

        protected MyStack<MyStack<T>> stacks = new MyStack<MyStack<T>>();
                // Constains Stack of Stacks<T>, composition over inheritance for more flexibility.

        protected readonly int stackSize;
                // Constant, the size of each stack.

        /// <summary>
        /// Method: AddNewSet
        /// Adds a stack
        /// </summary>
        protected void AddNewSet()
        {
            stacks.Push(new MyStack<T>());
            nStacks++;
        }

        /// <summary>
        /// Method: RemoveSet
        /// Removes a stack.
        /// </summary>
        protected void RemoveSet()
        {
            stacks.Pop();
            nStacks--;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stackSize"></param>
        public SetOfStacks(int stackSize = 10)
        {
            this.stackSize = stackSize;
            AddNewSet(); // Creates the initial stack.
        }

        /// <summary>
        /// Method: Push
        /// Pushes an element into a stack
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value)
        {
            if(stacks.Peek().GetCount() == stackSize) {  AddNewSet(); }
                        /// Checks the current stack size, and if it has reached
                        /// the maximum number of elements it can contain, a new stack is created.
            stacks.Peek().Push(value);
                        // Push the element in the last created stack.
        }

        /// <summary>
        /// Method: Pop
        /// Pops an element from the last stack.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T data = stacks.Peek().Pop(); // Pop a value from the last stack.

            if (stacks.Peek().GetCount() == 0) { RemoveSet(); }
                    /// If the last stack contains zero element, remove that stack.

            return data;
        }

        /// <summary>
        /// Method: IsEmpty
        /// Checks whether the structure contains any stack.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return nStacks == 0;
        }

        /// <summary>
        /// Method: PopAt
        /// Pops an element from nth stack.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T PopAt(int index)
        {
            var node = stacks.FindNodeAt((nStacks - 1) - index);
            if (node == null) throw new InvalidOperationException("Couldn't locate the stack");
            if (node.Data.GetCount() == 0) throw new InvalidOperationException("This stack doesn't contain anymore element");

            var data = node.Data.Pop();

            if (stacks.Peek().GetCount() == 0) { RemoveSet(); }
            // Just in case the stack is the last one, it should be removed when it contains no more element.
            return data;
        }
    }

    /// <summary>
    /// Type: StackQueue
    /// This data structure implments two stacks to represent a queue
    /// Solution to interview question 3.4
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class StackQueue<T> : MyStack<T>
    {
        /// <summary>
        /// This second stack is the reversed stack to pop the oldest element from the original stack.
        /// </summary>
        protected MyStack<T> queue = new MyStack<T>();

        /// <summary>
        /// Method: Enqueue
        /// Simply pushes an element into the stack.
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(T value)
        {
            Push(value);
        }

        /// <summary>
        /// Method: Dequeue
        /// Slightly more complicated than enqueueing
        /// if the second stack is empty, the first stack is emptied into the second stack
        /// reversing its order to act as queue, which takes out the oldest element first.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            if(queue.IsEmpty())
                /// Checks whether the second stack is empty or not.
            {
                while(!base.IsEmpty()) { queue.Push(Pop()); }
                        // Empties the first stack into the second stack.
                if (queue.IsEmpty()) throw new InvalidOperationException("Nothing to POP");
                else
                {
                    // Pops the oldest element, which is from the second stack.
                    return queue.Pop();
                }
            }
            else
            {
                // Pops the oldeest element, which is from the second stack.
                return queue.Pop();
            }
        }

        /// <summary>
        /// Method: IsEmpty
        /// It is empty only when both stacks are emptied.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (base.IsEmpty() && queue.IsEmpty());
        }
    }
}
