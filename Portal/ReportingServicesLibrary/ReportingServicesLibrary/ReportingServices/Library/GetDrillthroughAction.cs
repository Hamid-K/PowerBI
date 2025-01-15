using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.RdlGeneration;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C0 RID: 704
	internal sealed class GetDrillthroughAction : SnapshotUpdatingEvent<string, GetDrillthroughAction.Output>
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x00065FBC File Offset: 0x000641BC
		public GetDrillthroughAction(ClientRequest session, RSService service)
			: base(session, service, null)
		{
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00065FC8 File Offset: 0x000641C8
		public static CatalogItemContext GetDrillForModel(CatalogItemContext modelContext, RSService service, out byte[] targetDefinition)
		{
			service.WillDisconnectStorage();
			CatalogItemContext catalogItemContext;
			try
			{
				catalogItemContext = GetDrillthroughAction.InternalGetDrillForModel(modelContext, service, out targetDefinition);
			}
			catch (Exception ex)
			{
				service.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				service.DisconnectStorage();
			}
			return catalogItemContext;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00066024 File Offset: 0x00064224
		protected override GetDrillthroughAction.Output RunEvent(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc)
		{
			NameValueCollection nameValueCollection;
			OnDemandProcessingResult onDemandProcessingResult;
			string text = base.ProcessingEngine.ProcessDrillthroughEvent(base.EventParameter, base.Session.SessionReport.EventInfo, pc, out nameValueCollection, out onDemandProcessingResult);
			base.ProcessingResult = onDemandProcessingResult;
			if (text == null)
			{
				throw new InvalidItemPathException("(null)");
			}
			CatalogItemContext catalogItemContext = SessionReportItem.CreateContextFromSession(base.Service, base.Session).Combine(text, false) as CatalogItemContext;
			if (catalogItemContext == null)
			{
				throw new InvalidItemPathException(text);
			}
			catalogItemContext.RSRequestParameters.SetReportParameters(nameValueCollection);
			byte[] array;
			bool flag;
			return new GetDrillthroughAction.Output(this.TranslateTarget(catalogItemContext, out array, out flag), array, catalogItemContext, flag);
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00010319 File Offset: 0x0000E519
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.DrillThrough;
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000660B6 File Offset: 0x000642B6
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
			if (base.WriteEventParameters)
			{
				execInfo.AdditionalInfo.DrillthroughId = base.EventParameter;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000660D8 File Offset: 0x000642D8
		private CatalogItemContext TranslateTarget(CatalogItemContext reportOrModelContext, out byte[] targetDefinition, out bool performedAccessCheck)
		{
			targetDefinition = null;
			performedAccessCheck = false;
			base.Service.WillDisconnectStorage();
			try
			{
				byte[] array = null;
				ItemType itemType = ItemType.Unknown;
				RSTrace.CatalogTrace.Assert(!reportOrModelContext.ItemPath.IsEditSession, "!reportOrModelContext.ItemPath.IsEditSession");
				base.Service.Storage.ObjectExists(reportOrModelContext.ItemPath, out itemType, out array);
				switch (itemType)
				{
				case ItemType.Folder:
				case ItemType.Resource:
				case ItemType.DataSource:
				case ItemType.DataSet:
				case ItemType.Kpi:
				case ItemType.MobileReport:
				case ItemType.PowerBIReport:
				case ItemType.ExcelWorkbook:
					throw new WrongItemTypeException(reportOrModelContext.OriginalItemPath.Value);
				case ItemType.Report:
				case ItemType.LinkedReport:
					GetDrillthroughAction.AccessCheckReport(base.Service, array, reportOrModelContext.ItemPath);
					performedAccessCheck = true;
					return reportOrModelContext;
				case ItemType.Model:
					return GetDrillthroughAction.InternalGetDrillForModel(reportOrModelContext, base.Service, out targetDefinition);
				}
				throw new ItemNotFoundException(reportOrModelContext.OriginalItemPath.Value);
			}
			catch
			{
				base.Service.AbortTransaction();
				throw;
			}
			finally
			{
				base.Service.DisconnectStorage();
			}
			CatalogItemContext catalogItemContext;
			return catalogItemContext;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000661F8 File Offset: 0x000643F8
		private static void AccessCheckReport(RSService service, byte[] reportSecDesc, ExternalItemPath itemPath)
		{
			if (!service.SecMgr.CheckAccess(ItemType.Report, reportSecDesc, ReportOperation.ExecuteAndView, itemPath))
			{
				throw new AccessDeniedException(service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0006621C File Offset: 0x0006441C
		private static CatalogItemContext InternalGetDrillForModel(CatalogItemContext modelContext, RSService service, out byte[] targetDefinition)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DynamicDrillthrough);
			targetDefinition = null;
			DrillthroughType drillthroughType;
			string text;
			NameValueCollection nameValueCollection;
			GetDrillthroughAction.GetModelDrillParameters(modelContext.RSRequestParameters.GetAllParameters(), service, out drillthroughType, out text, out nameValueCollection);
			ModelCatalogItem modelCatalogItem = service.CatalogItemFactory.GetCatalogItem(modelContext, ItemType.Model) as ModelCatalogItem;
			ModelEntity modelEntity = modelCatalogItem.LoadUserModelAndGetEntity(text);
			CatalogItemPath drillThroughReport = service.Storage.GetDrillThroughReport(modelContext.CatalogItemPath, text, (short)drillthroughType);
			CatalogItemContext catalogItemContext;
			if (drillThroughReport == null)
			{
				modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadProperties);
				targetDefinition = RdlGenerator.CreateDrillthroughReport(modelContext.OriginalItemPath.Value, modelEntity, (ModelDrillthroughType)drillthroughType);
				catalogItemContext = new CatalogItemContext(service, new ExternalItemPath("/"), "Report");
				catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
				catalogItemContext.RSRequestParameters.SetRenderingParameters(modelContext.RSRequestParameters.RenderingParameters);
				catalogItemContext.RSRequestParameters.SetReportParameters(nameValueCollection);
			}
			else
			{
				catalogItemContext = new CatalogItemContext(service, drillThroughReport, "Report");
				catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
				catalogItemContext.RSRequestParameters.SetRenderingParameters(modelContext.RSRequestParameters.RenderingParameters);
				catalogItemContext.RSRequestParameters.SetReportParameters(nameValueCollection);
			}
			return catalogItemContext;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00066330 File Offset: 0x00064530
		private static void GetModelDrillParameters(NameValueCollection allParams, RSService service, out DrillthroughType drillType, out string entityId, out NameValueCollection realReportParameters)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(service);
			RSParameterTranslator rsparameterTranslator = new RSParameterTranslator(service.GetScopedStorage());
			catalogItemContext.RSRequestParameters.ParseQueryString(allParams, rsparameterTranslator, catalogItemContext.ItemPath);
			try
			{
				drillType = (DrillthroughType)Enum.Parse(typeof(DrillthroughType), catalogItemContext.RSRequestParameters.DrillTypeValue);
			}
			catch (FormatException ex)
			{
				throw new InvalidParameterException("DrillType", ex);
			}
			catch (ArgumentException ex2)
			{
				throw new InvalidParameterException("DrillType", ex2);
			}
			entityId = catalogItemContext.RSRequestParameters.EntityIdValue;
			if (entityId == null || entityId.Length == 0)
			{
				throw new InvalidParameterException("EntityID");
			}
			realReportParameters = catalogItemContext.RSRequestParameters.ReportParameters;
		}

		// Token: 0x020004D9 RID: 1241
		public sealed class Output
		{
			// Token: 0x0600247B RID: 9339 RVA: 0x000861D6 File Offset: 0x000843D6
			public Output(CatalogItemContext targetContext, byte[] targetDefinition, CatalogItemContext reportOrModelContext, bool accessChecksPerformed)
			{
				this.m_targetContext = targetContext;
				this.m_targetDefinition = targetDefinition;
				this.m_reportOrModelContext = reportOrModelContext;
				this.m_accessChecksPerformed = accessChecksPerformed;
			}

			// Token: 0x0600247C RID: 9340 RVA: 0x000861FC File Offset: 0x000843FC
			public ExecutionInfo3 LoadTargetReport(RSService service, ClientRequest newSession)
			{
				service.WillDisconnectStorage();
				ExecutionInfo3 result;
				try
				{
					if (!this.m_accessChecksPerformed)
					{
						ItemType itemType;
						byte[] array;
						service.Storage.ObjectExists(this.TargetContext.ItemPath, out itemType, out array);
						GetDrillthroughAction.AccessCheckReport(service, array, this.TargetContext.ItemPath);
						this.m_accessChecksPerformed = true;
					}
					CreateNewSessionAction createNewSessionAction = new CreateNewSessionAction(newSession, service, this.TargetContext);
					createNewSessionAction.Save();
					result = createNewSessionAction.Result;
				}
				catch (Exception ex)
				{
					service.AbortTransaction();
					if (ex is RSException)
					{
						throw;
					}
					throw new InternalCatalogException(ex, null);
				}
				finally
				{
					service.DisconnectStorage();
				}
				return result;
			}

			// Token: 0x17000AA1 RID: 2721
			// (get) Token: 0x0600247D RID: 9341 RVA: 0x000862A4 File Offset: 0x000844A4
			public CatalogItemContext TargetContext
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_targetContext;
				}
			}

			// Token: 0x17000AA2 RID: 2722
			// (get) Token: 0x0600247E RID: 9342 RVA: 0x000862AC File Offset: 0x000844AC
			public byte[] TargetDefinition
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_targetDefinition;
				}
			}

			// Token: 0x17000AA3 RID: 2723
			// (get) Token: 0x0600247F RID: 9343 RVA: 0x000862B4 File Offset: 0x000844B4
			public CatalogItemContext ReportOrModelContext
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_reportOrModelContext;
				}
			}

			// Token: 0x04001128 RID: 4392
			private CatalogItemContext m_targetContext;

			// Token: 0x04001129 RID: 4393
			private byte[] m_targetDefinition;

			// Token: 0x0400112A RID: 4394
			private CatalogItemContext m_reportOrModelContext;

			// Token: 0x0400112B RID: 4395
			private bool m_accessChecksPerformed;
		}
	}
}
