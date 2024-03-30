using System;
using UnityEngine;

namespace DataStrucuture4
{
    public class StackNode<T>
    {
        public T Data { get; set; }
        public StackNode<T> Next { get; set; }

        public StackNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class Stack<T>
    {
        private StackNode<T> top;

        public Stack()
        {
            top = null;
        }

        public void Push(T data)
        {
            StackNode<T> newNode = new StackNode<T>(data);
            newNode.Next = top;
            top = newNode;
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Stack is empty");
            }

            T data = top.Data;
            top = top.Next;
            return data;
        }

        public bool IsEmpty()
        {
            return top == null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Console.WriteLine(stack.Pop()); // Output: 3
            Console.WriteLine(stack.Pop()); // Output: 2
            Console.WriteLine(stack.Pop()); // Output: 1
        }
    }
}