using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000EF RID: 239
	internal class VowelFoldingTokenizerContext : TokenizerContext
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002C054 File Offset: 0x0002A254
		public VowelFoldingTokenizerContext(string delimiters, int[] cols)
		{
			this.m_isDelimiter = new BitArray(65536);
			for (int i = 0; i < delimiters.Length; i++)
			{
				char c = delimiters.get_Chars(i);
				this.m_isDelimiter.Set((int)c, true);
			}
			this.m_buffer = new char[0];
			this.m_columns = cols;
			this.m_scratchStringExtent.Array = this.m_buffer;
			this.m_scratchStringExtent.Offset = 0;
			this.m_scratchStringExtent.Length = 0;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002C0DB File Offset: 0x0002A2DB
		public override void Reset()
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		public int CopyColumnToBuffer(IDataRecord record, int col)
		{
			string @string = record.GetString(col);
			if (this.m_buffer.Length < @string.Length)
			{
				Array.Resize<char>(ref this.m_buffer, @string.Length);
				this.m_scratchStringExtent.Array = this.m_buffer;
			}
			@string.CopyToEx(0, this.m_buffer, 0, @string.Length);
			return @string.Length;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002C141 File Offset: 0x0002A341
		public IEnumerable<StringExtent> Tokenize(IDataRecord record)
		{
			foreach (int num in this.m_columns)
			{
				if (!record.IsDBNull(num))
				{
					int attrLen = this.CopyColumnToBuffer(record, num);
					int num3;
					for (int i = 0; i < attrLen; i = num3 + 1)
					{
						if (!this.m_isDelimiter[(int)this.m_buffer[i]])
						{
							int num2 = i;
							bool flag = char.IsLetter(this.m_buffer[i]);
							for (;;)
							{
								num3 = i + 1;
								i = num3;
								if (num3 >= attrLen || this.m_isDelimiter[(int)this.m_buffer[i]])
								{
									break;
								}
								if (!char.IsLetter(this.m_buffer[i]))
								{
									flag = false;
								}
							}
							if (flag)
							{
								char c = '0';
								int num4 = num2;
								for (int k = num2; k < i; k++)
								{
									char c2 = this.m_buffer[k];
									if (c2 == 'A' || c2 == 'E' || c2 == 'I' || c2 == 'O' || c2 == 'U')
									{
										c2 = '_';
									}
									if (c2 != c)
									{
										this.m_buffer[num4++] = c2;
										c = c2;
									}
								}
								this.m_scratchStringExtent.Offset = num2;
								this.m_scratchStringExtent.Length = num4 - num2;
							}
							else
							{
								this.m_scratchStringExtent.Offset = num2;
								this.m_scratchStringExtent.Length = i - num2;
							}
							yield return this.m_scratchStringExtent;
						}
						num3 = i;
					}
				}
			}
			int[] array = null;
			yield break;
		}

		// Token: 0x040003AF RID: 943
		private BitArray m_isDelimiter;

		// Token: 0x040003B0 RID: 944
		private int[] m_columns;

		// Token: 0x040003B1 RID: 945
		public ITokenIdProvider m_tokenIdProvider;

		// Token: 0x040003B2 RID: 946
		private char[] m_buffer;

		// Token: 0x040003B3 RID: 947
		private StringExtent m_scratchStringExtent;
	}
}
