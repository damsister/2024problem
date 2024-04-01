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

        // 스택에 요소를 추가합니다.
        public void Push(T data)
        {
            // queue1에 요소를 모두 이동합니다.
            while (queue1.Count > 0)
            {
                queue2.Enqueue(queue1.Dequeue());
            }

            // 새로운 요소를 queue1에 추가합니다.
            queue1.Enqueue(data);

            // queue2의 요소를 다시 queue1로 이동합니다.
            while (queue2.Count > 0)
            {
                queue1.Enqueue(queue2.Dequeue());
            }

            count++;
        }

        // 스택에서 요소를 제거하고 반환합니다.
        public T Pop()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            count--;
            return queue1.Dequeue();
        }

        // 스택의 맨 위에 있는 요소를 반환합니다.
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

        // 스택이 비어 있는지 여부를 반환합니다.
        public bool IsEmpty()
        {
            return count == 0;
        }
    }
}