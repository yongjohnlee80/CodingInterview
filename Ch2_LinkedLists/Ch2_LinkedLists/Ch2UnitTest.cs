using NUnit.Framework;

namespace Ch2_LinkedLists
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            
            Ch2LinkedList question1 = new Ch2LinkedList();
            TLinkedList<char> list = question1.Solution();
            list.WriteContent();
        }
    }
}