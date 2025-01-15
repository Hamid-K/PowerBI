using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Editions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000067 RID: 103
	internal static class SkuAndPermissionCollection
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		static SkuAndPermissionCollection()
		{
			RevokedPermissionsForSku revokedPermissionsForSku = new RevokedPermissionsForSku();
			revokedPermissionsForSku.Sku = SkuType.SsrsExpress;
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Create Roles");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Delete Roles");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Create Schedules");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Read Schedules");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Delete Schedules");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Update Schedules");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Execute Report Definition");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Update Role Properties");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Manage Shared Schedules");
			revokedPermissionsForSku.RevokedSystemPermissions.Add("Generate Events");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Create Any Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Create Report History");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Create Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Delete Any Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Delete Report History");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Delete Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("List Report History");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Read Policy");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Update Policy");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Read Any Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Create Model");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Read Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Update Any Subscription");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Read Model Item Security Policies");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Update Model Item Security Policies");
			revokedPermissionsForSku.RevokedGeneralPermissions.Add("Update Subscription");
			revokedPermissionsForSku.RevokedModelPermissions.Add("Read Content");
			revokedPermissionsForSku.RevokedModelPermissions.Add("Read Properties");
			revokedPermissionsForSku.RevokedModelPermissions.Add("Update Content");
			revokedPermissionsForSku.RevokedModelPermissions.Add("Update Properties");
			revokedPermissionsForSku.RevokedModelPermissions.Add("Read Policy");
			revokedPermissionsForSku.RevokedModelPermissions.Add("Update Policy");
			SkuAndPermissionCollection.m_skuAndPermissions.Add(revokedPermissionsForSku);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		internal static StringCollection GetSkuAdjustedPermissions(StringCollection initialPermissions, SecurityItemType itemType, SkuType serverSku)
		{
			foreach (RevokedPermissionsForSku revokedPermissionsForSku in SkuAndPermissionCollection.m_skuAndPermissions)
			{
				if (revokedPermissionsForSku.Sku == serverSku)
				{
					return SkuAndPermissionCollection.RemoveRevokedPerms(itemType, revokedPermissionsForSku, initialPermissions);
				}
			}
			return initialPermissions;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000DB38 File Offset: 0x0000BD38
		private static StringCollection RemoveRevokedPerms(SecurityItemType itemType, RevokedPermissionsForSku skuAndPerm, StringCollection initialPermissions)
		{
			switch (itemType)
			{
			case SecurityItemType.Catalog:
				return SkuAndPermissionCollection.RemoveMatches(initialPermissions, skuAndPerm.RevokedSystemPermissions);
			case SecurityItemType.Folder:
			case SecurityItemType.Report:
			case SecurityItemType.Resource:
			case SecurityItemType.Datasource:
				return SkuAndPermissionCollection.RemoveMatches(initialPermissions, skuAndPerm.RevokedGeneralPermissions);
			case SecurityItemType.Model:
				return SkuAndPermissionCollection.RemoveMatches(initialPermissions, skuAndPerm.RevokedModelPermissions);
			default:
				return initialPermissions;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000DB90 File Offset: 0x0000BD90
		private static StringCollection RemoveMatches(StringCollection initialCollection, List<string> permissionsToRemove)
		{
			StringCollection stringCollection = new StringCollection();
			foreach (string text in initialCollection)
			{
				if (!permissionsToRemove.Contains(text))
				{
					stringCollection.Add(text);
				}
			}
			return stringCollection;
		}

		// Token: 0x04000334 RID: 820
		private static List<RevokedPermissionsForSku> m_skuAndPermissions = new List<RevokedPermissionsForSku>();
	}
}
