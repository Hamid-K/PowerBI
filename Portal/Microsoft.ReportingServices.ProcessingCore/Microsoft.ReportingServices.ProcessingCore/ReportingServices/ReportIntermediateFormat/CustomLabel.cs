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
	// Token: 0x020003DE RID: 990
	[Serializable]
	internal sealed class CustomLabel : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002847 RID: 10311 RVA: 0x000BCDD2 File Offset: 0x000BAFD2
		internal CustomLabel()
		{
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000BCDDA File Offset: 0x000BAFDA
		internal CustomLabel(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x000BCDE3 File Offset: 0x000BAFE3
		// (set) Token: 0x0600284A RID: 10314 RVA: 0x000BCDEB File Offset: 0x000BAFEB
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000BCDF4 File Offset: 0x000BAFF4
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x000BCDFC File Offset: 0x000BAFFC
		internal ExpressionInfo Text
		{
			get
			{
				return this.m_text;
			}
			set
			{
				this.m_text = value;
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000BCE05 File Offset: 0x000BB005
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x000BCE0D File Offset: 0x000BB00D
		internal ExpressionInfo AllowUpsideDown
		{
			get
			{
				return this.m_allowUpsideDown;
			}
			set
			{
				this.m_allowUpsideDown = value;
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000BCE16 File Offset: 0x000BB016
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000BCE1E File Offset: 0x000BB01E
		internal ExpressionInfo DistanceFromScale
		{
			get
			{
				return this.m_distanceFromScale;
			}
			set
			{
				this.m_distanceFromScale = value;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000BCE27 File Offset: 0x000BB027
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x000BCE2F File Offset: 0x000BB02F
		internal ExpressionInfo FontAngle
		{
			get
			{
				return this.m_fontAngle;
			}
			set
			{
				this.m_fontAngle = value;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000BCE38 File Offset: 0x000BB038
		// (set) Token: 0x06002854 RID: 10324 RVA: 0x000BCE40 File Offset: 0x000BB040
		internal ExpressionInfo Placement
		{
			get
			{
				return this.m_placement;
			}
			set
			{
				this.m_placement = value;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000BCE49 File Offset: 0x000BB049
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x000BCE51 File Offset: 0x000BB051
		internal ExpressionInfo RotateLabel
		{
			get
			{
				return this.m_rotateLabel;
			}
			set
			{
				this.m_rotateLabel = value;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000BCE5A File Offset: 0x000BB05A
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x000BCE62 File Offset: 0x000BB062
		internal TickMarkStyle TickMarkStyle
		{
			get
			{
				return this.m_tickMarkStyle;
			}
			set
			{
				this.m_tickMarkStyle = value;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000BCE6B File Offset: 0x000BB06B
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x000BCE73 File Offset: 0x000BB073
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000BCE7C File Offset: 0x000BB07C
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000BCE84 File Offset: 0x000BB084
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000BCE8D File Offset: 0x000BB08D
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x000BCE95 File Offset: 0x000BB095
		internal ExpressionInfo UseFontPercent
		{
			get
			{
				return this.m_useFontPercent;
			}
			set
			{
				this.m_useFontPercent = value;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000BCE9E File Offset: 0x000BB09E
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000BCEAB File Offset: 0x000BB0AB
		internal CustomLabelExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000BCEB3 File Offset: 0x000BB0B3
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000BCEBC File Offset: 0x000BB0BC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.CustomLabelStart(this.m_name);
			base.Initialize(context);
			if (this.m_text != null)
			{
				this.m_text.Initialize("Text", context);
				context.ExprHostBuilder.CustomLabelText(this.m_text);
			}
			if (this.m_allowUpsideDown != null)
			{
				this.m_allowUpsideDown.Initialize("AllowUpsideDown", context);
				context.ExprHostBuilder.CustomLabelAllowUpsideDown(this.m_allowUpsideDown);
			}
			if (this.m_distanceFromScale != null)
			{
				this.m_distanceFromScale.Initialize("DistanceFromScale", context);
				context.ExprHostBuilder.CustomLabelDistanceFromScale(this.m_distanceFromScale);
			}
			if (this.m_fontAngle != null)
			{
				this.m_fontAngle.Initialize("FontAngle", context);
				context.ExprHostBuilder.CustomLabelFontAngle(this.m_fontAngle);
			}
			if (this.m_placement != null)
			{
				this.m_placement.Initialize("Placement", context);
				context.ExprHostBuilder.CustomLabelPlacement(this.m_placement);
			}
			if (this.m_rotateLabel != null)
			{
				this.m_rotateLabel.Initialize("RotateLabel", context);
				context.ExprHostBuilder.CustomLabelRotateLabel(this.m_rotateLabel);
			}
			if (this.m_tickMarkStyle != null)
			{
				this.m_tickMarkStyle.Initialize(context);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.CustomLabelValue(this.m_value);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.CustomLabelHidden(this.m_hidden);
			}
			if (this.m_useFontPercent != null)
			{
				this.m_useFontPercent.Initialize("UseFontPercent", context);
				context.ExprHostBuilder.CustomLabelUseFontPercent(this.m_useFontPercent);
			}
			this.m_exprHostID = context.ExprHostBuilder.CustomLabelEnd();
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000BD08C File Offset: 0x000BB28C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			CustomLabel customLabel = (CustomLabel)base.PublishClone(context);
			if (this.m_text != null)
			{
				customLabel.m_text = (ExpressionInfo)this.m_text.PublishClone(context);
			}
			if (this.m_allowUpsideDown != null)
			{
				customLabel.m_allowUpsideDown = (ExpressionInfo)this.m_allowUpsideDown.PublishClone(context);
			}
			if (this.m_distanceFromScale != null)
			{
				customLabel.m_distanceFromScale = (ExpressionInfo)this.m_distanceFromScale.PublishClone(context);
			}
			if (this.m_fontAngle != null)
			{
				customLabel.m_fontAngle = (ExpressionInfo)this.m_fontAngle.PublishClone(context);
			}
			if (this.m_placement != null)
			{
				customLabel.m_placement = (ExpressionInfo)this.m_placement.PublishClone(context);
			}
			if (this.m_rotateLabel != null)
			{
				customLabel.m_rotateLabel = (ExpressionInfo)this.m_rotateLabel.PublishClone(context);
			}
			if (this.m_tickMarkStyle != null)
			{
				customLabel.m_tickMarkStyle = (TickMarkStyle)this.m_tickMarkStyle.PublishClone(context);
			}
			if (this.m_value != null)
			{
				customLabel.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				customLabel.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_useFontPercent != null)
			{
				customLabel.m_useFontPercent = (ExpressionInfo)this.m_useFontPercent.PublishClone(context);
			}
			return customLabel;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000BD1E0 File Offset: 0x000BB3E0
		internal void SetExprHost(CustomLabelExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_tickMarkStyle != null && this.m_exprHost.TickMarkStyleHost != null)
			{
				this.m_tickMarkStyle.SetExprHost(this.m_exprHost.TickMarkStyleHost, reportObjectModel);
			}
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000BD23C File Offset: 0x000BB43C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Text, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AllowUpsideDown, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DistanceFromScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FontAngle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Placement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RotateLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TickMarkStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TickMarkStyle),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UseFontPercent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000BD358 File Offset: 0x000BB558
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(CustomLabel.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.Value)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						if (memberName == MemberName.Value)
						{
							writer.Write(this.m_value);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Text)
						{
							writer.Write(this.m_text);
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TickMarkStyle)
				{
					if (memberName == MemberName.AllowUpsideDown)
					{
						writer.Write(this.m_allowUpsideDown);
						continue;
					}
					if (memberName == MemberName.TickMarkStyle)
					{
						writer.Write(this.m_tickMarkStyle);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						writer.Write(this.m_distanceFromScale);
						continue;
					}
					switch (memberName)
					{
					case MemberName.Placement:
						writer.Write(this.m_placement);
						continue;
					case MemberName.RotateLabel:
						writer.Write(this.m_rotateLabel);
						continue;
					case MemberName.UseFontPercent:
						writer.Write(this.m_useFontPercent);
						continue;
					default:
						if (memberName == MemberName.FontAngle)
						{
							writer.Write(this.m_fontAngle);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000BD4F0 File Offset: 0x000BB6F0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(CustomLabel.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.Value)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.Value)
						{
							this.m_value = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Text)
						{
							this.m_text = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TickMarkStyle)
				{
					if (memberName == MemberName.AllowUpsideDown)
					{
						this.m_allowUpsideDown = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.TickMarkStyle)
					{
						this.m_tickMarkStyle = (TickMarkStyle)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						this.m_distanceFromScale = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Placement:
						this.m_placement = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.RotateLabel:
						this.m_rotateLabel = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.UseFontPercent:
						this.m_useFontPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.FontAngle)
						{
							this.m_fontAngle = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000BD6BD File Offset: 0x000BB8BD
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomLabel;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000BD6C4 File Offset: 0x000BB8C4
		internal string EvaluateText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateCustomLabelTextExpression(this, this.m_gaugePanel.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_gaugePanel.StyleClass, this.m_styleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, this.m_gaugePanel.Name);
			}
			return text;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000BD742 File Offset: 0x000BB942
		internal bool EvaluateAllowUpsideDown(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelAllowUpsideDownExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000BD768 File Offset: 0x000BB968
		internal double EvaluateDistanceFromScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelDistanceFromScaleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000BD78E File Offset: 0x000BB98E
		internal double EvaluateFontAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelFontAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000BD7B4 File Offset: 0x000BB9B4
		internal GaugeLabelPlacements EvaluatePlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeLabelPlacements(context.ReportRuntime.EvaluateCustomLabelPlacementExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000BD7E5 File Offset: 0x000BB9E5
		internal bool EvaluateRotateLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelRotateLabelExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000BD80B File Offset: 0x000BBA0B
		internal double EvaluateValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelValueExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000BD831 File Offset: 0x000BBA31
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000BD857 File Offset: 0x000BBA57
		internal bool EvaluateUseFontPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCustomLabelUseFontPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016B5 RID: 5813
		private int m_exprHostID;

		// Token: 0x040016B6 RID: 5814
		[NonSerialized]
		private CustomLabelExprHost m_exprHost;

		// Token: 0x040016B7 RID: 5815
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x040016B8 RID: 5816
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CustomLabel.GetDeclaration();

		// Token: 0x040016B9 RID: 5817
		private string m_name;

		// Token: 0x040016BA RID: 5818
		private ExpressionInfo m_text;

		// Token: 0x040016BB RID: 5819
		private ExpressionInfo m_allowUpsideDown;

		// Token: 0x040016BC RID: 5820
		private ExpressionInfo m_distanceFromScale;

		// Token: 0x040016BD RID: 5821
		private ExpressionInfo m_fontAngle;

		// Token: 0x040016BE RID: 5822
		private ExpressionInfo m_placement;

		// Token: 0x040016BF RID: 5823
		private ExpressionInfo m_rotateLabel;

		// Token: 0x040016C0 RID: 5824
		private TickMarkStyle m_tickMarkStyle;

		// Token: 0x040016C1 RID: 5825
		private ExpressionInfo m_value;

		// Token: 0x040016C2 RID: 5826
		private ExpressionInfo m_hidden;

		// Token: 0x040016C3 RID: 5827
		private ExpressionInfo m_useFontPercent;
	}
}
