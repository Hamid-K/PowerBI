using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x020001EB RID: 491
	internal sealed class NonIndentedTextWriter : TextWriterWrapper
	{
		// Token: 0x06001356 RID: 4950 RVA: 0x00037D46 File Offset: 0x00035F46
		public NonIndentedTextWriter(TextWriter writer)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00037D5B File Offset: 0x00035F5B
		public override void Write(string s)
		{
			this.writer.Write(s);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00037D69 File Offset: 0x00035F69
		public override void Write(char value)
		{
			this.writer.Write(value);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0000250D File Offset: 0x0000070D
		public override void WriteLine()
		{
		}
	}
}
