using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000209 RID: 521
	internal class ConcurrentLockFreeQueue
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00037FC4 File Offset: 0x000361C4
		public ConcurrentLockFreeQueue()
		{
			QueueNode queueNode = new QueueNode(0);
			this._head = queueNode;
			this._tail = queueNode;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00037FF4 File Offset: 0x000361F4
		public bool dequeue(ref object Data)
		{
			QueueNode head;
			QueueNode next;
			for (;;)
			{
				head = this._head;
				QueueNode tail = this._tail;
				next = head.Next;
				if (this._head == head)
				{
					if (head == tail)
					{
						if (next == QueueNode.NullObject)
						{
							break;
						}
						if (next != null)
						{
							Interlocked.CompareExchange<QueueNode>(ref this._tail, next, tail);
						}
						else
						{
							Thread.MemoryBarrier();
						}
					}
					else if (Interlocked.CompareExchange<QueueNode>(ref this._head, next, head) == head)
					{
						goto Block_4;
					}
				}
			}
			return false;
			Block_4:
			head.Next = null;
			Data = next.Data;
			next.Clear();
			return true;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00038070 File Offset: 0x00036270
		public void enqueue(object Data)
		{
			QueueNode queueNode = new QueueNode(Data);
			QueueNode tail;
			for (;;)
			{
				tail = this._tail;
				QueueNode next = tail.Next;
				if (tail == this._tail)
				{
					if (next == QueueNode.NullObject)
					{
						if (Interlocked.CompareExchange<QueueNode>(ref tail.Next, queueNode, next) == next)
						{
							break;
						}
					}
					else if (next != null)
					{
						Interlocked.CompareExchange<QueueNode>(ref this._tail, next, tail);
					}
					else
					{
						Thread.MemoryBarrier();
					}
				}
			}
			Interlocked.CompareExchange<QueueNode>(ref this._tail, queueNode, tail);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000380E0 File Offset: 0x000362E0
		public int FuzzyCutPaste(ConcurrentLockFreeQueue.CutDelegate userSpecifiedCutFunction, ConcurrentLockFreeQueue pasteQueue)
		{
			QueueNode queueNode = this._head.Next;
			int num = 0;
			while (queueNode != QueueNode.NullObject && queueNode != null)
			{
				if (userSpecifiedCutFunction(queueNode.Data) && queueNode.Data != null)
				{
					pasteQueue.enqueue(queueNode.Data);
					queueNode.Data = null;
					num++;
				}
				queueNode = queueNode.Next;
			}
			return num;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00038140 File Offset: 0x00036340
		public bool TryPeek(ref object data)
		{
			QueueNode head = this._head;
			QueueNode next = head.Next;
			if (next == QueueNode.NullObject || next == null)
			{
				return false;
			}
			data = next.Data;
			return true;
		}

		// Token: 0x04000ADD RID: 2781
		private QueueNode _head;

		// Token: 0x04000ADE RID: 2782
		private QueueNode _tail;

		// Token: 0x0200020A RID: 522
		// (Invoke) Token: 0x060010FC RID: 4348
		public delegate bool CutDelegate(object dataInQueueNode);
	}
}
