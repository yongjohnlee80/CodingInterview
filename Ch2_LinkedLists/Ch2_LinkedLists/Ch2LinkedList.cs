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
            set { next = value; }
        }

        public Node<T>? Prev
        {
            get { return prev; }
            set { prev = value; }
        }

        public Node<T>? Delete()
        {
            if(this.prev != null) this.prev.next = this.next;
            if(this.next != null) this.next.prev = this.prev;
            return this.next;
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
            else
            {
                tail.Next = new Node<T> (data);
                tail.Next.Prev = tail;
                tail = tail.Next;
                return tail;
            }
        }

        public Node<T>? LoadArray(T[] data)
        {
            foreach (T item in data)
            {
                Append(item);
            }
            return tail;
        }

        public void WriteContent(bool reverse = false)
        {
            Node<T>? node = head;
            if (reverse) node = tail;
            while (node != null)
            {
                Console.Write(node.Data);
                node = (reverse ? node.Prev : node.Next);
            }
            Console.WriteLine();
        }

        public void RemoveDuplicates()
        {
            if (head == null) return;

            HashSet<T> set = new HashSet<T>();
            Node<T>? current = head;
            Node<T>? previous = null;

            while (current != null)
            {
                if (set.Contains(current.Data))
                {
                    previous.Next = current.Next;
                    current.Next.Prev = previous; 
                }
                else
                {
                    set.Add(current.Data);
                    previous = current;
                }
                current = current.Next;
            }

            //Node<T>? first = head;
            //Node<T>? second = first.Next;
            //while (first.Next != null)
            //{
            //    while (second != null)
            //    {
            //        if (first.Data.Equals(second.Data))
            //        {
            //            second = second.Delete();
            //        }
            //        else
            //        {
            //            second = second.Next;
            //        }
            //    }
            //    first = first.Next;
            //    second = first.Next;
            //}
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
