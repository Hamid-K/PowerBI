using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Models;
using Microsoft.ReportingServices.Portal.Interfaces.Models.Impl;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x0200008C RID: 140
	public interface ICatalogService
	{
		// Token: 0x0600044F RID: 1103
		IEnumerable<ICatalogItem> GetFolderContents(IPrincipal userPrincipal, string path);

		// Token: 0x06000450 RID: 1104
		ICatalogItem GetCatalogItem(IPrincipal userPrincipal, string path);

		// Token: 0x06000451 RID: 1105
		ItemPolicy GetItemPolicy(IPrincipal userPrincipal, string item);

		// Token: 0x06000452 RID: 1106
		void SetItemPolicy(IPrincipal userPrincipal, string item, ItemPolicy itemPolicy);

		// Token: 0x06000453 RID: 1107
		IEnumerable<Role> GetCatalogRoles(IPrincipal userPrincipal);

		// Token: 0x06000454 RID: 1108
		IEnumerable<Subscription> GetItemSubscriptions(IPrincipal userPrincipal, string path);

		// Token: 0x06000455 RID: 1109
		void DeleteSubscription(IPrincipal userPrincipal, string id);

		// Token: 0x06000456 RID: 1110
		void EnableSubscription(IPrincipal userPrincipal, string id);

		// Token: 0x06000457 RID: 1111
		void DisableSubscription(IPrincipal userPrincipal, string id);

		// Token: 0x06000458 RID: 1112
		void CreateFolder(IPrincipal userPrincipal, string path, string name);

		// Token: 0x06000459 RID: 1113
		void DeleteItem(IPrincipal userPrincipal, string path);

		// Token: 0x0600045A RID: 1114
		void UpdateFolder(IPrincipal userPrincipal, string path, string oldName, IFolderCatalogItem newItem);

		// Token: 0x0600045B RID: 1115
		void MoveItem(IPrincipal userPrincipal, string sourcePath, string targetPath);

		// Token: 0x0600045C RID: 1116
		IDictionary<string, string> GetItemProperties(IPrincipal userPrincipal, string path);

		// Token: 0x0600045D RID: 1117
		IResourceCatalogItem GetResource(IPrincipal userPrincipal, string path);
	}
}
