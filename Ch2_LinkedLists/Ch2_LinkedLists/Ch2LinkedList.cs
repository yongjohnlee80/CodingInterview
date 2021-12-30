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
    internal class Node<T> : IComparable<Node<T>>
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
            set { data = value; }
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

        public int CompareTo(Node<T>? other)
        {
            if (other == null) return 1;
            return Convert.ToInt32(data).CompareTo(Convert.ToInt32(other.data));
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
        protected Node<T>? head = null;
        protected Node<T>? tail = null;
        protected int count = 0;

        public Node<T>? Head { get { return head; } }
        public Node<T>? Tail 
        { 
            get { return tail; } 
            set { tail = value; } // !! This was an added security breach to allow Question 2_8 !!
        }

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
        /// Method: Append
        /// Second version of Append
        /// Append an already existing node at the end of the list.
        /// </summary>
        /// <param name="node"></param>
        public void Append(Node<T> node)
        {
            /// Eliminates side effects by isolating the node
            /// if the node is from an exisiting linked list.
            node.Prev = null;
            node.Next = null;
            
            /// if this is the very first element...
            if(head == null || tail == null)
            {
                head = node;
                tail = node;
            }
            /// otherwise, ...
            else
            {
                tail.Next = node;
                tail.Next.Prev = tail;

                tail = tail.Next; // update the tail node.
            }
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
            /// The following codes are implemented as doubly linked list...
            //if(node != null && node != this.head && node != this.tail)
            //{
            //    Link(node.Prev, node.Next);
            //}

            /// The Following codes are implemented as singly linked list...
            if(node.Prev == null || node.Next == null) return;
            Node<T> next = node.Next;

            node.Data = next.Data;
            node.Next = next.Next;

            node.Prev = next.Prev;  /// this line won't be necessary for singly linked list but 
                                    /// we have doubly linked list here so we have it here for compatibility
                                    /// with the rest of the code
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

        /// <summary>
        /// Method: Partition
        /// Answer to Interview Question 2.4
        /// Partition the list by a given value.
        /// Nodes containing smaller values will be placed on the left while others are 
        /// placed on the right.
        /// For simplicity, the generic value, T, will be treated as int32
        /// </summary>
        /// <param name="value"></param>
        public void Partition(T value)
        {
            /// linked lists for smaller values and bigger values.
            TLinkedList<T> small = new TLinkedList<T>();
            TLinkedList<T> big = new TLinkedList<T>();

            var node = head; // let's begin with the head node.

            while(node != null)
                // until the last element. The Big O is O(n).
            {
                var next = node.Next; // keep the next node before any modification to the node.

                /// the followings are very important steps to isolate the node
                /// from the previous list. The followings are commented out only because
                /// the Append method used here automatically performs the isolation procedure.
                //node.Prev = null;
                //node.Next = null;

                if (Convert.ToInt32(node.Data) < Convert.ToInt32(value))
                    /// Comparing T types as if they are Int32.
                    /// In real life, we are most likely to accomodate other data type situations.
                {
                    small.Append(node); 
                            // append the node to the small list if it is smaller than the partitioning value.
                }
                else
                {
                    big.Append(node); // otherwise, append it to the big list.
                }
                node = next; // NEXT!!!
            }

            Link(small.tail, big.head); // Connect the small and big lists.

            /// update this linked list with the NEW ORDER of nodes.
            this.head = small.head;
            this.tail = big.tail;
        }

        /// <summary>
        /// Method: SumLists
        /// Answer to Interview Question 2.5
        /// two lists containing single digit numbers as nodes are added together and 
        /// produce a third linked list that contains the resulting sum of the two lists.
        /// Simply put, each list is a representation of a single integer value where each node
        /// is considered as a single digit.
        /// These lists are in reverse order where 1's digit is at the head of the list.
        /// So read them in reverse order when you print the contents of the lists.
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static TLinkedList<int> SumLists(TLinkedList<int> list1, TLinkedList<int> list2)
        {
            var result = new TLinkedList<int>(); // This is where the output is going to be.

            Node<int> node1 = list1.head; // head of the first list.
            Node<int> node2 = list2.head; // head of the second list.

            int sum = 0; // sum of each digit place.
            int carry = 0; // carry over value.

            /// This loop continues until both lists are completely spent
            /// with an exception of last nodes producing an carryover value.
            /// In that exception one more iteration is to be done to accomodate the 
            /// last digit in the new list.
            while (node1 != null || node2 != null || sum > 0)
            {
                /// Take a digit from the first list
                if(node1 != null)
                {
                    sum += node1.Data;
                    node1 = node1.Next;
                }
                /// take a digit from the second list.
                if(node2 != null)
                {
                    sum += node2.Data;
                    node2 = node2.Next;
                }
                /// If a carryover (overflow) is produced, ...
                if(sum > 9)
                {
                    carry = sum / 10; // compute the carryover portion.
                    sum = sum % 10; // only a single digit number in the resulting digit.
                }

                /// Create a node with the resulting digit in the output list.
                result.Append(sum);

                /// resetting the flag variables
                if(carry > 0)
                    /// if there was a carryover value then
                    /// it's carried over to the sum variabler for 
                    /// next iteration.
                {
                    sum = carry;
                    carry = 0;
                }
                else
                    /// otherwise, reset sum to zero.
                {
                    sum = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// Method: SumLists2
        /// Answer to the question 2.5 second half.
        /// The differnece between the first part is that the digits are in normal order
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static TLinkedList<int> SumLists2(TLinkedList<int> list1, TLinkedList<int> list2)
        {
            /// resulting list.
            TLinkedList<int> result = new TLinkedList<int>();

            Node<int> node1 = list1.head; // head of the first list.
            Node<int> node2 = list2.head; // head of the second list.

            int sum = 0; // sum of each digit place.
            int carry = 0; // carry over value
                           // .
            /// The following codes deals with uneven lengths between the two lists.
            /// longer list contains higher digit values so that they should be taken care of 
            /// before entering the summing part of the code.
            int k = list1.Count - list2.Count;
            while (k != 0)
            {
                if (k < 0) // if list2 is longer, ...
                {
                    /// take a digit from list2 and put it in the resulting list.
                    result.Append(node2.Data);
                    node2 = node2.Next;
                    k++;
                }
                else // if list1 is longer, ...
                {
                    /// take a digit from list1 and put it in the resulting list.
                    result.Append(node1.Data);
                    node1 = node1.Next;
                    k--;
                }
            }

            while (node1 != null) // Now the list contains the same number of nodes
            {
                /// For any carryover values are multiplied by 10 to
                /// represent they are one digit higher.
                sum *= 10;

                /// Take a digit from the first list
                sum += node1.Data;
                node1 = node1.Next;
                
                /// take a digit from the second list.
                sum += node2.Data;
                node2 = node2.Next;

                /// if the current digit produces carryover, 
                /// put it in the resulting list
                if (sum > 9)
                {
                    result.Append(sum / 10);
                    sum = sum % 10;
                }
            }
            /// Must take care of the very last digit as shown below.
            result.Append(sum);
            
            return result;
        }

        /// <summary>
        /// Method: isEqual
        /// Method checks the contents of the list.
        /// Returns true if they are identical.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool isEqual(TLinkedList<T> other)
        {
            if(this.Count != other.Count) return false;
            Node<T> node1 = this.head;
            Node<T> node2 = other.head;

            while(node1 != null)
            {
                if (!node1.Data.Equals(node2.Data)) return false;
                node1 = node1.Next;
                node2 = node2.Next;
            }
            return true;
        }

        /// <summary>
        /// Method: GetReversedList
        /// Returns a reversed linked list.
        /// </summary>
        /// <returns></returns>
        public TLinkedList<T> GetReverseList()
        {
            /// The following implementation make use of the doubly linked
            /// list traits.
            TLinkedList<T> reversed = new TLinkedList<T>();
            Node<T> node = this.tail;
            while(node != null)
            {
                reversed.Append(node.Data);
                node = node.Prev;
            }
            return reversed;


            /// The following implementations are treating TLinkedList structure
            /// as if it is a singly linked list.
            //TLinkedList<T> reversed = new TLinkedList<T>();
            //reversed.count = this.Count;

            //Node<T> node = this.head;

            //reversed.tail = reversed.head;
            //while(node != null)
            //{
            //    Node<T> temp = new Node<T>(node.Data);
            //    Link(temp, reversed.head);
            //    reversed.head = temp;
            //    node = node.Next;
            //}

            //node = reversed.head;
            //while(node.Next != null) node = node.Next;
            //reversed.tail = node;

            //return reversed;
        }

        /// <summary>
        /// Method: CheckPalindrome
        /// Answer to the interview question 2.6
        /// The method checks whether the list is palindrome or not.
        /// </summary>
        /// <returns></returns>
        public bool CheckPalindrome()
        {
            /// Using Doubly Linked List Approach.
            /// This approach simply compares the iteration of nodes from head
            /// and tail are the same or not.
            /// 
            //Node<T>? node1 = this.head;
            //Node<T>? node2 = this.tail;

            //if (node1 == null || node2 == null) return false;
            //while (node1 != null)
            //{
            //    if (!node1.Data.Equals(node2.Data)) return false;
            //    /// as soon as difference is found, return false.
            //    node1 = node1.Next;
            //    node2 = node2.Prev;
            //}
            //return true; /// passed!!! truely, indded a palindrome.

            TLinkedList<T> reversed = GetReverseList();
            return isEqual(reversed);
        }

        /// <summary>
        /// Method: IsIntersectWIth
        /// Answer to the interview question 2.7
        /// The method that checks whether two lists contains an exactly same
        /// node by its reference value of the object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Node<T> IsIntersectWith(TLinkedList<T> other)
        {
            Node<T> list1 = head;

            while(list1 != null)
            {
                Node<T> list2 = other.head;

                while (list2 != null)
                {
                    if(list1.Equals(list2)) { return list1; }
                    list2 = list1.Next;
                }
                list1 = list1.Next;
            }
            return null;
        }

        /// <summary>
        /// Method: TraverseNode
        /// Safe way to move nodes in the list.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="steps"></param>
        protected void TraverseNode(Node<T> node, int steps = 1)
        {
            while(node != null && steps > 0)
            {
                node = node.Next;
                steps--;
            }
        }

        /// <summary>
        /// Method: IsCorrupt
        /// Checks whether a list is cyclic.
        /// </summary>
        /// <returns></returns>
        public bool IsCorrupt()
        {
            Node<T> hare = head;
            Node<T> turtle = head;

            while(hare != null)
            {
                if(hare == turtle) return true;
                TraverseNode(turtle, 1);
                TraverseNode(hare, 2);
            }
            return false;
        }
    }
}
