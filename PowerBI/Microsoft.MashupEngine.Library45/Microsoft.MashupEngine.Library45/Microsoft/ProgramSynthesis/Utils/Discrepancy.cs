using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000482 RID: 1154
	public class Discrepancy<T> : Tuple<int, Optional<T>, Optional<T>>
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x0004EDF8 File Offset: 0x0004CFF8
		public Discrepancy(int index, Optional<T> leftItem, Optional<T> rightItem)
			: base(index, leftItem, rightItem)
		{
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0004EE03 File Offset: 0x0004D003
		public int Index
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0004EE0B File Offset: 0x0004D00B
		public Optional<T> LeftItem
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0004EE13 File Offset: 0x0004D013
		public Optional<T> RightItem
		{
			get
			{
				return base.Item3;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0004EE1C File Offset: 0x0004D01C
		public bool LeftListIsPrefixOfRight
		{
			get
			{
				return !this.LeftItem.HasValue;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0004EE3C File Offset: 0x0004D03C
		public bool RightListIsPrefixOfLeft
		{
			get
			{
				return !this.RightItem.HasValue;
			}
		}
	}
}
