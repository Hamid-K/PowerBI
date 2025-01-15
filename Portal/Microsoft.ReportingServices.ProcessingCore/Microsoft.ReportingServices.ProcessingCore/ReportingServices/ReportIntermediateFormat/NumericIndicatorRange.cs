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
	// Token: 0x020003FC RID: 1020
	[Serializable]
	internal sealed class NumericIndicatorRange : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002B6A RID: 11114 RVA: 0x000C8E8A File Offset: 0x000C708A
		internal NumericIndicatorRange()
		{
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000C8E99 File Offset: 0x000C7099
		internal NumericIndicatorRange(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000C8EAF File Offset: 0x000C70AF
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000C8EB7 File Offset: 0x000C70B7
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

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000C8EC0 File Offset: 0x000C70C0
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x000C8EC8 File Offset: 0x000C70C8
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

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000C8ED1 File Offset: 0x000C70D1
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x000C8ED9 File Offset: 0x000C70D9
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

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000C8EE2 File Offset: 0x000C70E2
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x000C8EEA File Offset: 0x000C70EA
		internal ExpressionInfo DecimalDigitColor
		{
			get
			{
				return this.m_decimalDigitColor;
			}
			set
			{
				this.m_decimalDigitColor = value;
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000C8EF3 File Offset: 0x000C70F3
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000C8EFB File Offset: 0x000C70FB
		internal ExpressionInfo DigitColor
		{
			get
			{
				return this.m_digitColor;
			}
			set
			{
				this.m_digitColor = value;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000C8F04 File Offset: 0x000C7104
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000C8F11 File Offset: 0x000C7111
		internal NumericIndicatorRangeExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000C8F19 File Offset: 0x000C7119
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000C8F24 File Offset: 0x000C7124
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.NumericIndicatorRangeStart(this.m_name);
			if (this.m_decimalDigitColor != null)
			{
				this.m_decimalDigitColor.Initialize("DecimalDigitColor", context);
				context.ExprHostBuilder.NumericIndicatorRangeDecimalDigitColor(this.m_decimalDigitColor);
			}
			if (this.m_digitColor != null)
			{
				this.m_digitColor.Initialize("DigitColor", context);
				context.ExprHostBuilder.NumericIndicatorRangeDigitColor(this.m_digitColor);
			}
			this.m_exprHostID = context.ExprHostBuilder.NumericIndicatorRangeEnd();
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000C8FAC File Offset: 0x000C71AC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			NumericIndicatorRange numericIndicatorRange = (NumericIndicatorRange)base.MemberwiseClone();
			numericIndicatorRange.m_gaugePanel = (GaugePanel)context.CurrentDataRegionClone;
			if (this.m_startValue != null)
			{
				numericIndicatorRange.m_startValue = (GaugeInputValue)this.m_startValue.PublishClone(context);
			}
			if (this.m_endValue != null)
			{
				numericIndicatorRange.m_endValue = (GaugeInputValue)this.m_endValue.PublishClone(context);
			}
			if (this.m_decimalDigitColor != null)
			{
				numericIndicatorRange.m_decimalDigitColor = (ExpressionInfo)this.m_decimalDigitColor.PublishClone(context);
			}
			if (this.m_digitColor != null)
			{
				numericIndicatorRange.m_digitColor = (ExpressionInfo)this.m_digitColor.PublishClone(context);
			}
			return numericIndicatorRange;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000C9054 File Offset: 0x000C7254
		internal void SetExprHost(NumericIndicatorRangeExprHost exprHost, ObjectModelImpl reportObjectModel)
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
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000C90E8 File Offset: 0x000C72E8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NumericIndicatorRange, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.StartValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.EndValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.DecimalDigitColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DigitColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000C9198 File Offset: 0x000C7398
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(NumericIndicatorRange.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.GaugePanel)
				{
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
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
					if (memberName == MemberName.DecimalDigitColor)
					{
						writer.Write(this.m_decimalDigitColor);
						continue;
					}
					if (memberName == MemberName.DigitColor)
					{
						writer.Write(this.m_digitColor);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000C9294 File Offset: 0x000C7494
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(NumericIndicatorRange.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.GaugePanel)
				{
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
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
					if (memberName == MemberName.DecimalDigitColor)
					{
						this.m_decimalDigitColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DigitColor)
					{
						this.m_digitColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000C93A8 File Offset: 0x000C75A8
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(NumericIndicatorRange.m_Declaration.ObjectType, out list))
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

		// Token: 0x06002B80 RID: 11136 RVA: 0x000C944C File Offset: 0x000C764C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NumericIndicatorRange;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000C9453 File Offset: 0x000C7653
		internal string EvaluateDecimalDigitColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorRangeDecimalDigitColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000C9479 File Offset: 0x000C7679
		internal string EvaluateDigitColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorRangeDigitColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001799 RID: 6041
		private int m_exprHostID = -1;

		// Token: 0x0400179A RID: 6042
		[NonSerialized]
		private NumericIndicatorRangeExprHost m_exprHost;

		// Token: 0x0400179B RID: 6043
		[Reference]
		private GaugePanel m_gaugePanel;

		// Token: 0x0400179C RID: 6044
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = NumericIndicatorRange.GetDeclaration();

		// Token: 0x0400179D RID: 6045
		private string m_name;

		// Token: 0x0400179E RID: 6046
		private GaugeInputValue m_startValue;

		// Token: 0x0400179F RID: 6047
		private GaugeInputValue m_endValue;

		// Token: 0x040017A0 RID: 6048
		private ExpressionInfo m_decimalDigitColor;

		// Token: 0x040017A1 RID: 6049
		private ExpressionInfo m_digitColor;
	}
}
