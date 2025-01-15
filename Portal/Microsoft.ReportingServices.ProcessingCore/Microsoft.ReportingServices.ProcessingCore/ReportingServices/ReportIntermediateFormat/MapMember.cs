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
	// Token: 0x02000419 RID: 1049
	[Serializable]
	internal sealed class MapMember : ReportHierarchyNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002DD0 RID: 11728 RVA: 0x000D16D9 File Offset: 0x000CF8D9
		internal MapMember()
		{
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000D16E1 File Offset: 0x000CF8E1
		internal MapMember(int id, MapDataRegion crItem)
			: base(id, crItem)
		{
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000D16EB File Offset: 0x000CF8EB
		internal override string RdlElementName
		{
			get
			{
				return "MapMember";
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000D16F2 File Offset: 0x000CF8F2
		internal override HierarchyNodeList InnerHierarchy
		{
			get
			{
				return this.m_innerMembers;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000D16FA File Offset: 0x000CF8FA
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x000D1720 File Offset: 0x000CF920
		internal MapMember ChildMapMember
		{
			get
			{
				if (this.m_innerMembers != null && this.m_innerMembers.Count == 1)
				{
					return this.m_innerMembers[0];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (this.m_innerMembers == null)
				{
					this.m_innerMembers = new MapMemberList();
				}
				else
				{
					this.m_innerMembers.Clear();
				}
				this.m_innerMembers.Add(value);
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000D1753 File Offset: 0x000CF953
		internal void SetIsCategoryMember(bool value)
		{
			this.m_isColumn = value;
			if (this.ChildMapMember != null)
			{
				this.ChildMapMember.SetIsCategoryMember(value);
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000D1770 File Offset: 0x000CF970
		protected override void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			builder.DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.MapDataRegion, this.m_isColumn);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000D177F File Offset: 0x000CF97F
		protected override int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			return builder.DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.MapDataRegion, this.m_isColumn);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000D178E File Offset: 0x000CF98E
		private List<MapVectorLayer> GetChildMapLayers()
		{
			return ((MapDataRegion)base.DataRegionDef).GetChildVectorLayers();
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000D17A0 File Offset: 0x000CF9A0
		internal override bool InnerInitialize(InitializationContext context, bool restrictive)
		{
			foreach (MapVectorLayer mapVectorLayer in this.GetChildMapLayers())
			{
				mapVectorLayer.InitializeMapMember(context);
			}
			return base.InnerInitialize(context, restrictive);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000D17FC File Offset: 0x000CF9FC
		internal override bool Initialize(InitializationContext context, bool restrictive)
		{
			if (!this.m_isColumn)
			{
				if (this.m_grouping != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidRowMapMemberCannotBeDynamic, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion, context.TablixName, "MapMember", new string[]
					{
						"Group",
						this.m_grouping.Name
					});
				}
				if (this.m_innerMembers != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidRowMapMemberCannotContainChildMember, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion, context.TablixName, "MapMember", Array.Empty<string>());
				}
			}
			else if (this.m_innerMembers != null && this.m_innerMembers.Count > 1)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidColumnMapMemberCannotContainMultipleChildMember, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion, context.TablixName, "MapMember", Array.Empty<string>());
			}
			return base.Initialize(context, restrictive);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000D18CC File Offset: 0x000CFACC
		internal override object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion)
		{
			MapMember mapMember = (MapMember)base.PublishClone(context, newContainingRegion);
			if (this.ChildMapMember != null)
			{
				mapMember.ChildMapMember = (MapMember)this.ChildMapMember.PublishClone(context, newContainingRegion);
			}
			return mapMember;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000D1908 File Offset: 0x000CFB08
		[SkipMemberStaticValidation(MemberName.MapMember)]
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMember)
			});
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000D1940 File Offset: 0x000CFB40
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapMember.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.MapMember)
				{
					writer.Write(this.ChildMapMember);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000D1998 File Offset: 0x000CFB98
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapMember.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.MapMember)
				{
					this.ChildMapMember = (MapMember)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000D19F5 File Offset: 0x000CFBF5
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000D19FF File Offset: 0x000CFBFF
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMember;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000D1A08 File Offset: 0x000CFC08
		internal override void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(memberExprHost != null && reportObjectModel != null);
			base.MemberNodeSetExprHost(memberExprHost, reportObjectModel);
			List<MapVectorLayer> childMapLayers = this.GetChildMapLayers();
			MapMemberExprHost mapMemberExprHost = (MapMemberExprHost)memberExprHost;
			IList<MapPolygonLayerExprHost> mapPolygonLayersHostsRemotable = mapMemberExprHost.MapPolygonLayersHostsRemotable;
			IList<MapPointLayerExprHost> mapPointLayersHostsRemotable = mapMemberExprHost.MapPointLayersHostsRemotable;
			IList<MapLineLayerExprHost> mapLineLayersHostsRemotable = mapMemberExprHost.MapLineLayersHostsRemotable;
			if (childMapLayers != null)
			{
				for (int i = 0; i < childMapLayers.Count; i++)
				{
					MapVectorLayer mapVectorLayer = childMapLayers[i];
					if (mapVectorLayer != null && mapVectorLayer.ExpressionHostMapMemberID > -1)
					{
						if (mapVectorLayer is MapPolygonLayer)
						{
							if (mapPolygonLayersHostsRemotable != null)
							{
								mapVectorLayer.SetExprHostMapMember(mapPolygonLayersHostsRemotable[mapVectorLayer.ExpressionHostMapMemberID], reportObjectModel);
							}
						}
						else if (mapVectorLayer is MapPointLayer)
						{
							if (mapPointLayersHostsRemotable != null)
							{
								mapVectorLayer.SetExprHostMapMember(mapPointLayersHostsRemotable[mapVectorLayer.ExpressionHostMapMemberID], reportObjectModel);
							}
						}
						else if (mapVectorLayer is MapLineLayer && mapLineLayersHostsRemotable != null)
						{
							mapVectorLayer.SetExprHostMapMember(mapLineLayersHostsRemotable[mapVectorLayer.ExpressionHostMapMemberID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000D1AF2 File Offset: 0x000CFCF2
		internal override void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
		}

		// Token: 0x04001851 RID: 6225
		[NonSerialized]
		private MapMemberList m_innerMembers;

		// Token: 0x04001852 RID: 6226
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapMember.GetDeclaration();
	}
}
