using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000227 RID: 551
	internal sealed class AuthzData
	{
		// Token: 0x060013CC RID: 5068 RVA: 0x0004A2AF File Offset: 0x000484AF
		internal static FolderOperation CommonOperationToFolderOperation(CommonOperation operaion)
		{
			switch (operaion)
			{
			case CommonOperation.Delete:
				return FolderOperation.Delete;
			case CommonOperation.ReadProperties:
				return FolderOperation.ReadProperties;
			case CommonOperation.UpdateProperties:
				return FolderOperation.UpdateProperties;
			case CommonOperation.ReadAuthorizationPolicy:
				return FolderOperation.ReadAuthorizationPolicy;
			case CommonOperation.UpdateDeleteAuthorizationPolicy:
				return FolderOperation.UpdateDeleteAuthorizationPolicy;
			default:
				return (FolderOperation)(-1);
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004A2D8 File Offset: 0x000484D8
		internal static ReportOperation CommonOperationToReportOperation(CommonOperation operation)
		{
			switch (operation)
			{
			case CommonOperation.Delete:
				return ReportOperation.Delete;
			case CommonOperation.ReadProperties:
				return ReportOperation.ReadProperties;
			case CommonOperation.UpdateProperties:
				return ReportOperation.UpdateProperties;
			case CommonOperation.ReadAuthorizationPolicy:
				return ReportOperation.ReadAuthorizationPolicy;
			case CommonOperation.UpdateDeleteAuthorizationPolicy:
				return ReportOperation.UpdateDeleteAuthorizationPolicy;
			case CommonOperation.ReadDatasource:
				return ReportOperation.ReadDatasource;
			case CommonOperation.Comment:
				return ReportOperation.Comment;
			case CommonOperation.ManageComments:
				return ReportOperation.ManageComments;
			default:
				return (ReportOperation)(-1);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004A317 File Offset: 0x00048517
		internal static ResourceOperation CommonOperationToResourceOperation(CommonOperation operation)
		{
			switch (operation)
			{
			case CommonOperation.Delete:
				return ResourceOperation.Delete;
			case CommonOperation.ReadProperties:
				return ResourceOperation.ReadProperties;
			case CommonOperation.UpdateProperties:
				return ResourceOperation.UpdateProperties;
			case CommonOperation.ReadAuthorizationPolicy:
				return ResourceOperation.ReadAuthorizationPolicy;
			case CommonOperation.UpdateDeleteAuthorizationPolicy:
				return ResourceOperation.UpdateDeleteAuthorizationPolicy;
			case CommonOperation.Comment:
				return ResourceOperation.Comment;
			case CommonOperation.ManageComments:
				return ResourceOperation.ManageComments;
			}
			return (ResourceOperation)(-1);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004A350 File Offset: 0x00048550
		internal static DatasourceOperation CommonOperationToDatasourceOperation(CommonOperation operation)
		{
			switch (operation)
			{
			case CommonOperation.Delete:
				return DatasourceOperation.Delete;
			case CommonOperation.ReadProperties:
				return DatasourceOperation.ReadProperties;
			case CommonOperation.UpdateProperties:
				return DatasourceOperation.UpdateProperties;
			case CommonOperation.ReadAuthorizationPolicy:
				return DatasourceOperation.ReadAuthorizationPolicy;
			case CommonOperation.UpdateDeleteAuthorizationPolicy:
				return DatasourceOperation.UpdateDeleteAuthorizationPolicy;
			default:
				return (DatasourceOperation)(-1);
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004A379 File Offset: 0x00048579
		internal static ModelOperation CommonOperationToModelOperation(CommonOperation operation)
		{
			switch (operation)
			{
			case CommonOperation.Delete:
				return ModelOperation.Delete;
			case CommonOperation.ReadProperties:
				return ModelOperation.ReadProperties;
			case CommonOperation.UpdateProperties:
				return ModelOperation.UpdateProperties;
			case CommonOperation.ReadAuthorizationPolicy:
				return ModelOperation.ReadAuthorizationPolicy;
			case CommonOperation.UpdateDeleteAuthorizationPolicy:
				return ModelOperation.UpdateDeleteAuthorizationPolicy;
			case CommonOperation.ReadDatasource:
				return ModelOperation.ReadDatasource;
			default:
				return (ModelOperation)(-1);
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004A3A8 File Offset: 0x000485A8
		static AuthzData()
		{
			AuthzData.InitializeMaps();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004A3B0 File Offset: 0x000485B0
		public static SecurityItemType GetSecType(ItemType itemType)
		{
			switch (itemType)
			{
			case ItemType.Folder:
				return SecurityItemType.Folder;
			case ItemType.Report:
			case ItemType.LinkedReport:
			case ItemType.DataSet:
			case ItemType.RdlxReport:
			case ItemType.MobileReport:
			case ItemType.PowerBIReport:
			case ItemType.ExcelWorkbook:
				return SecurityItemType.Report;
			case ItemType.Resource:
				return SecurityItemType.Resource;
			case ItemType.DataSource:
				return SecurityItemType.Datasource;
			case ItemType.Model:
				return SecurityItemType.Model;
			case ItemType.Component:
			case ItemType.Kpi:
				return SecurityItemType.Resource;
			}
			throw new InternalCatalogException("Wrong item type.");
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004A418 File Offset: 0x00048618
		internal static AuthzData.SecurityTask FindTaskByID(string id)
		{
			AuthzData.SecurityTask[] array = AuthzData.m_CatalogItemTasks;
			AuthzData.SecurityTask securityTask = AuthzData.TaskList.FindTaskByID(id, array);
			if (securityTask != null)
			{
				return securityTask;
			}
			array = AuthzData.m_CatalogTasks;
			securityTask = AuthzData.TaskList.FindTaskByID(id, array);
			if (securityTask != null)
			{
				return securityTask;
			}
			array = AuthzData.m_ModelItemTasks;
			securityTask = AuthzData.TaskList.FindTaskByID(id, array);
			if (securityTask != null)
			{
				return securityTask;
			}
			throw new TaskNotFoundException(id);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004A468 File Offset: 0x00048668
		internal static Microsoft.ReportingServices.Library.Soap.Task[] TaskMaskToTaskList(string strTaskMask, AuthzData.SecurityScope scope)
		{
			AuthzData.TaskList taskList;
			switch (scope)
			{
			case AuthzData.SecurityScope.CatalogItem:
			{
				AuthzData.SecurityTask[] array = AuthzData.m_CatalogItemTasks;
				taskList = AuthzData.TaskList.TaskMaskToTaskList(strTaskMask, array);
				break;
			}
			case AuthzData.SecurityScope.Catalog:
			{
				AuthzData.SecurityTask[] array = AuthzData.m_CatalogTasks;
				taskList = AuthzData.TaskList.TaskMaskToTaskList(strTaskMask, array);
				break;
			}
			case AuthzData.SecurityScope.ModelItem:
			{
				AuthzData.SecurityTask[] array = AuthzData.m_ModelItemTasks;
				taskList = AuthzData.TaskList.TaskMaskToTaskList(strTaskMask, array);
				break;
			}
			default:
				throw new InternalCatalogException("Unsupported SecurityScope.");
			}
			if (taskList.Count == 0)
			{
				throw new InternalCatalogException("No task were found in the list.");
			}
			return Microsoft.ReportingServices.Library.Soap.Task.TaskListToThisArray(taskList);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004A4E0 File Offset: 0x000486E0
		internal static void InitializeMaps()
		{
			AuthzData.m_CatalogItemTasks = new AuthzData.CatalogItemTask[18];
			AuthzData.m_CatalogItemTasks[0] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ConfigureAccess, "E96A7928-F3A5-409d-A6AA-0808172B28CB", "ConfigureAccessName", "ConfigureAccessDescription");
			AuthzData.m_CatalogItemTasks[1] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.CreateLinkedReports, "3246FDF7-16FB-4802-ABFA-76D4F4A8AD62", "CreateLinkedReportsName", "CreateLinkedReportsDescription");
			AuthzData.m_CatalogItemTasks[2] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ViewReports, "E2723F22-E29C-496b-B981-1D775F45FC09", "ViewReportsName", "ViewReportsDescription");
			AuthzData.m_CatalogItemTasks[3] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageReports, "88552A24-99BA-46ab-90CD-EF66FD3E444D", "ManageReportsName", "ManageReportsDescription");
			AuthzData.m_CatalogItemTasks[4] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ViewResources, "993C580B-3FBF-444b-B85E-A8DA50ADF40F", "ViewResourcesName", "ViewResourcesDescription");
			AuthzData.m_CatalogItemTasks[5] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageResources, "1D574E69-B01D-4278-A25A-DEE6B3790F81", "ManageResourcesName", "ManageResourcesDescription");
			AuthzData.m_CatalogItemTasks[6] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ViewFolders, "2FBF7AE5-0DB6-46f2-B9BB-480794FBC97E", "ViewFoldersName", "ViewFoldersDescription");
			AuthzData.m_CatalogItemTasks[7] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageFolders, "683665AB-EE4B-4026-99AE-68A9899F8B6E", "ManageFoldersName", "ManageFoldersDescription");
			AuthzData.m_CatalogItemTasks[8] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageSnapshots, "75D856B8-2FC1-41c9-8DFA-FE0FF6153C20", "ManageReportHistoryName", "ManageReportHistoryDescription");
			AuthzData.m_CatalogItemTasks[9] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.Subscribe, "F95F31D0-834A-4c0e-B290-E3E4477908CA", "SubscribeName", "SubscribeDescription");
			AuthzData.m_CatalogItemTasks[10] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageAnySubscription, "7813C99B-3F84-4d02-930F-72FA3B86024A", "ManageAnySubscriptionName", "ManageAnySubscriptionDescription");
			AuthzData.m_CatalogItemTasks[11] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ViewDataSources, "B96D28BB-23D0-4a32-B57A-7C12EBDD0704", "ViewDatasourceName", "ViewDatasourceDescription");
			AuthzData.m_CatalogItemTasks[12] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageDataSources, "AD4523AD-09B6-46ab-A7F0-AD1F52449FE1", "ManageDatasourceName", "ManageDatasourceDescription");
			AuthzData.m_CatalogItemTasks[13] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ViewModels, "A1BDCA29-F891-418f-BEAA-43E937F800C4", "ViewModelsName", "ViewModelsDescription");
			AuthzData.m_CatalogItemTasks[14] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageModels, "F59546B7-49A1-41de-8E9A-34137F3D677F", "ManageModelsName", "ManageModelsDescription");
			AuthzData.m_CatalogItemTasks[15] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ConsumeReports, "4BF862B5-10D0-4793-B075-802F7775561E", "ConsumeReportsName", "ConsumeReportsDescription");
			AuthzData.m_CatalogItemTasks[16] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.Comment, "938488DB-E742-4362-9837-499B168D8C79", "CommentName", "CommentDescription");
			AuthzData.m_CatalogItemTasks[17] = new AuthzData.CatalogItemTask(AuthzData.CatalogItemTaskEnum.ManageComments, "42AD2650-A3A9-478F-B6DF-51456A08461E", "ManageCommentsName", "ManageCommentsDescription");
			AuthzData.m_CatalogTasks = new AuthzData.CatalogTask[9];
			AuthzData.m_CatalogTasks[0] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ManageRoles, "A7E17983-FFC5-4252-9EBC-DF5C495A4CFA", "ManageRolesName", "ManageRolesDescription");
			AuthzData.m_CatalogTasks[1] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ManageSystemSecurity, "075BE56B-589C-47ed-B072-5B5EDCF80C66", "ManageSystemSecurityName", "ManageSystemSecurityDescription");
			AuthzData.m_CatalogTasks[2] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ViewSystemProperties, "D07A969D-0B11-4a4a-8685-7E0E6DEEBA36", "ViewSystemPropertiesName", "ViewSystemPropertiesDescription");
			AuthzData.m_CatalogTasks[3] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ManageSystemProperties, "DED0947B-E39C-4a03-A512-10AF2933772B", "ManageSystemPropertiesName", "ManageSystemPropertiesDescription");
			AuthzData.m_CatalogTasks[4] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ViewSharedSchedules, "20278B55-A582-4926-8EB6-5C0D27CED8F9", "ViewSharedSchedulesName", "ViewSharedSchedulesDescription");
			AuthzData.m_CatalogTasks[5] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ManageSharedSchedules, "7663B035-0C99-488d-942B-7B36345178EB", "ManageSharedSchedulesName", "ManageSharedSchedulesDescription");
			AuthzData.m_CatalogTasks[6] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.GenerateEvents, "79D94B43-57B0-45fd-8EB3-A8654C347413", "GenerateEventsName", "GenerateEventsDescription");
			AuthzData.m_CatalogTasks[7] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ManageJobs, "EC6EE51B-0D96-478b-8477-826AA7637B68", "ManageJobsName", "ManageJobsDescription");
			AuthzData.m_CatalogTasks[8] = new AuthzData.CatalogTask(AuthzData.CatalogTaskEnum.ExecuteReportDefinitions, "B8326178-0D1A-4054-B591-1CE98F492FCF", "ExecuteReportDefinitionsName", "ExecuteReportDefinitionsDescription");
			AuthzData.m_ModelItemTasks = new AuthzData.ModelItemTask[1];
			AuthzData.m_ModelItemTasks[0] = new AuthzData.ModelItemTask(AuthzData.ModelItemTaskEnum.ViewModelItems, "69383E55-A810-41f4-8A89-69A0D5976F49", "ViewModelItemsName", "ViewModelItemsDescription");
			AuthzData.m_CatalogItemTaskMap = new Hashtable();
			FolderOperation[] array = new FolderOperation[]
			{
				FolderOperation.ReadAuthorizationPolicy,
				FolderOperation.UpdateDeleteAuthorizationPolicy
			};
			ReportOperation[] array2 = new ReportOperation[]
			{
				ReportOperation.ReadAuthorizationPolicy,
				ReportOperation.UpdateDeleteAuthorizationPolicy
			};
			ResourceOperation[] array3 = new ResourceOperation[]
			{
				ResourceOperation.ReadAuthorizationPolicy,
				ResourceOperation.UpdateDeleteAuthorizationPolicy
			};
			DatasourceOperation[] array4 = new DatasourceOperation[]
			{
				DatasourceOperation.ReadAuthorizationPolicy,
				DatasourceOperation.UpdateDeleteAuthorizationPolicy
			};
			ModelOperation[] array5 = new ModelOperation[]
			{
				ModelOperation.ReadAuthorizationPolicy,
				ModelOperation.UpdateDeleteAuthorizationPolicy
			};
			AuthzData.TaskOperationMap taskOperationMap = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ConfigureAccess);
			taskOperationMap.m_FldOper = array;
			taskOperationMap.m_RptOper = array2;
			taskOperationMap.m_ResOper = array3;
			taskOperationMap.m_DSOper = array4;
			taskOperationMap.m_ModelOper = array5;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ConfigureAccess, taskOperationMap);
			FolderOperation[] array6 = null;
			ReportOperation[] array7 = new ReportOperation[]
			{
				ReportOperation.ReadProperties,
				ReportOperation.CreateLink
			};
			ResourceOperation[] array8 = null;
			AuthzData.TaskOperationMap taskOperationMap2 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.CreateLinkedReports);
			taskOperationMap2.m_FldOper = array6;
			taskOperationMap2.m_RptOper = array7;
			taskOperationMap2.m_ResOper = array8;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.CreateLinkedReports, taskOperationMap2);
			FolderOperation[] array9 = null;
			ReportOperation[] array10 = new ReportOperation[]
			{
				ReportOperation.ExecuteAndView,
				ReportOperation.ReadProperties,
				ReportOperation.ListHistory
			};
			ResourceOperation[] array11 = null;
			AuthzData.TaskOperationMap taskOperationMap3 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ViewReports);
			taskOperationMap3.m_FldOper = array9;
			taskOperationMap3.m_RptOper = array10;
			taskOperationMap3.m_ResOper = array11;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ViewReports, taskOperationMap3);
			FolderOperation[] array12 = new FolderOperation[] { FolderOperation.CreateReport };
			ReportOperation[] array13 = new ReportOperation[]
			{
				ReportOperation.Delete,
				ReportOperation.ReadProperties,
				ReportOperation.UpdateProperties,
				ReportOperation.UpdateParameters,
				ReportOperation.ReadDatasource,
				ReportOperation.UpdateDatasource,
				ReportOperation.ReadReportDefinition,
				ReportOperation.UpdateReportDefinition,
				ReportOperation.UpdatePolicy,
				ReportOperation.ReadPolicy,
				ReportOperation.Execute
			};
			ResourceOperation[] array14 = null;
			AuthzData.TaskOperationMap taskOperationMap4 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageReports);
			taskOperationMap4.m_FldOper = array12;
			taskOperationMap4.m_RptOper = array13;
			taskOperationMap4.m_ResOper = array14;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageReports, taskOperationMap4);
			FolderOperation[] array15 = null;
			ReportOperation[] array16 = null;
			ResourceOperation[] array17 = new ResourceOperation[]
			{
				ResourceOperation.ReadProperties,
				ResourceOperation.ReadContent
			};
			AuthzData.TaskOperationMap taskOperationMap5 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ViewResources);
			taskOperationMap5.m_FldOper = array15;
			taskOperationMap5.m_RptOper = array16;
			taskOperationMap5.m_ResOper = array17;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ViewResources, taskOperationMap5);
			FolderOperation[] array18 = new FolderOperation[] { FolderOperation.CreateResource };
			ReportOperation[] array19 = null;
			ResourceOperation[] array20 = new ResourceOperation[]
			{
				ResourceOperation.Delete,
				ResourceOperation.ReadProperties,
				ResourceOperation.UpdateProperties,
				ResourceOperation.UpdateContent
			};
			AuthzData.TaskOperationMap taskOperationMap6 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageResources);
			taskOperationMap6.m_FldOper = array18;
			taskOperationMap6.m_RptOper = array19;
			taskOperationMap6.m_ResOper = array20;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageResources, taskOperationMap6);
			FolderOperation[] array21 = new FolderOperation[] { FolderOperation.ReadProperties };
			ReportOperation[] array22 = null;
			ResourceOperation[] array23 = null;
			AuthzData.TaskOperationMap taskOperationMap7 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ViewFolders);
			taskOperationMap7.m_FldOper = array21;
			taskOperationMap7.m_RptOper = array22;
			taskOperationMap7.m_ResOper = array23;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ViewFolders, taskOperationMap7);
			FolderOperation[] array24 = new FolderOperation[]
			{
				FolderOperation.CreateFolder,
				FolderOperation.Delete,
				FolderOperation.ReadProperties,
				FolderOperation.UpdateProperties
			};
			ReportOperation[] array25 = null;
			ResourceOperation[] array26 = null;
			AuthzData.TaskOperationMap taskOperationMap8 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageFolders);
			taskOperationMap8.m_FldOper = array24;
			taskOperationMap8.m_RptOper = array25;
			taskOperationMap8.m_ResOper = array26;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageFolders, taskOperationMap8);
			FolderOperation[] array27 = null;
			ReportOperation[] array28 = new ReportOperation[]
			{
				ReportOperation.ReadProperties,
				ReportOperation.UpdatePolicy,
				ReportOperation.ReadPolicy,
				ReportOperation.CreateSnapshot,
				ReportOperation.DeleteHistory,
				ReportOperation.ListHistory,
				ReportOperation.Execute
			};
			ResourceOperation[] array29 = null;
			AuthzData.TaskOperationMap taskOperationMap9 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageSnapshots);
			taskOperationMap9.m_FldOper = array27;
			taskOperationMap9.m_RptOper = array28;
			taskOperationMap9.m_ResOper = array29;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageSnapshots, taskOperationMap9);
			FolderOperation[] array30 = null;
			ReportOperation[] array31 = new ReportOperation[]
			{
				ReportOperation.ReadProperties,
				ReportOperation.CreateSubscription,
				ReportOperation.DeleteSubscription,
				ReportOperation.ReadSubscription,
				ReportOperation.UpdateSubscription
			};
			ResourceOperation[] array32 = null;
			AuthzData.TaskOperationMap taskOperationMap10 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.Subscribe);
			taskOperationMap10.m_FldOper = array30;
			taskOperationMap10.m_RptOper = array31;
			taskOperationMap10.m_ResOper = array32;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.Subscribe, taskOperationMap10);
			FolderOperation[] array33 = null;
			ReportOperation[] array34 = new ReportOperation[]
			{
				ReportOperation.ReadProperties,
				ReportOperation.CreateAnySubscription,
				ReportOperation.DeleteAnySubscription,
				ReportOperation.ReadAnySubscription,
				ReportOperation.UpdateAnySubscription
			};
			ResourceOperation[] array35 = null;
			AuthzData.TaskOperationMap taskOperationMap11 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageAnySubscription);
			taskOperationMap11.m_FldOper = array33;
			taskOperationMap11.m_RptOper = array34;
			taskOperationMap11.m_ResOper = array35;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageAnySubscription, taskOperationMap11);
			FolderOperation[] array36 = null;
			ReportOperation[] array37 = null;
			ResourceOperation[] array38 = null;
			DatasourceOperation[] array39 = new DatasourceOperation[]
			{
				DatasourceOperation.ReadProperties,
				DatasourceOperation.ReadContent
			};
			AuthzData.TaskOperationMap taskOperationMap12 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ViewDataSources);
			taskOperationMap12.m_FldOper = array36;
			taskOperationMap12.m_RptOper = array37;
			taskOperationMap12.m_ResOper = array38;
			taskOperationMap12.m_DSOper = array39;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ViewDataSources, taskOperationMap12);
			FolderOperation[] array40 = new FolderOperation[] { FolderOperation.CreateDatasource };
			ReportOperation[] array41 = null;
			ResourceOperation[] array42 = null;
			DatasourceOperation[] array43 = new DatasourceOperation[]
			{
				DatasourceOperation.Delete,
				DatasourceOperation.ReadProperties,
				DatasourceOperation.UpdateProperties,
				DatasourceOperation.ReadContent,
				DatasourceOperation.UpdateContent
			};
			AuthzData.TaskOperationMap taskOperationMap13 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageDataSources);
			taskOperationMap13.m_FldOper = array40;
			taskOperationMap13.m_RptOper = array41;
			taskOperationMap13.m_ResOper = array42;
			taskOperationMap13.m_DSOper = array43;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageDataSources, taskOperationMap13);
			FolderOperation[] array44 = null;
			ReportOperation[] array45 = null;
			ResourceOperation[] array46 = null;
			DatasourceOperation[] array47 = null;
			ModelOperation[] array48 = new ModelOperation[] { ModelOperation.ReadProperties };
			AuthzData.TaskOperationMap taskOperationMap14 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ViewModels);
			taskOperationMap14.m_FldOper = array44;
			taskOperationMap14.m_RptOper = array45;
			taskOperationMap14.m_ResOper = array46;
			taskOperationMap14.m_DSOper = array47;
			taskOperationMap14.m_ModelOper = array48;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ViewModels, taskOperationMap14);
			FolderOperation[] array49 = new FolderOperation[] { FolderOperation.CreateModel };
			ReportOperation[] array50 = null;
			ResourceOperation[] array51 = null;
			DatasourceOperation[] array52 = null;
			ModelOperation[] array53 = new ModelOperation[]
			{
				ModelOperation.Delete,
				ModelOperation.ReadProperties,
				ModelOperation.ReadContent,
				ModelOperation.ReadDatasource,
				ModelOperation.ReadModelItemAuthorizationPolicies,
				ModelOperation.UpdateDatasource,
				ModelOperation.UpdateContent,
				ModelOperation.UpdateModelItemAuthorizationPolicies,
				ModelOperation.UpdateProperties
			};
			AuthzData.TaskOperationMap taskOperationMap15 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageModels);
			taskOperationMap15.m_FldOper = array49;
			taskOperationMap15.m_RptOper = array50;
			taskOperationMap15.m_ResOper = array51;
			taskOperationMap15.m_DSOper = array52;
			taskOperationMap15.m_ModelOper = array53;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageModels, taskOperationMap15);
			ReportOperation[] array54 = new ReportOperation[]
			{
				ReportOperation.ReadProperties,
				ReportOperation.ReadReportDefinition,
				ReportOperation.ReadDatasource
			};
			AuthzData.TaskOperationMap taskOperationMap16 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ConsumeReports);
			taskOperationMap16.m_RptOper = array54;
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ConsumeReports, taskOperationMap16);
			ReportOperation[] array55 = new ReportOperation[] { ReportOperation.Comment };
			ResourceOperation[] array56 = new ResourceOperation[] { ResourceOperation.Comment };
			AuthzData.TaskOperationMap taskOperationMap17 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.Comment)
			{
				m_RptOper = array55,
				m_ResOper = array56
			};
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.Comment, taskOperationMap17);
			ReportOperation[] array57 = new ReportOperation[] { ReportOperation.ManageComments };
			ResourceOperation[] array58 = new ResourceOperation[] { ResourceOperation.ManageComments };
			AuthzData.TaskOperationMap taskOperationMap18 = new AuthzData.TaskOperationMap(AuthzData.CatalogItemTaskEnum.ManageComments)
			{
				m_RptOper = array57,
				m_ResOper = array58
			};
			AuthzData.m_CatalogItemTaskMap.Add(AuthzData.CatalogItemTaskEnum.ManageComments, taskOperationMap18);
			AuthzData.m_CatalogTaskMap = new Hashtable();
			CatalogOperation[] array59 = new CatalogOperation[]
			{
				CatalogOperation.CreateRoles,
				CatalogOperation.DeleteRoles,
				CatalogOperation.ReadRoleProperties,
				CatalogOperation.UpdateRoleProperties
			};
			AuthzData.TaskOperationMap taskOperationMap19 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ManageRoles);
			taskOperationMap19.m_CatOper = array59;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ManageRoles, taskOperationMap19);
			CatalogOperation[] array60 = new CatalogOperation[] { CatalogOperation.ReadSystemProperties };
			AuthzData.TaskOperationMap taskOperationMap20 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ViewSystemProperties);
			taskOperationMap20.m_CatOper = array60;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ViewSystemProperties, taskOperationMap20);
			CatalogOperation[] array61 = new CatalogOperation[]
			{
				CatalogOperation.ReadSystemProperties,
				CatalogOperation.UpdateSystemProperties
			};
			AuthzData.TaskOperationMap taskOperationMap21 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ManageSystemProperties);
			taskOperationMap21.m_CatOper = array61;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ManageSystemProperties, taskOperationMap21);
			CatalogOperation[] array62 = new CatalogOperation[] { CatalogOperation.ReadSchedules };
			AuthzData.TaskOperationMap taskOperationMap22 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ViewSharedSchedules);
			taskOperationMap22.m_CatOper = array62;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ViewSharedSchedules, taskOperationMap22);
			CatalogOperation[] array63 = new CatalogOperation[]
			{
				CatalogOperation.CreateSchedules,
				CatalogOperation.DeleteSchedules,
				CatalogOperation.ReadSchedules,
				CatalogOperation.UpdateSchedules
			};
			AuthzData.TaskOperationMap taskOperationMap23 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ManageSharedSchedules);
			taskOperationMap23.m_CatOper = array63;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ManageSharedSchedules, taskOperationMap23);
			CatalogOperation[] array64 = new CatalogOperation[]
			{
				CatalogOperation.ReadSystemSecurityPolicy,
				CatalogOperation.UpdateSystemSecurityPolicy
			};
			AuthzData.TaskOperationMap taskOperationMap24 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ManageSystemSecurity);
			taskOperationMap24.m_CatOper = array64;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ManageSystemSecurity, taskOperationMap24);
			CatalogOperation[] array65 = new CatalogOperation[] { CatalogOperation.GenerateEvents };
			AuthzData.TaskOperationMap taskOperationMap25 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.GenerateEvents);
			taskOperationMap25.m_CatOper = array65;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.GenerateEvents, taskOperationMap25);
			CatalogOperation[] array66 = new CatalogOperation[]
			{
				CatalogOperation.ListJobs,
				CatalogOperation.CancelJobs
			};
			AuthzData.TaskOperationMap taskOperationMap26 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ManageJobs);
			taskOperationMap26.m_CatOper = array66;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ManageJobs, taskOperationMap26);
			CatalogOperation[] array67 = new CatalogOperation[] { CatalogOperation.ExecuteReportDefinition };
			AuthzData.TaskOperationMap taskOperationMap27 = new AuthzData.TaskOperationMap(AuthzData.CatalogTaskEnum.ExecuteReportDefinitions);
			taskOperationMap27.m_CatOper = array67;
			AuthzData.m_CatalogTaskMap.Add(AuthzData.CatalogTaskEnum.ExecuteReportDefinitions, taskOperationMap27);
			AuthzData.m_ModelItemTaskMap = new Hashtable();
			ModelItemOperation[] array68 = new ModelItemOperation[1];
			AuthzData.TaskOperationMap taskOperationMap28 = new AuthzData.TaskOperationMap(AuthzData.ModelItemTaskEnum.ViewModelItems);
			taskOperationMap28.m_ModelItemOper = array68;
			AuthzData.m_ModelItemTaskMap.Add(AuthzData.ModelItemTaskEnum.ViewModelItems, taskOperationMap28);
		}

		// Token: 0x04000706 RID: 1798
		internal const int CatalogItemTaskCount = 18;

		// Token: 0x04000707 RID: 1799
		internal const int CatalogTaskCount = 9;

		// Token: 0x04000708 RID: 1800
		internal const int ModelItemTaskCount = 1;

		// Token: 0x04000709 RID: 1801
		internal static AuthzData.CatalogItemTask[] m_CatalogItemTasks;

		// Token: 0x0400070A RID: 1802
		internal static AuthzData.CatalogTask[] m_CatalogTasks;

		// Token: 0x0400070B RID: 1803
		internal static AuthzData.ModelItemTask[] m_ModelItemTasks;

		// Token: 0x0400070C RID: 1804
		internal static Hashtable m_CatalogItemTaskMap;

		// Token: 0x0400070D RID: 1805
		internal static Hashtable m_CatalogTaskMap;

		// Token: 0x0400070E RID: 1806
		internal static Hashtable m_ModelItemTaskMap;

		// Token: 0x0200049C RID: 1180
		internal sealed class PrincipalAndRoles
		{
			// Token: 0x060023DB RID: 9179 RVA: 0x0008537F File Offset: 0x0008357F
			internal PrincipalAndRoles(string principal)
			{
				this.m_principal = principal;
				this.m_Roles = null;
			}

			// Token: 0x060023DC RID: 9180 RVA: 0x00085398 File Offset: 0x00083598
			internal bool ContainsRole(string roleName)
			{
				if (this.m_Roles == null)
				{
					return false;
				}
				for (int i = 0; i < this.m_Roles.Count; i++)
				{
					if (Localization.CatalogCultureCompare((string)this.m_Roles[i], roleName) == 0)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001042 RID: 4162
			internal string m_principal;

			// Token: 0x04001043 RID: 4163
			internal ArrayList m_Roles;
		}

		// Token: 0x0200049D RID: 1181
		internal enum CatalogItemTaskEnum
		{
			// Token: 0x04001045 RID: 4165
			Invalid = 268435455,
			// Token: 0x04001046 RID: 4166
			ConfigureAccess = 0,
			// Token: 0x04001047 RID: 4167
			CreateLinkedReports,
			// Token: 0x04001048 RID: 4168
			ViewReports,
			// Token: 0x04001049 RID: 4169
			ManageReports,
			// Token: 0x0400104A RID: 4170
			ViewResources,
			// Token: 0x0400104B RID: 4171
			ManageResources,
			// Token: 0x0400104C RID: 4172
			ViewFolders,
			// Token: 0x0400104D RID: 4173
			ManageFolders,
			// Token: 0x0400104E RID: 4174
			ManageSnapshots,
			// Token: 0x0400104F RID: 4175
			Subscribe,
			// Token: 0x04001050 RID: 4176
			ManageAnySubscription,
			// Token: 0x04001051 RID: 4177
			ViewDataSources,
			// Token: 0x04001052 RID: 4178
			ManageDataSources,
			// Token: 0x04001053 RID: 4179
			ViewModels,
			// Token: 0x04001054 RID: 4180
			ManageModels,
			// Token: 0x04001055 RID: 4181
			ConsumeReports,
			// Token: 0x04001056 RID: 4182
			Comment,
			// Token: 0x04001057 RID: 4183
			ManageComments
		}

		// Token: 0x0200049E RID: 1182
		internal enum CatalogTaskEnum
		{
			// Token: 0x04001059 RID: 4185
			Invalid = 268435455,
			// Token: 0x0400105A RID: 4186
			ManageRoles = 0,
			// Token: 0x0400105B RID: 4187
			ManageSystemSecurity,
			// Token: 0x0400105C RID: 4188
			ViewSystemProperties,
			// Token: 0x0400105D RID: 4189
			ManageSystemProperties,
			// Token: 0x0400105E RID: 4190
			ViewSharedSchedules,
			// Token: 0x0400105F RID: 4191
			ManageSharedSchedules,
			// Token: 0x04001060 RID: 4192
			GenerateEvents,
			// Token: 0x04001061 RID: 4193
			ManageJobs,
			// Token: 0x04001062 RID: 4194
			ExecuteReportDefinitions
		}

		// Token: 0x0200049F RID: 1183
		internal enum ModelItemTaskEnum
		{
			// Token: 0x04001064 RID: 4196
			Invalid = 268435455,
			// Token: 0x04001065 RID: 4197
			ViewModelItems = 0
		}

		// Token: 0x020004A0 RID: 1184
		internal enum SecurityScope
		{
			// Token: 0x04001067 RID: 4199
			CatalogItem,
			// Token: 0x04001068 RID: 4200
			Catalog,
			// Token: 0x04001069 RID: 4201
			ModelItem
		}

		// Token: 0x020004A1 RID: 1185
		internal abstract class SecurityTask
		{
			// Token: 0x060023DD RID: 9181 RVA: 0x000853E1 File Offset: 0x000835E1
			internal SecurityTask(AuthzData.SecurityScope scope, string id, string name, string description, int bitmapLength, int myBitInMap)
			{
				this.m_scope = scope;
				this.m_id = id;
				this.m_name = name;
				this.m_description = description;
				this.m_bitmapLength = bitmapLength;
				this.m_myBitInMap = myBitInMap;
			}

			// Token: 0x060023DE RID: 9182 RVA: 0x00085418 File Offset: 0x00083618
			internal char[] GetEmptyMask()
			{
				char[] array = new char[this.m_bitmapLength];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = '0';
				}
				return array;
			}

			// Token: 0x060023DF RID: 9183 RVA: 0x00085445 File Offset: 0x00083645
			internal void IncludeMyselfInMask(char[] mask)
			{
				mask[this.m_myBitInMap] = '1';
			}

			// Token: 0x17000A7F RID: 2687
			// (get) Token: 0x060023E0 RID: 9184 RVA: 0x00085451 File Offset: 0x00083651
			internal AuthzData.SecurityScope Scope
			{
				get
				{
					return this.m_scope;
				}
			}

			// Token: 0x17000A80 RID: 2688
			// (get) Token: 0x060023E1 RID: 9185 RVA: 0x00085459 File Offset: 0x00083659
			internal string ID
			{
				get
				{
					return this.m_id;
				}
			}

			// Token: 0x17000A81 RID: 2689
			// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00085461 File Offset: 0x00083661
			internal string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17000A82 RID: 2690
			// (get) Token: 0x060023E3 RID: 9187 RVA: 0x00085469 File Offset: 0x00083669
			internal string Description
			{
				get
				{
					return this.m_description;
				}
			}

			// Token: 0x0400106A RID: 4202
			protected readonly AuthzData.SecurityScope m_scope;

			// Token: 0x0400106B RID: 4203
			private readonly string m_id;

			// Token: 0x0400106C RID: 4204
			private readonly string m_name;

			// Token: 0x0400106D RID: 4205
			private readonly string m_description;

			// Token: 0x0400106E RID: 4206
			private readonly int m_bitmapLength;

			// Token: 0x0400106F RID: 4207
			protected readonly int m_myBitInMap;
		}

		// Token: 0x020004A2 RID: 1186
		internal class CatalogItemTask : AuthzData.SecurityTask
		{
			// Token: 0x060023E4 RID: 9188 RVA: 0x00085471 File Offset: 0x00083671
			internal CatalogItemTask(AuthzData.CatalogItemTaskEnum task, string id, string name, string description)
				: base(AuthzData.SecurityScope.CatalogItem, id, name, description, 18, (int)task)
			{
			}

			// Token: 0x17000A83 RID: 2691
			// (get) Token: 0x060023E5 RID: 9189 RVA: 0x00085481 File Offset: 0x00083681
			internal AuthzData.CatalogItemTaskEnum TaskType
			{
				get
				{
					return (AuthzData.CatalogItemTaskEnum)this.m_myBitInMap;
				}
			}
		}

		// Token: 0x020004A3 RID: 1187
		internal class CatalogTask : AuthzData.SecurityTask
		{
			// Token: 0x060023E6 RID: 9190 RVA: 0x00085489 File Offset: 0x00083689
			internal CatalogTask(AuthzData.CatalogTaskEnum task, string id, string name, string description)
				: base(AuthzData.SecurityScope.Catalog, id, name, description, 9, (int)task)
			{
			}
		}

		// Token: 0x020004A4 RID: 1188
		internal class ModelItemTask : AuthzData.SecurityTask
		{
			// Token: 0x060023E7 RID: 9191 RVA: 0x00085499 File Offset: 0x00083699
			internal ModelItemTask(AuthzData.ModelItemTaskEnum task, string id, string name, string description)
				: base(AuthzData.SecurityScope.ModelItem, id, name, description, 1, (int)task)
			{
			}
		}

		// Token: 0x020004A5 RID: 1189
		internal class TaskList : List<AuthzData.SecurityTask>
		{
			// Token: 0x060023E8 RID: 9192 RVA: 0x000854A8 File Offset: 0x000836A8
			internal static string TaskListToTaskMask(string[] taskIDs, out AuthzData.SecurityScope scope)
			{
				if (taskIDs.Length < 1)
				{
					throw new EmptyRoleException();
				}
				if (taskIDs[0] == null)
				{
					throw new MissingElementException("TaskID");
				}
				AuthzData.SecurityTask securityTask = AuthzData.FindTaskByID(taskIDs[0]);
				char[] emptyMask = securityTask.GetEmptyMask();
				securityTask.IncludeMyselfInMask(emptyMask);
				for (int i = 1; i < taskIDs.Length; i++)
				{
					if (taskIDs[i] == null)
					{
						throw new MissingElementException("TaskID");
					}
					AuthzData.SecurityTask securityTask2 = AuthzData.FindTaskByID(taskIDs[i]);
					if (securityTask2.Scope != securityTask.Scope)
					{
						throw new MixedTasksException();
					}
					securityTask2.IncludeMyselfInMask(emptyMask);
				}
				scope = securityTask.Scope;
				return new string(emptyMask);
			}

			// Token: 0x060023E9 RID: 9193 RVA: 0x00085538 File Offset: 0x00083738
			internal static AuthzData.TaskList TaskMaskToTaskList(string taskMask, AuthzData.SecurityTask[] allTasks)
			{
				if (allTasks.Length != taskMask.Length)
				{
					throw new InternalCatalogException("Task mask length and task count mismatch!");
				}
				AuthzData.TaskList taskList = new AuthzData.TaskList();
				for (int i = 0; i < taskMask.Length; i++)
				{
					if (taskMask[i] == '1')
					{
						taskList.Add(allTasks[i]);
					}
				}
				return taskList;
			}

			// Token: 0x060023EA RID: 9194 RVA: 0x00085588 File Offset: 0x00083788
			internal static AuthzData.SecurityTask FindTaskByID(string id, AuthzData.SecurityTask[] allTasks)
			{
				for (int i = 0; i < allTasks.Length; i++)
				{
					if (Localization.CatalogCultureCompare(id, allTasks[i].ID) == 0)
					{
						return allTasks[i];
					}
				}
				return null;
			}

			// Token: 0x060023EB RID: 9195 RVA: 0x000855B8 File Offset: 0x000837B8
			internal static Microsoft.ReportingServices.Library.Soap.Task[] GetTaskList(AuthzData.SecurityTask[] allTasks)
			{
				Microsoft.ReportingServices.Library.Soap.Task[] array = new Microsoft.ReportingServices.Library.Soap.Task[allTasks.Length];
				for (int i = 0; i < allTasks.Length; i++)
				{
					string id = allTasks[i].ID;
					string @string = SecLib.Keys.GetString(allTasks[i].Name);
					string string2 = SecLib.Keys.GetString(allTasks[i].Description);
					RSTrace.SecurityTracer.Assert(@string != null);
					RSTrace.SecurityTracer.Assert(string2 != null);
					if (@string != null && string2 != null)
					{
						array[i] = new Microsoft.ReportingServices.Library.Soap.Task
						{
							TaskID = id,
							Name = @string,
							Description = string2
						};
					}
				}
				return array;
			}
		}

		// Token: 0x020004A6 RID: 1190
		internal sealed class TaskOperationMap
		{
			// Token: 0x060023ED RID: 9197 RVA: 0x00085654 File Offset: 0x00083854
			internal TaskOperationMap(AuthzData.CatalogItemTaskEnum task)
			{
				this.m_catalogItemTask = task;
				this.m_catalogTask = AuthzData.CatalogTaskEnum.Invalid;
				this.m_modelItemTask = AuthzData.ModelItemTaskEnum.Invalid;
			}

			// Token: 0x060023EE RID: 9198 RVA: 0x00085679 File Offset: 0x00083879
			internal TaskOperationMap(AuthzData.CatalogTaskEnum sysTask)
			{
				this.m_catalogItemTask = AuthzData.CatalogItemTaskEnum.Invalid;
				this.m_catalogTask = sysTask;
				this.m_modelItemTask = AuthzData.ModelItemTaskEnum.Invalid;
			}

			// Token: 0x060023EF RID: 9199 RVA: 0x0008569E File Offset: 0x0008389E
			internal TaskOperationMap(AuthzData.ModelItemTaskEnum modelTask)
			{
				this.m_catalogItemTask = AuthzData.CatalogItemTaskEnum.Invalid;
				this.m_catalogTask = AuthzData.CatalogTaskEnum.Invalid;
				this.m_modelItemTask = modelTask;
			}

			// Token: 0x04001070 RID: 4208
			internal AuthzData.CatalogItemTaskEnum m_catalogItemTask;

			// Token: 0x04001071 RID: 4209
			internal AuthzData.CatalogTaskEnum m_catalogTask;

			// Token: 0x04001072 RID: 4210
			internal AuthzData.ModelItemTaskEnum m_modelItemTask;

			// Token: 0x04001073 RID: 4211
			internal ReportOperation[] m_RptOper;

			// Token: 0x04001074 RID: 4212
			internal FolderOperation[] m_FldOper;

			// Token: 0x04001075 RID: 4213
			internal ResourceOperation[] m_ResOper;

			// Token: 0x04001076 RID: 4214
			internal DatasourceOperation[] m_DSOper;

			// Token: 0x04001077 RID: 4215
			internal ModelOperation[] m_ModelOper;

			// Token: 0x04001078 RID: 4216
			internal CatalogOperation[] m_CatOper;

			// Token: 0x04001079 RID: 4217
			internal ModelItemOperation[] m_ModelItemOper;
		}

		// Token: 0x020004A7 RID: 1191
		internal sealed class SecurityPolicy
		{
			// Token: 0x060023F0 RID: 9200 RVA: 0x000856C4 File Offset: 0x000838C4
			internal SecurityPolicy(Security securityMgr, IDataRecord polRecord)
			{
				this.m_policyId = polRecord.GetGuid(0);
				if (!polRecord.IsDBNull(1))
				{
					this.m_xmlPolicy = polRecord.GetString(1);
					securityMgr.UpdateUserNames(ref this.m_xmlPolicy);
				}
				else
				{
					this.m_xmlPolicy = Security.ProduceEmptyPolicy();
				}
				int @byte = (int)polRecord.GetByte(2);
				this.m_scope = (AuthzData.SecurityScope)@byte;
				this.m_secItemType = SecurityItemType.Unknown;
				switch (this.m_scope)
				{
				case AuthzData.SecurityScope.CatalogItem:
					if (!polRecord.IsDBNull(3))
					{
						this.m_secItemType = AuthzData.GetSecType((ItemType)polRecord.GetInt32(3));
					}
					if (!polRecord.IsDBNull(4))
					{
						this.m_catalogItemPath = new ExternalItemPath(polRecord.GetString(4));
					}
					break;
				case AuthzData.SecurityScope.Catalog:
					this.m_secItemType = SecurityItemType.Catalog;
					break;
				case AuthzData.SecurityScope.ModelItem:
					this.m_secItemType = SecurityItemType.ModelItem;
					this.m_catalogItemID = polRecord.GetGuid(5);
					this.m_modelItemID = polRecord.GetString(6);
					break;
				default:
					throw new InternalCatalogException("Invalid security scope.");
				}
				this.m_relatedRoles = new Hashtable();
				this.AddRelatedRole(polRecord);
			}

			// Token: 0x060023F1 RID: 9201 RVA: 0x000857C5 File Offset: 0x000839C5
			internal SecurityPolicy(string xmlPolicy, SecurityItemType secItemType, AuthzData.SecurityScope scope, ExternalItemPath itemPath, Guid catalogItemID, string modelItemID)
			{
				this.m_xmlPolicy = xmlPolicy;
				this.m_secItemType = secItemType;
				this.m_scope = scope;
				this.m_catalogItemPath = itemPath;
				this.m_catalogItemID = catalogItemID;
				this.m_modelItemID = modelItemID;
			}

			// Token: 0x060023F2 RID: 9202 RVA: 0x000857FC File Offset: 0x000839FC
			internal void AddRelatedRole(IDataRecord roleRecord)
			{
				AuthzData.SecurityRole securityRole = new AuthzData.SecurityRole(roleRecord);
				if (!this.m_relatedRoles.ContainsKey(securityRole.RoleId))
				{
					this.m_relatedRoles.Add(securityRole.RoleId, securityRole);
				}
			}

			// Token: 0x17000A84 RID: 2692
			// (get) Token: 0x060023F3 RID: 9203 RVA: 0x0008583F File Offset: 0x00083A3F
			internal SecurityItemType SecItemType
			{
				get
				{
					return this.m_secItemType;
				}
			}

			// Token: 0x17000A85 RID: 2693
			// (get) Token: 0x060023F4 RID: 9204 RVA: 0x00085847 File Offset: 0x00083A47
			internal Guid PolicyId
			{
				get
				{
					return this.m_policyId;
				}
			}

			// Token: 0x17000A86 RID: 2694
			// (get) Token: 0x060023F5 RID: 9205 RVA: 0x0008584F File Offset: 0x00083A4F
			internal AuthzData.SecurityScope Scope
			{
				get
				{
					return this.m_scope;
				}
			}

			// Token: 0x17000A87 RID: 2695
			// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00085857 File Offset: 0x00083A57
			// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0008585F File Offset: 0x00083A5F
			internal string XmlPolicy
			{
				get
				{
					return this.m_xmlPolicy;
				}
				set
				{
					this.m_xmlPolicy = value;
				}
			}

			// Token: 0x17000A88 RID: 2696
			// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00085868 File Offset: 0x00083A68
			internal ExternalItemPath CatalogItemPath
			{
				get
				{
					return this.m_catalogItemPath;
				}
			}

			// Token: 0x17000A89 RID: 2697
			// (get) Token: 0x060023F9 RID: 9209 RVA: 0x00085870 File Offset: 0x00083A70
			internal Guid CatalogItemID
			{
				get
				{
					return this.m_catalogItemID;
				}
			}

			// Token: 0x17000A8A RID: 2698
			// (get) Token: 0x060023FA RID: 9210 RVA: 0x00085878 File Offset: 0x00083A78
			internal string ModelItemID
			{
				get
				{
					return this.m_modelItemID;
				}
			}

			// Token: 0x17000A8B RID: 2699
			// (get) Token: 0x060023FB RID: 9211 RVA: 0x00085880 File Offset: 0x00083A80
			internal Hashtable RelatedRoles
			{
				get
				{
					return this.m_relatedRoles;
				}
			}

			// Token: 0x0400107A RID: 4218
			private Guid m_policyId;

			// Token: 0x0400107B RID: 4219
			private string m_xmlPolicy;

			// Token: 0x0400107C RID: 4220
			private AuthzData.SecurityScope m_scope;

			// Token: 0x0400107D RID: 4221
			private SecurityItemType m_secItemType;

			// Token: 0x0400107E RID: 4222
			private ExternalItemPath m_catalogItemPath;

			// Token: 0x0400107F RID: 4223
			private Guid m_catalogItemID;

			// Token: 0x04001080 RID: 4224
			private string m_modelItemID;

			// Token: 0x04001081 RID: 4225
			private Hashtable m_relatedRoles;
		}

		// Token: 0x020004A8 RID: 1192
		internal sealed class SecurityRole
		{
			// Token: 0x060023FC RID: 9212 RVA: 0x00085888 File Offset: 0x00083A88
			internal SecurityRole(IDataRecord policyRecord)
			{
				int num = 7;
				this.m_roleId = policyRecord.GetGuid(num++);
				this.m_name = policyRecord.GetString(num++);
				this.m_taskMask = policyRecord.GetString(num++);
				int @byte = (int)policyRecord.GetByte(num++);
				this.m_scope = (AuthzData.SecurityScope)@byte;
			}

			// Token: 0x060023FD RID: 9213 RVA: 0x000858E3 File Offset: 0x00083AE3
			internal SecurityRole(string roleName, string taskMask, AuthzData.SecurityScope scope)
			{
				this.m_roleId = Guid.Empty;
				this.m_name = roleName;
				this.m_taskMask = taskMask;
				this.m_scope = scope;
			}

			// Token: 0x17000A8C RID: 2700
			// (get) Token: 0x060023FE RID: 9214 RVA: 0x0008590B File Offset: 0x00083B0B
			internal Guid RoleId
			{
				get
				{
					return this.m_roleId;
				}
			}

			// Token: 0x17000A8D RID: 2701
			// (get) Token: 0x060023FF RID: 9215 RVA: 0x00085913 File Offset: 0x00083B13
			internal string RoleName
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17000A8E RID: 2702
			// (get) Token: 0x06002400 RID: 9216 RVA: 0x0008591B File Offset: 0x00083B1B
			internal AuthzData.SecurityScope Scope
			{
				get
				{
					return this.m_scope;
				}
			}

			// Token: 0x17000A8F RID: 2703
			// (get) Token: 0x06002401 RID: 9217 RVA: 0x00085923 File Offset: 0x00083B23
			internal string TaskMask
			{
				get
				{
					return this.m_taskMask;
				}
			}

			// Token: 0x04001082 RID: 4226
			private Guid m_roleId;

			// Token: 0x04001083 RID: 4227
			private string m_name;

			// Token: 0x04001084 RID: 4228
			private AuthzData.SecurityScope m_scope;

			// Token: 0x04001085 RID: 4229
			private string m_taskMask;
		}
	}
}
