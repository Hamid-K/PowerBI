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
	// Token: 0x02000495 RID: 1173
	[Serializable]
	internal sealed class ChartFormulaParameter : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003831 RID: 14385 RVA: 0x000F4EDF File Offset: 0x000F30DF
		internal ChartFormulaParameter()
		{
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000F4EE7 File Offset: 0x000F30E7
		internal ChartFormulaParameter(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartDerivedSeries parentDerivedSeries)
		{
			this.m_chart = chart;
			this.m_parentDerivedSeries = parentDerivedSeries;
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06003833 RID: 14387 RVA: 0x000F4EFD File Offset: 0x000F30FD
		// (set) Token: 0x06003834 RID: 14388 RVA: 0x000F4F05 File Offset: 0x000F3105
		internal string FormulaParameterName
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

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000F4F0E File Offset: 0x000F310E
		internal ChartFormulaParameterExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000F4F16 File Offset: 0x000F3116
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000F4F1E File Offset: 0x000F311E
		// (set) Token: 0x06003838 RID: 14392 RVA: 0x000F4F26 File Offset: 0x000F3126
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

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x000F4F2F File Offset: 0x000F312F
		// (set) Token: 0x0600383A RID: 14394 RVA: 0x000F4F37 File Offset: 0x000F3137
		internal string Source
		{
			get
			{
				return this.m_source;
			}
			set
			{
				this.m_source = value;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000F4F40 File Offset: 0x000F3140
		private ChartSeries SourceSeries
		{
			get
			{
				if (this.m_sourceSeries == null && this.m_parentDerivedSeries != null)
				{
					this.m_sourceSeries = this.m_parentDerivedSeries.SourceSeries;
				}
				return this.m_sourceSeries;
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000F4F69 File Offset: 0x000F3169
		internal void SetExprHost(ChartFormulaParameterExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000F4F98 File Offset: 0x000F3198
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartFormulaParameterStart(this.m_name);
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.ChartFormulaParameterValue(this.m_value);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartFormulaParameterEnd();
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000F4FF4 File Offset: 0x000F31F4
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartFormulaParameter chartFormulaParameter = (ChartFormulaParameter)base.MemberwiseClone();
			chartFormulaParameter.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_value != null)
			{
				chartFormulaParameter.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			return chartFormulaParameter;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000F5040 File Offset: 0x000F3240
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartFormulaParameter, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Source, Token.String),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.SourceSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference)
			});
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000F50D9 File Offset: 0x000F32D9
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.SourceSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartFormulaParameterValueExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000F5100 File Offset: 0x000F3300
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartFormulaParameter.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Source)
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
					if (memberName == MemberName.Source)
					{
						writer.Write(this.m_source);
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
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
					if (memberName == MemberName.SourceSeries)
					{
						writer.WriteReference(this.SourceSeries);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000F51D4 File Offset: 0x000F33D4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartFormulaParameter.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Source)
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
					if (memberName == MemberName.Source)
					{
						this.m_source = reader.ReadString();
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
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
					if (memberName == MemberName.SourceSeries)
					{
						this.m_sourceSeries = reader.ReadReference<ChartSeries>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000F52B0 File Offset: 0x000F34B0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartFormulaParameter.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.Chart)
					{
						if (memberName != MemberName.SourceSeries)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_sourceSeries = (ChartSeries)referenceableItems[memberReference.RefID];
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000F5398 File Offset: 0x000F3598
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartFormulaParameter;
		}

		// Token: 0x04001B2B RID: 6955
		private string m_name;

		// Token: 0x04001B2C RID: 6956
		private int m_exprHostID;

		// Token: 0x04001B2D RID: 6957
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001B2E RID: 6958
		[Reference]
		private ChartSeries m_sourceSeries;

		// Token: 0x04001B2F RID: 6959
		[NonSerialized]
		private ChartDerivedSeries m_parentDerivedSeries;

		// Token: 0x04001B30 RID: 6960
		private ExpressionInfo m_value;

		// Token: 0x04001B31 RID: 6961
		private string m_source;

		// Token: 0x04001B32 RID: 6962
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartFormulaParameter.GetDeclaration();

		// Token: 0x04001B33 RID: 6963
		[NonSerialized]
		private ChartFormulaParameterExprHost m_exprHost;
	}
}
