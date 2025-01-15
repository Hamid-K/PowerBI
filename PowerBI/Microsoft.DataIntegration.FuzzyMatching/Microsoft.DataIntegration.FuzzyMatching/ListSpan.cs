using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	internal sealed class ListSpan<T>
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001BB30 File Offset: 0x00019D30
		internal ListSpan(T[] globalList)
		{
			this.globalList = globalList;
			this.beginPos = (this.endPos = 0);
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001BB5A File Offset: 0x00019D5A
		internal int Size
		{
			get
			{
				return this.endPos - this.beginPos;
			}
		}

		// Token: 0x17000148 RID: 328
		internal T this[int idx]
		{
			get
			{
				return this.globalList[this.beginPos + idx];
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001BB7E File Offset: 0x00019D7E
		internal void ResetGlobalList(T[] globalList)
		{
			this.globalList = globalList;
		}

		// Token: 0x04000235 RID: 565
		internal T[] globalList;

		// Token: 0x04000236 RID: 566
		internal int beginPos;

		// Token: 0x04000237 RID: 567
		internal int endPos;
	}
}
