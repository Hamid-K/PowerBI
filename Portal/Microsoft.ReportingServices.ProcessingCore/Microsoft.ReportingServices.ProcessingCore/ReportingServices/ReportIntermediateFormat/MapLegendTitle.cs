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
	// Token: 0x02000429 RID: 1065
	[Serializable]
	internal sealed class MapLegendTitle : MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F22 RID: 12066 RVA: 0x000D5AC7 File Offset: 0x000D3CC7
		internal MapLegendTitle()
		{
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000D5ACF File Offset: 0x000D3CCF
		internal MapLegendTitle(Map map)
			: base(map)
		{
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x000D5AD8 File Offset: 0x000D3CD8
		// (set) Token: 0x06002F25 RID: 12069 RVA: 0x000D5AE0 File Offset: 0x000D3CE0
		internal ExpressionInfo Caption
		{
			get
			{
				return this.m_caption;
			}
			set
			{
				this.m_caption = value;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000D5AE9 File Offset: 0x000D3CE9
		// (set) Token: 0x06002F27 RID: 12071 RVA: 0x000D5AF1 File Offset: 0x000D3CF1
		internal ExpressionInfo TitleSeparator
		{
			get
			{
				return this.m_titleSeparator;
			}
			set
			{
				this.m_titleSeparator = value;
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000D5AFA File Offset: 0x000D3CFA
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x000D5B02 File Offset: 0x000D3D02
		internal ExpressionInfo TitleSeparatorColor
		{
			get
			{
				return this.m_titleSeparatorColor;
			}
			set
			{
				this.m_titleSeparatorColor = value;
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x000D5B0B File Offset: 0x000D3D0B
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000D5B18 File Offset: 0x000D3D18
		internal MapLegendTitleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000D5B20 File Offset: 0x000D3D20
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLegendTitleStart();
			base.Initialize(context);
			if (this.m_caption != null)
			{
				this.m_caption.Initialize("Caption", context);
				context.ExprHostBuilder.MapLegendTitleCaption(this.m_caption);
			}
			if (this.m_titleSeparator != null)
			{
				this.m_titleSeparator.Initialize("TitleSeparator", context);
				context.ExprHostBuilder.MapLegendTitleTitleSeparator(this.m_titleSeparator);
			}
			if (this.m_titleSeparatorColor != null)
			{
				this.m_titleSeparatorColor.Initialize("TitleSeparatorColor", context);
				context.ExprHostBuilder.MapLegendTitleTitleSeparatorColor(this.m_titleSeparatorColor);
			}
			context.ExprHostBuilder.MapLegendTitleEnd();
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000D5BD0 File Offset: 0x000D3DD0
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapLegendTitle mapLegendTitle = (MapLegendTitle)base.PublishClone(context);
			if (this.m_caption != null)
			{
				mapLegendTitle.m_caption = (ExpressionInfo)this.m_caption.PublishClone(context);
			}
			if (this.m_titleSeparator != null)
			{
				mapLegendTitle.m_titleSeparator = (ExpressionInfo)this.m_titleSeparator.PublishClone(context);
			}
			if (this.m_titleSeparatorColor != null)
			{
				mapLegendTitle.m_titleSeparatorColor = (ExpressionInfo)this.m_titleSeparatorColor.PublishClone(context);
			}
			return mapLegendTitle;
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000D5C48 File Offset: 0x000D3E48
		internal void SetExprHost(MapLegendTitleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000D5C74 File Offset: 0x000D3E74
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLegendTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Caption, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TitleSeparator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TitleSeparatorColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000D5CD8 File Offset: 0x000D3ED8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapLegendTitle.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Caption)
				{
					if (memberName != MemberName.TitleSeparator)
					{
						if (memberName != MemberName.TitleSeparatorColor)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_titleSeparatorColor);
						}
					}
					else
					{
						writer.Write(this.m_titleSeparator);
					}
				}
				else
				{
					writer.Write(this.m_caption);
				}
			}
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000D5D64 File Offset: 0x000D3F64
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapLegendTitle.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Caption)
				{
					if (memberName != MemberName.TitleSeparator)
					{
						if (memberName != MemberName.TitleSeparatorColor)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_titleSeparatorColor = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_titleSeparator = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_caption = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000D5DFD File Offset: 0x000D3FFD
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLegendTitle;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000D5E04 File Offset: 0x000D4004
		internal string EvaluateCaption(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapLegendTitleCaptionExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000D5E44 File Offset: 0x000D4044
		internal MapLegendTitleSeparator EvaluateTitleSeparator(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapLegendTitleSeparator(context.ReportRuntime.EvaluateMapLegendTitleTitleSeparatorExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000D5E75 File Offset: 0x000D4075
		internal string EvaluateTitleSeparatorColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendTitleTitleSeparatorColorExpression(this, this.m_map.Name);
		}

		// Token: 0x040018A4 RID: 6308
		[NonSerialized]
		private MapLegendTitleExprHost m_exprHost;

		// Token: 0x040018A5 RID: 6309
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLegendTitle.GetDeclaration();

		// Token: 0x040018A6 RID: 6310
		private ExpressionInfo m_caption;

		// Token: 0x040018A7 RID: 6311
		private ExpressionInfo m_titleSeparator;

		// Token: 0x040018A8 RID: 6312
		private ExpressionInfo m_titleSeparatorColor;
	}
}
