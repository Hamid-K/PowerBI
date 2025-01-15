using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200008D RID: 141
	internal class TextPrimitiveParserToken : PrimitiveParserToken
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0000F3AB File Offset: 0x0000D5AB
		internal TextPrimitiveParserToken(string text)
		{
			this.Text = text;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000F3BA File Offset: 0x0000D5BA
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000F3C2 File Offset: 0x0000D5C2
		internal string Text { get; private set; }

		// Token: 0x0600044F RID: 1103 RVA: 0x0000F3CB File Offset: 0x0000D5CB
		internal override object Materialize(Type clrType)
		{
			return ClientConvert.ChangeType(this.Text, clrType);
		}
	}
}
