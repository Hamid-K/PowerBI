using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200063C RID: 1596
	public sealed class RenderingContext
	{
		// Token: 0x06005761 RID: 22369 RVA: 0x0016F684 File Offset: 0x0016D884
		public RenderingContext(ICatalogItemContext reportContext, string reportDescription, EventInformation eventInfo, ReportRuntimeSetup reportRuntimeSetup, ReportProcessing.StoreServerParameters storeServerParameters, UserProfileState allowUserProfileState, PaginationMode clientPaginationMode, int previousTotalPages)
		{
			Global.Tracer.Assert(reportContext != null, "(null != reportContext)");
			this.m_reportContext = reportContext;
			this.m_reportDescription = reportDescription;
			this.m_eventInfo = eventInfo;
			this.m_storeServerParameters = storeServerParameters;
			this.m_allowUserProfileState = allowUserProfileState;
			this.m_reportRuntimeSetup = reportRuntimeSetup;
			this.m_clientPaginationMode = clientPaginationMode;
			this.m_previousTotalPages = previousTotalPages;
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x06005762 RID: 22370 RVA: 0x0016F6E7 File Offset: 0x0016D8E7
		internal string Format
		{
			get
			{
				return this.m_reportContext.RSRequestParameters.FormatParamValue;
			}
		}

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06005763 RID: 22371 RVA: 0x0016F6F9 File Offset: 0x0016D8F9
		internal Uri ReportUri
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_reportContext.HostRootUri))
				{
					return null;
				}
				return new Uri(new CatalogItemUrlBuilder(this.m_reportContext).ToString());
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06005764 RID: 22372 RVA: 0x0016F724 File Offset: 0x0016D924
		internal string ShowHideToggle
		{
			get
			{
				return this.m_reportContext.RSRequestParameters.ShowHideToggleParamValue;
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x0016F736 File Offset: 0x0016D936
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x0016F73E File Offset: 0x0016D93E
		internal string ReportDescription
		{
			get
			{
				return this.m_reportDescription;
			}
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x06005767 RID: 22375 RVA: 0x0016F746 File Offset: 0x0016D946
		// (set) Token: 0x06005768 RID: 22376 RVA: 0x0016F74E File Offset: 0x0016D94E
		internal EventInformation EventInfo
		{
			get
			{
				return this.m_eventInfo;
			}
			set
			{
				this.m_eventInfo = value;
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x0016F757 File Offset: 0x0016D957
		internal ReportProcessing.StoreServerParameters StoreServerParametersCallback
		{
			get
			{
				return this.m_storeServerParameters;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x0016F75F File Offset: 0x0016D95F
		internal UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_allowUserProfileState;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x0600576B RID: 22379 RVA: 0x0016F767 File Offset: 0x0016D967
		internal ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_reportRuntimeSetup;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x0016F76F File Offset: 0x0016D96F
		internal PaginationMode ClientPaginationMode
		{
			get
			{
				return this.m_clientPaginationMode;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x0600576D RID: 22381 RVA: 0x0016F777 File Offset: 0x0016D977
		internal int PreviousTotalPages
		{
			get
			{
				return this.m_previousTotalPages;
			}
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x0016F780 File Offset: 0x0016D980
		internal Hashtable GetRenderProperties(bool reprocessSnapshot)
		{
			Hashtable hashtable = new Hashtable(4);
			if (reprocessSnapshot)
			{
				hashtable.Add("ClientPaginationMode", this.m_clientPaginationMode);
				hashtable.Add("PreviousTotalPages", 0);
			}
			else
			{
				hashtable.Add("ClientPaginationMode", this.m_clientPaginationMode);
				hashtable.Add("PreviousTotalPages", this.m_previousTotalPages);
			}
			return hashtable;
		}

		// Token: 0x04002E32 RID: 11826
		private ICatalogItemContext m_reportContext;

		// Token: 0x04002E33 RID: 11827
		private string m_reportDescription;

		// Token: 0x04002E34 RID: 11828
		private EventInformation m_eventInfo;

		// Token: 0x04002E35 RID: 11829
		internal ReportProcessing.GetReportChunk m_getReportChunkCallback;

		// Token: 0x04002E36 RID: 11830
		internal ReportProcessing.GetChunkMimeType m_getChunkMimeType;

		// Token: 0x04002E37 RID: 11831
		private ReportProcessing.StoreServerParameters m_storeServerParameters;

		// Token: 0x04002E38 RID: 11832
		private UserProfileState m_allowUserProfileState;

		// Token: 0x04002E39 RID: 11833
		private ReportRuntimeSetup m_reportRuntimeSetup;

		// Token: 0x04002E3A RID: 11834
		private PaginationMode m_clientPaginationMode;

		// Token: 0x04002E3B RID: 11835
		private int m_previousTotalPages;
	}
}
