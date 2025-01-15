using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002FB RID: 763
	public sealed class RenderingContext
	{
		// Token: 0x06001B82 RID: 7042 RVA: 0x0006E6A4 File Offset: 0x0006C8A4
		internal RenderingContext(string rendererID, Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot reportSnapshot, EventInformation eventInfo, OnDemandProcessingContext processingContext)
		{
			this.m_rendererID = rendererID;
			this.m_isSubReportContext = false;
			this.m_reportSnapshot = reportSnapshot;
			this.InitEventInfo(eventInfo);
			this.m_odpContext = processingContext;
			if (processingContext.ChunkFactory != null)
			{
				this.m_chunkManager = new RenderingChunkManager(rendererID, processingContext.ChunkFactory);
			}
			this.m_segmentationManager = new DataShapeSegmentationManager();
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0006E702 File Offset: 0x0006C902
		internal RenderingContext(string rendererID, Microsoft.ReportingServices.ReportProcessing.ReportSnapshot reportSnapshot, IChunkFactory chunkFactory, EventInformation eventInfo)
		{
			this.m_rendererID = rendererID;
			this.m_isSubReportContext = false;
			this.m_oldReportSnapshot = reportSnapshot;
			this.InitEventInfo(eventInfo);
			if (chunkFactory != null)
			{
				this.m_chunkManager = new RenderingChunkManager(rendererID, chunkFactory);
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0006E737 File Offset: 0x0006C937
		internal RenderingContext(RenderingContext parentContext)
			: this(parentContext, false)
		{
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0006E744 File Offset: 0x0006C944
		internal RenderingContext(RenderingContext parentContext, bool hasReportItemReferences)
		{
			this.m_rendererID = parentContext.m_rendererID;
			this.m_isSubReportContext = true;
			this.m_pageEvaluation = null;
			this.m_dynamicInstances = null;
			this.m_eventInfo = parentContext.EventInfo;
			this.m_reportSnapshot = parentContext.m_reportSnapshot;
			this.m_oldReportSnapshot = parentContext.m_oldReportSnapshot;
			this.m_chunkManager = parentContext.m_chunkManager;
			if (this.m_oldReportSnapshot != null)
			{
				this.m_odpContext = parentContext.OdpContext;
				return;
			}
			this.m_odpContext = new OnDemandProcessingContext(parentContext.m_odpContext, hasReportItemReferences, this.m_reportSnapshot.Report);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0006E7DA File Offset: 0x0006C9DA
		private void InitEventInfo(EventInformation eventInfo)
		{
			if (eventInfo == null)
			{
				this.m_eventInfo = new EventInformation();
			}
			else
			{
				this.m_eventInfo = eventInfo;
			}
			this.m_eventInfo.Changed = false;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0006E800 File Offset: 0x0006CA00
		internal RenderingContext(RenderingContext parentContext, OnDemandProcessingContext onDemandProcessingContext)
		{
			this.m_rendererID = parentContext.m_rendererID;
			this.m_isSubReportContext = true;
			this.m_pageEvaluation = null;
			this.m_dynamicInstances = null;
			this.m_oldReportSnapshot = parentContext.m_oldReportSnapshot;
			this.m_eventInfo = parentContext.EventInfo;
			this.m_reportSnapshot = parentContext.m_reportSnapshot;
			this.m_chunkManager = parentContext.m_chunkManager;
			this.m_odpContext = onDemandProcessingContext;
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0006E86B File Offset: 0x0006CA6B
		internal bool IsSubReportContext
		{
			get
			{
				return this.m_isSubReportContext;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0006E873 File Offset: 0x0006CA73
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0006E87B File Offset: 0x0006CA7B
		internal bool SubReportProcessedWithError
		{
			get
			{
				return this.m_subReportProcessedWithError;
			}
			set
			{
				this.m_subReportProcessedWithError = value;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0006E884 File Offset: 0x0006CA84
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x0006E88C File Offset: 0x0006CA8C
		internal bool SubReportHasNoInstance
		{
			get
			{
				return this.m_subReportHasNoInstance;
			}
			set
			{
				this.m_subReportHasNoInstance = value;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0006E895 File Offset: 0x0006CA95
		internal bool SubReportHasErrorOrNoInstance
		{
			get
			{
				return this.m_subReportProcessedWithError || this.m_subReportHasNoInstance;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0006E8A7 File Offset: 0x0006CAA7
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0006E8C3 File Offset: 0x0006CAC3
		internal bool InstanceAccessDisallowed
		{
			get
			{
				return this.m_instanceAccessDisallowed || (this.IsSubReportContext && this.SubReportHasErrorOrNoInstance);
			}
			set
			{
				this.m_instanceAccessDisallowed = value;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0006E8CC File Offset: 0x0006CACC
		internal OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0006E8D4 File Offset: 0x0006CAD4
		internal IErrorContext ErrorContext
		{
			get
			{
				if (this.m_odpContext != null)
				{
					return this.m_odpContext.ReportRuntime;
				}
				return null;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0006E8EB File Offset: 0x0006CAEB
		internal EventInformation EventInfo
		{
			get
			{
				return this.m_eventInfo;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0006E8F3 File Offset: 0x0006CAF3
		internal bool EventInfoChanged
		{
			get
			{
				return this.m_eventInfo.Changed;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0006E900 File Offset: 0x0006CB00
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot ReportSnapshot
		{
			get
			{
				return this.m_reportSnapshot;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0006E908 File Offset: 0x0006CB08
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x0006E910 File Offset: 0x0006CB10
		internal bool NativeAllCRITypes
		{
			get
			{
				return this.m_nativeAllCRITypes;
			}
			set
			{
				this.m_nativeAllCRITypes = value;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0006E919 File Offset: 0x0006CB19
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x0006E921 File Offset: 0x0006CB21
		internal Hashtable NativeCRITypes
		{
			get
			{
				return this.m_nativeCRITypes;
			}
			set
			{
				this.m_nativeCRITypes = value;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0006E92A File Offset: 0x0006CB2A
		internal DataShapeSegmentationManager SegmentationManager
		{
			get
			{
				return this.m_segmentationManager;
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0006E932 File Offset: 0x0006CB32
		internal void AddDynamicInstance(IDynamicInstance instance)
		{
			if (this.m_dynamicInstances == null)
			{
				this.m_dynamicInstances = new List<IDynamicInstance>();
			}
			this.m_dynamicInstances.Add(instance);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0006E954 File Offset: 0x0006CB54
		internal void ResetContext()
		{
			if (this.m_dynamicInstances != null)
			{
				for (int i = 0; i < this.m_dynamicInstances.Count; i++)
				{
					this.m_dynamicInstances[i].ResetContext();
				}
			}
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0006E990 File Offset: 0x0006CB90
		internal void SetPageEvaluation(PageEvaluation pageEvaluation)
		{
			this.m_pageEvaluation = pageEvaluation;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0006E999 File Offset: 0x0006CB99
		internal void AddToCurrentPage(string textboxName, object textboxValue)
		{
			if (this.m_pageEvaluation != null)
			{
				this.m_pageEvaluation.Add(textboxName, textboxValue);
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
		internal void AddDrillthroughAction(string drillthroughId, string reportName, DrillthroughParameters reportParameters)
		{
			if (this.m_rendererID == null)
			{
				return;
			}
			this.CheckResetEventInfo();
			EventInformation.RendererEventInformation rendererEventInformation = this.m_eventInfo.GetRendererEventInformation(this.m_rendererID);
			if (rendererEventInformation.DrillthroughInfo == null)
			{
				rendererEventInformation.DrillthroughInfo = new Hashtable();
			}
			if (!rendererEventInformation.DrillthroughInfo.ContainsKey(drillthroughId))
			{
				rendererEventInformation.DrillthroughInfo.Add(drillthroughId, new DrillthroughInfo(reportName, reportParameters));
				this.m_eventInfo.Changed = true;
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0006EA1E File Offset: 0x0006CC1E
		private void CheckResetEventInfo()
		{
			if (!this.m_eventInfo.Changed)
			{
				this.m_eventInfo.GetRendererEventInformation(this.m_rendererID).Reset();
				this.m_eventInfo.Changed = true;
			}
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0006EA50 File Offset: 0x0006CC50
		internal void AddValidToggleSender(string senderUniqueName)
		{
			this.CheckResetEventInfo();
			EventInformation.RendererEventInformation rendererEventInformation = this.m_eventInfo.GetRendererEventInformation(this.m_rendererID);
			if (rendererEventInformation.ValidToggleSenders == null)
			{
				rendererEventInformation.ValidToggleSenders = new Hashtable();
			}
			if (!rendererEventInformation.ValidToggleSenders.ContainsKey(senderUniqueName))
			{
				rendererEventInformation.ValidToggleSenders.Add(senderUniqueName, null);
				this.m_eventInfo.Changed = true;
			}
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0006EAB0 File Offset: 0x0006CCB0
		internal bool IsSenderToggled(string uniqueName)
		{
			EventInformation eventInfo = this.EventInfo;
			if (eventInfo != null)
			{
				Hashtable toggleStateInfo = eventInfo.ToggleStateInfo;
				return toggleStateInfo != null && toggleStateInfo.ContainsKey(uniqueName);
			}
			return false;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0006EADC File Offset: 0x0006CCDC
		internal SortOptions GetSortState(string eventSourceUniqueName)
		{
			if (this.m_eventInfo != null && this.m_eventInfo.OdpSortInfo != null)
			{
				return this.m_eventInfo.OdpSortInfo.GetSortState(eventSourceUniqueName);
			}
			return SortOptions.None;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0006EB06 File Offset: 0x0006CD06
		internal string GenerateShimUniqueName(string baseUniqueName)
		{
			return "x" + baseUniqueName;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0006EB13 File Offset: 0x0006CD13
		internal Stream GetOrCreateChunk(ReportProcessing.ReportChunkTypes type, string chunkName, bool createChunkIfNotExists, out bool isNewChunk)
		{
			if (!this.IsChunkManagerValid())
			{
				isNewChunk = false;
				return null;
			}
			return this.m_chunkManager.GetOrCreateChunk(type, chunkName, createChunkIfNotExists, out isNewChunk);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0006EB33 File Offset: 0x0006CD33
		internal Stream CreateChunk(ReportProcessing.ReportChunkTypes type, string chunkName)
		{
			if (!this.IsChunkManagerValid())
			{
				return null;
			}
			return this.m_chunkManager.CreateChunk(type, chunkName);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006EB4C File Offset: 0x0006CD4C
		private bool IsChunkManagerValid()
		{
			bool flag = true;
			if (this.m_chunkManager == null)
			{
				if (this.m_odpContext != null)
				{
					this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsRenderingChunksUnavailable, Severity.Warning, ObjectType.Report, "Report", "Report", Array.Empty<string>());
					this.m_odpContext.TraceOneTimeWarning(ProcessingErrorCode.rsRenderingChunksUnavailable);
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006EBA5 File Offset: 0x0006CDA5
		internal void CloseRenderingChunkManager()
		{
			if (this.m_chunkManager != null)
			{
				this.m_chunkManager.CloseAllChunks();
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0006EBBA File Offset: 0x0006CDBA
		internal bool IsRenderAsNativeCri(Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem criDef)
		{
			return this.m_nativeAllCRITypes || (this.m_nativeCRITypes != null && this.m_nativeCRITypes.ContainsKey(criDef.Type));
		}

		// Token: 0x04000EA9 RID: 3753
		private bool m_isSubReportContext;

		// Token: 0x04000EAA RID: 3754
		private bool m_subReportProcessedWithError;

		// Token: 0x04000EAB RID: 3755
		private bool m_instanceAccessDisallowed;

		// Token: 0x04000EAC RID: 3756
		private bool m_subReportHasNoInstance;

		// Token: 0x04000EAD RID: 3757
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot m_reportSnapshot;

		// Token: 0x04000EAE RID: 3758
		private Microsoft.ReportingServices.ReportProcessing.ReportSnapshot m_oldReportSnapshot;

		// Token: 0x04000EAF RID: 3759
		private EventInformation m_eventInfo;

		// Token: 0x04000EB0 RID: 3760
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04000EB1 RID: 3761
		private List<IDynamicInstance> m_dynamicInstances;

		// Token: 0x04000EB2 RID: 3762
		private PageEvaluation m_pageEvaluation;

		// Token: 0x04000EB3 RID: 3763
		private bool m_nativeAllCRITypes;

		// Token: 0x04000EB4 RID: 3764
		private Hashtable m_nativeCRITypes;

		// Token: 0x04000EB5 RID: 3765
		private RenderingChunkManager m_chunkManager;

		// Token: 0x04000EB6 RID: 3766
		private string m_rendererID;

		// Token: 0x04000EB7 RID: 3767
		private readonly DataShapeSegmentationManager m_segmentationManager;
	}
}
