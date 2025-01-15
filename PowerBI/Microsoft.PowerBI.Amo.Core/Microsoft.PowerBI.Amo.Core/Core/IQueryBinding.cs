using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000E7 RID: 231
	[Guid("3C586F85-9994-4FDF-8DD8-31BB0D583C59")]
	public interface IQueryBinding : IBinding, IComponent, IDisposable
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000E63 RID: 3683
		// (set) Token: 0x06000E64 RID: 3684
		string DataSourceID { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000E65 RID: 3685
		// (set) Token: 0x06000E66 RID: 3686
		string QueryDefinition { get; set; }
	}
}
