using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001102 RID: 4354
	internal sealed class PageReaderDataReaderSource : IDataReaderSource, IDisposable
	{
		// Token: 0x060071EC RID: 29164 RVA: 0x00187A79 File Offset: 0x00185C79
		public PageReaderDataReaderSource(IPageReader pageReader)
		{
			this.pageReader = pageReader;
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x060071ED RID: 29165 RVA: 0x000020FA File Offset: 0x000002FA
		public ITableSource TableSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x060071EE RID: 29166 RVA: 0x00187A88 File Offset: 0x00185C88
		public IPageReader PageReader
		{
			get
			{
				return this.pageReader;
			}
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x00187A90 File Offset: 0x00185C90
		public void Dispose()
		{
			if (this.pageReader != null)
			{
				this.pageReader.Dispose();
				this.pageReader = null;
			}
		}

		// Token: 0x04003EF5 RID: 16117
		private IPageReader pageReader;
	}
}
