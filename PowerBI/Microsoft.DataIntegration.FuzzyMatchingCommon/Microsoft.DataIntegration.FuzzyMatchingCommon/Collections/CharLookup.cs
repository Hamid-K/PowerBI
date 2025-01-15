using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	internal class CharLookup
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x000241E7 File Offset: 0x000223E7
		public CharLookup()
			: this(256)
		{
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000241F4 File Offset: 0x000223F4
		public CharLookup(int arrayCharCount)
		{
			this.ArrayCharCount = arrayCharCount;
			this.m_itemsArray = new short[this.ArrayCharCount];
			for (int i = 0; i < this.m_itemsArray.Length; i++)
			{
				this.m_itemsArray[i] = -1;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0002423B File Offset: 0x0002243B
		public bool TryGetValue(char c, out short cost)
		{
			cost = -1;
			if ((int)c < this.ArrayCharCount)
			{
				cost = this.m_itemsArray[(int)c];
				return cost != -1;
			}
			return this.m_itemsHash != null && this.m_itemsHash.TryGetValue(c, ref cost);
		}

		// Token: 0x17000114 RID: 276
		public short this[char index]
		{
			set
			{
				if ((int)index < this.ArrayCharCount)
				{
					this.m_itemsArray[(int)index] = value;
				}
				if (this.m_itemsHash == null)
				{
					this.m_itemsHash = new Dictionary<char, short>();
				}
				this.m_itemsHash[index] = value;
			}
		}

		// Token: 0x04000160 RID: 352
		private readonly int ArrayCharCount;

		// Token: 0x04000161 RID: 353
		private const short UndefinedCost = -1;

		// Token: 0x04000162 RID: 354
		private short[] m_itemsArray;

		// Token: 0x04000163 RID: 355
		private Dictionary<char, short> m_itemsHash;
	}
}
