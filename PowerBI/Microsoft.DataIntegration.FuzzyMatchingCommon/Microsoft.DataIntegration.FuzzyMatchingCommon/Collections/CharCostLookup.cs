using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	internal class CharCostLookup
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x000240BA File Offset: 0x000222BA
		private int FormChar2(char from, char to)
		{
			return (int)(((int)from << 16) + to);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000240C2 File Offset: 0x000222C2
		private long FormChar3(char preceding, char from, char to)
		{
			return (long)(preceding + (from << 16) + to);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000240D0 File Offset: 0x000222D0
		public int GetCost(char precedingChar, char from, char to)
		{
			short num;
			switch (this.mode)
			{
			case 1:
				goto IL_0050;
			case 2:
				break;
			case 3:
				if (this.m_costs3.TryGetValue(this.FormChar3(precedingChar, from, to), ref num))
				{
					return (int)num;
				}
				break;
			default:
				return 10;
			}
			if (this.m_costs2.TryGetValue(this.FormChar2(from, to), ref num))
			{
				return (int)num;
			}
			IL_0050:
			if (this.m_costs1.TryGetValue(from, out num))
			{
				return (int)num;
			}
			return 10;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00024144 File Offset: 0x00022344
		public void SetCharCost(string from, short cost)
		{
			for (int i = 0; i < from.Length; i++)
			{
				char c = from.get_Chars(i);
				this.SetCharCost(c, cost);
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00024174 File Offset: 0x00022374
		public void SetCharCost(char from, short cost)
		{
			this.m_costs1[from] = cost;
			this.mode = Math.Max(this.mode, 1);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00024195 File Offset: 0x00022395
		public void SetCharCost(char from, char to, short cost)
		{
			this.m_costs2[this.FormChar2(from, to)] = cost;
			this.mode = Math.Max(this.mode, 2);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000241BD File Offset: 0x000223BD
		public void SetCharCost(char precedingChar, char from, char to, short cost)
		{
			this.m_costs3[this.FormChar3(precedingChar, from, to)] = cost;
			this.mode = Math.Max(this.mode, 3);
		}

		// Token: 0x0400015A RID: 346
		private const int ArraySize = 256;

		// Token: 0x0400015B RID: 347
		private const short DefaultCost = 10;

		// Token: 0x0400015C RID: 348
		private CharLookup m_costs1 = new CharLookup(256);

		// Token: 0x0400015D RID: 349
		private Dictionary<int, short> m_costs2 = new Dictionary<int, short>();

		// Token: 0x0400015E RID: 350
		private Dictionary<long, short> m_costs3 = new Dictionary<long, short>();

		// Token: 0x0400015F RID: 351
		private short mode;
	}
}
