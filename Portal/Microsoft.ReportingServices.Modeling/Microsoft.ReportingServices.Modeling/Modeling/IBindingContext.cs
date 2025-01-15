using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000065 RID: 101
	internal interface IBindingContext
	{
		// Token: 0x0600041A RID: 1050
		DataSourceView GetDataSourceView();

		// Token: 0x0600041B RID: 1051
		Binding GetParentBinding();
	}
}
