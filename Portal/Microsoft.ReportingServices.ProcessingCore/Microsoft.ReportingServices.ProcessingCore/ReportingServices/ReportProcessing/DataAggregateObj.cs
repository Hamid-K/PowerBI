using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200067A RID: 1658
	internal sealed class DataAggregateObj : IErrorContext
	{
		// Token: 0x06005B02 RID: 23298 RVA: 0x001762B4 File Offset: 0x001744B4
		internal DataAggregateObj(DataAggregateInfo aggInfo, ReportProcessing.ProcessingContext processingContext)
		{
			this.m_nonAggregateMode = false;
			this.m_name = aggInfo.Name;
			this.m_duplicateNames = aggInfo.DuplicateNames;
			switch (aggInfo.AggregateType)
			{
			case DataAggregateInfo.AggregateTypes.First:
				this.m_aggregator = new First();
				break;
			case DataAggregateInfo.AggregateTypes.Last:
				this.m_aggregator = new Last();
				break;
			case DataAggregateInfo.AggregateTypes.Sum:
				this.m_aggregator = new Sum();
				break;
			case DataAggregateInfo.AggregateTypes.Avg:
				this.m_aggregator = new Avg();
				break;
			case DataAggregateInfo.AggregateTypes.Max:
				this.m_aggregator = new Max(processingContext.CompareInfo, processingContext.ClrCompareOptions);
				break;
			case DataAggregateInfo.AggregateTypes.Min:
				this.m_aggregator = new Min(processingContext.CompareInfo, processingContext.ClrCompareOptions);
				break;
			case DataAggregateInfo.AggregateTypes.CountDistinct:
				this.m_aggregator = new CountDistinct();
				break;
			case DataAggregateInfo.AggregateTypes.CountRows:
				this.m_aggregator = new CountRows();
				break;
			case DataAggregateInfo.AggregateTypes.Count:
				this.m_aggregator = new Count();
				break;
			case DataAggregateInfo.AggregateTypes.StDev:
				this.m_aggregator = new StDev();
				break;
			case DataAggregateInfo.AggregateTypes.Var:
				this.m_aggregator = new Var();
				break;
			case DataAggregateInfo.AggregateTypes.StDevP:
				this.m_aggregator = new StDevP();
				break;
			case DataAggregateInfo.AggregateTypes.VarP:
				this.m_aggregator = new VarP();
				break;
			case DataAggregateInfo.AggregateTypes.Aggregate:
				this.m_aggregator = new Aggregate();
				break;
			case DataAggregateInfo.AggregateTypes.Previous:
				this.m_aggregator = new Previous();
				break;
			default:
				Global.Tracer.Assert(false, "Unsupport aggregate type.");
				break;
			}
			this.m_aggregateDef = aggInfo;
			this.m_reportRT = processingContext.ReportRuntime;
			if (this.m_reportRT.ReportExprHost != null)
			{
				this.m_aggregateDef.SetExprHosts(this.m_reportRT.ReportExprHost, processingContext.ReportObjectModel);
			}
			this.m_aggregateResult = default(DataAggregateObjResult);
			this.Init();
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x0017647F File Offset: 0x0017467F
		internal DataAggregateObj(DataAggregateInfo aggrDef, DataAggregateObjResult aggrResult)
		{
			this.m_nonAggregateMode = true;
			this.m_aggregateDef = aggrDef;
			this.m_aggregateResult = aggrResult;
		}

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0017649C File Offset: 0x0017469C
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x06005B05 RID: 23301 RVA: 0x001764A4 File Offset: 0x001746A4
		internal StringList DuplicateNames
		{
			get
			{
				return this.m_duplicateNames;
			}
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x001764AC File Offset: 0x001746AC
		internal bool NonAggregateMode
		{
			get
			{
				return this.m_nonAggregateMode;
			}
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x06005B07 RID: 23303 RVA: 0x001764B4 File Offset: 0x001746B4
		internal DataAggregateInfo AggregateDef
		{
			get
			{
				return this.m_aggregateDef;
			}
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x001764BC File Offset: 0x001746BC
		// (set) Token: 0x06005B09 RID: 23305 RVA: 0x001764C4 File Offset: 0x001746C4
		internal bool UsedInExpression
		{
			get
			{
				return this.m_usedInExpression;
			}
			set
			{
				this.m_usedInExpression = value;
			}
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x001764CD File Offset: 0x001746CD
		internal void Init()
		{
			if (this.m_nonAggregateMode)
			{
				return;
			}
			this.m_aggregator.Init();
			this.m_aggregateResult = default(DataAggregateObjResult);
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x001764F0 File Offset: 0x001746F0
		internal void Update()
		{
			if (this.m_aggregateResult.ErrorOccurred || this.m_nonAggregateMode)
			{
				return;
			}
			if (this.m_aggregateDef.FieldsUsedInValueExpression == null)
			{
				this.m_reportRT.ReportObjectModel.FieldsImpl.ResetUsedInExpression();
			}
			object[] array;
			DataFieldStatus dataFieldStatus;
			this.m_aggregateResult.ErrorOccurred = this.EvaluateParameters(out array, out dataFieldStatus);
			if (dataFieldStatus != DataFieldStatus.None)
			{
				this.m_aggregateResult.HasCode = true;
				this.m_aggregateResult.FieldStatus = dataFieldStatus;
			}
			if (this.m_aggregateDef.FieldsUsedInValueExpression == null)
			{
				this.m_aggregateDef.FieldsUsedInValueExpression = new List<string>();
				this.m_reportRT.ReportObjectModel.FieldsImpl.AddFieldsUsedInExpression(this.m_aggregateDef.FieldsUsedInValueExpression);
			}
			if (this.m_aggregateResult.ErrorOccurred)
			{
				return;
			}
			try
			{
				this.m_aggregator.Update(array, this);
			}
			catch (ReportProcessingException)
			{
				this.m_aggregateResult.ErrorOccurred = true;
			}
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x001765E0 File Offset: 0x001747E0
		internal DataAggregateObjResult AggregateResult()
		{
			if (!this.m_nonAggregateMode && !this.m_aggregateResult.ErrorOccurred)
			{
				try
				{
					this.m_aggregateResult.Value = this.m_aggregator.Result();
				}
				catch (ReportProcessingException)
				{
					this.m_aggregateResult.ErrorOccurred = true;
					this.m_aggregateResult.Value = null;
				}
			}
			return this.m_aggregateResult;
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x0017664C File Offset: 0x0017484C
		internal bool EvaluateParameters(out object[] values, out DataFieldStatus fieldStatus)
		{
			bool flag = false;
			fieldStatus = DataFieldStatus.None;
			values = new object[this.m_aggregateDef.Expressions.Length];
			for (int i = 0; i < this.m_aggregateDef.Expressions.Length; i++)
			{
				VariantResult variantResult = this.m_reportRT.EvaluateAggregateVariantOrBinaryParamExpr(this.m_aggregateDef, i, this);
				values[i] = variantResult.Value;
				flag |= variantResult.ErrorOccurred;
				if (variantResult.FieldStatus != DataFieldStatus.None)
				{
					fieldStatus = variantResult.FieldStatus;
				}
			}
			return flag;
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x001766C2 File Offset: 0x001748C2
		internal void Set(DataAggregateObjResult aggregateResult)
		{
			this.m_nonAggregateMode = true;
			this.m_aggregateResult = aggregateResult;
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x001766D2 File Offset: 0x001748D2
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, params string[] arguments)
		{
			if (!this.m_aggregateResult.HasCode)
			{
				this.m_aggregateResult.HasCode = true;
				this.m_aggregateResult.Code = code;
				this.m_aggregateResult.Severity = severity;
				this.m_aggregateResult.Arguments = arguments;
			}
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x00176711 File Offset: 0x00174911
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			if (!this.m_aggregateResult.HasCode)
			{
				this.m_aggregateResult.HasCode = true;
				this.m_aggregateResult.Code = code;
				this.m_aggregateResult.Severity = severity;
				this.m_aggregateResult.Arguments = arguments;
			}
		}

		// Token: 0x04002F4E RID: 12110
		private bool m_nonAggregateMode;

		// Token: 0x04002F4F RID: 12111
		private string m_name;

		// Token: 0x04002F50 RID: 12112
		private StringList m_duplicateNames;

		// Token: 0x04002F51 RID: 12113
		private DataAggregate m_aggregator;

		// Token: 0x04002F52 RID: 12114
		private DataAggregateInfo m_aggregateDef;

		// Token: 0x04002F53 RID: 12115
		private ReportRuntime m_reportRT;

		// Token: 0x04002F54 RID: 12116
		private bool m_usedInExpression;

		// Token: 0x04002F55 RID: 12117
		private DataAggregateObjResult m_aggregateResult;
	}
}
