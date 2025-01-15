using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000539 RID: 1337
	internal class PersistenceConstants
	{
		// Token: 0x0400206E RID: 8302
		internal const int NullReferenceID = -2;

		// Token: 0x0400206F RID: 8303
		internal static readonly int MajorVersion = 12;

		// Token: 0x04002070 RID: 8304
		internal static readonly int MinorVersion = 3;

		// Token: 0x04002071 RID: 8305
		internal const int UndefinedCompatVersion = 0;

		// Token: 0x04002072 RID: 8306
		internal static readonly int CurrentCompatVersion = ReportProcessingCompatibilityVersion.CurrentVersion;
	}
}
