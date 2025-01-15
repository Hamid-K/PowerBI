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
	// Token: 0x020003E1 RID: 993
	[Serializable]
	internal sealed class RadialGauge : Gauge, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002896 RID: 10390 RVA: 0x000BE066 File Offset: 0x000BC266
		internal RadialGauge()
		{
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000BE06E File Offset: 0x000BC26E
		internal RadialGauge(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000BE078 File Offset: 0x000BC278
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x000BE080 File Offset: 0x000BC280
		internal List<RadialScale> GaugeScales
		{
			get
			{
				return this.m_gaugeScales;
			}
			set
			{
				this.m_gaugeScales = value;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x000BE089 File Offset: 0x000BC289
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x000BE091 File Offset: 0x000BC291
		internal ExpressionInfo PivotX
		{
			get
			{
				return this.m_pivotX;
			}
			set
			{
				this.m_pivotX = value;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000BE09A File Offset: 0x000BC29A
		// (set) Token: 0x0600289D RID: 10397 RVA: 0x000BE0A2 File Offset: 0x000BC2A2
		internal ExpressionInfo PivotY
		{
			get
			{
				return this.m_pivotY;
			}
			set
			{
				this.m_pivotY = value;
			}
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000BE0AC File Offset: 0x000BC2AC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.RadialGaugeStart(this.m_name);
			base.Initialize(context);
			if (this.m_gaugeScales != null)
			{
				for (int i = 0; i < this.m_gaugeScales.Count; i++)
				{
					this.m_gaugeScales[i].Initialize(context);
				}
			}
			if (this.m_pivotX != null)
			{
				this.m_pivotX.Initialize("PivotX", context);
				context.ExprHostBuilder.RadialGaugePivotX(this.m_pivotX);
			}
			if (this.m_pivotY != null)
			{
				this.m_pivotY.Initialize("PivotY", context);
				context.ExprHostBuilder.RadialGaugePivotY(this.m_pivotY);
			}
			this.m_exprHostID = context.ExprHostBuilder.RadialGaugeEnd();
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000BE16C File Offset: 0x000BC36C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			RadialGauge radialGauge = (RadialGauge)base.PublishClone(context);
			if (this.m_gaugeScales != null)
			{
				radialGauge.m_gaugeScales = new List<RadialScale>(this.m_gaugeScales.Count);
				foreach (RadialScale radialScale in this.m_gaugeScales)
				{
					radialGauge.m_gaugeScales.Add((RadialScale)radialScale.PublishClone(context));
				}
			}
			if (this.m_pivotX != null)
			{
				radialGauge.m_pivotX = (ExpressionInfo)this.m_pivotX.PublishClone(context);
			}
			if (this.m_pivotY != null)
			{
				radialGauge.m_pivotY = (ExpressionInfo)this.m_pivotY.PublishClone(context);
			}
			return radialGauge;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000BE23C File Offset: 0x000BC43C
		internal void SetExprHost(RadialGaugeExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			IList<RadialScaleExprHost> radialScalesHostsRemotable = ((RadialGaugeExprHost)this.m_exprHost).RadialScalesHostsRemotable;
			if (this.m_gaugeScales != null && radialScalesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_gaugeScales.Count; i++)
				{
					RadialScale radialScale = this.m_gaugeScales[i];
					if (radialScale != null && radialScale.ExpressionHostID > -1)
					{
						radialScale.SetExprHost(radialScalesHostsRemotable[radialScale.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000BE2CC File Offset: 0x000BC4CC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialGauge, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Gauge, new List<MemberInfo>
			{
				new MemberInfo(MemberName.GaugeScales, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialScale),
				new MemberInfo(MemberName.PivotX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PivotY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000BE330 File Offset: 0x000BC530
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RadialGauge.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.GaugeScales:
					writer.Write<RadialScale>(this.m_gaugeScales);
					break;
				case MemberName.PivotX:
					writer.Write(this.m_pivotX);
					break;
				case MemberName.PivotY:
					writer.Write(this.m_pivotY);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000BE3BC File Offset: 0x000BC5BC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RadialGauge.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.GaugeScales:
					this.m_gaugeScales = reader.ReadGenericListOfRIFObjects<RadialScale>();
					break;
				case MemberName.PivotX:
					this.m_pivotX = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.PivotY:
					this.m_pivotY = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000BE450 File Offset: 0x000BC650
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialGauge;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000BE457 File Offset: 0x000BC657
		internal double EvaluatePivotX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateRadialGaugePivotXExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000BE47D File Offset: 0x000BC67D
		internal double EvaluatePivotY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateRadialGaugePivotYExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016CC RID: 5836
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RadialGauge.GetDeclaration();

		// Token: 0x040016CD RID: 5837
		private List<RadialScale> m_gaugeScales;

		// Token: 0x040016CE RID: 5838
		private ExpressionInfo m_pivotX;

		// Token: 0x040016CF RID: 5839
		private ExpressionInfo m_pivotY;
	}
}
