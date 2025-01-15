using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x0200021D RID: 541
	internal sealed class NonIndentedTextWriter : TextWriterWrapper
	{
		// Token: 0x060017D9 RID: 6105 RVA: 0x0004411A File Offset: 0x0004231A
		public NonIndentedTextWriter(TextWriter writer)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0004412F File Offset: 0x0004232F
		public override void Write(string s)
		{
			this.writer.Write(s);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0004413D File Offset: 0x0004233D
		public override void Write(char value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0000239D File Offset: 0x0000059D
		public override void WriteLine()
		{
		}
	}
}
