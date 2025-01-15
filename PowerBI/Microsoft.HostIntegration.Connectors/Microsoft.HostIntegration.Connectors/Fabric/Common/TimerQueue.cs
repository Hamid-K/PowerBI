using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000427 RID: 1063
	internal abstract class TimerQueue
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x00071028 File Offset: 0x0006F228
		internal TimerQueue(WaitableTimer waitableTimer)
		{
			this.m_lock = new object();
			this.m_sentinel = new TimerQueue.Sentinel();
			ReleaseAssert.IsTrue(this.m_sentinel.Next == this.m_sentinel);
			ReleaseAssert.IsTrue(this.m_sentinel.Prev == this.m_sentinel);
			this.m_waitableTimerBeingAdjusted = false;
			this.m_sortedTail = this.m_sentinel;
			this.m_waitCallbackTimer = null;
			this.m_waitableTimer = waitableTimer;
			this.m_waitableTimer.WaitCallback = new WaitOrTimerCallback(this.WaitCallback);
			this.m_countOfTimersEnqueued = 0;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000710C0 File Offset: 0x0006F2C0
		// (set) Token: 0x060024F7 RID: 9463 RVA: 0x000710CD File Offset: 0x0006F2CD
		internal TimerObject QueueHead
		{
			get
			{
				return this.m_sentinel.Next;
			}
			private set
			{
				this.m_sentinel.Next = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000710DB File Offset: 0x0006F2DB
		// (set) Token: 0x060024F9 RID: 9465 RVA: 0x000710E8 File Offset: 0x0006F2E8
		internal TimerObject QueueTail
		{
			get
			{
				return this.m_sentinel.Prev;
			}
			private set
			{
				this.m_sentinel.Prev = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000710F6 File Offset: 0x0006F2F6
		public TimerObject EarliestExpiringTimer
		{
			get
			{
				return this.QueueHead;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x000710FE File Offset: 0x0006F2FE
		public bool IsEmpty
		{
			get
			{
				return this.QueueHead == this.m_sentinel;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x0007110E File Offset: 0x0006F30E
		public int Count
		{
			get
			{
				return this.m_countOfTimersEnqueued;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00071116 File Offset: 0x0006F316
		internal WaitableTimer WaitableTimer
		{
			get
			{
				return this.m_waitableTimer;
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00071120 File Offset: 0x0006F320
		public void Enqueue(TimerObject timer)
		{
			if (timer == null)
			{
				throw new ArgumentNullException("timer");
			}
			ReleaseAssert.IsTrue(timer.ExpirationTime != FileTime.MaxValue);
			bool flag = false;
			lock (this.m_lock)
			{
				ReleaseAssert.IsTrue(!timer.IsEnqueued);
				TimerObject timerObject = null;
				TimerObject timerObject2 = this.m_sortedTail;
				bool flag2 = false;
				for (int i = 0; i < TimerQueue.s_minTailLengthToExamine; i++)
				{
					if (timer.ExpirationTime >= timerObject2.ExpirationTime || timerObject2 == this.m_sentinel)
					{
						timerObject = timerObject2;
						flag2 = true;
						break;
					}
					timerObject2 = timerObject2.Prev;
				}
				if (timerObject == null)
				{
					if (timer.ExpirationTime < this.QueueHead.ExpirationTime)
					{
						timerObject = this.m_sentinel;
						flag2 = true;
					}
					else
					{
						timerObject = this.QueueTail;
					}
				}
				if (timerObject == this.m_sentinel && !this.m_waitableTimerBeingAdjusted)
				{
					flag = true;
					this.m_waitableTimerBeingAdjusted = true;
				}
				if (flag2 && timerObject == this.m_sortedTail)
				{
					this.m_sortedTail = timer;
				}
				TimerQueue.LinkAfter(timer, timerObject);
				timer.MarkAsEnqueued();
				this.m_countOfTimersEnqueued++;
			}
			if (flag)
			{
				this.AdjustTimer();
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00071250 File Offset: 0x0006F450
		public bool Dequeue(TimerObject timer)
		{
			if (timer == null)
			{
				throw new ArgumentNullException("timer");
			}
			if (!timer.IsEnqueued)
			{
				return false;
			}
			bool flag = false;
			lock (this.m_lock)
			{
				if (!timer.IsEnqueued)
				{
					return false;
				}
				TimerObject timerObject = this.QueueHead;
				if (timerObject == timer)
				{
					this.SortAndMerge();
					timerObject = this.QueueHead;
					if (timer.ExpirationTime != timerObject.ExpirationTime)
					{
						ReleaseAssert.IsTrue(timer.ExpirationTime < timerObject.ExpirationTime);
						if (!this.m_waitableTimerBeingAdjusted)
						{
							flag = true;
							this.m_waitableTimerBeingAdjusted = true;
						}
					}
				}
				if (timer == this.m_sortedTail)
				{
					this.m_sortedTail = timer.Prev;
				}
				TimerQueue.Delink(timer);
				timer.MarkAsDequeued();
				this.m_countOfTimersEnqueued--;
			}
			if (flag)
			{
				this.AdjustTimer();
			}
			return true;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0007133C File Offset: 0x0006F53C
		public void SortQueue()
		{
			lock (this.m_lock)
			{
				this.SortAndMerge();
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00071378 File Offset: 0x0006F578
		internal void Map(TimerQueue.Apply apply)
		{
			lock (this.m_lock)
			{
				this.SortAndMerge();
				for (TimerObject timerObject = this.QueueHead; timerObject != this.m_sentinel; timerObject = timerObject.Next)
				{
					apply(timerObject);
				}
				apply(null);
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000713D8 File Offset: 0x0006F5D8
		private void AdjustTimer()
		{
			ReleaseAssert.IsTrue(this.m_waitableTimerBeingAdjusted);
			for (;;)
			{
				TimerObject timerObject = this.QueueHead;
				TimerObject timerObject2 = timerObject;
				long enqueuedVersion = timerObject.EnqueuedVersion;
				this.m_waitCallbackTimer = null;
				this.SetTimer(timerObject2);
				timerObject = this.QueueHead;
				if (timerObject2 == timerObject && enqueuedVersion == timerObject.EnqueuedVersion)
				{
					lock (this.m_lock)
					{
						TimerObject queueHead = this.QueueHead;
						if (timerObject2 != queueHead || enqueuedVersion != queueHead.EnqueuedVersion)
						{
							continue;
						}
						if (this.m_waitCallbackTimer == timerObject2)
						{
							continue;
						}
						this.m_waitableTimerBeingAdjusted = false;
					}
					break;
				}
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00071474 File Offset: 0x0006F674
		private void WaitCallback(object state, bool timedOut)
		{
			ReleaseAssert.IsTrue(!timedOut);
			bool flag = false;
			FileTime now = FileTime.Now;
			List<TimerObject> list = null;
			lock (this.m_lock)
			{
				bool flag2;
				if (this.QueueHead.ExpirationTime <= now)
				{
					this.SortAndMerge();
					list = new List<TimerObject>();
					TimerObject timerObject = this.QueueHead;
					do
					{
						ReleaseAssert.IsTrue(timerObject != this.m_sentinel);
						TimerObject timerObject2 = timerObject;
						timerObject = timerObject.Next;
						timerObject2.MarkAsDequeued();
						this.m_countOfTimersEnqueued--;
						list.Add(timerObject2);
					}
					while (timerObject.ExpirationTime <= now);
					this.QueueHead = timerObject;
					timerObject.Prev = this.m_sentinel;
					if (timerObject == this.m_sentinel)
					{
						this.m_sortedTail = this.m_sentinel;
					}
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				if (!this.m_waitableTimerBeingAdjusted)
				{
					flag = true;
					this.m_waitableTimerBeingAdjusted = true;
				}
				else if (!flag2)
				{
					this.m_waitCallbackTimer = this.QueueHead;
				}
			}
			if (flag)
			{
				this.AdjustTimer();
			}
			this.ProcessExpiredTimers(list);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00071594 File Offset: 0x0006F794
		private void ProcessExpiredTimers(List<TimerObject> expiredList)
		{
			if (expiredList == null)
			{
				return;
			}
			foreach (TimerObject timerObject in expiredList)
			{
				TimerThatShouldNotExpire timerThatShouldNotExpire = timerObject as TimerThatShouldNotExpire;
				if (timerThatShouldNotExpire != null)
				{
					timerThatShouldNotExpire.FireTimer();
				}
			}
			foreach (TimerObject timerObject2 in expiredList)
			{
				if (!(timerObject2 is TimerThatShouldNotExpire) && !this.InvokeExpiredTimer(timerObject2, TimerQueue.s_timerDelegate))
				{
					TimerQueue.RetryTimer retryTimer = timerObject2 as TimerQueue.RetryTimer;
					if (retryTimer == null)
					{
						retryTimer = new TimerQueue.RetryTimer(timerObject2, this);
					}
					retryTimer.Enqueue();
				}
			}
		}

		// Token: 0x06002505 RID: 9477
		protected abstract bool InvokeExpiredTimer(TimerObject expiredTimer, WaitCallback callback);

		// Token: 0x06002506 RID: 9478 RVA: 0x0007165C File Offset: 0x0006F85C
		private static void FireTimerCallback(object state)
		{
			TimerObject timerObject = (TimerObject)state;
			timerObject.FireTimer();
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00071678 File Offset: 0x0006F878
		private void SetTimer(TimerObject head)
		{
			ReleaseAssert.IsTrue(this.m_waitableTimer != null);
			ReleaseAssert.IsTrue(head != null);
			if (head == this.m_sentinel)
			{
				this.m_waitableTimer.CancelTimer();
				return;
			}
			this.m_waitableTimer.SetTimer(head.ExpirationTime.Ticks);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000716D0 File Offset: 0x0006F8D0
		private void SortAndMerge()
		{
			ReleaseAssert.IsTrue(this.QueueHead.Prev == this.m_sentinel);
			ReleaseAssert.IsTrue(this.QueueTail.Next == this.m_sentinel);
			if (this.m_sortedTail != this.QueueTail)
			{
				ReleaseAssert.IsTrue(this.QueueTail != this.m_sentinel);
				TimerObject timerObject;
				TimerObject timerObject2;
				this.QuickSort(this.m_sortedTail.Next, this.QueueTail, out timerObject, out timerObject2);
				ReleaseAssert.IsTrue(timerObject2 == this.QueueTail);
				ReleaseAssert.IsTrue(timerObject == this.m_sortedTail.Next);
				ReleaseAssert.IsTrue(timerObject.Prev == this.m_sortedTail);
				if (this.m_sortedTail != this.m_sentinel)
				{
					this.Merge(this.QueueHead, this.m_sortedTail, timerObject, timerObject2);
				}
				this.m_sortedTail = this.QueueTail;
			}
			ReleaseAssert.IsTrue(this.QueueHead.Prev == this.m_sentinel);
			ReleaseAssert.IsTrue(this.QueueTail.Next == this.m_sentinel);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000717E4 File Offset: 0x0006F9E4
		private int DetermineSwapItems(TimerObject start, TimerObject end, FileTime partitionTime, out TimerObject left, out TimerObject right)
		{
			ReleaseAssert.IsTrue(start != this.m_sortedTail);
			ReleaseAssert.IsTrue(end != this.m_sentinel);
			left = start;
			right = end;
			while (left.ExpirationTime <= partitionTime)
			{
				if (left == right)
				{
					return 1;
				}
				left = left.Next;
			}
			while (right.ExpirationTime > partitionTime)
			{
				if (right == left)
				{
					return 2;
				}
				right = right.Prev;
			}
			return 3;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0007186C File Offset: 0x0006FA6C
		private void QuickSort(TimerObject start, TimerObject end, out TimerObject head, out TimerObject tail)
		{
			if (start == end)
			{
				head = start;
				tail = end;
				return;
			}
			FileTime fileTime = (start.ExpirationTime + end.ExpirationTime) / 2UL;
			TimerObject timerObject;
			TimerObject timerObject2;
			int num = this.DetermineSwapItems(start, end, fileTime, out timerObject, out timerObject2);
			TimerObject timerObject3;
			TimerObject timerObject4;
			if (num != 1)
			{
				if (num == 2)
				{
					if (timerObject == start)
					{
						ReleaseAssert.IsTrue(start.ExpirationTime == end.ExpirationTime);
						head = start;
						if (end.Prev == start)
						{
							tail = end;
							return;
						}
						timerObject3 = end.Prev;
						TimerQueue.Delink(end);
						TimerQueue.LinkAfter(end, start);
						timerObject4 = end.Next;
						TimerObject timerObject5;
						this.QuickSort(timerObject4, timerObject3, out timerObject5, out tail);
						return;
					}
					else
					{
						timerObject4 = start;
						timerObject3 = end;
						timerObject2 = timerObject;
						timerObject = timerObject.Prev;
					}
				}
				else
				{
					timerObject4 = ((timerObject == start) ? timerObject2 : start);
					timerObject3 = ((timerObject2 == end) ? timerObject : end);
					do
					{
						TimerObject next = timerObject.Next;
						TimerObject prev = timerObject2.Prev;
						TimerQueue.Swap(timerObject, timerObject2);
						num = this.DetermineSwapItems(next, prev, fileTime, out timerObject, out timerObject2);
					}
					while (num == 3);
					if (num == 1)
					{
						timerObject2 = timerObject.Next;
					}
					else
					{
						timerObject = timerObject2.Prev;
					}
				}
				ReleaseAssert.IsTrue(timerObject.Next == timerObject2);
				TimerObject timerObject6;
				this.QuickSort(timerObject4, timerObject, out head, out timerObject6);
				this.QuickSort(timerObject2, timerObject3, out timerObject6, out tail);
				return;
			}
			ReleaseAssert.IsTrue(start.ExpirationTime == end.ExpirationTime);
			tail = end;
			if (start.Next == end)
			{
				head = start;
				return;
			}
			timerObject4 = start.Next;
			TimerQueue.Delink(start);
			TimerQueue.LinkBefore(start, end);
			timerObject3 = start.Prev;
			TimerObject timerObject7;
			this.QuickSort(timerObject4, timerObject3, out head, out timerObject7);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000719E4 File Offset: 0x0006FBE4
		private static void Delink(TimerObject timer)
		{
			TimerObject next = timer.Next;
			TimerObject prev = timer.Prev;
			prev.Next = next;
			next.Prev = prev;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00071A10 File Offset: 0x0006FC10
		private static void LinkBefore(TimerObject timer, TimerObject before)
		{
			TimerObject prev = before.Prev;
			timer.Next = before;
			timer.Prev = prev;
			before.Prev = timer;
			prev.Next = timer;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00071A44 File Offset: 0x0006FC44
		private static void LinkAfter(TimerObject timer, TimerObject after)
		{
			TimerObject next = after.Next;
			timer.Next = next;
			timer.Prev = after;
			next.Prev = timer;
			after.Next = timer;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00071A78 File Offset: 0x0006FC78
		private static void Swap(TimerObject left, TimerObject right)
		{
			TimerObject prev = left.Prev;
			TimerObject next = right.Next;
			TimerQueue.Delink(left);
			TimerQueue.Delink(right);
			TimerQueue.LinkAfter(right, prev);
			TimerQueue.LinkBefore(left, next);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00071AB0 File Offset: 0x0006FCB0
		private void Merge(TimerObject list1Start, TimerObject list1End, TimerObject list2Start, TimerObject list2End)
		{
			ReleaseAssert.IsTrue(list1Start == this.QueueHead);
			ReleaseAssert.IsTrue(list1Start.Prev == this.m_sentinel);
			ReleaseAssert.IsTrue(this.QueueTail == list2End);
			ReleaseAssert.IsTrue(list2End.Next == this.m_sentinel);
			ReleaseAssert.IsTrue(list1End.Next == list2Start);
			ReleaseAssert.IsTrue(list2Start.Prev == list1End);
			if (list1End.ExpirationTime <= list2Start.ExpirationTime)
			{
				return;
			}
			if (list2End.ExpirationTime <= list1Start.ExpirationTime)
			{
				TimerQueue.LinkSegments(list2End, list1Start);
				this.QueueHead = list2Start;
				list2Start.Prev = this.m_sentinel;
				this.QueueTail = list1End;
				list1End.Next = this.m_sentinel;
				return;
			}
			if (!(list2End.ExpirationTime > list1End.ExpirationTime))
			{
				TimerObject timerObject = list1Start;
				TimerObject timerObject2 = list1End;
				list1Start = list2Start;
				list1End = list2End;
				list2Start = timerObject;
				list2End = timerObject2;
				this.QueueHead = list1Start;
				list1Start.Prev = this.m_sentinel;
				this.QueueTail = list2End;
				list2End.Next = this.m_sentinel;
			}
			list1End.Next = null;
			list2Start.Prev = null;
			TimerObject timerObject3 = list1End;
			TimerObject timerObject4 = list2End.Prev;
			TimerObject timerObject5;
			TimerObject timerObject6;
			for (;;)
			{
				IL_0131:
				if (!(timerObject4.ExpirationTime > timerObject3.ExpirationTime))
				{
					timerObject5 = timerObject3;
					while (timerObject3 != list1Start)
					{
						timerObject3 = timerObject3.Prev;
						if (!(timerObject3.ExpirationTime > timerObject4.ExpirationTime))
						{
							timerObject6 = timerObject3.Next;
							TimerQueue.LinkSegmentAfter(timerObject6, timerObject5, timerObject4);
							goto IL_0131;
						}
					}
					goto Block_6;
				}
				if (timerObject4 == list2Start)
				{
					break;
				}
				timerObject4 = timerObject4.Prev;
			}
			TimerQueue.LinkSegments(timerObject3, list2Start);
			return;
			Block_6:
			this.QueueHead = list2Start;
			list2Start.Prev = this.m_sentinel;
			timerObject6 = list1Start;
			TimerQueue.LinkSegmentAfter(timerObject6, timerObject5, timerObject4);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00071C58 File Offset: 0x0006FE58
		private static void LinkSegmentAfter(TimerObject segmentStart, TimerObject segmentEnd, TimerObject after)
		{
			TimerObject next = after.Next;
			after.Next = segmentStart;
			segmentStart.Prev = after;
			segmentEnd.Next = next;
			next.Prev = segmentEnd;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00071C88 File Offset: 0x0006FE88
		private static void LinkSegments(TimerObject segmentEnd, TimerObject segmentStart)
		{
			segmentEnd.Next = segmentStart;
			segmentStart.Prev = segmentEnd;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00071C98 File Offset: 0x0006FE98
		private void PrintQueue(TimerObject start)
		{
			TimerObject timerObject = start;
			while (timerObject != this.m_sentinel)
			{
				Console.Write(timerObject.ExpirationTime);
				if (timerObject == this.m_sortedTail)
				{
					Console.Write("[1]");
				}
				timerObject = timerObject.Next;
				if (timerObject != this.m_sentinel)
				{
					Console.Write("->");
				}
			}
			Console.WriteLine();
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		internal void PrintQueue()
		{
			lock (this.m_lock)
			{
				Console.Write("\nEarliestTimer:{0} {1}\n", this.IsEmpty ? "empty" : this.QueueHead.ExpirationTime.ToString(), (this.m_sortedTail == this.QueueTail) ? "Sorted" : "");
				this.PrintQueue(this.QueueHead);
			}
		}

		// Token: 0x04001686 RID: 5766
		private const int PROCESSOR_MULTIPLICATION_FACTOR = 3;

		// Token: 0x04001687 RID: 5767
		private const int MINIMUM_TAIL_LENTH_TO_EXAMINE = 5;

		// Token: 0x04001688 RID: 5768
		private const int SWAP_STOP_LEFT = 1;

		// Token: 0x04001689 RID: 5769
		private const int SWAP_STOP_RIGHT = 2;

		// Token: 0x0400168A RID: 5770
		private const int SWAP_STOP_NONE = 3;

		// Token: 0x0400168B RID: 5771
		private static WaitCallback s_timerDelegate = new WaitCallback(TimerQueue.FireTimerCallback);

		// Token: 0x0400168C RID: 5772
		private static int s_minTailLengthToExamine = ((Environment.ProcessorCount * 3 < 5) ? 5 : (Environment.ProcessorCount * 3));

		// Token: 0x0400168D RID: 5773
		private object m_lock;

		// Token: 0x0400168E RID: 5774
		private TimerQueue.Sentinel m_sentinel;

		// Token: 0x0400168F RID: 5775
		private bool m_waitableTimerBeingAdjusted;

		// Token: 0x04001690 RID: 5776
		private TimerObject m_sortedTail;

		// Token: 0x04001691 RID: 5777
		private TimerObject m_waitCallbackTimer;

		// Token: 0x04001692 RID: 5778
		private WaitableTimer m_waitableTimer;

		// Token: 0x04001693 RID: 5779
		private int m_countOfTimersEnqueued;

		// Token: 0x02000428 RID: 1064
		// (Invoke) Token: 0x06002516 RID: 9494
		internal delegate void Apply(TimerObject timer);

		// Token: 0x02000429 RID: 1065
		private class Sentinel : TimerObject
		{
			// Token: 0x06002519 RID: 9497 RVA: 0x00071DAC File Offset: 0x0006FFAC
			public Sentinel()
				: base(FileTime.MaxValue)
			{
			}

			// Token: 0x0600251A RID: 9498 RVA: 0x00071DB9 File Offset: 0x0006FFB9
			protected override void OnTimerElapsed()
			{
				ReleaseAssert.IsTrue(false);
			}
		}

		// Token: 0x0200042A RID: 1066
		private class RetryTimer : TimerObject
		{
			// Token: 0x0600251B RID: 9499 RVA: 0x00071DC1 File Offset: 0x0006FFC1
			public RetryTimer(TimerObject expiredTimer, TimerQueue timerQueue)
				: base(FileTime.MaxValue)
			{
				ReleaseAssert.IsTrue(!expiredTimer.IsEnqueued);
				this._expiredTimer = expiredTimer;
				this._timerQueue = timerQueue;
			}

			// Token: 0x0600251C RID: 9500 RVA: 0x00071DEA File Offset: 0x0006FFEA
			public void Enqueue()
			{
				base.ExpirationTime = FileTime.FromTimeSpan(TimerQueue.RetryTimer.RetryTime);
				this._timerQueue.Enqueue(this);
			}

			// Token: 0x0600251D RID: 9501 RVA: 0x00071E08 File Offset: 0x00070008
			protected override void OnTimerElapsed()
			{
				this._expiredTimer.FireTimer();
			}

			// Token: 0x04001694 RID: 5780
			private static TimeSpan RetryTime = new TimeSpan(0, 0, 10);

			// Token: 0x04001695 RID: 5781
			private TimerObject _expiredTimer;

			// Token: 0x04001696 RID: 5782
			private TimerQueue _timerQueue;
		}
	}
}
