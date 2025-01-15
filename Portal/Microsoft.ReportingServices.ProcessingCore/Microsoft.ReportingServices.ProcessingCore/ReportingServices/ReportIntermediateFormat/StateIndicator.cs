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
	// Token: 0x020003FE RID: 1022
	[Serializable]
	internal sealed class StateIndicator : GaugePanelItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002BBC RID: 11196 RVA: 0x000CA148 File Offset: 0x000C8348
		internal StateIndicator()
		{
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000CA150 File Offset: 0x000C8350
		internal StateIndicator(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000CA15A File Offset: 0x000C835A
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x000CA162 File Offset: 0x000C8362
		internal GaugeInputValue GaugeInputValue
		{
			get
			{
				return this.m_gaugeInputValue;
			}
			set
			{
				this.m_gaugeInputValue = value;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000CA16B File Offset: 0x000C836B
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x000CA173 File Offset: 0x000C8373
		internal ExpressionInfo TransformationType
		{
			get
			{
				return this.m_transformationType;
			}
			set
			{
				this.m_transformationType = value;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000CA17C File Offset: 0x000C837C
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x000CA184 File Offset: 0x000C8384
		internal string TransformationScope
		{
			get
			{
				return this.m_transformationScope;
			}
			set
			{
				this.m_transformationScope = value;
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000CA18D File Offset: 0x000C838D
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000CA195 File Offset: 0x000C8395
		internal GaugeInputValue MaximumValue
		{
			get
			{
				return this.m_maximumValue;
			}
			set
			{
				this.m_maximumValue = value;
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000CA19E File Offset: 0x000C839E
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x000CA1A6 File Offset: 0x000C83A6
		internal GaugeInputValue MinimumValue
		{
			get
			{
				return this.m_minimumValue;
			}
			set
			{
				this.m_minimumValue = value;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000CA1AF File Offset: 0x000C83AF
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x000CA1B7 File Offset: 0x000C83B7
		internal ExpressionInfo IndicatorStyle
		{
			get
			{
				return this.m_indicatorStyle;
			}
			set
			{
				this.m_indicatorStyle = value;
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000CA1C0 File Offset: 0x000C83C0
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x000CA1C8 File Offset: 0x000C83C8
		internal IndicatorImage IndicatorImage
		{
			get
			{
				return this.m_indicatorImage;
			}
			set
			{
				this.m_indicatorImage = value;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000CA1D1 File Offset: 0x000C83D1
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x000CA1D9 File Offset: 0x000C83D9
		internal ExpressionInfo ScaleFactor
		{
			get
			{
				return this.m_scaleFactor;
			}
			set
			{
				this.m_scaleFactor = value;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000CA1E2 File Offset: 0x000C83E2
		// (set) Token: 0x06002BCF RID: 11215 RVA: 0x000CA1EA File Offset: 0x000C83EA
		internal List<IndicatorState> IndicatorStates
		{
			get
			{
				return this.m_indicatorStates;
			}
			set
			{
				this.m_indicatorStates = value;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000CA1F3 File Offset: 0x000C83F3
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x000CA1FB File Offset: 0x000C83FB
		internal ExpressionInfo ResizeMode
		{
			get
			{
				return this.m_resizeMode;
			}
			set
			{
				this.m_resizeMode = value;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000CA204 File Offset: 0x000C8404
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x000CA20C File Offset: 0x000C840C
		internal ExpressionInfo Angle
		{
			get
			{
				return this.m_angle;
			}
			set
			{
				this.m_angle = value;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000CA215 File Offset: 0x000C8415
		// (set) Token: 0x06002BD5 RID: 11221 RVA: 0x000CA21D File Offset: 0x000C841D
		internal string StateDataElementName
		{
			get
			{
				return this.m_stateDataElementName;
			}
			set
			{
				this.m_stateDataElementName = value;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000CA226 File Offset: 0x000C8426
		// (set) Token: 0x06002BD7 RID: 11223 RVA: 0x000CA22E File Offset: 0x000C842E
		internal DataElementOutputTypes StateDataElementOutput
		{
			get
			{
				return this.m_stateDataElementOutput;
			}
			set
			{
				this.m_stateDataElementOutput = value;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000CA237 File Offset: 0x000C8437
		internal new StateIndicatorExprHost ExprHost
		{
			get
			{
				return (StateIndicatorExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000CA244 File Offset: 0x000C8444
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.StateIndicatorStart(this.m_name);
			base.Initialize(context);
			if (this.m_transformationType != null)
			{
				this.m_transformationType.Initialize("TransformationType", context);
				context.ExprHostBuilder.StateIndicatorTransformationType(this.m_transformationType);
			}
			if (this.m_indicatorStyle != null)
			{
				this.m_indicatorStyle.Initialize("IndicatorStyle", context);
				context.ExprHostBuilder.StateIndicatorIndicatorStyle(this.m_indicatorStyle);
			}
			if (this.m_indicatorImage != null)
			{
				this.m_indicatorImage.Initialize(context);
			}
			if (this.m_scaleFactor != null)
			{
				this.m_scaleFactor.Initialize("ScaleFactor", context);
				context.ExprHostBuilder.StateIndicatorScaleFactor(this.m_scaleFactor);
			}
			if (this.m_indicatorStates != null)
			{
				for (int i = 0; i < this.m_indicatorStates.Count; i++)
				{
					this.m_indicatorStates[i].Initialize(context);
				}
			}
			if (this.m_resizeMode != null)
			{
				this.m_resizeMode.Initialize("ResizeMode", context);
				context.ExprHostBuilder.StateIndicatorResizeMode(this.m_resizeMode);
			}
			if (this.m_angle != null)
			{
				this.m_angle.Initialize("Angle", context);
				context.ExprHostBuilder.StateIndicatorAngle(this.m_angle);
			}
			this.m_exprHostID = context.ExprHostBuilder.StateIndicatorEnd();
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000CA398 File Offset: 0x000C8598
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			StateIndicator stateIndicator = (StateIndicator)base.PublishClone(context);
			if (this.m_gaugeInputValue != null)
			{
				stateIndicator.m_gaugeInputValue = (GaugeInputValue)this.m_gaugeInputValue.PublishClone(context);
			}
			if (this.m_transformationType != null)
			{
				stateIndicator.m_transformationType = (ExpressionInfo)this.m_transformationType.PublishClone(context);
			}
			if (this.m_maximumValue != null)
			{
				stateIndicator.m_maximumValue = (GaugeInputValue)this.m_maximumValue.PublishClone(context);
			}
			if (this.m_minimumValue != null)
			{
				stateIndicator.m_minimumValue = (GaugeInputValue)this.m_minimumValue.PublishClone(context);
			}
			if (this.m_indicatorStyle != null)
			{
				stateIndicator.m_indicatorStyle = (ExpressionInfo)this.m_indicatorStyle.PublishClone(context);
			}
			if (this.m_indicatorImage != null)
			{
				stateIndicator.m_indicatorImage = (IndicatorImage)this.m_indicatorImage.PublishClone(context);
			}
			if (this.m_scaleFactor != null)
			{
				stateIndicator.m_scaleFactor = (ExpressionInfo)this.m_scaleFactor.PublishClone(context);
			}
			if (this.m_indicatorStates != null)
			{
				stateIndicator.m_indicatorStates = new List<IndicatorState>(this.m_indicatorStates.Count);
				foreach (IndicatorState indicatorState in this.m_indicatorStates)
				{
					stateIndicator.m_indicatorStates.Add((IndicatorState)indicatorState.PublishClone(context));
				}
			}
			if (this.m_resizeMode != null)
			{
				stateIndicator.m_resizeMode = (ExpressionInfo)this.m_resizeMode.PublishClone(context);
			}
			if (this.m_angle != null)
			{
				stateIndicator.m_angle = (ExpressionInfo)this.m_angle.PublishClone(context);
			}
			return stateIndicator;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000CA540 File Offset: 0x000C8740
		internal void SetExprHost(StateIndicatorExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_gaugeInputValue != null && this.ExprHost.GaugeInputValueHost != null)
			{
				this.m_gaugeInputValue.SetExprHost(this.ExprHost.GaugeInputValueHost, reportObjectModel);
			}
			if (this.m_maximumValue != null && this.ExprHost.MaximumValueHost != null)
			{
				this.m_maximumValue.SetExprHost(this.ExprHost.MaximumValueHost, reportObjectModel);
			}
			if (this.m_minimumValue != null && this.ExprHost.MinimumValueHost != null)
			{
				this.m_minimumValue.SetExprHost(this.ExprHost.MinimumValueHost, reportObjectModel);
			}
			if (this.m_indicatorImage != null && this.ExprHost.IndicatorImageHost != null)
			{
				this.m_indicatorImage.SetExprHost(this.ExprHost.IndicatorImageHost, reportObjectModel);
			}
			IList<IndicatorStateExprHost> indicatorStatesHostsRemotable = this.ExprHost.IndicatorStatesHostsRemotable;
			if (this.m_indicatorStates != null && indicatorStatesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_indicatorStates.Count; i++)
				{
					IndicatorState indicatorState = this.m_indicatorStates[i];
					if (indicatorState != null && indicatorState.ExpressionHostID > -1)
					{
						indicatorState.SetExprHost(indicatorStatesHostsRemotable[indicatorState.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000CA678 File Offset: 0x000C8878
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StateIndicator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.GaugeInputValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.IndicatorStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IndicatorImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorImage),
				new MemberInfo(MemberName.ScaleFactor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IndicatorStates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorState),
				new MemberInfo(MemberName.ResizeMode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Angle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TransformationType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TransformationScope, Token.String),
				new MemberInfo(MemberName.MaximumValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.MinimumValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.StateDataElementName, Token.String),
				new MemberInfo(MemberName.StateDataElementOutput, Token.Enum)
			});
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000CA7AC File Offset: 0x000C89AC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(StateIndicator.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ResizeMode)
				{
					if (memberName <= MemberName.GaugeInputValue)
					{
						if (memberName == MemberName.Angle)
						{
							writer.Write(this.m_angle);
							continue;
						}
						if (memberName == MemberName.GaugeInputValue)
						{
							writer.Write(this.m_gaugeInputValue);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.MaximumValue)
						{
							writer.Write(this.m_maximumValue);
							continue;
						}
						if (memberName == MemberName.MinimumValue)
						{
							writer.Write(this.m_minimumValue);
							continue;
						}
						if (memberName == MemberName.ResizeMode)
						{
							writer.Write(this.m_resizeMode);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.IndicatorStyle)
				{
					if (memberName == MemberName.ScaleFactor)
					{
						writer.Write(this.m_scaleFactor);
						continue;
					}
					if (memberName == MemberName.IndicatorStyle)
					{
						writer.Write(this.m_indicatorStyle);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.IndicatorImage)
					{
						writer.Write(this.m_indicatorImage);
						continue;
					}
					if (memberName == MemberName.IndicatorStates)
					{
						writer.Write<IndicatorState>(this.m_indicatorStates);
						continue;
					}
					switch (memberName)
					{
					case MemberName.TransformationType:
						writer.Write(this.m_transformationType);
						continue;
					case MemberName.TransformationScope:
						writer.Write(this.m_transformationScope);
						continue;
					case MemberName.StateDataElementName:
						writer.Write(this.m_stateDataElementName);
						continue;
					case MemberName.StateDataElementOutput:
						writer.WriteEnum((int)this.m_stateDataElementOutput);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000CA974 File Offset: 0x000C8B74
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(StateIndicator.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ResizeMode)
				{
					if (memberName <= MemberName.GaugeInputValue)
					{
						if (memberName != MemberName.Angle)
						{
							if (memberName != MemberName.GaugeInputValue)
							{
								goto IL_01D2;
							}
							this.m_gaugeInputValue = (GaugeInputValue)reader.ReadRIFObject();
						}
						else
						{
							this.m_angle = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else if (memberName != MemberName.MaximumValue)
					{
						if (memberName != MemberName.MinimumValue)
						{
							if (memberName != MemberName.ResizeMode)
							{
								goto IL_01D2;
							}
							this.m_resizeMode = (ExpressionInfo)reader.ReadRIFObject();
						}
						else
						{
							this.m_minimumValue = (GaugeInputValue)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_maximumValue = (GaugeInputValue)reader.ReadRIFObject();
					}
				}
				else if (memberName <= MemberName.IndicatorStyle)
				{
					if (memberName != MemberName.ScaleFactor)
					{
						if (memberName != MemberName.IndicatorStyle)
						{
							goto IL_01D2;
						}
						this.m_indicatorStyle = (ExpressionInfo)reader.ReadRIFObject();
					}
					else
					{
						this.m_scaleFactor = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else if (memberName != MemberName.IndicatorImage)
				{
					if (memberName != MemberName.IndicatorStates)
					{
						switch (memberName)
						{
						case MemberName.TransformationType:
							this.m_transformationType = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.TransformationScope:
							this.m_transformationScope = reader.ReadString();
							break;
						case MemberName.StateDataElementName:
							this.m_stateDataElementName = reader.ReadString();
							break;
						case MemberName.StateDataElementOutput:
							this.m_stateDataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
							break;
						default:
							goto IL_01D2;
						}
					}
					else
					{
						this.m_indicatorStates = reader.ReadGenericListOfRIFObjects<IndicatorState>();
					}
				}
				else
				{
					this.m_indicatorImage = (IndicatorImage)reader.ReadRIFObject();
				}
				IL_01DD:
				if (reader.IntermediateFormatVersion.CompareTo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatVersion.SQL16) >= 0)
				{
					continue;
				}
				AttributeInfo attributeInfo;
				if (!this.m_styleClass.GetAttributeInfo("BorderStyle", out attributeInfo))
				{
					this.m_styleClass.AddAttribute("BorderStyle", new ExpressionInfo
					{
						StringValue = "Solid",
						ConstantType = DataType.String
					});
					continue;
				}
				attributeInfo.IsExpression = false;
				attributeInfo.Value = "Solid";
				continue;
				IL_01D2:
				Global.Tracer.Assert(false);
				goto IL_01DD;
			}
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000CABCE File Offset: 0x000C8DCE
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StateIndicator;
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000CABD5 File Offset: 0x000C8DD5
		internal GaugeTransformationType EvaluateTransformationType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeTransformationType(context.ReportRuntime.EvaluateStateIndicatorTransformationTypeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000CAC06 File Offset: 0x000C8E06
		internal GaugeStateIndicatorStyles EvaluateIndicatorStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeStateIndicatorStyles(context.ReportRuntime.EvaluateStateIndicatorIndicatorStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000CAC37 File Offset: 0x000C8E37
		internal double EvaluateScaleFactor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateStateIndicatorScaleFactorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000CAC5D File Offset: 0x000C8E5D
		internal GaugeResizeModes EvaluateResizeMode(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeResizeModes(context.ReportRuntime.EvaluateStateIndicatorResizeModeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000CAC8E File Offset: 0x000C8E8E
		internal double EvaluateAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateStateIndicatorAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040017B5 RID: 6069
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = StateIndicator.GetDeclaration();

		// Token: 0x040017B6 RID: 6070
		private GaugeInputValue m_gaugeInputValue;

		// Token: 0x040017B7 RID: 6071
		private ExpressionInfo m_transformationType;

		// Token: 0x040017B8 RID: 6072
		private string m_transformationScope;

		// Token: 0x040017B9 RID: 6073
		private GaugeInputValue m_maximumValue;

		// Token: 0x040017BA RID: 6074
		private GaugeInputValue m_minimumValue;

		// Token: 0x040017BB RID: 6075
		private ExpressionInfo m_indicatorStyle;

		// Token: 0x040017BC RID: 6076
		private IndicatorImage m_indicatorImage;

		// Token: 0x040017BD RID: 6077
		private ExpressionInfo m_scaleFactor;

		// Token: 0x040017BE RID: 6078
		private List<IndicatorState> m_indicatorStates;

		// Token: 0x040017BF RID: 6079
		private ExpressionInfo m_resizeMode;

		// Token: 0x040017C0 RID: 6080
		private ExpressionInfo m_angle;

		// Token: 0x040017C1 RID: 6081
		private string m_stateDataElementName;

		// Token: 0x040017C2 RID: 6082
		private DataElementOutputTypes m_stateDataElementOutput;
	}
}
