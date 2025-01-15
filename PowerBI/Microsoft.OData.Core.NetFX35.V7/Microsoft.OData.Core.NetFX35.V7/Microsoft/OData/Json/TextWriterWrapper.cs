using System;
using System.IO;
using System.Text;

namespace Microsoft.OData.Json
{
	// Token: 0x020001EE RID: 494
	internal abstract class TextWriterWrapper : TextWriter
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x000381E9 File Offset: 0x000363E9
		protected TextWriterWrapper(IFormatProvider formatProvider)
			: base(formatProvider)
		{
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x000381F2 File Offset: 0x000363F2
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x000381FF File Offset: 0x000363FF
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0000250D File Offset: 0x0000070D
		public virtual void IncreaseIndentation()
		{
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0000250D File Offset: 0x0000070D
		public virtual void DecreaseIndentation()
		{
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0003820C File Offset: 0x0003640C
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0000FA90 File Offset: 0x0000DC90
		protected static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040009C7 RID: 2503
		protected TextWriter writer;
	}
}
