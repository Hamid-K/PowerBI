using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003FB RID: 1019
	internal struct Priority : IEquatable<Priority>
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x0006D4FC File Offset: 0x0006B6FC
		private Priority(Priority.PriorityValue value)
		{
			this.m_value = value;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0006D505 File Offset: 0x0006B705
		public static void InitializeThreadPriorities(ThreadPriority lowThreadPriority, ThreadPriority normalThreadPriority, ThreadPriority highThreadPriority)
		{
			Priority.s_lowThreadPriority = lowThreadPriority;
			Priority.s_normalThreadPriority = normalThreadPriority;
			Priority.s_highThreadPriority = highThreadPriority;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0006D519 File Offset: 0x0006B719
		public static ThreadPriority LowThreadPriority
		{
			get
			{
				return Priority.s_lowThreadPriority;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x0006D520 File Offset: 0x0006B720
		public static ThreadPriority NormalThreadPriority
		{
			get
			{
				return Priority.s_normalThreadPriority;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0006D527 File Offset: 0x0006B727
		public static ThreadPriority HighThreadPriority
		{
			get
			{
				return Priority.s_highThreadPriority;
			}
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0006D52E File Offset: 0x0006B72E
		public static bool operator ==(Priority p1, Priority p2)
		{
			return p1.m_value == p2.m_value;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0006D540 File Offset: 0x0006B740
		public static bool operator !=(Priority p1, Priority p2)
		{
			return p1.m_value != p2.m_value;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0006D555 File Offset: 0x0006B755
		public override int GetHashCode()
		{
			return (int)this.m_value;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x0006D55D File Offset: 0x0006B75D
		public override bool Equals(object obj)
		{
			return obj is Priority && this == (Priority)obj;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x0006D57A File Offset: 0x0006B77A
		public bool Equals(Priority other)
		{
			return this == other;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0006D588 File Offset: 0x0006B788
		public static bool operator >(Priority p1, Priority p2)
		{
			return p1.m_value > p2.m_value;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0006D59A File Offset: 0x0006B79A
		public static bool operator >=(Priority p1, Priority p2)
		{
			return p1.m_value >= p2.m_value;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0006D5AF File Offset: 0x0006B7AF
		public static bool operator <(Priority p1, Priority p2)
		{
			return p1.m_value < p2.m_value;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0006D5C1 File Offset: 0x0006B7C1
		public static bool operator <=(Priority p1, Priority p2)
		{
			return p1.m_value <= p2.m_value;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0006D5D6 File Offset: 0x0006B7D6
		public override string ToString()
		{
			return this.m_value.ToString();
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0006D5E8 File Offset: 0x0006B7E8
		public static Priority FromString(string priorityString)
		{
			Priority.PriorityValue priorityValue = (Priority.PriorityValue)Enum.Parse(typeof(Priority.PriorityValue), priorityString);
			return new Priority(priorityValue);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0006D614 File Offset: 0x0006B814
		internal static Priority GetPriority(Thread thread)
		{
			ThreadPriority priority = thread.Priority;
			Priority priority2;
			if (priority == Priority.s_highThreadPriority)
			{
				priority2 = Priority.HighPriority;
			}
			else if (priority == Priority.s_lowThreadPriority)
			{
				priority2 = Priority.LowPriority;
			}
			else
			{
				priority2 = Priority.NormalPriority;
			}
			return priority2;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0006D64F File Offset: 0x0006B84F
		public static void RevertPriority(Thread thread, ThreadPriority threadPriority)
		{
			if (thread.Priority != threadPriority)
			{
				thread.Priority = threadPriority;
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0006D664 File Offset: 0x0006B864
		public static void SetPriority(Thread thread, Priority priority, ThreadPriority currentThreadPriority)
		{
			ThreadPriority threadPriority;
			if (priority == Priority.NormalPriority)
			{
				threadPriority = Priority.NormalThreadPriority;
			}
			else if (priority == Priority.HighPriority)
			{
				threadPriority = Priority.HighThreadPriority;
			}
			else
			{
				threadPriority = Priority.LowThreadPriority;
			}
			if (currentThreadPriority != threadPriority)
			{
				thread.Priority = threadPriority;
			}
		}

		// Token: 0x04001626 RID: 5670
		public static readonly Priority LowPriority = new Priority(Priority.PriorityValue.Low);

		// Token: 0x04001627 RID: 5671
		public static readonly Priority NormalPriority = new Priority(Priority.PriorityValue.Normal);

		// Token: 0x04001628 RID: 5672
		public static readonly Priority HighPriority = new Priority(Priority.PriorityValue.High);

		// Token: 0x04001629 RID: 5673
		private static ThreadPriority s_lowThreadPriority = ThreadPriority.Lowest;

		// Token: 0x0400162A RID: 5674
		private static ThreadPriority s_normalThreadPriority = ThreadPriority.Normal;

		// Token: 0x0400162B RID: 5675
		private static ThreadPriority s_highThreadPriority = ThreadPriority.Highest;

		// Token: 0x0400162C RID: 5676
		private Priority.PriorityValue m_value;

		// Token: 0x020003FC RID: 1020
		private enum PriorityValue
		{
			// Token: 0x0400162E RID: 5678
			Low = -1,
			// Token: 0x0400162F RID: 5679
			Normal,
			// Token: 0x04001630 RID: 5680
			High
		}
	}
}
