using NUnit.Framework;

namespace Ch2_LinkedLists
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Solution to Question 2.1
        /// Write Code to remove duplicates from an unsorted linked list.
        /// </summary>
        [Test]
        public void RemoveDups2_1()
        {
            // Initialize a doubly linked list with char data.
            TLinkedList<char> list = new TLinkedList<char>();
            list.LoadArray("FOLLOW UP".ToCharArray());

            // Removes duplicates from the linked list.
            list.RemoveDuplicates();

            // Displays the results
            list.WriteContent();

            // Displays the length of the list.
            System.Console.WriteLine($"The length of the list is {list.Count}");
        }

        /// <summary>
        /// Solution to Question 2.2
        /// Question 2.2 dictates that we should use singly linked list; thus,
        /// the algorithm does not use tail node nor prev pointer.
        /// </summary>
        [Test]
        public void ReturnKthToLast2_2()
        {
            /// Initialize the linked list and load the integer data to the list.
            TLinkedList<int> list = new TLinkedList<int>();
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8 };
            list.LoadArray(data);

            // Retrieve the kth node to the end of the list.
            Node<int>? result = list.GetNodeFromLast(4);

            // 4th element from the last should be 5.
            Assert.That(result.Data, Is.EqualTo(5));
        }

        /// <summary>
        /// Solution to Question 2.3
        /// Given access to a middle node, which is neither a head or tail node, 
        /// delte the node.
        /// </summary>
        [Test]
        public void DeleteMiddleNode2_3()
        {
            /// Initialize the linked list and load the character data to the list.
            TLinkedList<char> list = new TLinkedList<char>();
            string data = "abcdef";
            list.LoadArray(data.ToCharArray());

            /// Delete the node that contains the value 'c'
            /// The following implementation can be replaced with the following line:
            /// 
            /// list.DeleteMiddleNode(list.FindNode('c'));
            /// 
            list.DeleteMiddleNode('c');

            list.WriteContent();
        }

        /// <summary>
        /// Solution to Question 2.4
        /// Partitions the nodes based on their values in relation to the
        /// given partition value. Nodes containing smaller values are placed
        /// on the left and vice versa.
        /// </summary>
        [Test]
        public void Partition2_4()
        {
            /// Initialize the linked list and load the integers to the list.
            TLinkedList<int> list = new TLinkedList<int>();
            int[] data = { 3, 5, 8, 5, 10, 2, 1 };
            list.LoadArray(data);

            /// Partition the list with value, 5.
            /// Any nodes with smaller than 5 are placed on the left
            /// and vice versa.
            list.Partition(5);

            list.WriteContent();
        }

        /// <summary>
        /// Solution to Question 2.5
        /// Adding two linked list. Each list contains nodes with single digit.
        /// Each lists are in reverse order, meaning the 1's digits are at the head.
        /// </summary>
        [Test]
        public void SumLists2_5()
        {
            /// Initialize the data values. These digits are in reverse order in the lists.
            int[] data1 = { 7, 1, 6 };
            int[] data2 = { 5, 9, 2 };

            /// Initialize two lists and load them with data.
            TLinkedList<int> list1 = new TLinkedList<int>();
            list1.LoadArray(data1);

            TLinkedList<int> list2 = new TLinkedList<int>();
            list2.LoadArray(data2);

            /// Compute summation of the integers.
            var list3 = TLinkedList<int>.SumLists(list1, list2);

            list3.WriteContent();
        }

        /// <summary>
        /// Solution to Question 2.5 Follow up question.
        /// Adding two linked list similar to the previous solution.
        /// This time, however, the lists are in normal orders, meaning
        /// 1's digits are at the tail.
        /// </summary>
        [Test]
        public void SumLists2_5B()
        {
            /// Initialize the data values. These digits are in normal order in the lists.
            int[] data1 = { 6, 1, 7 };
            int[] data2 = { 2, 9, 5 };

            /// Initialize two lists and load them with data.
            TLinkedList<int> list1 = new TLinkedList<int>();
            list1.LoadArray(data1);

            TLinkedList<int> list2 = new TLinkedList<int>();
            list2.LoadArray(data2);

            /// Compute summation of the integers.
            var list3 = TLinkedList<int>.SumLists2(list1, list2);

            list3.WriteContent();
        }

        /// <summary>
        /// Solutoin to Question 2.6.
        /// Checks whether a linked list is a palindrome or not.
        /// </summary>
        [Test]
        public void CheckPalindrome2_6()
        {
            string data = "ABCDDDCBA";

            TLinkedList<char> list = new TLinkedList<char>();
            list.LoadArray(data.ToCharArray());

            Assert.That(list.CheckPalindrome(), Is.True);
        }

        /// <summary>
        /// Solution to Question 2.8.
        /// </summary>
        [Test]
        public void CheckLoop2_8()
        {
            string data = "ABCDEFGHIJKLMN";

            TLinkedList<char> list = new TLinkedList<char>();
            list.LoadArray(data.ToCharArray());

            var node = list.FindNode('H');

            /// The following operations are normally illegal as the head and tail node are 
            /// read only and the class automatically maintains them.
            list.FindNode('N').Next = node;
            node.Prev = list.FindNode('N');

            //System.Console.WriteLine(list.GetLoopHead().Data);
            Assert.AreEqual('H', list.GetLoopHead().Data);
        }
    }
}