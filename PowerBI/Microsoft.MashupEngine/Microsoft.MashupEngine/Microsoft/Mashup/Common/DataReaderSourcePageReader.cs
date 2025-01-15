using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE0 RID: 7136
	public sealed class DataReaderSourcePageReader : DelegatingPageReader, IPageReaderWithTableSource, IPageReader, IDisposable
	{
		// Token: 0x0600B21E RID: 45598 RVA: 0x00245228 File Offset: 0x00243428
		public DataReaderSourcePageReader(IDataReaderSource dataReaderSource)
			: base(dataReaderSource.PageReader)
		{
			this.dataReaderSource = dataReaderSource;
		}

		// Token: 0x17002CBC RID: 11452
		// (get) Token: 0x0600B21F RID: 45599 RVA: 0x0024523D File Offset: 0x0024343D
		public ITableSource TableSource
		{
			get
			{
				return this.dataReaderSource.TableSource;
			}
		}

		// Token: 0x0600B220 RID: 45600 RVA: 0x0024524A File Offset: 0x0024344A
		public override void Dispose()
		{
			base.Dispose();
			this.dataReaderSource.Dispose();
		}

		// Token: 0x04005B30 RID: 23344
		private readonly IDataReaderSource dataReaderSource;
	}
}
