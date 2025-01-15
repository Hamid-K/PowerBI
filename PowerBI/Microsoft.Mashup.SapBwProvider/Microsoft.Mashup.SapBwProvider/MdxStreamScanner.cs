using System;
using System.Globalization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000032 RID: 50
	public class MdxStreamScanner
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000B431 File Offset: 0x00009631
		public MdxStreamScanner(string input)
		{
			this.input = input;
			this.position = 0;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B447 File Offset: 0x00009647
		public bool HasMore
		{
			get
			{
				return this.input.Length > this.position;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B45C File Offset: 0x0000965C
		public string ParseNext()
		{
			this.EnsureLength(4);
			int num = int.Parse(this.input.Substring(this.position, 3), CultureInfo.InvariantCulture);
			this.position += 4;
			int num2 = this.position;
			this.position += num + 1;
			this.EnsureLength(0);
			return this.input.Substring(num2, num);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B4C6 File Offset: 0x000096C6
		private void EnsureLength(int length)
		{
			if (this.input.Length < this.position + length)
			{
				throw new FormatException(Resources.UnexpectedEOF);
			}
		}

		// Token: 0x040001C6 RID: 454
		private readonly string input;

		// Token: 0x040001C7 RID: 455
		private int position;
	}
}
