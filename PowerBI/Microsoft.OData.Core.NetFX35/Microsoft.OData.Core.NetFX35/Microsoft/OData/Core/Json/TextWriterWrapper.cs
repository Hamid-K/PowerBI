using System;
using System.IO;
using System.Text;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000110 RID: 272
	internal abstract class TextWriterWrapper : TextWriter
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00025F19 File Offset: 0x00024119
		protected TextWriterWrapper(IFormatProvider formatProvider)
			: base(formatProvider)
		{
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00025F22 File Offset: 0x00024122
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00025F2F File Offset: 0x0002412F
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00025F3C File Offset: 0x0002413C
		public virtual void IncreaseIndentation()
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00025F3E File Offset: 0x0002413E
		public virtual void DecreaseIndentation()
		{
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00025F40 File Offset: 0x00024140
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00025F4D File Offset: 0x0002414D
		protected static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400040E RID: 1038
		protected TextWriter writer;
	}
}
