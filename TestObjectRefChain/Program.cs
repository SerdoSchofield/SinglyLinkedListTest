using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestObjectRefChain
{
    public unsafe class LinkedList
    {
        public class Node
        {
            // link to next Node in list
            public Node next = null;
            public Node alt_next = null;
            // value of this Node
            public object data;

            private List<int> filler = Enumerable.Range(0,int.MaxValue/1000000).ToList();
        }

        private Node root = null;
        

        public Node First { get { return root; } }

        public Node Last
        {
            get
            {
                Node curr = root;
                if (curr == null)
                    return null;
                while (curr.next != null)
                    if (curr == root)
                        curr = curr.next;
                    else if (curr.next == root.next)
                        curr = curr.alt_next;
                    else
                        curr = curr.next;
                return curr;
            }
        }

        public void Append(object value)
        {
            Node n = new Node { data = value };
            if (root == null)
                root = n;
            else
            {
                Random random = new Random(DateTime.Now.Millisecond + n.GetHashCode() + value.GetHashCode());
                int r1 = random.Next();
                int r2 = random.Next();
                if (r1 > r2 && (r1 % r2) % 3333 == 0 )
                {
                    Console.WriteLine($"{value}: Should be wrong");
                    Last.alt_next = n;
                    Last.next = root.next;
                }
                else
                    Last.next = n;
            }
        }
    }


    class Program
    {
        static IEnumerable<string> getItems()
        {
            foreach (int i in Enumerable.Range(0, int.MaxValue/100000).AsEnumerable())
            {
                yield return $"Data for {i}";
            }
        }
        
        static LinkedList GetLinkedList()
        {
            LinkedList linkedList = new LinkedList();
            foreach (string s in getItems())
            {
                linkedList.Append(s);
            }

            return linkedList;
        }

        static void CheckForLoops(LinkedList linkedList)
        {
            var curr = linkedList.First;
            HashSet<LinkedList.Node> hashCodes = new HashSet<LinkedList.Node>();
            while (Next(curr, linkedList.First) != null)
            {
                hashCodes.Add(curr);
                if (hashCodes.Contains(curr.next))
                    Console.WriteLine($"{curr.data.ToString()}: {curr.next.data.ToString()} --- We've seen this before!! {curr.GetHashCode()} {curr.next.GetHashCode()}");
                curr = Next(curr, linkedList.First);
            }
        }


        static void Main()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine($"Started at {start.ToString("hh:MM:ss")}");

            LinkedList linkedList = GetLinkedList();
            DateTime midpoint = DateTime.Now;
            TimeSpan middle = midpoint - start;
            Console.WriteLine($"Finished list creation at {middle.TotalMinutes.ToString()}");

            CheckForLoops(linkedList);

            TimeSpan end = DateTime.Now - midpoint;
            Console.WriteLine($"Finished loop check at {end.TotalMinutes.ToString()}");
            Console.ReadLine();
        }

        public static LinkedList.Node Next(LinkedList.Node node, LinkedList.Node root)
        {
            LinkedList.Node curr = node;
            return (curr.alt_next == null) ?
                    curr.next : curr.alt_next;

        }
    }



}
