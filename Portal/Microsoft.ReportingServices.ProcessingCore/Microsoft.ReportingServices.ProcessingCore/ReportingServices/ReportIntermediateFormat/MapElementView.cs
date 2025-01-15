using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000455 RID: 1109
	[Serializable]
	internal sealed class MapElementView : MapView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060032B1 RID: 12977 RVA: 0x000E1F22 File Offset: 0x000E0122
		internal MapElementView()
		{
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000E1F2A File Offset: 0x000E012A
		internal MapElementView(Map map)
			: base(map)
		{
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000E1F33 File Offset: 0x000E0133
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x000E1F3B File Offset: 0x000E013B
		internal ExpressionInfo LayerName
		{
			get
			{
				return this.m_layerName;
			}
			set
			{
				this.m_layerName = value;
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000E1F44 File Offset: 0x000E0144
		// (set) Token: 0x060032B6 RID: 12982 RVA: 0x000E1F4C File Offset: 0x000E014C
		internal List<MapBindingFieldPair> MapBindingFieldPairs
		{
			get
			{
				return this.m_mapBindingFieldPairs;
			}
			set
			{
				this.m_mapBindingFieldPairs = value;
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000E1F55 File Offset: 0x000E0155
		internal new MapElementViewExprHost ExprHost
		{
			get
			{
				return (MapElementViewExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000E1F64 File Offset: 0x000E0164
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapElementViewStart();
			base.Initialize(context);
			if (this.m_layerName != null)
			{
				this.m_layerName.Initialize("LayerName", context);
				context.ExprHostBuilder.MapElementViewLayerName(this.m_layerName);
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				for (int i = 0; i < this.m_mapBindingFieldPairs.Count; i++)
				{
					this.m_mapBindingFieldPairs[i].Initialize(context, i);
				}
			}
			context.ExprHostBuilder.MapElementViewEnd();
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000E1FEC File Offset: 0x000E01EC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapElementView mapElementView = (MapElementView)base.PublishClone(context);
			if (this.m_layerName != null)
			{
				mapElementView.m_layerName = (ExpressionInfo)this.m_layerName.PublishClone(context);
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				mapElementView.m_mapBindingFieldPairs = new List<MapBindingFieldPair>(this.m_mapBindingFieldPairs.Count);
				foreach (MapBindingFieldPair mapBindingFieldPair in this.m_mapBindingFieldPairs)
				{
					mapElementView.m_mapBindingFieldPairs.Add((MapBindingFieldPair)mapBindingFieldPair.PublishClone(context));
				}
			}
			return mapElementView;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000E209C File Offset: 0x000E029C
		internal override void SetExprHost(MapViewExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			IList<MapBindingFieldPairExprHost> mapBindingFieldPairsHostsRemotable = this.ExprHost.MapBindingFieldPairsHostsRemotable;
			if (this.m_mapBindingFieldPairs != null && mapBindingFieldPairsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapBindingFieldPairs.Count; i++)
				{
					MapBindingFieldPair mapBindingFieldPair = this.m_mapBindingFieldPairs[i];
					if (mapBindingFieldPair != null && mapBindingFieldPair.ExpressionHostID > -1)
					{
						mapBindingFieldPair.SetExprHost(mapBindingFieldPairsHostsRemotable[mapBindingFieldPair.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000E2124 File Offset: 0x000E0324
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapElementView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView, new List<MemberInfo>
			{
				new MemberInfo(MemberName.LayerName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapBindingFieldPairs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBindingFieldPair)
			});
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000E2174 File Offset: 0x000E0374
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapElementView.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.LayerName)
				{
					if (memberName != MemberName.MapBindingFieldPairs)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write<MapBindingFieldPair>(this.m_mapBindingFieldPairs);
					}
				}
				else
				{
					writer.Write(this.m_layerName);
				}
			}
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000E21E8 File Offset: 0x000E03E8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapElementView.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.LayerName)
				{
					if (memberName != MemberName.MapBindingFieldPairs)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_mapBindingFieldPairs = reader.ReadGenericListOfRIFObjects<MapBindingFieldPair>();
					}
				}
				else
				{
					this.m_layerName = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000E2260 File Offset: 0x000E0460
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapElementView;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000E2267 File Offset: 0x000E0467
		internal string EvaluateLayerName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapElementViewLayerNameExpression(this, this.m_map.Name);
		}

		// Token: 0x0400198B RID: 6539
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapElementView.GetDeclaration();

		// Token: 0x0400198C RID: 6540
		private ExpressionInfo m_layerName;

		// Token: 0x0400198D RID: 6541
		private List<MapBindingFieldPair> m_mapBindingFieldPairs;
	}
}
