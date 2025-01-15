using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x02000A23 RID: 2595
	internal class StringLineReader : ILineReader, IDisposable
	{
		// Token: 0x0600488A RID: 18570 RVA: 0x000F27BF File Offset: 0x000F09BF
		public StringLineReader(string text)
		{
			this.text = text;
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x000F27CE File Offset: 0x000F09CE
		public string GetLine()
		{
			return this.text;
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x000F27D6 File Offset: 0x000F09D6
		public char[] Text
		{
			get
			{
				return this.chars;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x00002105 File Offset: 0x00000305
		public int LineStart
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000F27DE File Offset: 0x000F09DE
		public int LineEnd
		{
			get
			{
				return this.text.Length;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x000F27EB File Offset: 0x000F09EB
		public bool HasQuotes
		{
			get
			{
				return this.hasQuotes;
			}
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x000F27F3 File Offset: 0x000F09F3
		public bool MoveNext()
		{
			if (this.chars != null)
			{
				return false;
			}
			this.hasQuotes = this.text.IndexOf('"') != -1;
			this.chars = this.text.ToCharArray();
			return true;
		}

		// Token: 0x040026BA RID: 9914
		private string text;

		// Token: 0x040026BB RID: 9915
		private char[] chars;

		// Token: 0x040026BC RID: 9916
		private bool hasQuotes;
	}
}
