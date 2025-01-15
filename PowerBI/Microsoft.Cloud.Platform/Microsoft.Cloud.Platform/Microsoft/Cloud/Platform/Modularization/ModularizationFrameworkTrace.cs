using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000CD RID: 205
	public class ModularizationFrameworkTrace : TraceSourceBase<ModularizationFrameworkTrace>
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00014B76 File Offset: 0x00012D76
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.ModularizationFramework");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
