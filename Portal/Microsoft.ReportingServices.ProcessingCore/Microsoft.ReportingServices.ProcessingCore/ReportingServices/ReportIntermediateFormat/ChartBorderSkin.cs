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
	// Token: 0x020004A0 RID: 1184
	[Serializable]
	internal sealed class ChartBorderSkin : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600397E RID: 14718 RVA: 0x000F9F09 File Offset: 0x000F8109
		internal ChartBorderSkin()
		{
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000F9F11 File Offset: 0x000F8111
		internal ChartBorderSkin(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x000F9F1A File Offset: 0x000F811A
		internal ChartBorderSkinExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000F9F22 File Offset: 0x000F8122
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x000F9F2A File Offset: 0x000F812A
		internal ExpressionInfo BorderSkinType
		{
			get
			{
				return this.m_borderSkinType;
			}
			set
			{
				this.m_borderSkinType = value;
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000F9F33 File Offset: 0x000F8133
		internal void SetExprHost(ChartBorderSkinExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000F9F60 File Offset: 0x000F8160
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartBorderSkinStart();
			base.Initialize(context);
			if (this.m_borderSkinType != null)
			{
				this.m_borderSkinType.Initialize("ChartBorderSkinType", context);
				context.ExprHostBuilder.ChartBorderSkinBorderSkinType(this.m_borderSkinType);
			}
			context.ExprHostBuilder.ChartBorderSkinEnd();
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000F9FB8 File Offset: 0x000F81B8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartBorderSkin chartBorderSkin = (ChartBorderSkin)base.PublishClone(context);
			if (this.m_borderSkinType != null)
			{
				chartBorderSkin.m_borderSkinType = (ExpressionInfo)this.m_borderSkinType.PublishClone(context);
			}
			return chartBorderSkin;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000F9FF4 File Offset: 0x000F81F4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartBorderSkin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.BorderSkinType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000FA02C File Offset: 0x000F822C
		internal ChartBorderSkinType EvaluateBorderSkinType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartBorderSkinType(context.ReportRuntime.EvaluateChartBorderSkinBorderSkinTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000FA060 File Offset: 0x000F8260
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartBorderSkin.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.BorderSkinType)
				{
					writer.Write(this.m_borderSkinType);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000FA0B8 File Offset: 0x000F82B8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartBorderSkin.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.BorderSkinType)
				{
					this.m_borderSkinType = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000FA115 File Offset: 0x000F8315
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000FA11F File Offset: 0x000F831F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartBorderSkin;
		}

		// Token: 0x04001B97 RID: 7063
		private ExpressionInfo m_borderSkinType;

		// Token: 0x04001B98 RID: 7064
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartBorderSkin.GetDeclaration();

		// Token: 0x04001B99 RID: 7065
		[NonSerialized]
		private ChartBorderSkinExprHost m_exprHost;
	}
}
