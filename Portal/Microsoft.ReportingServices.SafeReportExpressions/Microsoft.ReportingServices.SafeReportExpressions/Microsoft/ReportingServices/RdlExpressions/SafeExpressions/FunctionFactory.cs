using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000013 RID: 19
	internal sealed class FunctionFactory
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000298E File Offset: 0x00000B8E
		public FunctionFactory(ISafeExpressionsReportContext safeExpressionsReportContext)
		{
			BuiltInFunctions.Add(this, safeExpressionsReportContext);
			ExecutionFlowFunctions.Add(this);
			StringFunctions.Add(this);
			InspectionFunctions.Add(this);
			DateTimeFunctions.Add(this);
			MathFunctions.Add(this);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000029CB File Offset: 0x00000BCB
		public bool TryGet(string functionName, out IFunction function)
		{
			return this._functions.TryGetValue(functionName, out function);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000029DA File Offset: 0x00000BDA
		public bool Supports(string functionName)
		{
			return this._functions.ContainsKey(functionName);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000029E8 File Offset: 0x00000BE8
		public void Add(Type returnType, IArgumentValidator argumentValidator, IFunctionEvaluator evaluator)
		{
			Function function = new Function(returnType, argumentValidator, evaluator);
			this._functions.Add(argumentValidator.FunctionName, function);
		}

		// Token: 0x04000022 RID: 34
		private readonly Dictionary<string, IFunction> _functions = new Dictionary<string, IFunction>(StringComparer.OrdinalIgnoreCase);
	}
}
