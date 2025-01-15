using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000701 RID: 1793
	[Serializable]
	internal sealed class ChartDataPoint : IActionOwner
	{
		// Token: 0x060063F6 RID: 25590 RVA: 0x0018CD6E File Offset: 0x0018AF6E
		internal ChartDataPoint()
		{
			this.m_dataValues = new ExpressionInfoList();
		}

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x0018CD88 File Offset: 0x0018AF88
		// (set) Token: 0x060063F8 RID: 25592 RVA: 0x0018CD90 File Offset: 0x0018AF90
		internal ExpressionInfoList DataValues
		{
			get
			{
				return this.m_dataValues;
			}
			set
			{
				this.m_dataValues = value;
			}
		}

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x060063F9 RID: 25593 RVA: 0x0018CD99 File Offset: 0x0018AF99
		// (set) Token: 0x060063FA RID: 25594 RVA: 0x0018CDA1 File Offset: 0x0018AFA1
		internal ChartDataLabel DataLabel
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

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x060063FB RID: 25595 RVA: 0x0018CDAA File Offset: 0x0018AFAA
		// (set) Token: 0x060063FC RID: 25596 RVA: 0x0018CDB2 File Offset: 0x0018AFB2
		internal Microsoft.ReportingServices.ReportProcessing.Action Action
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

		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x060063FD RID: 25597 RVA: 0x0018CDBB File Offset: 0x0018AFBB
		// (set) Token: 0x060063FE RID: 25598 RVA: 0x0018CDC3 File Offset: 0x0018AFC3
		internal Style StyleClass
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

		// Token: 0x17002362 RID: 9058
		// (get) Token: 0x060063FF RID: 25599 RVA: 0x0018CDCC File Offset: 0x0018AFCC
		// (set) Token: 0x06006400 RID: 25600 RVA: 0x0018CDD4 File Offset: 0x0018AFD4
		internal ChartDataPoint.MarkerTypes MarkerType
		{
			get
			{
				return this.m_markerType;
			}
			set
			{
				this.m_markerType = value;
			}
		}

		// Token: 0x17002363 RID: 9059
		// (get) Token: 0x06006401 RID: 25601 RVA: 0x0018CDDD File Offset: 0x0018AFDD
		// (set) Token: 0x06006402 RID: 25602 RVA: 0x0018CDE5 File Offset: 0x0018AFE5
		internal string MarkerSize
		{
			get
			{
				return this.m_markerSize;
			}
			set
			{
				this.m_markerSize = value;
			}
		}

		// Token: 0x17002364 RID: 9060
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x0018CDEE File Offset: 0x0018AFEE
		// (set) Token: 0x06006404 RID: 25604 RVA: 0x0018CDF6 File Offset: 0x0018AFF6
		internal Style MarkerStyleClass
		{
			get
			{
				return this.m_markerStyleClass;
			}
			set
			{
				this.m_markerStyleClass = value;
			}
		}

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x06006405 RID: 25605 RVA: 0x0018CDFF File Offset: 0x0018AFFF
		// (set) Token: 0x06006406 RID: 25606 RVA: 0x0018CE07 File Offset: 0x0018B007
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

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x06006407 RID: 25607 RVA: 0x0018CE10 File Offset: 0x0018B010
		// (set) Token: 0x06006408 RID: 25608 RVA: 0x0018CE18 File Offset: 0x0018B018
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

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x06006409 RID: 25609 RVA: 0x0018CE21 File Offset: 0x0018B021
		// (set) Token: 0x0600640A RID: 25610 RVA: 0x0018CE29 File Offset: 0x0018B029
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x0018CE32 File Offset: 0x0018B032
		// (set) Token: 0x0600640C RID: 25612 RVA: 0x0018CE3A File Offset: 0x0018B03A
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

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x0600640D RID: 25613 RVA: 0x0018CE43 File Offset: 0x0018B043
		internal ChartDataPointExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x0600640E RID: 25614 RVA: 0x0018CE4B File Offset: 0x0018B04B
		Microsoft.ReportingServices.ReportProcessing.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x0018CE53 File Offset: 0x0018B053
		// (set) Token: 0x06006410 RID: 25616 RVA: 0x0018CE5B File Offset: 0x0018B05B
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

		// Token: 0x06006411 RID: 25617 RVA: 0x0018CE64 File Offset: 0x0018B064
		internal void Initialize(InitializationContext context)
		{
			ExprHostBuilder exprHostBuilder = context.ExprHostBuilder;
			exprHostBuilder.ChartDataPointStart();
			for (int i = 0; i < this.m_dataValues.Count; i++)
			{
				this.m_dataValues[i].Initialize("DataPoint", context);
				exprHostBuilder.ChartDataPointDataValue(this.m_dataValues[i]);
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
			if (this.m_markerStyleClass != null)
			{
				exprHostBuilder.DataPointMarkerStyleStart();
				this.m_markerStyleClass.Initialize(context);
				exprHostBuilder.DataPointMarkerStyleEnd();
			}
			if (this.m_markerSize != null)
			{
				double num = context.ValidateSize(this.m_markerSize, "MarkerSize");
				this.m_markerSize = Converter.ConvertSize(num);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, true, context);
			}
			this.DataRendererInitialize(context);
			this.m_exprHostID = exprHostBuilder.ChartDataPointEnd();
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x0018CF76 File Offset: 0x0018B176
		internal void DataRendererInitialize(InitializationContext context)
		{
			CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, "Value", context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x0018CFA4 File Offset: 0x0018B1A4
		internal void SetExprHost(ChartDataPointExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_action != null)
			{
				if (this.m_exprHost.ActionInfoHost != null)
				{
					this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
				}
				else if (this.m_exprHost.ActionHost != null)
				{
					this.m_action.SetExprHost(this.m_exprHost.ActionHost, reportObjectModel);
				}
			}
			if (this.m_styleClass != null && this.m_exprHost.StyleHost != null)
			{
				this.m_exprHost.StyleHost.SetReportObjectModel(reportObjectModel);
				this.m_styleClass.SetStyleExprHost(this.m_exprHost.StyleHost);
			}
			if (this.m_markerStyleClass != null && this.m_exprHost.MarkerStyleHost != null)
			{
				this.m_exprHost.MarkerStyleHost.SetReportObjectModel(reportObjectModel);
				this.m_markerStyleClass.SetStyleExprHost(this.m_exprHost.MarkerStyleHost);
			}
			if (this.m_dataLabel != null && this.m_dataLabel.StyleClass != null && this.m_exprHost.DataLabelStyleHost != null)
			{
				this.m_dataLabel.SetExprHost(this.m_exprHost.DataLabelStyleHost, reportObjectModel);
			}
			if (this.m_customProperties != null && this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x0018D108 File Offset: 0x0018B308
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.DataValues, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.DataLabel, ObjectType.ChartDataLabel),
				new MemberInfo(MemberName.Action, ObjectType.Action),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.MarkerType, Token.Enum),
				new MemberInfo(MemberName.MarkerSize, Token.String),
				new MemberInfo(MemberName.MarkerStyleClass, ObjectType.Style),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.CustomProperties, ObjectType.DataValueList)
			});
		}

		// Token: 0x0400322E RID: 12846
		private ExpressionInfoList m_dataValues;

		// Token: 0x0400322F RID: 12847
		private ChartDataLabel m_dataLabel;

		// Token: 0x04003230 RID: 12848
		private Microsoft.ReportingServices.ReportProcessing.Action m_action;

		// Token: 0x04003231 RID: 12849
		private Style m_styleClass;

		// Token: 0x04003232 RID: 12850
		private ChartDataPoint.MarkerTypes m_markerType;

		// Token: 0x04003233 RID: 12851
		private string m_markerSize;

		// Token: 0x04003234 RID: 12852
		private Style m_markerStyleClass;

		// Token: 0x04003235 RID: 12853
		private string m_dataElementName;

		// Token: 0x04003236 RID: 12854
		private DataElementOutputTypes m_dataElementOutput;

		// Token: 0x04003237 RID: 12855
		private int m_exprHostID = -1;

		// Token: 0x04003238 RID: 12856
		private DataValueList m_customProperties;

		// Token: 0x04003239 RID: 12857
		[NonSerialized]
		private ChartDataPointExprHost m_exprHost;

		// Token: 0x0400323A RID: 12858
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x02000CD1 RID: 3281
		internal enum MarkerTypes
		{
			// Token: 0x04004ED9 RID: 20185
			None,
			// Token: 0x04004EDA RID: 20186
			Square,
			// Token: 0x04004EDB RID: 20187
			Circle,
			// Token: 0x04004EDC RID: 20188
			Diamond,
			// Token: 0x04004EDD RID: 20189
			Triangle,
			// Token: 0x04004EDE RID: 20190
			Cross,
			// Token: 0x04004EDF RID: 20191
			Auto
		}
	}
}
