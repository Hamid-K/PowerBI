using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000719 RID: 1817
	[Serializable]
	internal sealed class ReportInstance : ReportItemInstance, IPageItem
	{
		// Token: 0x0600656E RID: 25966 RVA: 0x0018F950 File Offset: 0x0018DB50
		internal ReportInstance(ReportProcessing.ProcessingContext pc, Report reportItemDef, ParameterInfoCollection parameters, string reportlanguage, bool noRows)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ReportInstanceInfo(pc, reportItemDef, this, parameters, noRows);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			this.m_reportItemColInstance = new ReportItemColInstance(pc, reportItemDef.ReportItems);
			this.m_language = reportlanguage;
			this.m_noRows = noRows;
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x0018F9BC File Offset: 0x0018DBBC
		internal ReportInstance()
		{
		}

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x0018F9D2 File Offset: 0x0018DBD2
		// (set) Token: 0x06006571 RID: 25969 RVA: 0x0018F9DA File Offset: 0x0018DBDA
		internal ReportItemColInstance ReportItemColInstance
		{
			get
			{
				return this.m_reportItemColInstance;
			}
			set
			{
				this.m_reportItemColInstance = value;
			}
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x0018F9E3 File Offset: 0x0018DBE3
		// (set) Token: 0x06006573 RID: 25971 RVA: 0x0018F9EB File Offset: 0x0018DBEB
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

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x0018F9F4 File Offset: 0x0018DBF4
		// (set) Token: 0x06006575 RID: 25973 RVA: 0x0018F9FC File Offset: 0x0018DBFC
		internal int NumberOfPages
		{
			get
			{
				return this.m_numberOfPages;
			}
			set
			{
				this.m_numberOfPages = value;
			}
		}

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x06006576 RID: 25974 RVA: 0x0018FA05 File Offset: 0x0018DC05
		internal bool NoRows
		{
			get
			{
				return this.m_noRows;
			}
		}

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x06006577 RID: 25975 RVA: 0x0018FA0D File Offset: 0x0018DC0D
		// (set) Token: 0x06006578 RID: 25976 RVA: 0x0018FA15 File Offset: 0x0018DC15
		int IPageItem.StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x0018FA1E File Offset: 0x0018DC1E
		// (set) Token: 0x0600657A RID: 25978 RVA: 0x0018FA26 File Offset: 0x0018DC26
		int IPageItem.EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0018FA30 File Offset: 0x0018DC30
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportItemColInstance, ObjectType.ReportItemColInstance),
				new MemberInfo(MemberName.Language, Token.String),
				new MemberInfo(MemberName.NumberOfPages, Token.Int32)
			});
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x0018FA8A File Offset: 0x0018DC8A
		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return ((ISearchByUniqueName)this.m_reportItemColInstance).Find(targetUniqueName, ref nonCompNames, chunkManager);
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x0018FA9C File Offset: 0x0018DC9C
		internal ReportInstanceInfo GetCachedReportInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				if (this.m_cachedInstanceInfo == null)
				{
					IntermediateFormatReader reader = chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset);
					this.m_cachedInstanceInfo = reader.ReadReportInstanceInfo((Report)this.m_reportItemDef);
				}
				return this.m_cachedInstanceInfo;
			}
			return (ReportInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x0018FAFE File Offset: 0x0018DCFE
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadReportInstanceInfo((Report)this.m_reportItemDef);
		}

		// Token: 0x040032B9 RID: 12985
		private ReportItemColInstance m_reportItemColInstance;

		// Token: 0x040032BA RID: 12986
		private string m_language;

		// Token: 0x040032BB RID: 12987
		private int m_numberOfPages;

		// Token: 0x040032BC RID: 12988
		[NonSerialized]
		private ReportInstanceInfo m_cachedInstanceInfo;

		// Token: 0x040032BD RID: 12989
		[NonSerialized]
		private bool m_noRows;

		// Token: 0x040032BE RID: 12990
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x040032BF RID: 12991
		[NonSerialized]
		private int m_endPage = -1;
	}
}
