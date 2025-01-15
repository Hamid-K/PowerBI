using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E4 RID: 740
	internal abstract class StreamRequestHandler
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x00069470 File Offset: 0x00067670
		protected StreamRequestHandler()
		{
			this.m_cancelableRequestWrapper = new CancelableRequestWrapper();
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001A4C RID: 6732
		protected abstract string ServerDisplayPath { get; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001A4D RID: 6733
		protected abstract RSService Service { get; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001A4E RID: 6734
		protected abstract StreamFactoryBase StreamFactory { get; }

		// Token: 0x06001A4F RID: 6735
		protected abstract SessionfulClientRequest CreateSessionManager();

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001A50 RID: 6736
		protected abstract IStreamRequest Request { get; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001A51 RID: 6737
		protected abstract IStreamResponse Response { get; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001A52 RID: 6738
		protected abstract RSTrace Tracer { get; }

		// Token: 0x06001A53 RID: 6739
		protected abstract byte[] GetStyleSheet(string styleSheetName);

		// Token: 0x06001A54 RID: 6740
		protected abstract byte[] GetStyleSheetImage(string imageName);

		// Token: 0x06001A55 RID: 6741 RVA: 0x00069483 File Offset: 0x00067683
		protected virtual void Clean()
		{
			this.m_itemContext = null;
			this.m_normalizedItemPath = null;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00069493 File Offset: 0x00067693
		protected CatalogItemContext ItemContext
		{
			get
			{
				if (this.m_itemContext == null)
				{
					this.m_itemContext = new CatalogItemContext(this.Service);
				}
				return this.m_itemContext;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x000694B4 File Offset: 0x000676B4
		protected string NormalizedItemPath
		{
			get
			{
				if (this.m_normalizedItemPath == null)
				{
					this.m_normalizedItemPath = StreamRequestHandler.NormalizeItemPath(this.Request.RequestedItemPath);
				}
				return this.m_normalizedItemPath;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000694DA File Offset: 0x000676DA
		protected NameValueCollection RequestParameters
		{
			get
			{
				return this.Request.RequestParameters;
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000694E8 File Offset: 0x000676E8
		private ExternalItemPath GetParentFullPath()
		{
			RSTrace.WebServerTracer.Assert(this.NormalizedItemPath != null);
			ListParentsAction listParentsAction = this.Service.ListParentsAction;
			listParentsAction.ActionParameters.ItemPath = this.NormalizedItemPath;
			listParentsAction.Execute();
			CatalogItemList parents = listParentsAction.ActionParameters.Parents;
			if (parents == null || parents.Count == 0)
			{
				return null;
			}
			return parents[0].Path;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006954E File Offset: 0x0006774E
		protected virtual ItemType GetItemType()
		{
			GetItemTypeAction getItemTypeAction = this.Service.GetItemTypeAction;
			getItemTypeAction.ActionParameters.ItemPath = this.NormalizedItemPath;
			getItemTypeAction.Execute();
			return (ItemType)getItemTypeAction.ActionParameters.ItemType;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0006957C File Offset: 0x0006777C
		protected virtual void DefaultRender()
		{
			this.DisallowEditSession();
			if ("/".Equals(this.NormalizedItemPath, StringComparison.Ordinal))
			{
				this.RenderItem(ItemType.Folder);
				return;
			}
			this.RenderItem();
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000695A8 File Offset: 0x000677A8
		public void ExecuteCommand()
		{
			string text = this.RequestParameters["pv:Command"];
			if (text == null)
			{
				text = this.RequestParameters["rs:Command"];
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Entering StreamRequestHandler.ExecuteCommand - Command = {0}", new object[] { text });
			using (MonitoredScope.NewConcat("StreamRequestHandler.ExecuteCommand - Command = ", text))
			{
				ItemPathOptions itemPathOptions = ItemPathOptions.Validate | ItemPathOptions.Convert | ItemPathOptions.Translate | ItemPathOptions.AllowEditSessionSyntax;
				if (!this.ItemContext.SetPath(this.NormalizedItemPath, itemPathOptions))
				{
					throw new InvalidItemPathException(this.NormalizedItemPath);
				}
				string parentPathForSharePoint = ItemPathBase.GetParentPathForSharePoint(this.ItemContext.OriginalItemPath.Value);
				this.Service.EnsureSecurityZone(parentPathForSharePoint);
				this.Service.PopulateAdditionalToken(parentPathForSharePoint);
				if (this.ItemContext.ItemPath.IsEditSession)
				{
					text = "Render";
				}
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2516748054U)
				{
					if (num <= 1087202031U)
					{
						if (num <= 303739913U)
						{
							if (num != 78384968U)
							{
								if (num != 303739913U)
								{
									goto IL_04D0;
								}
								if (!(text == "RenderEdit"))
								{
									goto IL_04D0;
								}
								goto IL_04C1;
							}
							else
							{
								if (!(text == "ExecuteQueries"))
								{
									goto IL_04D0;
								}
								goto IL_04C1;
							}
						}
						else if (num != 458106446U)
						{
							if (num != 890041389U)
							{
								if (num != 1087202031U)
								{
									goto IL_04D0;
								}
								if (!(text == "Drillthrough"))
								{
									goto IL_04D0;
								}
							}
							else
							{
								if (!(text == "Render"))
								{
									goto IL_04D0;
								}
								this.RenderItem(ItemType.Report);
								goto IL_04E2;
							}
						}
						else
						{
							if (!(text == "GetExternalImages"))
							{
								goto IL_04D0;
							}
							goto IL_04C1;
						}
					}
					else if (num <= 1699144161U)
					{
						if (num != 1122240865U)
						{
							if (num != 1684507325U)
							{
								if (num != 1699144161U)
								{
									goto IL_04D0;
								}
								if (!(text == "StyleSheet"))
								{
									goto IL_04D0;
								}
								this.DisallowEditSession();
								this.HandleStyleSheetRequest();
								goto IL_04E2;
							}
							else if (!(text == "GetModelDefinition"))
							{
								goto IL_04D0;
							}
						}
						else
						{
							if (!(text == "Blank"))
							{
								goto IL_04D0;
							}
							this.DisallowEditSession();
							this.RenderBlank();
							goto IL_04E2;
						}
					}
					else if (num != 2220468209U)
					{
						if (num != 2319542722U)
						{
							if (num != 2516748054U)
							{
								goto IL_04D0;
							}
							if (!(text == "StyleSheetImage"))
							{
								goto IL_04D0;
							}
							this.HandleStyleSheetImageRequest();
							goto IL_04E2;
						}
						else
						{
							if (!(text == "LogClientTraceEvents"))
							{
								goto IL_04D0;
							}
							goto IL_04C1;
						}
					}
					else
					{
						if (!(text == "Sort"))
						{
							goto IL_04D0;
						}
						this.PopulateRSRequestParameters();
						this.SortInSession(this.CreateSessionManager());
						goto IL_04E2;
					}
					this.DisallowEditSession();
					this.RenderItem(ItemType.Model);
					goto IL_04E2;
				}
				if (num <= 3063365367U)
				{
					if (num <= 2890446926U)
					{
						if (num != 2703307612U)
						{
							if (num != 2890446926U)
							{
								goto IL_04D0;
							}
							if (!(text == "CancelProgressiveSessionJobs"))
							{
								goto IL_04D0;
							}
						}
						else
						{
							if (!(text == "ExecuteQuery"))
							{
								goto IL_04D0;
							}
							this.DisallowEditSession();
							this.PerformStreamedOperation(StreamRequestHandler.StreamedOperation.ExecuteQuery);
							goto IL_04E2;
						}
					}
					else if (num != 2903333460U)
					{
						if (num != 3007150816U)
						{
							if (num != 3063365367U)
							{
								goto IL_04D0;
							}
							if (!(text == "Get"))
							{
								goto IL_04D0;
							}
							this.DisallowEditSession();
							this.PopulateRSRequestParameters();
							this.HandleGetRequest();
							goto IL_04E2;
						}
						else
						{
							if (!(text == "GetDataSetDefinition"))
							{
								goto IL_04D0;
							}
							this.DisallowEditSession();
							this.RenderItem(ItemType.DataSet);
							goto IL_04E2;
						}
					}
					else if (!(text == "GetReportAndModels"))
					{
						goto IL_04D0;
					}
				}
				else if (num <= 3447542798U)
				{
					if (num != 3065995125U)
					{
						if (num != 3397598612U)
						{
							if (num != 3447542798U)
							{
								goto IL_04D0;
							}
							if (!(text == "ListChildren"))
							{
								goto IL_04D0;
							}
							this.DisallowEditSession();
							this.RenderItem(ItemType.Folder);
							goto IL_04E2;
						}
						else if (!(text == "GetModel"))
						{
							goto IL_04D0;
						}
					}
					else
					{
						if (!(text == "GetResourceContents"))
						{
							goto IL_04D0;
						}
						this.DisallowEditSession();
						this.RenderItem(ItemType.Resource);
						goto IL_04E2;
					}
				}
				else if (num != 3554376909U)
				{
					if (num != 3683224032U)
					{
						if (num != 4168372227U)
						{
							goto IL_04D0;
						}
						if (!(text == "GetComponentDefinition"))
						{
							goto IL_04D0;
						}
						this.DisallowEditSession();
						this.RenderItem(ItemType.Component);
						goto IL_04E2;
					}
					else
					{
						if (!(text == "GetDataSourceContents"))
						{
							goto IL_04D0;
						}
						this.DisallowEditSession();
						this.RenderItem(ItemType.DataSource);
						goto IL_04E2;
					}
				}
				else if (!(text == "CloseSession"))
				{
					goto IL_04D0;
				}
				IL_04C1:
				this.EnsureCrescentSupported();
				this.ExecuteProgressivePackageRequest(text);
				goto IL_04E2;
				IL_04D0:
				this.DefaultRender();
			}
			IL_04E2:
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Exiting StreamRequestHandler.ExecuteCommand - Command = {0} (success)", new object[] { text });
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00069AD0 File Offset: 0x00067CD0
		private void ExecuteProgressivePackageRequest(string command)
		{
			try
			{
				ProgressiveReportCounters.Current.AllRequestsTotal.Increment();
				ProgressiveReportCounters.Current.ActiveThreads.Increment();
				string text = this.RequestParameters["rs:ProgressiveSessionId"];
				if ("CloseSession".Equals(command, StringComparison.Ordinal))
				{
					this.CloseSession(text);
				}
				else
				{
					this.Response.MimeType = "application/progressive-report";
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(command);
					if (num <= 458106446U)
					{
						if (num != 78384968U)
						{
							if (num != 303739913U)
							{
								if (num != 458106446U)
								{
									goto IL_01F5;
								}
								if (!(command == "GetExternalImages"))
								{
									goto IL_01F5;
								}
							}
							else
							{
								if (!(command == "RenderEdit"))
								{
									goto IL_01F5;
								}
								try
								{
									ProgressiveReportCounters.Current.ReportRequestsTotal.Increment();
									ProgressiveReportCounters.Current.ReportRequestsActive.Increment();
									this.RenderEdit(text);
									goto IL_0205;
								}
								finally
								{
									ProgressiveReportCounters.Current.ReportRequestsActive.Decrement();
								}
								goto IL_0191;
							}
						}
						else
						{
							if (!(command == "ExecuteQueries"))
							{
								goto IL_01F5;
							}
							try
							{
								ProgressiveReportCounters.Current.QueryRequestsTotal.Increment();
								ProgressiveReportCounters.Current.QueryRequestsActive.Increment();
								this.ExecuteQueries(text);
								goto IL_0205;
							}
							finally
							{
								ProgressiveReportCounters.Current.QueryRequestsActive.Decrement();
							}
						}
						this.GetExternalImages(text);
						goto IL_0205;
					}
					if (num <= 2890446926U)
					{
						if (num != 2319542722U)
						{
							if (num != 2890446926U)
							{
								goto IL_01F5;
							}
							if (!(command == "CancelProgressiveSessionJobs"))
							{
								goto IL_01F5;
							}
							this.CancelProgressiveSessionJobs(text);
							goto IL_0205;
						}
						else
						{
							if (!(command == "LogClientTraceEvents"))
							{
								goto IL_01F5;
							}
							this.LogClientTraceEvents(text);
							goto IL_0205;
						}
					}
					else if (num != 2903333460U)
					{
						if (num != 3397598612U)
						{
							goto IL_01F5;
						}
						if (!(command == "GetModel"))
						{
							goto IL_01F5;
						}
					}
					else
					{
						if (!(command == "GetReportAndModels"))
						{
							goto IL_01F5;
						}
						this.GetReportAndModels();
						goto IL_0205;
					}
					IL_0191:
					this.GetModel(text);
					goto IL_0205;
					IL_01F5:
					RSTrace.CatalogTrace.Assert(false, "Unexpected progressive package command");
				}
				IL_0205:;
			}
			catch
			{
				ProgressiveReportCounters.Current.FailuresTotal.Increment();
				throw;
			}
			finally
			{
				ProgressiveReportCounters.Current.ActiveThreads.Decrement();
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void EnsureRSAddinVersionMatch()
		{
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00069D6C File Offset: 0x00067F6C
		protected virtual string GetPowerViewSessionId()
		{
			return this.RequestParameters["ocp-sqlrs-session-id"];
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00069D7E File Offset: 0x00067F7E
		protected virtual void EnsureCrescentSupported()
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Crescent);
			this.EnsureRSAddinVersionMatch();
			this.DisallowEditSession();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00069DA0 File Offset: 0x00067FA0
		private void RenderItem()
		{
			ItemType itemType = this.GetItemType();
			if (itemType != ItemType.Unknown)
			{
				this.RenderItem(itemType);
				return;
			}
			throw new ItemNotFoundException(this.ItemContext.OriginalItemPath.Value);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00069DD4 File Offset: 0x00067FD4
		private void RenderItem(ItemType itemType)
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderItem"))
			{
				switch (itemType)
				{
				case ItemType.Folder:
					this.RenderFolder();
					return;
				case ItemType.Report:
				case ItemType.LinkedReport:
					this.PerformStreamedOperation(StreamRequestHandler.StreamedOperation.RenderReport);
					return;
				case ItemType.Resource:
					this.RenderResource();
					return;
				case ItemType.DataSource:
					this.RenderDataSource();
					return;
				case ItemType.Model:
					this.PopulateRSRequestParameters();
					this.RenderModel();
					return;
				case ItemType.DataSet:
					this.RenderDataSet();
					return;
				case ItemType.Component:
					this.RenderComponent();
					return;
				case ItemType.ExcelWorkbook:
					this.RenderExcelWorkbook();
					return;
				}
				throw new WrongItemTypeException(this.NormalizedItemPath);
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00069E9C File Offset: 0x0006809C
		private void PerformStreamedOperation(StreamRequestHandler.StreamedOperation operation)
		{
			using (MonitoredScope.New("StreamRequestHandler.PerformStreamedOperation"))
			{
				using (this.Service.SetStreamFactory(this.StreamFactory))
				{
					if (operation != StreamRequestHandler.StreamedOperation.RenderReport)
					{
						if (operation != StreamRequestHandler.StreamedOperation.ExecuteQuery)
						{
							throw new InternalCatalogException("unexpected operation");
						}
						this.ExecuteQuery();
					}
					else
					{
						this.PopulateRSRequestParameters();
						if ("PPTX-SL".Equals(this.ItemContext.RSRequestParameters.FormatParamValue, StringComparison.OrdinalIgnoreCase))
						{
							this.EnsureCrescentSupported();
							using (((IExecutionDataProvider)new RSServiceDataProvider(this.Service)).EnterStorageContext(null))
							{
								FullReportCatalogItem fullReportCatalogItem = (FullReportCatalogItem)this.Service.CatalogItemFactory.GetCatalogItem(this.ItemContext, ItemType.RdlxReport);
								fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
								fullReportCatalogItem.LoadDefinition(true);
								using (MemoryStream memoryStream = new MemoryStream(fullReportCatalogItem.Content))
								{
									PowerPointExport.Export(memoryStream, Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext.CrescentXapLocation, Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext.ReportServerVirtualDirectoryUri.ToString(), this.NormalizedItemPath, this.Request.RequestFiles, new CreateAndRegisterStream(this.Service.StreamManager.GetNewStream));
									return;
								}
							}
						}
						SessionfulClientRequest sessionfulClientRequest = this.CreateSessionManager();
						this.RenderReport(sessionfulClientRequest);
					}
				}
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0006A050 File Offset: 0x00068250
		protected virtual void RenderReport(SessionfulClientRequest sessionManager)
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderReport"))
			{
				if (this.GetBoolRequestParam("GetNextStream", false))
				{
					this.Service.StreamManager.SetPersistStreams(sessionManager.SessionID, false);
					RSStream nextStream = this.Service.StreamManager.PersistedStreamManager.GetNextStream();
					this.Response.WriteRSStream(nextStream);
				}
				else
				{
					RenderReportAction renderReportAction = RenderReportAction.CreateWithCatalogItemContext(sessionManager, this.Service, this.ItemContext);
					bool boolRequestParam = this.GetBoolRequestParam("PersistStreams", false);
					renderReportAction.UsePersistedStreams = boolRequestParam;
					renderReportAction.Render();
					RSStream resultStream = renderReportAction.ResultStream;
					Warning[] warnings = renderReportAction.Warnings;
					RSTrace.CatalogTrace.Assert(!sessionManager.RedirectRequired, "!m_sessionManager.RedirectRequired");
					this.Response.WriteRSStream(resultStream);
					if (this.Tracer.TraceInfo)
					{
						string imageIDParamValue = this.ItemContext.RSRequestParameters.ImageIDParamValue;
						this.Tracer.Trace(TraceLevel.Info, "Processed report. Report='{0}', Stream='{1}'", new object[] { this.NormalizedItemPath, imageIDParamValue });
					}
				}
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0006A174 File Offset: 0x00068374
		internal static Stream InprocRenderReport(string reportUrl, string userName)
		{
			RSService rsservice = new RSService(userName, AuthenticationType.Windows, reportUrl);
			return StreamRequestHandler.RenderReport(reportUrl, rsservice);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0006A194 File Offset: 0x00068394
		internal static RSStream RenderReport(string reportUrl, RSService service)
		{
			string text;
			string text2;
			return StreamRequestHandler.RenderReport(reportUrl, service, string.Empty, null, out text, out text2);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0006A1B4 File Offset: 0x000683B4
		internal static RSStream RenderReport(string reportUrl, RSService service, string executionId, Func<object, string> cs, out string reportParams, out string localizedReportParams)
		{
			RSStream resultStream;
			using (MonitoredScope.New("StreamRequestHandler.RenderReport - static"))
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(new Uri(reportUrl).Query);
				string text = StreamRequestHandler.NormalizeItemPath(nameValueCollection[null]);
				ItemPathBase.GetParentPathForSharePoint(text);
				CatalogItemContext catalogItemContext = new CatalogItemContext(service);
				if (!catalogItemContext.SetPath(text, ItemPathOptions.Validate | ItemPathOptions.Convert | ItemPathOptions.Translate | ItemPathOptions.AllowEditSessionSyntax))
				{
					throw new InvalidItemPathException(text);
				}
				reportParams = null;
				localizedReportParams = null;
				bool flag = !string.IsNullOrEmpty(executionId);
				MemoryThenFileStreamFactory memoryThenFileStreamFactory = new MemoryThenFileStreamFactory();
				using (service.SetStreamFactory(memoryThenFileStreamFactory))
				{
					catalogItemContext.RSRequestParameters.ParseQueryString(nameValueCollection, new RSParameterTranslator(), catalogItemContext.ItemPath);
					catalogItemContext.RSRequestParameters.DetectFormatIfNotPresent();
					string text2 = catalogItemContext.RSRequestParameters.CatalogParameters["ParameterLanguage"];
					if (text2 != null)
					{
						Localization.SetReportParameterCulture(text2);
					}
					ClientRequest clientRequest;
					if (flag)
					{
						AlertingSessionfulClientRequest alertingSessionfulClientRequest = new AlertingSessionfulClientRequest(DatabaseSessionStorage.Current, executionId);
						alertingSessionfulClientRequest.InitForRequest(catalogItemContext, service.UserContext);
						clientRequest = alertingSessionfulClientRequest;
					}
					else
					{
						clientRequest = new SnapshotNoOpClientRequest(service.UserContext, catalogItemContext.ItemPath);
					}
					RenderReportAction renderReportAction = RenderReportAction.CreateWithCatalogItemContext(clientRequest, service, catalogItemContext);
					if (service.UserContext.UserToken == null)
					{
						renderReportAction.JobType = SubscriptionJobType.CreateSubscriptionJobType(false, JobTypeEnum.System);
					}
					if (flag)
					{
						reportParams = clientRequest.SessionReport.Report.EffectiveParams.ToUrl(true);
						if (cs != null)
						{
							localizedReportParams = clientRequest.SessionReport.Report.EffectiveParams.ToUrl(true, cs);
						}
						if (!string.IsNullOrEmpty(reportParams))
						{
							reportParams += "&rs:ParameterLanguage=";
						}
					}
					using (new RSServiceStorageAccess(service))
					{
						CatalogItem catalogItem = service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
						catalogItem.LoadProperties();
						((BaseReportCatalogItem)catalogItem).ThrowIfNoAccess(ReportOperation.CreateSubscription);
					}
					renderReportAction.Render();
					renderReportAction.ResultStream.Flush();
					renderReportAction.ResultStream.Position = 0L;
					memoryThenFileStreamFactory.UnregisterStream(renderReportAction.ResultStream.InnerStream);
					RSTrace.CatalogTrace.Assert(!clientRequest.RedirectRequired, "!session.RedirectRequired");
					resultStream = renderReportAction.ResultStream;
				}
			}
			return resultStream;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0006A414 File Offset: 0x00068614
		protected static bool IsFirstRequest(SessionfulClientRequest sessionManager, string imageId, bool getNextStream)
		{
			if (!sessionManager.IsNew)
			{
				if (getNextStream || imageId != null)
				{
					if (sessionManager.SessionReport.HasSnapshotData)
					{
						return false;
					}
					if (getNextStream)
					{
						throw new StreamNotFoundException(null);
					}
					if (imageId != null)
					{
						throw new StreamNotFoundException(imageId);
					}
				}
				return sessionManager.SessionReport.AwaitingFirstExecution || !sessionManager.SessionReport.HasSnapshotData;
			}
			if (getNextStream)
			{
				throw new StreamNotFoundException(null);
			}
			if (imageId != null)
			{
				throw new StreamNotFoundException(imageId);
			}
			return true;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0006A488 File Offset: 0x00068688
		protected bool GetBoolRequestParam(string requestParam, bool defaultValue)
		{
			string text = this.ItemContext.RSRequestParameters.CatalogParameters[requestParam];
			if (text == null)
			{
				return defaultValue;
			}
			bool flag;
			if (!bool.TryParse(text, out flag))
			{
				throw new InvalidParameterException(requestParam);
			}
			return flag;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0006A4C4 File Offset: 0x000686C4
		protected virtual void ExecuteQuery()
		{
			using (MonitoredScope.New("StreamRequestHandler.ExecuteQuery"))
			{
				string text = this.RequestParameters["rs:DataSourceName"];
				string text2 = this.RequestParameters["rs:CommandText"];
				bool flag = false;
				if (string.Compare(this.RequestParameters["rs:GetUserModel"], "True", StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
				}
				if (text == null)
				{
					throw new MissingParameterException("rs:DataSourceName");
				}
				if (text2 == null && !flag)
				{
					throw new MissingParameterException("rs:CommandText");
				}
				RSStream rsstream;
				if (text2 != null)
				{
					int num = 120;
					string text3 = this.RequestParameters["rs:Timeout"];
					if (text3 != null && !int.TryParse(text3, out num))
					{
						throw new InvalidParameterException("rs:Timeout");
					}
					if (this.Tracer.TraceInfo)
					{
						this.Tracer.Trace(TraceLevel.Info, "Call to ExecuteQuery. DataSource='{0}'", new object[] { text });
					}
					bool[] array = new bool[0];
					NameValueCollection nameValueCollection2;
					NameValueCollection nameValueCollection = RSRequestParameters.ExtractReportParameters(this.RequestParameters, ref array, out nameValueCollection2);
					this.ItemContext.SetPath(text);
					string text4 = this.RequestParameters["rs:ProgressiveSessionId"];
					IDbConnectionPool connectionPool = ProgressiveExecutionCacheManager.GetConnectionPool(new RenderEditRequest(this.Service.UserName, text4));
					rsstream = ExecuteQueryCancelableStep.ExecuteQuery(this.Service, this.ItemContext.ItemPath, text2, nameValueCollection, num, connectionPool);
				}
				else
				{
					string text5 = this.RequestParameters["rs:PerspectiveID"];
					if (this.Tracer.TraceInfo)
					{
						this.Tracer.Trace(TraceLevel.Info, "Call to GetUserModel. Model='{0}', PerspectiveID='{1}'", new object[] { text, text5 });
					}
					this.ItemContext.SetPath(text);
					rsstream = GetUserModelCancelableStep.GetUserModel(this.Service, this.ItemContext.ItemPath, text5);
				}
				this.Response.WriteRSStream(rsstream);
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0006A6AC File Offset: 0x000688AC
		private void SortInSession(SessionfulClientRequest sessionManager)
		{
			using (MonitoredScope.New("StreamRequestHandler.SortInSession"))
			{
				this.ItemContext.RSRequestParameters.AllowNewSessionsParamValue = bool.FalseString;
				sessionManager.InitForRequest(this.ItemContext, this.Service.UserContext);
				SortAction sortAction = new SortAction(this.Service, sessionManager);
				bool flag = false;
				string text = this.ItemContext.RSRequestParameters.CatalogParameters["SortId"];
				if (text == null)
				{
					throw new MissingParameterException("SortItem");
				}
				SortDirectionEnum sortDirectionEnum = SortDirectionEnum.None;
				string text2 = this.ItemContext.RSRequestParameters.CatalogParameters["SortDirection"];
				if (text2 != null)
				{
					try
					{
						sortDirectionEnum = (SortDirectionEnum)Enum.Parse(typeof(SortDirectionEnum), text2);
					}
					catch (FormatException)
					{
					}
				}
				if (!bool.TryParse(this.ItemContext.RSRequestParameters.CatalogParameters["ClearSort"], out flag))
				{
					flag = false;
				}
				using (this.Service.SetStreamFactory(new MemoryThenFileStreamFactory()))
				{
					SortAction.InputParameters inputParameters = new SortAction.InputParameters(text, sortDirectionEnum, flag, new PageCountModeValue(this.ItemContext.RSRequestParameters.PaginationModeValue).ToProcessingPaginationMode());
					int pageNumber = sortAction.PerformAction(inputParameters).PageNumber;
					this.ItemContext.RSRequestParameters.RenderingParameters.Add("Section", pageNumber.ToString(CultureInfo.InvariantCulture));
					this.RenderItem(ItemType.Report);
				}
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0006A868 File Offset: 0x00068A68
		protected virtual void RenderResource()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderResource"))
			{
				GetResourceContentsAction getResourceContentsAction = this.Service.GetResourceContentsAction;
				getResourceContentsAction.ActionParameters.ItemPath = this.NormalizedItemPath;
				getResourceContentsAction.Execute();
				string mimeType = getResourceContentsAction.ActionParameters.MimeType;
				byte[] content = getResourceContentsAction.ActionParameters.Content;
				this.Response.MimeType = mimeType;
				this.Response.BinaryWrite(content);
				if (this.Tracer.TraceInfo)
				{
					this.Tracer.Trace(TraceLevel.Info, "Processed resource '{0}'", new object[] { this.NormalizedItemPath });
				}
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0006A91C File Offset: 0x00068B1C
		protected virtual void RenderExcelWorkbook()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderExcelWorkbook"))
			{
				GetExcelWorkbookContentsAction getExcelWorkbookContentsAction = this.Service.GetExcelWorkbookContentsAction;
				getExcelWorkbookContentsAction.ActionParameters.ItemPath = this.NormalizedItemPath;
				getExcelWorkbookContentsAction.Execute();
				string mimeType = getExcelWorkbookContentsAction.ActionParameters.MimeType;
				byte[] content = getExcelWorkbookContentsAction.ActionParameters.Content;
				this.Response.MimeType = mimeType;
				this.Response.BinaryWrite(content);
				if (this.Tracer.TraceInfo)
				{
					this.Tracer.Trace(TraceLevel.Info, "Processed resource '{0}'", new object[] { this.NormalizedItemPath });
				}
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0006A9D0 File Offset: 0x00068BD0
		protected string RenderModelDrillthrough(CatalogItemContext targetContext, byte[] targetDefinition, ClientRequest sessionManager)
		{
			CreateNewSessionAction createNewSessionAction = new CreateNewSessionAction(sessionManager, this.Service, targetContext);
			if (targetDefinition != null)
			{
				createNewSessionAction.SaveFromDefinition(targetDefinition, targetContext.RSRequestParameters.ReportParameters, false, this.ItemContext.ItemPath);
			}
			else
			{
				createNewSessionAction.Save();
				new SetExecutionParametersAction(this.ItemContext.RSRequestParameters.ReportParameters, Localization.ClientPrimaryCulture.Name, sessionManager, this.Service).Save();
			}
			return this.ModelToReportRedirect(sessionManager.SessionID, targetContext);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0006AA4C File Offset: 0x00068C4C
		private string ModelToReportRedirect(string sessionId, CatalogItemContext targetContext)
		{
			string text;
			using (MonitoredScope.New("ReportServiceHttpHandler.ModelToReportRedirect"))
			{
				targetContext.RSRequestParameters.CatalogParameters["SessionID"] = sessionId;
				targetContext.RSRequestParameters.CatalogParameters.Remove("ClearSession");
				targetContext.RSRequestParameters.CatalogParameters["Command"] = "Render";
				CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(targetContext);
				catalogItemUrlBuilder.AppendCatalogParameters(targetContext.RSRequestParameters.CatalogParameters);
				catalogItemUrlBuilder.AppendRenderingParameters(targetContext.RSRequestParameters.RenderingParameters);
				text = catalogItemUrlBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0006AAF4 File Offset: 0x00068CF4
		protected virtual void RenderModel()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderModel"))
			{
				GetModelDefinitionAction getModelDefinitionAction = this.Service.GetModelDefinitionAction;
				getModelDefinitionAction.ActionParameters.ItemPath = this.NormalizedItemPath;
				getModelDefinitionAction.Execute();
				this.Response.MimeType = "text/xml";
				this.Response.ContentEncoding = Encoding.UTF8;
				this.Response.BinaryWrite(getModelDefinitionAction.ActionParameters.ModelDefinition);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0006AB84 File Offset: 0x00068D84
		private void RenderFolder()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderFolder"))
			{
				this.Response.MimeType = "text/html";
				this.Response.ContentEncoding = Encoding.UTF8;
				string text = HttpUtility.HtmlEncode(this.ServerDisplayPath + " - " + this.NormalizedItemPath).Replace("  ", " &nbsp;");
				this.Response.Write("<html>\r\n  <head>\r\n");
				this.Response.Write("    <meta charset=\"utf-8\">\r\n");
				this.Response.Write("    <meta name=\"Generator\" content=\"");
				this.Response.Write(HttpUtility.HtmlEncode(Globals.Configuration.ServerProductNameAndVersion));
				this.Response.Write("\">\r\n    <title>");
				this.Response.Write(text);
				this.Response.Write("</title>\r\n  </head>\r\n  <body><H1>");
				this.Response.Write(text);
				this.Response.Write("</H1><hr>\r\n\r\n<pre>");
				ExternalItemPath parentFullPath = this.GetParentFullPath();
				if (parentFullPath != null)
				{
					this.Response.Write("<A HREF=\"");
					CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service);
					catalogItemContext.SetPath(parentFullPath);
					CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(catalogItemContext, true);
					catalogItemUrlBuilder.AppendCatalogParameter("Command", "ListChildren");
					this.Response.Write(HttpUtility.HtmlAttributeEncode(catalogItemUrlBuilder.ToString()));
					this.Response.Write("\">[");
					this.Response.Write(HttpUtility.HtmlEncode(RepLibRes.ParentDirectoryLink));
					this.Response.Write("]</A>\r\n");
				}
				ListChildrenAction listChildrenAction = this.Service.ListChildrenAction;
				listChildrenAction.ActionParameters.ItemPath = this.NormalizedItemPath;
				listChildrenAction.ActionParameters.Recursive = false;
				listChildrenAction.Execute();
				CatalogItemList children = listChildrenAction.ActionParameters.Children;
				for (int i = 0; i < children.Count; i++)
				{
					CatalogItemDescriptor catalogItemDescriptor = children[i];
					string name = catalogItemDescriptor.Name;
					int num = ((catalogItemDescriptor.Size >= 0) ? catalogItemDescriptor.Size : 0);
					ItemType type = catalogItemDescriptor.Type;
					string text2 = string.Empty;
					string text3 = null;
					switch (type)
					{
					case ItemType.Folder:
					case ItemType.Site:
						text2 = "        &lt;dir&gt;";
						text3 = "ListChildren";
						break;
					case ItemType.Report:
					case ItemType.RdlxReport:
						text2 = num.ToString(CultureInfo.InvariantCulture);
						text2 = text2.PadLeft(13);
						text3 = "Render";
						break;
					case ItemType.Resource:
						text2 = num.ToString(CultureInfo.InvariantCulture);
						text2 = text2.PadLeft(13);
						text3 = "GetResourceContents";
						break;
					case ItemType.LinkedReport:
						text2 = "       &lt;link&gt;";
						text3 = "Render";
						break;
					case ItemType.DataSource:
						text2 = "         &lt;ds&gt;";
						text3 = "GetDataSourceContents";
						break;
					case ItemType.Model:
						text2 = num.ToString(CultureInfo.InvariantCulture);
						text2 = text2.PadLeft(13);
						text3 = "GetModelDefinition";
						break;
					case ItemType.DataSet:
						text2 = num.ToString(CultureInfo.InvariantCulture);
						text2 = text2.PadLeft(13);
						text3 = "GetDataSetDefinition";
						break;
					case ItemType.Component:
						text2 = num.ToString(CultureInfo.InvariantCulture);
						text2 = text2.PadLeft(13);
						text3 = "GetComponentDefinition";
						break;
					case ItemType.Kpi:
						text2 = "        &lt;kpi&gt;";
						break;
					case ItemType.MobileReport:
						text2 = "         &lt;mr&gt;";
						break;
					case ItemType.PowerBIReport:
						text2 = "       &lt;pbix&gt;";
						break;
					case ItemType.ExcelWorkbook:
						text2 = "      &lt;excel&gt;";
						break;
					default:
						throw new InternalCatalogException("Unexpected item type");
					}
					string text4 = string.Empty;
					if (catalogItemDescriptor.CreationDate > DateTime.MinValue)
					{
						text4 = catalogItemDescriptor.CreationDate.ToString("f", Localization.ClientPrimaryCulture);
					}
					text4 = text4.PadLeft(38);
					this.Response.Write(text4);
					this.Response.Write(text2);
					if (name != null && name.Length != 0)
					{
						this.Response.Write(" <A HREF=\"");
						StringBuilder stringBuilder = new StringBuilder("?" + HttpUtility.UrlEncode(catalogItemDescriptor.Path.Value));
						if (text3 != null)
						{
							stringBuilder.Append("&");
							stringBuilder.Append("rs:");
							stringBuilder.Append("Command");
							stringBuilder.Append("=");
							stringBuilder.Append(text3);
						}
						this.Response.Write(HttpUtility.HtmlAttributeEncode(stringBuilder.ToString()));
						this.Response.Write("\">");
						this.Response.Write(HttpUtility.HtmlEncode(name));
						this.Response.Write("</A>");
					}
					this.Response.Write("\r\n");
				}
				this.Response.Write("</pre><hr>\r\n");
				this.Response.Write(Globals.Configuration.ServerProductNameAndVersion);
				this.Response.Write("\r\n\r\n  </body>\r\n</html>");
				if (this.Tracer.TraceInfo)
				{
					this.Tracer.Trace(TraceLevel.Info, "Processed folder '{0}'", new object[] { this.NormalizedItemPath });
				}
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0006B0C4 File Offset: 0x000692C4
		private void RenderDataSource()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderDataSource"))
			{
				GetDataSourceContentsAction getDataSourceContentsAction = this.Service.GetDataSourceContentsAction;
				getDataSourceContentsAction.ActionParameters.DataSourcePath = this.NormalizedItemPath;
				getDataSourceContentsAction.Execute();
				this.Response.MimeType = "text/xml";
				this.Response.ContentEncoding = Encoding.UTF8;
				this.Response.Write(DataSourceDefinition.ThisToXml(getDataSourceContentsAction.ActionParameters.DataSourceDefinition));
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0006B158 File Offset: 0x00069358
		private void RenderComponent()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderComponent"))
			{
				GetComponentDefinitionAction getComponentDefinitionAction = this.Service.GetComponentDefinitionAction;
				getComponentDefinitionAction.ActionParameters.ItemPath = this.NormalizedItemPath;
				getComponentDefinitionAction.Execute();
				this.Response.MimeType = "text/xml";
				this.Response.ContentEncoding = Encoding.UTF8;
				this.Response.BinaryWrite(getComponentDefinitionAction.ActionParameters.ComponentDefinition);
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0006B1E8 File Offset: 0x000693E8
		private void RenderDataSet()
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderDataSet"))
			{
				GetDataSetDefinitionAction getDataSetDefinitionAction = this.Service.GetDataSetDefinitionAction;
				getDataSetDefinitionAction.ActionParameters.ItemPath = this.NormalizedItemPath;
				getDataSetDefinitionAction.Execute();
				this.Response.MimeType = "text/xml";
				this.Response.ContentEncoding = Encoding.UTF8;
				this.Response.BinaryWrite(getDataSetDefinitionAction.ActionParameters.DataSetDefinition);
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0006B278 File Offset: 0x00069478
		private void RenderEdit(string sessionId)
		{
			using (MonitoredScope.New("StreamRequestHandler.RenderEdit"))
			{
				using (new AverageDurationCounterMonitor(ProgressiveReportCounters.Current.ReportRequestsAverageDuration))
				{
					using (RenderEditCancelableStep renderEditCancelableStep = new RenderEditCancelableStep(this.Service, this.ItemContext, sessionId, this.Request.InputStream, this.Response.OutputStream, this.Response.ResponseFlags))
					{
						this.m_cancelableRequestWrapper.ExecuteCancelableProgressiveRequest(renderEditCancelableStep);
					}
				}
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0006B328 File Offset: 0x00069528
		private void ExecuteQueries(string sessionId)
		{
			using (MonitoredScope.New("StreamRequestHandler.ExecuteQueries"))
			{
				using (new AverageDurationCounterMonitor(ProgressiveReportCounters.Current.QueryRequestsAverageDuration))
				{
					string text = this.RequestParameters["rs:IsCancellable"];
					string text2 = this.RequestParameters["rs:DataSourceName"];
					bool flag;
					if (!bool.TryParse(text, out flag))
					{
						flag = true;
					}
					using (ExecuteQueriesCancelableStep executeQueriesCancelableStep = new ExecuteQueriesCancelableStep(this.Service, this.NormalizedItemPath, text2, this.ItemContext, sessionId, this.Request.InputStream, this.Response.OutputStream, this.Response.ResponseFlags, flag))
					{
						this.m_cancelableRequestWrapper.ExecuteCancelableProgressiveRequest(executeQueriesCancelableStep);
					}
				}
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0006B410 File Offset: 0x00069610
		private void GetExternalImages(string sessionId)
		{
			using (MonitoredScope.New("StreamRequestHandler.GetExternalImages"))
			{
				using (GetExternalImagesCancelableStep getExternalImagesCancelableStep = new GetExternalImagesCancelableStep(sessionId, this.Request.InputStream, this.Response.OutputStream, this.Response.ResponseFlags, this.ItemContext, this.Service))
				{
					new CancelableRequestWrapper().ExecuteCancelableProgressiveRequest(getExternalImagesCancelableStep);
				}
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0006B49C File Offset: 0x0006969C
		private void LogClientTraceEvents(string sessionId)
		{
			using (MonitoredScope.New("StreamRequestHandler.LogClientTraceEvents"))
			{
				using (LogClientTraceEventsAction logClientTraceEventsAction = new LogClientTraceEventsAction(new RenderEditRequest(this.Service.UserName, sessionId), this.Request.InputStream, this.Response.OutputStream, this.Response.ResponseFlags, this.Service))
				{
					logClientTraceEventsAction.Execute();
				}
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0006B52C File Offset: 0x0006972C
		private void GetModel(string sessionId)
		{
			using (MonitoredScope.New("StreamRequestHandler.GetModel"))
			{
				using (new AverageDurationCounterMonitor(ProgressiveReportCounters.Current.ModelRequestsAverageDuration))
				{
					try
					{
						ProgressiveReportCounters.Current.ModelRequestsTotal.Increment();
						ProgressiveReportCounters.Current.ModelRequestsActive.Increment();
						using (MonitoredScope.New("StreamRequestHandler.GetModel"))
						{
							using (new AverageDurationCounterMonitor(ProgressiveReportCounters.Current.ModelRequestsAverageDuration))
							{
								IRenderEditSession renderEditSession = new RenderEditRequest(this.Service.UserName, sessionId);
								string text = this.RequestParameters["rs:DataSourceName"];
								string text2 = this.RequestParameters["rs:ModelMetadataVersion"];
								using (GetModelCancelableStep getModelCancelableStep = new GetModelCancelableStep(renderEditSession, this.NormalizedItemPath, text, text2, this.Request.InputStream, this.Response.OutputStream, this.Response.ResponseMetadata, this.Response.ResponseFlags, this.Service, this.ItemContext))
								{
									this.m_cancelableRequestWrapper.ExecuteCancelableProgressiveRequest(getModelCancelableStep);
								}
							}
						}
					}
					finally
					{
						ProgressiveReportCounters.Current.ModelRequestsActive.Decrement();
					}
				}
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0006B6F4 File Offset: 0x000698F4
		private void GetReportAndModels()
		{
			string text = this.RequestParameters["rs:OmitModelDefinitions"];
			string text2 = this.RequestParameters["rs:ModelMetadataVersion"];
			using (GetReportAndModelsAction getReportAndModelsAction = new GetReportAndModelsAction(this.NormalizedItemPath, text2, bool.TrueString.Equals(text, StringComparison.OrdinalIgnoreCase), this.Response.OutputStream, this.Response.ResponseFlags, this.Service, this.ItemContext))
			{
				getReportAndModelsAction.Execute();
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0006B780 File Offset: 0x00069980
		private void CloseSession(string sessionId)
		{
			if (string.IsNullOrEmpty(sessionId))
			{
				throw new MissingParameterException("rs:ProgressiveSessionId");
			}
			ProgressiveExecutionCacheManager.RemoveCacheEntry(new RenderEditRequest(this.Service.UserName, sessionId));
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0006B7AC File Offset: 0x000699AC
		private void CancelProgressiveSessionJobs(string sessionId)
		{
			using (MonitoredScope.New("StreamRequestHandler.CancelProgressiveSessionJobs"))
			{
				using (CancelProgressiveSessionJobsAction cancelProgressiveSessionJobsAction = new CancelProgressiveSessionJobsAction(new RenderEditRequest(this.Service.UserName, sessionId), this.Response.OutputStream, this.Response.ResponseFlags, this.Service))
				{
					cancelProgressiveSessionJobsAction.Execute();
				}
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006B830 File Offset: 0x00069A30
		private void HandleGetRequest()
		{
			using (MonitoredScope.New("StreamRequestHandler.HandleGetRequest"))
			{
				string text = null;
				byte[] array = null;
				string text2 = this.ItemContext.RSRequestParameters.RenderingParameters["GetImage"];
				if (string.IsNullOrEmpty(text2))
				{
					throw new InternalResourceNotFoundException();
				}
				if (array == null)
				{
					using (this.Service.SetStreamFactory(new MemoryStreamFactory()))
					{
						array = this.Service.GetRenderResource(this.ItemContext, out text);
					}
				}
				if (array == null || array.Length == 0)
				{
					throw new InternalResourceNotFoundException(text2);
				}
				this.Response.MimeType = text;
				this.Response.BinaryWrite(array);
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006B8FC File Offset: 0x00069AFC
		private void HandleStyleSheetRequest()
		{
			using (MonitoredScope.New("StreamRequestHandler.HandleStyleSheetRequest"))
			{
				string text = this.RequestParameters["Name"];
				byte[] styleSheet = this.GetStyleSheet(text);
				this.Response.MimeType = "text/css";
				this.Response.BinaryWrite(styleSheet);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0006B968 File Offset: 0x00069B68
		private void HandleStyleSheetImageRequest()
		{
			using (MonitoredScope.New("StreamRequestHandler.HandleStyleSheetImage"))
			{
				string text = this.RequestParameters["Name"];
				byte[] styleSheetImage = this.GetStyleSheetImage(text);
				string text2 = "image/" + Path.GetExtension(text).Replace(".", string.Empty);
				if (text2.Equals("image/jpg", StringComparison.OrdinalIgnoreCase))
				{
					text2 = "image/jpeg";
				}
				this.Response.MimeType = text2;
				this.Response.BinaryWrite(styleSheetImage);
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0006BA04 File Offset: 0x00069C04
		private void RenderBlank()
		{
			this.Response.MimeType = "text/html";
			this.Response.ContentEncoding = Encoding.UTF8;
			this.Response.Write("<HTML />");
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0006BA38 File Offset: 0x00069C38
		private void PopulateRSRequestParameters()
		{
			if (this.m_populatedRequestParameters)
			{
				return;
			}
			this.Service.EnsureValidDatabase();
			this.ItemContext.RSRequestParameters.ParseQueryString(this.RequestParameters, new RSParameterTranslator(), this.ItemContext.ItemPath);
			this.ItemContext.RSRequestParameters.DetectFormatIfNotPresent();
			this.ItemContext.RSRequestParameters.SetBrowserCapabilities(this.Request.BrowserCapabilities);
			this.m_populatedRequestParameters = true;
			this.SetInitialState();
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0006BAB8 File Offset: 0x00069CB8
		protected virtual void SetInitialState()
		{
			string text = this.ItemContext.RSRequestParameters.CatalogParameters["ParameterLanguage"];
			if (text != null)
			{
				Localization.SetReportParameterCulture(text);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0006BAEC File Offset: 0x00069CEC
		private static string NormalizeItemPath(string itemPath)
		{
			if (string.IsNullOrEmpty(itemPath) || "/".Equals(itemPath, StringComparison.Ordinal))
			{
				return "/";
			}
			if (itemPath.EndsWith("/", StringComparison.Ordinal))
			{
				char[] array = new char[] { '/' };
				return itemPath.TrimEnd(array);
			}
			return itemPath;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0006BB38 File Offset: 0x00069D38
		internal static Exception GetExceptionToWrite(Exception e, out ErrorCode errorCode)
		{
			Exception ex = e;
			ThreadAbortException ex2 = e as ThreadAbortException;
			if (ex2 != null && ex2.ExceptionState != null)
			{
				ReportServerAbortInfo reportServerAbortInfo = ex2.ExceptionState as ReportServerAbortInfo;
				if (reportServerAbortInfo != null && (reportServerAbortInfo.Reason == ReportServerAbortInfo.AbortReason.JobCanceled || reportServerAbortInfo.Reason == ReportServerAbortInfo.AbortReason.JobOrphaned))
				{
					ex = new JobCanceledException(ex2);
				}
			}
			if (ex is RSException)
			{
				errorCode = ((RSException)ex).Code;
			}
			else
			{
				errorCode = ErrorCode.rsUnexpectedError;
			}
			return ex;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0006BBA0 File Offset: 0x00069DA0
		private void DisallowEditSession()
		{
			if (this.ItemContext.ItemPath.IsEditSession)
			{
				throw new InvalidItemPathException(this.NormalizedItemPath);
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00005BEF File Offset: 0x00003DEF
		internal static bool IsAdomdException(Exception e)
		{
			return false;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0006BBC0 File Offset: 0x00069DC0
		internal static int GetStatusCodeForException(RSException rsException)
		{
			int num = Constants.StatusCode_ServerInternalError;
			if (!rsException.ContainsErrorCode(ErrorCode.rsInternalError) && !rsException.ContainsErrorCode(ErrorCode.rsUnexpectedError))
			{
				if (rsException.ContainsErrorCode(ErrorCode.rsInvalidItemPath) || rsException.ContainsErrorCode(ErrorCode.rsInvalidParameter) || rsException.ContainsErrorCode(ErrorCode.rsMissingParameter) || rsException.ContainsErrorCode(ErrorCode.rsMalformedXml) || rsException.ContainsErrorCode(ErrorCode.rsWrongItemType) || rsException.ContainsErrorCode(ErrorCode.rsInternalResourceNotSpecifiedError) || rsException.ContainsErrorCode(ErrorCode.rsInvalidSessionId) || rsException.ContainsErrorCode(ErrorCode.rsInvalidSessionCatalogItems) || rsException.ContainsErrorCode(ErrorCode.rsSessionNotFound) || rsException.ContainsErrorCode(ErrorCode.rsApiVersionDiscontinued) || rsException.ContainsErrorCode(ErrorCode.rsApiVersionNotRecognized) || rsException.ContainsErrorCode(ErrorCode.rsDataShapeQueryTranslationError) || rsException.ContainsErrorCode(ErrorCode.rsSessionOutOfSync) || rsException.ContainsErrorCode(ErrorCode.rsSessionFailedOver))
				{
					num = Constants.StatusCode_BadRequest;
				}
				else if (rsException.ContainsErrorCode(ErrorCode.rsItemNotFound) || rsException.ContainsErrorCode(ErrorCode.rsDataSourceNotFound) || rsException.ContainsErrorCode(ErrorCode.rsInternalResourceNotFoundError))
				{
					num = Constants.StatusCode_BadRequest;
				}
				else if (rsException.ContainsErrorCode(ErrorCode.rsOperationNotSupported))
				{
					num = Constants.StatusCode_NotFound;
				}
			}
			return num;
		}

		// Token: 0x0400097D RID: 2429
		private bool m_populatedRequestParameters;

		// Token: 0x0400097E RID: 2430
		private CatalogItemContext m_itemContext;

		// Token: 0x0400097F RID: 2431
		private string m_normalizedItemPath;

		// Token: 0x04000980 RID: 2432
		private readonly CancelableRequestWrapper m_cancelableRequestWrapper;

		// Token: 0x020004EB RID: 1259
		private enum StreamedOperation
		{
			// Token: 0x0400114B RID: 4427
			RenderReport,
			// Token: 0x0400114C RID: 4428
			ExecuteQuery
		}
	}
}
