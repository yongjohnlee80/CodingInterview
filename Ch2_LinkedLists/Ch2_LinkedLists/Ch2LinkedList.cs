using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Cracking the Coding Interview
/// Chapter 2 Linked Lists
/// </summary>
namespace Ch2_LinkedLists
{
    /// <summary>
    /// Generic Linked List Node (Doubly Linked).
    /// We can also use the LinkedListNode data type provided by STL.
    /// However, to show the internal workings of the whole algorithm, 
    /// Type Node was created.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Node<T>
    {
        // Fields.
        Node<T>? next = null; // pointer to the next link, null if tail.
        Node<T>? prev = null; // pointer to the previous link, null if head.
        T? data = default(T); // data value.

        // Constructor.
        public Node(T data)
        {
            this.data = data;
        }

        /// <summary>
        /// Getters and Setters for all fields.
        /// </summary>
        public T? Data
        {
            // data field is immutable once initialized (no access point)
            get { return data; } 
            // implement setter if it is necessary to modify the value.
        }
        public Node<T>? Next
        {
            get { return next; }
            set { next = value; }
        }
        public Node<T>? Prev
        {
            get { return prev; }
            set { prev = value; }
        }

        /// <summary>
        /// Method: Delete
        /// Delete the current node (by GC) and link the previous and next nodes.
        /// It is advised to use the Link() Method from TLinkedList type.
        /// </summary>
        /// <returns>Returns the next node</returns>
        public Node<T>? Delete()
        {
            if (this.prev != null) this.prev.next = this.next;
            if (this.next != null) this.next.prev = this.prev;
            return this.next;
        }
    }

