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
        }
    }
}