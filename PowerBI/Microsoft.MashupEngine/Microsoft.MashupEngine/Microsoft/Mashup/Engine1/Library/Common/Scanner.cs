using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001124 RID: 4388
	internal class Scanner
	{
		// Token: 0x060072B8 RID: 29368 RVA: 0x0018A2C2 File Offset: 0x001884C2
		public Scanner(string input)
		{
			this.input = input;
			this.position = 0;
		}

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x060072B9 RID: 29369 RVA: 0x0018A2D8 File Offset: 0x001884D8
		public bool HasMore
		{
			get
			{
				return this.input.Length != this.position;
			}
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x0018A2F0 File Offset: 0x001884F0
		public char Peek()
		{
			this.EnsureHasMore();
			return this.input[this.position];
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x0018A30C File Offset: 0x0018850C
		public char Pop()
		{
			this.EnsureHasMore();
			string text = this.input;
			int num = this.position;
			this.position = num + 1;
			return text[num];
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x0018A33C File Offset: 0x0018853C
		public void Pop(char expected)
		{
			char c = this.Pop();
			if (c != expected)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Expected character '{0}' at position '{1}' but found {2}.", expected, this.position, c));
			}
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x0018A380 File Offset: 0x00188580
		private void EnsureHasMore()
		{
			if (!this.HasMore)
			{
				throw new FormatException("Unexpected EOF.");
			}
		}

		// Token: 0x04003F3E RID: 16190
		private readonly string input;

		// Token: 0x04003F3F RID: 16191
		private int position;
	}
}
