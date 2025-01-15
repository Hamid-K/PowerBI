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
	// Token: 0x020003EB RID: 1003
	[Serializable]
	internal class GaugeInputValue : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600294F RID: 10575 RVA: 0x000C1276 File Offset: 0x000BF476
		internal GaugeInputValue()
		{
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000C1285 File Offset: 0x000BF485
		internal GaugeInputValue(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x000C129B File Offset: 0x000BF49B
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x000C12A3 File Offset: 0x000BF4A3
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

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000C12AC File Offset: 0x000BF4AC
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x000C12B4 File Offset: 0x000BF4B4
		internal ExpressionInfo Formula
		{
			get
			{
				return this.m_formula;
			}
			set
			{
				this.m_formula = value;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000C12BD File Offset: 0x000BF4BD
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x000C12C5 File Offset: 0x000BF4C5
		internal ExpressionInfo MinPercent
		{
			get
			{
				return this.m_minPercent;
			}
			set
			{
				this.m_minPercent = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x000C12CE File Offset: 0x000BF4CE
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x000C12D6 File Offset: 0x000BF4D6
		internal ExpressionInfo MaxPercent
		{
			get
			{
				return this.m_maxPercent;
			}
			set
			{
				this.m_maxPercent = value;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000C12DF File Offset: 0x000BF4DF
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x000C12E7 File Offset: 0x000BF4E7
		internal ExpressionInfo Multiplier
		{
			get
			{
				return this.m_multiplier;
			}
			set
			{
				this.m_multiplier = value;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x000C12F0 File Offset: 0x000BF4F0
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x000C12F8 File Offset: 0x000BF4F8
		internal ExpressionInfo AddConstant
		{
			get
			{
				return this.m_addConstant;
			}
			set
			{
				this.m_addConstant = value;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000C1301 File Offset: 0x000BF501
		// (set) Token: 0x0600295E RID: 10590 RVA: 0x000C1309 File Offset: 0x000BF509
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000C1312 File Offset: 0x000BF512
		// (set) Token: 0x06002960 RID: 10592 RVA: 0x000C131A File Offset: 0x000BF51A
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000C1323 File Offset: 0x000BF523
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000C1330 File Offset: 0x000BF530
		internal GaugeInputValueExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000C1338 File Offset: 0x000BF538
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000C1340 File Offset: 0x000BF540
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.GaugeInputValueStart(index);
			if (this.m_value != null)
			{
				this.InitializeValue(context);
			}
			if (this.m_formula != null)
			{
				this.m_formula.Initialize("Formula", context);
				context.ExprHostBuilder.GaugeInputValueFormula(this.m_formula);
			}
			if (this.m_minPercent != null)
			{
				this.m_minPercent.Initialize("MinPercent", context);
				context.ExprHostBuilder.GaugeInputValueMinPercent(this.m_minPercent);
			}
			if (this.m_maxPercent != null)
			{
				this.m_maxPercent.Initialize("MaxPercent", context);
				context.ExprHostBuilder.GaugeInputValueMaxPercent(this.m_maxPercent);
			}
			if (this.m_multiplier != null)
			{
				this.m_multiplier.Initialize("Multiplier", context);
				context.ExprHostBuilder.GaugeInputValueMultiplier(this.m_multiplier);
			}
			if (this.m_addConstant != null)
			{
				this.m_addConstant.Initialize("AddConstant", context);
				context.ExprHostBuilder.GaugeInputValueAddConstant(this.m_addConstant);
			}
			this.m_exprHostID = context.ExprHostBuilder.GaugeInputValueEnd();
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000C1452 File Offset: 0x000BF652
		protected virtual void InitializeValue(InitializationContext context)
		{
			this.m_value.Initialize("Value", context);
			context.ExprHostBuilder.GaugeInputValueValue(this.m_value);
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000C1478 File Offset: 0x000BF678
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			GaugeInputValue gaugeInputValue = (GaugeInputValue)base.MemberwiseClone();
			gaugeInputValue.m_gaugePanel = (GaugePanel)context.CurrentDataRegionClone;
			if (this.m_value != null)
			{
				gaugeInputValue.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_formula != null)
			{
				gaugeInputValue.m_formula = (ExpressionInfo)this.m_formula.PublishClone(context);
			}
			if (this.m_minPercent != null)
			{
				gaugeInputValue.m_minPercent = (ExpressionInfo)this.m_minPercent.PublishClone(context);
			}
			if (this.m_maxPercent != null)
			{
				gaugeInputValue.m_maxPercent = (ExpressionInfo)this.m_maxPercent.PublishClone(context);
			}
			if (this.m_multiplier != null)
			{
				gaugeInputValue.m_multiplier = (ExpressionInfo)this.m_multiplier.PublishClone(context);
			}
			if (this.m_addConstant != null)
			{
				gaugeInputValue.m_addConstant = (ExpressionInfo)this.m_addConstant.PublishClone(context);
			}
			return gaugeInputValue;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000C155E File Offset: 0x000BF75E
		internal void SetExprHost(GaugeInputValueExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000C1588 File Offset: 0x000BF788
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Formula, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinPercent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaxPercent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Multiplier, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AddConstant, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Token.Reference)
			});
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000C1674 File Offset: 0x000BF874
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(GaugeInputValue.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementName)
				{
					if (memberName <= MemberName.Formula)
					{
						if (memberName == MemberName.Value)
						{
							writer.Write(this.m_value);
							continue;
						}
						if (memberName == MemberName.Formula)
						{
							writer.Write(this.m_formula);
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
						if (memberName == MemberName.DataElementName)
						{
							writer.Write(this.m_dataElementName);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.GaugePanel)
				{
					if (memberName == MemberName.DataElementOutput)
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
						continue;
					}
					if (memberName == MemberName.GaugePanel)
					{
						writer.WriteReference(this.m_gaugePanel);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.MinPercent:
						writer.Write(this.m_minPercent);
						continue;
					case MemberName.MaxPercent:
						writer.Write(this.m_maxPercent);
						continue;
					case MemberName.AddConstant:
						writer.Write(this.m_addConstant);
						continue;
					default:
						if (memberName == MemberName.Multiplier)
						{
							writer.Write(this.m_multiplier);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000C17DC File Offset: 0x000BF9DC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(GaugeInputValue.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementName)
				{
					if (memberName <= MemberName.Formula)
					{
						if (memberName == MemberName.Value)
						{
							this.m_value = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Formula)
						{
							this.m_formula = (ExpressionInfo)reader.ReadRIFObject();
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
						if (memberName == MemberName.DataElementName)
						{
							this.m_dataElementName = reader.ReadString();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.GaugePanel)
				{
					if (memberName == MemberName.DataElementOutput)
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.GaugePanel)
					{
						this.m_gaugePanel = reader.ReadReference<GaugePanel>(this);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.MinPercent:
						this.m_minPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MaxPercent:
						this.m_maxPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.AddConstant:
						this.m_addConstant = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.Multiplier)
						{
							this.m_multiplier = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000C1964 File Offset: 0x000BFB64
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000C196C File Offset: 0x000BFB6C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(GaugeInputValue.m_Declaration.ObjectType, out list))
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

		// Token: 0x0600296D RID: 10605 RVA: 0x000C1A10 File Offset: 0x000BFC10
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(this.m_gaugePanel.GaugeRow != null && this.m_gaugePanel.GaugeRow.GaugeCell != null);
			context.SetupContext(this.m_gaugePanel.GaugeRow.GaugeCell, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeInputValueValueExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000C1A78 File Offset: 0x000BFC78
		internal GaugeInputValueFormulas EvaluateFormula(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(this.m_gaugePanel.GaugeRow != null && this.m_gaugePanel.GaugeRow.GaugeCell != null);
			context.SetupContext(this.m_gaugePanel.GaugeRow.GaugeCell, reportScopeInstance);
			return EnumTranslator.TranslateGaugeInputValueFormulas(context.ReportRuntime.EvaluateGaugeInputValueFormulaExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000C1AEC File Offset: 0x000BFCEC
		internal double EvaluateMinPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(this.m_gaugePanel.GaugeRow != null && this.m_gaugePanel.GaugeRow.GaugeCell != null);
			context.SetupContext(this.m_gaugePanel.GaugeRow.GaugeCell, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeInputValueMinPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000C1B54 File Offset: 0x000BFD54
		internal double EvaluateMaxPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(this.m_gaugePanel.GaugeRow != null && this.m_gaugePanel.GaugeRow.GaugeCell != null);
			context.SetupContext(this.m_gaugePanel.GaugeRow.GaugeCell, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeInputValueMaxPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000C1BBC File Offset: 0x000BFDBC
		internal double EvaluateMultiplier(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(this.m_gaugePanel.GaugeRow != null && this.m_gaugePanel.GaugeRow.GaugeCell != null);
			context.SetupContext(this.m_gaugePanel.GaugeRow.GaugeCell, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeInputValueMultiplierExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000C1C24 File Offset: 0x000BFE24
		internal double EvaluateAddConstant(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(this.m_gaugePanel.GaugeRow != null && this.m_gaugePanel.GaugeRow.GaugeCell != null);
			context.SetupContext(this.m_gaugePanel.GaugeRow.GaugeCell, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeInputValueAddConstantExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016FA RID: 5882
		[NonSerialized]
		private GaugeInputValueExprHost m_exprHost;

		// Token: 0x040016FB RID: 5883
		[Reference]
		private GaugePanel m_gaugePanel;

		// Token: 0x040016FC RID: 5884
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeInputValue.GetDeclaration();

		// Token: 0x040016FD RID: 5885
		private ExpressionInfo m_value;

		// Token: 0x040016FE RID: 5886
		private ExpressionInfo m_formula;

		// Token: 0x040016FF RID: 5887
		private ExpressionInfo m_minPercent;

		// Token: 0x04001700 RID: 5888
		private ExpressionInfo m_maxPercent;

		// Token: 0x04001701 RID: 5889
		private ExpressionInfo m_multiplier;

		// Token: 0x04001702 RID: 5890
		private ExpressionInfo m_addConstant;

		// Token: 0x04001703 RID: 5891
		private string m_dataElementName;

		// Token: 0x04001704 RID: 5892
		private DataElementOutputTypes m_dataElementOutput;

		// Token: 0x04001705 RID: 5893
		private int m_exprHostID = -1;
	}
}
