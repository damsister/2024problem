using System;
using UnityEngine;

namespace DataStrucuture4
{
    public class QueueStack<T>
    {
        private Queue<T> queue1;
        private Queue<T> queue2;
        private int count;

        public QueueStack()
        {
            queue1 = new Queue<T>();
            queue2 = new Queue<T>();
            count = 0;
        }

        // ���ÿ� ��Ҹ� �߰��մϴ�.
        public void Push(T data)
        {
            // queue1�� ��Ҹ� ��� �̵��մϴ�.
            while (queue1.Count > 0)
            {
                queue2.Enqueue(queue1.Dequeue());
            }

            // ���ο� ��Ҹ� queue1�� �߰��մϴ�.
            queue1.Enqueue(data);

            // queue2�� ��Ҹ� �ٽ� queue1�� �̵��մϴ�.
            while (queue2.Count > 0)
            {
                queue1.Enqueue(queue2.Dequeue());
            }

            count++;
        }

        // ���ÿ��� ��Ҹ� �����ϰ� ��ȯ�մϴ�.
        public T Pop()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            count--;
            return queue1.Dequeue();
        }

        // ������ �� ���� �ִ� ��Ҹ� ��ȯ�մϴ�.
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            T topElement = queue1.Dequeue();
            queue1.Enqueue(topElement);
            return topElement;
        }

        // ������ ��� �ִ��� ���θ� ��ȯ�մϴ�.
        public bool IsEmpty()
        {
            return count == 0;
        }
    }
}