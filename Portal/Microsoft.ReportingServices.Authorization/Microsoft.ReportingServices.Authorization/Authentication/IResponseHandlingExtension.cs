using System;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x02000008 RID: 8
	public interface IResponseHandlingExtension
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17
		// (set) Token: 0x06000012 RID: 18
		IResponseHandler ResponseHandler { get; set; }
	}
}
