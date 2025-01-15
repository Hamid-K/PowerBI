using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007EC RID: 2028
	public class CachedList : List<PositionMatch>
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x00078B36 File Offset: 0x00076D36
		public CachedList()
		{
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00078B3E File Offset: 0x00076D3E
		public CachedList(IEnumerable<PositionMatch> l)
			: base(l)
		{
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x00078B48 File Offset: 0x00076D48
		public Record<int, int>? GetValues(uint start, uint end)
		{
			int num = this.BinarySearchForFirstGreaterOrEqual(start);
			if (num == -1 || base[num].Right > end)
			{
				return null;
			}
			int num2 = this.BinarySearchForLastLessThanOrEqual(end);
			if (num2 == -1 || base[num2].Position < start)
			{
				return null;
			}
			return new Record<int, int>?(Record.Create<int, int>(num, num2));
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00078BAC File Offset: 0x00076DAC
		public bool TryGetMatchStartingAt(uint pos, out PositionMatch match)
		{
			int num = this.BinarySearchForFirstGreaterOrEqual(pos);
			if (num < 0)
			{
				match = default(PositionMatch);
				return false;
			}
			return (match = base[num]).Position == pos;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x00078BE8 File Offset: 0x00076DE8
		public bool TryGetMatchEndingAt(uint right, out PositionMatch match)
		{
			int num = this.BinarySearchForLastLessThanOrEqual(right);
			if (num < 0)
			{
				match = default(PositionMatch);
				return false;
			}
			while (num > 0 && base[num - 1].Right == right)
			{
				num--;
			}
			return (match = base[num]).Right == right;
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00078C3C File Offset: 0x00076E3C
		public int BinarySearchForFirstGreaterOrEqual(uint key)
		{
			int count = base.Count;
			int num = 0;
			int i = count - 1;
			while (i >= num)
			{
				int num2 = num + (i - num) / 2;
				uint position = base[num2].Position;
				if (position < key)
				{
					num = num2 + 1;
				}
				else
				{
					if (position <= key)
					{
						return num2;
					}
					i = num2 - 1;
				}
			}
			if (num < base.Count)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00078C94 File Offset: 0x00076E94
		public int BinarySearchForLastLessThanOrEqual(uint key)
		{
			int count = base.Count;
			int num = 0;
			int i = count - 1;
			while (i >= num)
			{
				int num2 = num + (i - num) / 2;
				uint right = base[num2].Right;
				if (right < key)
				{
					num = num2 + 1;
				}
				else
				{
					if (right <= key)
					{
						while (num2 < i && base[num2 + 1].Right == key)
						{
							num2++;
						}
						return num2;
					}
					i = num2 - 1;
				}
			}
			if (i == base.Count)
			{
				return i - 1;
			}
			if (i >= 0)
			{
				return i;
			}
			return -1;
		}
	}
}
