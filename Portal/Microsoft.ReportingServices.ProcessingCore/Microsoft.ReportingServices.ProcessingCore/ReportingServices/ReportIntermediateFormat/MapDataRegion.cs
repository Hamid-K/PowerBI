using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000417 RID: 1047
	[Serializable]
	internal sealed class MapDataRegion : DataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002DAF RID: 11695 RVA: 0x000D10BE File Offset: 0x000CF2BE
		internal MapDataRegion(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000D10C7 File Offset: 0x000CF2C7
		internal MapDataRegion(int id, ReportItem parent)
			: base(id, parent)
		{
			base.RowCount = 1;
			base.ColumnCount = 1;
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000D10DF File Offset: 0x000CF2DF
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000D10E3 File Offset: 0x000CF2E3
		internal override HierarchyNodeList ColumnMembers
		{
			get
			{
				return this.m_columnMembers;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000D10EB File Offset: 0x000CF2EB
		internal override HierarchyNodeList RowMembers
		{
			get
			{
				return this.m_rowMembers;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000D10F3 File Offset: 0x000CF2F3
		internal override RowList Rows
		{
			get
			{
				return this.m_rows;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000D10FB File Offset: 0x000CF2FB
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x000D1121 File Offset: 0x000CF321
		internal MapMember MapMember
		{
			get
			{
				if (this.m_columnMembers != null && this.m_columnMembers.Count == 1)
				{
					return this.m_columnMembers[0];
				}
				return null;
			}
			set
			{
				if (this.m_columnMembers == null)
				{
					this.m_columnMembers = new MapMemberList();
				}
				else
				{
					this.m_columnMembers.Clear();
				}
				this.m_innerMostMapMember = null;
				this.m_columnMembers.Add(value);
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000D1157 File Offset: 0x000CF357
		internal MapMember InnerMostMapMember
		{
			get
			{
				if (this.m_innerMostMapMember == null)
				{
					this.m_innerMostMapMember = this.MapMember;
					while (this.m_innerMostMapMember.ChildMapMember != null)
					{
						this.m_innerMostMapMember = this.m_innerMostMapMember.ChildMapMember;
					}
				}
				return this.m_innerMostMapMember;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000D1193 File Offset: 0x000CF393
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x000D11B9 File Offset: 0x000CF3B9
		internal MapMember MapRowMember
		{
			get
			{
				if (this.m_rowMembers != null && this.m_rowMembers.Count == 1)
				{
					return this.m_rowMembers[0];
				}
				return null;
			}
			set
			{
				if (this.m_rowMembers == null)
				{
					this.m_rowMembers = new MapMemberList();
				}
				else
				{
					this.m_rowMembers.Clear();
				}
				this.m_rowMembers.Add(value);
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000D11E8 File Offset: 0x000CF3E8
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x000D120E File Offset: 0x000CF40E
		internal MapRow MapRow
		{
			get
			{
				if (this.m_rows != null && this.m_rows.Count == 1)
				{
					return this.m_rows[0];
				}
				return null;
			}
			set
			{
				if (this.m_rows == null)
				{
					this.m_rows = new MapRowList();
				}
				else
				{
					this.m_rows.Clear();
				}
				this.m_rows.Add(value);
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000D123D File Offset: 0x000CF43D
		internal MapDataRegionExprHost MapDataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000D1245 File Offset: 0x000CF445
		protected override IndexedExprHost UserSortExpressionsHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000D125C File Offset: 0x000CF45C
		private Map Map
		{
			get
			{
				return (Map)this.m_parent;
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000D126C File Offset: 0x000CF46C
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && (context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsDataRegionInDetailList, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			else
			{
				if (!context.RegisterDataRegion(this))
				{
					return false;
				}
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
				context.ExprHostBuilder.DataRegionStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.MapDataRegion, this.m_name);
				base.Initialize(context);
				base.ExprHostID = context.ExprHostBuilder.DataRegionEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.MapDataRegion);
				context.UnRegisterDataRegion(this);
			}
			return false;
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000D1322 File Offset: 0x000CF522
		protected override void InitializeCorner(InitializationContext context)
		{
			if (this.MapRow != null)
			{
				this.MapRow.Initialize(context);
			}
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000D1338 File Offset: 0x000CF538
		protected override bool ValidateInnerStructure(InitializationContext context)
		{
			return true;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000D133C File Offset: 0x000CF53C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapDataRegion mapDataRegion = (MapDataRegion)base.PublishClone(context);
			context.CurrentDataRegionClone = mapDataRegion;
			mapDataRegion.m_parent = context.CurrentMapClone;
			mapDataRegion.m_rows = new MapRowList();
			mapDataRegion.m_rowMembers = new MapMemberList();
			mapDataRegion.m_columnMembers = new MapMemberList();
			if (this.MapMember != null)
			{
				mapDataRegion.MapMember = (MapMember)this.MapMember.PublishClone(context, mapDataRegion);
			}
			if (this.MapRowMember != null)
			{
				mapDataRegion.MapRowMember = (MapMember)this.MapRowMember.PublishClone(context);
			}
			if (this.MapRow != null)
			{
				mapDataRegion.MapRow = (MapRow)this.MapRow.PublishClone(context);
			}
			return mapDataRegion;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000D13EB File Offset: 0x000CF5EB
		internal override object EvaluateNoRowsMessageExpression()
		{
			return null;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000D13F0 File Offset: 0x000CF5F0
		[SkipMemberStaticValidation(MemberName.MapMember)]
		[SkipMemberStaticValidation(MemberName.MapRowMember)]
		[SkipMemberStaticValidation(MemberName.MapRow)]
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMember),
				new MemberInfo(MemberName.MapRowMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMember),
				new MemberInfo(MemberName.MapRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapRow)
			});
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000D1454 File Offset: 0x000CF654
		internal List<MapVectorLayer> GetChildVectorLayers()
		{
			List<MapVectorLayer> list = new List<MapVectorLayer>();
			if (this.Map.MapLayers != null)
			{
				foreach (MapLayer mapLayer in this.Map.MapLayers)
				{
					if (mapLayer is MapVectorLayer)
					{
						MapVectorLayer mapVectorLayer = (MapVectorLayer)mapLayer;
						if (string.Equals(mapVectorLayer.MapDataRegionName, base.Name, StringComparison.Ordinal))
						{
							list.Add(mapVectorLayer);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000D14E4 File Offset: 0x000CF6E4
		public override void CreateDomainScopeMember(ReportHierarchyNode parentNode, Grouping grouping, AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "CreateDomainScopeMember should not be called for MapDataRegion");
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000D14F8 File Offset: 0x000CF6F8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapDataRegion.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.MapMember:
					writer.Write(this.MapMember);
					break;
				case MemberName.MapRowMember:
					writer.Write(this.MapRowMember);
					break;
				case MemberName.MapRow:
					writer.Write(this.MapRow);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000D1584 File Offset: 0x000CF784
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapDataRegion.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.MapMember:
					this.MapMember = (MapMember)reader.ReadRIFObject();
					break;
				case MemberName.MapRowMember:
					this.MapRowMember = (MapMember)reader.ReadRIFObject();
					break;
				case MemberName.MapRow:
					this.MapRow = (MapRow)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000D161D File Offset: 0x000CF81D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000D1627 File Offset: 0x000CF827
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataRegion;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000D1630 File Offset: 0x000CF830
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.MapDataRegionHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, this.m_exprHost.SortHost, this.m_exprHost.FilterHostsRemotable, this.m_exprHost.UserSortExpressionsHost, this.m_exprHost.PageBreakExprHost, this.m_exprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000D16B5 File Offset: 0x000CF8B5
		internal override void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
		}

		// Token: 0x0400184B RID: 6219
		[NonSerialized]
		private MapMemberList m_columnMembers;

		// Token: 0x0400184C RID: 6220
		[NonSerialized]
		private MapMemberList m_rowMembers;

		// Token: 0x0400184D RID: 6221
		[NonSerialized]
		private MapMember m_innerMostMapMember;

		// Token: 0x0400184E RID: 6222
		[NonSerialized]
		private MapRowList m_rows;

		// Token: 0x0400184F RID: 6223
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapDataRegion.GetDeclaration();

		// Token: 0x04001850 RID: 6224
		[NonSerialized]
		private MapDataRegionExprHost m_exprHost;
	}
}
