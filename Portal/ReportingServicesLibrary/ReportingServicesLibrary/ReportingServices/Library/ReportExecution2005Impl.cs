using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001FE RID: 510
	internal class ReportExecution2005Impl
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x0003B0EA File Offset: 0x000392EA
		internal ReportExecution2005Impl(RSService service, SessionfulClientRequest session, GetExceptionForEndpoint getExceptionForEndpoint)
		{
			this.m_service = service;
			this.m_session = session;
			this.m_getExceptionForEndpoint = getExceptionForEndpoint;
			Global.m_Tracer.Assert(this.m_getExceptionForEndpoint != null, "m_getExceptionForEndpoint is null");
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0003B11F File Offset: 0x0003931F
		protected virtual RSService Service
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0003B127 File Offset: 0x00039327
		protected virtual SessionfulClientRequest Session
		{
			get
			{
				return this.m_session;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003B12F File Offset: 0x0003932F
		protected GetExceptionForEndpoint GetExceptionForEndpoint
		{
			get
			{
				return this.m_getExceptionForEndpoint;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0003B138 File Offset: 0x00039338
		internal void LoadReport(string Report, string HistoryID, out ExecutionInfo3 executionInfo)
		{
			try
			{
				if (Report == null)
				{
					throw new MissingParameterException("Report");
				}
				if (HistoryID != null)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				}
				this.Service.AllowEditSessionItemPaths = true;
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateNew(this.Session, this.Service, Report, HistoryID);
					CreateNewSessionAction createNewSessionAction = new CreateNewSessionAction(sessionStarterAction.Session, this.Service, sessionStarterAction.ReportContext);
					createNewSessionAction.Save();
					executionInfo = createNewSessionAction.Result;
					sessionStarterAction.WriteSessionId(executionInfo);
					RSTrace.WebServerTracer.Trace(TraceLevel.Verbose, "LoadReport {0},  create sessionid={1}", new object[] { Report, executionInfo.ExecutionID });
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003B224 File Offset: 0x00039424
		internal void LoadReportDefinition(byte[] Definition, out ExecutionInfo3 executionInfo, out Warning[] warnings)
		{
			try
			{
				if (Definition == null)
				{
					throw new MissingParameterException("Definition");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateNew(this.Session, this.Service);
					CreateNewSessionAction createNewSessionAction = new CreateNewSessionAction(sessionStarterAction.Session, this.Service, sessionStarterAction.ReportContext);
					ExternalItemPath externalItemPath = null;
					createNewSessionAction.SaveFromDefinition(Definition, null, true, externalItemPath, false);
					executionInfo = createNewSessionAction.Result;
					warnings = createNewSessionAction.Warnings;
					sessionStarterAction.WriteSessionId(executionInfo);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003B2DC File Offset: 0x000394DC
		internal void SetExecutionCredentials(DataSourceCredentials[] Credentials, out ExecutionInfo3 executionInfo)
		{
			try
			{
				if (Credentials == null)
				{
					throw new MissingParameterException("Credentials");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateExisting(this.Session, this.Service);
					SetCredentialsAction setCredentialsAction = new SetCredentialsAction(Credentials, sessionStarterAction.Session, this.Service);
					setCredentialsAction.Save();
					executionInfo = setCredentialsAction.Result;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0003B374 File Offset: 0x00039574
		internal void SetExecutionParameters(ParameterValue[] Parameters, string ParameterLanguage, out ExecutionInfo3 executionInfo)
		{
			try
			{
				if (Parameters == null)
				{
					throw new MissingParameterException("Parameters");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateExisting(this.Session, this.Service);
					SetExecutionParametersAction setExecutionParametersAction = new SetExecutionParametersAction(ParameterValue.ThisArrayToNameValueCollection(Parameters), ParameterLanguage, sessionStarterAction.Session, this.Service);
					setExecutionParametersAction.Save();
					executionInfo = setExecutionParametersAction.Result;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003B414 File Offset: 0x00039614
		internal void ResetExecution(out ExecutionInfo3 executionInfo)
		{
			try
			{
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					ResetExecutionAction resetExecutionAction = new ResetExecutionAction(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service);
					resetExecutionAction.Save();
					executionInfo = resetExecutionAction.Result;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003B49C File Offset: 0x0003969C
		internal void Render(string Format, string DeviceInfo, PageCountMode pageCountMode, out byte[] Result, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			try
			{
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					Stream stream;
					this.InternalRender(Format, DeviceInfo, pageCountMode, out stream, out Extension, out MimeType, out Encoding, out Warnings, out StreamIds);
					Result = StreamSupport.ReadToEndNotUsingLength(stream, Global.ResponseBufferSizeBytes);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003B518 File Offset: 0x00039718
		internal void DeliverReportItem(string Format, string DeviceInfo, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData)
		{
			try
			{
				new DeliverReportItemAction(this.Session, this.Service).DeliverReportItem(Format, DeviceInfo, ExtensionSettings, Description, EventType, MatchData);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0003B564 File Offset: 0x00039764
		internal void RenderStream(string Format, string StreamID, string DeviceInfo, out byte[] Result, out string Encoding, out string MimeType)
		{
			try
			{
				if (Format == null)
				{
					throw new MissingParameterException("Format");
				}
				if (StreamID == null)
				{
					throw new MissingParameterException("StreamID");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					RenderReportAction renderReportAction = RenderReportAction.CreateWithFormatDeviceInfo(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service, Format, DeviceInfo, PageCountMode.Estimate);
					renderReportAction.RenderStream(StreamID);
					Result = renderReportAction.Result;
					MimeType = renderReportAction.MimeType;
					Encoding = renderReportAction.Encoding;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0003B620 File Offset: 0x00039820
		internal void GetExecutionInfo(out ExecutionInfo3 executionInfo)
		{
			try
			{
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					GetExecutionInfoAction getExecutionInfoAction = new GetExecutionInfoAction(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service);
					getExecutionInfoAction.Execute();
					executionInfo = getExecutionInfoAction.Result;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0003B6A8 File Offset: 0x000398A8
		internal void GetDocumentMap(out DocumentMapNode result)
		{
			try
			{
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					GetDocumentMapAction getDocumentMapAction = new GetDocumentMapAction(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service);
					result = getDocumentMapAction.PerformAction(default(NoEventParameters));
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003B734 File Offset: 0x00039934
		internal void LoadDrillthroughTarget(string DrillthroughID, out ExecutionInfo3 ExecutionInfo)
		{
			try
			{
				if (DrillthroughID == null)
				{
					throw new MissingParameterException("DrillthroughID");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					GetDrillthroughAction.Output output = new GetDrillthroughAction(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service).PerformAction(DrillthroughID);
					this.Session.SessionReport = null;
					SessionStarterAction sessionStarterAction;
					if (output.TargetDefinition != null)
					{
						sessionStarterAction = SessionStarterAction.CreateNew(this.Session, this.Service);
						CreateNewSessionAction createNewSessionAction = new CreateNewSessionAction(sessionStarterAction.Session, this.Service, sessionStarterAction.ReportContext);
						createNewSessionAction.SaveFromDefinition(output.TargetDefinition, output.TargetContext.RSRequestParameters.ReportParameters, false, output.ReportOrModelContext.ItemPath);
						ExecutionInfo = createNewSessionAction.Result;
					}
					else
					{
						sessionStarterAction = SessionStarterAction.CreateNew(this.Session, this.Service, output.TargetContext.OriginalItemPath.Value, null);
						ExecutionInfo = output.LoadTargetReport(this.Service, sessionStarterAction.Session);
					}
					sessionStarterAction.WriteSessionId(ExecutionInfo);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0003B88C File Offset: 0x00039A8C
		internal void ToggleItem(string ToggleID, out bool Found)
		{
			try
			{
				if (ToggleID == null)
				{
					throw new MissingParameterException("ToggleID");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					ToggleAction toggleAction = new ToggleAction(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service);
					Found = toggleAction.PerformAction(ToggleID);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0003B91C File Offset: 0x00039B1C
		internal void NavigateDocumentMap(string DocMapID, out int PageNumber)
		{
			try
			{
				if (DocMapID == null)
				{
					throw new MissingParameterException("DocMapID");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateExisting(this.Session, this.Service);
					DocMapNavigator docMapNavigator = new DocMapNavigator(sessionStarterAction.Session, this.Service, sessionStarterAction.UserName, sessionStarterAction.ReportContext);
					PageNumber = docMapNavigator.PerformAction(DocMapID);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003B9BC File Offset: 0x00039BBC
		internal void NavigateBookmark(string BookmarkID, out int PageNumber, out string UniqueName)
		{
			try
			{
				if (BookmarkID == null)
				{
					throw new MissingParameterException("BookmarkID");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateExisting(this.Session, this.Service);
					BookmarkNavigator.Output output = new BookmarkNavigator(sessionStarterAction.Session, this.Service, sessionStarterAction.UserName, sessionStarterAction.ReportContext).PerformAction(BookmarkID);
					PageNumber = output.PageNumber;
					UniqueName = output.UniqueName;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003BA68 File Offset: 0x00039C68
		internal void FindString(int StartPage, int EndPage, string FindValue, out int PageNumber)
		{
			try
			{
				if (FindValue == null)
				{
					throw new MissingParameterException("FindValue");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateExisting(this.Session, this.Service);
					FindStringNavigator findStringNavigator = new FindStringNavigator(sessionStarterAction.Session, this.Service, sessionStarterAction.UserName, sessionStarterAction.ReportContext);
					PageNumber = findStringNavigator.PerformAction(new FindStringNavigator.Parameters(StartPage, EndPage, FindValue));
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003BB10 File Offset: 0x00039D10
		internal void Sort(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, out int PageNumber, out string ReportItem, out ExecutionInfo3 ExecutionInfo)
		{
			try
			{
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					if (SortItem == null)
					{
						throw new MissingParameterException("SortItem");
					}
					SessionStarterAction sessionStarterAction = SessionStarterAction.CreateExisting(this.Session, this.Service);
					ReportProcessingEvent<SortAction.InputParameters, SortAction.OutputParameters> reportProcessingEvent = new SortAction(this.Service, sessionStarterAction.Session);
					SortAction.InputParameters inputParameters = new SortAction.InputParameters(SortItem, Direction, Clear, new PageCountModeValue(PaginationMode).ToProcessingPaginationMode());
					SortAction.OutputParameters outputParameters = reportProcessingEvent.PerformAction(inputParameters);
					PageNumber = outputParameters.PageNumber;
					ReportItem = outputParameters.ReportItem;
					ExecutionInfo = outputParameters.PostSortExecutionInfo;
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003BBD4 File Offset: 0x00039DD4
		internal void GetRenderResource(string Format, string DeviceInfo, out byte[] Result, out string MimeType)
		{
			try
			{
				if (Format == null || Format.Length == 0)
				{
					throw new MissingParameterException("Format");
				}
				using (this.Service.SetStreamFactory(ReportExecution2005Impl.ConstructStreamFactory()))
				{
					CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service);
					catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
					catalogItemContext.RSRequestParameters.FormatParamValue = Format;
					catalogItemContext.RSRequestParameters.SetRenderingParameters(DeviceInfo);
					Result = this.Service.GetRenderResource(catalogItemContext, out MimeType);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003BC80 File Offset: 0x00039E80
		internal void ListRenderingExtensions(out Microsoft.ReportingServices.Library.Soap2005.Extension[] Extensions)
		{
			try
			{
				Extensions = this.Service.ListExtensions(ExtensionTypeEnum.Render);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003BCBC File Offset: 0x00039EBC
		internal void InternalRender(string Format, string DeviceInfo, PageCountMode pageCountMode, out Stream Result, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			try
			{
				if (Format == null)
				{
					throw new MissingParameterException("Format");
				}
				this.Service.SetStreamFactory(new MemoryThenFileStreamFactory());
				RenderReportAction renderReportAction = RenderReportAction.CreateWithFormatDeviceInfo(SessionStarterAction.CreateExisting(this.Session, this.Service).Session, this.Service, Format, DeviceInfo, pageCountMode);
				renderReportAction.Render();
				Result = renderReportAction.ResultStream;
				if (Result != null)
				{
					Result.Seek(0L, SeekOrigin.Begin);
				}
				Extension = renderReportAction.Extension;
				MimeType = renderReportAction.MimeType;
				Encoding = renderReportAction.Encoding;
				Warnings = renderReportAction.Warnings;
				StreamIds = renderReportAction.StreamIds;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003BD7C File Offset: 0x00039F7C
		private static StreamFactoryBase ConstructStreamFactory()
		{
			return new MemoryThenFileStreamFactory();
		}

		// Token: 0x0400067F RID: 1663
		private RSService m_service;

		// Token: 0x04000680 RID: 1664
		private SessionfulClientRequest m_session;

		// Token: 0x04000681 RID: 1665
		private readonly GetExceptionForEndpoint m_getExceptionForEndpoint;
	}
}
