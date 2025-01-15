using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B3 RID: 4275
	internal sealed class DisposingDataReader : DelegatingDataReaderWithTableSchema
	{
		// Token: 0x06006FFC RID: 28668 RVA: 0x0018193F File Offset: 0x0017FB3F
		public DisposingDataReader(IDataReaderWithTableSchema reader, IDisposable disposable)
			: base(reader)
		{
			this.disposable = disposable;
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x0018194F File Offset: 0x0017FB4F
		public override void Dispose()
		{
			base.Dispose();
			if (this.disposable != null)
			{
				this.disposable.Dispose();
			}
			this.disposable = null;
		}

		// Token: 0x04003DF6 RID: 15862
		private IDisposable disposable;
	}
}
