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
	// Token: 0x020003F7 RID: 1015
	[Serializable]
	internal sealed class Thermometer : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002AC3 RID: 10947 RVA: 0x000C69E3 File Offset: 0x000C4BE3
		internal Thermometer()
		{
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000C69EB File Offset: 0x000C4BEB
		internal Thermometer(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000C69F4 File Offset: 0x000C4BF4
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000C69FC File Offset: 0x000C4BFC
		internal ExpressionInfo BulbOffset
		{
			get
			{
				return this.m_bulbOffset;
			}
			set
			{
				this.m_bulbOffset = value;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000C6A05 File Offset: 0x000C4C05
		// (set) Token: 0x06002AC8 RID: 10952 RVA: 0x000C6A0D File Offset: 0x000C4C0D
		internal ExpressionInfo BulbSize
		{
			get
			{
				return this.m_bulbSize;
			}
			set
			{
				this.m_bulbSize = value;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x000C6A16 File Offset: 0x000C4C16
		// (set) Token: 0x06002ACA RID: 10954 RVA: 0x000C6A1E File Offset: 0x000C4C1E
		internal ExpressionInfo ThermometerStyle
		{
			get
			{
				return this.m_thermometerStyle;
			}
			set
			{
				this.m_thermometerStyle = value;
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000C6A27 File Offset: 0x000C4C27
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x000C6A34 File Offset: 0x000C4C34
		internal ThermometerExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000C6A3C File Offset: 0x000C4C3C
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ThermometerStart();
			base.Initialize(context);
			if (this.m_bulbOffset != null)
			{
				this.m_bulbOffset.Initialize("BulbOffset", context);
				context.ExprHostBuilder.ThermometerBulbOffset(this.m_bulbOffset);
			}
			if (this.m_bulbSize != null)
			{
				this.m_bulbSize.Initialize("BulbSize", context);
				context.ExprHostBuilder.ThermometerBulbSize(this.m_bulbSize);
			}
			if (this.m_thermometerStyle != null)
			{
				this.m_thermometerStyle.Initialize("ThermometerStyle", context);
				context.ExprHostBuilder.ThermometerThermometerStyle(this.m_thermometerStyle);
			}
			context.ExprHostBuilder.ThermometerEnd();
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000C6AEC File Offset: 0x000C4CEC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Thermometer thermometer = (Thermometer)base.PublishClone(context);
			if (this.m_bulbOffset != null)
			{
				thermometer.m_bulbOffset = (ExpressionInfo)this.m_bulbOffset.PublishClone(context);
			}
			if (this.m_bulbSize != null)
			{
				thermometer.m_bulbSize = (ExpressionInfo)this.m_bulbSize.PublishClone(context);
			}
			if (this.m_thermometerStyle != null)
			{
				thermometer.m_thermometerStyle = (ExpressionInfo)this.m_thermometerStyle.PublishClone(context);
			}
			return thermometer;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000C6B64 File Offset: 0x000C4D64
		internal void SetExprHost(ThermometerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000C6B8C File Offset: 0x000C4D8C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Thermometer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.BulbOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BulbSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ThermometerStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000C6BF0 File Offset: 0x000C4DF0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Thermometer.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.BulbOffset:
					writer.Write(this.m_bulbOffset);
					break;
				case MemberName.BulbSize:
					writer.Write(this.m_bulbSize);
					break;
				case MemberName.ThermometerStyle:
					writer.Write(this.m_thermometerStyle);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000C6C7C File Offset: 0x000C4E7C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Thermometer.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.BulbOffset:
					this.m_bulbOffset = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.BulbSize:
					this.m_bulbSize = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ThermometerStyle:
					this.m_thermometerStyle = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000C6D15 File Offset: 0x000C4F15
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Thermometer;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000C6D1C File Offset: 0x000C4F1C
		internal double EvaluateBulbOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateThermometerBulbOffsetExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000C6D42 File Offset: 0x000C4F42
		internal double EvaluateBulbSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateThermometerBulbSizeExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000C6D68 File Offset: 0x000C4F68
		internal GaugeThermometerStyles EvaluateThermometerStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeThermometerStyles(context.ReportRuntime.EvaluateThermometerThermometerStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x0400176B RID: 5995
		[NonSerialized]
		private ThermometerExprHost m_exprHost;

		// Token: 0x0400176C RID: 5996
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Thermometer.GetDeclaration();

		// Token: 0x0400176D RID: 5997
		private ExpressionInfo m_bulbOffset;

		// Token: 0x0400176E RID: 5998
		private ExpressionInfo m_bulbSize;

		// Token: 0x0400176F RID: 5999
		private ExpressionInfo m_thermometerStyle;
	}
}
