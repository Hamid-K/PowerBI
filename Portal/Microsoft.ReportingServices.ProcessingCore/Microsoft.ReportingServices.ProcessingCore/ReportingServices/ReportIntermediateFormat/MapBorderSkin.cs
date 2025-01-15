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
	// Token: 0x02000421 RID: 1057
	[Serializable]
	internal sealed class MapBorderSkin : MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002E54 RID: 11860 RVA: 0x000D30BB File Offset: 0x000D12BB
		internal MapBorderSkin()
		{
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000D30C3 File Offset: 0x000D12C3
		internal MapBorderSkin(Map map)
			: base(map)
		{
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000D30CC File Offset: 0x000D12CC
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x000D30D4 File Offset: 0x000D12D4
		internal ExpressionInfo MapBorderSkinType
		{
			get
			{
				return this.m_mapBorderSkinType;
			}
			set
			{
				this.m_mapBorderSkinType = value;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000D30DD File Offset: 0x000D12DD
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000D30EA File Offset: 0x000D12EA
		internal MapBorderSkinExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000D30F4 File Offset: 0x000D12F4
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapBorderSkinStart();
			base.Initialize(context);
			if (this.m_mapBorderSkinType != null)
			{
				this.m_mapBorderSkinType.Initialize("MapBorderSkinType", context);
				context.ExprHostBuilder.MapBorderSkinMapBorderSkinType(this.m_mapBorderSkinType);
			}
			context.ExprHostBuilder.MapBorderSkinEnd();
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000D314C File Offset: 0x000D134C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapBorderSkin mapBorderSkin = (MapBorderSkin)base.PublishClone(context);
			if (this.m_mapBorderSkinType != null)
			{
				mapBorderSkin.m_mapBorderSkinType = (ExpressionInfo)this.m_mapBorderSkinType.PublishClone(context);
			}
			return mapBorderSkin;
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000D3186 File Offset: 0x000D1386
		internal void SetExprHost(MapBorderSkinExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000D31B0 File Offset: 0x000D13B0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBorderSkin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapBorderSkinType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000D31E8 File Offset: 0x000D13E8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapBorderSkin.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.MapBorderSkinType)
				{
					writer.Write(this.m_mapBorderSkinType);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000D3240 File Offset: 0x000D1440
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapBorderSkin.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.MapBorderSkinType)
				{
					this.m_mapBorderSkinType = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000D329D File Offset: 0x000D149D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBorderSkin;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000D32A4 File Offset: 0x000D14A4
		internal MapBorderSkinType EvaluateMapBorderSkinType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapBorderSkinType(context.ReportRuntime.EvaluateMapBorderSkinMapBorderSkinTypeExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x0400186F RID: 6255
		[NonSerialized]
		private MapBorderSkinExprHost m_exprHost;

		// Token: 0x04001870 RID: 6256
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapBorderSkin.GetDeclaration();

		// Token: 0x04001871 RID: 6257
		private ExpressionInfo m_mapBorderSkinType;
	}
}
