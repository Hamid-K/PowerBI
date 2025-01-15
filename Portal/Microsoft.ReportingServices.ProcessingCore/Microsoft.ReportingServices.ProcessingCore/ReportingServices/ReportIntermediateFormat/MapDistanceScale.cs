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
	// Token: 0x02000426 RID: 1062
	[Serializable]
	internal sealed class MapDistanceScale : MapDockableSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002ED7 RID: 11991 RVA: 0x000D4B6C File Offset: 0x000D2D6C
		internal MapDistanceScale()
		{
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000D4B74 File Offset: 0x000D2D74
		internal MapDistanceScale(Map map, int id)
			: base(map, id)
		{
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000D4B7E File Offset: 0x000D2D7E
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x000D4B86 File Offset: 0x000D2D86
		internal ExpressionInfo ScaleColor
		{
			get
			{
				return this.m_scaleColor;
			}
			set
			{
				this.m_scaleColor = value;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000D4B8F File Offset: 0x000D2D8F
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x000D4B97 File Offset: 0x000D2D97
		internal ExpressionInfo ScaleBorderColor
		{
			get
			{
				return this.m_scaleBorderColor;
			}
			set
			{
				this.m_scaleBorderColor = value;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000D4BA0 File Offset: 0x000D2DA0
		internal new MapDistanceScaleExprHost ExprHost
		{
			get
			{
				return (MapDistanceScaleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000D4BB0 File Offset: 0x000D2DB0
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapDistanceScaleStart();
			base.Initialize(context);
			if (this.m_scaleColor != null)
			{
				this.m_scaleColor.Initialize("ScaleColor", context);
				context.ExprHostBuilder.MapDistanceScaleScaleColor(this.m_scaleColor);
			}
			if (this.m_scaleBorderColor != null)
			{
				this.m_scaleBorderColor.Initialize("ScaleBorderColor", context);
				context.ExprHostBuilder.MapDistanceScaleScaleBorderColor(this.m_scaleBorderColor);
			}
			context.ExprHostBuilder.MapDistanceScaleEnd();
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000D4C34 File Offset: 0x000D2E34
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapDistanceScale mapDistanceScale = (MapDistanceScale)base.PublishClone(context);
			if (this.m_scaleColor != null)
			{
				mapDistanceScale.m_scaleColor = (ExpressionInfo)this.m_scaleColor.PublishClone(context);
			}
			if (this.m_scaleBorderColor != null)
			{
				mapDistanceScale.m_scaleBorderColor = (ExpressionInfo)this.m_scaleBorderColor.PublishClone(context);
			}
			return mapDistanceScale;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000D4C8D File Offset: 0x000D2E8D
		internal void SetExprHost(MapDistanceScaleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000D4CB0 File Offset: 0x000D2EB0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDistanceScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDockableSubItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ScaleColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ScaleBorderColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000D4D00 File Offset: 0x000D2F00
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapDistanceScale.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ScaleColor)
				{
					if (memberName != MemberName.ScaleBorderColor)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_scaleBorderColor);
					}
				}
				else
				{
					writer.Write(this.m_scaleColor);
				}
			}
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000D4D74 File Offset: 0x000D2F74
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapDistanceScale.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ScaleColor)
				{
					if (memberName != MemberName.ScaleBorderColor)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_scaleBorderColor = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_scaleColor = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000D4DF1 File Offset: 0x000D2FF1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDistanceScale;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000D4DF8 File Offset: 0x000D2FF8
		internal string EvaluateScaleColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapDistanceScaleScaleColorExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000D4E1E File Offset: 0x000D301E
		internal string EvaluateScaleBorderColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapDistanceScaleScaleBorderColorExpression(this, this.m_map.Name);
		}

		// Token: 0x04001892 RID: 6290
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapDistanceScale.GetDeclaration();

		// Token: 0x04001893 RID: 6291
		private ExpressionInfo m_scaleColor;

		// Token: 0x04001894 RID: 6292
		private ExpressionInfo m_scaleBorderColor;
	}
}
