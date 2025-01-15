using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000107 RID: 263
	internal sealed class SortAction : ReportProcessingEvent<SortAction.InputParameters, SortAction.OutputParameters>
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x00027C98 File Offset: 0x00025E98
		public SortAction(RSService service, ClientRequest session)
			: base(session, service, null)
		{
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool RequiresItemContext
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001031C File Offset: 0x0000E51C
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.Sort;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00027CA4 File Offset: 0x00025EA4
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
			if (base.WriteEventParameters)
			{
				execInfo.AdditionalInfo.SortItem = base.EventParameter.SortItem;
				execInfo.AdditionalInfo.Direction = base.EventParameter.Direction.ToString();
				execInfo.AdditionalInfo.ClearExistingSort = base.EventParameter.ClearExistingSort.ToString();
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00027D18 File Offset: 0x00025F18
		protected override SortAction.OutputParameters Event()
		{
			ReportSnapshot reportSnapshot = base.Service.AllocateNewReportSnapshot(false, base.Session.SessionReport.Report.EffectiveParams, base.Session.SessionReport.ExecutionDateTime, base.Session.SessionReport.Report.Description, base.Session.SessionReport.Report.SnapshotData.ProcessingFlags);
			SortAction.OutputParameters outputParameters;
			try
			{
				base.Session.SessionReport.Report.SnapshotData.PrepareExecutionSnapshot(reportSnapshot, null);
				int num;
				string text;
				OnDemandProcessingResult onDemandProcessingResult = this.Sort(reportSnapshot, out num, out text);
				if (onDemandProcessingResult != null)
				{
					if (onDemandProcessingResult.EventInfoChanged)
					{
						base.Session.SessionReport.EventInfo = onDemandProcessingResult.NewEventInfo;
					}
					if (onDemandProcessingResult.SnapshotChanged)
					{
						base.Session.SessionReport.Report.SnapshotData = reportSnapshot;
						base.Session.SessionReport.ProcessingResult = onDemandProcessingResult;
						base.Session.SessionReport.HasDocumentMap = onDemandProcessingResult.HasDocumentMap;
						RSTrace.CatalogTrace.Assert(onDemandProcessingResult.NumberOfPages != 0, "processingResult.NumberOfPages");
						base.Session.SessionReport.PageCount = onDemandProcessingResult.NumberOfPages;
						base.Session.SessionReport.PaginationMode = onDemandProcessingResult.UpdatedPaginationMode;
						base.Service.Storage.PromoteSnapshotInfo(reportSnapshot, onDemandProcessingResult.NumberOfPages, onDemandProcessingResult.HasDocumentMap, onDemandProcessingResult.UpdatedPaginationMode, onDemandProcessingResult.UpdatedReportProcessingFlags);
						base.Service.Storage.Commit();
						base.Session.SessionReport.FoundInCache = false;
						base.Session.SessionReport.Save(SessionReportItem.SaveAction.SaveSnapshot);
					}
					else
					{
						reportSnapshot.DeleteSnapshotAndChunks();
					}
				}
				ExecutionInfo3 executionInfo = ExecutionInfo.ConstructFromUserSession(base.Session, base.ReportContext, base.Service);
				outputParameters = new SortAction.OutputParameters(num, text, executionInfo);
			}
			catch (Exception)
			{
				reportSnapshot.DeleteSnapshotAndChunks();
				throw;
			}
			return outputParameters;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00027F10 File Offset: 0x00026110
		private OnDemandProcessingResult Sort(ReportSnapshot newSnapshot, out int pageNumber, out string newReportItem)
		{
			OnDemandProcessingResult onDemandProcessingResult = null;
			ReportProcessing.ExecutionType executionType;
			using (SurrogateContextFactory.CreateContext(out executionType))
			{
				RSTrace.CatalogTrace.Assert(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext != null, "ProcessingContext.JobContext");
				ReportProcessingContext reportProcessingContext = new ReportProcessingContext(base.ReportContext, base.Service.UserName, base.Session.SessionReport.Report.EffectiveParams, null, null, null, new ServerGetResourceForProcessing(base.Service), newSnapshot, executionType, Localization.ClientPrimaryCulture, UserProfileState.Both, UserProfileState.None, new ServerDataExtensionConnection(base.Service.HowToCreateDataExtensionInstance, base.Service.UserContext, executionType, new ServerAdditionalToken(base.Service, base.ReportContext)), ReportRuntimeSetup.GetDefault(), new CreateAndRegisterStream(base.Service.StreamManager.GetNewStream), false, ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), new ServerExtensionFactory(), DataProtection.Instance, null);
				ServerParameterStore serverParameterStore = new ServerParameterStore(base.Service.Storage.ConnectionManager);
				RenderingContext renderingContext = new RenderingContext(base.ReportContext, base.Session.SessionReport.Report.Description, base.Session.SessionReport.EventInfo, ReportRuntimeSetup.GetDefault(), new ReportProcessing.StoreServerParameters(serverParameterStore.Store), UserProfileState.Both, base.EventParameter.RequestedPaginationMode, 0);
				newReportItem = null;
				pageNumber = 0;
				using (ISnapshotTransaction snapshotTransaction = newSnapshot.EnterTransactionContext())
				{
					IChunkFactory chunkFactory = ReadOnlyChunkFactory.FromSnapshot(base.Session.SessionReport.Report.SnapshotData);
					onDemandProcessingResult = base.ProcessingEngine.ProcessUserSortEvent(base.EventParameter.SortItem, SortDirection.SoapSortDirectionToSortOptions(base.EventParameter.Direction), base.EventParameter.ClearExistingSort, reportProcessingContext, renderingContext, chunkFactory, out newReportItem, out pageNumber);
					snapshotTransaction.Commit();
				}
			}
			return onDemandProcessingResult;
		}

		// Token: 0x02000466 RID: 1126
		public sealed class InputParameters
		{
			// Token: 0x06002366 RID: 9062 RVA: 0x00084AB8 File Offset: 0x00082CB8
			public InputParameters(string sortItem, SortDirectionEnum direction, bool clearExistingSort, PaginationMode requestedPaginationMode)
			{
				this.m_sortItem = sortItem;
				this.m_direction = direction;
				this.m_clearExistingSort = clearExistingSort;
				this.m_requestedPaginationMode = requestedPaginationMode;
			}

			// Token: 0x17000A67 RID: 2663
			// (get) Token: 0x06002367 RID: 9063 RVA: 0x00084ADD File Offset: 0x00082CDD
			public string SortItem
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_sortItem;
				}
			}

			// Token: 0x17000A68 RID: 2664
			// (get) Token: 0x06002368 RID: 9064 RVA: 0x00084AE5 File Offset: 0x00082CE5
			public SortDirectionEnum Direction
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_direction;
				}
			}

			// Token: 0x17000A69 RID: 2665
			// (get) Token: 0x06002369 RID: 9065 RVA: 0x00084AED File Offset: 0x00082CED
			public bool ClearExistingSort
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_clearExistingSort;
				}
			}

			// Token: 0x17000A6A RID: 2666
			// (get) Token: 0x0600236A RID: 9066 RVA: 0x00084AF5 File Offset: 0x00082CF5
			public PaginationMode RequestedPaginationMode
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_requestedPaginationMode;
				}
			}

			// Token: 0x04000FBA RID: 4026
			private readonly string m_sortItem;

			// Token: 0x04000FBB RID: 4027
			private readonly SortDirectionEnum m_direction;

			// Token: 0x04000FBC RID: 4028
			private readonly bool m_clearExistingSort;

			// Token: 0x04000FBD RID: 4029
			private readonly PaginationMode m_requestedPaginationMode;
		}

		// Token: 0x02000467 RID: 1127
		public sealed class OutputParameters
		{
			// Token: 0x0600236B RID: 9067 RVA: 0x00084AFD File Offset: 0x00082CFD
			public OutputParameters(int pageNumber, string reportItem, ExecutionInfo3 postSortExecutionInfo)
			{
				this.m_pageNumber = pageNumber;
				this.m_reportItem = reportItem;
				this.m_postSortExecutionInfo = postSortExecutionInfo;
			}

			// Token: 0x17000A6B RID: 2667
			// (get) Token: 0x0600236C RID: 9068 RVA: 0x00084B1A File Offset: 0x00082D1A
			public int PageNumber
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_pageNumber;
				}
			}

			// Token: 0x17000A6C RID: 2668
			// (get) Token: 0x0600236D RID: 9069 RVA: 0x00084B22 File Offset: 0x00082D22
			public string ReportItem
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_reportItem;
				}
			}

			// Token: 0x17000A6D RID: 2669
			// (get) Token: 0x0600236E RID: 9070 RVA: 0x00084B2A File Offset: 0x00082D2A
			public ExecutionInfo3 PostSortExecutionInfo
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_postSortExecutionInfo;
				}
			}

			// Token: 0x04000FBE RID: 4030
			private readonly int m_pageNumber;

			// Token: 0x04000FBF RID: 4031
			private readonly string m_reportItem;

			// Token: 0x04000FC0 RID: 4032
			private readonly ExecutionInfo3 m_postSortExecutionInfo;
		}
	}
}
