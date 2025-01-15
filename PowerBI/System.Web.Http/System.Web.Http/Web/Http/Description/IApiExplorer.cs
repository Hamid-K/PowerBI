using System;
using System.Collections.ObjectModel;

namespace System.Web.Http.Description
{
	// Token: 0x020000EB RID: 235
	public interface IApiExplorer
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000623 RID: 1571
		Collection<ApiDescription> ApiDescriptions { get; }
	}
}
