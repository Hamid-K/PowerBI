using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001106 RID: 4358
	internal class ProgressDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x060071FF RID: 29183 RVA: 0x00187DF3 File Offset: 0x00185FF3
		public ProgressDbDataReader(DbDataReaderWithTableSchema reader, IHostProgress hostProgress)
			: base(reader)
		{
			this.hostProgress = hostProgress;
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x00187E03 File Offset: 0x00186003
		public override bool Read()
		{
			bool flag = base.Read();
			if (flag)
			{
				this.hostProgress.RecordRowRead();
			}
			return flag;
		}

		// Token: 0x04003EFB RID: 16123
		private IHostProgress hostProgress;
	}
}
