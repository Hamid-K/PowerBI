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
	// Token: 0x020003EF RID: 1007
	[Serializable]
	internal sealed class LinearPointer : GaugePointer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060029DB RID: 10715 RVA: 0x000C3386 File Offset: 0x000C1586
		internal LinearPointer()
		{
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000C338E File Offset: 0x000C158E
		internal LinearPointer(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000C3398 File Offset: 0x000C1598
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x000C33A0 File Offset: 0x000C15A0
		internal ExpressionInfo Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000C33A9 File Offset: 0x000C15A9
		// (set) Token: 0x060029E0 RID: 10720 RVA: 0x000C33B1 File Offset: 0x000C15B1
		internal Thermometer Thermometer
		{
			get
			{
				return this.m_thermometer;
			}
			set
			{
				this.m_thermometer = value;
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000C33BC File Offset: 0x000C15BC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.LinearPointerStart(this.m_name);
			base.Initialize(context);
			if (this.m_type != null)
			{
				this.m_type.Initialize("Type", context);
				context.ExprHostBuilder.LinearPointerType(this.m_type);
			}
			if (this.m_thermometer != null)
			{
				this.m_thermometer.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.LinearPointerEnd();
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000C3434 File Offset: 0x000C1634
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			LinearPointer linearPointer = (LinearPointer)base.PublishClone(context);
			if (this.m_type != null)
			{
				linearPointer.m_type = (ExpressionInfo)this.m_type.PublishClone(context);
			}
			if (this.m_thermometer != null)
			{
				linearPointer.m_thermometer = (Thermometer)this.m_thermometer.PublishClone(context);
			}
			return linearPointer;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000C3490 File Offset: 0x000C1690
		internal void SetExprHost(LinearPointerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_thermometer != null && ((LinearPointerExprHost)this.m_exprHost).ThermometerHost != null)
			{
				this.m_thermometer.SetExprHost(((LinearPointerExprHost)this.m_exprHost).ThermometerHost, reportObjectModel);
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000C34F8 File Offset: 0x000C16F8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearPointer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePointer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Type, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Thermometer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Thermometer)
			});
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000C3548 File Offset: 0x000C1748
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(LinearPointer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Type)
				{
					if (memberName != MemberName.Thermometer)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_thermometer);
					}
				}
				else
				{
					writer.Write(this.m_type);
				}
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000C35BC File Offset: 0x000C17BC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(LinearPointer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Type)
				{
					if (memberName != MemberName.Thermometer)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_thermometer = (Thermometer)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_type = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000C3639 File Offset: 0x000C1839
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearPointer;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000C3640 File Offset: 0x000C1840
		internal LinearPointerTypes EvaluateType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateLinearPointerTypes(context.ReportRuntime.EvaluateLinearPointerTypeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x04001729 RID: 5929
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LinearPointer.GetDeclaration();

		// Token: 0x0400172A RID: 5930
		private ExpressionInfo m_type;

		// Token: 0x0400172B RID: 5931
		private Thermometer m_thermometer;
	}
}
