using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B7 RID: 183
	internal class SaveChangesEventArgs : EventArgs
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0001B351 File Offset: 0x00019551
		public SaveChangesEventArgs(DataServiceResponse response)
		{
			this.response = response;
		}

		// Token: 0x040002B3 RID: 691
		[SuppressMessage("Microsoft.Performance", "CA1823", Justification = "No upstream callers.")]
		private DataServiceResponse response;
	}
}
