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
	// Token: 0x020003FF RID: 1023
	[Serializable]
	internal sealed class IndicatorState : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002BE6 RID: 11238 RVA: 0x000CACC0 File Offset: 0x000C8EC0
		internal IndicatorState()
		{
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000CACCF File Offset: 0x000C8ECF
		internal IndicatorState(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000CACE5 File Offset: 0x000C8EE5
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000CACED File Offset: 0x000C8EED
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

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000CACF6 File Offset: 0x000C8EF6
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000CACFE File Offset: 0x000C8EFE
		internal GaugeInputValue StartValue
		{
			get
			{
				return this.m_startValue;
			}
			set
			{
				this.m_startValue = value;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000CAD07 File Offset: 0x000C8F07
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000CAD0F File Offset: 0x000C8F0F
		internal GaugeInputValue EndValue
		{
			get
			{
				return this.m_endValue;
			}
			set
			{
				this.m_endValue = value;
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000CAD18 File Offset: 0x000C8F18
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x000CAD20 File Offset: 0x000C8F20
		internal ExpressionInfo Color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000CAD29 File Offset: 0x000C8F29
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x000CAD31 File Offset: 0x000C8F31
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

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000CAD3A File Offset: 0x000C8F3A
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000CAD42 File Offset: 0x000C8F42
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

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000CAD4B File Offset: 0x000C8F4B
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x000CAD53 File Offset: 0x000C8F53
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

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000CAD5C File Offset: 0x000C8F5C
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000CAD69 File Offset: 0x000C8F69
		internal IndicatorStateExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000CAD71 File Offset: 0x000C8F71
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000CAD7C File Offset: 0x000C8F7C
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.IndicatorStateStart(this.m_name);
			if (this.m_color != null)
			{
				this.m_color.Initialize("Color", context);
				context.ExprHostBuilder.IndicatorStateColor(this.m_color);
			}
			if (this.m_scaleFactor != null)
			{
				this.m_scaleFactor.Initialize("ScaleFactor", context);
				context.ExprHostBuilder.IndicatorStateScaleFactor(this.m_scaleFactor);
			}
			if (this.m_indicatorStyle != null)
			{
				this.m_indicatorStyle.Initialize("IndicatorStyle", context);
				context.ExprHostBuilder.IndicatorStateIndicatorStyle(this.m_indicatorStyle);
			}
			if (this.m_indicatorImage != null)
			{
				this.m_indicatorImage.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.IndicatorStateEnd();
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000CAE44 File Offset: 0x000C9044
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			IndicatorState indicatorState = (IndicatorState)base.MemberwiseClone();
			indicatorState.m_gaugePanel = (GaugePanel)context.CurrentDataRegionClone;
			if (this.m_startValue != null)
			{
				indicatorState.m_startValue = (GaugeInputValue)this.m_startValue.PublishClone(context);
			}
			if (this.m_endValue != null)
			{
				indicatorState.m_endValue = (GaugeInputValue)this.m_endValue.PublishClone(context);
			}
			if (this.m_color != null)
			{
				indicatorState.m_color = (ExpressionInfo)this.m_color.PublishClone(context);
			}
			if (this.m_scaleFactor != null)
			{
				indicatorState.m_scaleFactor = (ExpressionInfo)this.m_scaleFactor.PublishClone(context);
			}
			if (this.m_indicatorStyle != null)
			{
				indicatorState.m_indicatorStyle = (ExpressionInfo)this.m_indicatorStyle.PublishClone(context);
			}
			if (this.m_indicatorImage != null)
			{
				indicatorState.m_indicatorImage = (IndicatorImage)this.m_indicatorImage.PublishClone(context);
			}
			return indicatorState;
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000CAF2C File Offset: 0x000C912C
		internal void SetExprHost(IndicatorStateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_startValue != null && this.ExprHost.StartValueHost != null)
			{
				this.m_startValue.SetExprHost(this.ExprHost.StartValueHost, reportObjectModel);
			}
			if (this.m_endValue != null && this.ExprHost.EndValueHost != null)
			{
				this.m_endValue.SetExprHost(this.ExprHost.EndValueHost, reportObjectModel);
			}
			if (this.m_indicatorImage != null && this.ExprHost.IndicatorImageHost != null)
			{
				this.m_indicatorImage.SetExprHost(this.ExprHost.IndicatorImageHost, reportObjectModel);
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000CAFEC File Offset: 0x000C91EC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorState, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.StartValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.EndValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.Color, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ScaleFactor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IndicatorStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IndicatorImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorImage),
				new MemberInfo(MemberName.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000CB0C8 File Offset: 0x000C92C8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(IndicatorState.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.GaugePanel)
				{
					if (memberName <= MemberName.Color)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						if (memberName == MemberName.Color)
						{
							writer.Write(this.m_color);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						if (memberName == MemberName.GaugePanel)
						{
							writer.WriteReference(this.m_gaugePanel);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.EndValue)
				{
					if (memberName == MemberName.StartValue)
					{
						writer.Write(this.m_startValue);
						continue;
					}
					if (memberName == MemberName.EndValue)
					{
						writer.Write(this.m_endValue);
						continue;
					}
				}
				else
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
					if (memberName == MemberName.IndicatorImage)
					{
						writer.Write(this.m_indicatorImage);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000CB20C File Offset: 0x000C940C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(IndicatorState.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.GaugePanel)
				{
					if (memberName <= MemberName.Color)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.Color)
						{
							this.m_color = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.GaugePanel)
						{
							this.m_gaugePanel = reader.ReadReference<GaugePanel>(this);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.EndValue)
				{
					if (memberName == MemberName.StartValue)
					{
						this.m_startValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.EndValue)
					{
						this.m_endValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ScaleFactor)
					{
						this.m_scaleFactor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.IndicatorStyle)
					{
						this.m_indicatorStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.IndicatorImage)
					{
						this.m_indicatorImage = (IndicatorImage)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000CB378 File Offset: 0x000C9578
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(IndicatorState.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.GaugePanel)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_gaugePanel = (GaugePanel)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000CB41C File Offset: 0x000C961C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorState;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000CB423 File Offset: 0x000C9623
		internal string EvaluateColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateIndicatorStateColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000CB449 File Offset: 0x000C9649
		internal double EvaluateScaleFactor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateIndicatorStateScaleFactorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000CB46F File Offset: 0x000C966F
		internal GaugeStateIndicatorStyles EvaluateIndicatorStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeStateIndicatorStyles(context.ReportRuntime.EvaluateIndicatorStateIndicatorStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x040017C3 RID: 6083
		private int m_exprHostID = -1;

		// Token: 0x040017C4 RID: 6084
		[NonSerialized]
		private IndicatorStateExprHost m_exprHost;

		// Token: 0x040017C5 RID: 6085
		[Reference]
		private GaugePanel m_gaugePanel;

		// Token: 0x040017C6 RID: 6086
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = IndicatorState.GetDeclaration();

		// Token: 0x040017C7 RID: 6087
		private string m_name;

		// Token: 0x040017C8 RID: 6088
		private GaugeInputValue m_startValue;

		// Token: 0x040017C9 RID: 6089
		private GaugeInputValue m_endValue;

		// Token: 0x040017CA RID: 6090
		private ExpressionInfo m_color;

		// Token: 0x040017CB RID: 6091
		private ExpressionInfo m_scaleFactor;

		// Token: 0x040017CC RID: 6092
		private ExpressionInfo m_indicatorStyle;

		// Token: 0x040017CD RID: 6093
		private IndicatorImage m_indicatorImage;
	}
}
