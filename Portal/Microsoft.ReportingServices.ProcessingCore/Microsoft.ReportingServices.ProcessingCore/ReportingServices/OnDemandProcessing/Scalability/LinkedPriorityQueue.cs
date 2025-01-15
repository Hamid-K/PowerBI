using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000847 RID: 2119
	internal sealed class LinkedPriorityQueue<T> where T : class
	{
		// Token: 0x06007665 RID: 30309 RVA: 0x001EAD46 File Offset: 0x001E8F46
		internal LinkedPriorityQueue()
		{
			this.m_priorityLevels = new SortedDictionary<int, LinkedPriorityQueue<T>.PriorityLevel>(EqualityComparers.ReversedInt32ComparerInstance);
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x001EAD60 File Offset: 0x001E8F60
		internal void Enqueue(T item, int priority)
		{
			Global.Tracer.Assert(!this.m_pendingDecumulatorCommit, "Cannot perform operations on the queue until the open enumerator is Disposed");
			LinkedPriorityQueue<T>.PriorityLevel priorityLevel;
			if (!this.m_priorityLevels.TryGetValue(priority, out priorityLevel))
			{
				priorityLevel = new LinkedPriorityQueue<T>.PriorityLevel();
				priorityLevel.Priority = priority;
				priorityLevel.Queue = new LinkedBucketedQueue<T>(100);
				this.m_priorityLevels[priority] = priorityLevel;
			}
			priorityLevel.Queue.Enqueue(item);
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x001EADC8 File Offset: 0x001E8FC8
		internal T Dequeue()
		{
			Global.Tracer.Assert(!this.m_pendingDecumulatorCommit, "Cannot perform operations on the queue until the open enumerator is Disposed");
			T t2;
			using (IDecumulator<T> decumulator = this.GetDecumulator())
			{
				decumulator.MoveNext();
				T t = decumulator.Current;
				decumulator.RemoveCurrent();
				t2 = t;
			}
			return t2;
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x001EAE28 File Offset: 0x001E9028
		internal IDecumulator<T> GetDecumulator()
		{
			Global.Tracer.Assert(!this.m_pendingDecumulatorCommit, "Cannot perform operations on the queue until the open enumerator is Disposed");
			this.m_pendingDecumulatorCommit = true;
			return new LinkedPriorityQueue<T>.PriorityQueueDecumulator(this);
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x001EAE54 File Offset: 0x001E9054
		internal void Clear()
		{
			this.m_priorityLevels.Clear();
		}

		// Token: 0x170027AE RID: 10158
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x001EAE61 File Offset: 0x001E9061
		internal int LevelCount
		{
			get
			{
				Global.Tracer.Assert(!this.m_pendingDecumulatorCommit, "Cannot perform operations on the queue until the open enumerator is Disposed");
				return this.m_priorityLevels.Count;
			}
		}

		// Token: 0x04003BF7 RID: 15351
		private SortedDictionary<int, LinkedPriorityQueue<T>.PriorityLevel> m_priorityLevels;

		// Token: 0x04003BF8 RID: 15352
		private bool m_pendingDecumulatorCommit;

		// Token: 0x04003BF9 RID: 15353
		private const int QueueBucketSize = 100;

		// Token: 0x02000D05 RID: 3333
		internal struct PriorityQueueDecumulator : IDecumulator<T>, IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06008E7D RID: 36477 RVA: 0x00245054 File Offset: 0x00243254
			internal PriorityQueueDecumulator(LinkedPriorityQueue<T> queue)
			{
				this.m_queue = queue;
				this.m_enumerator = this.m_queue.m_priorityLevels.Values.GetEnumerator();
				this.m_pendingLevelRemovals = new List<int>();
				this.m_currentLevelDecumulator = null;
				this.m_currentLevel = null;
			}

			// Token: 0x06008E7E RID: 36478 RVA: 0x002450A4 File Offset: 0x002432A4
			public bool MoveNext()
			{
				if (this.m_currentLevel != null && (this.m_currentLevelDecumulator == null || this.m_currentLevelDecumulator.MoveNext()))
				{
					return true;
				}
				if (!this.m_enumerator.MoveNext())
				{
					return false;
				}
				this.m_currentLevel = this.m_enumerator.Current;
				this.m_currentLevelDecumulator = this.m_currentLevel.Queue.GetDecumulator();
				return this.m_currentLevelDecumulator.MoveNext();
			}

			// Token: 0x06008E7F RID: 36479 RVA: 0x00245111 File Offset: 0x00243311
			public void RemoveCurrent()
			{
				this.m_currentLevelDecumulator.RemoveCurrent();
				if (this.m_currentLevel.Queue.Count == 0)
				{
					this.m_pendingLevelRemovals.Add(this.m_currentLevel.Priority);
					this.m_currentLevel = null;
				}
			}

			// Token: 0x17002BB7 RID: 11191
			// (get) Token: 0x06008E80 RID: 36480 RVA: 0x0024514D File Offset: 0x0024334D
			public T Current
			{
				get
				{
					return this.m_currentLevelDecumulator.Current;
				}
			}

			// Token: 0x06008E81 RID: 36481 RVA: 0x0024515C File Offset: 0x0024335C
			public void Dispose()
			{
				this.m_enumerator.Dispose();
				this.m_enumerator = null;
				for (int i = 0; i < this.m_pendingLevelRemovals.Count; i++)
				{
					this.m_queue.m_priorityLevels.Remove(this.m_pendingLevelRemovals[i]);
				}
				this.m_queue.m_pendingDecumulatorCommit = false;
			}

			// Token: 0x17002BB8 RID: 11192
			// (get) Token: 0x06008E82 RID: 36482 RVA: 0x002451BA File Offset: 0x002433BA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008E83 RID: 36483 RVA: 0x002451C7 File Offset: 0x002433C7
			public void Reset()
			{
				Global.Tracer.Assert(false, "Reset is not supported");
			}

			// Token: 0x0400501C RID: 20508
			private IDecumulator<T> m_currentLevelDecumulator;

			// Token: 0x0400501D RID: 20509
			private LinkedPriorityQueue<T>.PriorityLevel m_currentLevel;

			// Token: 0x0400501E RID: 20510
			private LinkedPriorityQueue<T> m_queue;

			// Token: 0x0400501F RID: 20511
			private IEnumerator<LinkedPriorityQueue<T>.PriorityLevel> m_enumerator;

			// Token: 0x04005020 RID: 20512
			private List<int> m_pendingLevelRemovals;
		}

		// Token: 0x02000D06 RID: 3334
		internal class PriorityLevel
		{
			// Token: 0x04005021 RID: 20513
			public LinkedBucketedQueue<T> Queue;

			// Token: 0x04005022 RID: 20514
			public int Priority;
		}
	}
}
