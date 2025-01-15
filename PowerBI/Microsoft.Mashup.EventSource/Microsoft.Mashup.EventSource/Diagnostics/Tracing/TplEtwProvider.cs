using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200000D RID: 13
	internal static class TplEtwProvider
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000277F File Offset: 0x0000097F
		public static TplEtwProvider.Logger Log
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000012 RID: 18
		private static TplEtwProvider.Logger m_logger = new TplEtwProvider.Logger();

		// Token: 0x0200007F RID: 127
		public class Logger
		{
			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000E991 File Offset: 0x0000CB91
			public bool Debug
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000E994 File Offset: 0x0000CB94
			public void DebugFacilityMessage(params object[] args)
			{
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000E996 File Offset: 0x0000CB96
			public void SetActivityId(Guid guid)
			{
			}
		}
	}
}
