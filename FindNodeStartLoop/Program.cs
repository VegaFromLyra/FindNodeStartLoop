using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindNodeStartLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = node5;
            node5.Next = node3;

            Node result1 = FindStartNode1(node1);
            Console.WriteLine(result1.Data);

            Node result2 = FindStartNode2(node1);
            Console.WriteLine(result2.Data);

            Node result3 = FindStartNode3(node1);
            Console.WriteLine(result3.Data);
        }

        // We know that Floyd’s Cycle detection algorithm terminates when fast and slow pointers meet 
        // at a common point. We also know that this common point is one of the loop nodes 
        // We store the address of this in a pointer variable say node2. 
        // Then we start from the head of the Linked List and check for nodes one by one if they are reachable from node2. 
        // When we find a node that is reachable, we know that this node is the starting node of the loop in Linked List 
        static Node FindStartNode1(Node head)
        {
            Node loopNode = FindCollisionPoint(head);

            if (loopNode == null)
            {
                return null;
            }

            Node node1 = head;
            Node node2 = null;

            bool done = false;

            while (!done)
            {
                node2 = loopNode;
                while (node2.Next != loopNode && node2.Next != node1)
                {
                    node2 = node2.Next;
                }

                if (node2.Next == node1)
                {
                    break;
                }

                node1 = node1.Next;
            }

            return node1;
        }

        static Node FindCollisionPoint(Node head)
        {
            Node current = head;
            Node runner = head;

            while (current != null && runner != null && runner.Next != null)
            {
                current = current.Next;
                runner = runner.Next.Next;

                if (runner == current)
                {
                    return runner;
                }
            }

            return null;
        }

        // Another way of finding start of loop
        // O(n) time and O(n) space
        static Node FindStartNode2(Node head)
        {
            HashSet<Node> visitedNodes = new HashSet<Node>();

            Node n = head;

            if (n == null)
            {
                return null;
            }

            while (!visitedNodes.Contains(n))
            {
                visitedNodes.Add(n);
                n = n.Next;
            }

            return n;
        }

        // Check if given list has a loop
        // This method uses 2 references - current and runner
        // current advances by one node at a time
        // runner advances by two nodes at a time
        // If these two nodes meet at some point then there is a loop
        static bool IsLoop(Node head)
        {
            Node current = head;
            Node runner = head;

            while (current != null && runner != null && runner.Next != null)
            {
                current = current.Next;
                runner = runner.Next.Next;

                if (runner == current)
                {
                    return true;
                }
            }

            return false;
        }


        //1) Detect Loop using Floyd’s Cycle detection algo and get the pointer to a loop node.
        //2) Count the number of nodes in loop. Let the count be k.
        //3) Fix one pointer to the head and another to kth node from head.
        //4) Move both pointers at the same pace, they will meet at loop starting node.
        static Node FindStartNode3(Node head)
        {
            Node loopNode = FindCollisionPoint(head);

            if (loopNode == null)
            {
                return null;
            }

            int k = 1;

            Node tempNode = loopNode;

            while (tempNode.Next != loopNode)
            {
                k++;
                tempNode = tempNode.Next;
            }

            Node node1 = head;
            Node node2 = head;

            for (int i = 0; i < k; i++)
            {
                node2 = node2.Next;
            }

            while (node1 != node2)
            {
                node1 = node1.Next;
                node2 = node2.Next;
            }

            return node1;
        }
    }

    // a - b - c - a

    class Node
    {
        public Node(int data)
        {
            Data = data;
        }

        public int Data { private set; get; }
        public Node Next { get; set; }
    }
}
