using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000298 RID: 664
	[Serializable]
	internal sealed class ChunkMemoryStream : MemoryStream
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x00062459 File Offset: 0x00060659
		public override void Close()
		{
			if (this.CanBeClosed)
			{
				base.Close();
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00062469 File Offset: 0x00060669
		protected override void Dispose(bool disposing)
		{
			if (this.CanBeClosed)
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x040008BC RID: 2236
		[NonSerialized]
		public bool CanBeClosed;
	}
}
