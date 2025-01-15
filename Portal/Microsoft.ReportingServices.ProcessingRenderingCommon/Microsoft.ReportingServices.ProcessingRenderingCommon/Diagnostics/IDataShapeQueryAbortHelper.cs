using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000050 RID: 80
	internal interface IDataShapeQueryAbortHelper : IDataShapeAbortHelper, IAbortHelper, IDisposable
	{
		// Token: 0x06000249 RID: 585
		IDataShapeAbortHelper CreateDataShapeAbortHelper();
	}
}
