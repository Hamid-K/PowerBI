using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003A RID: 58
	internal interface IParameterSource
	{
		// Token: 0x060001C3 RID: 451
		string GetSourceNameForTrace();

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C4 RID: 452
		bool UseExternalStore { get; }

		// Token: 0x060001C5 RID: 453
		string GetParameter(string name);
	}
}
