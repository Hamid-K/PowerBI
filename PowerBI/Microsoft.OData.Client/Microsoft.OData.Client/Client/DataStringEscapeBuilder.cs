using System;
using System.Text;

namespace Microsoft.OData.Client
{
	// Token: 0x02000067 RID: 103
	internal class DataStringEscapeBuilder
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000D591 File Offset: 0x0000B791
		private DataStringEscapeBuilder(string dataString)
		{
			this.input = dataString;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		internal static string EscapeDataString(string input)
		{
			DataStringEscapeBuilder dataStringEscapeBuilder = new DataStringEscapeBuilder(input);
			return dataStringEscapeBuilder.Build();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		private string Build()
		{
			this.index = 0;
			while (this.index < this.input.Length)
			{
				char c = this.input[this.index];
				if (c == '\'' || c == '"')
				{
					this.ReadQuotedString(c);
				}
				else if ("+".IndexOf(c) >= 0)
				{
					this.output.Append(Uri.EscapeDataString(c.ToString()));
				}
				else
				{
					this.output.Append(c);
				}
				this.index++;
			}
			return this.output.ToString();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D664 File Offset: 0x0000B864
		private void ReadQuotedString(char quoteStart)
		{
			if (this.quotedDataBuilder == null)
			{
				this.quotedDataBuilder = new StringBuilder();
			}
			this.output.Append(quoteStart);
			for (;;)
			{
				int num = this.index + 1;
				this.index = num;
				if (num >= this.input.Length)
				{
					goto IL_00AB;
				}
				if (this.input[this.index] == quoteStart)
				{
					break;
				}
				this.quotedDataBuilder.Append(this.input[this.index]);
			}
			this.output.Append(Uri.EscapeDataString(this.quotedDataBuilder.ToString()));
			this.output.Append(quoteStart);
			this.quotedDataBuilder.Clear();
			IL_00AB:
			if (this.quotedDataBuilder.Length > 0)
			{
				this.output.Append(Uri.EscapeDataString(this.quotedDataBuilder.ToString()));
				this.quotedDataBuilder.Clear();
			}
		}

		// Token: 0x0400011D RID: 285
		private const string SensitiveCharacters = "+";

		// Token: 0x0400011E RID: 286
		private readonly string input;

		// Token: 0x0400011F RID: 287
		private readonly StringBuilder output = new StringBuilder();

		// Token: 0x04000120 RID: 288
		private int index;

		// Token: 0x04000121 RID: 289
		private StringBuilder quotedDataBuilder;
	}
}
