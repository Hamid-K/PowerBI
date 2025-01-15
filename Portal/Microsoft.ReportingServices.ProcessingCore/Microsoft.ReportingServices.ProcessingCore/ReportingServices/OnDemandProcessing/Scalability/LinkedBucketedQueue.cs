using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000849 RID: 2121
	internal sealed class LinkedBucketedQueue<T> : IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x0600766C RID: 30316 RVA: 0x001EAE86 File Offset: 0x001E9086
		internal LinkedBucketedQueue(int bucketSize)
		{
			this.m_count = 0;
			this.m_bucketSize = bucketSize;
		}

		// Token: 0x170027AF RID: 10159
		// (get) Token: 0x0600766D RID: 30317 RVA: 0x001EAEA4 File Offset: 0x001E90A4
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x0600766E RID: 30318 RVA: 0x001EAEAC File Offset: 0x001E90AC
		internal void Enqueue(T item)
		{
			if (this.m_firstBucket == null)
			{
				this.m_firstBucket = new LinkedBucketedQueue<T>.QueueBucket(this.m_bucketSize);
				this.m_lastBucket = this.m_firstBucket;
				this.m_firstBucket.NextBucket = null;
				this.m_firstBucket.PreviousBucket = null;
				this.m_insertIndex = 0;
			}
			if (this.m_insertIndex == this.m_bucketSize)
			{
				LinkedBucketedQueue<T>.QueueBucket lastBucket = this.m_lastBucket;
				this.m_lastBucket = new LinkedBucketedQueue<T>.QueueBucket(this.m_bucketSize);
				this.m_lastBucket.NextBucket = null;
				this.m_lastBucket.PreviousBucket = lastBucket;
				lastBucket.NextBucket = this.m_lastBucket;
				this.m_insertIndex = 0;
			}
			this.m_lastBucket.Array[this.m_insertIndex] = item;
			this.m_lastBucket.Count++;
			this.m_insertIndex++;
			this.m_count++;
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x001EAF94 File Offset: 0x001E9194
		internal T Dequeue()
		{
			T t = default(T);
			while (t == null && this.m_count > 0)
			{
				t = this.m_firstBucket.Array[this.m_removeIndex];
				this.m_firstBucket.Array[this.m_removeIndex] = default(T);
				this.m_removeIndex++;
				if (t != null)
				{
					this.m_firstBucket.Count--;
					this.m_count--;
				}
				if (this.m_firstBucket.Count == 0)
				{
					this.RemoveFirstBucket();
				}
			}
			return t;
		}

		// Token: 0x06007670 RID: 30320 RVA: 0x001EB043 File Offset: 0x001E9243
		private void RemoveFirstBucket()
		{
			this.m_firstBucket = this.m_firstBucket.NextBucket;
			this.m_removeIndex = 0;
			if (this.m_firstBucket == null)
			{
				this.m_lastBucket = null;
				return;
			}
			this.m_firstBucket.PreviousBucket = null;
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x001EB079 File Offset: 0x001E9279
		internal void Clear()
		{
			this.m_count = 0;
			this.m_firstBucket = null;
			this.m_lastBucket = null;
		}

		// Token: 0x06007672 RID: 30322 RVA: 0x001EB090 File Offset: 0x001E9290
		public IDecumulator<T> GetDecumulator()
		{
			return new LinkedBucketedQueue<T>.QueueEnumerator(this);
		}

		// Token: 0x06007673 RID: 30323 RVA: 0x001EB098 File Offset: 0x001E9298
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetDecumulator();
		}

		// Token: 0x06007674 RID: 30324 RVA: 0x001EB0A0 File Offset: 0x001E92A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetDecumulator();
		}

		// Token: 0x04003BFA RID: 15354
		private LinkedBucketedQueue<T>.QueueBucket m_firstBucket;

		// Token: 0x04003BFB RID: 15355
		private LinkedBucketedQueue<T>.QueueBucket m_lastBucket;

		// Token: 0x04003BFC RID: 15356
		private int m_count;

		// Token: 0x04003BFD RID: 15357
		private int m_bucketSize = 20;

		// Token: 0x04003BFE RID: 15358
		private int m_insertIndex;

		// Token: 0x04003BFF RID: 15359
		private int m_removeIndex;

		// Token: 0x02000D07 RID: 3335
		private class QueueBucket
		{
			// Token: 0x06008E85 RID: 36485 RVA: 0x002451E1 File Offset: 0x002433E1
			internal QueueBucket(int size)
			{
				this.Array = new T[size];
				this.PreviousBucket = null;
				this.NextBucket = null;
				this.Count = 0;
			}

			// Token: 0x04005023 RID: 20515
			internal T[] Array;

			// Token: 0x04005024 RID: 20516
			internal LinkedBucketedQueue<T>.QueueBucket PreviousBucket;

			// Token: 0x04005025 RID: 20517
			internal LinkedBucketedQueue<T>.QueueBucket NextBucket;

			// Token: 0x04005026 RID: 20518
			internal int Count;
		}

		// Token: 0x02000D08 RID: 3336
		private class QueueEnumerator : IDecumulator<T>, IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06008E86 RID: 36486 RVA: 0x0024520A File Offset: 0x0024340A
			internal QueueEnumerator(LinkedBucketedQueue<T> queue)
			{
				this.m_queue = queue;
				this.Reset();
			}

			// Token: 0x06008E87 RID: 36487 RVA: 0x00245220 File Offset: 0x00243420
			public void RemoveCurrent()
			{
				this.m_currentBucket.Array[this.m_currentIndex] = default(T);
				this.m_currentBucket.Count--;
				this.m_queue.m_count--;
				if (this.m_currentBucket.Count == 0)
				{
					if (this.m_currentBucket == this.m_queue.m_firstBucket)
					{
						this.m_queue.RemoveFirstBucket();
						this.m_currentBucket = this.m_queue.m_firstBucket;
						this.m_currentIndex = -1;
						return;
					}
					if (this.m_currentBucket == this.m_queue.m_lastBucket)
					{
						if (this.m_currentBucket.PreviousBucket == null)
						{
							this.m_queue.m_firstBucket = null;
							this.m_queue.m_lastBucket = null;
							this.m_queue.m_count = 0;
							return;
						}
						this.m_queue.m_lastBucket = this.m_currentBucket.PreviousBucket;
						this.m_queue.m_lastBucket.NextBucket = null;
						this.m_queue.m_insertIndex = this.m_queue.m_bucketSize;
						this.m_currentBucket.PreviousBucket = null;
						return;
					}
					else
					{
						this.m_currentBucket.NextBucket.PreviousBucket = this.m_currentBucket.PreviousBucket;
						this.m_currentBucket.PreviousBucket.NextBucket = this.m_currentBucket.NextBucket;
						LinkedBucketedQueue<T>.QueueBucket currentBucket = this.m_currentBucket;
						this.m_currentBucket = this.m_currentBucket.NextBucket;
						this.m_currentIndex = -1;
						currentBucket.NextBucket = null;
						currentBucket.PreviousBucket = null;
					}
				}
			}

			// Token: 0x17002BB9 RID: 11193
			// (get) Token: 0x06008E88 RID: 36488 RVA: 0x002453A9 File Offset: 0x002435A9
			public T Current
			{
				get
				{
					return this.m_currentBucket.Array[this.m_currentIndex];
				}
			}

			// Token: 0x06008E89 RID: 36489 RVA: 0x002453C1 File Offset: 0x002435C1
			public void Dispose()
			{
			}

			// Token: 0x17002BBA RID: 11194
			// (get) Token: 0x06008E8A RID: 36490 RVA: 0x002453C3 File Offset: 0x002435C3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008E8B RID: 36491 RVA: 0x002453D0 File Offset: 0x002435D0
			public bool MoveNext()
			{
				if (this.m_currentBucket == null)
				{
					this.m_currentBucket = this.m_queue.m_firstBucket;
					this.m_currentIndex = -1;
				}
				do
				{
					this.m_currentIndex++;
					if (this.m_currentBucket != null && this.m_currentIndex == this.m_queue.m_bucketSize)
					{
						this.m_currentBucket = this.m_currentBucket.NextBucket;
						this.m_currentIndex = 0;
					}
				}
				while (this.m_currentBucket != null && this.m_currentBucket.Array[this.m_currentIndex] == null);
				return this.m_currentBucket != null;
			}

			// Token: 0x06008E8C RID: 36492 RVA: 0x0024546C File Offset: 0x0024366C
			public void Reset()
			{
				this.m_currentBucket = null;
				this.m_currentIndex = -1;
			}

			// Token: 0x04005027 RID: 20519
			private LinkedBucketedQueue<T> m_queue;

			// Token: 0x04005028 RID: 20520
			private LinkedBucketedQueue<T>.QueueBucket m_currentBucket;

			// Token: 0x04005029 RID: 20521
			private int m_currentIndex;
		}
	}
}
