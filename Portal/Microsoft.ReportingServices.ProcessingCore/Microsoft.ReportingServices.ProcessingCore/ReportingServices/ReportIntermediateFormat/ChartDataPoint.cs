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
	// Token: 0x02000498 RID: 1176
	[Serializable]
	internal sealed class ChartDataPoint : Cell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner, IStyleContainer, ICustomPropertiesHolder
	{
		// Token: 0x0600385F RID: 14431 RVA: 0x000F59FA File Offset: 0x000F3BFA
		internal ChartDataPoint()
		{
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000F5A09 File Offset: 0x000F3C09
		internal ChartDataPoint(int id, Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(id, chart)
		{
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000F5A1A File Offset: 0x000F3C1A
		protected override bool IsDataRegionBodyCell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000F5A1D File Offset: 0x000F3C1D
		// (set) Token: 0x06003863 RID: 14435 RVA: 0x000F5A25 File Offset: 0x000F3C25
		internal ChartDataPointValues DataPointValues
		{
			get
			{
				return this.m_dataPointValues;
			}
			set
			{
				this.m_dataPointValues = value;
			}
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000F5A2E File Offset: 0x000F3C2E
		// (set) Token: 0x06003865 RID: 14437 RVA: 0x000F5A36 File Offset: 0x000F3C36
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

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000F5A3F File Offset: 0x000F3C3F
		// (set) Token: 0x06003867 RID: 14439 RVA: 0x000F5A47 File Offset: 0x000F3C47
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

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000F5A50 File Offset: 0x000F3C50
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x000F5A58 File Offset: 0x000F3C58
		public Microsoft.ReportingServices.ReportIntermediateFormat.Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000F5A61 File Offset: 0x000F3C61
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000F5A64 File Offset: 0x000F3C64
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000F5A68 File Offset: 0x000F3C68
		public string Name
		{
			get
			{
				return this.m_dataRegionDef.Name;
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000F5A75 File Offset: 0x000F3C75
		// (set) Token: 0x0600386E RID: 14446 RVA: 0x000F5A7D File Offset: 0x000F3C7D
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

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000F5A86 File Offset: 0x000F3C86
		// (set) Token: 0x06003870 RID: 14448 RVA: 0x000F5A8E File Offset: 0x000F3C8E
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

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000F5A97 File Offset: 0x000F3C97
		DataValueList ICustomPropertiesHolder.CustomProperties
		{
			get
			{
				return this.CustomProperties;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000F5A9F File Offset: 0x000F3C9F
		IInstancePath ICustomPropertiesHolder.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000F5AA2 File Offset: 0x000F3CA2
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x000F5AAA File Offset: 0x000F3CAA
		internal DataValueList CustomProperties
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

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000F5AB3 File Offset: 0x000F3CB3
		internal ChartDataPointExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000F5ABB File Offset: 0x000F3CBB
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000F5AC3 File Offset: 0x000F3CC3
		// (set) Token: 0x06003878 RID: 14456 RVA: 0x000F5ACB File Offset: 0x000F3CCB
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

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000F5AD4 File Offset: 0x000F3CD4
		// (set) Token: 0x0600387A RID: 14458 RVA: 0x000F5ADC File Offset: 0x000F3CDC
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

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x000F5AE5 File Offset: 0x000F3CE5
		// (set) Token: 0x0600387C RID: 14460 RVA: 0x000F5AED File Offset: 0x000F3CED
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

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000F5AF6 File Offset: 0x000F3CF6
		// (set) Token: 0x0600387E RID: 14462 RVA: 0x000F5AFE File Offset: 0x000F3CFE
		internal ChartItemInLegend ItemInLegend
		{
			get
			{
				return this.m_itemInLegend;
			}
			set
			{
				this.m_itemInLegend = value;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000F5B07 File Offset: 0x000F3D07
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x000F5B0F File Offset: 0x000F3D0F
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

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x000F5B18 File Offset: 0x000F3D18
		public override Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.ChartDataPoint;
			}
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000F5B1C File Offset: 0x000F3D1C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint)base.PublishClone(context);
			if (this.m_action != null)
			{
				chartDataPoint.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_styleClass != null)
			{
				chartDataPoint.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)this.m_styleClass.PublishClone(context);
			}
			if (this.m_customProperties != null)
			{
				chartDataPoint.m_customProperties = new DataValueList(this.m_customProperties.Count);
				foreach (object obj in this.m_customProperties)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)obj;
					chartDataPoint.m_customProperties.Add((Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)dataValue.PublishClone(context));
				}
			}
			if (this.m_marker != null)
			{
				chartDataPoint.m_marker = (ChartMarker)this.m_marker.PublishClone(context);
			}
			if (this.m_dataPointValues != null)
			{
				chartDataPoint.m_dataPointValues = (ChartDataPointValues)this.m_dataPointValues.PublishClone(context);
				chartDataPoint.m_dataPointValues.DataPoint = chartDataPoint;
			}
			if (this.m_dataLabel != null)
			{
				chartDataPoint.m_dataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)this.m_dataLabel.PublishClone(context);
			}
			if (this.m_axisLabel != null)
			{
				chartDataPoint.m_axisLabel = (ExpressionInfo)this.m_axisLabel.PublishClone(context);
			}
			if (this.m_itemInLegend != null)
			{
				chartDataPoint.m_itemInLegend = (ChartItemInLegend)this.m_itemInLegend.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartDataPoint.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			return chartDataPoint;
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000F5CB8 File Offset: 0x000F3EB8
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder exprHostBuilder = context.ExprHostBuilder;
			if (this.m_dataPointValues != null)
			{
				this.m_dataPointValues.Initialize(context);
			}
			if (this.m_dataLabel != null)
			{
				this.m_dataLabel.Initialize(context);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_styleClass != null)
			{
				exprHostBuilder.DataPointStyleStart();
				this.m_styleClass.Initialize(context);
				exprHostBuilder.DataPointStyleEnd();
			}
			if (this.m_marker != null)
			{
				this.m_marker.Initialize(context);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, context);
			}
			if (this.m_axisLabel != null)
			{
				this.m_axisLabel.Initialize("AxisLabel", context);
				context.ExprHostBuilder.ChartDataPointAxisLabel(this.m_axisLabel);
			}
			if (this.m_itemInLegend != null)
			{
				this.m_itemInLegend.Initialize(context);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartDataPointToolTip(this.m_toolTip);
			}
			this.DataRendererInitialize(context);
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000F5DCD File Offset: 0x000F3FCD
		protected override Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode
		{
			get
			{
				return Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Chart;
			}
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000F5DD0 File Offset: 0x000F3FD0
		internal void DataRendererInitialize(InitializationContext context)
		{
			if (this.m_dataElementOutput == DataElementOutputTypes.Auto)
			{
				this.m_dataElementOutput = DataElementOutputTypes.Output;
			}
			Microsoft.ReportingServices.ReportPublishing.CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, "Value", context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000F5E10 File Offset: 0x000F4010
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataLabel),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.DataPointValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPointValues),
				new MemberInfo(MemberName.Marker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMarker),
				new MemberInfo(MemberName.AxisLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartItemInLegend, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartItemInLegend),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000F5F17 File Offset: 0x000F4117
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateAxisLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointAxisLabelExpression(this, this.m_dataRegionDef.Name);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000F5F38 File Offset: 0x000F4138
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartDataPointToolTipExpression(this, this.m_dataRegionDef.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_dataRegionDef.StyleClass, this.m_styleClass, context, this.ObjectType, this.Name);
			}
			return text;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000F5FB0 File Offset: 0x000F41B0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartItemInLegend)
				{
					if (memberName <= MemberName.ToolTip)
					{
						if (memberName == MemberName.StyleClass)
						{
							writer.Write(this.m_styleClass);
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Marker)
						{
							writer.Write(this.m_marker);
							continue;
						}
						switch (memberName)
						{
						case MemberName.DataLabel:
							writer.Write(this.m_dataLabel);
							continue;
						case MemberName.AxisLabel:
							writer.Write(this.m_axisLabel);
							continue;
						case MemberName.ChartItemInLegend:
							writer.Write(this.m_itemInLegend);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DataElementName)
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.DataElementName)
					{
						writer.Write(this.m_dataElementName);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataElementOutput)
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
						continue;
					}
					if (memberName == MemberName.DataPointValues)
					{
						writer.Write(this.m_dataPointValues);
						continue;
					}
					if (memberName == MemberName.CustomProperties)
					{
						writer.Write(this.m_customProperties);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000F6134 File Offset: 0x000F4334
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartItemInLegend)
				{
					if (memberName <= MemberName.ToolTip)
					{
						if (memberName == MemberName.StyleClass)
						{
							this.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Marker)
						{
							this.m_marker = (ChartMarker)reader.ReadRIFObject();
							continue;
						}
						switch (memberName)
						{
						case MemberName.DataLabel:
							this.m_dataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)reader.ReadRIFObject();
							continue;
						case MemberName.AxisLabel:
							this.m_axisLabel = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.ChartItemInLegend:
							this.m_itemInLegend = (ChartItemInLegend)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DataElementName)
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DataElementName)
					{
						this.m_dataElementName = reader.ReadString();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataElementOutput)
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.DataPointValues)
					{
						this.m_dataPointValues = (ChartDataPointValues)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.CustomProperties)
					{
						this.m_customProperties = reader.ReadListOfRIFObjects<DataValueList>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000F62E9 File Offset: 0x000F44E9
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000F62F3 File Offset: 0x000F44F3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000F62FC File Offset: 0x000F44FC
		internal void SetExprHost(ChartDataPointExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
			if (this.m_styleClass != null && this.m_exprHost.StyleHost != null)
			{
				this.m_exprHost.StyleHost.SetReportObjectModel(reportObjectModel);
				this.m_styleClass.SetStyleExprHost(this.m_exprHost.StyleHost);
			}
			if (this.m_marker != null && this.m_exprHost.ChartMarkerHost != null)
			{
				this.m_marker.SetExprHost(this.m_exprHost.ChartMarkerHost, reportObjectModel);
			}
			if (this.m_dataLabel != null && this.m_exprHost.DataLabelHost != null)
			{
				this.m_dataLabel.SetExprHost(this.m_exprHost.DataLabelHost, reportObjectModel);
			}
			if (this.m_itemInLegend != null && this.m_exprHost.DataPointInLegendHost != null)
			{
				this.m_itemInLegend.SetExprHost(this.m_exprHost.DataPointInLegendHost, reportObjectModel);
			}
			if (this.m_customProperties != null && this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
			base.BaseSetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x04001B3D RID: 6973
		private ChartDataPointValues m_dataPointValues;

		// Token: 0x04001B3E RID: 6974
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel m_dataLabel;

		// Token: 0x04001B3F RID: 6975
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001B40 RID: 6976
		private Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04001B41 RID: 6977
		private string m_dataElementName;

		// Token: 0x04001B42 RID: 6978
		private DataElementOutputTypes m_dataElementOutput = DataElementOutputTypes.ContentsOnly;

		// Token: 0x04001B43 RID: 6979
		private DataValueList m_customProperties;

		// Token: 0x04001B44 RID: 6980
		private ChartMarker m_marker;

		// Token: 0x04001B45 RID: 6981
		private ExpressionInfo m_axisLabel;

		// Token: 0x04001B46 RID: 6982
		private ChartItemInLegend m_itemInLegend;

		// Token: 0x04001B47 RID: 6983
		private ExpressionInfo m_toolTip;

		// Token: 0x04001B48 RID: 6984
		[NonSerialized]
		private ChartDataPointExprHost m_exprHost;

		// Token: 0x04001B49 RID: 6985
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001B4A RID: 6986
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint.GetDeclaration();

		// Token: 0x04001B4B RID: 6987
		[NonSerialized]
		private Formatter m_formatter;
	}
}
