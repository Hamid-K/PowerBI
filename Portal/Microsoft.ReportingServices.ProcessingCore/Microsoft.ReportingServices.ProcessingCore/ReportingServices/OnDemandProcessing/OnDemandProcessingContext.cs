using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Utils;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200081F RID: 2079
	public sealed class OnDemandProcessingContext : IInternalProcessingContext, IStaticReferenceable
	{
		// Token: 0x06007374 RID: 29556 RVA: 0x001DF318 File Offset: 0x001DD518
		internal OnDemandProcessingContext(Microsoft.ReportingServices.ReportProcessing.ProcessingContext externalProcessingContext, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, OnDemandMetadata odpMetadata, ErrorContext errorContext, DateTime executionTime, bool snapshotProcessing, bool reprocessSnapshot, bool processWithCachedData, ReportProcessing.StoreServerParameters storeServerParameters, UserProfileState userProfileState, ExecutionLogContext executionLogContext, IConfiguration configuration, OnDemandProcessingContext.Mode contextMode, IAbortHelper abortHelper)
		{
			IJobContext jobContext = externalProcessingContext.JobContext;
			bool flag = false;
			AbortHelper abortHelper2 = abortHelper as AbortHelper;
			if (abortHelper2 == null)
			{
				if (!snapshotProcessing && !reprocessSnapshot)
				{
					abortHelper2 = new ReportAbortHelper(externalProcessingContext.JobContext, contextMode == OnDemandProcessingContext.Mode.Streaming);
				}
			}
			else
			{
				flag = true;
			}
			this.m_commonInfo = new OnDemandProcessingContext.CommonInfo(externalProcessingContext.ChunkFactory, externalProcessingContext.OnDemandSubReportCallback, externalProcessingContext.GetResourceCallback, storeServerParameters, externalProcessingContext.ReportRuntimeSetup, externalProcessingContext.AllowUserProfileState, externalProcessingContext.RequestUserName, externalProcessingContext.UserLanguage, executionTime, reprocessSnapshot, processWithCachedData, externalProcessingContext.CreateStreamCallback, externalProcessingContext.EnableDataBackedParameters, externalProcessingContext.JobContext, externalProcessingContext.ExtFactory, externalProcessingContext.DataProtection, executionLogContext, externalProcessingContext.DataSources, externalProcessingContext.SharedDataSetReferences, externalProcessingContext.CreateAndSetupDataExtensionFunction, configuration, new ReportProcessing.DataSourceInfoHashtable(), externalProcessingContext as ReportProcessingContext, abortHelper2, flag, userProfileState, this, contextMode, OnDemandProcessingContext.CreateImageCacheManager(contextMode, odpMetadata, externalProcessingContext.ChunkFactory), externalProcessingContext.UsePreviewCommands, externalProcessingContext.CompareSafeExpressionsToLegacy, externalProcessingContext.UseUserLanguageForProcessing);
			this.m_errorContext = errorContext;
			this.m_snapshotProcessing = snapshotProcessing;
			this.m_catalogItemContext = externalProcessingContext.ReportContext;
			this.m_reportDefinition = report;
			this.m_odpMetadata = odpMetadata;
			this.m_parentContext = null;
			this.m_reportItemsReferenced = report.HasReportItemReferences;
			this.m_reportItemThisDotValueReferenced = false;
			this.m_embeddedImages = report.EmbeddedImages;
			this.m_processReportParameters = false;
			this.m_reportRuntime = null;
			this.m_inSubreport = false;
			this.m_inSubreportInDataRegion = false;
			this.m_isSharedDataSetExecutionOnly = false;
			this.m_externalDataSetContext = null;
			this.m_stateManager = this.CreateStateManager(contextMode);
			this.m_reportObjectModel = new ObjectModelImpl(this);
			if (contextMode != OnDemandProcessingContext.Mode.DefinitionOnly)
			{
				this.m_specialRecursiveAggregates = report.HasSpecialRecursiveAggregates;
				this.m_userSortFilterContext = new Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext();
				this.InitializeDataSetMembers(report.MappingNameToDataSet.Count);
			}
			this.InitFlags(report);
			this.m_odpMetadata.OdpContexts.Add(this);
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x001DF518 File Offset: 0x001DD718
		internal OnDemandProcessingContext(Microsoft.ReportingServices.ReportProcessing.ProcessingContext externalProcessingContext, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, OnDemandMetadata odpMetadata, ErrorContext errorContext, DateTime executionTime, ReportProcessing.StoreServerParameters storeServerParameters, UserProfileState userProfileState, ExecutionLogContext executionLogContext, IConfiguration configuration, IAbortHelper abortHelper)
			: this(externalProcessingContext, report, odpMetadata, errorContext, executionTime, false, false, false, storeServerParameters, userProfileState, executionLogContext, configuration, OnDemandProcessingContext.Mode.DefinitionOnly, abortHelper)
		{
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x001DF540 File Offset: 0x001DD740
		internal OnDemandProcessingContext(OnDemandProcessingContext aContext, bool aReportItemsReferenced, Microsoft.ReportingServices.ReportIntermediateFormat.Report aReport)
		{
			this.m_isPageHeaderFooter = true;
			this.m_reportDefinition = aReport;
			this.m_parentContext = aContext;
			this.m_odpMetadata = aContext.OdpMetadata;
			this.m_commonInfo = aContext.m_commonInfo;
			this.m_errorContext = aContext.ErrorContext;
			this.m_inSubreport = aContext.m_inSubreport;
			this.m_inSubreportInDataRegion = aContext.m_inSubreportInDataRegion;
			this.m_isSharedDataSetExecutionOnly = false;
			this.m_externalDataSetContext = null;
			this.m_snapshotProcessing = aContext.m_snapshotProcessing;
			this.m_catalogItemContext = aContext.m_catalogItemContext;
			this.m_reportItemsReferenced = aReportItemsReferenced;
			this.m_reportItemThisDotValueReferenced = false;
			this.m_embeddedImages = aContext.m_embeddedImages;
			this.m_processReportParameters = false;
			this.m_initializedRuntime = false;
			this.m_specialRecursiveAggregates = false;
			this.m_userSortFilterContext = new Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext();
			this.m_threadCulture = aContext.m_threadCulture;
			this.m_compareInfo = aContext.m_compareInfo;
			this.m_clrCompareOptions = aContext.m_clrCompareOptions;
			this.m_stateManager = this.CreateStateManager(this.m_commonInfo.ContextMode);
			if (this.m_commonInfo.ContextMode != OnDemandProcessingContext.Mode.DefinitionOnly)
			{
				this.m_reportObjectModel = new ObjectModelImpl(aContext.ReportObjectModel, this);
				this.m_reportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(UserProfileState.OnDemandExpressions);
				this.m_reportRuntime = new Microsoft.ReportingServices.RdlExpressions.ReportRuntime(aReport.ObjectType, this.m_reportObjectModel, this.ErrorContext);
				this.m_reportRuntime.LoadCompiledCode(aReport, false, false, this.m_reportObjectModel, this.ReportRuntimeSetup);
				this.m_reportRuntime.CustomCodeOnInit(aReport);
			}
			this.m_isUnrestrictedRenderFormatReferenceMode = true;
			this.m_odpMetadata.OdpContexts.Add(this);
		}

		// Token: 0x06007377 RID: 29559 RVA: 0x001DF70C File Offset: 0x001DD90C
		internal OnDemandProcessingContext(Microsoft.ReportingServices.ReportProcessing.ProcessingContext originalProcessingContext, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, ErrorContext errorContext, DateTime executionTime, bool snapshotProcessing, IConfiguration configuration)
		{
			this.m_commonInfo = new OnDemandProcessingContext.CommonInfo(null, null, null, null, originalProcessingContext.ReportRuntimeSetup, originalProcessingContext.AllowUserProfileState, originalProcessingContext.RequestUserName, originalProcessingContext.UserLanguage, executionTime, false, false, originalProcessingContext.CreateStreamCallback, originalProcessingContext.EnableDataBackedParameters, originalProcessingContext.JobContext, originalProcessingContext.ExtFactory, originalProcessingContext.DataProtection, new ExecutionLogContext(originalProcessingContext.JobContext), originalProcessingContext.DataSources, originalProcessingContext.SharedDataSetReferences, originalProcessingContext.CreateAndSetupDataExtensionFunction, configuration, new ReportProcessing.DataSourceInfoHashtable(), originalProcessingContext as ReportProcessingContext, new ReportAbortHelper(originalProcessingContext.JobContext, false), false, UserProfileState.None, this, OnDemandProcessingContext.Mode.Full, null, originalProcessingContext.UsePreviewCommands, originalProcessingContext.CompareSafeExpressionsToLegacy, originalProcessingContext.UseUserLanguageForProcessing);
			this.m_errorContext = errorContext;
			this.m_snapshotProcessing = snapshotProcessing;
			this.m_catalogItemContext = originalProcessingContext.ReportContext;
			this.m_reportDefinition = report;
			this.m_odpMetadata = null;
			this.m_reportItemsReferenced = false;
			this.m_reportItemThisDotValueReferenced = false;
			this.m_embeddedImages = null;
			this.m_processReportParameters = true;
			this.m_initializedRuntime = false;
			this.m_specialRecursiveAggregates = false;
			this.m_userSortFilterContext = new Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext();
			this.m_inSubreport = false;
			this.m_inSubreportInDataRegion = false;
			this.m_isSharedDataSetExecutionOnly = false;
			this.m_externalDataSetContext = null;
			this.m_stateManager = new OnDemandStateManagerFull(this);
			if (report != null)
			{
				this.m_reportObjectModel = new ObjectModelImpl(this);
				this.m_reportObjectModel.Initialize(report, null);
				this.m_reportRuntime = new Microsoft.ReportingServices.RdlExpressions.ReportRuntime(report.ObjectType, this.m_reportObjectModel, this.ErrorContext);
				this.m_reportRuntime.LoadCompiledCode(report, true, true, this.m_reportObjectModel, this.ReportRuntimeSetup);
				this.m_reportRuntime.CustomCodeOnInit(report);
			}
		}

		// Token: 0x06007378 RID: 29560 RVA: 0x001DF8DC File Offset: 0x001DDADC
		internal OnDemandProcessingContext(DataSetContext dc, DataSetDefinition dataSetDefinition, ErrorContext errorContext, IConfiguration configuration)
		{
			this.m_externalDataSetContext = dc;
			AbortHelper abortHelper = dc.JobContext.GetAbortHelper() as AbortHelper;
			bool flag;
			if (abortHelper != null)
			{
				flag = true;
			}
			else
			{
				abortHelper = new ReportAbortHelper(dc.JobContext, false);
				flag = false;
			}
			this.m_commonInfo = new OnDemandProcessingContext.CommonInfo(dc.CreateChunkFactory, null, null, null, dc.DataSetRuntimeSetup, dc.AllowUserProfileState, dc.RequestUserName, dc.Culture, dc.ExecutionTimeStamp, false, false, dc.CreateStreamCallbackForScalability, false, dc.JobContext, null, dc.DataProtection, new ExecutionLogContext(dc.JobContext), dc.DataSources, null, dc.CreateAndSetupDataExtensionFunction, configuration, new ReportProcessing.DataSourceInfoHashtable(), null, abortHelper, flag, UserProfileState.None, this, OnDemandProcessingContext.Mode.Full, null, false, false, false);
			this.m_errorContext = errorContext;
			this.m_snapshotProcessing = false;
			this.m_isSharedDataSetExecutionOnly = true;
			this.m_catalogItemContext = dc.ItemContext;
			this.m_odpMetadata = null;
			this.m_reportItemsReferenced = false;
			this.m_reportItemThisDotValueReferenced = false;
			this.m_embeddedImages = null;
			this.m_processReportParameters = false;
			this.m_initializedRuntime = false;
			this.m_specialRecursiveAggregates = false;
			this.m_userSortFilterContext = new Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext();
			this.m_inSubreport = false;
			this.m_inSubreportInDataRegion = false;
			this.m_stateManager = new OnDemandStateManagerFull(this);
			this.m_reportObjectModel = new ObjectModelImpl(this);
			this.m_reportObjectModel.Initialize(dataSetDefinition);
			IExpressionHostAssemblyHolder dataSetCore = dataSetDefinition.DataSetCore;
			this.m_reportRuntime = new Microsoft.ReportingServices.RdlExpressions.ReportRuntime(dataSetCore.ObjectType, this.m_reportObjectModel, this.ErrorContext);
			this.m_reportRuntime.LoadCompiledCode(dataSetCore, true, true, this.m_reportObjectModel, this.ReportRuntimeSetup);
		}

		// Token: 0x06007379 RID: 29561 RVA: 0x001DFA98 File Offset: 0x001DDC98
		internal OnDemandProcessingContext(OnDemandProcessingContext aContext, ICatalogItemContext reportContext, Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport)
		{
			this.m_parentContext = aContext;
			this.m_snapshotProcessing = aContext.SnapshotProcessing;
			this.m_reportDefinition = subReport.Report;
			this.m_odpMetadata = aContext.OdpMetadata;
			this.m_inSubreport = true;
			this.m_inSubreportInDataRegion = aContext.InSubreportInDataRegion | subReport.InDataRegion;
			this.m_processReportParameters = false;
			this.m_isSharedDataSetExecutionOnly = false;
			this.m_initializedRuntime = false;
			this.m_catalogItemContext = reportContext;
			this.m_externalDataSetContext = null;
			this.m_errorContext = new ProcessingErrorContext();
			if (subReport.Report != null)
			{
				this.m_subReportInfo = this.m_odpMetadata.GetSubReportInfo(aContext.InSubreport, subReport.SubReportDefinitionPath, subReport.ReportName);
				int lastID = this.m_subReportInfo.LastID;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report = subReport.Report;
			this.m_commonInfo = aContext.m_commonInfo;
			this.m_stateManager = this.CreateStateManager(this.m_commonInfo.ContextMode);
			this.m_subReportInstanceOrSharedDatasetUniqueName = null;
			this.m_reportItemThisDotValueReferenced = aContext.m_reportItemThisDotValueReferenced;
			if (this.m_commonInfo.ContextMode != OnDemandProcessingContext.Mode.DefinitionOnly)
			{
				if (report != null)
				{
					this.m_reportItemsReferenced = report.HasReportItemReferences;
					this.m_embeddedImages = report.EmbeddedImages;
					this.InitializeDataSetMembers(report.MappingNameToDataSet.Count);
				}
				else
				{
					this.m_reportItemsReferenced = false;
					this.m_embeddedImages = null;
					this.InitializeDataSetMembers(-1);
				}
				this.m_reportObjectModel = new ObjectModelImpl(this);
				this.m_reportRuntime = null;
				this.m_userSortFilterContext = new Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext(aContext.UserSortFilterContext, subReport);
			}
			this.m_compareInfo = aContext.m_compareInfo;
			this.m_clrCompareOptions = aContext.m_clrCompareOptions;
			this.m_threadCulture = aContext.m_threadCulture;
			this.InitFlags(report);
			this.m_odpMetadata.OdpContexts.Add(this);
		}

		// Token: 0x0600737A RID: 29562 RVA: 0x001DFC84 File Offset: 0x001DDE84
		private OnDemandStateManager CreateStateManager(OnDemandProcessingContext.Mode contextMode)
		{
			switch (contextMode)
			{
			case OnDemandProcessingContext.Mode.Full:
				return new OnDemandStateManagerFull(this);
			case OnDemandProcessingContext.Mode.Streaming:
				return new OnDemandStateManagerStreaming(this);
			case OnDemandProcessingContext.Mode.DefinitionOnly:
				return new OnDemandStateManagerDefinitionOnly(this);
			default:
				Global.Tracer.Assert(false, "CreateStateManager: invalid contextMode.");
				throw new InvalidOperationException("CreateStateManager: invalid contextMode.");
			}
		}

		// Token: 0x0600737B RID: 29563 RVA: 0x001DFCD4 File Offset: 0x001DDED4
		private static ImageCacheManager CreateImageCacheManager(OnDemandProcessingContext.Mode contextMode, OnDemandMetadata odpMetadata, IChunkFactory chunkFactory)
		{
			switch (contextMode)
			{
			case OnDemandProcessingContext.Mode.Full:
			case OnDemandProcessingContext.Mode.DefinitionOnly:
				return new SnapshotImageCacheManager(odpMetadata, chunkFactory);
			case OnDemandProcessingContext.Mode.Streaming:
				return new StreamingImageCacheManager(odpMetadata, chunkFactory);
			default:
				Global.Tracer.Assert(false, "CreateImageCacheManager: invalid contextMode.");
				throw new InvalidOperationException("CreateImageCacheManager: invalid contextMode.");
			}
		}

		// Token: 0x17002702 RID: 9986
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x001DFD14 File Offset: 0x001DDF14
		internal UserProfileState HasUserProfileState
		{
			get
			{
				return this.m_commonInfo.HasUserProfileState;
			}
		}

		// Token: 0x0600737D RID: 29565 RVA: 0x001DFD21 File Offset: 0x001DDF21
		internal void MergeHasUserProfileState(UserProfileState newProfileStateFlags)
		{
			this.m_commonInfo.MergeHasUserProfileState(newProfileStateFlags);
		}

		// Token: 0x17002703 RID: 9987
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x001DFD2F File Offset: 0x001DDF2F
		// (set) Token: 0x0600737F RID: 29567 RVA: 0x001DFD3C File Offset: 0x001DDF3C
		internal bool HasRenderFormatDependencyInDocumentMap
		{
			get
			{
				return this.m_commonInfo.HasRenderFormatDependencyInDocumentMap;
			}
			set
			{
				this.m_commonInfo.HasRenderFormatDependencyInDocumentMap = value;
			}
		}

		// Token: 0x17002704 RID: 9988
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x001DFD4A File Offset: 0x001DDF4A
		internal ExecutionLogContext ExecutionLogContext
		{
			get
			{
				return this.m_commonInfo.ExecutionLogContext;
			}
		}

		// Token: 0x17002705 RID: 9989
		// (get) Token: 0x06007381 RID: 29569 RVA: 0x001DFD57 File Offset: 0x001DDF57
		internal IJobContext JobContext
		{
			get
			{
				return this.m_commonInfo.JobContext;
			}
		}

		// Token: 0x17002706 RID: 9990
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x001DFD64 File Offset: 0x001DDF64
		internal IExtensionFactory ExtFactory
		{
			get
			{
				return this.m_commonInfo.ExtFactory;
			}
		}

		// Token: 0x17002707 RID: 9991
		// (get) Token: 0x06007383 RID: 29571 RVA: 0x001DFD71 File Offset: 0x001DDF71
		internal IDataProtection DataProtection
		{
			get
			{
				return this.m_commonInfo.DataProtection;
			}
		}

		// Token: 0x17002708 RID: 9992
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x001DFD7E File Offset: 0x001DDF7E
		public bool EnableDataBackedParameters
		{
			get
			{
				return this.m_commonInfo.EnableDataBackedParameters;
			}
		}

		// Token: 0x17002709 RID: 9993
		// (get) Token: 0x06007385 RID: 29573 RVA: 0x001DFD8B File Offset: 0x001DDF8B
		internal IChunkFactory ChunkFactory
		{
			get
			{
				return this.m_commonInfo.ChunkFactory;
			}
		}

		// Token: 0x1700270A RID: 9994
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x001DFD98 File Offset: 0x001DDF98
		internal string RequestUserName
		{
			get
			{
				return this.m_commonInfo.RequestUserName;
			}
		}

		// Token: 0x1700270B RID: 9995
		// (get) Token: 0x06007387 RID: 29575 RVA: 0x001DFDA5 File Offset: 0x001DDFA5
		public DateTime ExecutionTime
		{
			get
			{
				return this.m_commonInfo.ExecutionTime;
			}
		}

		// Token: 0x1700270C RID: 9996
		// (get) Token: 0x06007388 RID: 29576 RVA: 0x001DFDB2 File Offset: 0x001DDFB2
		internal CultureInfo UserLanguage
		{
			get
			{
				return this.m_commonInfo.UserLanguage;
			}
		}

		// Token: 0x1700270D RID: 9997
		// (get) Token: 0x06007389 RID: 29577 RVA: 0x001DFDBF File Offset: 0x001DDFBF
		internal ReportProcessing.OnDemandSubReportCallback SubReportCallback
		{
			get
			{
				return this.m_commonInfo.SubReportCallback;
			}
		}

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x0600738A RID: 29578 RVA: 0x001DFDCC File Offset: 0x001DDFCC
		// (set) Token: 0x0600738B RID: 29579 RVA: 0x001DFDD4 File Offset: 0x001DDFD4
		internal bool HasBookmarks
		{
			get
			{
				return this.m_hasBookmarks;
			}
			set
			{
				if (this.m_parentContext != null)
				{
					this.m_parentContext.HasBookmarks = value;
				}
				else if (!this.SnapshotProcessing || this.m_commonInfo.ReprocessSnapshot)
				{
					this.m_odpMetadata.ReportSnapshot.HasBookmarks = value;
				}
				this.m_hasBookmarks = this.m_hasBookmarks || value;
			}
		}

		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x0600738C RID: 29580 RVA: 0x001DFE39 File Offset: 0x001DE039
		// (set) Token: 0x0600738D RID: 29581 RVA: 0x001DFE44 File Offset: 0x001DE044
		internal bool HasShowHide
		{
			get
			{
				return this.m_hasShowHide;
			}
			set
			{
				if (this.m_parentContext != null)
				{
					this.m_parentContext.HasShowHide = value;
				}
				else if (!this.SnapshotProcessing || this.m_commonInfo.ReprocessSnapshot)
				{
					this.m_odpMetadata.ReportSnapshot.HasShowHide = value;
				}
				this.m_hasShowHide = this.m_hasShowHide || value;
			}
		}

		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x0600738E RID: 29582 RVA: 0x001DFEA9 File Offset: 0x001DE0A9
		internal bool HasUserSortFilter
		{
			get
			{
				return this.m_reportDefinition != null && this.m_reportDefinition.ReportOrDescendentHasUserSortFilter;
			}
		}

		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x0600738F RID: 29583 RVA: 0x001DFEC0 File Offset: 0x001DE0C0
		internal UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_commonInfo.AllowUserProfileState;
			}
		}

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x06007390 RID: 29584 RVA: 0x001DFECD File Offset: 0x001DE0CD
		// (set) Token: 0x06007391 RID: 29585 RVA: 0x001DFED5 File Offset: 0x001DE0D5
		public bool SnapshotProcessing
		{
			get
			{
				return this.m_snapshotProcessing;
			}
			set
			{
				this.m_snapshotProcessing = value;
			}
		}

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x06007392 RID: 29586 RVA: 0x001DFEDE File Offset: 0x001DE0DE
		public bool ReprocessSnapshot
		{
			get
			{
				return this.m_commonInfo.ReprocessSnapshot;
			}
		}

		// Token: 0x17002714 RID: 10004
		// (get) Token: 0x06007393 RID: 29587 RVA: 0x001DFEEB File Offset: 0x001DE0EB
		internal bool ProcessWithCachedData
		{
			get
			{
				return this.m_commonInfo.ProcessWithCachedData;
			}
		}

		// Token: 0x17002715 RID: 10005
		// (get) Token: 0x06007394 RID: 29588 RVA: 0x001DFEF8 File Offset: 0x001DE0F8
		internal bool StreamingMode
		{
			get
			{
				return this.m_commonInfo.StreamingMode;
			}
		}

		// Token: 0x17002716 RID: 10006
		// (get) Token: 0x06007395 RID: 29589 RVA: 0x001DFF05 File Offset: 0x001DE105
		internal bool UseVerboseExecutionLogging
		{
			get
			{
				return this.JobContext != null && this.JobContext.ExecutionLogLevel == ExecutionLogLevel.Verbose && this.m_commonInfo.StreamingMode;
			}
		}

		// Token: 0x17002717 RID: 10007
		// (get) Token: 0x06007396 RID: 29590 RVA: 0x001DFF2A File Offset: 0x001DE12A
		internal bool ShouldExecuteLiveQueries
		{
			get
			{
				return this.StreamingMode || (!this.SnapshotProcessing && !this.ReprocessSnapshot);
			}
		}

		// Token: 0x17002718 RID: 10008
		// (get) Token: 0x06007397 RID: 29591 RVA: 0x001DFF49 File Offset: 0x001DE149
		internal ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_commonInfo.ReportRuntimeSetup;
			}
		}

		// Token: 0x17002719 RID: 10009
		// (get) Token: 0x06007398 RID: 29592 RVA: 0x001DFF56 File Offset: 0x001DE156
		internal ReportProcessing.StoreServerParameters StoreServerParameters
		{
			get
			{
				return this.m_commonInfo.StoreServerParameters;
			}
		}

		// Token: 0x1700271A RID: 10010
		// (get) Token: 0x06007399 RID: 29593 RVA: 0x001DFF63 File Offset: 0x001DE163
		internal bool HasPreviousAggregates
		{
			get
			{
				return this.m_reportDefinition != null && this.m_reportDefinition.HasPreviousAggregates;
			}
		}

		// Token: 0x1700271B RID: 10011
		// (get) Token: 0x0600739A RID: 29594 RVA: 0x001DFF7A File Offset: 0x001DE17A
		// (set) Token: 0x0600739B RID: 29595 RVA: 0x001DFF87 File Offset: 0x001DE187
		internal EventInformation UserSortFilterInfo
		{
			get
			{
				return this.m_commonInfo.UserSortFilterInfo;
			}
			set
			{
				this.m_commonInfo.UserSortFilterInfo = value;
			}
		}

		// Token: 0x1700271C RID: 10012
		// (get) Token: 0x0600739C RID: 29596 RVA: 0x001DFF95 File Offset: 0x001DE195
		// (set) Token: 0x0600739D RID: 29597 RVA: 0x001DFFA2 File Offset: 0x001DE1A2
		internal SortFilterEventInfoMap OldSortFilterEventInfo
		{
			get
			{
				return this.m_commonInfo.OldSortFilterEventInfo;
			}
			set
			{
				this.m_commonInfo.OldSortFilterEventInfo = value;
			}
		}

		// Token: 0x1700271D RID: 10013
		// (get) Token: 0x0600739E RID: 29598 RVA: 0x001DFFB0 File Offset: 0x001DE1B0
		// (set) Token: 0x0600739F RID: 29599 RVA: 0x001DFFBD File Offset: 0x001DE1BD
		internal string UserSortFilterEventSourceUniqueName
		{
			get
			{
				return this.m_commonInfo.UserSortFilterEventSourceUniqueName;
			}
			set
			{
				this.m_commonInfo.UserSortFilterEventSourceUniqueName = value;
			}
		}

		// Token: 0x1700271E RID: 10014
		// (get) Token: 0x060073A0 RID: 29600 RVA: 0x001DFFCB File Offset: 0x001DE1CB
		// (set) Token: 0x060073A1 RID: 29601 RVA: 0x001DFFD8 File Offset: 0x001DE1D8
		internal SortFilterEventInfoMap NewSortFilterEventInfo
		{
			get
			{
				return this.m_commonInfo.NewSortFilterEventInfo;
			}
			set
			{
				this.m_commonInfo.NewSortFilterEventInfo = value;
			}
		}

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x060073A2 RID: 29602 RVA: 0x001DFFE6 File Offset: 0x001DE1E6
		// (set) Token: 0x060073A3 RID: 29603 RVA: 0x001DFFF3 File Offset: 0x001DE1F3
		internal List<IReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo>> ReportRuntimeUserSortFilterInfo
		{
			get
			{
				return this.m_commonInfo.ReportRuntimeUserSortFilterInfo;
			}
			set
			{
				this.m_commonInfo.ReportRuntimeUserSortFilterInfo = value;
			}
		}

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x060073A4 RID: 29604 RVA: 0x001E0001 File Offset: 0x001DE201
		internal CreateAndRegisterStream CreateStreamCallback
		{
			get
			{
				return this.m_commonInfo.CreateStreamCallback;
			}
		}

		// Token: 0x17002721 RID: 10017
		// (get) Token: 0x060073A5 RID: 29605 RVA: 0x001E000E File Offset: 0x001DE20E
		internal bool IsPageHeaderFooter
		{
			get
			{
				return this.m_isPageHeaderFooter;
			}
		}

		// Token: 0x17002722 RID: 10018
		// (get) Token: 0x060073A6 RID: 29606 RVA: 0x001E0016 File Offset: 0x001DE216
		internal Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo> ReportAggregates
		{
			get
			{
				return this.m_reportAggregates;
			}
		}

		// Token: 0x17002723 RID: 10019
		// (get) Token: 0x060073A7 RID: 29607 RVA: 0x001E001E File Offset: 0x001DE21E
		internal OnDemandStateManager StateManager
		{
			get
			{
				return this.m_stateManager;
			}
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x001E0026 File Offset: 0x001DE226
		internal void CreatedScopeInstance(IRIFReportDataScope scope)
		{
			this.m_stateManager.CreatedScopeInstance(scope);
		}

		// Token: 0x17002724 RID: 10020
		// (get) Token: 0x060073A9 RID: 29609 RVA: 0x001E0034 File Offset: 0x001DE234
		public bool UsePreviewCommands
		{
			get
			{
				return this.m_commonInfo.UsePreviewCommands;
			}
		}

		// Token: 0x17002725 RID: 10021
		// (get) Token: 0x060073AA RID: 29610 RVA: 0x001E0041 File Offset: 0x001DE241
		public bool CompareSafeExpressionsToLegacy
		{
			get
			{
				return this.m_commonInfo.CompareSafeExpressionsToLegacy;
			}
		}

		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x060073AB RID: 29611 RVA: 0x001E004E File Offset: 0x001DE24E
		public bool UseUserLanguageForProcessing
		{
			get
			{
				return this.m_commonInfo.UseUserLanguageForProcessing;
			}
		}

		// Token: 0x17002727 RID: 10023
		// (get) Token: 0x060073AC RID: 29612 RVA: 0x001E005B File Offset: 0x001DE25B
		internal ReportProcessingContext ExternalProcessingContext
		{
			get
			{
				return this.m_commonInfo.ExternalProcessingContext;
			}
		}

		// Token: 0x17002728 RID: 10024
		// (get) Token: 0x060073AD RID: 29613 RVA: 0x001E0068 File Offset: 0x001DE268
		internal DataSetContext ExternalDataSetContext
		{
			get
			{
				return this.m_externalDataSetContext;
			}
		}

		// Token: 0x17002729 RID: 10025
		// (get) Token: 0x060073AE RID: 29614 RVA: 0x001E0070 File Offset: 0x001DE270
		// (set) Token: 0x060073AF RID: 29615 RVA: 0x001E0078 File Offset: 0x001DE278
		internal bool ErrorSavingSnapshotData
		{
			get
			{
				return this.m_errorSavingSnapshotData;
			}
			set
			{
				this.m_errorSavingSnapshotData = value;
			}
		}

		// Token: 0x1700272A RID: 10026
		// (get) Token: 0x060073B0 RID: 29616 RVA: 0x001E0081 File Offset: 0x001DE281
		internal OnDemandProcessingContext ParentContext
		{
			get
			{
				return this.m_parentContext;
			}
		}

		// Token: 0x1700272B RID: 10027
		// (get) Token: 0x060073B1 RID: 29617 RVA: 0x001E0089 File Offset: 0x001DE289
		internal OnDemandProcessingContext TopLevelContext
		{
			get
			{
				return this.m_commonInfo.TopLevelContext;
			}
		}

		// Token: 0x1700272C RID: 10028
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x001E0096 File Offset: 0x001DE296
		internal RuntimeDataSourceInfoCollection DataSourceInfos
		{
			get
			{
				return this.m_commonInfo.DataSourceInfos;
			}
		}

		// Token: 0x1700272D RID: 10029
		// (get) Token: 0x060073B3 RID: 29619 RVA: 0x001E00A3 File Offset: 0x001DE2A3
		internal RuntimeDataSetInfoCollection SharedDataSetReferences
		{
			get
			{
				return this.m_commonInfo.SharedDataSetReferences;
			}
		}

		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x060073B4 RID: 29620 RVA: 0x001E00B0 File Offset: 0x001DE2B0
		internal IProcessingDataExtensionConnection CreateAndSetupDataExtensionFunction
		{
			get
			{
				return this.m_commonInfo.CreateAndSetupDataExtensionFunction;
			}
		}

		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x060073B5 RID: 29621 RVA: 0x001E00BD File Offset: 0x001DE2BD
		// (set) Token: 0x060073B6 RID: 29622 RVA: 0x001E00C5 File Offset: 0x001DE2C5
		internal CultureInfo ThreadCulture
		{
			get
			{
				return this.m_threadCulture;
			}
			set
			{
				this.m_threadCulture = value;
			}
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x001E00CE File Offset: 0x001DE2CE
		internal void EnsureCultureIsSetOnCurrentThread()
		{
			if (this.m_threadCulture != null && Thread.CurrentThread.CurrentCulture.LCID != this.m_threadCulture.LCID)
			{
				Thread.CurrentThread.CurrentCulture = this.m_threadCulture;
			}
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x060073B8 RID: 29624 RVA: 0x001E0104 File Offset: 0x001DE304
		// (set) Token: 0x060073B9 RID: 29625 RVA: 0x001E0111 File Offset: 0x001DE311
		internal uint LanguageInstanceId
		{
			get
			{
				return this.m_commonInfo.LanguageInstanceId;
			}
			set
			{
				this.m_commonInfo.LanguageInstanceId = value;
			}
		}

		// Token: 0x17002731 RID: 10033
		// (get) Token: 0x060073BA RID: 29626 RVA: 0x001E011F File Offset: 0x001DE31F
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_catalogItemContext;
			}
		}

		// Token: 0x17002732 RID: 10034
		// (get) Token: 0x060073BB RID: 29627 RVA: 0x001E0127 File Offset: 0x001DE327
		// (set) Token: 0x060073BC RID: 29628 RVA: 0x001E012F File Offset: 0x001DE32F
		internal Microsoft.ReportingServices.RdlExpressions.ReportRuntime ReportRuntime
		{
			get
			{
				return this.m_reportRuntime;
			}
			set
			{
				this.m_reportRuntime = value;
			}
		}

		// Token: 0x17002733 RID: 10035
		// (get) Token: 0x060073BD RID: 29629 RVA: 0x001E0138 File Offset: 0x001DE338
		internal ObjectModelImpl ReportObjectModel
		{
			get
			{
				return this.m_reportObjectModel;
			}
		}

		// Token: 0x17002734 RID: 10036
		// (get) Token: 0x060073BE RID: 29630 RVA: 0x001E0140 File Offset: 0x001DE340
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report ReportDefinition
		{
			get
			{
				return this.m_reportDefinition;
			}
		}

		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x060073BF RID: 29631 RVA: 0x001E0148 File Offset: 0x001DE348
		internal OnDemandMetadata OdpMetadata
		{
			get
			{
				return this.m_odpMetadata;
			}
		}

		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x060073C0 RID: 29632 RVA: 0x001E0150 File Offset: 0x001DE350
		// (set) Token: 0x060073C1 RID: 29633 RVA: 0x001E0158 File Offset: 0x001DE358
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance CurrentReportInstance
		{
			get
			{
				return this.m_currentReportInstance;
			}
			set
			{
				this.m_currentReportInstance = value;
			}
		}

		// Token: 0x17002737 RID: 10039
		// (get) Token: 0x060073C2 RID: 29634 RVA: 0x001E0161 File Offset: 0x001DE361
		internal int CurrentDataSetIndex
		{
			get
			{
				if (this.m_reportObjectModel.CurrentFields == null || this.m_reportObjectModel.CurrentFields.DataSet == null)
				{
					return -1;
				}
				return this.m_currentDataSetIndex;
			}
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x001E018A File Offset: 0x001DE38A
		internal void SetupEnvironment(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance)
		{
			this.m_currentDataSetIndex = -1;
			this.m_currentReportInstance = reportInstance;
			reportInstance.SetupEnvironment(this);
		}

		// Token: 0x17002738 RID: 10040
		// (get) Token: 0x060073C4 RID: 29636 RVA: 0x001E01A1 File Offset: 0x001DE3A1
		internal ImageCacheManager ImageCacheManager
		{
			get
			{
				return this.m_commonInfo.ImageCacheManager;
			}
		}

		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x060073C5 RID: 29637 RVA: 0x001E01AE File Offset: 0x001DE3AE
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance CurrentOdpDataSetInstance
		{
			get
			{
				return this.m_currentDataSetInstance;
			}
		}

		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x060073C6 RID: 29638 RVA: 0x001E01B8 File Offset: 0x001DE3B8
		internal IReportScope CurrentReportScope
		{
			get
			{
				IReportScopeInstance lastROMInstance = this.m_stateManager.LastROMInstance;
				if (lastROMInstance == null)
				{
					return null;
				}
				return lastROMInstance.ReportScope;
			}
		}

		// Token: 0x1700273B RID: 10043
		// (get) Token: 0x060073C7 RID: 29639 RVA: 0x001E01DC File Offset: 0x001DE3DC
		internal List<object> GroupExpressionValues
		{
			get
			{
				return this.m_groupExprValues;
			}
		}

		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x060073C8 RID: 29640 RVA: 0x001E01E4 File Offset: 0x001DE3E4
		// (set) Token: 0x060073C9 RID: 29641 RVA: 0x001E01EC File Offset: 0x001DE3EC
		internal bool PeerOuterGroupProcessing
		{
			get
			{
				return this.m_peerOuterGroupProcessing;
			}
			set
			{
				this.m_peerOuterGroupProcessing = value;
			}
		}

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x060073CA RID: 29642 RVA: 0x001E01F5 File Offset: 0x001DE3F5
		internal bool ReportItemsReferenced
		{
			get
			{
				return this.m_reportItemsReferenced;
			}
		}

		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x060073CB RID: 29643 RVA: 0x001E01FD File Offset: 0x001DE3FD
		internal bool ReportItemThisDotValueReferenced
		{
			get
			{
				return this.m_reportItemThisDotValueReferenced;
			}
		}

		// Token: 0x1700273F RID: 10047
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x001E0205 File Offset: 0x001DE405
		internal ReportProcessing.DataSourceInfoHashtable GlobalDataSourceInfo
		{
			get
			{
				return this.m_commonInfo.GlobalDataSourceInfo;
			}
		}

		// Token: 0x17002740 RID: 10048
		// (get) Token: 0x060073CD RID: 29645 RVA: 0x001E0212 File Offset: 0x001DE412
		internal Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
		}

		// Token: 0x17002741 RID: 10049
		// (get) Token: 0x060073CE RID: 29646 RVA: 0x001E021A File Offset: 0x001DE41A
		public ErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x17002742 RID: 10050
		// (get) Token: 0x060073CF RID: 29647 RVA: 0x001E0222 File Offset: 0x001DE422
		// (set) Token: 0x060073D0 RID: 29648 RVA: 0x001E022A File Offset: 0x001DE42A
		internal bool ProcessReportParameters
		{
			get
			{
				return this.m_processReportParameters;
			}
			set
			{
				this.m_processReportParameters = value;
			}
		}

		// Token: 0x17002743 RID: 10051
		// (get) Token: 0x060073D1 RID: 29649 RVA: 0x001E0233 File Offset: 0x001DE433
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.m_compareInfo;
			}
		}

		// Token: 0x17002744 RID: 10052
		// (get) Token: 0x060073D2 RID: 29650 RVA: 0x001E023B File Offset: 0x001DE43B
		// (set) Token: 0x060073D3 RID: 29651 RVA: 0x001E0243 File Offset: 0x001DE443
		internal CompareOptions ClrCompareOptions
		{
			get
			{
				return this.m_clrCompareOptions;
			}
			set
			{
				this.SetComparisonInformation(this.m_compareInfo, value, this.m_nullsAsBlanks, this.m_useOrdinalStringKeyGeneration);
			}
		}

		// Token: 0x17002745 RID: 10053
		// (get) Token: 0x060073D4 RID: 29652 RVA: 0x001E025E File Offset: 0x001DE45E
		internal bool NullsAsBlanks
		{
			get
			{
				return this.m_nullsAsBlanks;
			}
		}

		// Token: 0x17002746 RID: 10054
		// (get) Token: 0x060073D5 RID: 29653 RVA: 0x001E0266 File Offset: 0x001DE466
		internal bool UseOrdinalStringKeyGeneration
		{
			get
			{
				return this.m_useOrdinalStringKeyGeneration;
			}
		}

		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x060073D6 RID: 29654 RVA: 0x001E0270 File Offset: 0x001DE470
		internal IDataComparer ProcessingComparer
		{
			get
			{
				if (this.m_processingComparer == null)
				{
					if (this.m_commonInfo.StreamingMode)
					{
						this.m_processingComparer = new CommonDataComparer(this.m_compareInfo, this.m_clrCompareOptions, this.m_nullsAsBlanks);
					}
					else
					{
						this.m_processingComparer = new ReportProcessing.ProcessingComparer(this.m_compareInfo, this.m_clrCompareOptions, this.m_nullsAsBlanks);
					}
				}
				return this.m_processingComparer;
			}
		}

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x060073D7 RID: 29655 RVA: 0x001E02D4 File Offset: 0x001DE4D4
		internal StringKeyGenerator StringKeyGenerator
		{
			get
			{
				if (this.m_stringKeyGenerator == null)
				{
					this.m_stringKeyGenerator = new StringKeyGenerator(this.m_compareInfo, this.m_clrCompareOptions, this.m_nullsAsBlanks, this.m_useOrdinalStringKeyGeneration);
				}
				return this.m_stringKeyGenerator;
			}
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x060073D8 RID: 29656 RVA: 0x001E0307 File Offset: 0x001DE507
		internal IEqualityComparer<object> EqualityComparer
		{
			get
			{
				return this.ProcessingComparer;
			}
		}

		// Token: 0x060073D9 RID: 29657 RVA: 0x001E030F File Offset: 0x001DE50F
		internal void SetComparisonInformation(DataSetCore dataSet)
		{
			this.SetComparisonInformation(dataSet.CreateCultureInfoFromLcid().CompareInfo, dataSet.GetCLRCompareOptions(), dataSet.NullsAsBlanks, dataSet.UseOrdinalStringKeyGeneration);
		}

		// Token: 0x060073DA RID: 29658 RVA: 0x001E0334 File Offset: 0x001DE534
		internal void SetComparisonInformation(CompareInfo compareInfo, CompareOptions clrCompareOptions, bool nullsAsBlanks, bool useOrdinalStringKeyGeneration)
		{
			this.m_compareInfo = compareInfo;
			this.m_clrCompareOptions = clrCompareOptions;
			this.m_nullsAsBlanks = nullsAsBlanks;
			this.m_useOrdinalStringKeyGeneration = useOrdinalStringKeyGeneration;
			this.m_processingComparer = null;
			this.m_stringKeyGenerator = null;
		}

		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x060073DB RID: 29659 RVA: 0x001E0361 File Offset: 0x001DE561
		internal string SubReportDataChunkNameModifier
		{
			get
			{
				return this.m_subReportDataChunkNameModifier;
			}
		}

		// Token: 0x1700274B RID: 10059
		// (get) Token: 0x060073DC RID: 29660 RVA: 0x001E0369 File Offset: 0x001DE569
		internal string ProcessingAbortItemUniqueIdentifier
		{
			get
			{
				if (this.m_inSubreport || this.m_isSharedDataSetExecutionOnly)
				{
					return this.m_subReportInstanceOrSharedDatasetUniqueName;
				}
				return null;
			}
		}

		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x060073DD RID: 29661 RVA: 0x001E0383 File Offset: 0x001DE583
		internal bool FoundExistingSubReportInstance
		{
			get
			{
				return this.m_foundExistingSubReportInstance;
			}
		}

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x060073DE RID: 29662 RVA: 0x001E038B File Offset: 0x001DE58B
		internal string SubReportUniqueName
		{
			get
			{
				if (!this.m_inSubreport || this.m_subReportInfo == null)
				{
					return null;
				}
				return this.m_subReportInfo.UniqueName;
			}
		}

		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x060073DF RID: 29663 RVA: 0x001E03AC File Offset: 0x001DE5AC
		internal string ReportFolder
		{
			get
			{
				if (this.m_inSubreport)
				{
					string text = this.m_subReportInfo.CommonSubReportInfo.OriginalCatalogPath;
					if (!string.IsNullOrEmpty(text))
					{
						int num = text.LastIndexOf('/');
						if (num > 0)
						{
							return text.Substring(0, num);
						}
					}
					else
					{
						text = "/";
					}
					return text;
				}
				return this.m_catalogItemContext.ParentPath;
			}
		}

		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x060073E0 RID: 29664 RVA: 0x001E0403 File Offset: 0x001DE603
		internal bool InSubreport
		{
			get
			{
				return this.m_inSubreport;
			}
		}

		// Token: 0x17002750 RID: 10064
		// (get) Token: 0x060073E1 RID: 29665 RVA: 0x001E040B File Offset: 0x001DE60B
		internal bool InSubreportInDataRegion
		{
			get
			{
				return this.m_inSubreportInDataRegion;
			}
		}

		// Token: 0x17002751 RID: 10065
		// (get) Token: 0x060073E2 RID: 29666 RVA: 0x001E0413 File Offset: 0x001DE613
		internal AbortHelper AbortInfo
		{
			get
			{
				return this.m_commonInfo.AbortInfo;
			}
		}

		// Token: 0x060073E3 RID: 29667 RVA: 0x001E0420 File Offset: 0x001DE620
		internal void UnregisterAbortInfo()
		{
			this.m_commonInfo.UnregisterAbortInfo();
		}

		// Token: 0x17002752 RID: 10066
		// (get) Token: 0x060073E4 RID: 29668 RVA: 0x001E042D File Offset: 0x001DE62D
		// (set) Token: 0x060073E5 RID: 29669 RVA: 0x001E0435 File Offset: 0x001DE635
		internal SecondPassOperations SecondPassOperation
		{
			get
			{
				return this.m_secondPassOperation;
			}
			set
			{
				this.m_secondPassOperation = value;
			}
		}

		// Token: 0x060073E6 RID: 29670 RVA: 0x001E043E File Offset: 0x001DE63E
		internal bool HasSecondPassOperation(SecondPassOperations op)
		{
			return (this.m_secondPassOperation & op) > SecondPassOperations.None;
		}

		// Token: 0x17002753 RID: 10067
		// (get) Token: 0x060073E7 RID: 29671 RVA: 0x001E044B File Offset: 0x001DE64B
		internal bool SpecialRecursiveAggregates
		{
			get
			{
				return this.m_specialRecursiveAggregates;
			}
		}

		// Token: 0x17002754 RID: 10068
		// (get) Token: 0x060073E8 RID: 29672 RVA: 0x001E0453 File Offset: 0x001DE653
		// (set) Token: 0x060073E9 RID: 29673 RVA: 0x001E045B File Offset: 0x001DE65B
		internal bool InitializedRuntime
		{
			get
			{
				return this.m_initializedRuntime;
			}
			set
			{
				this.m_initializedRuntime = value;
			}
		}

		// Token: 0x17002755 RID: 10069
		// (get) Token: 0x060073EA RID: 29674 RVA: 0x001E0464 File Offset: 0x001DE664
		internal bool[] DataSetRetrievalComplete
		{
			get
			{
				return this.m_dataSetRetrievalComplete;
			}
		}

		// Token: 0x17002756 RID: 10070
		// (get) Token: 0x060073EB RID: 29675 RVA: 0x001E046C File Offset: 0x001DE66C
		// (set) Token: 0x060073EC RID: 29676 RVA: 0x001E0474 File Offset: 0x001DE674
		internal ParameterInfoCollection ReportParameters
		{
			get
			{
				return this.m_reportParameters;
			}
			set
			{
				this.m_reportParameters = value;
			}
		}

		// Token: 0x17002757 RID: 10071
		// (get) Token: 0x060073ED RID: 29677 RVA: 0x001E047D File Offset: 0x001DE67D
		internal IScalabilityCache TablixProcessingScalabilityCache
		{
			get
			{
				return this.m_tablixProcessingScalabilityCache;
			}
		}

		// Token: 0x17002758 RID: 10072
		// (get) Token: 0x060073EE RID: 29678 RVA: 0x001E0485 File Offset: 0x001DE685
		// (set) Token: 0x060073EF RID: 29679 RVA: 0x001E048D File Offset: 0x001DE68D
		internal CommonRowCache TablixProcessingLookupRowCache
		{
			get
			{
				return this.m_tablixProcessingLookupRowCache;
			}
			set
			{
				this.m_tablixProcessingLookupRowCache = value;
			}
		}

		// Token: 0x17002759 RID: 10073
		// (get) Token: 0x060073F0 RID: 29680 RVA: 0x001E0496 File Offset: 0x001DE696
		// (set) Token: 0x060073F1 RID: 29681 RVA: 0x001E04A3 File Offset: 0x001DE6A3
		internal OnDemandProcessingContext.CustomReportItemControls CriProcessingControls
		{
			get
			{
				return this.m_commonInfo.CriProcessingControls;
			}
			set
			{
				this.m_commonInfo.CriProcessingControls = value;
			}
		}

		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x060073F2 RID: 29682 RVA: 0x001E04B1 File Offset: 0x001DE6B1
		internal IConfiguration Configuration
		{
			get
			{
				return this.m_commonInfo.Configuration;
			}
		}

		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x060073F3 RID: 29683 RVA: 0x001E04BE File Offset: 0x001DE6BE
		// (set) Token: 0x060073F4 RID: 29684 RVA: 0x001E04C6 File Offset: 0x001DE6C6
		internal DomainScopeContext DomainScopeContext
		{
			get
			{
				return this.m_domainScopeContext;
			}
			set
			{
				this.m_domainScopeContext = value;
			}
		}

		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x060073F5 RID: 29685 RVA: 0x001E04CF File Offset: 0x001DE6CF
		internal OnDemandProcessingContext.Mode ContextMode
		{
			get
			{
				return this.m_commonInfo.ContextMode;
			}
		}

		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x060073F6 RID: 29686 RVA: 0x001E04DC File Offset: 0x001DE6DC
		internal bool ProhibitSerializableValues
		{
			get
			{
				return this.Configuration != null && this.Configuration.ProhibitSerializableValues;
			}
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x001E04F3 File Offset: 0x001DE6F3
		internal void ResetUserSortFilterContext()
		{
			if (this.m_userSortFilterContext != null)
			{
				this.m_userSortFilterContext.ResetContextForTopLevelDataSet();
			}
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x001E0508 File Offset: 0x001DE708
		internal bool IsRdlSandboxingEnabled()
		{
			return this.Configuration != null && this.Configuration.RdlSandboxing != null;
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x001E0522 File Offset: 0x001DE722
		internal int GetActiveCompatibilityVersion()
		{
			return ReportProcessingCompatibilityVersion.GetCompatibilityVersion(this.Configuration);
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x001E0530 File Offset: 0x001DE730
		private void InitFlags(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			if (report != null)
			{
				if (!this.SnapshotProcessing || this.m_commonInfo.ReprocessSnapshot)
				{
					this.m_odpMetadata.ReportSnapshot.DefinitionTreeHasDocumentMap |= report.HasLabels;
					this.m_odpMetadata.ReportSnapshot.HasDocumentMap |= report.HasLabels;
				}
				this.HasBookmarks = report.HasBookmarks;
				this.HasShowHide = report.ShowHideType == Microsoft.ReportingServices.ReportIntermediateFormat.Report.ShowHideTypes.Interactive;
			}
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x001E05AA File Offset: 0x001DE7AA
		internal void InitializeDataSetMembers(int dataSetCount)
		{
			if (dataSetCount >= 0)
			{
				this.m_dataSetRetrievalComplete = new bool[dataSetCount];
				return;
			}
			this.m_dataSetRetrievalComplete = null;
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x001E05C4 File Offset: 0x001DE7C4
		internal void RuntimeInitializePageSectionVariables(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, object[] reportVariableValues)
		{
			if (report.Variables != null)
			{
				this.AddVariablesToReportObjectModel(report.Variables, null, report.ObjectType, null, reportVariableValues);
			}
			if (report.GroupsWithVariables != null)
			{
				int count = report.GroupsWithVariables.Count;
				for (int i = 0; i < count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = report.GroupsWithVariables[i].Grouping;
					this.AddVariablesToReportObjectModel(grouping.Variables, null, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, grouping.Name, null);
				}
			}
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x001E0638 File Offset: 0x001DE838
		internal void RuntimeInitializeVariables(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			if (report.Variables != null)
			{
				this.AddVariablesToReportObjectModel(report.Variables, (report.ReportExprHost == null) ? null : report.ReportExprHost.VariableValueHosts, report.ObjectType, report.Name, null);
			}
			if (report.GroupsWithVariables != null)
			{
				int count = report.GroupsWithVariables.Count;
				for (int i = 0; i < count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = report.GroupsWithVariables[i].Grouping;
					this.AddVariablesToReportObjectModel(grouping.Variables, (grouping.ExprHost == null) ? null : grouping.ExprHost.VariableValueHosts, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, grouping.Name, null);
				}
			}
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x001E06DC File Offset: 0x001DE8DC
		private void AddVariablesToReportObjectModel(List<Microsoft.ReportingServices.ReportIntermediateFormat.Variable> variableDef, IndexedExprHost variableValuesHost, Microsoft.ReportingServices.ReportProcessing.ObjectType parentObjectType, string parentObjectName, object[] variableValues)
		{
			if (variableDef == null)
			{
				return;
			}
			int count = variableDef.Count;
			for (int i = 0; i < count; i++)
			{
				VariableImpl variableImpl = new VariableImpl(variableDef[i], variableValuesHost, parentObjectType, parentObjectName, this.m_reportRuntime, i);
				if (variableValues != null)
				{
					variableImpl.SetValue(variableValues[i], true);
				}
				this.m_reportObjectModel.VariablesImpl.Add(variableImpl);
			}
		}

		// Token: 0x060073FF RID: 29695 RVA: 0x001E0738 File Offset: 0x001DE938
		internal void RuntimeInitializeLookups(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			if (report.DataSources != null)
			{
				for (int i = 0; i < report.DataSources.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource = report.DataSources[i];
					if (dataSource.DataSets != null)
					{
						for (int j = 0; j < dataSource.DataSets.Count; j++)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = dataSource.DataSets[j];
							if (dataSet.Lookups != null)
							{
								for (int k = 0; k < dataSet.Lookups.Count; k++)
								{
									LookupImpl lookupImpl = new LookupImpl(dataSet.Lookups[k], this.m_reportRuntime);
									this.m_reportObjectModel.LookupsImpl.Add(lookupImpl);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007400 RID: 29696 RVA: 0x001E07F4 File Offset: 0x001DE9F4
		internal void RuntimeInitializeTextboxObjs(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection reportItems, bool setExprHost)
		{
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.RuntimeInitializeTextboxObjs(reportItems[i], setExprHost);
			}
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x001E0820 File Offset: 0x001DEA20
		internal void RuntimeInitializeTextboxObjs(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem, bool setExprHost)
		{
			if (setExprHost && this.m_reportRuntime.ReportExprHost != null)
			{
				reportItem.SetExprHost(this.m_reportRuntime.ReportExprHost, this.m_reportObjectModel);
			}
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = reportItem.ObjectType;
			if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.Textbox)
			{
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
				{
					this.RuntimeInitializeTextboxObjs(((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)reportItem).ReportItems, setExprHost);
					return;
				}
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Textbox)
				{
					return;
				}
				Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)reportItem;
				TextBoxImpl textBoxImpl = new TextBoxImpl(textBox, this.m_reportRuntime, this.m_reportRuntime);
				if (setExprHost)
				{
					if (textBox.ValueReferenced)
					{
						Global.Tracer.Assert(textBox.ExprHost != null, "(textBoxDef.ExprHost != null)");
						this.m_reportItemThisDotValueReferenced = true;
						textBox.TextBoxExprHost.SetTextBox(textBoxImpl);
					}
					if (textBox.TextRunValueReferenced)
					{
						for (int i = 0; i < textBox.Paragraphs.Count; i++)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph = textBox.Paragraphs[i];
							if (paragraph.TextRunValueReferenced)
							{
								for (int j = 0; j < paragraph.TextRuns.Count; j++)
								{
									Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun = paragraph.TextRuns[j];
									if (textRun.ValueReferenced)
									{
										Global.Tracer.Assert(textRun.ExprHost != null);
										this.m_reportItemThisDotValueReferenced = true;
										textRun.ExprHost.SetTextRun(textBoxImpl.Paragraphs[i].TextRuns[j]);
									}
								}
							}
						}
					}
				}
				this.m_reportObjectModel.ReportItemsImpl.Add(textBoxImpl);
			}
			else if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem && objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix = (Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)reportItem;
				if (tablix.Corner != null && tablix.Corner.Count != 0)
				{
					foreach (List<Microsoft.ReportingServices.ReportIntermediateFormat.TablixCornerCell> list in tablix.Corner)
					{
						foreach (Microsoft.ReportingServices.ReportIntermediateFormat.TablixCornerCell tablixCornerCell in list)
						{
							if (tablixCornerCell.CellContents != null)
							{
								this.RuntimeInitializeTextboxObjs(tablixCornerCell.CellContents, setExprHost);
								if (tablixCornerCell.AltCellContents != null)
								{
									this.RuntimeInitializeTextboxObjs(tablixCornerCell.AltCellContents, setExprHost);
								}
							}
						}
					}
				}
				if (tablix.Rows != null && tablix.RowCount != 0)
				{
					for (int k = 0; k < tablix.RowCount; k++)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.TablixRow tablixRow = tablix.TablixRows[k];
						for (int l = 0; l < tablix.ColumnCount; l++)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.TablixCell tablixCell = tablixRow.TablixCells[l];
							if (tablixCell.CellContents != null)
							{
								this.RuntimeInitializeTextboxObjs(tablixCell.CellContents, setExprHost);
								if (tablixCell.AltCellContents != null)
								{
									this.RuntimeInitializeTextboxObjs(tablixCell.AltCellContents, setExprHost);
								}
							}
						}
					}
				}
				this.RuntimeInitializeTextboxObjsInMemberTree(tablix.ColumnMembers, setExprHost);
				this.RuntimeInitializeTextboxObjsInMemberTree(tablix.RowMembers, setExprHost);
				return;
			}
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x001E0B20 File Offset: 0x001DED20
		private void RuntimeInitializeTextboxObjsInMemberTree(HierarchyNodeList memberNodes, bool setExprHost)
		{
			if (memberNodes == null)
			{
				return;
			}
			for (int i = 0; i < memberNodes.Count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.TablixMember tablixMember = (Microsoft.ReportingServices.ReportIntermediateFormat.TablixMember)memberNodes[i];
				if (tablixMember.TablixHeader != null && tablixMember.TablixHeader.CellContents != null)
				{
					this.RuntimeInitializeTextboxObjs(tablixMember.TablixHeader.CellContents, setExprHost);
					if (tablixMember.TablixHeader.AltCellContents != null)
					{
						this.RuntimeInitializeTextboxObjs(tablixMember.TablixHeader.AltCellContents, setExprHost);
					}
				}
				if (tablixMember.InnerHierarchy != null)
				{
					this.RuntimeInitializeTextboxObjsInMemberTree(tablixMember.InnerHierarchy, setExprHost);
				}
			}
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x001E0BAC File Offset: 0x001DEDAC
		internal void RuntimeInitializeReportItemObjs(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection reportItems, bool traverseDataRegions)
		{
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.RuntimeInitializeReportItemObjs(reportItems[i], traverseDataRegions);
			}
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x001E0BD8 File Offset: 0x001DEDD8
		internal void RuntimeInitializeReportItemObjs(List<Microsoft.ReportingServices.ReportIntermediateFormat.MapDataRegion> mapDataRegions, bool traverseDataRegions)
		{
			for (int i = 0; i < mapDataRegions.Count; i++)
			{
				this.RuntimeInitializeReportItemObjs(mapDataRegions[i], traverseDataRegions);
			}
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x001E0C04 File Offset: 0x001DEE04
		internal void RuntimeInitializeReportItemObjs(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem, bool traverseDataRegions)
		{
			if (reportItem.IsDataRegion)
			{
				if (traverseDataRegions)
				{
					if (this.m_reportRuntime.ReportExprHost != null)
					{
						reportItem.SetExprHost(this.m_reportRuntime.ReportExprHost, this.m_reportObjectModel);
					}
					Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = reportItem as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion;
					dataRegion.DataRegionContentsSetExprHost(this.m_reportObjectModel, traverseDataRegions);
					Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = dataRegion.ObjectType;
					if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
					{
						if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
						{
							this.RuntimeInitializeMemberTree(dataRegion.ColumnMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanel)dataRegion).GaugePanelExprHost.MemberTreeHostsRemotable, traverseDataRegions);
							this.RuntimeInitializeMemberTree(dataRegion.RowMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanel)dataRegion).GaugePanelExprHost.MemberTreeHostsRemotable, traverseDataRegions);
							return;
						}
						if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
						{
							return;
						}
						this.RuntimeInitializeMemberTree(dataRegion.ColumnMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.Chart)dataRegion).ChartExprHost.MemberTreeHostsRemotable, traverseDataRegions);
						this.RuntimeInitializeMemberTree(dataRegion.RowMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.Chart)dataRegion).ChartExprHost.MemberTreeHostsRemotable, traverseDataRegions);
						return;
					}
					else
					{
						if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
						{
							this.RuntimeInitializeMemberTree(dataRegion.ColumnMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem)dataRegion).CustomReportItemExprHost.MemberTreeHostsRemotable, traverseDataRegions);
							this.RuntimeInitializeMemberTree(dataRegion.RowMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem)dataRegion).CustomReportItemExprHost.MemberTreeHostsRemotable, traverseDataRegions);
							return;
						}
						if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
						{
							this.RuntimeInitializeMemberTree(dataRegion.ColumnMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)dataRegion).TablixExprHost.MemberTreeHostsRemotable, traverseDataRegions);
							this.RuntimeInitializeMemberTree(dataRegion.RowMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)dataRegion).TablixExprHost.MemberTreeHostsRemotable, traverseDataRegions);
							return;
						}
						if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion)
						{
							return;
						}
						this.RuntimeInitializeMemberTree(dataRegion.ColumnMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.MapDataRegion)dataRegion).MapDataRegionExprHost.MemberTreeHostsRemotable, traverseDataRegions);
						this.RuntimeInitializeMemberTree(dataRegion.RowMembers, (dataRegion.ExprHost == null) ? null : ((Microsoft.ReportingServices.ReportIntermediateFormat.MapDataRegion)dataRegion).MapDataRegionExprHost.MemberTreeHostsRemotable, traverseDataRegions);
						return;
					}
				}
			}
			else
			{
				if (reportItem.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
				{
					this.RuntimeInitializeReportItemObjs(((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)reportItem).ReportItems, traverseDataRegions);
					return;
				}
				if (reportItem.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Map && ((Microsoft.ReportingServices.ReportIntermediateFormat.Map)reportItem).MapDataRegions != null)
				{
					this.RuntimeInitializeReportItemObjs(((Microsoft.ReportingServices.ReportIntermediateFormat.Map)reportItem).MapDataRegions, traverseDataRegions);
				}
			}
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x001E0E64 File Offset: 0x001DF064
		private void RuntimeInitializeMemberTree(HierarchyNodeList memberNodes, IList<IMemberNode> memberExprHosts, bool traverseDataRegions)
		{
			if (memberNodes == null)
			{
				return;
			}
			for (int i = 0; i < memberNodes.Count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode reportHierarchyNode = memberNodes[i];
				IList<IMemberNode> list;
				if (reportHierarchyNode.ExprHostID >= 0 && memberExprHosts != null)
				{
					reportHierarchyNode.SetExprHost(memberExprHosts[reportHierarchyNode.ExprHostID], this.m_reportObjectModel);
					list = memberExprHosts[reportHierarchyNode.ExprHostID].MemberTreeHostsRemotable;
				}
				else
				{
					list = null;
				}
				if (reportHierarchyNode.InnerHierarchy != null && 0 < reportHierarchyNode.InnerHierarchy.Count)
				{
					this.RuntimeInitializeMemberTree(reportHierarchyNode.InnerHierarchy, list, traverseDataRegions);
				}
				reportHierarchyNode.MemberContentsSetExprHost(this.m_reportObjectModel, traverseDataRegions);
			}
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x001E0EFC File Offset: 0x001DF0FC
		internal void RuntimeInitializeAggregates<AggregateType>(List<AggregateType> aggregates) where AggregateType : Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo
		{
			if (aggregates == null)
			{
				return;
			}
			int count = aggregates.Count;
			for (int i = 0; i < count; i++)
			{
				AggregateType aggregateType = aggregates[i];
				if (!this.m_reportAggregates.ContainsKey(aggregateType.Name))
				{
					this.m_reportAggregates.Add(aggregateType.Name, aggregateType);
					if (aggregateType.DuplicateNames != null)
					{
						int count2 = aggregateType.DuplicateNames.Count;
						for (int j = 0; j < count2; j++)
						{
							this.m_reportAggregates.Add(aggregateType.DuplicateNames[j], aggregateType);
						}
					}
				}
			}
		}

		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x06007408 RID: 29704 RVA: 0x001E0FB3 File Offset: 0x001DF1B3
		// (set) Token: 0x06007409 RID: 29705 RVA: 0x001E0FBB File Offset: 0x001DF1BB
		internal bool IsTablixProcessingMode
		{
			get
			{
				return this.m_isTablixProcessingMode;
			}
			set
			{
				this.m_isTablixProcessingMode = value;
				this.m_stateManager.ResetOnDemandState();
			}
		}

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x0600740A RID: 29706 RVA: 0x001E0FCF File Offset: 0x001DF1CF
		// (set) Token: 0x0600740B RID: 29707 RVA: 0x001E0FD7 File Offset: 0x001DF1D7
		internal bool IsUnrestrictedRenderFormatReferenceMode
		{
			get
			{
				return this.m_isUnrestrictedRenderFormatReferenceMode;
			}
			set
			{
				this.m_isUnrestrictedRenderFormatReferenceMode = value;
			}
		}

		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x0600740C RID: 29708 RVA: 0x001E0FE0 File Offset: 0x001DF1E0
		// (set) Token: 0x0600740D RID: 29709 RVA: 0x001E0FE8 File Offset: 0x001DF1E8
		internal bool IsTopLevelSubReportProcessing
		{
			get
			{
				return this.m_isTopLevelSubReportProcessing;
			}
			set
			{
				this.m_isTopLevelSubReportProcessing = value;
			}
		}

		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x0600740E RID: 29710 RVA: 0x001E0FF1 File Offset: 0x001DF1F1
		internal bool IsSharedDataSetExecutionOnly
		{
			get
			{
				return this.m_isSharedDataSetExecutionOnly;
			}
		}

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x0600740F RID: 29711 RVA: 0x001E0FF9 File Offset: 0x001DF1F9
		// (set) Token: 0x06007410 RID: 29712 RVA: 0x001E1006 File Offset: 0x001DF206
		internal IInstancePath LastRIFObject
		{
			get
			{
				return this.m_stateManager.LastRIFObject;
			}
			set
			{
				this.m_stateManager.LastRIFObject = value;
			}
		}

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x06007411 RID: 29713 RVA: 0x001E1014 File Offset: 0x001DF214
		internal QueryRestartInfo QueryRestartInfo
		{
			get
			{
				return this.m_stateManager.QueryRestartInfo;
			}
		}

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x06007412 RID: 29714 RVA: 0x001E1021 File Offset: 0x001DF221
		// (set) Token: 0x06007413 RID: 29715 RVA: 0x001E102E File Offset: 0x001DF22E
		internal IRIFReportScope LastTablixProcessingReportScope
		{
			get
			{
				return this.m_stateManager.LastTablixProcessingReportScope;
			}
			set
			{
				this.m_stateManager.LastTablixProcessingReportScope = value;
			}
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x001E103C File Offset: 0x001DF23C
		internal int RecursiveLevel(string scopeName)
		{
			return this.m_stateManager.RecursiveLevel(scopeName);
		}

		// Token: 0x06007415 RID: 29717 RVA: 0x001E104A File Offset: 0x001DF24A
		internal bool InScope(string scopeName)
		{
			return this.m_stateManager.InScope(scopeName);
		}

		// Token: 0x06007416 RID: 29718 RVA: 0x001E1058 File Offset: 0x001DF258
		internal Dictionary<string, object> GetCurrentSpecialGroupingValues()
		{
			return this.m_stateManager.GetCurrentSpecialGroupingValues();
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x001E1065 File Offset: 0x001DF265
		internal IRecordRowReader CreateSequentialDataReader(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance)
		{
			return this.m_stateManager.CreateSequentialDataReader(dataSet, out dataSetInstance);
		}

		// Token: 0x06007418 RID: 29720 RVA: 0x001E1074 File Offset: 0x001DF274
		internal bool CalculateAggregate(string aggregateName)
		{
			return this.m_stateManager.CalculateAggregate(aggregateName);
		}

		// Token: 0x06007419 RID: 29721 RVA: 0x001E1082 File Offset: 0x001DF282
		internal bool CalculateLookup(LookupInfo lookup)
		{
			return this.m_stateManager.CalculateLookup(lookup);
		}

		// Token: 0x0600741A RID: 29722 RVA: 0x001E1090 File Offset: 0x001DF290
		internal bool PrepareFieldsCollectionForDirectFields()
		{
			return this.m_stateManager.PrepareFieldsCollectionForDirectFields();
		}

		// Token: 0x0600741B RID: 29723 RVA: 0x001E109D File Offset: 0x001DF29D
		internal void RestoreContext(IInstancePath originalObject)
		{
			this.m_stateManager.RestoreContext(originalObject);
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x001E10AB File Offset: 0x001DF2AB
		internal void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance)
		{
			this.m_stateManager.SetupContext(rifObject, romInstance);
		}

		// Token: 0x0600741D RID: 29725 RVA: 0x001E10BA File Offset: 0x001DF2BA
		internal void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.m_stateManager.SetupContext(rifObject, romInstance, moveNextInstanceIndex);
		}

		// Token: 0x0600741E RID: 29726 RVA: 0x001E10CA File Offset: 0x001DF2CA
		internal void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.CheckAndThrowIfAborted();
			this.m_stateManager.BindNextMemberInstance(rifObject, romInstance, moveNextInstanceIndex);
		}

		// Token: 0x0600741F RID: 29727 RVA: 0x001E10E0 File Offset: 0x001DF2E0
		internal void OnDemandProcessDataPipelineWithRestore(DataSetAggregateDataPipelineManager pipeline)
		{
			FieldsContext currentFields = this.ReportObjectModel.CurrentFields;
			IScalabilityCache tablixProcessingScalabilityCache = this.m_tablixProcessingScalabilityCache;
			this.m_tablixProcessingScalabilityCache = null;
			pipeline.StartProcessing();
			pipeline.StopProcessing();
			this.m_tablixProcessingScalabilityCache = tablixProcessingScalabilityCache;
			if (currentFields != null)
			{
				this.ReportObjectModel.RestoreFields(currentFields);
			}
		}

		// Token: 0x06007420 RID: 29728 RVA: 0x001E1129 File Offset: 0x001DF329
		internal void SetupEmptyTopLevelFields()
		{
			this.m_reportObjectModel.SetupEmptyTopLevelFields();
			this.m_currentDataSetIndex = -1;
			this.m_currentDataSetInstance = null;
		}

		// Token: 0x06007421 RID: 29729 RVA: 0x001E1144 File Offset: 0x001DF344
		internal void SetupFieldsForNewDataSetPageSection(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataset)
		{
			this.m_reportObjectModel.SetupPageSectionDataSetFields(dataset);
			this.m_currentDataSetIndex = dataset.IndexInCollection;
			this.m_currentDataSetInstance = null;
		}

		// Token: 0x06007422 RID: 29730 RVA: 0x001E1165 File Offset: 0x001DF365
		internal void SetupFieldsForNewDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataset, Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance, bool addRowIndex, bool noRows)
		{
			this.m_reportObjectModel.SetupFieldsForNewDataSet(dataset, addRowIndex, noRows, false);
			this.m_currentDataSetIndex = dataset.IndexInCollection;
			this.m_currentDataSetInstance = dataSetInstance;
		}

		// Token: 0x06007423 RID: 29731 RVA: 0x001E118A File Offset: 0x001DF38A
		internal void EnsureRuntimeEnvironmentForDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, bool noRows)
		{
			if (this.m_currentDataSetIndex != dataSet.IndexInCollection)
			{
				dataSet.SetupRuntimeEnvironment(this);
				this.SetupFieldsForNewDataSet(dataSet, null, true, noRows);
			}
		}

		// Token: 0x06007424 RID: 29732 RVA: 0x001E11AB File Offset: 0x001DF3AB
		internal void AddSpecialDataRowSort(IReference<IDataRowSortOwner> sortOwner)
		{
			if (this.m_dataRowSortOwners == null)
			{
				this.m_dataRowSortOwners = new List<IReference<IDataRowSortOwner>>();
			}
			this.m_dataRowSortOwners.Add(sortOwner);
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x001E11CC File Offset: 0x001DF3CC
		internal void AddSpecialDataRegionFilters(Filters filters)
		{
			if (this.m_specialDataRegionFilters == null)
			{
				this.m_specialDataRegionFilters = new List<Filters>();
			}
			this.m_specialDataRegionFilters.Add(filters);
		}

		// Token: 0x06007426 RID: 29734 RVA: 0x001E11F0 File Offset: 0x001DF3F0
		private bool ProcessDataRegionsWithSpecialFiltersOrDataRowSorting()
		{
			bool flag = false;
			int num = ((this.m_dataRowSortOwners == null) ? 0 : this.m_dataRowSortOwners.Count);
			if (this.m_specialDataRegionFilters != null)
			{
				int count = this.m_specialDataRegionFilters.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_specialDataRegionFilters[i].FinishReadingRows();
				}
				this.m_specialDataRegionFilters.RemoveRange(0, count);
				flag |= this.m_specialDataRegionFilters.Count > 0;
			}
			if (num != 0)
			{
				for (int j = 0; j < num; j++)
				{
					using (this.m_dataRowSortOwners[j].PinValue())
					{
						this.m_dataRowSortOwners[j].Value().DataRowSortTraverse();
					}
				}
				this.m_dataRowSortOwners.RemoveRange(0, num);
				flag |= this.m_dataRowSortOwners.Count > 0;
			}
			return flag;
		}

		// Token: 0x06007427 RID: 29735 RVA: 0x001E12E0 File Offset: 0x001DF4E0
		internal bool PopulateRuntimeSortFilterEventInfo(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			return this.m_userSortFilterContext.PopulateRuntimeSortFilterEventInfo(this, dataSet);
		}

		// Token: 0x06007428 RID: 29736 RVA: 0x001E12EF File Offset: 0x001DF4EF
		internal bool IsSortFilterTarget(bool[] isSortFilterTarget, IReference<IScope> outerScope, IReference<IHierarchyObj> target, ref Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeUserSortTargetInfo userSortTargetInfo)
		{
			return this.m_userSortFilterContext.IsSortFilterTarget(isSortFilterTarget, outerScope, target, ref userSortTargetInfo);
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x001E1301 File Offset: 0x001DF501
		internal void ProcessUserSortForTarget(IReference<IHierarchyObj> target, ref ScalableList<DataFieldRow> dataRows, bool targetForNonDetailSort)
		{
			this.m_userSortFilterContext.ProcessUserSortForTarget(this.m_reportObjectModel, this.m_reportRuntime, target, ref dataRows, targetForNonDetailSort);
		}

		// Token: 0x0600742A RID: 29738 RVA: 0x001E131D File Offset: 0x001DF51D
		internal void RegisterSortFilterExpressionScope(IReference<IScope> container, IReference<RuntimeDataRegionObj> scopeObj, bool[] isSortFilterExpressionScope)
		{
			this.m_userSortFilterContext.RegisterSortFilterExpressionScope(container, scopeObj, isSortFilterExpressionScope);
		}

		// Token: 0x0600742B RID: 29739 RVA: 0x001E132D File Offset: 0x001DF52D
		internal EventInformation GetUserSortFilterInformation(out string oldUniqueName)
		{
			return this.m_commonInfo.GetUserSortFilterInformation(out oldUniqueName);
		}

		// Token: 0x0600742C RID: 29740 RVA: 0x001E133B File Offset: 0x001DF53B
		internal void MergeNewUserSortFilterInformation()
		{
			this.m_commonInfo.MergeNewUserSortFilterInformation();
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x001E1348 File Offset: 0x001DF548
		internal void FirstPassPostProcess()
		{
			while (this.ProcessDataRegionsWithSpecialFiltersOrDataRowSorting())
			{
			}
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x001E135F File Offset: 0x001DF55F
		internal void ApplyUserSorts()
		{
			while (this.m_userSortFilterContext.ProcessUserSort(this))
			{
			}
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x001E1370 File Offset: 0x001DF570
		internal List<object>[] GetScopeValues(Microsoft.ReportingServices.ReportIntermediateFormat.GroupingList containingScopes, IScope containingScope)
		{
			List<object>[] array = null;
			if (containingScopes != null && 0 < containingScopes.Count)
			{
				array = new List<object>[containingScopes.Count];
				int num = 0;
				containingScope.GetScopeValues(null, array, ref num);
			}
			return array;
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x001E13A4 File Offset: 0x001DF5A4
		internal ProcessingMessageList RegisterComparisonErrorForSortFilterEvent(string propertyName)
		{
			Global.Tracer.Assert(this.m_userSortFilterContext.CurrentSortFilterEventSource != null, "(null != m_userSortFilterContext.CurrentSortFilterEventSource)");
			this.ErrorContext.Register(ProcessingErrorCode.rsComparisonError, Severity.Error, this.m_userSortFilterContext.CurrentSortFilterEventSource.ObjectType, this.m_userSortFilterContext.CurrentSortFilterEventSource.Name, propertyName, Array.Empty<string>());
			return this.ErrorContext.Messages;
		}

		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x06007431 RID: 29745 RVA: 0x001E1411 File Offset: 0x001DF611
		internal Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext UserSortFilterContext
		{
			get
			{
				return this.m_userSortFilterContext;
			}
		}

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x06007432 RID: 29746 RVA: 0x001E1419 File Offset: 0x001DF619
		internal List<IReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo>> RuntimeSortFilterInfo
		{
			get
			{
				return this.m_userSortFilterContext.RuntimeSortFilterInfo;
			}
		}

		// Token: 0x06007433 RID: 29747 RVA: 0x001E1428 File Offset: 0x001DF628
		internal void CheckAndThrowIfAborted()
		{
			AbortHelper abortInfo = this.m_commonInfo.AbortInfo;
			if (abortInfo != null)
			{
				abortInfo.ThrowIfAborted(CancelationTrigger.ReportProcessing, this.m_subReportInstanceOrSharedDatasetUniqueName);
			}
		}

		// Token: 0x06007434 RID: 29748 RVA: 0x001E1454 File Offset: 0x001DF654
		internal void AddDataChunkReader(int dataSetIndexInCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader dataReader)
		{
			if (this.m_dataSetToDataReader == null)
			{
				this.m_dataSetToDataReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader[this.m_reportDefinition.MappingNameToDataSet.Count];
			}
			Global.Tracer.Assert(this.m_dataSetToDataReader[dataSetIndexInCollection] == null, "(null == m_dataSetToDataReader[dataSetIndexInCollection])");
			this.m_dataSetToDataReader[dataSetIndexInCollection] = dataReader;
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x001E14A8 File Offset: 0x001DF6A8
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader GetDataChunkReader(int dataSetIndex)
		{
			if (this.IsPageHeaderFooter)
			{
				return this.m_parentContext.GetDataChunkReader(dataSetIndex);
			}
			if (this.m_dataSetToDataReader == null || this.m_dataSetToDataReader[dataSetIndex] == null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_reportDefinition.MappingDataSetIndexToDataSet[dataSetIndex];
				string text;
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance = this.GetDataSetInstance(dataSet, out text);
				Global.Tracer.Assert(dataSetInstance != null, "Missing expected DataSetInstance. Report: {0} DataSet: {1} DataSetIndex: {2}", new object[]
				{
					this.m_reportDefinition.Name,
					dataSet.Name,
					dataSetIndex
				});
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader dataChunkReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader(dataSetInstance, this, text);
				this.AddDataChunkReader(dataSetIndex, dataChunkReader);
				return dataChunkReader;
			}
			return this.m_dataSetToDataReader[dataSetIndex];
		}

		// Token: 0x06007436 RID: 29750 RVA: 0x001E154C File Offset: 0x001DF74C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance GetDataSetInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			if (this.StreamingMode)
			{
				return null;
			}
			string text;
			return this.GetDataSetInstance(dataSet, out text);
		}

		// Token: 0x06007437 RID: 29751 RVA: 0x001E156C File Offset: 0x001DF76C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance GetDataSetInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out string chunkName)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance = null;
			chunkName = null;
			if (dataSet.UsedOnlyInParameters)
			{
				return null;
			}
			chunkName = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.GenerateDataChunkName(this, dataSet.ID, this.m_inSubreport);
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance> dataChunkMap = this.m_odpMetadata.DataChunkMap;
			if (dataChunkMap == null || !dataChunkMap.TryGetValue(chunkName, out dataSetInstance))
			{
				chunkName = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.GenerateLegacySharedSubReportDataChunkName(this, dataSet.ID);
				if ((dataChunkMap == null || !dataChunkMap.TryGetValue(chunkName, out dataSetInstance)) && this.SnapshotProcessing)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Dataset not found in data chunk map. Name={0}, Chunkname={1}", new object[] { dataSet.Name, chunkName });
				}
			}
			if (dataSetInstance != null && dataSetInstance.DataSetDef == null)
			{
				dataSetInstance.DataSetDef = dataSet;
			}
			return dataSetInstance;
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x001E1614 File Offset: 0x001DF814
		internal bool[] GenerateDataSetExclusionList(out int unprocessedDataSetCount)
		{
			int dataSetCount = this.m_reportDefinition.DataSetCount;
			unprocessedDataSetCount = dataSetCount;
			bool[] array = new bool[dataSetCount];
			for (int i = 0; i < dataSetCount; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_reportDefinition.MappingDataSetIndexToDataSet[i];
				if (dataSet.UsedOnlyInParameters)
				{
					array[i] = true;
					unprocessedDataSetCount--;
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance = this.m_currentReportInstance.GetDataSetInstance(dataSet, this);
					if (dataSetInstance == null || this.IsTablixProcessingComplete(i))
					{
						array[i] = true;
						unprocessedDataSetCount--;
					}
				}
			}
			return array;
		}

		// Token: 0x06007439 RID: 29753 RVA: 0x001E1690 File Offset: 0x001DF890
		internal void FreeAllResources()
		{
			if (this.m_odpMetadata == null)
			{
				this.FreeResources();
				return;
			}
			foreach (OnDemandProcessingContext onDemandProcessingContext in this.m_odpMetadata.OdpContexts)
			{
				onDemandProcessingContext.FreeResources();
			}
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x001E16F4 File Offset: 0x001DF8F4
		private void FreeResources()
		{
			if (this.m_dataSetToDataReader != null)
			{
				for (int i = 0; i < this.m_dataSetToDataReader.Length; i++)
				{
					if (this.m_dataSetToDataReader[i] != null)
					{
						this.m_dataSetToDataReader[i].Close();
						this.m_dataSetToDataReader[i] = null;
					}
				}
				this.m_dataSetToDataReader = null;
			}
			if (this.m_stateManager != null)
			{
				this.m_stateManager.FreeResources();
			}
			this.EnsureScalabilityCleanup();
		}

		// Token: 0x0600743B RID: 29755 RVA: 0x001E175C File Offset: 0x001DF95C
		internal void EnsureScalabilitySetup()
		{
			if (this.m_tablixProcessingScalabilityCache == null)
			{
				this.m_tablixProcessingScalabilityCache = ScalabilityUtils.CreateCacheForTransientAllocations(this.CreateStreamCallback, "RGT", StorageObjectCreator.Instance, RuntimeReferenceCreator.Instance, ComponentType.Processing, 5);
				this.ExecutionLogContext.RegisterTablixProcessingScaleCache(this.m_isSharedDataSetExecutionOnly ? 0 : this.m_reportDefinition.GlobalID);
			}
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x001E17B4 File Offset: 0x001DF9B4
		internal void EnsureScalabilityCleanup()
		{
			if (this.m_tablixProcessingScalabilityCache != null)
			{
				this.ExecutionLogContext.UnRegisterTablixProcessingScaleCache(this.m_isSharedDataSetExecutionOnly ? 0 : this.m_reportDefinition.GlobalID, this.m_tablixProcessingScalabilityCache);
				this.m_tablixProcessingScalabilityCache.Dispose();
				this.m_tablixProcessingScalabilityCache = null;
			}
		}

		// Token: 0x0600743D RID: 29757 RVA: 0x001E1802 File Offset: 0x001DFA02
		internal bool IsTablixProcessingComplete(int dataSetIndexInCollection)
		{
			return this.m_odpMetadata.IsTablixProcessingComplete(this, dataSetIndexInCollection);
		}

		// Token: 0x0600743E RID: 29758 RVA: 0x001E1811 File Offset: 0x001DFA11
		internal void SetTablixProcessingComplete(int dataSetIndexInCollection)
		{
			this.m_odpMetadata.SetTablixProcessingComplete(this, dataSetIndexInCollection);
		}

		// Token: 0x0600743F RID: 29759 RVA: 0x001E1820 File Offset: 0x001DFA20
		internal int CreateUniqueID()
		{
			int num = this.m_commonInfo.CreateUniqueID();
			if (this.m_subReportInfo != null)
			{
				this.m_odpMetadata.MetadataHasChanged = true;
				this.m_subReportInfo.LastID = num;
			}
			return num;
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x001E185C File Offset: 0x001DFA5C
		internal bool GetResource(string path, out byte[] resource, out string mimeType, out bool registerInvalidSizeWarning)
		{
			if (this.m_commonInfo.GetResourceCallback != null)
			{
				ExecutionLogContext executionLogContext = this.m_commonInfo.ExecutionLogContext;
				long externalImageCount = executionLogContext.ExternalImageCount;
				executionLogContext.ExternalImageCount = externalImageCount + 1L;
				this.m_commonInfo.ExecutionLogContext.StartExternalImageTimer();
				bool flag;
				try
				{
					this.m_commonInfo.GetResourceCallback.GetResource(this.m_catalogItemContext, path, out resource, out mimeType, out flag, out registerInvalidSizeWarning);
					if (resource != null)
					{
						this.m_commonInfo.ExecutionLogContext.ExternalImageBytes += (long)resource.Length;
					}
				}
				finally
				{
					this.m_commonInfo.ExecutionLogContext.StopExternalImageTimer();
				}
				if (flag)
				{
					this.ErrorContext.Register(ProcessingErrorCode.rsWarningFetchingExternalImages, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.Report, null, null, Array.Empty<string>());
				}
				if (registerInvalidSizeWarning)
				{
					this.TraceOneTimeWarning(ProcessingErrorCode.rsSandboxingExternalResourceExceedsMaximumSize);
				}
				return true;
			}
			resource = null;
			mimeType = null;
			registerInvalidSizeWarning = false;
			return false;
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x001E1940 File Offset: 0x001DFB40
		internal void TraceOneTimeWarning(ProcessingErrorCode errorCode)
		{
			this.m_commonInfo.TraceOneTimeWarning(errorCode, this.m_catalogItemContext);
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x001E1954 File Offset: 0x001DFB54
		internal void LoadExistingSubReportDataChunkNameModifier(Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance)
		{
			Global.Tracer.Assert(this.m_subReportInfo != null, "Cannot set DataChunkName modifier if the subreport definition could not be found");
			this.m_subReportDataChunkNameModifier = subReportInstance.GetChunkNameModifier(this.m_subReportInfo, true, false, out this.m_foundExistingSubReportInstance);
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x001E1988 File Offset: 0x001DFB88
		internal void SetSubReportNameModifierAndParameters(Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance, bool addEntry)
		{
			Global.Tracer.Assert(this.m_subReportInfo != null, "Cannot set DataChunkName modifier and parameters if the subreport definition could not be found");
			this.m_subReportDataChunkNameModifier = subReportInstance.GetChunkNameModifier(this.m_subReportInfo, false, addEntry, out this.m_foundExistingSubReportInstance);
			if (addEntry && !this.m_foundExistingSubReportInstance)
			{
				this.m_odpMetadata.MetadataHasChanged = true;
			}
			if (this.SnapshotProcessing)
			{
				ParametersImpl parameters = subReportInstance.Parameters;
				if (parameters != null)
				{
					this.m_reportObjectModel.ParametersImpl = parameters;
				}
			}
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x001E19FC File Offset: 0x001DFBFC
		internal void SetSharedDataSetUniqueName(string chunkName)
		{
			this.m_subReportInstanceOrSharedDatasetUniqueName = chunkName;
			AbortHelper abortInfo = this.m_commonInfo.AbortInfo;
			if (abortInfo != null)
			{
				abortInfo.AddSubreportInstanceOrSharedDataSet(this.m_subReportInstanceOrSharedDatasetUniqueName);
			}
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x001E1A2C File Offset: 0x001DFC2C
		internal void SetSubReportContext(Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance, bool setupReportOM)
		{
			Global.Tracer.Assert(this.m_subReportInfo != null, "Cannot SetSubReportContext if the subreport definition could not be found");
			string text = this.m_subReportInfo.UniqueName + "x" + subReportInstance.InstanceUniqueName;
			if (!this.SnapshotProcessing && this.m_reportDefinition != null)
			{
				this.InitializeDataSetMembers(this.m_reportDefinition.MappingNameToDataSet.Count);
			}
			if (this.m_subReportInstanceOrSharedDatasetUniqueName != text)
			{
				this.m_subReportInstanceOrSharedDatasetUniqueName = text;
				AbortHelper abortInfo = this.m_commonInfo.AbortInfo;
				if (abortInfo != null)
				{
					abortInfo.AddSubreportInstanceOrSharedDataSet(this.m_subReportInstanceOrSharedDatasetUniqueName);
				}
				this.ResetDataSetToDataReader();
				if (subReportInstance.ThreadCulture != null)
				{
					this.m_threadCulture = subReportInstance.ThreadCulture;
				}
				this.m_currentReportInstance = subReportInstance.ReportInstance.Value();
				this.m_currentDataSetIndex = -1;
				this.m_stateManager.ResetOnDemandState();
				if (setupReportOM && this.m_reportObjectModel != null)
				{
					this.m_reportObjectModel.SetForNewSubReportContext(subReportInstance.Parameters);
				}
			}
		}

		// Token: 0x06007446 RID: 29766 RVA: 0x001E1B1C File Offset: 0x001DFD1C
		private void ResetDataSetToDataReader()
		{
			if (this.m_dataSetToDataReader != null && this.m_currentReportInstance != null)
			{
				for (int i = 0; i < this.m_dataSetToDataReader.Length; i++)
				{
					if (this.m_dataSetToDataReader[i] != null)
					{
						((IDisposable)this.m_dataSetToDataReader[i]).Dispose();
						this.m_dataSetToDataReader[i] = null;
					}
				}
			}
		}

		// Token: 0x06007447 RID: 29767 RVA: 0x001E1B6C File Offset: 0x001DFD6C
		internal static ParameterInfoCollection EvaluateSubReportParameters(OnDemandProcessingContext parentContext, Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport)
		{
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			if (subReport.Parameters != null && subReport.ParametersFromCatalog != null)
			{
				for (int i = 0; i < subReport.Parameters.Count; i++)
				{
					string name = subReport.Parameters[i].Name;
					if (subReport.ParametersFromCatalog[name] == null)
					{
						throw new UnknownReportParameterException(name);
					}
					parentContext.LastRIFObject = subReport;
					Microsoft.ReportingServices.RdlExpressions.ParameterValueResult parameterValueResult = parentContext.ReportRuntime.EvaluateParameterValueExpression(subReport.Parameters[i], subReport.ObjectType, subReport.Name, "ParameterValue");
					if (parameterValueResult.ErrorOccurred)
					{
						throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { name });
					}
					object[] array = parameterValueResult.Value as object[];
					object[] array2;
					if (array != null)
					{
						array2 = array;
					}
					else
					{
						array2 = new object[] { parameterValueResult.Value };
					}
					parameterInfoCollection.Add(new ParameterInfo
					{
						Name = name,
						Values = array2,
						DataType = parameterValueResult.Type
					});
				}
			}
			ParameterInfoCollection parameterInfoCollection2 = new ParameterInfoCollection();
			subReport.ParametersFromCatalog.CopyTo(parameterInfoCollection2);
			return ParameterInfoCollection.Combine(parameterInfoCollection2, parameterInfoCollection, true, false, false, false, Localization.ClientPrimaryCulture);
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x001E1CA6 File Offset: 0x001DFEA6
		internal bool StoreUpdatedVariableValue(int index, object value)
		{
			return this.m_odpMetadata.StoreUpdatedVariableValue(this, this.m_currentReportInstance, index, value);
		}

		// Token: 0x06007449 RID: 29769 RVA: 0x001E1CBC File Offset: 0x001DFEBC
		internal int CompareAndStopOnError(object value1, object value2, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, bool extendedTypeComparisons)
		{
			int num;
			try
			{
				bool flag;
				num = this.ProcessingComparer.Compare(value1, value2, true, extendedTypeComparisons, out flag);
			}
			catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError)
			{
				throw new ReportProcessingException(this.RegisterSpatialTypeComparisonError(objectType, objectName, reportProcessingException_SpatialTypeComparisonError.Type));
			}
			catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError)
			{
				throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError, objectType, objectName, propertyName));
			}
			catch (CommonDataComparerException ex)
			{
				throw new ReportProcessingException(this.RegisterComparisonError(ex, objectType, objectName, propertyName));
			}
			return num;
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x001E1D48 File Offset: 0x001DFF48
		internal ProcessingMessageList RegisterComparisonError(IDataComparisonError e, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (e == null)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsComparisonError, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsComparisonTypeError, Severity.Error, objectType, objectName, propertyName, new string[] { e.TypeX, e.TypeY });
			}
			return this.m_errorContext.Messages;
		}

		// Token: 0x0600744B RID: 29771 RVA: 0x001E1DAE File Offset: 0x001DFFAE
		internal ProcessingMessageList RegisterSpatialTypeComparisonError(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string spatialTypeName)
		{
			this.m_errorContext.Register(ProcessingErrorCode.rsCannotCompareSpatialType, Severity.Error, objectType, objectName, spatialTypeName, Array.Empty<string>());
			return this.m_errorContext.Messages;
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x001E1DD5 File Offset: 0x001DFFD5
		internal void LogMetrics()
		{
			if (this.m_odpMetadata != null)
			{
				this.m_odpMetadata.LogMetrics();
			}
		}

		// Token: 0x17002767 RID: 10087
		// (get) Token: 0x0600744D RID: 29773 RVA: 0x001E1DEA File Offset: 0x001DFFEA
		int IStaticReferenceable.ID
		{
			get
			{
				return this.m_staticRefId;
			}
		}

		// Token: 0x0600744E RID: 29774 RVA: 0x001E1DF2 File Offset: 0x001DFFF2
		void IStaticReferenceable.SetID(int id)
		{
			this.m_staticRefId = id;
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x001E1DFB File Offset: 0x001DFFFB
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IStaticReferenceable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.OnDemandProcessingContext;
		}

		// Token: 0x04003B0E RID: 15118
		private readonly DataSetContext m_externalDataSetContext;

		// Token: 0x04003B0F RID: 15119
		private readonly OnDemandProcessingContext m_parentContext;

		// Token: 0x04003B10 RID: 15120
		private readonly OnDemandProcessingContext.CommonInfo m_commonInfo;

		// Token: 0x04003B11 RID: 15121
		private readonly ICatalogItemContext m_catalogItemContext;

		// Token: 0x04003B12 RID: 15122
		private ObjectModelImpl m_reportObjectModel;

		// Token: 0x04003B13 RID: 15123
		private readonly bool m_reportItemsReferenced;

		// Token: 0x04003B14 RID: 15124
		private bool m_reportItemThisDotValueReferenced;

		// Token: 0x04003B15 RID: 15125
		private readonly Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> m_embeddedImages;

		// Token: 0x04003B16 RID: 15126
		private bool m_processReportParameters;

		// Token: 0x04003B17 RID: 15127
		private Microsoft.ReportingServices.RdlExpressions.ReportRuntime m_reportRuntime;

		// Token: 0x04003B18 RID: 15128
		private ParameterInfoCollection m_reportParameters;

		// Token: 0x04003B19 RID: 15129
		private readonly ErrorContext m_errorContext;

		// Token: 0x04003B1A RID: 15130
		private bool m_snapshotProcessing;

		// Token: 0x04003B1B RID: 15131
		private CultureInfo m_threadCulture;

		// Token: 0x04003B1C RID: 15132
		private CompareInfo m_compareInfo = Thread.CurrentThread.CurrentCulture.CompareInfo;

		// Token: 0x04003B1D RID: 15133
		private CompareOptions m_clrCompareOptions;

		// Token: 0x04003B1E RID: 15134
		private bool m_nullsAsBlanks;

		// Token: 0x04003B1F RID: 15135
		private bool m_useOrdinalStringKeyGeneration;

		// Token: 0x04003B20 RID: 15136
		private IDataComparer m_processingComparer;

		// Token: 0x04003B21 RID: 15137
		private StringKeyGenerator m_stringKeyGenerator;

		// Token: 0x04003B22 RID: 15138
		private readonly bool m_inSubreport;

		// Token: 0x04003B23 RID: 15139
		private readonly bool m_inSubreportInDataRegion;

		// Token: 0x04003B24 RID: 15140
		private bool m_isTablixProcessingMode;

		// Token: 0x04003B25 RID: 15141
		private bool m_isTopLevelSubReportProcessing;

		// Token: 0x04003B26 RID: 15142
		private bool m_isUnrestrictedRenderFormatReferenceMode;

		// Token: 0x04003B27 RID: 15143
		private readonly bool m_isSharedDataSetExecutionOnly;

		// Token: 0x04003B28 RID: 15144
		private readonly Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo> m_reportAggregates = new Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>();

		// Token: 0x04003B29 RID: 15145
		private bool m_errorSavingSnapshotData;

		// Token: 0x04003B2A RID: 15146
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.Report m_reportDefinition;

		// Token: 0x04003B2B RID: 15147
		private readonly OnDemandMetadata m_odpMetadata;

		// Token: 0x04003B2C RID: 15148
		private bool m_hasBookmarks;

		// Token: 0x04003B2D RID: 15149
		private bool m_hasShowHide;

		// Token: 0x04003B2E RID: 15150
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance m_currentReportInstance;

		// Token: 0x04003B2F RID: 15151
		private int m_currentDataSetIndex = -1;

		// Token: 0x04003B30 RID: 15152
		private List<object> m_groupExprValues = new List<object>();

		// Token: 0x04003B31 RID: 15153
		private bool m_peerOuterGroupProcessing;

		// Token: 0x04003B32 RID: 15154
		private string m_subReportInstanceOrSharedDatasetUniqueName;

		// Token: 0x04003B33 RID: 15155
		private bool m_foundExistingSubReportInstance;

		// Token: 0x04003B34 RID: 15156
		private string m_subReportDataChunkNameModifier;

		// Token: 0x04003B35 RID: 15157
		private SubReportInfo m_subReportInfo;

		// Token: 0x04003B36 RID: 15158
		private readonly bool m_specialRecursiveAggregates;

		// Token: 0x04003B37 RID: 15159
		private SecondPassOperations m_secondPassOperation;

		// Token: 0x04003B38 RID: 15160
		private List<Filters> m_specialDataRegionFilters;

		// Token: 0x04003B39 RID: 15161
		private List<IReference<IDataRowSortOwner>> m_dataRowSortOwners;

		// Token: 0x04003B3A RID: 15162
		private readonly Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext m_userSortFilterContext;

		// Token: 0x04003B3B RID: 15163
		private bool m_initializedRuntime;

		// Token: 0x04003B3C RID: 15164
		private readonly bool m_isPageHeaderFooter;

		// Token: 0x04003B3D RID: 15165
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader[] m_dataSetToDataReader;

		// Token: 0x04003B3E RID: 15166
		private bool[] m_dataSetRetrievalComplete;

		// Token: 0x04003B3F RID: 15167
		private IScalabilityCache m_tablixProcessingScalabilityCache;

		// Token: 0x04003B40 RID: 15168
		private CommonRowCache m_tablixProcessingLookupRowCache;

		// Token: 0x04003B41 RID: 15169
		private int m_staticRefId = int.MaxValue;

		// Token: 0x04003B42 RID: 15170
		private DomainScopeContext m_domainScopeContext;

		// Token: 0x04003B43 RID: 15171
		private readonly OnDemandStateManager m_stateManager;

		// Token: 0x04003B44 RID: 15172
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance m_currentDataSetInstance;

		// Token: 0x02000CF7 RID: 3319
		internal enum Mode
		{
			// Token: 0x04004FCE RID: 20430
			Full,
			// Token: 0x04004FCF RID: 20431
			Streaming,
			// Token: 0x04004FD0 RID: 20432
			DefinitionOnly
		}

		// Token: 0x02000CF8 RID: 3320
		private sealed class CommonInfo
		{
			// Token: 0x06008E16 RID: 36374 RVA: 0x0024412C File Offset: 0x0024232C
			internal CommonInfo(IChunkFactory chunkFactory, ReportProcessing.OnDemandSubReportCallback subReportCallback, IGetResource getResourceCallback, ReportProcessing.StoreServerParameters storeServerParameters, ReportRuntimeSetup reportRuntimeSetup, UserProfileState allowUserProfileState, string requestUserName, CultureInfo userLanguage, DateTime executionTime, bool reprocessSnapshot, bool processWithCachedData, CreateAndRegisterStream createStreamCallback, bool enableDataBackedParameters, IJobContext jobContext, IExtensionFactory extFactory, IDataProtection dataProtection, ExecutionLogContext executionLogContext, RuntimeDataSourceInfoCollection dataSourceInfos, RuntimeDataSetInfoCollection sharedDataSetReferences, IProcessingDataExtensionConnection createAndSetupDataExtensionFunction, IConfiguration configuration, ReportProcessing.DataSourceInfoHashtable globalDataSourceInfo, ReportProcessingContext externalProcessingContext, AbortHelper abortInfo, bool abortInfoInherited, UserProfileState hasUserProfileState, OnDemandProcessingContext topLevelContext, OnDemandProcessingContext.Mode contextMode, ImageCacheManager imageCacheManager, bool usePreviewCommands, bool compareSafeExpressionsToLegacy, bool useUserLanguageForProcessing)
			{
				this.m_chunkFactory = chunkFactory;
				this.m_subReportCallback = subReportCallback;
				this.m_getResourceCallback = getResourceCallback;
				this.m_storeServerParameters = storeServerParameters;
				this.m_reportRuntimeSetup = reportRuntimeSetup;
				this.m_allowUserProfileState = allowUserProfileState;
				this.m_requestUserName = requestUserName;
				this.m_userLanguage = userLanguage;
				this.m_executionTime = executionTime;
				this.m_reprocessSnapshot = reprocessSnapshot;
				this.m_processWithCachedData = processWithCachedData;
				this.m_createStreamCallback = createStreamCallback;
				this.m_enableDataBackedParameters = enableDataBackedParameters;
				this.m_jobContext = jobContext;
				this.m_extFactory = extFactory;
				this.m_dataProtection = dataProtection;
				this.m_executionLogContext = executionLogContext;
				this.m_dataSourceInfos = dataSourceInfos;
				this.m_sharedDataSetReferences = sharedDataSetReferences;
				this.m_createAndSetupDataExtensionFunction = createAndSetupDataExtensionFunction;
				this.m_configuration = configuration;
				this.m_hasTracedOneTimeMessage = new Dictionary<ProcessingErrorCode, bool>();
				this.m_globalDataSourceInfo = globalDataSourceInfo;
				this.m_externalProcessingContext = externalProcessingContext;
				this.m_abortInfo = abortInfo;
				this.m_abortInfoInherited = abortInfoInherited;
				this.m_hasUserProfileState = hasUserProfileState;
				this.m_topLevelContext = topLevelContext;
				this.m_contextMode = contextMode;
				this.m_imageCacheManager = imageCacheManager;
				this.m_usePreviewCommands = usePreviewCommands;
				this.m_compareSafeExpressionsToLegacy = compareSafeExpressionsToLegacy;
				this.m_useUserLanguageForProcessing = useUserLanguageForProcessing;
			}

			// Token: 0x17002B85 RID: 11141
			// (get) Token: 0x06008E17 RID: 36375 RVA: 0x00244252 File Offset: 0x00242452
			internal IGetResource GetResourceCallback
			{
				get
				{
					return this.m_getResourceCallback;
				}
			}

			// Token: 0x17002B86 RID: 11142
			// (get) Token: 0x06008E18 RID: 36376 RVA: 0x0024425A File Offset: 0x0024245A
			internal string RequestUserName
			{
				get
				{
					return this.m_requestUserName;
				}
			}

			// Token: 0x17002B87 RID: 11143
			// (get) Token: 0x06008E19 RID: 36377 RVA: 0x00244262 File Offset: 0x00242462
			internal DateTime ExecutionTime
			{
				get
				{
					return this.m_executionTime;
				}
			}

			// Token: 0x17002B88 RID: 11144
			// (get) Token: 0x06008E1A RID: 36378 RVA: 0x0024426A File Offset: 0x0024246A
			internal CultureInfo UserLanguage
			{
				get
				{
					return this.m_userLanguage;
				}
			}

			// Token: 0x17002B89 RID: 11145
			// (get) Token: 0x06008E1B RID: 36379 RVA: 0x00244272 File Offset: 0x00242472
			internal ReportProcessing.OnDemandSubReportCallback SubReportCallback
			{
				get
				{
					return this.m_subReportCallback;
				}
			}

			// Token: 0x17002B8A RID: 11146
			// (get) Token: 0x06008E1C RID: 36380 RVA: 0x0024427A File Offset: 0x0024247A
			internal UserProfileState AllowUserProfileState
			{
				get
				{
					return this.m_allowUserProfileState;
				}
			}

			// Token: 0x17002B8B RID: 11147
			// (get) Token: 0x06008E1D RID: 36381 RVA: 0x00244282 File Offset: 0x00242482
			internal bool StreamingMode
			{
				get
				{
					return this.m_contextMode == OnDemandProcessingContext.Mode.Streaming;
				}
			}

			// Token: 0x17002B8C RID: 11148
			// (get) Token: 0x06008E1E RID: 36382 RVA: 0x0024428D File Offset: 0x0024248D
			internal bool ReprocessSnapshot
			{
				get
				{
					return this.m_reprocessSnapshot;
				}
			}

			// Token: 0x17002B8D RID: 11149
			// (get) Token: 0x06008E1F RID: 36383 RVA: 0x00244295 File Offset: 0x00242495
			internal bool ProcessWithCachedData
			{
				get
				{
					return this.m_processWithCachedData;
				}
			}

			// Token: 0x17002B8E RID: 11150
			// (get) Token: 0x06008E20 RID: 36384 RVA: 0x0024429D File Offset: 0x0024249D
			internal IChunkFactory ChunkFactory
			{
				get
				{
					return this.m_chunkFactory;
				}
			}

			// Token: 0x17002B8F RID: 11151
			// (get) Token: 0x06008E21 RID: 36385 RVA: 0x002442A5 File Offset: 0x002424A5
			internal ReportRuntimeSetup ReportRuntimeSetup
			{
				get
				{
					return this.m_reportRuntimeSetup;
				}
			}

			// Token: 0x17002B90 RID: 11152
			// (get) Token: 0x06008E22 RID: 36386 RVA: 0x002442AD File Offset: 0x002424AD
			internal ReportProcessing.StoreServerParameters StoreServerParameters
			{
				get
				{
					return this.m_storeServerParameters;
				}
			}

			// Token: 0x17002B91 RID: 11153
			// (get) Token: 0x06008E23 RID: 36387 RVA: 0x002442B5 File Offset: 0x002424B5
			// (set) Token: 0x06008E24 RID: 36388 RVA: 0x002442BD File Offset: 0x002424BD
			internal EventInformation UserSortFilterInfo
			{
				get
				{
					return this.m_userSortFilterInfo;
				}
				set
				{
					this.m_userSortFilterInfo = value;
				}
			}

			// Token: 0x17002B92 RID: 11154
			// (get) Token: 0x06008E25 RID: 36389 RVA: 0x002442C6 File Offset: 0x002424C6
			// (set) Token: 0x06008E26 RID: 36390 RVA: 0x002442CE File Offset: 0x002424CE
			internal SortFilterEventInfoMap OldSortFilterEventInfo
			{
				get
				{
					return this.m_oldSortFilterEventInfo;
				}
				set
				{
					this.m_oldSortFilterEventInfo = value;
				}
			}

			// Token: 0x17002B93 RID: 11155
			// (get) Token: 0x06008E27 RID: 36391 RVA: 0x002442D7 File Offset: 0x002424D7
			// (set) Token: 0x06008E28 RID: 36392 RVA: 0x002442DF File Offset: 0x002424DF
			internal SortFilterEventInfoMap NewSortFilterEventInfo
			{
				get
				{
					return this.m_newSortFilterEventInfo;
				}
				set
				{
					this.m_newSortFilterEventInfo = value;
				}
			}

			// Token: 0x17002B94 RID: 11156
			// (get) Token: 0x06008E29 RID: 36393 RVA: 0x002442E8 File Offset: 0x002424E8
			// (set) Token: 0x06008E2A RID: 36394 RVA: 0x002442F0 File Offset: 0x002424F0
			internal string UserSortFilterEventSourceUniqueName
			{
				get
				{
					return this.m_userSortFilterEventSourceUniqueName;
				}
				set
				{
					this.m_userSortFilterEventSourceUniqueName = value;
				}
			}

			// Token: 0x17002B95 RID: 11157
			// (get) Token: 0x06008E2B RID: 36395 RVA: 0x002442F9 File Offset: 0x002424F9
			// (set) Token: 0x06008E2C RID: 36396 RVA: 0x00244301 File Offset: 0x00242501
			internal List<IReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo>> ReportRuntimeUserSortFilterInfo
			{
				get
				{
					return this.m_reportRuntimeUserSortFilterInfo;
				}
				set
				{
					this.m_reportRuntimeUserSortFilterInfo = value;
				}
			}

			// Token: 0x17002B96 RID: 11158
			// (get) Token: 0x06008E2D RID: 36397 RVA: 0x0024430A File Offset: 0x0024250A
			internal CreateAndRegisterStream CreateStreamCallback
			{
				get
				{
					return this.m_createStreamCallback;
				}
			}

			// Token: 0x17002B97 RID: 11159
			// (get) Token: 0x06008E2E RID: 36398 RVA: 0x00244312 File Offset: 0x00242512
			internal ReportProcessingContext ExternalProcessingContext
			{
				get
				{
					return this.m_externalProcessingContext;
				}
			}

			// Token: 0x17002B98 RID: 11160
			// (get) Token: 0x06008E2F RID: 36399 RVA: 0x0024431A File Offset: 0x0024251A
			// (set) Token: 0x06008E30 RID: 36400 RVA: 0x00244335 File Offset: 0x00242535
			internal OnDemandProcessingContext.CustomReportItemControls CriProcessingControls
			{
				get
				{
					if (this.m_criControls == null)
					{
						this.m_criControls = new OnDemandProcessingContext.CustomReportItemControls();
					}
					return this.m_criControls;
				}
				set
				{
					this.m_criControls = value;
				}
			}

			// Token: 0x17002B99 RID: 11161
			// (get) Token: 0x06008E31 RID: 36401 RVA: 0x0024433E File Offset: 0x0024253E
			internal bool EnableDataBackedParameters
			{
				get
				{
					return this.m_enableDataBackedParameters;
				}
			}

			// Token: 0x17002B9A RID: 11162
			// (get) Token: 0x06008E32 RID: 36402 RVA: 0x00244346 File Offset: 0x00242546
			internal IJobContext JobContext
			{
				get
				{
					return this.m_jobContext;
				}
			}

			// Token: 0x17002B9B RID: 11163
			// (get) Token: 0x06008E33 RID: 36403 RVA: 0x0024434E File Offset: 0x0024254E
			internal IExtensionFactory ExtFactory
			{
				get
				{
					return this.m_extFactory;
				}
			}

			// Token: 0x17002B9C RID: 11164
			// (get) Token: 0x06008E34 RID: 36404 RVA: 0x00244356 File Offset: 0x00242556
			internal IDataProtection DataProtection
			{
				get
				{
					return this.m_dataProtection;
				}
			}

			// Token: 0x17002B9D RID: 11165
			// (get) Token: 0x06008E35 RID: 36405 RVA: 0x0024435E File Offset: 0x0024255E
			internal ExecutionLogContext ExecutionLogContext
			{
				get
				{
					return this.m_executionLogContext;
				}
			}

			// Token: 0x17002B9E RID: 11166
			// (get) Token: 0x06008E36 RID: 36406 RVA: 0x00244366 File Offset: 0x00242566
			internal RuntimeDataSourceInfoCollection DataSourceInfos
			{
				get
				{
					return this.m_dataSourceInfos;
				}
			}

			// Token: 0x17002B9F RID: 11167
			// (get) Token: 0x06008E37 RID: 36407 RVA: 0x0024436E File Offset: 0x0024256E
			internal RuntimeDataSetInfoCollection SharedDataSetReferences
			{
				get
				{
					return this.m_sharedDataSetReferences;
				}
			}

			// Token: 0x17002BA0 RID: 11168
			// (get) Token: 0x06008E38 RID: 36408 RVA: 0x00244376 File Offset: 0x00242576
			internal IProcessingDataExtensionConnection CreateAndSetupDataExtensionFunction
			{
				get
				{
					return this.m_createAndSetupDataExtensionFunction;
				}
			}

			// Token: 0x17002BA1 RID: 11169
			// (get) Token: 0x06008E39 RID: 36409 RVA: 0x0024437E File Offset: 0x0024257E
			internal IConfiguration Configuration
			{
				get
				{
					return this.m_configuration;
				}
			}

			// Token: 0x17002BA2 RID: 11170
			// (get) Token: 0x06008E3A RID: 36410 RVA: 0x00244386 File Offset: 0x00242586
			internal ReportProcessing.DataSourceInfoHashtable GlobalDataSourceInfo
			{
				get
				{
					return this.m_globalDataSourceInfo;
				}
			}

			// Token: 0x17002BA3 RID: 11171
			// (get) Token: 0x06008E3B RID: 36411 RVA: 0x0024438E File Offset: 0x0024258E
			internal AbortHelper AbortInfo
			{
				get
				{
					return this.m_abortInfo;
				}
			}

			// Token: 0x17002BA4 RID: 11172
			// (get) Token: 0x06008E3C RID: 36412 RVA: 0x00244396 File Offset: 0x00242596
			// (set) Token: 0x06008E3D RID: 36413 RVA: 0x0024439E File Offset: 0x0024259E
			internal uint LanguageInstanceId
			{
				get
				{
					return this.m_languageInstanceId;
				}
				set
				{
					this.m_languageInstanceId = value;
				}
			}

			// Token: 0x17002BA5 RID: 11173
			// (get) Token: 0x06008E3E RID: 36414 RVA: 0x002443A7 File Offset: 0x002425A7
			internal UserProfileState HasUserProfileState
			{
				get
				{
					return this.m_hasUserProfileState;
				}
			}

			// Token: 0x17002BA6 RID: 11174
			// (get) Token: 0x06008E3F RID: 36415 RVA: 0x002443AF File Offset: 0x002425AF
			// (set) Token: 0x06008E40 RID: 36416 RVA: 0x002443B7 File Offset: 0x002425B7
			internal bool HasRenderFormatDependencyInDocumentMap
			{
				get
				{
					return this.m_hasRenderFormatDependencyInDocumentMap;
				}
				set
				{
					this.m_hasRenderFormatDependencyInDocumentMap = value;
				}
			}

			// Token: 0x17002BA7 RID: 11175
			// (get) Token: 0x06008E41 RID: 36417 RVA: 0x002443C0 File Offset: 0x002425C0
			internal OnDemandProcessingContext TopLevelContext
			{
				get
				{
					return this.m_topLevelContext;
				}
			}

			// Token: 0x17002BA8 RID: 11176
			// (get) Token: 0x06008E42 RID: 36418 RVA: 0x002443C8 File Offset: 0x002425C8
			internal OnDemandProcessingContext.Mode ContextMode
			{
				get
				{
					return this.m_contextMode;
				}
			}

			// Token: 0x17002BA9 RID: 11177
			// (get) Token: 0x06008E43 RID: 36419 RVA: 0x002443D0 File Offset: 0x002425D0
			internal ImageCacheManager ImageCacheManager
			{
				get
				{
					return this.m_imageCacheManager;
				}
			}

			// Token: 0x17002BAA RID: 11178
			// (get) Token: 0x06008E44 RID: 36420 RVA: 0x002443D8 File Offset: 0x002425D8
			internal bool UsePreviewCommands
			{
				get
				{
					return this.m_usePreviewCommands;
				}
			}

			// Token: 0x17002BAB RID: 11179
			// (get) Token: 0x06008E45 RID: 36421 RVA: 0x002443E0 File Offset: 0x002425E0
			internal bool CompareSafeExpressionsToLegacy
			{
				get
				{
					return this.m_compareSafeExpressionsToLegacy;
				}
			}

			// Token: 0x17002BAC RID: 11180
			// (get) Token: 0x06008E46 RID: 36422 RVA: 0x002443E8 File Offset: 0x002425E8
			internal bool UseUserLanguageForProcessing
			{
				get
				{
					return this.m_useUserLanguageForProcessing;
				}
			}

			// Token: 0x06008E47 RID: 36423 RVA: 0x002443F0 File Offset: 0x002425F0
			internal void MergeHasUserProfileState(UserProfileState newProfileStateFlags)
			{
				object hasUserProfileStateLock = this.m_hasUserProfileStateLock;
				lock (hasUserProfileStateLock)
				{
					this.m_hasUserProfileState |= newProfileStateFlags;
				}
			}

			// Token: 0x06008E48 RID: 36424 RVA: 0x00244438 File Offset: 0x00242638
			internal void UnregisterAbortInfo()
			{
				if (this.m_abortInfo != null && !this.m_abortInfoInherited)
				{
					this.m_abortInfo.Dispose();
					this.m_abortInfo = null;
				}
			}

			// Token: 0x06008E49 RID: 36425 RVA: 0x0024445C File Offset: 0x0024265C
			internal int CreateUniqueID()
			{
				int num = this.m_uniqueIDCounter + 1;
				this.m_uniqueIDCounter = num;
				return num;
			}

			// Token: 0x06008E4A RID: 36426 RVA: 0x0024447A File Offset: 0x0024267A
			internal EventInformation GetUserSortFilterInformation(out string sourceUniqueName)
			{
				sourceUniqueName = this.m_userSortFilterEventSourceUniqueName;
				if (this.m_newOdpSortEventInfo == null)
				{
					return null;
				}
				return new EventInformation
				{
					OdpSortInfo = this.m_newOdpSortEventInfo
				};
			}

			// Token: 0x06008E4B RID: 36427 RVA: 0x002444A0 File Offset: 0x002426A0
			internal void MergeNewUserSortFilterInformation()
			{
				int num = ((this.m_reportRuntimeUserSortFilterInfo == null) ? 0 : this.m_reportRuntimeUserSortFilterInfo.Count);
				if (num == 0)
				{
					return;
				}
				if (this.m_newOdpSortEventInfo == null)
				{
					this.m_newOdpSortEventInfo = new EventInformation.OdpSortEventInfo();
				}
				for (int i = 0; i < num; i++)
				{
					IReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo> reference = this.m_reportRuntimeUserSortFilterInfo[i];
					using (reference.PinValue())
					{
						Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
						if (runtimeSortFilterEventInfo.NewUniqueName == null)
						{
							runtimeSortFilterEventInfo.NewUniqueName = runtimeSortFilterEventInfo.OldUniqueName;
						}
						Hashtable hashtable = null;
						if (runtimeSortFilterEventInfo.PeerSortFilters != null)
						{
							int count = runtimeSortFilterEventInfo.PeerSortFilters.Count;
							if (count > 0)
							{
								hashtable = new Hashtable(count);
								IDictionaryEnumerator enumerator = runtimeSortFilterEventInfo.PeerSortFilters.GetEnumerator();
								while (enumerator.MoveNext())
								{
									if (enumerator.Value != null)
									{
										hashtable.Add(enumerator.Value, null);
									}
								}
							}
						}
						this.m_newOdpSortEventInfo.Add(runtimeSortFilterEventInfo.NewUniqueName, runtimeSortFilterEventInfo.SortDirection, hashtable);
						if (runtimeSortFilterEventInfo.OldUniqueName == this.m_userSortFilterEventSourceUniqueName)
						{
							this.m_userSortFilterEventSourceUniqueName = runtimeSortFilterEventInfo.NewUniqueName;
						}
					}
				}
				this.m_reportRuntimeUserSortFilterInfo = null;
			}

			// Token: 0x06008E4C RID: 36428 RVA: 0x002445D8 File Offset: 0x002427D8
			internal void TraceOneTimeWarning(ProcessingErrorCode errorCode, ICatalogItemContext itemContext)
			{
				if (this.m_hasTracedOneTimeMessage == null)
				{
					return;
				}
				if (!this.m_hasTracedOneTimeMessage.ContainsKey(errorCode))
				{
					string itemPathAsString = itemContext.ItemPathAsString;
					if (errorCode <= ProcessingErrorCode.rsSandboxingStringResultExceedsMaximumLength)
					{
						if (errorCode == ProcessingErrorCode.rsWarningFetchingExternalImages)
						{
							Global.Tracer.Trace(TraceLevel.Info, "There was an error loading the external image", new object[] { itemPathAsString });
							goto IL_010C;
						}
						if (errorCode == ProcessingErrorCode.rsSandboxingStringResultExceedsMaximumLength)
						{
							Global.Tracer.Trace(TraceLevel.Info, "RDL Sandboxing: Item: '{0}' attempted to use a String that violated the maximum allowed length.", new object[] { itemPathAsString });
							goto IL_010C;
						}
					}
					else
					{
						if (errorCode == ProcessingErrorCode.rsSandboxingArrayResultExceedsMaximumLength)
						{
							Global.Tracer.Trace(TraceLevel.Info, "RDL Sandboxing: Item: '{0}' attempted to use an array that violated the maximum allowed length.", new object[] { itemPathAsString });
							goto IL_010C;
						}
						if (errorCode == ProcessingErrorCode.rsSandboxingExternalResourceExceedsMaximumSize)
						{
							Global.Tracer.Trace(TraceLevel.Info, "RDL Sandboxing: Item: '{0}' attempted to reference an external resource larger than the maximum allowed size.", new object[] { itemPathAsString });
							goto IL_010C;
						}
						if (errorCode == ProcessingErrorCode.rsRenderingChunksUnavailable)
						{
							Global.Tracer.Trace(TraceLevel.Info, "A rendering extension attempted to use Report.GetOrCreateChunk or Report.CreateChunk while rendering item '{0}'. Rendering chunks are not available using the current report execution method.", new object[] { itemPathAsString });
							goto IL_010C;
						}
					}
					Global.Tracer.Assert(false, "Invalid error code: '{0}'.  Expected an error code", new object[] { errorCode });
					IL_010C:
					this.m_hasTracedOneTimeMessage[errorCode] = true;
				}
			}

			// Token: 0x04004FD1 RID: 20433
			private readonly bool m_enableDataBackedParameters;

			// Token: 0x04004FD2 RID: 20434
			private readonly IChunkFactory m_chunkFactory;

			// Token: 0x04004FD3 RID: 20435
			private readonly ReportProcessing.OnDemandSubReportCallback m_subReportCallback;

			// Token: 0x04004FD4 RID: 20436
			private readonly IGetResource m_getResourceCallback;

			// Token: 0x04004FD5 RID: 20437
			private readonly ReportProcessing.StoreServerParameters m_storeServerParameters;

			// Token: 0x04004FD6 RID: 20438
			private readonly ReportRuntimeSetup m_reportRuntimeSetup;

			// Token: 0x04004FD7 RID: 20439
			private readonly UserProfileState m_allowUserProfileState;

			// Token: 0x04004FD8 RID: 20440
			private readonly string m_requestUserName;

			// Token: 0x04004FD9 RID: 20441
			private readonly CultureInfo m_userLanguage;

			// Token: 0x04004FDA RID: 20442
			private readonly DateTime m_executionTime;

			// Token: 0x04004FDB RID: 20443
			private readonly bool m_reprocessSnapshot;

			// Token: 0x04004FDC RID: 20444
			private readonly bool m_processWithCachedData;

			// Token: 0x04004FDD RID: 20445
			private int m_uniqueIDCounter;

			// Token: 0x04004FDE RID: 20446
			private EventInformation m_userSortFilterInfo;

			// Token: 0x04004FDF RID: 20447
			private SortFilterEventInfoMap m_oldSortFilterEventInfo;

			// Token: 0x04004FE0 RID: 20448
			private SortFilterEventInfoMap m_newSortFilterEventInfo;

			// Token: 0x04004FE1 RID: 20449
			private List<IReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo>> m_reportRuntimeUserSortFilterInfo;

			// Token: 0x04004FE2 RID: 20450
			private EventInformation.OdpSortEventInfo m_newOdpSortEventInfo;

			// Token: 0x04004FE3 RID: 20451
			private string m_userSortFilterEventSourceUniqueName;

			// Token: 0x04004FE4 RID: 20452
			private readonly CreateAndRegisterStream m_createStreamCallback;

			// Token: 0x04004FE5 RID: 20453
			private OnDemandProcessingContext.CustomReportItemControls m_criControls;

			// Token: 0x04004FE6 RID: 20454
			private bool m_usePreviewCommands;

			// Token: 0x04004FE7 RID: 20455
			private readonly IJobContext m_jobContext;

			// Token: 0x04004FE8 RID: 20456
			private readonly IExtensionFactory m_extFactory;

			// Token: 0x04004FE9 RID: 20457
			private readonly IDataProtection m_dataProtection;

			// Token: 0x04004FEA RID: 20458
			private readonly ExecutionLogContext m_executionLogContext;

			// Token: 0x04004FEB RID: 20459
			private readonly RuntimeDataSourceInfoCollection m_dataSourceInfos;

			// Token: 0x04004FEC RID: 20460
			private readonly RuntimeDataSetInfoCollection m_sharedDataSetReferences;

			// Token: 0x04004FED RID: 20461
			private readonly IProcessingDataExtensionConnection m_createAndSetupDataExtensionFunction;

			// Token: 0x04004FEE RID: 20462
			private readonly IConfiguration m_configuration;

			// Token: 0x04004FEF RID: 20463
			private readonly Dictionary<ProcessingErrorCode, bool> m_hasTracedOneTimeMessage;

			// Token: 0x04004FF0 RID: 20464
			private readonly ReportProcessing.DataSourceInfoHashtable m_globalDataSourceInfo;

			// Token: 0x04004FF1 RID: 20465
			private readonly ReportProcessingContext m_externalProcessingContext;

			// Token: 0x04004FF2 RID: 20466
			private AbortHelper m_abortInfo;

			// Token: 0x04004FF3 RID: 20467
			private readonly bool m_abortInfoInherited;

			// Token: 0x04004FF4 RID: 20468
			private uint m_languageInstanceId;

			// Token: 0x04004FF5 RID: 20469
			[NonSerialized]
			private readonly object m_hasUserProfileStateLock = new object();

			// Token: 0x04004FF6 RID: 20470
			private UserProfileState m_hasUserProfileState;

			// Token: 0x04004FF7 RID: 20471
			private bool m_hasRenderFormatDependencyInDocumentMap;

			// Token: 0x04004FF8 RID: 20472
			private readonly OnDemandProcessingContext m_topLevelContext;

			// Token: 0x04004FF9 RID: 20473
			private readonly OnDemandProcessingContext.Mode m_contextMode;

			// Token: 0x04004FFA RID: 20474
			private readonly ImageCacheManager m_imageCacheManager;

			// Token: 0x04004FFB RID: 20475
			private readonly bool m_compareSafeExpressionsToLegacy;

			// Token: 0x04004FFC RID: 20476
			private readonly bool m_useUserLanguageForProcessing;
		}

		// Token: 0x02000CF9 RID: 3321
		internal sealed class CustomReportItemControls
		{
			// Token: 0x06008E4D RID: 36429 RVA: 0x002446FE File Offset: 0x002428FE
			internal CustomReportItemControls()
			{
				this.m_controls = new Hashtable();
			}

			// Token: 0x06008E4E RID: 36430 RVA: 0x00244714 File Offset: 0x00242914
			internal ICustomReportItem GetControlInstance(string name, IExtensionFactory extFactory)
			{
				ICustomReportItem customReportItem2;
				lock (this)
				{
					OnDemandProcessingContext.CustomReportItemControls.CustomControlInfo customControlInfo = this.m_controls[name] as OnDemandProcessingContext.CustomReportItemControls.CustomControlInfo;
					if (customControlInfo == null)
					{
						Global.Tracer.Assert(extFactory != null, "extFactory != null");
						ICustomReportItem customReportItem = extFactory.GetNewCustomReportItemProcessingInstanceClass(name) as ICustomReportItem;
						customControlInfo = new OnDemandProcessingContext.CustomReportItemControls.CustomControlInfo();
						customControlInfo.Instance = customReportItem;
						customControlInfo.IsValid = customReportItem != null;
						this.m_controls.Add(name, customControlInfo);
					}
					Global.Tracer.Assert(customControlInfo != null, "(null != info)");
					if (customControlInfo.IsValid)
					{
						customReportItem2 = customControlInfo.Instance;
					}
					else
					{
						customReportItem2 = null;
					}
				}
				return customReportItem2;
			}

			// Token: 0x04004FFD RID: 20477
			private Hashtable m_controls;

			// Token: 0x02000D4D RID: 3405
			private class CustomControlInfo
			{
				// Token: 0x17002C1F RID: 11295
				// (get) Token: 0x06008FDF RID: 36831 RVA: 0x00247EF2 File Offset: 0x002460F2
				// (set) Token: 0x06008FE0 RID: 36832 RVA: 0x00247EFA File Offset: 0x002460FA
				internal bool IsValid
				{
					get
					{
						return this.m_valid;
					}
					set
					{
						this.m_valid = value;
					}
				}

				// Token: 0x17002C20 RID: 11296
				// (get) Token: 0x06008FE1 RID: 36833 RVA: 0x00247F03 File Offset: 0x00246103
				// (set) Token: 0x06008FE2 RID: 36834 RVA: 0x00247F0B File Offset: 0x0024610B
				internal ICustomReportItem Instance
				{
					get
					{
						return this.m_instance;
					}
					set
					{
						this.m_instance = value;
					}
				}

				// Token: 0x040050FE RID: 20734
				private bool m_valid;

				// Token: 0x040050FF RID: 20735
				private ICustomReportItem m_instance;
			}
		}
	}
}
