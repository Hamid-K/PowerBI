using System;
using Microsoft.ReportingServices.OnDemandProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000605 RID: 1541
	public abstract class OnDemandProcessingResult
	{
		// Token: 0x0600551A RID: 21786 RVA: 0x001672BC File Offset: 0x001654BC
		protected OnDemandProcessingResult(IChunkFactory createChunkFactory, bool hasDocumentMap, bool hasInteractivity, ParameterInfoCollection parameters, int autoRefresh, int numberOfPages, ProcessingMessageList warnings, bool eventInfoChanged, EventInformation newEventInfo, PaginationMode updatedPaginationMode, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
		{
			this.m_createChunkFactory = createChunkFactory;
			this.m_hasDocumentMap = hasDocumentMap;
			this.m_numberOfPages = numberOfPages;
			this.m_hasInteractivity = hasInteractivity;
			this.m_parameters = parameters;
			this.m_autoRefresh = autoRefresh;
			this.m_warnings = warnings;
			this.m_eventInfoChanged = eventInfoChanged;
			this.m_newEventInfo = newEventInfo;
			this.m_parameters = parameters;
			this.m_updatedPaginationMode = updatedPaginationMode;
			this.m_updatedReportProcessingFlags = updatedProcessingFlags;
			this.m_usedUserProfileState = usedUserProfileState;
			this.m_executionLogContext = executionLogContext;
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x0600551B RID: 21787
		public abstract bool SnapshotChanged { get; }

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x0016733C File Offset: 0x0016553C
		public bool HasInteractivity
		{
			get
			{
				return this.m_hasInteractivity;
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x00167344 File Offset: 0x00165544
		public bool HasDocumentMap
		{
			get
			{
				return this.m_hasDocumentMap;
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x0016734C File Offset: 0x0016554C
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x00167354 File Offset: 0x00165554
		public int AutoRefresh
		{
			get
			{
				return this.m_autoRefresh;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x0016735C File Offset: 0x0016555C
		public ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x06005521 RID: 21793 RVA: 0x00167364 File Offset: 0x00165564
		public int NumberOfPages
		{
			get
			{
				return this.m_numberOfPages;
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x0016736C File Offset: 0x0016556C
		public bool EventInfoChanged
		{
			get
			{
				return this.m_eventInfoChanged;
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x06005523 RID: 21795 RVA: 0x00167374 File Offset: 0x00165574
		public EventInformation NewEventInfo
		{
			get
			{
				return this.m_newEventInfo;
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x0016737C File Offset: 0x0016557C
		public PaginationMode UpdatedPaginationMode
		{
			get
			{
				return this.m_updatedPaginationMode;
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x00167384 File Offset: 0x00165584
		public ReportProcessingFlags UpdatedReportProcessingFlags
		{
			get
			{
				return this.m_updatedReportProcessingFlags;
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x06005526 RID: 21798 RVA: 0x0016738C File Offset: 0x0016558C
		public UserProfileState UsedUserProfileState
		{
			get
			{
				return this.m_usedUserProfileState;
			}
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x06005527 RID: 21799 RVA: 0x00167394 File Offset: 0x00165594
		public ExecutionLogContext ExecutionLogContext
		{
			get
			{
				return this.m_executionLogContext;
			}
		}

		// Token: 0x06005528 RID: 21800
		public abstract void Save();

		// Token: 0x04002D06 RID: 11526
		private readonly bool m_hasInteractivity;

		// Token: 0x04002D07 RID: 11527
		private readonly ParameterInfoCollection m_parameters;

		// Token: 0x04002D08 RID: 11528
		private readonly int m_autoRefresh;

		// Token: 0x04002D09 RID: 11529
		private readonly ProcessingMessageList m_warnings;

		// Token: 0x04002D0A RID: 11530
		private readonly int m_numberOfPages;

		// Token: 0x04002D0B RID: 11531
		private readonly bool m_hasDocumentMap;

		// Token: 0x04002D0C RID: 11532
		protected readonly IChunkFactory m_createChunkFactory;

		// Token: 0x04002D0D RID: 11533
		private readonly bool m_eventInfoChanged;

		// Token: 0x04002D0E RID: 11534
		private readonly EventInformation m_newEventInfo;

		// Token: 0x04002D0F RID: 11535
		private readonly PaginationMode m_updatedPaginationMode;

		// Token: 0x04002D10 RID: 11536
		private readonly ReportProcessingFlags m_updatedReportProcessingFlags;

		// Token: 0x04002D11 RID: 11537
		private readonly UserProfileState m_usedUserProfileState;

		// Token: 0x04002D12 RID: 11538
		private readonly ExecutionLogContext m_executionLogContext;
	}
}
