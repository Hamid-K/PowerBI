using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Utils;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000575 RID: 1397
	internal sealed class SafeExpressionsRuntime
	{
		// Token: 0x060050B2 RID: 20658 RVA: 0x00152F01 File Offset: 0x00151101
		public SafeExpressionsRuntime(ObjectModelImpl reportObjectModel)
		{
			this.m_ExpressionEvaluator = new ExpressionEvaluator(new SafeExpressionsReportContext(reportObjectModel));
			this.m_PerformanceAggregator = new PerformanceAggregator();
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x00152F28 File Offset: 0x00151128
		public void EvaluateExpression(ExpressionInfo expressionInfo, ref VariantResult result)
		{
			try
			{
				ExpressionStructureInfo expressionStructureInfo = expressionInfo.ExpressionStructureInfo;
				if (expressionStructureInfo == null)
				{
					string expressionString = expressionInfo.GetExpressionString();
					expressionStructureInfo = new ExpressionStructureInfo(this.ParseExpressionString(expressionString));
					expressionInfo.ExpressionStructureInfo = expressionStructureInfo;
				}
				using (this.m_PerformanceAggregator.StartMeasurement(PerformanceMetricType.Evaluation))
				{
					result.Value = this.m_ExpressionEvaluator.Evaluate(expressionStructureInfo.ExpressionSyntax);
				}
			}
			catch (NotSupportedException ex)
			{
				RSTrace.SanitizedRdlEngineHostTracer.TraceException(TraceLevel.Warning, string.Format("SafeExpressionRuntime: Attempted evaluation of unsupported expression. {0}", ex));
				SyntaxTree syntaxTree = expressionInfo.ExpressionStructureInfo.ExpressionSyntax.SyntaxTree;
				this.TraceSyntaxTree(syntaxTree);
				this.SetResultToFailed(ref result, ex);
			}
			catch (Exception ex2)
			{
				this.SetResultToFailed(ref result, ex2);
			}
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00152FFC File Offset: 0x001511FC
		internal void LogMetrics()
		{
			try
			{
				foreach (PerformanceAggregatorSummary performanceAggregatorSummary in this.m_PerformanceAggregator.GetSummaryInMs())
				{
					RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Verbose, string.Format("SafeExpressionsRuntime: {0} expression {1} operation(s) took {2} ms with Avg {3} ms. Individual durations: Max {4} ms, Min {5}", new object[] { performanceAggregatorSummary.Count, performanceAggregatorSummary.MetricType, performanceAggregatorSummary.Sum, performanceAggregatorSummary.Avg, performanceAggregatorSummary.Max, performanceAggregatorSummary.Min }));
				}
			}
			catch (Exception ex)
			{
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, string.Format("Failed to log metrics in safe expression, exception: {0}", ex));
			}
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x001530E4 File Offset: 0x001512E4
		private ExpressionSyntax ParseExpressionString(string expressionString)
		{
			ExpressionSyntax expressionSyntax;
			using (this.m_PerformanceAggregator.StartMeasurement(PerformanceMetricType.Parsing))
			{
				expressionSyntax = this.m_ExpressionEvaluator.ParseExpression(expressionString);
			}
			return expressionSyntax;
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x00153128 File Offset: 0x00151328
		private void SetResultToFailed(ref VariantResult result, Exception exception)
		{
			result.Value = null;
			result.ErrorOccurred = true;
			result.ExceptionMessage = exception.Message;
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00153144 File Offset: 0x00151344
		private void TraceSyntaxTree(SyntaxTree syntaxTree)
		{
			try
			{
				SyntaxTreeSerializer syntaxTreeSerializer = new SyntaxTreeSerializer(1000);
				RSTrace.SanitizedRdlEngineHostTracer.TraceException(TraceLevel.Warning, "Syntax tree for errored expression: " + syntaxTreeSerializer.SerializeSyntaxTree(syntaxTree));
			}
			catch (Exception ex)
			{
				RSTrace.SanitizedRdlEngineHostTracer.TraceException(TraceLevel.Warning, string.Format("There was an error creating the syntax tree for the expression: {0}", ex));
			}
		}

		// Token: 0x040028B1 RID: 10417
		private const int MaxSyntaxNodeCountForTracing = 1000;

		// Token: 0x040028B2 RID: 10418
		private readonly ExpressionEvaluator m_ExpressionEvaluator;

		// Token: 0x040028B3 RID: 10419
		private readonly PerformanceAggregator m_PerformanceAggregator;
	}
}
