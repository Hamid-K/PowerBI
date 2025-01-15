using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200014E RID: 334
	internal sealed class DataTransformFunctionArgumentsBuilder
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x000335D0 File Offset: 0x000317D0
		internal DataTransformFunctionArgumentsBuilder(TranslationErrorContext errorContext, IQueryExpressionGenerator expressionGenerator)
		{
			this.m_errorContext = errorContext;
			this.m_expressionGenerator = expressionGenerator;
			this.m_arguments = new List<QueryExpression>();
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000335F1 File Offset: 0x000317F1
		internal void AddConventionalArguments(QueryTable inputTable, QueryTable inputRoleMapping, QueryTable outputRoleMapping)
		{
			this.m_arguments.Add(inputTable.Expression);
			this.m_arguments.Add(inputRoleMapping.Expression);
			this.m_arguments.Add(outputRoleMapping.Expression);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00033628 File Offset: 0x00031828
		internal void AddCustomArguments(IReadOnlyList<DataTransformParameter> transformParams, IReadOnlyList<IDaxDataTransformParameterMetadata> daxFunctionParams)
		{
			daxFunctionParams = daxFunctionParams ?? Util.EmptyReadOnlyCollection<IDaxDataTransformParameterMetadata>();
			transformParams = transformParams ?? Util.EmptyReadOnlyCollection<DataTransformParameter>();
			HashSet<int> hashSet = new HashSet<int>();
			foreach (IDaxDataTransformParameterMetadata daxDataTransformParameterMetadata in daxFunctionParams)
			{
				if (hashSet.Count == transformParams.Count)
				{
					break;
				}
				DataTransformParameter dataTransformParameter = DataTransformFunctionArgumentsBuilder.FindAndConsumeParameter(daxDataTransformParameterMetadata.Name, transformParams, hashSet);
				if (dataTransformParameter == null)
				{
					this.m_arguments.Add(DataTransformConstants.NullStringLiteral);
				}
				else
				{
					QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(dataTransformParameter.Value.ExpressionId.Value, dataTransformParameter.CreateValueExpressionContext(this.m_errorContext));
					this.m_arguments.Add(queryExpressionContext.QueryExpression);
				}
			}
			this.RegisterErrorsForUnconsumedParameters(transformParams, hashSet);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00033704 File Offset: 0x00031904
		private void RegisterErrorsForUnconsumedParameters(IReadOnlyList<DataTransformParameter> transformParams, HashSet<int> consumedTransformParams)
		{
			if (consumedTransformParams.Count == transformParams.Count)
			{
				return;
			}
			for (int i = 0; i < transformParams.Count; i++)
			{
				if (!consumedTransformParams.Contains(i))
				{
					DataTransformParameter dataTransformParameter = transformParams[i];
					this.m_errorContext.Register(TranslationMessages.UnexpectedDataTransformParameter(EngineMessageSeverity.Error, ObjectType.DataTransformParameter, dataTransformParameter.Id, "Value"));
				}
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00033760 File Offset: 0x00031960
		private static DataTransformParameter FindAndConsumeParameter(string name, IReadOnlyList<DataTransformParameter> transformParams, HashSet<int> consumedParams)
		{
			for (int i = 0; i < transformParams.Count; i++)
			{
				DataTransformParameter dataTransformParameter = transformParams[i];
				if (!consumedParams.Contains(i) && string.Equals(name, dataTransformParameter.Id.Value, StringComparison.OrdinalIgnoreCase))
				{
					consumedParams.Add(i);
					return dataTransformParameter;
				}
			}
			return null;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x000337AE File Offset: 0x000319AE
		internal IReadOnlyList<QueryExpression> ToList()
		{
			return this.m_arguments;
		}

		// Token: 0x04000636 RID: 1590
		internal const int InputTableArgumentIndex = 0;

		// Token: 0x04000637 RID: 1591
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000638 RID: 1592
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x04000639 RID: 1593
		private readonly List<QueryExpression> m_arguments;
	}
}
