using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Utils;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000578 RID: 1400
	internal class SafeExpressionsComparator
	{
		// Token: 0x060050BE RID: 20670 RVA: 0x0015331B File Offset: 0x0015151B
		public SafeExpressionsComparator(ISafeExpressionsReportContext safeExpressionsReportContext)
		{
			this._expressionEvaluator = new ExpressionEvaluator(safeExpressionsReportContext);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0015333C File Offset: 0x0015153C
		public SafeExpressionsComparisonResult Compare(ExpressionInfo expressionInfo, object legacyRuntimeResult)
		{
			try
			{
				string expressionString = expressionInfo.GetExpressionString();
				if (this.IsLimitOfComparisonsReached(expressionString))
				{
					return SafeExpressionsComparisonResult.LimitReached;
				}
				ExpressionSyntax expressionSyntax;
				if (expressionInfo.ExpressionStructureInfo != null)
				{
					if (expressionInfo.ExpressionStructureInfo.ExpressionSyntax == null)
					{
						return SafeExpressionsComparisonResult.NotSupported;
					}
					expressionSyntax = expressionInfo.ExpressionStructureInfo.ExpressionSyntax;
				}
				else
				{
					expressionSyntax = this._expressionEvaluator.ParseExpression(expressionString);
					if (!SafeExpressionsComparator.IsSupportedForSafeExpressions(this._expressionEvaluator, expressionSyntax))
					{
						expressionInfo.ExpressionStructureInfo = new ExpressionStructureInfo(null);
						return SafeExpressionsComparisonResult.NotSupported;
					}
					expressionInfo.ExpressionStructureInfo = new ExpressionStructureInfo(expressionSyntax);
				}
				if (this.ShouldSkipComparison(expressionSyntax))
				{
					RSTrace.SanitizedRdlEngineHostTracer.Trace("SERT: Skipping comparison of expression");
					return SafeExpressionsComparisonResult.Skipped;
				}
				object obj = null;
				bool flag = false;
				List<SyntaxNodeEvaluation> list;
				if (SafeExpressionsComparator.TryEvaluateExpression(this._expressionEvaluator, expressionSyntax, out list))
				{
					if (list.Count > 0)
					{
						obj = list[0].EvaluationResult.Value;
					}
					flag = SafeExpressionsComparator.CompareLegacyAndSafeExpressionsRuntimeResults(legacyRuntimeResult, obj);
				}
				if (!flag)
				{
					RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Info, "SERT: Mismatch in safe expression and legacy runtime results. Expression Syntax tree: " + SafeExpressionsComparator.SerializeSyntaxTree(expressionSyntax));
					SafeExpressionsComparator.TraceExpressionAndResultsWithCustomerContent(expressionInfo, legacyRuntimeResult, obj, list);
					return SafeExpressionsComparisonResult.Failure;
				}
			}
			catch (Exception ex)
			{
				string text = "<removed>";
				if (ex is NotImplementedException || ex is NotSupportedException)
				{
					text = ex.Message;
				}
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, string.Format("SERT: Unexpected exception thrown during expression parsing, ExceptionType: {0}, StackTrace: {1}, EnvStackTrace: {2}, Message: {3}", new object[]
				{
					ex.GetType(),
					ex.StackTrace,
					Environment.StackTrace,
					text
				}));
				SafeExpressionsComparator.TraceExceptionWithCustomerContent(ex);
				return SafeExpressionsComparisonResult.Failure;
			}
			return SafeExpressionsComparisonResult.Success;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x001534E4 File Offset: 0x001516E4
		private bool IsLimitOfComparisonsReached(string expressionString)
		{
			if (this.VisitedExpressions.Count > 100)
			{
				return true;
			}
			int num;
			if (this.VisitedExpressions.TryGetValue(expressionString, out num) && num > 15)
			{
				return true;
			}
			this.VisitedExpressions.AddOrUpdate(expressionString, 0, (string key, int oldvalue) => ++oldvalue);
			return false;
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x00153548 File Offset: 0x00151748
		private static string SerializeSyntaxTree(ExpressionSyntax expressionSyntax)
		{
			SyntaxTreeSerializer syntaxTreeSerializer = new SyntaxTreeSerializer(1000);
			try
			{
				return syntaxTreeSerializer.SerializeSyntaxTree(expressionSyntax.SyntaxTree);
			}
			catch (Exception ex)
			{
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, string.Format("SERT: Unexpected exception thrown during syntaxtree serialization, ExceptionType: {0}, StackTrace: {1}", ex.GetType(), ex.StackTrace));
			}
			return string.Empty;
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x001535AC File Offset: 0x001517AC
		private bool ShouldSkipComparison(ExpressionSyntax expressionSyntax)
		{
			return this.ShouldSkipSyntaxNode(expressionSyntax.SyntaxTree.GetRoot(default(CancellationToken)));
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x001535D4 File Offset: 0x001517D4
		private bool ShouldSkipSyntaxNode(SyntaxNode syntaxNode)
		{
			InvocationExpressionSyntax invocationExpressionSyntax = syntaxNode as InvocationExpressionSyntax;
			if (invocationExpressionSyntax != null)
			{
				IdentifierNameSyntax identifierNameSyntax = invocationExpressionSyntax.Expression as IdentifierNameSyntax;
				if (identifierNameSyntax != null)
				{
					string valueText = identifierNameSyntax.Identifier.ValueText;
					if (this.ShouldSkipFunction(valueText))
					{
						return true;
					}
				}
			}
			IdentifierNameSyntax identifierNameSyntax2 = syntaxNode as IdentifierNameSyntax;
			if (identifierNameSyntax2 != null)
			{
				string valueText2 = identifierNameSyntax2.Identifier.ValueText;
				if (this.ShouldSkipFunction(valueText2))
				{
					return true;
				}
			}
			IEnumerable<SyntaxNode> enumerable = syntaxNode.ChildNodes();
			if (enumerable.Any<SyntaxNode>())
			{
				foreach (SyntaxNode syntaxNode2 in enumerable)
				{
					if (this.ShouldSkipSyntaxNode(syntaxNode2))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0015369C File Offset: 0x0015189C
		private bool ShouldSkipFunction(string functionName)
		{
			string text = functionName.ToUpperInvariant();
			return text == "NOW" || text == "TIMEOFDAY" || text == "TODAY";
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x001536DC File Offset: 0x001518DC
		private static bool CompareLegacyAndSafeExpressionsRuntimeResults(object legacyRuntimeResult, object safeExpressionsRuntimeResult)
		{
			SafeExpressionsAndLegacyComparison safeExpressionsAndLegacyComparison = new SafeExpressionsAndLegacyComparison(safeExpressionsRuntimeResult, legacyRuntimeResult);
			bool flag = false;
			try
			{
				flag = safeExpressionsAndLegacyComparison.CompareExpressionResults();
				if (!flag)
				{
					RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Info, "SERT: Mismatch details for safe expression and legacy runtime results comparison: " + safeExpressionsAndLegacyComparison.MismatchDetail);
				}
			}
			catch (Exception ex)
			{
				string text = "<removed>";
				if (ex is InvalidOperationException)
				{
					text = ex.Message;
				}
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, string.Format("SERT: Unexpected exception thrown during expression result comparision, ExceptionType: {0}, StackTrace: {1}, EnvStackTrace: {2}, Message: {3}", new object[]
				{
					ex.GetType(),
					ex.StackTrace,
					Environment.StackTrace,
					text
				}));
				SafeExpressionsComparator.TraceExceptionWithCustomerContent(ex);
			}
			return flag;
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00153784 File Offset: 0x00151984
		private static bool TryEvaluateExpression(ExpressionEvaluator expressionEvaluator, ExpressionSyntax expressionSyntax, out List<SyntaxNodeEvaluation> safeExpressionsNodeResults)
		{
			safeExpressionsNodeResults = null;
			try
			{
				expressionEvaluator.EvaluateAndCollectNodeEvaluations(expressionSyntax, out safeExpressionsNodeResults);
				return true;
			}
			catch (Exception ex)
			{
				string text = "<removed>";
				if (ex is NotImplementedException || ex is NotSupportedException)
				{
					text = ex.Message;
				}
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, string.Format("SERT: Unexpected exception thrown during safe expression runtime evaluation, ExceptionType: {0}, StackTrace: {1}, EnvStackTrace: {2}, Expression Syntax tree: {3}, Message: {4}", new object[]
				{
					ex.GetType(),
					ex.StackTrace,
					Environment.StackTrace,
					SafeExpressionsComparator.SerializeSyntaxTree(expressionSyntax),
					text
				}));
				SafeExpressionsComparator.TraceExceptionWithCustomerContent(ex);
			}
			return false;
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x0015381C File Offset: 0x00151A1C
		private static bool IsSupportedForSafeExpressions(ExpressionEvaluator expressionEvaluator, ExpressionSyntax expressionSyntax)
		{
			try
			{
				return expressionEvaluator.Validate(expressionSyntax).Supported;
			}
			catch (Exception ex)
			{
				string text = "<removed>";
				if (ex is NotImplementedException || ex is NotSupportedException)
				{
					text = ex.Message;
				}
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, string.Format("SERT: Unexpected exception thrown during safe expression runtime validation, ExceptionType: {0}, StackTrace: {1}, EnvStackTrace: {2}, Expression Syntax tree: {3}, Message: {4}", new object[]
				{
					ex.GetType(),
					ex.StackTrace,
					Environment.StackTrace,
					SafeExpressionsComparator.SerializeSyntaxTree(expressionSyntax),
					text
				}));
				SafeExpressionsComparator.TraceExceptionWithCustomerContent(ex);
			}
			return false;
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x001538B8 File Offset: 0x00151AB8
		private static void TraceExceptionWithCustomerContent(Exception ex)
		{
			string text = string.Format("SERT: Unexpected exception thrown during expression result comparision, Exception: {0}", ex);
			if (ex.InnerException != null)
			{
				text += string.Format(", InnerException:{0}", ex.InnerException);
			}
			SafeExpressionsComparator.TraceCustomerContent(text);
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x001538F8 File Offset: 0x00151AF8
		private static void TraceExpressionAndResultsWithCustomerContent(ExpressionInfo expressionInfo, object legacyRuntimeResult, object safeExpressionsRuntimeResult, List<SyntaxNodeEvaluation> safeExpressionsNodeResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Expression => '" + expressionInfo.GetExpressionString() + "'");
			stringBuilder.AppendLine(string.Format("LegacyRuntime result => '{0}'", legacyRuntimeResult));
			stringBuilder.AppendLine(string.Format("SERT result => '{0}'", safeExpressionsRuntimeResult));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("SERT node evaluations:");
			if (safeExpressionsNodeResults != null)
			{
				foreach (SyntaxNodeEvaluation syntaxNodeEvaluation in safeExpressionsNodeResults)
				{
					stringBuilder.AppendLine(syntaxNodeEvaluation.ToString());
				}
			}
			SafeExpressionsComparator.TraceCustomerContent(stringBuilder.ToString());
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x001539B4 File Offset: 0x00151BB4
		private static void TraceCustomerContent(string traceContent)
		{
			if (ProcessingContext.ReqContext == null)
			{
				RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Warning, "SERT: Unable to trace customer content. ProcessingContext.ReqContext is null.");
				return;
			}
			ProcessingContext.ReqContext.TraceCustomerContentByHost(traceContent);
		}

		// Token: 0x040028B5 RID: 10421
		private readonly ExpressionEvaluator _expressionEvaluator;

		// Token: 0x040028B6 RID: 10422
		private const int MaxSyntaxNodeCountForTracing = 1000;

		// Token: 0x040028B7 RID: 10423
		private const int ComparisonLimitPerExpression = 15;

		// Token: 0x040028B8 RID: 10424
		private const int ComparisonLimitPerReport = 100;

		// Token: 0x040028B9 RID: 10425
		private const string RemovedTag = "<removed>";

		// Token: 0x040028BA RID: 10426
		private ConcurrentDictionary<string, int> VisitedExpressions = new ConcurrentDictionary<string, int>();
	}
}
