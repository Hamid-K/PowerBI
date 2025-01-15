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
	// Token: 0x020003E0 RID: 992
	[Serializable]
	internal sealed class LinearGauge : Gauge, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002887 RID: 10375 RVA: 0x000BDCDE File Offset: 0x000BBEDE
		internal LinearGauge()
		{
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000BDCE6 File Offset: 0x000BBEE6
		internal LinearGauge(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000BDCF0 File Offset: 0x000BBEF0
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x000BDCF8 File Offset: 0x000BBEF8
		internal List<LinearScale> GaugeScales
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

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x000BDD01 File Offset: 0x000BBF01
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x000BDD09 File Offset: 0x000BBF09
		internal ExpressionInfo Orientation
		{
			get
			{
				return this.m_orientation;
			}
			set
			{
				this.m_orientation = value;
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000BDD14 File Offset: 0x000BBF14
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.LinearGaugeStart(this.m_name);
			base.Initialize(context);
			if (this.m_gaugeScales != null)
			{
				for (int i = 0; i < this.m_gaugeScales.Count; i++)
				{
					this.m_gaugeScales[i].Initialize(context);
				}
			}
			if (this.m_orientation != null)
			{
				this.m_orientation.Initialize("Orientation", context);
				context.ExprHostBuilder.LinearGaugeOrientation(this.m_orientation);
			}
			this.m_exprHostID = context.ExprHostBuilder.LinearGaugeEnd();
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000BDDA8 File Offset: 0x000BBFA8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			LinearGauge linearGauge = (LinearGauge)base.PublishClone(context);
			if (this.m_gaugeScales != null)
			{
				linearGauge.m_gaugeScales = new List<LinearScale>(this.m_gaugeScales.Count);
				foreach (LinearScale linearScale in this.m_gaugeScales)
				{
					linearGauge.m_gaugeScales.Add((LinearScale)linearScale.PublishClone(context));
				}
			}
			if (this.m_orientation != null)
			{
				linearGauge.m_orientation = (ExpressionInfo)this.m_orientation.PublishClone(context);
			}
			return linearGauge;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000BDE58 File Offset: 0x000BC058
		internal void SetExprHost(LinearGaugeExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			IList<LinearScaleExprHost> linearScalesHostsRemotable = ((LinearGaugeExprHost)this.m_exprHost).LinearScalesHostsRemotable;
			if (this.m_gaugeScales != null && linearScalesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_gaugeScales.Count; i++)
				{
					LinearScale linearScale = this.m_gaugeScales[i];
					if (linearScale != null && linearScale.ExpressionHostID > -1)
					{
						linearScale.SetExprHost(linearScalesHostsRemotable[linearScale.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000BDEE8 File Offset: 0x000BC0E8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearGauge, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Gauge, new List<MemberInfo>
			{
				new MemberInfo(MemberName.GaugeScales, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearScale),
				new MemberInfo(MemberName.Orientation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000BDF38 File Offset: 0x000BC138
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(LinearGauge.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Orientation)
				{
					if (memberName == MemberName.GaugeScales)
					{
						writer.Write<LinearScale>(this.m_gaugeScales);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_orientation);
				}
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000BDFAC File Offset: 0x000BC1AC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(LinearGauge.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Orientation)
				{
					if (memberName == MemberName.GaugeScales)
					{
						this.m_gaugeScales = reader.ReadGenericListOfRIFObjects<LinearScale>();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_orientation = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000BE022 File Offset: 0x000BC222
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearGauge;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000BE029 File Offset: 0x000BC229
		internal GaugeOrientations EvaluateOrientation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeOrientations(context.ReportRuntime.EvaluateLinearGaugeOrientationExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x040016C9 RID: 5833
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LinearGauge.GetDeclaration();

		// Token: 0x040016CA RID: 5834
		private List<LinearScale> m_gaugeScales;

		// Token: 0x040016CB RID: 5835
		private ExpressionInfo m_orientation;
	}
}
