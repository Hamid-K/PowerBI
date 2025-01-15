using System;
using System.IO;
using System.Text;

namespace Microsoft.OData.Json
{
	// Token: 0x02000221 RID: 545
	internal abstract class TextWriterWrapper : TextWriter
	{
		// Token: 0x060017EA RID: 6122 RVA: 0x00044804 File Offset: 0x00042A04
		protected TextWriterWrapper(IFormatProvider formatProvider)
			: base(formatProvider)
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0004480D File Offset: 0x00042A0D
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0004481A File Offset: 0x00042A1A
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0000239D File Offset: 0x0000059D
		public virtual void IncreaseIndentation()
		{
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0000239D File Offset: 0x0000059D
		public virtual void DecreaseIndentation()
		{
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00044827 File Offset: 0x00042A27
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000032BD File Offset: 0x000014BD
		protected static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000AB3 RID: 2739
		protected TextWriter writer;
	}
}
