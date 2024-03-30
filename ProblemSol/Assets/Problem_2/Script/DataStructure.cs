using System;
using UnityEngine;

namespace DataStrucuture
{
    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public LinkedListNode<T> head;
        public LinkedListNode<T> foot;

        public LinkedList()
        {
            head = null;
            foot = null;
        }

        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (head == null)
            {
                head = newNode;
                foot = newNode;
            }
            else
            {
                //LinkedListNode<T> current = head;
                /*while (current.Next != null)
                {
                    current = current.Next;
                }*/
                if(head == foot)
                {
                    head.Next = newNode;
                }
                foot.Next = newNode;
                foot = newNode;
                //current.Next = newNode;
            }
        }
    }

    // 1 - node ���� : head, foot
    // add 2 -> 1�� next : 2
    // add 3 -> 2�� next : 3
    //head�� �״�� foot�� �þ���� �и�

    public class Queue<T>
    {
        private LinkedList<T> list;

        public Queue()
        {
            list = new LinkedList<T>();
        }

        // ť�� ��Ҹ� �߰��մϴ�.
        public void Enqueue(T data)
        {
            list.Add(data);
        }

        // ť���� ��Ҹ� �����ϰ� ��ȯ�մϴ�.
        public T Dequeue()
        {
            if (list.head == null)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T data = list.head.Data;
            list.head = list.head.Next;
            return data;
        }

    }



}