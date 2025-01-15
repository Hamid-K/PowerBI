using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000432 RID: 1074
	[Serializable]
	internal sealed class MapSpatialDataRegion : MapSpatialData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002FCF RID: 12239 RVA: 0x000D820F File Offset: 0x000D640F
		internal MapSpatialDataRegion()
		{
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000D8217 File Offset: 0x000D6417
		internal MapSpatialDataRegion(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000D8221 File Offset: 0x000D6421
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x000D8229 File Offset: 0x000D6429
		internal ExpressionInfo VectorData
		{
			get
			{
				return this.m_vectorData;
			}
			set
			{
				this.m_vectorData = value;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000D8232 File Offset: 0x000D6432
		internal new MapSpatialDataRegionExprHost ExprHost
		{
			get
			{
				return (MapSpatialDataRegionExprHost)this.m_exprHost;
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000D823F File Offset: 0x000D643F
		private IInstancePath InstancePath
		{
			get
			{
				return this.m_mapVectorLayer.InstancePath;
			}
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000D824C File Offset: 0x000D644C
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapSpatialDataRegionStart();
			base.InitializeMapMember(context);
			if (this.m_vectorData != null)
			{
				this.m_vectorData.Initialize("VectorData", context);
				context.ExprHostBuilder.MapSpatialDataRegionVectorData(this.m_vectorData);
			}
			context.ExprHostBuilder.MapSpatialDataRegionEnd();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000D82A4 File Offset: 0x000D64A4
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapSpatialDataRegion mapSpatialDataRegion = (MapSpatialDataRegion)base.PublishClone(context);
			if (this.m_vectorData != null)
			{
				mapSpatialDataRegion.m_vectorData = (ExpressionInfo)this.m_vectorData.PublishClone(context);
			}
			return mapSpatialDataRegion;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000D82DE File Offset: 0x000D64DE
		internal override void SetExprHostMapMember(MapSpatialDataExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			base.SetExprHostInternal(exprHost, reportObjectModel);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000D82E8 File Offset: 0x000D64E8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialDataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialData, new List<MemberInfo>
			{
				new MemberInfo(MemberName.VectorData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000D8320 File Offset: 0x000D6520
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapSpatialDataRegion.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.VectorData)
				{
					writer.Write(this.m_vectorData);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000D8378 File Offset: 0x000D6578
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapSpatialDataRegion.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.VectorData)
				{
					this.m_vectorData = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000D83D5 File Offset: 0x000D65D5
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialDataRegion;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000D83DC File Offset: 0x000D65DC
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateVectorData(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSpatialDataRegionVectorDataExpression(this, this.m_map.Name);
		}

		// Token: 0x040018CF RID: 6351
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSpatialDataRegion.GetDeclaration();

		// Token: 0x040018D0 RID: 6352
		private ExpressionInfo m_vectorData;
	}
}
