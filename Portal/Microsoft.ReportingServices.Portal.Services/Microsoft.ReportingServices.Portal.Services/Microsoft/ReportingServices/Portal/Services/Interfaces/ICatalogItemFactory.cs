using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Interfaces
{
	// Token: 0x0200003A RID: 58
	internal interface ICatalogItemFactory
	{
		// Token: 0x06000255 RID: 597
		ICatalogItem Create(IPrincipal userPrincipal, CatalogItemDescriptor itemDescriptor);
	}
}
