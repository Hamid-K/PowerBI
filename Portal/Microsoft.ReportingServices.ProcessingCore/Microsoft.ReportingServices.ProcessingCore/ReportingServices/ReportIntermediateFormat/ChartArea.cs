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
	// Token: 0x02000487 RID: 1159
	[Serializable]
	internal sealed class ChartArea : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060035FB RID: 13819 RVA: 0x000EC123 File Offset: 0x000EA323
		internal ChartArea()
		{
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000EC12B File Offset: 0x000EA32B
		internal ChartArea(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000EC134 File Offset: 0x000EA334
		// (set) Token: 0x060035FE RID: 13822 RVA: 0x000EC13C File Offset: 0x000EA33C
		internal string ChartAreaName
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

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000EC145 File Offset: 0x000EA345
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x000EC14D File Offset: 0x000EA34D
		internal List<ChartAxis> CategoryAxes
		{
			get
			{
				return this.m_categoryAxes;
			}
			set
			{
				this.m_categoryAxes = value;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x000EC156 File Offset: 0x000EA356
		// (set) Token: 0x06003602 RID: 13826 RVA: 0x000EC15E File Offset: 0x000EA35E
		internal List<ChartAxis> ValueAxes
		{
			get
			{
				return this.m_valueAxes;
			}
			set
			{
				this.m_valueAxes = value;
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x000EC167 File Offset: 0x000EA367
		// (set) Token: 0x06003604 RID: 13828 RVA: 0x000EC16F File Offset: 0x000EA36F
		internal ChartThreeDProperties ThreeDProperties
		{
			get
			{
				return this.m_3dProperties;
			}
			set
			{
				this.m_3dProperties = value;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000EC178 File Offset: 0x000EA378
		internal ChartAreaExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000EC180 File Offset: 0x000EA380
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000EC188 File Offset: 0x000EA388
		// (set) Token: 0x06003608 RID: 13832 RVA: 0x000EC190 File Offset: 0x000EA390
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

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000EC199 File Offset: 0x000EA399
		// (set) Token: 0x0600360A RID: 13834 RVA: 0x000EC1A1 File Offset: 0x000EA3A1
		internal ExpressionInfo AlignOrientation
		{
			get
			{
				return this.m_alignOrientation;
			}
			set
			{
				this.m_alignOrientation = value;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000EC1AA File Offset: 0x000EA3AA
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x000EC1B2 File Offset: 0x000EA3B2
		internal ChartAlignType ChartAlignType
		{
			get
			{
				return this.m_chartAlignType;
			}
			set
			{
				this.m_chartAlignType = value;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000EC1BB File Offset: 0x000EA3BB
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x000EC1C3 File Offset: 0x000EA3C3
		internal string AlignWithChartArea
		{
			get
			{
				return this.m_alignWithChartArea;
			}
			set
			{
				this.m_alignWithChartArea = value;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000EC1CC File Offset: 0x000EA3CC
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x000EC1D4 File Offset: 0x000EA3D4
		internal ExpressionInfo EquallySizedAxesFont
		{
			get
			{
				return this.m_equallySizedAxesFont;
			}
			set
			{
				this.m_equallySizedAxesFont = value;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000EC1DD File Offset: 0x000EA3DD
		// (set) Token: 0x06003612 RID: 13842 RVA: 0x000EC1E5 File Offset: 0x000EA3E5
		internal ChartElementPosition ChartElementPosition
		{
			get
			{
				return this.m_chartElementPosition;
			}
			set
			{
				this.m_chartElementPosition = value;
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000EC1EE File Offset: 0x000EA3EE
		// (set) Token: 0x06003614 RID: 13844 RVA: 0x000EC1F6 File Offset: 0x000EA3F6
		internal ChartElementPosition ChartInnerPlotPosition
		{
			get
			{
				return this.m_chartInnerPlotPosition;
			}
			set
			{
				this.m_chartInnerPlotPosition = value;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000EC1FF File Offset: 0x000EA3FF
		// (set) Token: 0x06003616 RID: 13846 RVA: 0x000EC207 File Offset: 0x000EA407
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Chart Chart
		{
			get
			{
				return this.m_chart;
			}
			set
			{
				this.m_chart = value;
			}
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000EC210 File Offset: 0x000EA410
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartAreaStart(this.m_name);
			base.Initialize(context);
			if (this.m_categoryAxes != null)
			{
				for (int i = 0; i < this.m_categoryAxes.Count; i++)
				{
					this.m_categoryAxes[i].Initialize(context, false);
				}
			}
			if (this.m_valueAxes != null)
			{
				for (int j = 0; j < this.m_valueAxes.Count; j++)
				{
					this.m_valueAxes[j].Initialize(context, true);
				}
			}
			if (this.m_3dProperties != null)
			{
				this.m_3dProperties.Initialize(context);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ChartAreaHidden(this.m_hidden);
			}
			if (this.m_alignOrientation != null)
			{
				this.m_alignOrientation.Initialize("AlignOrientation", context);
				context.ExprHostBuilder.ChartAreaAlignOrientation(this.m_alignOrientation);
			}
			if (this.m_chartAlignType != null)
			{
				this.m_chartAlignType.Initialize(context);
			}
			if (this.m_equallySizedAxesFont != null)
			{
				this.m_equallySizedAxesFont.Initialize("EquallySizedAxesFont", context);
				context.ExprHostBuilder.ChartAreaEquallySizedAxesFont(this.m_equallySizedAxesFont);
			}
			if (this.m_chartElementPosition != null)
			{
				this.m_chartElementPosition.Initialize(context);
			}
			if (this.m_chartInnerPlotPosition != null)
			{
				this.m_chartInnerPlotPosition.Initialize(context, true);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartAreaEnd();
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000EC37C File Offset: 0x000EA57C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartArea chartArea = (ChartArea)base.PublishClone(context);
			if (this.m_categoryAxes != null)
			{
				chartArea.m_categoryAxes = new List<ChartAxis>(this.m_categoryAxes.Count);
				foreach (ChartAxis chartAxis in this.m_categoryAxes)
				{
					chartArea.m_categoryAxes.Add((ChartAxis)chartAxis.PublishClone(context));
				}
			}
			if (this.m_valueAxes != null)
			{
				chartArea.m_valueAxes = new List<ChartAxis>(this.m_valueAxes.Count);
				foreach (ChartAxis chartAxis2 in this.m_valueAxes)
				{
					chartArea.m_valueAxes.Add((ChartAxis)chartAxis2.PublishClone(context));
				}
			}
			if (this.m_3dProperties != null)
			{
				chartArea.m_3dProperties = (ChartThreeDProperties)this.m_3dProperties.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				chartArea.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_alignOrientation != null)
			{
				chartArea.m_alignOrientation = (ExpressionInfo)this.m_alignOrientation.PublishClone(context);
			}
			if (this.m_chartAlignType != null)
			{
				chartArea.m_chartAlignType = (ChartAlignType)this.m_chartAlignType.PublishClone(context);
			}
			if (this.m_equallySizedAxesFont != null)
			{
				chartArea.m_equallySizedAxesFont = (ExpressionInfo)this.m_equallySizedAxesFont.PublishClone(context);
			}
			if (this.m_chartElementPosition != null)
			{
				chartArea.m_chartElementPosition = (ChartElementPosition)this.m_chartElementPosition.PublishClone(context);
			}
			if (this.m_chartInnerPlotPosition != null)
			{
				chartArea.m_chartInnerPlotPosition = (ChartElementPosition)this.m_chartInnerPlotPosition.PublishClone(context);
			}
			return chartArea;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000EC554 File Offset: 0x000EA754
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartArea, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.CategoryAxes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxis),
				new MemberInfo(MemberName.ValueAxes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxis),
				new MemberInfo(MemberName.ThreeDProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ThreeDProperties),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AlignOrientation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartAlignType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAlignType),
				new MemberInfo(MemberName.AlignWithChartArea, Token.String),
				new MemberInfo(MemberName.EquallySizedAxesFont, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ChartElementPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartElementPosition),
				new MemberInfo(MemberName.ChartInnerPlotPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartElementPosition)
			});
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000EC674 File Offset: 0x000EA874
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartArea.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ThreeDProperties)
				{
					if (memberName <= MemberName.Hidden)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
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
						if (memberName == MemberName.ThreeDProperties)
						{
							writer.Write(this.m_3dProperties);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CategoryAxes)
				{
					if (memberName == MemberName.ValueAxes)
					{
						writer.Write<ChartAxis>(this.m_valueAxes);
						continue;
					}
					if (memberName == MemberName.CategoryAxes)
					{
						writer.Write<ChartAxis>(this.m_categoryAxes);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.AlignOrientation:
						writer.Write(this.m_alignOrientation);
						continue;
					case MemberName.ChartAlignType:
						writer.Write(this.m_chartAlignType);
						continue;
					case MemberName.AlignWithChartArea:
						writer.Write(this.m_alignWithChartArea);
						continue;
					case MemberName.EquallySizedAxesFont:
						writer.Write(this.m_equallySizedAxesFont);
						continue;
					default:
						if (memberName == MemberName.ChartElementPosition)
						{
							writer.Write(this.m_chartElementPosition);
							continue;
						}
						if (memberName == MemberName.ChartInnerPlotPosition)
						{
							writer.Write(this.m_chartInnerPlotPosition);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000EC814 File Offset: 0x000EAA14
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartArea.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ThreeDProperties)
				{
					if (memberName <= MemberName.Hidden)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
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
						if (memberName == MemberName.ThreeDProperties)
						{
							this.m_3dProperties = (ChartThreeDProperties)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CategoryAxes)
				{
					if (memberName == MemberName.ValueAxes)
					{
						this.m_valueAxes = reader.ReadGenericListOfRIFObjects<ChartAxis>();
						continue;
					}
					if (memberName == MemberName.CategoryAxes)
					{
						this.m_categoryAxes = reader.ReadGenericListOfRIFObjects<ChartAxis>();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.AlignOrientation:
						this.m_alignOrientation = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ChartAlignType:
						this.m_chartAlignType = (ChartAlignType)reader.ReadRIFObject();
						continue;
					case MemberName.AlignWithChartArea:
						this.m_alignWithChartArea = reader.ReadString();
						continue;
					case MemberName.EquallySizedAxesFont:
						this.m_equallySizedAxesFont = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ChartElementPosition)
						{
							this.m_chartElementPosition = (ChartElementPosition)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ChartInnerPlotPosition)
						{
							this.m_chartInnerPlotPosition = (ChartElementPosition)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000EC9D7 File Offset: 0x000EABD7
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000EC9E1 File Offset: 0x000EABE1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartArea;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000EC9E8 File Offset: 0x000EABE8
		internal void SetExprHost(ChartAreaExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_3dProperties != null && this.m_exprHost.Chart3DPropertiesHost != null)
			{
				this.m_3dProperties.SetExprHost(this.m_exprHost.Chart3DPropertiesHost, reportObjectModel);
			}
			if (this.m_chartAlignType != null)
			{
				this.m_chartAlignType.SetExprHost(this);
			}
			IList<ChartAxisExprHost> categoryAxesHostsRemotable = exprHost.CategoryAxesHostsRemotable;
			if (this.m_categoryAxes != null && categoryAxesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_categoryAxes.Count; i++)
				{
					ChartAxis chartAxis = this.m_categoryAxes[i];
					if (chartAxis != null && chartAxis.ExpressionHostID > -1)
					{
						chartAxis.SetExprHost(categoryAxesHostsRemotable[chartAxis.ExpressionHostID], reportObjectModel);
					}
				}
			}
			IList<ChartAxisExprHost> valueAxesHostsRemotable = exprHost.ValueAxesHostsRemotable;
			if (this.m_valueAxes != null && valueAxesHostsRemotable != null)
			{
				for (int j = 0; j < this.m_valueAxes.Count; j++)
				{
					ChartAxis chartAxis2 = this.m_valueAxes[j];
					if (chartAxis2 != null && chartAxis2.ExpressionHostID > -1)
					{
						chartAxis2.SetExprHost(valueAxesHostsRemotable[chartAxis2.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_chartElementPosition != null && this.m_exprHost.ChartElementPositionHost != null)
			{
				this.m_chartElementPosition.SetExprHost(this.m_exprHost.ChartElementPositionHost, reportObjectModel);
			}
			if (this.m_chartInnerPlotPosition != null && this.m_exprHost.ChartInnerPlotPositionHost != null)
			{
				this.m_chartInnerPlotPosition.SetExprHost(this.m_exprHost.ChartInnerPlotPositionHost, reportObjectModel);
			}
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000ECB67 File Offset: 0x000EAD67
		internal bool EvaluateHidden(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAreaHiddenExpression(this, base.Name, "Hidden");
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000ECB8D File Offset: 0x000EAD8D
		internal ChartAreaAlignOrientations EvaluateAlignOrientation(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartAreaAlignOrientation(context.ReportRuntime.EvaluateChartAreaAlignOrientationExpression(this, base.Name, "AlignOrientation"), context.ReportRuntime);
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000ECBBE File Offset: 0x000EADBE
		internal bool EvaluateEquallySizedAxesFont(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAreaEquallySizedAxesFontExpression(this, base.Name, "EquallySizedAxesFont");
		}

		// Token: 0x04001A6D RID: 6765
		private string m_name;

		// Token: 0x04001A6E RID: 6766
		private List<ChartAxis> m_categoryAxes;

		// Token: 0x04001A6F RID: 6767
		private List<ChartAxis> m_valueAxes;

		// Token: 0x04001A70 RID: 6768
		private ChartThreeDProperties m_3dProperties;

		// Token: 0x04001A71 RID: 6769
		private ChartElementPosition m_chartElementPosition;

		// Token: 0x04001A72 RID: 6770
		private ChartElementPosition m_chartInnerPlotPosition;

		// Token: 0x04001A73 RID: 6771
		private int m_exprHostID;

		// Token: 0x04001A74 RID: 6772
		private ExpressionInfo m_hidden;

		// Token: 0x04001A75 RID: 6773
		private ExpressionInfo m_alignOrientation;

		// Token: 0x04001A76 RID: 6774
		private ChartAlignType m_chartAlignType;

		// Token: 0x04001A77 RID: 6775
		private string m_alignWithChartArea;

		// Token: 0x04001A78 RID: 6776
		private ExpressionInfo m_equallySizedAxesFont;

		// Token: 0x04001A79 RID: 6777
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartArea.GetDeclaration();

		// Token: 0x04001A7A RID: 6778
		[NonSerialized]
		private ChartAreaExprHost m_exprHost;
	}
}
