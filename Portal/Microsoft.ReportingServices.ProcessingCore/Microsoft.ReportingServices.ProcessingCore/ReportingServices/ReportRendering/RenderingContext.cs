using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000014 RID: 20
	internal sealed class RenderingContext
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00007D48 File Offset: 0x00005F48
		internal RenderingContext(ReportSnapshot reportSnapshot, string rendererID, DateTime executionTime, EmbeddedImageHashtable embeddedImages, ImageStreamNames imageStreamNames, EventInformation eventInfo, ICatalogItemContext reportContext, Uri contextUri, NameValueCollection reportParameters, ReportProcessing.GetReportChunk getChunkCallback, ChunkManager.RenderingChunkManager chunkManager, IGetResource getResourceCallback, ReportProcessing.GetChunkMimeType getChunkMimeType, ReportProcessing.StoreServerParameters storeServerParameters, bool retrieveRenderingInfo, UserProfileState allowUserProfileState, ReportRuntimeSetup reportRuntimeSetup, IJobContext jobContext, IDataProtection dataProtection)
		{
			this.m_commonInfo = new Microsoft.ReportingServices.ReportRendering.RenderingContext.CommonInfo(rendererID, executionTime, reportContext, reportParameters, getChunkCallback, chunkManager, getResourceCallback, getChunkMimeType, storeServerParameters, retrieveRenderingInfo, allowUserProfileState, reportRuntimeSetup, reportSnapshot.Report.IntermediateFormatVersion);
			this.m_inPageSection = false;
			this.m_prefix = null;
			this.m_eventInfo = eventInfo;
			this.m_reportSnapshot = reportSnapshot;
			this.m_processedItems = null;
			this.m_cachedHiddenInfo = null;
			this.m_contextUri = contextUri;
			this.m_embeddedImages = embeddedImages;
			this.m_imageStreamNames = imageStreamNames;
			this.m_currentReportICatalogItemContext = this.m_commonInfo.TopLevelReportContext;
			this.m_jobContext = jobContext;
			this.m_dataProtection = dataProtection;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00007DEC File Offset: 0x00005FEC
		internal RenderingContext(Microsoft.ReportingServices.ReportRendering.RenderingContext copy, Uri contextUri, EmbeddedImageHashtable embeddedImages, ImageStreamNames imageStreamNames, ICatalogItemContext subreportICatalogItemContext)
		{
			this.m_commonInfo = copy.m_commonInfo;
			this.m_inPageSection = false;
			this.m_prefix = null;
			this.m_eventInfo = copy.m_eventInfo;
			this.m_reportSnapshot = copy.ReportSnapshot;
			this.m_processedItems = null;
			this.m_cachedHiddenInfo = copy.m_cachedHiddenInfo;
			this.m_contextUri = contextUri;
			this.m_embeddedImages = embeddedImages;
			this.m_imageStreamNames = imageStreamNames;
			this.m_currentReportICatalogItemContext = subreportICatalogItemContext;
			this.m_jobContext = copy.m_jobContext;
			this.m_dataProtection = copy.m_dataProtection;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00007E7C File Offset: 0x0000607C
		internal RenderingContext(Microsoft.ReportingServices.ReportRendering.RenderingContext copy, string prefix)
		{
			this.m_commonInfo = copy.m_commonInfo;
			this.m_inPageSection = true;
			this.m_prefix = prefix;
			this.m_eventInfo = null;
			this.m_reportSnapshot = null;
			this.m_processedItems = null;
			this.m_cachedHiddenInfo = null;
			this.m_contextUri = copy.m_contextUri;
			this.m_embeddedImages = copy.EmbeddedImages;
			this.m_imageStreamNames = copy.ImageStreamNames;
			this.m_currentReportICatalogItemContext = this.m_commonInfo.TopLevelReportContext;
			this.m_jobContext = copy.m_jobContext;
			this.m_dataProtection = copy.m_dataProtection;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00007F12 File Offset: 0x00006112
		internal ICatalogItemContext TopLevelReportContext
		{
			get
			{
				return this.m_commonInfo.TopLevelReportContext;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00007F1F File Offset: 0x0000611F
		internal ReportProcessing.GetReportChunk GetChunkCallback
		{
			get
			{
				return this.m_commonInfo.GetChunkCallback;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00007F2C File Offset: 0x0000612C
		internal ReportProcessing.GetChunkMimeType GetChunkMimeType
		{
			get
			{
				return this.m_commonInfo.GetChunkMimeType;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00007F39 File Offset: 0x00006139
		internal ReportProcessing.StoreServerParameters StoreServerParameters
		{
			get
			{
				return this.m_commonInfo.StoreServerParameters;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00007F46 File Offset: 0x00006146
		internal string RendererID
		{
			get
			{
				return this.m_commonInfo.RendererID;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00007F53 File Offset: 0x00006153
		internal DateTime ExecutionTime
		{
			get
			{
				return this.m_commonInfo.ExecutionTime;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00007F60 File Offset: 0x00006160
		internal string ReplacementRoot
		{
			get
			{
				return this.m_commonInfo.ReplacementRoot;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00007F6D File Offset: 0x0000616D
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00007F7A File Offset: 0x0000617A
		internal bool CacheState
		{
			get
			{
				return this.m_commonInfo.CacheState;
			}
			set
			{
				this.m_commonInfo.CacheState = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00007F88 File Offset: 0x00006188
		internal RenderingInfoManager RenderingInfoManager
		{
			get
			{
				return this.m_commonInfo.RenderingInfoManager;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00007F95 File Offset: 0x00006195
		internal ChunkManager.RenderingChunkManager ChunkManager
		{
			get
			{
				return this.m_commonInfo.ChunkManager;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00007FA2 File Offset: 0x000061A2
		internal IGetResource GetResourceCallback
		{
			get
			{
				return this.m_commonInfo.GetResourceCallback;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00007FAF File Offset: 0x000061AF
		internal ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_commonInfo.ReportRuntimeSetup;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00007FBC File Offset: 0x000061BC
		internal IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_commonInfo.IntermediateFormatVersion;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00007FC9 File Offset: 0x000061C9
		internal ImageStreamNames ImageStreamNames
		{
			get
			{
				return this.m_imageStreamNames;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00007FD1 File Offset: 0x000061D1
		internal EmbeddedImageHashtable EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00007FD9 File Offset: 0x000061D9
		internal bool InPageSection
		{
			get
			{
				return this.m_inPageSection;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00007FE1 File Offset: 0x000061E1
		internal string UniqueNamePrefix
		{
			get
			{
				Global.Tracer.Assert(this.m_inPageSection);
				return this.m_prefix;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00007FF9 File Offset: 0x000061F9
		internal Uri ContextUri
		{
			get
			{
				return this.m_contextUri;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00008001 File Offset: 0x00006201
		internal ReportSnapshot ReportSnapshot
		{
			get
			{
				Global.Tracer.Assert(this.m_reportSnapshot != null);
				return this.m_reportSnapshot;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000801C File Offset: 0x0000621C
		private SenderInformationHashtable ShowHideSenderInfo
		{
			get
			{
				if (this.m_reportSnapshot != null)
				{
					return this.m_reportSnapshot.GetShowHideSenderInfo(this.ChunkManager);
				}
				return null;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00008039 File Offset: 0x00006239
		private ReceiverInformationHashtable ShowHideReceiverInfo
		{
			get
			{
				if (this.m_reportSnapshot != null)
				{
					return this.m_reportSnapshot.GetShowHideReceiverInfo(this.ChunkManager);
				}
				return null;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00008056 File Offset: 0x00006256
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000805E File Offset: 0x0000625E
		internal MatrixHeadingInstance HeadingInstance
		{
			get
			{
				return this.m_headingInstance;
			}
			set
			{
				this.m_headingInstance = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00008067 File Offset: 0x00006267
		internal UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_commonInfo.AllowUserProfileState;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00008074 File Offset: 0x00006274
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00008081 File Offset: 0x00006281
		internal UserProfileState UsedUserProfileState
		{
			get
			{
				return this.m_commonInfo.UsedUserProfileState;
			}
			set
			{
				this.m_commonInfo.UsedUserProfileState = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000808F File Offset: 0x0000628F
		internal ICatalogItemContext CurrentReportContext
		{
			get
			{
				return this.m_currentReportICatalogItemContext;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00008097 File Offset: 0x00006297
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000809F File Offset: 0x0000629F
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

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000080A8 File Offset: 0x000062A8
		// (set) Token: 0x06000357 RID: 855 RVA: 0x000080B0 File Offset: 0x000062B0
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000080B9 File Offset: 0x000062B9
		internal IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000080C1 File Offset: 0x000062C1
		internal IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000080CC File Offset: 0x000062CC
		internal Microsoft.ReportingServices.ReportRendering.ReportItem FindReportItemInBody(int uniqueName)
		{
			object obj = null;
			NonComputedUniqueNames nonComputedUniqueNames = null;
			QuickFindHashtable quickFind = this.ReportSnapshot.GetQuickFind(this.ChunkManager);
			if (quickFind != null)
			{
				obj = quickFind[uniqueName];
			}
			if (obj == null)
			{
				Global.Tracer.Assert(this.ReportSnapshot.ReportInstance != null);
				obj = ((ISearchByUniqueName)this.ReportSnapshot.ReportInstance).Find(uniqueName, ref nonComputedUniqueNames, this.ChunkManager);
				if (obj == null)
				{
					return null;
				}
			}
			Microsoft.ReportingServices.ReportRendering.ReportItem reportItem2;
			if (obj is Microsoft.ReportingServices.ReportProcessing.ReportItem)
			{
				Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem = (Microsoft.ReportingServices.ReportProcessing.ReportItem)obj;
				reportItem2 = Microsoft.ReportingServices.ReportRendering.ReportItem.CreateItem(-1, reportItem, null, this, nonComputedUniqueNames);
			}
			else
			{
				Microsoft.ReportingServices.ReportProcessing.ReportItemInstance reportItemInstance = (Microsoft.ReportingServices.ReportProcessing.ReportItemInstance)obj;
				reportItem2 = Microsoft.ReportingServices.ReportRendering.ReportItem.CreateItem(-1, reportItemInstance.ReportItemDef, reportItemInstance, this, nonComputedUniqueNames);
			}
			return reportItem2;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00008170 File Offset: 0x00006370
		internal bool IsItemHidden(int uniqueName, bool potentialSender)
		{
			bool flag;
			try
			{
				if (this.ShowHideReceiverInfo == null || this.ShowHideSenderInfo == null)
				{
					flag = false;
				}
				else
				{
					if (this.m_processedItems == null)
					{
						this.m_processedItems = new Hashtable();
					}
					flag = this.RecursiveIsItemHidden(uniqueName, potentialSender);
				}
			}
			finally
			{
				if (this.m_processedItems != null)
				{
					this.m_processedItems.Clear();
				}
			}
			return flag;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000081D4 File Offset: 0x000063D4
		internal bool ShowHideStateChanged
		{
			get
			{
				return this.m_eventInfo != null && this.m_eventInfo.ToggleStateInfo != null && this.m_eventInfo.HiddenInfo != null;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000081FB File Offset: 0x000063FB
		internal bool IsToggleStateNegated(int uniqueName)
		{
			return this.m_eventInfo != null && this.m_eventInfo.ToggleStateInfo != null && this.m_eventInfo.HiddenInfo != null && this.m_eventInfo.ToggleStateInfo.ContainsKey(uniqueName);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00008237 File Offset: 0x00006437
		internal bool IsToggleParent(int uniqueName)
		{
			return this.ShowHideSenderInfo != null && this.ShowHideSenderInfo.ContainsKey(uniqueName);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000824F File Offset: 0x0000644F
		internal bool IsToggleChild(int uniqueName)
		{
			return this.ShowHideReceiverInfo != null && this.ShowHideReceiverInfo.ContainsKey(uniqueName);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00008268 File Offset: 0x00006468
		internal Microsoft.ReportingServices.ReportRendering.TextBox GetToggleParent(int uniqueName)
		{
			if (this.ShowHideReceiverInfo != null)
			{
				ReceiverInformation receiverInformation = this.ShowHideReceiverInfo[uniqueName];
				if (receiverInformation != null)
				{
					Microsoft.ReportingServices.ReportRendering.ReportItem reportItem = this.FindReportItemInBody(receiverInformation.SenderUniqueName);
					Global.Tracer.Assert(reportItem != null);
					Global.Tracer.Assert(reportItem is Microsoft.ReportingServices.ReportRendering.TextBox);
					return (Microsoft.ReportingServices.ReportRendering.TextBox)reportItem;
				}
			}
			return null;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000082C3 File Offset: 0x000064C3
		internal static bool GetDefinitionHidden(Microsoft.ReportingServices.ReportProcessing.Visibility visibility)
		{
			return visibility != null && visibility.Hidden != null && ExpressionInfo.Types.Constant == visibility.Hidden.Type && visibility.Hidden.BoolValue;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000082EF File Offset: 0x000064EF
		internal SortOptions GetSortState(int uniqueName)
		{
			if (this.m_eventInfo != null && this.m_eventInfo.SortInfo != null)
			{
				return this.m_eventInfo.SortInfo.GetSortState(uniqueName);
			}
			return SortOptions.None;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000831C File Offset: 0x0000651C
		private bool RecursiveIsItemHidden(int uniqueName, bool potentialSender)
		{
			Global.Tracer.Assert(this.m_processedItems != null);
			if (this.m_processedItems.ContainsKey(uniqueName))
			{
				return false;
			}
			this.m_processedItems.Add(uniqueName, null);
			if (this.m_cachedHiddenInfo == null)
			{
				this.m_cachedHiddenInfo = new Hashtable();
			}
			else
			{
				object obj = this.m_cachedHiddenInfo[uniqueName];
				if (obj != null)
				{
					return (bool)obj;
				}
			}
			ReceiverInformation receiverInformation = this.ShowHideReceiverInfo[uniqueName];
			if (receiverInformation != null)
			{
				if (this.IsHidden(uniqueName, receiverInformation.StartHidden))
				{
					this.m_cachedHiddenInfo[uniqueName] = true;
					return true;
				}
				if (this.RecursiveIsItemHidden(receiverInformation.SenderUniqueName, true))
				{
					this.m_cachedHiddenInfo[uniqueName] = true;
					return true;
				}
			}
			if (potentialSender)
			{
				SenderInformation senderInformation = this.ShowHideSenderInfo[uniqueName];
				if (senderInformation != null)
				{
					if (this.IsHidden(uniqueName, senderInformation.StartHidden))
					{
						this.m_cachedHiddenInfo[uniqueName] = true;
						return true;
					}
					if (senderInformation.ContainerUniqueNames != null)
					{
						for (int i = senderInformation.ContainerUniqueNames.Length - 1; i >= 0; i--)
						{
							if (this.RecursiveIsItemHidden(senderInformation.ContainerUniqueNames[i], false))
							{
								this.m_cachedHiddenInfo[uniqueName] = true;
								return true;
							}
						}
					}
				}
			}
			this.m_cachedHiddenInfo[uniqueName] = false;
			return false;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000848E File Offset: 0x0000668E
		private bool IsHidden(int uniqueName, bool startHidden)
		{
			if (this.IsHiddenNegated(uniqueName))
			{
				return !startHidden;
			}
			return startHidden;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000849F File Offset: 0x0000669F
		private bool IsHiddenNegated(int uniqueName)
		{
			return this.m_eventInfo != null && this.m_eventInfo.ToggleStateInfo != null && this.m_eventInfo.HiddenInfo != null && this.m_eventInfo.HiddenInfo.ContainsKey(uniqueName);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000084DB File Offset: 0x000066DB
		internal static void FindRange(RenderingPagesRangesList pagesRangesList, int startIndex, int endIndex, int page, ref int startChild, ref int endChild)
		{
			Microsoft.ReportingServices.ReportRendering.RenderingContext.FindRange(pagesRangesList, startIndex, endIndex, page, true, true, ref startChild, ref endChild);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000084EC File Offset: 0x000066EC
		internal static void FindRange(RenderingPagesRangesList pagesRangesList, int startIndex, int endIndex, int page, bool checkStart, bool checkEnd, ref int startChild, ref int endChild)
		{
			bool flag = false;
			int num = 0;
			while (!flag && endIndex >= startIndex)
			{
				int num2 = startIndex + (endIndex - startIndex) / 2;
				RenderingPagesRanges renderingPagesRanges = pagesRangesList[num2];
				if (renderingPagesRanges.StartPage > page)
				{
					endIndex = num2 - 1;
				}
				else if (renderingPagesRanges.EndPage < page)
				{
					startIndex = num2 + 1;
				}
				else
				{
					flag = true;
					startChild = num2;
					endChild = num2;
					if (checkStart && renderingPagesRanges.StartPage == page)
					{
						Microsoft.ReportingServices.ReportRendering.RenderingContext.FindRange(pagesRangesList, startIndex, num2 - 1, page, true, false, ref startChild, ref num);
					}
					if (checkEnd && renderingPagesRanges.EndPage == page)
					{
						Microsoft.ReportingServices.ReportRendering.RenderingContext.FindRange(pagesRangesList, num2 + 1, endIndex, page, false, true, ref num, ref endChild);
					}
				}
			}
		}

		// Token: 0x04000039 RID: 57
		private Microsoft.ReportingServices.ReportRendering.RenderingContext.CommonInfo m_commonInfo;

		// Token: 0x0400003A RID: 58
		private bool m_inPageSection;

		// Token: 0x0400003B RID: 59
		private string m_prefix;

		// Token: 0x0400003C RID: 60
		private EventInformation m_eventInfo;

		// Token: 0x0400003D RID: 61
		private ReportSnapshot m_reportSnapshot;

		// Token: 0x0400003E RID: 62
		private Hashtable m_processedItems;

		// Token: 0x0400003F RID: 63
		private Hashtable m_cachedHiddenInfo;

		// Token: 0x04000040 RID: 64
		private Uri m_contextUri;

		// Token: 0x04000041 RID: 65
		private EmbeddedImageHashtable m_embeddedImages;

		// Token: 0x04000042 RID: 66
		private ImageStreamNames m_imageStreamNames;

		// Token: 0x04000043 RID: 67
		private MatrixHeadingInstance m_headingInstance;

		// Token: 0x04000044 RID: 68
		private ICatalogItemContext m_currentReportICatalogItemContext;

		// Token: 0x04000045 RID: 69
		private bool m_nativeAllCRITypes;

		// Token: 0x04000046 RID: 70
		private Hashtable m_nativeCRITypes;

		// Token: 0x04000047 RID: 71
		private IJobContext m_jobContext;

		// Token: 0x04000048 RID: 72
		private IDataProtection m_dataProtection;

		// Token: 0x02000909 RID: 2313
		private sealed class CommonInfo
		{
			// Token: 0x06007EF9 RID: 32505 RVA: 0x0020BA90 File Offset: 0x00209C90
			internal CommonInfo(string rendererID, DateTime executionTime, ICatalogItemContext reportContext, NameValueCollection reportParameters, ReportProcessing.GetReportChunk getChunkCallback, ChunkManager.RenderingChunkManager chunkManager, IGetResource getResourceCallback, ReportProcessing.GetChunkMimeType getChunkMimeType, ReportProcessing.StoreServerParameters storeServerParameters, bool retrieveRenderingInfo, UserProfileState allowUserProfileState, ReportRuntimeSetup reportRuntimeSetup, IntermediateFormatVersion intermediateFormatVersion)
			{
				this.m_rendererID = rendererID;
				this.m_executionTime = executionTime;
				this.m_reportContext = reportContext;
				if (reportParameters != null)
				{
					this.m_replacementRoot = reportParameters["ReplacementRoot"];
				}
				this.m_renderingInfoManager = new RenderingInfoManager(rendererID, getChunkCallback, retrieveRenderingInfo);
				this.m_chunkManager = chunkManager;
				this.m_getResourceCallback = getResourceCallback;
				this.m_getChunkCallback = getChunkCallback;
				this.m_getChunkMimeType = getChunkMimeType;
				this.m_storeServerParameters = storeServerParameters;
				this.m_allowUserProfileState = allowUserProfileState;
				this.m_reportRuntimeSetup = reportRuntimeSetup;
				this.m_intermediateFormatVersion = intermediateFormatVersion;
			}

			// Token: 0x17002943 RID: 10563
			// (get) Token: 0x06007EFA RID: 32506 RVA: 0x0020BB1E File Offset: 0x00209D1E
			internal ReportProcessing.GetReportChunk GetChunkCallback
			{
				get
				{
					return this.m_getChunkCallback;
				}
			}

			// Token: 0x17002944 RID: 10564
			// (get) Token: 0x06007EFB RID: 32507 RVA: 0x0020BB26 File Offset: 0x00209D26
			internal string RendererID
			{
				get
				{
					return this.m_rendererID;
				}
			}

			// Token: 0x17002945 RID: 10565
			// (get) Token: 0x06007EFC RID: 32508 RVA: 0x0020BB2E File Offset: 0x00209D2E
			internal DateTime ExecutionTime
			{
				get
				{
					return this.m_executionTime;
				}
			}

			// Token: 0x17002946 RID: 10566
			// (get) Token: 0x06007EFD RID: 32509 RVA: 0x0020BB36 File Offset: 0x00209D36
			internal string ReplacementRoot
			{
				get
				{
					return this.m_replacementRoot;
				}
			}

			// Token: 0x17002947 RID: 10567
			// (get) Token: 0x06007EFE RID: 32510 RVA: 0x0020BB3E File Offset: 0x00209D3E
			internal RenderingInfoManager RenderingInfoManager
			{
				get
				{
					return this.m_renderingInfoManager;
				}
			}

			// Token: 0x17002948 RID: 10568
			// (get) Token: 0x06007EFF RID: 32511 RVA: 0x0020BB46 File Offset: 0x00209D46
			// (set) Token: 0x06007F00 RID: 32512 RVA: 0x0020BB4E File Offset: 0x00209D4E
			internal bool CacheState
			{
				get
				{
					return this.m_cacheState;
				}
				set
				{
					this.m_cacheState = value;
				}
			}

			// Token: 0x17002949 RID: 10569
			// (get) Token: 0x06007F01 RID: 32513 RVA: 0x0020BB57 File Offset: 0x00209D57
			internal ChunkManager.RenderingChunkManager ChunkManager
			{
				get
				{
					return this.m_chunkManager;
				}
			}

			// Token: 0x1700294A RID: 10570
			// (get) Token: 0x06007F02 RID: 32514 RVA: 0x0020BB5F File Offset: 0x00209D5F
			internal IGetResource GetResourceCallback
			{
				get
				{
					return this.m_getResourceCallback;
				}
			}

			// Token: 0x1700294B RID: 10571
			// (get) Token: 0x06007F03 RID: 32515 RVA: 0x0020BB67 File Offset: 0x00209D67
			internal ReportProcessing.GetChunkMimeType GetChunkMimeType
			{
				get
				{
					return this.m_getChunkMimeType;
				}
			}

			// Token: 0x1700294C RID: 10572
			// (get) Token: 0x06007F04 RID: 32516 RVA: 0x0020BB6F File Offset: 0x00209D6F
			internal ReportProcessing.StoreServerParameters StoreServerParameters
			{
				get
				{
					return this.m_storeServerParameters;
				}
			}

			// Token: 0x1700294D RID: 10573
			// (get) Token: 0x06007F05 RID: 32517 RVA: 0x0020BB77 File Offset: 0x00209D77
			internal ICatalogItemContext TopLevelReportContext
			{
				get
				{
					return this.m_reportContext;
				}
			}

			// Token: 0x1700294E RID: 10574
			// (get) Token: 0x06007F06 RID: 32518 RVA: 0x0020BB7F File Offset: 0x00209D7F
			internal UserProfileState AllowUserProfileState
			{
				get
				{
					return this.m_allowUserProfileState;
				}
			}

			// Token: 0x1700294F RID: 10575
			// (get) Token: 0x06007F07 RID: 32519 RVA: 0x0020BB87 File Offset: 0x00209D87
			// (set) Token: 0x06007F08 RID: 32520 RVA: 0x0020BB8F File Offset: 0x00209D8F
			internal UserProfileState UsedUserProfileState
			{
				get
				{
					return this.m_usedUserProfileState;
				}
				set
				{
					this.m_usedUserProfileState = value;
				}
			}

			// Token: 0x17002950 RID: 10576
			// (get) Token: 0x06007F09 RID: 32521 RVA: 0x0020BB98 File Offset: 0x00209D98
			internal ReportRuntimeSetup ReportRuntimeSetup
			{
				get
				{
					return this.m_reportRuntimeSetup;
				}
			}

			// Token: 0x17002951 RID: 10577
			// (get) Token: 0x06007F0A RID: 32522 RVA: 0x0020BBA0 File Offset: 0x00209DA0
			internal IntermediateFormatVersion IntermediateFormatVersion
			{
				get
				{
					return this.m_intermediateFormatVersion;
				}
			}

			// Token: 0x04003EAF RID: 16047
			private string m_rendererID;

			// Token: 0x04003EB0 RID: 16048
			private DateTime m_executionTime;

			// Token: 0x04003EB1 RID: 16049
			private string m_replacementRoot;

			// Token: 0x04003EB2 RID: 16050
			private RenderingInfoManager m_renderingInfoManager;

			// Token: 0x04003EB3 RID: 16051
			private ChunkManager.RenderingChunkManager m_chunkManager;

			// Token: 0x04003EB4 RID: 16052
			private IGetResource m_getResourceCallback;

			// Token: 0x04003EB5 RID: 16053
			private bool m_cacheState;

			// Token: 0x04003EB6 RID: 16054
			private ICatalogItemContext m_reportContext;

			// Token: 0x04003EB7 RID: 16055
			private ReportProcessing.GetReportChunk m_getChunkCallback;

			// Token: 0x04003EB8 RID: 16056
			private ReportProcessing.GetChunkMimeType m_getChunkMimeType;

			// Token: 0x04003EB9 RID: 16057
			private ReportProcessing.StoreServerParameters m_storeServerParameters;

			// Token: 0x04003EBA RID: 16058
			private UserProfileState m_allowUserProfileState;

			// Token: 0x04003EBB RID: 16059
			private UserProfileState m_usedUserProfileState;

			// Token: 0x04003EBC RID: 16060
			private ReportRuntimeSetup m_reportRuntimeSetup;

			// Token: 0x04003EBD RID: 16061
			private IntermediateFormatVersion m_intermediateFormatVersion;
		}
	}
}
