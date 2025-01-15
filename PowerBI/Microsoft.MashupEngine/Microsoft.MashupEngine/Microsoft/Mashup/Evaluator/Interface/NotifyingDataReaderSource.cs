using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E3C RID: 7740
	public sealed class NotifyingDataReaderSource : IDataReaderSource, IDisposable
	{
		// Token: 0x0600BE63 RID: 48739 RVA: 0x00268234 File Offset: 0x00266434
		public NotifyingDataReaderSource(IDataReaderSource dataReaderSource, Action callback)
		{
			this.dataReaderSource = dataReaderSource;
			this.callback = callback;
		}

		// Token: 0x17002ED3 RID: 11987
		// (get) Token: 0x0600BE64 RID: 48740 RVA: 0x0026824A File Offset: 0x0026644A
		public ITableSource TableSource
		{
			get
			{
				return this.dataReaderSource.TableSource;
			}
		}

		// Token: 0x17002ED4 RID: 11988
		// (get) Token: 0x0600BE65 RID: 48741 RVA: 0x00268257 File Offset: 0x00266457
		public IPageReader PageReader
		{
			get
			{
				return this.dataReaderSource.PageReader;
			}
		}

		// Token: 0x0600BE66 RID: 48742 RVA: 0x00268264 File Offset: 0x00266464
		public void Dispose()
		{
			if (this.callback != null)
			{
				Action action = this.callback;
				this.callback = null;
				action();
			}
			if (this.dataReaderSource != null)
			{
				this.dataReaderSource.Dispose();
				this.dataReaderSource = null;
			}
		}

		// Token: 0x040060F8 RID: 24824
		private IDataReaderSource dataReaderSource;

		// Token: 0x040060F9 RID: 24825
		private Action callback;
	}
}