    /// <summary>
    /// Type: TLinkedList
    /// Not to be confused with LinkList from STL, prefix 'T' was added.
    /// Double Linked List.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TLinkedList<T>
    {
        // Fileds
        Node<T>? head = null;
        Node<T>? tail = null;
        int count = 0;

        /// <summary>
        /// Getters and Setters
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Method: Link
        /// Links two nodes, a and b.
        /// a must be the preceding node where b is the following node.
        /// There are two use cases for this method.
        /// 1. To delete a node, preceding node and following node of the current
        ///     node is linked.
        /// 2. To insert a node, a node is created, then inserted in between the two nodes.
        /// NOTE: Link method can also be used to append; however, use Append method already
        ///         provided in this class, which already handles special cases, such as 
        ///         creating the head node and incrementing the count field.
        /// </summary>
        /// <param name="a">preceding node</param>
        /// <param name="b">following node</param>
        public static void Link(Node<T>? a, Node<T>? b)
        {
            if (a == null)
                    /// (special case)
                    /// When Link method is called to remove head node,
                    /// the following node, b will become the new head node.
            {
                a = b;
                return;
            }
            a.Next = b;
            if(b != null) b.Prev = a;
                    /// Prevents a special case when a is the tail node.
                    /// if a is tail node, b is null.
        }

        /// <summary>
        /// Method: Append
        /// Creates a node at the end of the list.
        /// tail node is then moved to the new end of the list.
        /// </summary>
        /// <param name="data">The value the node contains.</param>
        public void Append(T data)
        {
            /// checks whether the list is empty (special case)
            /// checking tail is null or not isn't necessary, but without it,
            /// we get a warning of potential null reference.
            if (head == null || tail ==null)
            {
                head = new Node<T>(data); // create a node at the head.
                tail = head; // tail is updated.
            } 
            /// otherwise, ...
            else
            {
                tail.Next = new Node<T>(data); // create a node at the end of the list.
                tail.Next.Prev = tail;  /// Link(tail, tail.Next) can also be used instead.
                                        /// However, we only need the latter half of the Link() method;
                                        /// thus, this is simpler and more efficient.
                tail = tail.Next; // update tail node. Very IMPORTANT!
            }
            count++;
        }

        /// <summary>
        /// Method: LoadArray
        /// This method is very helpful when loading a series of data (array) into the list.
        /// </summary>
        /// <param name="data">Multiple Data in Array Format.</param>
        /// <returns>returns the self, giving flexibility for the user 
        /// to engage in further operations with the instance.</returns>
        public TLinkedList<T> LoadArray(T[] data)
        {
            foreach (T item in data)
            {
                Append(item);
            }
            return this;
        }

        /// <summary>
        /// Method: FindNode
        /// This method finds the Node containing the value provided.
        /// It should be noted that if multiple nodes contains the only the first encounterd node
        /// will be returned.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node<T>? FindNode(T value)
        {
            var node = head;
            if (node == null) return null;
            while (!node.Data.Equals(value)) { node = node.Next; }
            return node;
        }

        /// <summary>
        /// Method: WriteContent
        /// Displays the series of the values stored in the list.
        /// </summary>
        /// <param name="reverse">if true, displays from the end in reversed order.</param>
        public void WriteContent(bool reverse = false)
        {
            Node<T>? node = head;
            if (reverse) node = tail; // if reverse mode.
            while (node != null)
            {
                Console.Write($"{node.Data} ");
                node = (reverse ? node.Prev : node.Next);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Method: RemoveDuplicates
        /// This method removes duplicates of values in the list, keeping only the first occurence in the list.
        /// Answer to the Interview Question 2.1
        /// Note that there are two approaches implemented as described in the following.
        /// One of the approaches is naturally commented out.
        /// </summary>
        /// <returns>returns self to the user, giving flexibility to operate with updated instance.</returns>
        public TLinkedList<T> RemoveDuplicates()
        {
            if (head == null || tail == null) return this;
                        // Once again, checking tail is null isn't necessary.

            // Initialize the temp nodes.
            Node<T>? current = head;
            Node<T>? runner = null;

            ///**********************************************************************************************
            /// First Approach. TC: O(n)
            /// This approach uses HashSet to see whether the current data value has been detected earlier.
            /// Note that runner follows behind the current node.
            /// When the current node's data is already in the hash set, then
            /// runner node and current node's next node is linked (deleting the current node)
            ///
            //HashSet<T> set = new HashSet<T>(); // HashSet for data values.
            //while (current != null)
            //// iterate through the list.
            //{
            //    if (set.Contains(current.Data))
            //    // when the set already contains the value,
            //    {
            //        Link(runner, current.Next); // delete the current node.
            //        count--;
            //    }
            //    else
            //    {
            //        // otherwise, add to the set, then runner node follows the current node.
            //        set.Add(current.Data);
            //        runner = current;
            //    }
            //    current = current.Next; // Next!
            //}

            ///**********************************************************************************************
            /// Second Approach. TC: O(n^2)
            /// The interview question also asks us not to use a temporary buffer to solve this problem.
            /// In this case, the runner node iterates ahead of the current node to the end of the list
            /// for each iteration, and when the current node's value is found elsewhere in the list, 
            /// we delete the node. The Spatial Complexity is O(1) in this case.
            /// 
            while (current.Next != null)
                    // iterates through the list.
            {
                /// The runner node runs ahead to the end of list.
                runner = current.Next;
                while(runner != null)
                {
                    if(current.Data.Equals(runner.Data))
                            // if values are the same. Also, the current node cannot possibly be null despite the warning.
                    {
                        Link(runner.Prev, runner.Next); 
                                // Link the previous and following nodes (deletes the runner node)
                        count--;
                    }
                    runner = runner.Next; // runner node iterates.
                }
                current = current.Next; // current node iterates.
            }

            return this; // returns self.
        }

        /// <summary>
        /// Method: GetNodeFromLast
        /// Answer to Interview Question 2.2
        /// This question dictates that we find the kth element from the last in a singly
        /// linked list. Thus, tail node and prev pointer are not used.
        /// This method treats the linked list as if it is a singly linked list.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public Node<T>? GetNodeFromLast(int k)
        {
            int pos = Count - k;
                    // subtract k from the element count.
            if (pos < 0) return null;
                    // k is larger than the length of the list.
            Node<T>? current = head;
                    // starts from the head of the list.
            while(pos-- > 0) current = current.Next;
                    // move the current node to the correct position.
            return current;
        }

        /// <summary>
        /// Method: DeleteMiddleNode
        /// Answer to Interview Question 2.3
        /// This method basically delete a node by linking the previous and next nodes.
        /// </summary>
        /// <param name="node"></param>
        public void DeleteMiddleNode(Node<T> node)
        {
            if(node != null && node != this.head && node != this.tail)
            {
                Link(node.Prev, node.Next);
            }
        }

        /// <summary>
        /// Method: DeleteMiddleNode
        /// Answer to Interview Question 2.3
        /// Another version of the method that finds the node with the value,
        /// then deletes it.
        /// </summary>
        /// <param name="value"></param>
        public void DeleteMiddleNode(T value)
        {
            DeleteMiddleNode(FindNode(value));
        }
    }
}
