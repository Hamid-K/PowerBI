using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x02000A22 RID: 2594
	internal class ListLineReader : ILineReader, IDisposable
	{
		// Token: 0x06004882 RID: 18562 RVA: 0x000F270D File Offset: 0x000F090D
		public ListLineReader(ListValue list)
		{
			this.list = list;
			this.listIndex = -1;
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x000F2723 File Offset: 0x000F0923
		public string GetLine()
		{
			return this.line;
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x000F272B File Offset: 0x000F092B
		public char[] Text
		{
			get
			{
				return this.chars;
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x00002105 File Offset: 0x00000305
		public int LineStart
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x000F2733 File Offset: 0x000F0933
		public int LineEnd
		{
			get
			{
				return this.line.Length;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x000F2740 File Offset: 0x000F0940
		public bool HasQuotes
		{
			get
			{
				return this.hasQuotes;
			}
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x000F2748 File Offset: 0x000F0948
		public bool MoveNext()
		{
			this.listIndex++;
			if (this.listIndex >= this.list.Count)
			{
				return false;
			}
			this.line = this.list[this.listIndex].AsString;
			this.hasQuotes = this.line.IndexOf('"') != -1;
			this.chars = this.line.ToCharArray();
			return true;
		}

		// Token: 0x040026B5 RID: 9909
		private readonly ListValue list;

		// Token: 0x040026B6 RID: 9910
		private int listIndex;

		// Token: 0x040026B7 RID: 9911
		private string line;

		// Token: 0x040026B8 RID: 9912
		private char[] chars;

		// Token: 0x040026B9 RID: 9913
		private bool hasQuotes;
	}
}
