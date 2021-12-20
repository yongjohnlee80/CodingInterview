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
    }
}