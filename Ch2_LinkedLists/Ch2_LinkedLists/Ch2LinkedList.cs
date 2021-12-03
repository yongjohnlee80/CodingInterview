using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch2_LinkedLists
{
    internal class Node<T>
    {
        Node<T>? next = null;
        Node<T>? prev = null;
        T? data = default(T);

        public Node(T data)
        {
            this.data = data;
        }

        public T? Data
        {
            get { return data; }

        }

        public Node<T>? Next
        {
            get { return next; }
        }

        public Node<T>? Prev
        {
            get { return prev; }
        }

        public Node<T> Append(T data)
        {
            if (data == null) return this;
            this.next = new Node<T>(data);
            this.next.prev = this;
            return this.next;
        }

        public void Delete()
        {
            this.prev.next = this.next;
            this.next.prev = this.prev;
        }
    }

    internal class TLinkedList<T>
    {
        Node<T>? head = null;
        Node<T>? tail = null;

        public Node<T> Append(T data)
        {
            if (head == null)
            {
                head = new Node<T>(data);
                tail = head;
                return head;
            }
            tail = tail.Append(data);
            return tail;
        }

        public Node<T>? LoadArray(T[] data)
        {
            foreach(T item in data)
            {
                Append(item);
            }
            return tail;
        }

        public void WriteContent()
        {
            Node<T> node = head;
            while (node != null)
            {
                Console.Write(node.Data);
                node = node.Next;
            }
            Console.WriteLine();
        }

        public void RemoveDuplicates()
        {
            Node<T> first = head;
            Node<T> second;
            while(first != null)
            {
                second = tail;
                while(second.Prev != head || second.Prev != null)
                {
                    if(first.Data.Equals(second.Data))
                    {
                        second.Delete();
                    }
                    second = second.Prev;
                }
            }
        }
    }
    internal class Ch2LinkedList
    {
        public TLinkedList<char> Solution()
        {
            TLinkedList<char> list = new TLinkedList<char>();
            string data = "FOLLOW UP";
            list.LoadArray(data.ToCharArray());
            list.RemoveDuplicates();
            return list;
        }   
    }
}
