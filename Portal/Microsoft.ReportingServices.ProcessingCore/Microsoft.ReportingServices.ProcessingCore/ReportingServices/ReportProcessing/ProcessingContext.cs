using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000639 RID: 1593
	public abstract class ProcessingContext
	{
		// Token: 0x06005727 RID: 22311 RVA: 0x0016F1E4 File Offset: 0x0016D3E4
		internal ProcessingContext(ICatalogItemContext reportContext, string requestUserName, ParameterInfoCollection parameters, ReportProcessing.OnDemandSubReportCallback subReportCallback, IGetResource getResourceFunction, IChunkFactory createChunkFactory, ReportProcessing.ExecutionType interactiveExecution, CultureInfo culture, UserProfileState allowUserProfileState, UserProfileState initialUserProfileState, ReportRuntimeSetup reportRuntimeSetup, CreateAndRegisterStream createStreamCallback, bool isHistorySnapshot, IJobContext jobContext, IExtensionFactory extFactory, IDataProtection dataProtection)
		{
			Global.Tracer.Assert(reportContext != null, "(null != reportContext)");
			this.m_reportContext = reportContext;
			this.m_requestUserName = requestUserName;
			this.m_parameters = parameters;
			this.m_queryParameters = this.m_parameters.GetQueryParameters();
			this.m_subReportCallback = subReportCallback;
			this.m_getResourceFunction = getResourceFunction;
			this.m_chunkFactory = createChunkFactory;
			this.m_interactiveExecution = interactiveExecution;
			this.m_userLanguage = culture;
			this.m_allowUserProfileState = allowUserProfileState;
			this.m_initialUserProfileState = initialUserProfileState;
			this.m_reportRuntimeSetup = reportRuntimeSetup;
			this.m_createStreamCallback = createStreamCallback;
			this.m_isHistorySnapshot = isHistorySnapshot;
			ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(this.m_chunkFactory);
			this.m_createReportChunkCallback = new ReportProcessing.CreateReportChunk(chunkFactoryAdapter.CreateReportChunk);
			this.m_jobContext = jobContext;
			this.m_extFactory = extFactory;
			this.m_dataProtection = dataProtection;
		}

		// Token: 0x06005728 RID: 22312
		internal abstract ReportProcessing.ProcessingContext CreateInternalProcessingContext(string chartName, Report report, ErrorContext errorContext, DateTime executionTime, UserProfileState allowUserProfileState, bool isHistorySnapshot, bool snapshotProcessing, bool processWithCachedData, ReportProcessing.GetReportChunk getChunkCallback, ReportProcessing.CreateReportChunk cacheDataCallback);

		// Token: 0x06005729 RID: 22313
		internal abstract ReportProcessing.ProcessingContext ParametersInternalProcessingContext(ErrorContext errorContext, DateTime executionTimeStamp, bool isSnapshot);

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x0600572A RID: 22314
		internal abstract bool EnableDataBackedParameters { get; }

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x0600572B RID: 22315 RVA: 0x0016F2B6 File Offset: 0x0016D4B6
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x0600572C RID: 22316 RVA: 0x0016F2BE File Offset: 0x0016D4BE
		internal string RequestUserName
		{
			get
			{
				return this.m_requestUserName;
			}
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x0600572D RID: 22317 RVA: 0x0016F2C6 File Offset: 0x0016D4C6
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x0016F2CE File Offset: 0x0016D4CE
		internal ReportProcessing.OnDemandSubReportCallback OnDemandSubReportCallback
		{
			get
			{
				return this.m_subReportCallback;
			}
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x0600572F RID: 22319 RVA: 0x0016F2D6 File Offset: 0x0016D4D6
		// (set) Token: 0x06005730 RID: 22320 RVA: 0x0016F2DE File Offset: 0x0016D4DE
		internal ReportProcessing.CreateReportChunk CreateReportChunkCallback
		{
			get
			{
				return this.m_createReportChunkCallback;
			}
			set
			{
				this.m_createReportChunkCallback = value;
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x06005731 RID: 22321 RVA: 0x0016F2E7 File Offset: 0x0016D4E7
		// (set) Token: 0x06005732 RID: 22322 RVA: 0x0016F2F0 File Offset: 0x0016D4F0
		public IChunkFactory ChunkFactory
		{
			get
			{
				return this.m_chunkFactory;
			}
			set
			{
				this.m_chunkFactory = value;
				ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(this.m_chunkFactory);
				this.m_createReportChunkCallback = new ReportProcessing.CreateReportChunk(chunkFactoryAdapter.CreateReportChunk);
			}
		}

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06005733 RID: 22323 RVA: 0x0016F322 File Offset: 0x0016D522
		internal IGetResource GetResourceCallback
		{
			get
			{
				return this.m_getResourceFunction;
			}
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x0016F32A File Offset: 0x0016D52A
		internal ReportProcessing.ExecutionType InteractiveExecution
		{
			get
			{
				return this.m_interactiveExecution;
			}
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x0016F332 File Offset: 0x0016D532
		internal CultureInfo UserLanguage
		{
			get
			{
				return this.m_userLanguage;
			}
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x0016F33A File Offset: 0x0016D53A
		internal UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_allowUserProfileState;
			}
		}

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x06005737 RID: 22327 RVA: 0x0016F342 File Offset: 0x0016D542
		internal UserProfileState InitialUserProfileState
		{
			get
			{
				return this.m_initialUserProfileState;
			}
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x0016F34A File Offset: 0x0016D54A
		internal ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_reportRuntimeSetup;
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x0016F352 File Offset: 0x0016D552
		internal bool IsHistorySnapshot
		{
			get
			{
				return this.m_isHistorySnapshot;
			}
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x0016F35A File Offset: 0x0016D55A
		internal ReportProcessingFlags ReportProcessingFlags
		{
			get
			{
				if (this.m_chunkFactory == null)
				{
					return ReportProcessingFlags.NotSet;
				}
				return this.m_chunkFactory.ReportProcessingFlags;
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x0600573B RID: 22331
		internal abstract IProcessingDataExtensionConnection CreateAndSetupDataExtensionFunction { get; }

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x0600573C RID: 22332
		internal abstract RuntimeDataSourceInfoCollection DataSources { get; }

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x0016F371 File Offset: 0x0016D571
		internal virtual RuntimeDataSetInfoCollection SharedDataSetReferences
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x0600573E RID: 22334
		internal abstract bool CanShareDataSets { get; }

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x0016F374 File Offset: 0x0016D574
		internal CreateAndRegisterStream CreateStreamCallback
		{
			get
			{
				return this.m_createStreamCallback;
			}
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x0016F37C File Offset: 0x0016D57C
		public ParameterInfoCollection QueryParameters
		{
			get
			{
				return this.m_queryParameters;
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x0016F384 File Offset: 0x0016D584
		public IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06005742 RID: 22338 RVA: 0x0016F38C File Offset: 0x0016D58C
		public IExtensionFactory ExtFactory
		{
			get
			{
				return this.m_extFactory;
			}
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x0016F394 File Offset: 0x0016D594
		public IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x0016F39C File Offset: 0x0016D59C
		// (set) Token: 0x06005745 RID: 22341 RVA: 0x0016F3A4 File Offset: 0x0016D5A4
		public bool UsePreviewCommands
		{
			get
			{
				return this.m_usePreviewCommands;
			}
			set
			{
				this.m_usePreviewCommands = value;
			}
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x06005746 RID: 22342 RVA: 0x0016F3AD File Offset: 0x0016D5AD
		// (set) Token: 0x06005747 RID: 22343 RVA: 0x0016F3B5 File Offset: 0x0016D5B5
		public bool CompareSafeExpressionsToLegacy
		{
			get
			{
				return this.m_compareSafeExpressionsToLegacy;
			}
			set
			{
				this.m_compareSafeExpressionsToLegacy = value;
			}
		}

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06005748 RID: 22344 RVA: 0x0016F3BE File Offset: 0x0016D5BE
		// (set) Token: 0x06005749 RID: 22345 RVA: 0x0016F3C6 File Offset: 0x0016D5C6
		public bool UseUserLanguageForProcessing
		{
			get
			{
				return this.m_useUserLanguageForProcessing;
			}
			set
			{
				this.m_useUserLanguageForProcessing = value;
			}
		}

		// Token: 0x04002E13 RID: 11795
		private ICatalogItemContext m_reportContext;

		// Token: 0x04002E14 RID: 11796
		private string m_requestUserName;

		// Token: 0x04002E15 RID: 11797
		private ParameterInfoCollection m_parameters;

		// Token: 0x04002E16 RID: 11798
		private ParameterInfoCollection m_queryParameters;

		// Token: 0x04002E17 RID: 11799
		private ReportProcessing.OnDemandSubReportCallback m_subReportCallback;

		// Token: 0x04002E18 RID: 11800
		private IGetResource m_getResourceFunction;

		// Token: 0x04002E19 RID: 11801
		private ReportProcessing.ExecutionType m_interactiveExecution;

		// Token: 0x04002E1A RID: 11802
		private CultureInfo m_userLanguage;

		// Token: 0x04002E1B RID: 11803
		private UserProfileState m_allowUserProfileState;

		// Token: 0x04002E1C RID: 11804
		private UserProfileState m_initialUserProfileState;

		// Token: 0x04002E1D RID: 11805
		private ReportRuntimeSetup m_reportRuntimeSetup;

		// Token: 0x04002E1E RID: 11806
		private bool m_isHistorySnapshot;

		// Token: 0x04002E1F RID: 11807
		private IChunkFactory m_chunkFactory;

		// Token: 0x04002E20 RID: 11808
		private CreateAndRegisterStream m_createStreamCallback;

		// Token: 0x04002E21 RID: 11809
		private IJobContext m_jobContext;

		// Token: 0x04002E22 RID: 11810
		private IExtensionFactory m_extFactory;

		// Token: 0x04002E23 RID: 11811
		private IDataProtection m_dataProtection;

		// Token: 0x04002E24 RID: 11812
		private bool m_usePreviewCommands;

		// Token: 0x04002E25 RID: 11813
		private bool m_compareSafeExpressionsToLegacy;

		// Token: 0x04002E26 RID: 11814
		private bool m_useUserLanguageForProcessing;

		// Token: 0x04002E27 RID: 11815
		private ReportProcessing.CreateReportChunk m_createReportChunkCallback;
	}
}
