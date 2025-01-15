using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000008 RID: 8
	internal static class ProviderSpecificPropertyGroups
	{
		// Token: 0x0200000C RID: 12
		internal static class MSOLAP
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000B RID: 11 RVA: 0x00002380 File Offset: 0x00000580
			internal static Guid MSOLAPInit
			{
				get
				{
					return new Guid(2692533508U, 33096, 4560, 135, 187, 0, 192, 79, 195, 57, 66);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000C RID: 12 RVA: 0x000023BC File Offset: 0x000005BC
			internal static Guid MSOLAPDataSource
			{
				get
				{
					return new Guid(3282303852U, 6809, 17132, 189, 182, 204, 142, 251, 218, 116, 120);
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000D RID: 13 RVA: 0x00002400 File Offset: 0x00000600
			internal static Guid MSOLAPCommand
			{
				get
				{
					return new Guid(2550694911U, 45562, 20463, 170, 146, 62, 89, 96, 179, 15, 149);
				}
			}
		}
	}
}
