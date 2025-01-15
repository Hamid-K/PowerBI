using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000027 RID: 39
	internal class AuthzData
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000472D File Offset: 0x0000292D
		static AuthzData()
		{
			AuthzData.InitializeMaps();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004734 File Offset: 0x00002934
		internal static void InitializeMaps()
		{
			AuthzData.m_CatOper2PermMask = new Hashtable();
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.CreateRoles, 1U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.DeleteRoles, 2U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.ReadRoleProperties, 4U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.UpdateRoleProperties, 8U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.ReadSystemProperties, 16U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.UpdateSystemProperties, 32U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.GenerateEvents, 64U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.ReadSystemSecurityPolicy, 131072U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.UpdateSystemSecurityPolicy, 262144U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.CreateSchedules, 128U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.DeleteSchedules, 256U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.ReadSchedules, 512U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.UpdateSchedules, 1024U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.ListJobs, 2048U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.CancelJobs, 4096U);
			AuthzData.m_CatOper2PermMask.Add(CatalogOperation.ExecuteReportDefinition, 8192U);
			AuthzData.m_CatPerm2OperMask = new Hashtable();
			foreach (object obj in AuthzData.m_CatOper2PermMask.Keys)
			{
				CatalogOperation catalogOperation = (CatalogOperation)obj;
				uint num = (uint)AuthzData.m_CatOper2PermMask[catalogOperation];
				AuthzData.m_CatPerm2OperMask.Add(num, catalogOperation);
			}
			AuthzData.m_FldOper2PermMask = new Hashtable();
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.CreateFolder, 1U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.Delete, 2U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.ReadProperties, 4U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.UpdateProperties, 8U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.CreateReport, 16U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.CreateResource, 32U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.ReadAuthorizationPolicy, 131072U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.UpdateDeleteAuthorizationPolicy, 262144U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.CreateDatasource, 64U);
			AuthzData.m_FldOper2PermMask.Add(FolderOperation.CreateModel, 128U);
			AuthzData.m_FldPerm2OperMask = new Hashtable();
			foreach (object obj2 in AuthzData.m_FldOper2PermMask.Keys)
			{
				FolderOperation folderOperation = (FolderOperation)obj2;
				uint num2 = (uint)AuthzData.m_FldOper2PermMask[folderOperation];
				AuthzData.m_FldPerm2OperMask.Add(num2, folderOperation);
			}
			AuthzData.m_RptOper2PermMask = new Hashtable();
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.Delete, 1U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ExecuteAndView, 2U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadProperties, 4U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateProperties, 8U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateParameters, 16U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadDatasource, 32U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateDatasource, 64U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadReportDefinition, 128U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateReportDefinition, 256U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.CreateSubscription, 512U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.DeleteSubscription, 1024U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadSubscription, 2048U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadAuthorizationPolicy, 131072U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateDeleteAuthorizationPolicy, 262144U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateSubscription, 8192U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.CreateAnySubscription, 16384U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.DeleteAnySubscription, 32768U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadAnySubscription, 65536U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdateAnySubscription, 524288U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.UpdatePolicy, 2097152U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ReadPolicy, 1048576U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.DeleteHistory, 4096U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ListHistory, 1U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.CreateResource, 2U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.CreateSnapshot, 4U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.Execute, 8U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.CreateLink, 16U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.Comment, 32U);
			AuthzData.m_RptOper2PermMask.Add(ReportOperation.ManageComments, 64U);
			AuthzData.m_RptPerm2OperMaskPrimary = new Hashtable();
			foreach (object obj3 in AuthzData.m_RptOper2PermMask.Keys)
			{
				ReportOperation reportOperation = (ReportOperation)obj3;
				uint num3 = (uint)AuthzData.m_RptOper2PermMask[reportOperation];
				if ((ulong)reportOperation < (ulong)((long)AuthzConstants.MaxAceIndex))
				{
					AuthzData.m_RptPerm2OperMaskPrimary.Add(num3, reportOperation);
				}
			}
			AuthzData.m_RptPerm2OperMaskSecondary = new Hashtable();
			foreach (object obj4 in AuthzData.m_RptOper2PermMask.Keys)
			{
				ReportOperation reportOperation2 = (ReportOperation)obj4;
				uint num4 = (uint)AuthzData.m_RptOper2PermMask[reportOperation2];
				if ((ulong)reportOperation2 >= (ulong)((long)AuthzConstants.MaxAceIndex))
				{
					AuthzData.m_RptPerm2OperMaskSecondary.Add(num4, reportOperation2);
				}
			}
			AuthzData.m_ResOper2PermMask = new Hashtable();
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.Delete, 1U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.ReadProperties, 2U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.UpdateProperties, 4U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.ReadContent, 8U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.UpdateContent, 16U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.ReadAuthorizationPolicy, 131072U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.UpdateDeleteAuthorizationPolicy, 262144U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.Comment, 32U);
			AuthzData.m_ResOper2PermMask.Add(ResourceOperation.ManageComments, 64U);
			AuthzData.m_ResPerm2OperMask = new Hashtable();
			foreach (object obj5 in AuthzData.m_ResOper2PermMask.Keys)
			{
				ResourceOperation resourceOperation = (ResourceOperation)obj5;
				uint num5 = (uint)AuthzData.m_ResOper2PermMask[resourceOperation];
				AuthzData.m_ResPerm2OperMask.Add(num5, resourceOperation);
			}
			AuthzData.m_DSOper2PermMask = new Hashtable();
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.Delete, 1U);
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.ReadProperties, 2U);
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.UpdateProperties, 4U);
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.ReadContent, 8U);
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.UpdateContent, 16U);
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.ReadAuthorizationPolicy, 131072U);
			AuthzData.m_DSOper2PermMask.Add(DatasourceOperation.UpdateDeleteAuthorizationPolicy, 262144U);
			AuthzData.m_DSPerm2OperMask = new Hashtable();
			foreach (object obj6 in AuthzData.m_DSOper2PermMask.Keys)
			{
				DatasourceOperation datasourceOperation = (DatasourceOperation)obj6;
				uint num6 = (uint)AuthzData.m_DSOper2PermMask[datasourceOperation];
				AuthzData.m_DSPerm2OperMask.Add(num6, datasourceOperation);
			}
			AuthzData.m_ModelOper2PermMask = new Hashtable();
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.Delete, 1U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.ReadProperties, 2U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.UpdateProperties, 4U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.ReadDatasource, 32U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.UpdateDatasource, 64U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.ReadContent, 8U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.UpdateContent, 16U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.ReadAuthorizationPolicy, 131072U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.UpdateDeleteAuthorizationPolicy, 262144U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.ReadModelItemAuthorizationPolicies, 128U);
			AuthzData.m_ModelOper2PermMask.Add(ModelOperation.UpdateModelItemAuthorizationPolicies, 256U);
			AuthzData.m_ModelPerm2OperMask = new Hashtable();
			foreach (object obj7 in AuthzData.m_ModelOper2PermMask.Keys)
			{
				ModelOperation modelOperation = (ModelOperation)obj7;
				uint num7 = (uint)AuthzData.m_ModelOper2PermMask[modelOperation];
				AuthzData.m_ModelPerm2OperMask.Add(num7, modelOperation);
			}
			AuthzData.m_ModelItemOper2PermMask = new Hashtable();
			AuthzData.m_ModelItemOper2PermMask.Add(ModelItemOperation.ReadProperties, 1U);
			AuthzData.m_ModelItemPerm2OperMask = new Hashtable();
			foreach (object obj8 in AuthzData.m_ModelItemOper2PermMask.Keys)
			{
				ModelItemOperation modelItemOperation = (ModelItemOperation)obj8;
				uint num8 = (uint)AuthzData.m_ModelItemOper2PermMask[modelItemOperation];
				AuthzData.m_ModelItemPerm2OperMask.Add(num8, modelItemOperation);
			}
			AuthzData.m_CatOperNames = new Hashtable();
			AuthzData.m_CatOperNames.Add(CatalogOperation.CreateRoles, "Create Roles");
			AuthzData.m_CatOperNames.Add(CatalogOperation.DeleteRoles, "Delete Roles");
			AuthzData.m_CatOperNames.Add(CatalogOperation.ReadRoleProperties, "Read Role Properties");
			AuthzData.m_CatOperNames.Add(CatalogOperation.UpdateRoleProperties, "Update Role Properties");
			AuthzData.m_CatOperNames.Add(CatalogOperation.ReadSystemProperties, "Read System Properties");
			AuthzData.m_CatOperNames.Add(CatalogOperation.UpdateSystemProperties, "Update System Properties");
			AuthzData.m_CatOperNames.Add(CatalogOperation.GenerateEvents, "Generate Events");
			AuthzData.m_CatOperNames.Add(CatalogOperation.ReadSystemSecurityPolicy, "Read System Security Policies");
			AuthzData.m_CatOperNames.Add(CatalogOperation.UpdateSystemSecurityPolicy, "Update System Security Policies");
			AuthzData.m_CatOperNames.Add(CatalogOperation.CreateSchedules, "Create Schedules");
			AuthzData.m_CatOperNames.Add(CatalogOperation.DeleteSchedules, "Delete Schedules");
			AuthzData.m_CatOperNames.Add(CatalogOperation.ReadSchedules, "Read Schedules");
			AuthzData.m_CatOperNames.Add(CatalogOperation.UpdateSchedules, "Update Schedules");
			AuthzData.m_CatOperNames.Add(CatalogOperation.ListJobs, "List Jobs");
			AuthzData.m_CatOperNames.Add(CatalogOperation.CancelJobs, "Cancel Jobs");
			AuthzData.m_CatOperNames.Add(CatalogOperation.ExecuteReportDefinition, "Execute Report Definition");
			if (AuthzData.m_CatOperNames.Count != 16)
			{
				throw new InternalCatalogException("Number of catalog names don't match.");
			}
			if (AuthzData.m_CatOper2PermMask.Count != 16)
			{
				throw new InternalCatalogException("Number of catalog operations don't match.");
			}
			AuthzData.m_FldOperNames = new Hashtable();
			AuthzData.m_FldOperNames.Add(FolderOperation.CreateFolder, "Create Folder");
			AuthzData.m_FldOperNames.Add(FolderOperation.Delete, "Delete");
			AuthzData.m_FldOperNames.Add(FolderOperation.ReadProperties, "Read Properties");
			AuthzData.m_FldOperNames.Add(FolderOperation.UpdateProperties, "Update Properties");
			AuthzData.m_FldOperNames.Add(FolderOperation.CreateReport, "Create Report");
			AuthzData.m_FldOperNames.Add(FolderOperation.CreateResource, "Create Resource");
			AuthzData.m_FldOperNames.Add(FolderOperation.ReadAuthorizationPolicy, "Read Security Policies");
			AuthzData.m_FldOperNames.Add(FolderOperation.UpdateDeleteAuthorizationPolicy, "Update Security Policies");
			AuthzData.m_FldOperNames.Add(FolderOperation.CreateDatasource, "Create data source");
			AuthzData.m_FldOperNames.Add(FolderOperation.CreateModel, "Create Model");
			if (AuthzData.m_FldOperNames.Count != 10)
			{
				throw new InternalCatalogException("Number of folder names don't match.");
			}
			if (AuthzData.m_FldOper2PermMask.Count != 10)
			{
				throw new InternalCatalogException("Number of folder operations don't match.");
			}
			AuthzData.m_RptOperNames = new Hashtable();
			AuthzData.m_RptOperNames.Add(ReportOperation.Delete, "Delete");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadProperties, "Read Properties");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateProperties, "Update Properties");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateParameters, "Update Parameters");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadDatasource, "Read Data Sources");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateDatasource, "Update Data Sources");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadReportDefinition, "Read Report Definition");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateReportDefinition, "Update Report Definition");
			AuthzData.m_RptOperNames.Add(ReportOperation.CreateSubscription, "Create Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.DeleteSubscription, "Delete Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadSubscription, "Read Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateSubscription, "Update Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.CreateAnySubscription, "Create Any Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.DeleteAnySubscription, "Delete Any Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadAnySubscription, "Read Any Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateAnySubscription, "Update Any Subscription");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdatePolicy, "Update Policy");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadPolicy, "Read Policy");
			AuthzData.m_RptOperNames.Add(ReportOperation.DeleteHistory, "Delete Report History");
			AuthzData.m_RptOperNames.Add(ReportOperation.ListHistory, "List Report History");
			AuthzData.m_RptOperNames.Add(ReportOperation.ExecuteAndView, "Execute and View");
			AuthzData.m_RptOperNames.Add(ReportOperation.CreateResource, "Create Resource");
			AuthzData.m_RptOperNames.Add(ReportOperation.CreateSnapshot, "Create Report History");
			AuthzData.m_RptOperNames.Add(ReportOperation.ReadAuthorizationPolicy, "Read Security Policies");
			AuthzData.m_RptOperNames.Add(ReportOperation.UpdateDeleteAuthorizationPolicy, "Update Security Policies");
			AuthzData.m_RptOperNames.Add(ReportOperation.Execute, "Execute");
			AuthzData.m_RptOperNames.Add(ReportOperation.CreateLink, "Create Link");
			AuthzData.m_RptOperNames.Add(ReportOperation.Comment, "Comment on Reports");
			AuthzData.m_RptOperNames.Add(ReportOperation.ManageComments, "Manage Comments");
			if (AuthzData.m_RptOperNames.Count != 29)
			{
				throw new InternalCatalogException("Number of report names don't match.");
			}
			if (AuthzData.m_RptOper2PermMask.Count != 29)
			{
				throw new InternalCatalogException("Number of report operations don't match.");
			}
			AuthzData.m_ResOperNames = new Hashtable();
			AuthzData.m_ResOperNames.Add(ResourceOperation.Delete, "Delete");
			AuthzData.m_ResOperNames.Add(ResourceOperation.ReadProperties, "Read Properties");
			AuthzData.m_ResOperNames.Add(ResourceOperation.UpdateProperties, "Update Properties");
			AuthzData.m_ResOperNames.Add(ResourceOperation.ReadContent, "Read Content");
			AuthzData.m_ResOperNames.Add(ResourceOperation.UpdateContent, "Update Content");
			AuthzData.m_ResOperNames.Add(ResourceOperation.ReadAuthorizationPolicy, "Read Security Policies");
			AuthzData.m_ResOperNames.Add(ResourceOperation.UpdateDeleteAuthorizationPolicy, "Update Security Policies");
			AuthzData.m_ResOperNames.Add(ResourceOperation.Comment, "Comment on Reports");
			AuthzData.m_ResOperNames.Add(ResourceOperation.ManageComments, "Manage Comments");
			if (AuthzData.m_ResOperNames.Count != 9)
			{
				throw new InternalCatalogException("Number of resource names don't match.");
			}
			if (AuthzData.m_ResOper2PermMask.Count != 9)
			{
				throw new InternalCatalogException("Number of resource operations don't match.");
			}
			AuthzData.m_DSOperNames = new Hashtable();
			AuthzData.m_DSOperNames.Add(DatasourceOperation.Delete, "Delete");
			AuthzData.m_DSOperNames.Add(DatasourceOperation.ReadProperties, "Read Properties");
			AuthzData.m_DSOperNames.Add(DatasourceOperation.UpdateProperties, "Update Properties");
			AuthzData.m_DSOperNames.Add(DatasourceOperation.ReadContent, "Read Content");
			AuthzData.m_DSOperNames.Add(DatasourceOperation.UpdateContent, "Update Content");
			AuthzData.m_DSOperNames.Add(DatasourceOperation.ReadAuthorizationPolicy, "Read Security Policies");
			AuthzData.m_DSOperNames.Add(DatasourceOperation.UpdateDeleteAuthorizationPolicy, "Update Security Policies");
			if (AuthzData.m_DSOperNames.Count != 7)
			{
				throw new InternalCatalogException("Number of datasource names don't match.");
			}
			if (AuthzData.m_DSOper2PermMask.Count != 7)
			{
				throw new InternalCatalogException("Number of resource operations don't match.");
			}
			AuthzData.m_ModelOperNames = new Hashtable();
			AuthzData.m_ModelOperNames.Add(ModelOperation.Delete, "Delete");
			AuthzData.m_ModelOperNames.Add(ModelOperation.ReadProperties, "Read Properties");
			AuthzData.m_ModelOperNames.Add(ModelOperation.UpdateProperties, "Update Properties");
			AuthzData.m_ModelOperNames.Add(ModelOperation.ReadDatasource, "Read Data Sources");
			AuthzData.m_ModelOperNames.Add(ModelOperation.UpdateDatasource, "Update Data Sources");
			AuthzData.m_ModelOperNames.Add(ModelOperation.ReadContent, "Read Content");
			AuthzData.m_ModelOperNames.Add(ModelOperation.UpdateContent, "Update Content");
			AuthzData.m_ModelOperNames.Add(ModelOperation.ReadAuthorizationPolicy, "Read Security Policies");
			AuthzData.m_ModelOperNames.Add(ModelOperation.UpdateDeleteAuthorizationPolicy, "Update Security Policies");
			AuthzData.m_ModelOperNames.Add(ModelOperation.ReadModelItemAuthorizationPolicies, "Read Model Item Security Policies");
			AuthzData.m_ModelOperNames.Add(ModelOperation.UpdateModelItemAuthorizationPolicies, "Update Model Item Security Policies");
			if (AuthzData.m_ModelOperNames.Count != 11)
			{
				throw new InternalCatalogException("Number of model names don't match.");
			}
			if (AuthzData.m_ModelOper2PermMask.Count != 11)
			{
				throw new InternalCatalogException("Number of model operations don't match.");
			}
			AuthzData.m_ModelItemOperNames = new Hashtable();
			AuthzData.m_ModelItemOperNames.Add(ModelItemOperation.ReadProperties, "Read Properties");
			if (AuthzData.m_ModelItemOperNames.Count != 1)
			{
				throw new InternalCatalogException("Number of model item names don't match.");
			}
			if (AuthzData.m_ModelItemOper2PermMask.Count != 1)
			{
				throw new InternalCatalogException("Number of model item operations don't match");
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005C08 File Offset: 0x00003E08
		private static string GetOperationName(uint permissionBitMask, SecDescType objType)
		{
			switch (objType)
			{
			case SecDescType.Catalog:
			{
				CatalogOperation catalogOperation = (CatalogOperation)AuthzData.m_CatPerm2OperMask[permissionBitMask];
				return (string)AuthzData.m_CatOperNames[catalogOperation];
			}
			case SecDescType.Folder:
			{
				FolderOperation folderOperation = (FolderOperation)AuthzData.m_FldPerm2OperMask[permissionBitMask];
				return (string)AuthzData.m_FldOperNames[folderOperation];
			}
			case SecDescType.ReportPrimary:
			{
				ReportOperation reportOperation = (ReportOperation)AuthzData.m_RptPerm2OperMaskPrimary[permissionBitMask];
				return (string)AuthzData.m_RptOperNames[reportOperation];
			}
			case SecDescType.ReportSecondary:
			{
				object obj = AuthzData.m_RptPerm2OperMaskSecondary[permissionBitMask];
				if (obj != null)
				{
					ReportOperation reportOperation2 = (ReportOperation)obj;
					return (string)AuthzData.m_RptOperNames[reportOperation2];
				}
				return null;
			}
			case SecDescType.Resource:
			{
				ResourceOperation resourceOperation = (ResourceOperation)AuthzData.m_ResPerm2OperMask[permissionBitMask];
				return (string)AuthzData.m_ResOperNames[resourceOperation];
			}
			case SecDescType.Datasource:
			{
				DatasourceOperation datasourceOperation = (DatasourceOperation)AuthzData.m_DSPerm2OperMask[permissionBitMask];
				return (string)AuthzData.m_DSOperNames[datasourceOperation];
			}
			case SecDescType.Model:
			{
				ModelOperation modelOperation = (ModelOperation)AuthzData.m_ModelPerm2OperMask[permissionBitMask];
				return (string)AuthzData.m_ModelOperNames[modelOperation];
			}
			case SecDescType.ModelItem:
			{
				object obj2 = AuthzData.m_ModelItemPerm2OperMask[permissionBitMask];
				if (obj2 != null)
				{
					ModelItemOperation modelItemOperation = (ModelItemOperation)obj2;
					return (string)AuthzData.m_ModelItemOperNames[modelItemOperation];
				}
				return null;
			}
			default:
				throw new InternalCatalogException("Unknown security descriptor type.");
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005DC8 File Offset: 0x00003FC8
		internal static void Mask2OperationNames(uint rightsMask, SecDescType objType, StringCollection operationNames)
		{
			uint num = 1U;
			for (int i = 0; i < AuthzConstants.MaxAceIndex; i++)
			{
				if ((rightsMask & num) != 0U)
				{
					string operationName = AuthzData.GetOperationName(num, objType);
					if (operationName != null)
					{
						operationNames.Add(operationName);
					}
				}
				num <<= 1;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005E04 File Offset: 0x00004004
		private static SecDescType GetObjectType(SecurityItemType itemType, ReportSecDescType repSecDescType)
		{
			switch (itemType)
			{
			case SecurityItemType.Catalog:
				return SecDescType.Catalog;
			case SecurityItemType.Folder:
				return SecDescType.Folder;
			case SecurityItemType.Report:
				if (repSecDescType == ReportSecDescType.Primary)
				{
					return SecDescType.ReportPrimary;
				}
				if (repSecDescType == ReportSecDescType.Secondary)
				{
					return SecDescType.ReportSecondary;
				}
				throw new InternalCatalogException("Unexpected item type.");
			case SecurityItemType.Resource:
				return SecDescType.Resource;
			case SecurityItemType.Datasource:
				return SecDescType.Datasource;
			case SecurityItemType.Model:
				return SecDescType.Model;
			case SecurityItemType.ModelItem:
				return SecDescType.ModelItem;
			default:
				throw new InternalCatalogException("Wrong security item type.");
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005E64 File Offset: 0x00004064
		internal static StringCollection SecDesc2Operations(IntPtr userToken, SecurityItemType itemType, ref Hashtable secDescHash)
		{
			uint num = 0U;
			uint num2 = 0U;
			byte[] array;
			byte[] array2;
			SdAndType.GetRightSecDesc(itemType, secDescHash, out array, out array2);
			Native.GetNewEffectiveRights(array, userToken, out num);
			if (array2 != null)
			{
				Native.GetNewEffectiveRights(array2, userToken, out num2);
			}
			GC.KeepAlive(array);
			GC.KeepAlive(array2);
			return AuthzData.OperMask2OperationNames(Native.IsAdmin(userToken), itemType, num, num2);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005EB0 File Offset: 0x000040B0
		internal static StringCollection SecDesc2Operations(string userName, SecurityItemType itemType, ref Hashtable secDescHash)
		{
			uint num = 0U;
			uint num2 = 0U;
			byte[] array;
			byte[] array2;
			SdAndType.GetRightSecDesc(itemType, secDescHash, out array, out array2);
			Native.GetNewEffectiveRights(array, userName, out num);
			if (array2 != null)
			{
				Native.GetNewEffectiveRights(array2, userName, out num2);
			}
			GC.KeepAlive(array);
			GC.KeepAlive(array2);
			return AuthzData.OperMask2OperationNames(Native.IsAdmin(userName), itemType, num, num2);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005EFC File Offset: 0x000040FC
		private static StringCollection OperMask2OperationNames(bool isAdmin, SecurityItemType itemType, uint primMask, uint secMask)
		{
			StringCollection stringCollection = new StringCollection();
			if (primMask != 0U)
			{
				AuthzData.RightsMask2Collection(primMask, itemType, ReportSecDescType.Primary, isAdmin, stringCollection);
			}
			if (secMask != 0U)
			{
				AuthzData.RightsMask2Collection(secMask, itemType, ReportSecDescType.Secondary, isAdmin, stringCollection);
			}
			return stringCollection;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005F2C File Offset: 0x0000412C
		private static void RightsMask2Collection(uint rightsMask, SecurityItemType type, ReportSecDescType repSecDescType, bool isAdmin, StringCollection operCollection)
		{
			SecDescType objectType = AuthzData.GetObjectType(type, repSecDescType);
			AuthzData.Mask2OperationNames(rightsMask, objectType, operCollection);
			if (isAdmin)
			{
				AuthzData.AddAdminOperations(type, repSecDescType, operCollection);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005F58 File Offset: 0x00004158
		private static void AddAdminOperations(SecurityItemType itemType, ReportSecDescType secDescType, StringCollection operNames)
		{
			if (itemType == SecurityItemType.Catalog)
			{
				if (!operNames.Contains("Read Role Properties"))
				{
					operNames.Add("Read Role Properties");
				}
				if (!operNames.Contains("Create Roles"))
				{
					operNames.Add("Create Roles");
				}
				if (!operNames.Contains("Update Role Properties"))
				{
					operNames.Add("Update Role Properties");
					return;
				}
			}
			else if (itemType == SecurityItemType.Report)
			{
				if (secDescType == ReportSecDescType.Primary && !operNames.Contains("Read Properties"))
				{
					operNames.Add("Read Properties");
					return;
				}
			}
			else if (!operNames.Contains("Read Properties"))
			{
				operNames.Add("Read Properties");
			}
		}

		// Token: 0x0400010B RID: 267
		internal const uint SecurityDescriptorVersion = 5U;

		// Token: 0x0400010C RID: 268
		internal const uint UpgradeableDescriptorVersion = 2U;

		// Token: 0x0400010D RID: 269
		internal const int NrRptOperations = 29;

		// Token: 0x0400010E RID: 270
		internal const int NrFldOperations = 10;

		// Token: 0x0400010F RID: 271
		internal const int NrResOperations = 9;

		// Token: 0x04000110 RID: 272
		internal const int NrDSOperations = 7;

		// Token: 0x04000111 RID: 273
		internal const int NrCatOperations = 16;

		// Token: 0x04000112 RID: 274
		internal const int NrModelOperations = 11;

		// Token: 0x04000113 RID: 275
		internal const int NrModelItemOperations = 1;

		// Token: 0x04000114 RID: 276
		internal static Hashtable m_CatOperNames;

		// Token: 0x04000115 RID: 277
		internal static Hashtable m_FldOperNames;

		// Token: 0x04000116 RID: 278
		internal static Hashtable m_RptOperNames;

		// Token: 0x04000117 RID: 279
		internal static Hashtable m_ResOperNames;

		// Token: 0x04000118 RID: 280
		internal static Hashtable m_DSOperNames;

		// Token: 0x04000119 RID: 281
		internal static Hashtable m_ModelOperNames;

		// Token: 0x0400011A RID: 282
		internal static Hashtable m_ModelItemOperNames;

		// Token: 0x0400011B RID: 283
		internal static Hashtable m_CatOper2PermMask;

		// Token: 0x0400011C RID: 284
		internal static Hashtable m_CatPerm2OperMask;

		// Token: 0x0400011D RID: 285
		internal static Hashtable m_FldOper2PermMask;

		// Token: 0x0400011E RID: 286
		internal static Hashtable m_FldPerm2OperMask;

		// Token: 0x0400011F RID: 287
		internal static Hashtable m_RptOper2PermMask;

		// Token: 0x04000120 RID: 288
		internal static Hashtable m_RptPerm2OperMaskPrimary;

		// Token: 0x04000121 RID: 289
		internal static Hashtable m_RptPerm2OperMaskSecondary;

		// Token: 0x04000122 RID: 290
		internal static Hashtable m_ResOper2PermMask;

		// Token: 0x04000123 RID: 291
		internal static Hashtable m_ResPerm2OperMask;

		// Token: 0x04000124 RID: 292
		internal static Hashtable m_DSOper2PermMask;

		// Token: 0x04000125 RID: 293
		internal static Hashtable m_DSPerm2OperMask;

		// Token: 0x04000126 RID: 294
		internal static Hashtable m_ModelOper2PermMask;

		// Token: 0x04000127 RID: 295
		internal static Hashtable m_ModelPerm2OperMask;

		// Token: 0x04000128 RID: 296
		internal static Hashtable m_ModelItemOper2PermMask;

		// Token: 0x04000129 RID: 297
		internal static Hashtable m_ModelItemPerm2OperMask;
	}
}
