using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002D1 RID: 721
	public class ConstantExpressionEvaluator : IOptimizer
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		public static ConstantExpressionEvaluator Instance { get; } = new ConstantExpressionEvaluator();

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002D7EF File Offset: 0x0002B9EF
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			return steps.Select((SSAStep x) => this.EvaluateRhs(x)).ToList<SSAStep>();
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0002D808 File Offset: 0x0002BA08
		private SSAStep EvaluateRhs(SSAStep ssaStep)
		{
			SSARValue rvalue = ssaStep.RValue;
			SSARValue ssarvalue = (SSARValue)this.ConstantExpressionEvaluatorRec(rvalue);
			if (ssarvalue == rvalue)
			{
				return ssaStep;
			}
			ssaStep.ReplaceRValueInPlace(rvalue, ssarvalue);
			rvalue.Delete();
			return ssaStep;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0002D840 File Offset: 0x0002BA40
		private SSAValue ConstantExpressionEvaluatorRec(SSAValue expr)
		{
			SSAFunctionApplication ssafunctionApplication = expr as SSAFunctionApplication;
			if (ssafunctionApplication == null)
			{
				return expr;
			}
			IReadOnlyList<SSAValue> functionArguments = ssafunctionApplication.FunctionArguments;
			List<SSAValue> list = functionArguments.Select((SSAValue x) => this.ConstantExpressionEvaluatorRec(x)).ToList<SSAValue>();
			SSAFunctionApplication ssafunctionApplication2;
			if (list.Zip(functionArguments, (SSAValue x, SSAValue y) => x == y).All((bool b) => b))
			{
				ssafunctionApplication2 = ssafunctionApplication;
			}
			else
			{
				string functionName = ssafunctionApplication.FunctionName;
				ssafunctionApplication2 = new SSAFunctionApplication(ssafunctionApplication.ValueType, functionName, list, ssafunctionApplication.IsFunctionLocal);
				ssafunctionApplication.Delete();
			}
			return this.ConstantExpressionEvaluatorBase(ssafunctionApplication2);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002D8F2 File Offset: 0x0002BAF2
		private SSARValue ConstantExpressionEvaluatorBase(SSAFunctionApplication app)
		{
			return PythonCodeUtils.PythonConstantExpressionEvaluator(app);
		}
	}
}
