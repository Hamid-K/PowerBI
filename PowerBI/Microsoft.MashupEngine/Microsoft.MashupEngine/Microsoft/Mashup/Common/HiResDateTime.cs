using System;
using System.Diagnostics;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF9 RID: 7161
	public static class HiResDateTime
	{
		// Token: 0x0600B2C2 RID: 45762 RVA: 0x00246301 File Offset: 0x00244501
		static HiResDateTime()
		{
			HiResDateTime.offset.Start();
		}

		// Token: 0x17002CDA RID: 11482
		// (get) Token: 0x0600B2C3 RID: 45763 RVA: 0x00246321 File Offset: 0x00244521
		public static DateTime UtcNow
		{
			get
			{
				return HiResDateTime.epoch + HiResDateTime.offset.Elapsed;
			}
		}

		// Token: 0x04005B52 RID: 23378
		private static readonly DateTime epoch = DateTime.UtcNow;

		// Token: 0x04005B53 RID: 23379
		private static readonly Stopwatch offset = new Stopwatch();
	}
}
