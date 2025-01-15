using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C0 RID: 704
	public interface IDefinitionPath
	{
		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06001AB7 RID: 6839
		string DefinitionPath { get; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06001AB8 RID: 6840
		IDefinitionPath ParentDefinitionPath { get; }
	}
}
