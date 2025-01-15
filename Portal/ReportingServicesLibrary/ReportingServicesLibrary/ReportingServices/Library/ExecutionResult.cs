using System;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200010B RID: 267
	internal sealed class ExecutionResult
	{
		// Token: 0x04000494 RID: 1172
		public RSStream OutputStream;

		// Token: 0x04000495 RID: 1173
		public Warning[] Warnings;

		// Token: 0x04000496 RID: 1174
		public ParameterInfoCollection EffectiveParameters;

		// Token: 0x04000497 RID: 1175
		public string[] StreamIds;
	}
}
