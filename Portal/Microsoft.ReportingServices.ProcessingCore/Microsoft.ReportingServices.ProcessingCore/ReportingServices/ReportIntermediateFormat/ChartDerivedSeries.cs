using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	internal sealed class ChartDerivedSeries : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003846 RID: 14406 RVA: 0x000F53AB File Offset: 0x000F35AB
		internal ChartDerivedSeries()
		{
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000F53B3 File Offset: 0x000F35B3
		internal ChartDerivedSeries(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000F53C2 File Offset: 0x000F35C2
		internal ChartSeries SourceSeries
		{
			get
			{
				if (this.m_sourceSeries == null)
				{
					this.m_sourceSeries = this.m_chart.ChartSeriesCollection.GetByName(this.SourceChartSeriesName);
				}
				return this.m_sourceSeries;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000F53EE File Offset: 0x000F35EE
		internal ChartDerivedSeriesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000F53F6 File Offset: 0x000F35F6
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000F53FE File Offset: 0x000F35FE
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x000F5406 File Offset: 0x000F3606
		internal ChartSeries Series
		{
			get
			{
				return this.m_series;
			}
			set
			{
				this.m_series = value;
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000F540F File Offset: 0x000F360F
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000F5417 File Offset: 0x000F3617
		internal List<ChartFormulaParameter> FormulaParameters
		{
			get
			{
				return this.m_chartFormulaParameters;
			}
			set
			{
				this.m_chartFormulaParameters = value;
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000F5420 File Offset: 0x000F3620
		// (set) Token: 0x06003850 RID: 14416 RVA: 0x000F5437 File Offset: 0x000F3637
		internal string SourceChartSeriesName
		{
			get
			{
				if (this.m_sourceChartSeriesName != null)
				{
					return this.m_sourceChartSeriesName.StringValue;
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.m_sourceChartSeriesName = ExpressionInfo.CreateConstExpression(value);
					return;
				}
				this.m_sourceChartSeriesName = null;
			}
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000F5450 File Offset: 0x000F3650
		// (set) Token: 0x06003852 RID: 14418 RVA: 0x000F546C File Offset: 0x000F366C
		internal ChartSeriesFormula DerivedSeriesFormula
		{
			get
			{
				if (this.m_derivedSeriesFormula != null)
				{
					return EnumTranslator.TranslateChartSeriesFormula(this.m_derivedSeriesFormula.StringValue);
				}
				return ChartSeriesFormula.BollingerBands;
			}
			set
			{
				this.m_derivedSeriesFormula = ExpressionInfo.CreateConstExpression(value.ToString());
			}
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000F5488 File Offset: 0x000F3688
		internal void SetExprHost(ChartDerivedSeriesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_series != null && this.m_exprHost.ChartSeriesHost != null)
			{
				this.m_series.SetExprHost(this.m_exprHost.ChartSeriesHost, reportObjectModel);
			}
			IList<ChartFormulaParameterExprHost> chartFormulaParametersHostsRemotable = this.m_exprHost.ChartFormulaParametersHostsRemotable;
			if (this.m_chartFormulaParameters != null && chartFormulaParametersHostsRemotable != null)
			{
				for (int i = 0; i < this.m_chartFormulaParameters.Count; i++)
				{
					ChartFormulaParameter chartFormulaParameter = this.m_chartFormulaParameters[i];
					if (chartFormulaParameter != null && chartFormulaParameter.ExpressionHostID > -1)
					{
						chartFormulaParameter.SetExprHost(chartFormulaParametersHostsRemotable[chartFormulaParameter.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000F5548 File Offset: 0x000F3748
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.ChartDerivedSeriesStart(index);
			if (this.m_series != null)
			{
				this.m_series.Initialize(context, index.ToString(CultureInfo.InvariantCulture));
			}
			if (this.m_chartFormulaParameters != null)
			{
				for (int i = 0; i < this.m_chartFormulaParameters.Count; i++)
				{
					this.m_chartFormulaParameters[i].Initialize(context);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartDerivedSeriesEnd();
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000F55C4 File Offset: 0x000F37C4
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartDerivedSeries chartDerivedSeries = (ChartDerivedSeries)base.MemberwiseClone();
			chartDerivedSeries.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_series != null)
			{
				chartDerivedSeries.m_series = (ChartSeries)this.m_series.PublishClone(context);
			}
			if (this.m_sourceChartSeriesName != null)
			{
				chartDerivedSeries.m_sourceChartSeriesName = (ExpressionInfo)this.m_sourceChartSeriesName.PublishClone(context);
			}
			if (this.m_derivedSeriesFormula != null)
			{
				chartDerivedSeries.m_derivedSeriesFormula = (ExpressionInfo)this.m_derivedSeriesFormula.PublishClone(context);
			}
			if (this.m_chartFormulaParameters != null)
			{
				chartDerivedSeries.m_chartFormulaParameters = new List<ChartFormulaParameter>(this.m_chartFormulaParameters.Count);
				foreach (ChartFormulaParameter chartFormulaParameter in this.m_chartFormulaParameters)
				{
					chartDerivedSeries.m_chartFormulaParameters.Add((ChartFormulaParameter)chartFormulaParameter.PublishClone(context));
				}
			}
			return chartDerivedSeries;
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000F56C4 File Offset: 0x000F38C4
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDerivedSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Series, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries),
				new MemberInfo(MemberName.SourceChartSeriesName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DerivedSeriesFormula, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartFormulaParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartFormulaParameter),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference)
			});
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000F5764 File Offset: 0x000F3964
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartDerivedSeries.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartFormulaParameters)
				{
					switch (memberName)
					{
					case MemberName.Series:
						writer.Write(this.m_series);
						continue;
					case MemberName.SourceChartSeriesName:
						writer.Write(this.m_sourceChartSeriesName);
						continue;
					case MemberName.DerivedSeriesFormula:
						writer.Write(this.m_derivedSeriesFormula);
						continue;
					default:
						if (memberName == MemberName.ChartFormulaParameters)
						{
							writer.Write<ChartFormulaParameter>(this.m_chartFormulaParameters);
							continue;
						}
						break;
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
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000F583C File Offset: 0x000F3A3C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartDerivedSeries.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartFormulaParameters)
				{
					switch (memberName)
					{
					case MemberName.Series:
						this.m_series = (ChartSeries)reader.ReadRIFObject();
						continue;
					case MemberName.SourceChartSeriesName:
						this.m_sourceChartSeriesName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DerivedSeriesFormula:
						this.m_derivedSeriesFormula = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ChartFormulaParameters)
						{
							this.m_chartFormulaParameters = reader.ReadGenericListOfRIFObjects<ChartFormulaParameter>();
							continue;
						}
						break;
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
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000F5924 File Offset: 0x000F3B24
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartDerivedSeries.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Chart)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000F59C8 File Offset: 0x000F3BC8
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDerivedSeries;
		}

		// Token: 0x04001B34 RID: 6964
		private int m_exprHostID;

		// Token: 0x04001B35 RID: 6965
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001B36 RID: 6966
		[NonSerialized]
		private ChartSeries m_sourceSeries;

		// Token: 0x04001B37 RID: 6967
		private ChartSeries m_series;

		// Token: 0x04001B38 RID: 6968
		private ExpressionInfo m_sourceChartSeriesName;

		// Token: 0x04001B39 RID: 6969
		private ExpressionInfo m_derivedSeriesFormula;

		// Token: 0x04001B3A RID: 6970
		private List<ChartFormulaParameter> m_chartFormulaParameters;

		// Token: 0x04001B3B RID: 6971
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartDerivedSeries.GetDeclaration();

		// Token: 0x04001B3C RID: 6972
		[NonSerialized]
		private ChartDerivedSeriesExprHost m_exprHost;
	}
}
