using System;
using System.IO;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200011C RID: 284
	internal sealed class NonIndentedTextWriter : TextWriterWrapper
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x000270EA File Offset: 0x000252EA
		public NonIndentedTextWriter(TextWriter writer)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000270FF File Offset: 0x000252FF
		public override void Write(string s)
		{
			this.writer.Write(s);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002710D File Offset: 0x0002530D
		public override void Write(char value)
		{
			this.writer.Write(value);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002711B File Offset: 0x0002531B
		public override void WriteLine()
		{
		}
	}
}
