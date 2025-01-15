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
	// Token: 0x02000425 RID: 1061
	[Serializable]
	internal sealed class MapColorScaleTitle : MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002EC8 RID: 11976 RVA: 0x000D4938 File Offset: 0x000D2B38
		internal MapColorScaleTitle()
		{
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000D4940 File Offset: 0x000D2B40
		internal MapColorScaleTitle(Map map)
			: base(map)
		{
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000D4949 File Offset: 0x000D2B49
		// (set) Token: 0x06002ECB RID: 11979 RVA: 0x000D4951 File Offset: 0x000D2B51
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

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000D495A File Offset: 0x000D2B5A
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000D4967 File Offset: 0x000D2B67
		internal MapColorScaleTitleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000D4970 File Offset: 0x000D2B70
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapColorScaleTitleStart();
			base.Initialize(context);
			if (this.m_caption != null)
			{
				this.m_caption.Initialize("Caption", context);
				context.ExprHostBuilder.MapColorScaleTitleCaption(this.m_caption);
			}
			context.ExprHostBuilder.MapColorScaleTitleEnd();
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000D49C8 File Offset: 0x000D2BC8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapColorScaleTitle mapColorScaleTitle = (MapColorScaleTitle)base.PublishClone(context);
			if (this.m_caption != null)
			{
				mapColorScaleTitle.m_caption = (ExpressionInfo)this.m_caption.PublishClone(context);
			}
			return mapColorScaleTitle;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000D4A02 File Offset: 0x000D2C02
		internal void SetExprHost(MapColorScaleTitleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000D4A2C File Offset: 0x000D2C2C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorScaleTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Caption, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000D4A64 File Offset: 0x000D2C64
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapColorScaleTitle.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Caption)
				{
					writer.Write(this.m_caption);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000D4ABC File Offset: 0x000D2CBC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapColorScaleTitle.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Caption)
				{
					this.m_caption = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000D4B19 File Offset: 0x000D2D19
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorScaleTitle;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000D4B20 File Offset: 0x000D2D20
		internal string EvaluateCaption(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapColorScaleTitleCaptionExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x0400188F RID: 6287
		[NonSerialized]
		private MapColorScaleTitleExprHost m_exprHost;

		// Token: 0x04001890 RID: 6288
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapColorScaleTitle.GetDeclaration();

		// Token: 0x04001891 RID: 6289
		private ExpressionInfo m_caption;
	}
}
