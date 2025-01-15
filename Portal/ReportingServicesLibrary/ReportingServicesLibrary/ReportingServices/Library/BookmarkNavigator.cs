using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C3 RID: 707
	internal sealed class BookmarkNavigator : SnapshotUpdatingEvent<string, BookmarkNavigator.Output>
	{
		// Token: 0x06001953 RID: 6483 RVA: 0x000664E6 File Offset: 0x000646E6
		public BookmarkNavigator(ClientRequest session, RSService service, string userName, CatalogItemContext reportContext)
			: base(session, service, reportContext)
		{
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000664F4 File Offset: 0x000646F4
		protected override BookmarkNavigator.Output RunEvent(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc)
		{
			string text;
			OnDemandProcessingResult onDemandProcessingResult;
			int num = base.ProcessingEngine.ProcessBookmarkNavigationEvent(base.EventParameter, base.Session.SessionReport.EventInfo, pc, out text, out onDemandProcessingResult);
			base.ProcessingResult = onDemandProcessingResult;
			return new BookmarkNavigator.Output(num, text);
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00010309 File Offset: 0x0000E509
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.BookmarkNavigation;
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00066534 File Offset: 0x00064734
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
			if (base.WriteEventParameters)
			{
				execInfo.AdditionalInfo.BookmarkId = base.EventParameter;
			}
		}

		// Token: 0x020004DA RID: 1242
		public sealed class Output
		{
			// Token: 0x06002480 RID: 9344 RVA: 0x000862BC File Offset: 0x000844BC
			public Output(int pageNumber, string uniqueName)
			{
				this.m_pageNumber = pageNumber;
				this.m_uniqueName = uniqueName;
			}

			// Token: 0x17000AA4 RID: 2724
			// (get) Token: 0x06002481 RID: 9345 RVA: 0x000862D2 File Offset: 0x000844D2
			public int PageNumber
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_pageNumber;
				}
			}

			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x06002482 RID: 9346 RVA: 0x000862DA File Offset: 0x000844DA
			public string UniqueName
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_uniqueName;
				}
			}

			// Token: 0x0400112C RID: 4396
			private readonly int m_pageNumber;

			// Token: 0x0400112D RID: 4397
			private readonly string m_uniqueName;
		}
	}
}
