using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	internal sealed class CharacterMap
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00002F06 File Offset: 0x00001106
		public CharacterMap()
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002F1E File Offset: 0x0000111E
		public CharacterMap(CharacterMap.CharMap map)
		{
			this.BuildMap(map);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002F40 File Offset: 0x00001140
		public void BuildMap(CharacterMap.CharMap map)
		{
			for (char c = '\0'; c < '\uffff'; c += '\u0001')
			{
				this.m_charMap[(int)c] = map(c);
			}
		}

		// Token: 0x17000014 RID: 20
		public char this[char c]
		{
			get
			{
				return this.m_charMap[(int)c];
			}
		}

		// Token: 0x0400001F RID: 31
		private char[] m_charMap = new char[65536];

		// Token: 0x020000C6 RID: 198
		// (Invoke) Token: 0x0600086C RID: 2156
		public delegate char CharMap(char c);
	}
}
