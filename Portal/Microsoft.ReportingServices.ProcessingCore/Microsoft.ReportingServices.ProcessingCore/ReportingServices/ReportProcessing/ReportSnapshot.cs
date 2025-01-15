using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000718 RID: 1816
	[Serializable]
	internal sealed class ReportSnapshot
	{
		// Token: 0x06006530 RID: 25904 RVA: 0x0018F20C File Offset: 0x0018D40C
		internal ReportSnapshot(Report report, string reportName, ParameterInfoCollection parameters, string requestUserName, DateTime executionTime, string reportServerUrl, string reportFolder, string language)
		{
			this.m_report = report;
			this.m_reportName = reportName;
			this.m_parameters = parameters;
			this.m_requestUserName = requestUserName;
			this.m_executionTime = executionTime;
			this.m_reportServerUrl = reportServerUrl;
			this.m_reportFolder = reportFolder;
			this.m_language = language;
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x0018F25C File Offset: 0x0018D45C
		internal ReportSnapshot()
		{
			this.m_executionTime = DateTime.Now;
		}

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x0018F26F File Offset: 0x0018D46F
		// (set) Token: 0x06006533 RID: 25907 RVA: 0x0018F277 File Offset: 0x0018D477
		internal Report Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				this.m_report = value;
			}
		}

		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x06006534 RID: 25908 RVA: 0x0018F280 File Offset: 0x0018D480
		// (set) Token: 0x06006535 RID: 25909 RVA: 0x0018F288 File Offset: 0x0018D488
		internal ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x06006536 RID: 25910 RVA: 0x0018F291 File Offset: 0x0018D491
		// (set) Token: 0x06006537 RID: 25911 RVA: 0x0018F299 File Offset: 0x0018D499
		internal ReportInstance ReportInstance
		{
			get
			{
				return this.m_reportInstance;
			}
			set
			{
				this.m_reportInstance = value;
			}
		}

		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x06006538 RID: 25912 RVA: 0x0018F2A2 File Offset: 0x0018D4A2
		// (set) Token: 0x06006539 RID: 25913 RVA: 0x0018F2AA File Offset: 0x0018D4AA
		internal bool HasDocumentMap
		{
			get
			{
				return this.m_hasDocumentMap;
			}
			set
			{
				this.m_hasDocumentMap = value;
			}
		}

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x0600653A RID: 25914 RVA: 0x0018F2B3 File Offset: 0x0018D4B3
		// (set) Token: 0x0600653B RID: 25915 RVA: 0x0018F2BB File Offset: 0x0018D4BB
		internal bool HasBookmarks
		{
			get
			{
				return this.m_hasBookmarks;
			}
			set
			{
				this.m_hasBookmarks = value;
			}
		}

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x0600653C RID: 25916 RVA: 0x0018F2C4 File Offset: 0x0018D4C4
		// (set) Token: 0x0600653D RID: 25917 RVA: 0x0018F2CC File Offset: 0x0018D4CC
		internal bool HasShowHide
		{
			get
			{
				return this.m_hasShowHide;
			}
			set
			{
				this.m_hasShowHide = value;
			}
		}

		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x0018F2D5 File Offset: 0x0018D4D5
		// (set) Token: 0x0600653F RID: 25919 RVA: 0x0018F2DD File Offset: 0x0018D4DD
		internal bool HasImageStreams
		{
			get
			{
				return this.m_hasImageStreams;
			}
			set
			{
				this.m_hasImageStreams = value;
			}
		}

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x06006540 RID: 25920 RVA: 0x0018F2E6 File Offset: 0x0018D4E6
		// (set) Token: 0x06006541 RID: 25921 RVA: 0x0018F2EE File Offset: 0x0018D4EE
		internal string RequestUserName
		{
			get
			{
				return this.m_requestUserName;
			}
			set
			{
				this.m_requestUserName = value;
			}
		}

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x06006542 RID: 25922 RVA: 0x0018F2F7 File Offset: 0x0018D4F7
		// (set) Token: 0x06006543 RID: 25923 RVA: 0x0018F2FF File Offset: 0x0018D4FF
		internal DateTime ExecutionTime
		{
			get
			{
				return this.m_executionTime;
			}
			set
			{
				this.m_executionTime = value;
			}
		}

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x06006544 RID: 25924 RVA: 0x0018F308 File Offset: 0x0018D508
		// (set) Token: 0x06006545 RID: 25925 RVA: 0x0018F310 File Offset: 0x0018D510
		internal string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
			set
			{
				this.m_reportServerUrl = value;
			}
		}

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x06006546 RID: 25926 RVA: 0x0018F319 File Offset: 0x0018D519
		// (set) Token: 0x06006547 RID: 25927 RVA: 0x0018F321 File Offset: 0x0018D521
		internal string ReportFolder
		{
			get
			{
				return this.m_reportFolder;
			}
			set
			{
				this.m_reportFolder = value;
			}
		}

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x06006548 RID: 25928 RVA: 0x0018F32A File Offset: 0x0018D52A
		// (set) Token: 0x06006549 RID: 25929 RVA: 0x0018F332 File Offset: 0x0018D532
		internal string Language
		{
			get
			{
				return this.m_language;
			}
			set
			{
				this.m_language = value;
			}
		}

		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x0600654A RID: 25930 RVA: 0x0018F33B File Offset: 0x0018D53B
		// (set) Token: 0x0600654B RID: 25931 RVA: 0x0018F343 File Offset: 0x0018D543
		internal ProcessingMessageList Warnings
		{
			get
			{
				return this.m_processingMessages;
			}
			set
			{
				this.m_processingMessages = value;
			}
		}

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x0600654C RID: 25932 RVA: 0x0018F34C File Offset: 0x0018D54C
		// (set) Token: 0x0600654D RID: 25933 RVA: 0x0018F354 File Offset: 0x0018D554
		internal Int64List PageSectionOffsets
		{
			get
			{
				return this.m_pageSectionOffsets;
			}
			set
			{
				this.m_pageSectionOffsets = value;
			}
		}

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x0600654E RID: 25934 RVA: 0x0018F35D File Offset: 0x0018D55D
		// (set) Token: 0x0600654F RID: 25935 RVA: 0x0018F365 File Offset: 0x0018D565
		internal List<PageSectionInstance> PageSections
		{
			get
			{
				return this.m_pageSections;
			}
			set
			{
				this.m_pageSections = value;
			}
		}

		// Token: 0x170023E0 RID: 9184
		// (set) Token: 0x06006550 RID: 25936 RVA: 0x0018F36E File Offset: 0x0018D56E
		internal OffsetInfo DocumentMapOffset
		{
			set
			{
				this.m_documentMap = value;
			}
		}

		// Token: 0x170023E1 RID: 9185
		// (set) Token: 0x06006551 RID: 25937 RVA: 0x0018F377 File Offset: 0x0018D577
		internal OffsetInfo ShowHideSenderInfoOffset
		{
			set
			{
				this.m_showHideSenderInfo = value;
			}
		}

		// Token: 0x170023E2 RID: 9186
		// (set) Token: 0x06006552 RID: 25938 RVA: 0x0018F380 File Offset: 0x0018D580
		internal OffsetInfo ShowHideReceiverInfoOffset
		{
			set
			{
				this.m_showHideReceiverInfo = value;
			}
		}

		// Token: 0x170023E3 RID: 9187
		// (set) Token: 0x06006553 RID: 25939 RVA: 0x0018F389 File Offset: 0x0018D589
		internal OffsetInfo QuickFindOffset
		{
			set
			{
				this.m_quickFind = value;
			}
		}

		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x06006554 RID: 25940 RVA: 0x0018F392 File Offset: 0x0018D592
		// (set) Token: 0x06006555 RID: 25941 RVA: 0x0018F3C8 File Offset: 0x0018D5C8
		internal DocumentMapNode DocumentMap
		{
			get
			{
				if (this.m_documentMap == null)
				{
					return null;
				}
				if (this.m_documentMap is DocumentMapNode)
				{
					return (DocumentMapNode)this.m_documentMap;
				}
				Global.Tracer.Assert(false, string.Empty);
				return null;
			}
			set
			{
				this.m_documentMap = value;
			}
		}

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x0018F3D1 File Offset: 0x0018D5D1
		// (set) Token: 0x06006557 RID: 25943 RVA: 0x0018F3D9 File Offset: 0x0018D5D9
		internal BookmarksHashtable BookmarksInfo
		{
			get
			{
				return this.m_bookmarksInfo;
			}
			set
			{
				this.m_bookmarksInfo = value;
			}
		}

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x06006558 RID: 25944 RVA: 0x0018F3E2 File Offset: 0x0018D5E2
		// (set) Token: 0x06006559 RID: 25945 RVA: 0x0018F3EA File Offset: 0x0018D5EA
		internal ReportDrillthroughInfo DrillthroughInfo
		{
			get
			{
				return this.m_drillthroughInfo;
			}
			set
			{
				this.m_drillthroughInfo = value;
			}
		}

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x0600655A RID: 25946 RVA: 0x0018F3F3 File Offset: 0x0018D5F3
		// (set) Token: 0x0600655B RID: 25947 RVA: 0x0018F429 File Offset: 0x0018D629
		internal SenderInformationHashtable ShowHideSenderInfo
		{
			get
			{
				if (this.m_showHideSenderInfo == null)
				{
					return null;
				}
				if (this.m_showHideSenderInfo is SenderInformationHashtable)
				{
					return (SenderInformationHashtable)this.m_showHideSenderInfo;
				}
				Global.Tracer.Assert(false, string.Empty);
				return null;
			}
			set
			{
				this.m_showHideSenderInfo = value;
			}
		}

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x0018F432 File Offset: 0x0018D632
		// (set) Token: 0x0600655D RID: 25949 RVA: 0x0018F468 File Offset: 0x0018D668
		internal ReceiverInformationHashtable ShowHideReceiverInfo
		{
			get
			{
				if (this.m_showHideReceiverInfo == null)
				{
					return null;
				}
				if (this.m_showHideReceiverInfo is ReceiverInformationHashtable)
				{
					return (ReceiverInformationHashtable)this.m_showHideReceiverInfo;
				}
				Global.Tracer.Assert(false, string.Empty);
				return null;
			}
			set
			{
				this.m_showHideReceiverInfo = value;
			}
		}

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x0018F471 File Offset: 0x0018D671
		// (set) Token: 0x0600655F RID: 25951 RVA: 0x0018F4A7 File Offset: 0x0018D6A7
		internal QuickFindHashtable QuickFind
		{
			get
			{
				if (this.m_quickFind == null)
				{
					return null;
				}
				if (this.m_quickFind is QuickFindHashtable)
				{
					return (QuickFindHashtable)this.m_quickFind;
				}
				Global.Tracer.Assert(false, string.Empty);
				return null;
			}
			set
			{
				this.m_quickFind = value;
			}
		}

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x0018F4B0 File Offset: 0x0018D6B0
		// (set) Token: 0x06006561 RID: 25953 RVA: 0x0018F4E6 File Offset: 0x0018D6E6
		internal SortFilterEventInfoHashtable SortFilterEventInfo
		{
			get
			{
				if (this.m_sortFilterEventInfo == null)
				{
					return null;
				}
				if (this.m_sortFilterEventInfo is SortFilterEventInfoHashtable)
				{
					return (SortFilterEventInfoHashtable)this.m_sortFilterEventInfo;
				}
				Global.Tracer.Assert(false, string.Empty);
				return null;
			}
			set
			{
				this.m_sortFilterEventInfo = value;
			}
		}

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x06006562 RID: 25954 RVA: 0x0018F4EF File Offset: 0x0018D6EF
		// (set) Token: 0x06006563 RID: 25955 RVA: 0x0018F51E File Offset: 0x0018D71E
		internal OffsetInfo SortFilterEventInfoOffset
		{
			get
			{
				if (this.m_sortFilterEventInfo == null)
				{
					return null;
				}
				Global.Tracer.Assert(this.m_sortFilterEventInfo is OffsetInfo);
				return (OffsetInfo)this.m_sortFilterEventInfo;
			}
			set
			{
				this.m_sortFilterEventInfo = value;
			}
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x0018F528 File Offset: 0x0018D728
		internal void CreateNavigationActions(ReportProcessing.NavigationInfo navigationInfo)
		{
			if (this.m_reportInstance != null)
			{
				if (navigationInfo.DocumentMapChildren != null && 0 < navigationInfo.DocumentMapChildren.Count && navigationInfo.DocumentMapChildren[0] != null)
				{
					this.m_documentMap = new DocumentMapNode(this.m_reportInstance.UniqueName.ToString(CultureInfo.InvariantCulture), this.m_reportName, 0, navigationInfo.DocumentMapChildren[0]);
					this.m_hasDocumentMap = true;
				}
				if (navigationInfo.BookmarksInfo != null)
				{
					this.m_bookmarksInfo = navigationInfo.BookmarksInfo;
					this.m_hasBookmarks = true;
				}
			}
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x0018F5BC File Offset: 0x0018D7BC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ExecutionTime, Token.DateTime),
				new MemberInfo(MemberName.Report, ObjectType.Report),
				new MemberInfo(MemberName.Parameters, ObjectType.ParameterInfoCollection),
				new MemberInfo(MemberName.ReportInstance, ObjectType.ReportInstance),
				new MemberInfo(MemberName.HasDocumentMap, Token.Boolean),
				new MemberInfo(MemberName.HasShowHide, Token.Boolean),
				new MemberInfo(MemberName.HasBookmarks, Token.Boolean),
				new MemberInfo(MemberName.HasImageStreams, Token.Boolean),
				new MemberInfo(MemberName.RequestUserName, Token.String),
				new MemberInfo(MemberName.ReportServerUrl, Token.String),
				new MemberInfo(MemberName.ReportFolder, Token.String),
				new MemberInfo(MemberName.Language, Token.String),
				new MemberInfo(MemberName.ProcessingMessages, ObjectType.ProcessingMessageList),
				new MemberInfo(MemberName.PageSectionOffsets, ObjectType.Int64List)
			});
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x0018F6F8 File Offset: 0x0018D8F8
		internal DocumentMapNode GetDocumentMap(ChunkManager.RenderingChunkManager chunkManager)
		{
			IntermediateFormatReader intermediateFormatReader = null;
			if (this.m_documentMap != null)
			{
				if (!(this.m_documentMap is OffsetInfo))
				{
					return (DocumentMapNode)this.m_documentMap;
				}
				intermediateFormatReader = chunkManager.GetReaderForSpecialChunk(((OffsetInfo)this.m_documentMap).Offset);
			}
			else if (this.m_hasDocumentMap)
			{
				intermediateFormatReader = chunkManager.GetSpecialChunkReader(ChunkManager.SpecialChunkName.DocumentMap);
			}
			if (intermediateFormatReader != null)
			{
				return intermediateFormatReader.ReadDocumentMapNode();
			}
			return null;
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x0018F75C File Offset: 0x0018D95C
		private void GetShowHideInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_showHideSenderInfo == null && this.m_showHideReceiverInfo == null)
			{
				IntermediateFormatReader specialChunkReader = chunkManager.GetSpecialChunkReader(ChunkManager.SpecialChunkName.ShowHideInfo);
				if (specialChunkReader != null)
				{
					this.m_showHideSenderInfo = specialChunkReader.ReadSenderInformationHashtable();
					this.m_showHideReceiverInfo = specialChunkReader.ReadReceiverInformationHashtable();
				}
			}
		}

		// Token: 0x06006568 RID: 25960 RVA: 0x0018F79C File Offset: 0x0018D99C
		internal SenderInformationHashtable GetShowHideSenderInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_showHideSenderInfo == null)
			{
				this.GetShowHideInfo(chunkManager);
			}
			else if (this.m_showHideSenderInfo is OffsetInfo)
			{
				IntermediateFormatReader readerForSpecialChunk = chunkManager.GetReaderForSpecialChunk(((OffsetInfo)this.m_showHideSenderInfo).Offset);
				this.m_showHideSenderInfo = readerForSpecialChunk.ReadSenderInformationHashtable();
			}
			return (SenderInformationHashtable)this.m_showHideSenderInfo;
		}

		// Token: 0x06006569 RID: 25961 RVA: 0x0018F7F8 File Offset: 0x0018D9F8
		internal ReceiverInformationHashtable GetShowHideReceiverInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_showHideReceiverInfo == null)
			{
				this.GetShowHideInfo(chunkManager);
			}
			else if (this.m_showHideReceiverInfo is OffsetInfo)
			{
				IntermediateFormatReader readerForSpecialChunk = chunkManager.GetReaderForSpecialChunk(((OffsetInfo)this.m_showHideReceiverInfo).Offset);
				this.m_showHideReceiverInfo = readerForSpecialChunk.ReadReceiverInformationHashtable();
			}
			return (ReceiverInformationHashtable)this.m_showHideReceiverInfo;
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x0018F854 File Offset: 0x0018DA54
		internal QuickFindHashtable GetQuickFind(ChunkManager.RenderingChunkManager chunkManager)
		{
			IntermediateFormatReader intermediateFormatReader;
			if (this.m_quickFind != null)
			{
				if (!(this.m_quickFind is OffsetInfo))
				{
					return (QuickFindHashtable)this.m_quickFind;
				}
				intermediateFormatReader = chunkManager.GetReaderForSpecialChunk(((OffsetInfo)this.m_quickFind).Offset);
			}
			else
			{
				intermediateFormatReader = chunkManager.GetSpecialChunkReader(ChunkManager.SpecialChunkName.QuickFind);
			}
			if (intermediateFormatReader != null)
			{
				return intermediateFormatReader.ReadQuickFindHashtable();
			}
			return null;
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x0018F8B0 File Offset: 0x0018DAB0
		internal BookmarksHashtable GetBookmarksInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_bookmarksInfo != null)
			{
				return this.m_bookmarksInfo;
			}
			IntermediateFormatReader specialChunkReader = chunkManager.GetSpecialChunkReader(ChunkManager.SpecialChunkName.Bookmark);
			if (specialChunkReader != null)
			{
				return specialChunkReader.ReadBookmarksHashtable();
			}
			return null;
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x0018F8E0 File Offset: 0x0018DAE0
		internal SortFilterEventInfoHashtable GetSortFilterEventInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_sortFilterEventInfo != null)
			{
				return (SortFilterEventInfoHashtable)this.m_sortFilterEventInfo;
			}
			IntermediateFormatReader specialChunkReader = chunkManager.GetSpecialChunkReader(ChunkManager.SpecialChunkName.SortFilterEventInfo);
			if (specialChunkReader != null)
			{
				return specialChunkReader.ReadSortFilterEventInfoHashtable();
			}
			return null;
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x0018F918 File Offset: 0x0018DB18
		internal List<PageSectionInstance> GetPageSections(int pageNumber, ChunkManager.RenderingChunkManager chunkManager, PageSection headerDef, PageSection footerDef)
		{
			List<PageSectionInstance> list = null;
			int num;
			IntermediateFormatReader pageSectionReader = chunkManager.GetPageSectionReader(pageNumber, out num);
			if (pageSectionReader != null)
			{
				list = pageSectionReader.ReadPageSections(pageNumber, num, headerDef, footerDef);
				chunkManager.SetPageSectionReaderState(pageSectionReader.ReaderState, pageNumber);
			}
			return list;
		}

		// Token: 0x040032A2 RID: 12962
		private DateTime m_executionTime;

		// Token: 0x040032A3 RID: 12963
		private Report m_report;

		// Token: 0x040032A4 RID: 12964
		private ParameterInfoCollection m_parameters;

		// Token: 0x040032A5 RID: 12965
		private ReportInstance m_reportInstance;

		// Token: 0x040032A6 RID: 12966
		private bool m_hasDocumentMap;

		// Token: 0x040032A7 RID: 12967
		private bool m_hasShowHide;

		// Token: 0x040032A8 RID: 12968
		private bool m_hasBookmarks;

		// Token: 0x040032A9 RID: 12969
		private bool m_hasImageStreams;

		// Token: 0x040032AA RID: 12970
		private string m_requestUserName;

		// Token: 0x040032AB RID: 12971
		private string m_reportServerUrl;

		// Token: 0x040032AC RID: 12972
		private string m_reportFolder;

		// Token: 0x040032AD RID: 12973
		private string m_language;

		// Token: 0x040032AE RID: 12974
		private ProcessingMessageList m_processingMessages;

		// Token: 0x040032AF RID: 12975
		private Int64List m_pageSectionOffsets;

		// Token: 0x040032B0 RID: 12976
		[NonSerialized]
		private InfoBase m_documentMap;

		// Token: 0x040032B1 RID: 12977
		[NonSerialized]
		private InfoBase m_showHideSenderInfo;

		// Token: 0x040032B2 RID: 12978
		[NonSerialized]
		private InfoBase m_showHideReceiverInfo;

		// Token: 0x040032B3 RID: 12979
		[NonSerialized]
		private InfoBase m_quickFind;

		// Token: 0x040032B4 RID: 12980
		[NonSerialized]
		private BookmarksHashtable m_bookmarksInfo;

		// Token: 0x040032B5 RID: 12981
		[NonSerialized]
		private ReportDrillthroughInfo m_drillthroughInfo;

		// Token: 0x040032B6 RID: 12982
		[NonSerialized]
		private InfoBase m_sortFilterEventInfo;

		// Token: 0x040032B7 RID: 12983
		[NonSerialized]
		private List<PageSectionInstance> m_pageSections;

		// Token: 0x040032B8 RID: 12984
		[NonSerialized]
		private string m_reportName;
	}
}
