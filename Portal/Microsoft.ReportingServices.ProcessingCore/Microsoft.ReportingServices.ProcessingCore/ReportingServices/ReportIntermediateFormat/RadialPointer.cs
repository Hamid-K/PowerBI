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
	// Token: 0x020003F0 RID: 1008
	[Serializable]
	internal sealed class RadialPointer : GaugePointer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060029EA RID: 10730 RVA: 0x000C367D File Offset: 0x000C187D
		internal RadialPointer()
		{
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000C3685 File Offset: 0x000C1885
		internal RadialPointer(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x000C368F File Offset: 0x000C188F
		// (set) Token: 0x060029ED RID: 10733 RVA: 0x000C3697 File Offset: 0x000C1897
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

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000C36A0 File Offset: 0x000C18A0
		// (set) Token: 0x060029EF RID: 10735 RVA: 0x000C36A8 File Offset: 0x000C18A8
		internal PointerCap PointerCap
		{
			get
			{
				return this.m_pointerCap;
			}
			set
			{
				this.m_pointerCap = value;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000C36B1 File Offset: 0x000C18B1
		// (set) Token: 0x060029F1 RID: 10737 RVA: 0x000C36B9 File Offset: 0x000C18B9
		internal ExpressionInfo NeedleStyle
		{
			get
			{
				return this.m_needleStyle;
			}
			set
			{
				this.m_needleStyle = value;
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000C36C4 File Offset: 0x000C18C4
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.RadialPointerStart(this.m_name);
			base.Initialize(context);
			if (this.m_type != null)
			{
				this.m_type.Initialize("Type", context);
				context.ExprHostBuilder.RadialPointerType(this.m_type);
			}
			if (this.m_pointerCap != null)
			{
				this.m_pointerCap.Initialize(context);
			}
			if (this.m_needleStyle != null)
			{
				this.m_needleStyle.Initialize("NeedleStyle", context);
				context.ExprHostBuilder.RadialPointerNeedleStyle(this.m_needleStyle);
			}
			this.m_exprHostID = context.ExprHostBuilder.RadialPointerEnd();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000C3768 File Offset: 0x000C1968
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			RadialPointer radialPointer = (RadialPointer)base.PublishClone(context);
			if (this.m_type != null)
			{
				radialPointer.m_type = (ExpressionInfo)this.m_type.PublishClone(context);
			}
			if (this.m_pointerCap != null)
			{
				radialPointer.m_pointerCap = (PointerCap)this.m_pointerCap.PublishClone(context);
			}
			if (this.m_needleStyle != null)
			{
				radialPointer.m_needleStyle = (ExpressionInfo)this.m_needleStyle.PublishClone(context);
			}
			return radialPointer;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000C37E0 File Offset: 0x000C19E0
		internal void SetExprHost(RadialPointerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_pointerCap != null && ((RadialPointerExprHost)this.m_exprHost).PointerCapHost != null)
			{
				this.m_pointerCap.SetExprHost(((RadialPointerExprHost)this.m_exprHost).PointerCapHost, reportObjectModel);
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000C3848 File Offset: 0x000C1A48
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialPointer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePointer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Type, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PointerCap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PointerCap),
				new MemberInfo(MemberName.NeedleStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000C38AC File Offset: 0x000C1AAC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RadialPointer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Type)
				{
					if (memberName != MemberName.PointerCap)
					{
						if (memberName != MemberName.NeedleStyle)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_needleStyle);
						}
					}
					else
					{
						writer.Write(this.m_pointerCap);
					}
				}
				else
				{
					writer.Write(this.m_type);
				}
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000C3938 File Offset: 0x000C1B38
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RadialPointer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Type)
				{
					if (memberName != MemberName.PointerCap)
					{
						if (memberName != MemberName.NeedleStyle)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_needleStyle = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_pointerCap = (PointerCap)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_type = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000C39D1 File Offset: 0x000C1BD1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialPointer;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000C39D8 File Offset: 0x000C1BD8
		internal RadialPointerTypes EvaluateType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateRadialPointerTypes(context.ReportRuntime.EvaluateRadialPointerTypeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000C3A09 File Offset: 0x000C1C09
		internal RadialPointerNeedleStyles EvaluateNeedleStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateRadialPointerNeedleStyles(context.ReportRuntime.EvaluateRadialPointerNeedleStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x0400172C RID: 5932
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RadialPointer.GetDeclaration();

		// Token: 0x0400172D RID: 5933
		private ExpressionInfo m_type;

		// Token: 0x0400172E RID: 5934
		private PointerCap m_pointerCap;

		// Token: 0x0400172F RID: 5935
		private ExpressionInfo m_needleStyle;
	}
}
