using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData.Client
{
	// Token: 0x0200004C RID: 76
	internal class DataServiceSaveStream
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000952A File Offset: 0x0000772A
		internal DataServiceSaveStream(Stream stream, bool close, DataServiceRequestArgs args)
		{
			this.stream = stream;
			this.close = close;
			this.args = args;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009547 File Offset: 0x00007747
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000954F File Offset: 0x0000774F
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00009557 File Offset: 0x00007757
		[SuppressMessage("Microsoft.Performance", "CA1811", Justification = "The setter is called during de-serialization")]
		internal DataServiceRequestArgs Args
		{
			get
			{
				return this.args;
			}
			set
			{
				this.args = value;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009560 File Offset: 0x00007760
		internal void Close()
		{
			if (this.stream != null && this.close)
			{
				this.stream.Close();
			}
		}

		// Token: 0x040000CC RID: 204
		private DataServiceRequestArgs args;

		// Token: 0x040000CD RID: 205
		private Stream stream;

		// Token: 0x040000CE RID: 206
		private bool close;
	}
}
