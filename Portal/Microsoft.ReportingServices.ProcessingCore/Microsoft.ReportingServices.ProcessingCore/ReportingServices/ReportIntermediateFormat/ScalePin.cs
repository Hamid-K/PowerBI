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
	// Token: 0x020003FA RID: 1018
	[Serializable]
	internal sealed class ScalePin : TickMarkStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002B0F RID: 11023 RVA: 0x000C796C File Offset: 0x000C5B6C
		internal ScalePin()
		{
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000C7974 File Offset: 0x000C5B74
		internal ScalePin(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000C797D File Offset: 0x000C5B7D
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x000C7985 File Offset: 0x000C5B85
		internal ExpressionInfo Location
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000C798E File Offset: 0x000C5B8E
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000C7996 File Offset: 0x000C5B96
		internal ExpressionInfo Enable
		{
			get
			{
				return this.m_enable;
			}
			set
			{
				this.m_enable = value;
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000C799F File Offset: 0x000C5B9F
		// (set) Token: 0x06002B16 RID: 11030 RVA: 0x000C79A7 File Offset: 0x000C5BA7
		internal PinLabel PinLabel
		{
			get
			{
				return this.m_pinLabel;
			}
			set
			{
				this.m_pinLabel = value;
			}
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000C79B0 File Offset: 0x000C5BB0
		internal void Initialize(InitializationContext context, bool isMaximum)
		{
			context.ExprHostBuilder.ScalePinStart(isMaximum);
			base.InitializeInternal(context);
			if (this.m_location != null)
			{
				this.m_location.Initialize("Location", context);
				context.ExprHostBuilder.ScalePinLocation(this.m_location);
			}
			if (this.m_enable != null)
			{
				this.m_enable.Initialize("Enable", context);
				context.ExprHostBuilder.ScalePinEnable(this.m_enable);
			}
			if (this.m_pinLabel != null)
			{
				this.m_pinLabel.Initialize(context);
			}
			context.ExprHostBuilder.ScalePinEnd(isMaximum);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000C7A48 File Offset: 0x000C5C48
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ScalePin scalePin = (ScalePin)base.PublishClone(context);
			if (this.m_location != null)
			{
				scalePin.m_location = (ExpressionInfo)this.m_location.PublishClone(context);
			}
			if (this.m_enable != null)
			{
				scalePin.m_enable = (ExpressionInfo)this.m_enable.PublishClone(context);
			}
			if (this.m_pinLabel != null)
			{
				scalePin.m_pinLabel = (PinLabel)this.m_pinLabel.PublishClone(context);
			}
			return scalePin;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000C7AC0 File Offset: 0x000C5CC0
		internal void SetExprHost(ScalePinExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_pinLabel != null && ((ScalePinExprHost)this.m_exprHost).PinLabelHost != null)
			{
				this.m_pinLabel.SetExprHost(((ScalePinExprHost)this.m_exprHost).PinLabelHost, reportObjectModel);
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000C7B28 File Offset: 0x000C5D28
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalePin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TickMarkStyle, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Location, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Enable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PinLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PinLabel)
			});
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000C7B8C File Offset: 0x000C5D8C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ScalePin.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Location)
				{
					if (memberName != MemberName.Enable)
					{
						if (memberName != MemberName.PinLabel)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_pinLabel);
						}
					}
					else
					{
						writer.Write(this.m_enable);
					}
				}
				else
				{
					writer.Write(this.m_location);
				}
			}
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000C7C18 File Offset: 0x000C5E18
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ScalePin.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Location)
				{
					if (memberName != MemberName.Enable)
					{
						if (memberName != MemberName.PinLabel)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_pinLabel = (PinLabel)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_enable = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_location = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000C7CB1 File Offset: 0x000C5EB1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalePin;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000C7CB8 File Offset: 0x000C5EB8
		internal double EvaluateLocation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScalePinLocationExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000C7CDE File Offset: 0x000C5EDE
		internal bool EvaluateEnable(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScalePinEnableExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0400177E RID: 6014
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ScalePin.GetDeclaration();

		// Token: 0x0400177F RID: 6015
		private ExpressionInfo m_location;

		// Token: 0x04001780 RID: 6016
		private ExpressionInfo m_enable;

		// Token: 0x04001781 RID: 6017
		private PinLabel m_pinLabel;
	}
}
