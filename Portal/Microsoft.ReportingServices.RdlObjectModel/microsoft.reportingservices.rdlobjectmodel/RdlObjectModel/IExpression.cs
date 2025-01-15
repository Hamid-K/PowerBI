using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D8 RID: 472
	public interface IExpression
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000F69 RID: 3945
		// (set) Token: 0x06000F6A RID: 3946
		object Value { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000F6B RID: 3947
		// (set) Token: 0x06000F6C RID: 3948
		string Expression { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000F6D RID: 3949
		bool IsExpression { get; }

		// Token: 0x06000F6E RID: 3950
		void GetDependencies(IList<ReportObject> dependencies, ReportObject parent);
	}
}
