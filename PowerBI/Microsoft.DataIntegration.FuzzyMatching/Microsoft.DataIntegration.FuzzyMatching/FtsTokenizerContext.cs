using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E4 RID: 228
	internal sealed class FtsTokenizerContext : TokenizerContext
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x0002A4F4 File Offset: 0x000286F4
		public FtsTokenizerContext(int maxStringLength = 1048576)
		{
			this.m_charAllocator = new BlockedSegmentArray<char>(1, maxStringLength);
			this.m_tmpStrExtent.Array = this.m_buffer;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002A554 File Offset: 0x00028754
		public override void Reset()
		{
			this.m_charAllocator.Reset();
		}

		// Token: 0x04000387 RID: 903
		public IWordBreaker WordBreaker;

		// Token: 0x04000388 RID: 904
		public FtsTokenizerContext.WordSinkClass WordSink = new FtsTokenizerContext.WordSinkClass();

		// Token: 0x04000389 RID: 905
		public char[] m_buffer = new char[0];

		// Token: 0x0400038A RID: 906
		public char[] m_normalizationBuffer1 = new char[0];

		// Token: 0x0400038B RID: 907
		public char[] m_normalizationBuffer2 = new char[0];

		// Token: 0x0400038C RID: 908
		public StringExtent m_tmpStrExtent;

		// Token: 0x0400038D RID: 909
		public int[] ColumnIndexes;

		// Token: 0x0400038E RID: 910
		public bool[] IsString;

		// Token: 0x0400038F RID: 911
		public BlockedSegmentArray<char> m_charAllocator;

		// Token: 0x02000182 RID: 386
		internal class WordSinkClass : IWordSink
		{
			// Token: 0x06000D0D RID: 3341 RVA: 0x00037C50 File Offset: 0x00035E50
			public void PutWord(int cwc, string pwcInBuf, int cwcSrcLen, int cwcSrcPos)
			{
				if (cwcSrcPos > this.LastPos)
				{
					char[] array = pwcInBuf.Substring(0, cwc).ToCharArray();
					this.TokenList.Add(new StringExtent
					{
						Array = array,
						Offset = 0,
						Length = array.Length
					});
					this.LastPos = cwcSrcPos;
				}
			}

			// Token: 0x06000D0E RID: 3342 RVA: 0x00037CAB File Offset: 0x00035EAB
			public void PutAltWord(int cwc, string pwcInBuf, int cwcSrcLen, int cwcSrcPos)
			{
			}

			// Token: 0x06000D0F RID: 3343 RVA: 0x00037CAD File Offset: 0x00035EAD
			public void StartAltPhrase()
			{
			}

			// Token: 0x06000D10 RID: 3344 RVA: 0x00037CAF File Offset: 0x00035EAF
			public void EndAltPhrase()
			{
			}

			// Token: 0x06000D11 RID: 3345 RVA: 0x00037CB1 File Offset: 0x00035EB1
			public void PutBreak(WORDREP_BREAK_TYPE breakType)
			{
				switch (breakType)
				{
				default:
					return;
				}
			}

			// Token: 0x04000625 RID: 1573
			public int LastPos;

			// Token: 0x04000626 RID: 1574
			public ArraySegment<char> sourceString;

			// Token: 0x04000627 RID: 1575
			public List<StringExtent> TokenList = new List<StringExtent>();
		}
	}
}
