using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	internal sealed class KeyColumnEqualityComparer : IEqualityComparer<object[]>
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x000145B4 File Offset: 0x000127B4
		public KeyColumnEqualityComparer(int[] keyIndexes)
		{
			this.m_keyIndexes = keyIndexes;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000145C4 File Offset: 0x000127C4
		public bool Equals(object[] x, object[] y)
		{
			if (this.m_keyIndexes.Length != 0)
			{
				for (int i = 0; i < this.m_keyIndexes.Length; i++)
				{
					if (x[this.m_keyIndexes[i]] == null)
					{
						if (y[this.m_keyIndexes[i]] != null)
						{
							return false;
						}
					}
					else if (!x[this.m_keyIndexes[i]].Equals(y[this.m_keyIndexes[i]]))
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = 0; j < x.Length; j++)
				{
					if (x[j] == null)
					{
						if (y[j] != null)
						{
							return false;
						}
					}
					else if (!x[j].Equals(y[j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00014650 File Offset: 0x00012850
		public int GetHashCode(object[] x)
		{
			int num = 0;
			if (this.m_keyIndexes.Length != 0)
			{
				for (int i = 0; i < this.m_keyIndexes.Length; i++)
				{
					if (x[this.m_keyIndexes[i]] != null)
					{
						num ^= x[this.m_keyIndexes[i]].GetHashCode();
					}
				}
			}
			else
			{
				for (int j = 0; j < x.Length; j++)
				{
					if (x[j] != null)
					{
						num ^= x[j].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x04000173 RID: 371
		private int[] m_keyIndexes;
	}
}
