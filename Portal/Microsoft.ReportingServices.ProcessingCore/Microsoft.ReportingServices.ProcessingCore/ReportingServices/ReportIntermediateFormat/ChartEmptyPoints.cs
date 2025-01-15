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
	// Token: 0x02000494 RID: 1172
	[Serializable]
	internal sealed class ChartEmptyPoints : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner, ICustomPropertiesHolder
	{
		// Token: 0x06003813 RID: 14355 RVA: 0x000F46E7 File Offset: 0x000F28E7
		internal ChartEmptyPoints()
		{
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000F46EF File Offset: 0x000F28EF
		internal ChartEmptyPoints(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartSeries chartSeries)
			: base(chart)
		{
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x000F46FF File Offset: 0x000F28FF
		internal ChartEmptyPointsExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000F4707 File Offset: 0x000F2907
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x000F470F File Offset: 0x000F290F
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000F4718 File Offset: 0x000F2918
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000F4720 File Offset: 0x000F2920
		// (set) Token: 0x0600381A RID: 14362 RVA: 0x000F4728 File Offset: 0x000F2928
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x000F4731 File Offset: 0x000F2931
		// (set) Token: 0x0600381C RID: 14364 RVA: 0x000F4739 File Offset: 0x000F2939
		internal ChartMarker Marker
		{
			get
			{
				return this.m_marker;
			}
			set
			{
				this.m_marker = value;
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000F4742 File Offset: 0x000F2942
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x000F474A File Offset: 0x000F294A
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel DataLabel
		{
			get
			{
				return this.m_dataLabel;
			}
			set
			{
				this.m_dataLabel = value;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000F4753 File Offset: 0x000F2953
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x000F475B File Offset: 0x000F295B
		internal ExpressionInfo AxisLabel
		{
			get
			{
				return this.m_axisLabel;
			}
			set
			{
				this.m_axisLabel = value;
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x000F4764 File Offset: 0x000F2964
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x000F476C File Offset: 0x000F296C
		public DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x000F4775 File Offset: 0x000F2975
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x000F477D File Offset: 0x000F297D
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x000F4786 File Offset: 0x000F2986
		public override IInstancePath InstancePath
		{
			get
			{
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries;
				}
				return base.InstancePath;
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000F47A0 File Offset: 0x000F29A0
		internal void SetExprHost(ChartEmptyPointsExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_marker != null && this.m_exprHost.ChartMarkerHost != null)
			{
				this.m_marker.SetExprHost(this.m_exprHost.ChartMarkerHost, reportObjectModel);
			}
			if (this.m_dataLabel != null && this.m_exprHost.DataLabelHost != null)
			{
				this.m_dataLabel.SetExprHost(this.m_exprHost.DataLabelHost, reportObjectModel);
			}
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
			if (this.m_customProperties != null && this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000F4888 File Offset: 0x000F2A88
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartEmptyPointsStart();
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_marker != null)
			{
				this.m_marker.Initialize(context);
			}
			if (this.m_dataLabel != null)
			{
				this.m_dataLabel.Initialize(context);
			}
			if (this.m_axisLabel != null)
			{
				this.m_axisLabel.Initialize("AxisLabel", context);
				context.ExprHostBuilder.ChartEmptyPointsAxisLabel(this.m_axisLabel);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, context);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartEmptyPointsToolTip(this.m_toolTip);
			}
			context.ExprHostBuilder.ChartEmptyPointsEnd();
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000F495C File Offset: 0x000F2B5C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartEmptyPoints chartEmptyPoints = (ChartEmptyPoints)base.PublishClone(context);
			if (this.m_action != null)
			{
				chartEmptyPoints.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_marker != null)
			{
				chartEmptyPoints.m_marker = (ChartMarker)this.m_marker.PublishClone(context);
			}
			if (this.m_dataLabel != null)
			{
				chartEmptyPoints.m_dataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)this.m_dataLabel.PublishClone(context);
			}
			if (this.m_axisLabel != null)
			{
				chartEmptyPoints.m_axisLabel = (ExpressionInfo)this.m_axisLabel.PublishClone(context);
			}
			if (this.m_customProperties != null)
			{
				chartEmptyPoints.m_customProperties = new DataValueList(this.m_customProperties.Count);
				foreach (object obj in this.m_customProperties)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)obj;
					chartEmptyPoints.m_customProperties.Add((Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)dataValue.PublishClone(context));
				}
			}
			if (this.m_toolTip != null)
			{
				chartEmptyPoints.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			return chartEmptyPoints;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000F4A90 File Offset: 0x000F2C90
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartEmptyPoints, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Marker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMarker),
				new MemberInfo(MemberName.DataLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataLabel),
				new MemberInfo(MemberName.AxisLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000F4B48 File Offset: 0x000F2D48
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateAxisLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartEmptyPointsAxisLabelExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000F4B70 File Offset: 0x000F2D70
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartEmptyPointsToolTipExpression(this, this.m_chart.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_chart.StyleClass, this.m_styleClass, context, base.ObjectType, base.Name);
			}
			return text;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000F4BF0 File Offset: 0x000F2DF0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartEmptyPoints.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataLabel)
				{
					if (memberName == MemberName.ToolTip)
					{
						writer.Write(this.m_toolTip);
						continue;
					}
					if (memberName == MemberName.Marker)
					{
						writer.Write(this.m_marker);
						continue;
					}
					if (memberName == MemberName.DataLabel)
					{
						writer.Write(this.m_dataLabel);
						continue;
					}
				}
				else if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.AxisLabel)
					{
						writer.Write(this.m_axisLabel);
						continue;
					}
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.CustomProperties)
					{
						writer.Write(this.m_customProperties);
						continue;
					}
					if (memberName == MemberName.ChartSeries)
					{
						writer.WriteReference(this.m_chartSeries);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000F4CF8 File Offset: 0x000F2EF8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartEmptyPoints.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataLabel)
				{
					if (memberName == MemberName.ToolTip)
					{
						this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Marker)
					{
						this.m_marker = (ChartMarker)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DataLabel)
					{
						this.m_dataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.AxisLabel)
					{
						this.m_axisLabel = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.CustomProperties)
					{
						this.m_customProperties = reader.ReadListOfRIFObjects<DataValueList>();
						continue;
					}
					if (memberName == MemberName.ChartSeries)
					{
						this.m_chartSeries = reader.ReadReference<ChartSeries>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000F4E20 File Offset: 0x000F3020
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartEmptyPoints.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.ChartSeries)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chartSeries = (ChartSeries)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000F4ECC File Offset: 0x000F30CC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartEmptyPoints;
		}

		// Token: 0x04001B20 RID: 6944
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001B21 RID: 6945
		private ChartMarker m_marker;

		// Token: 0x04001B22 RID: 6946
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel m_dataLabel;

		// Token: 0x04001B23 RID: 6947
		private ExpressionInfo m_axisLabel;

		// Token: 0x04001B24 RID: 6948
		private DataValueList m_customProperties;

		// Token: 0x04001B25 RID: 6949
		private ExpressionInfo m_toolTip;

		// Token: 0x04001B26 RID: 6950
		[Reference]
		private ChartSeries m_chartSeries;

		// Token: 0x04001B27 RID: 6951
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartEmptyPoints.GetDeclaration();

		// Token: 0x04001B28 RID: 6952
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001B29 RID: 6953
		[NonSerialized]
		private ChartEmptyPointsExprHost m_exprHost;

		// Token: 0x04001B2A RID: 6954
		[NonSerialized]
		private Formatter m_formatter;
	}
}
