using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001841 RID: 6209
	internal class PySparkExpressionOptimizer : PythonExpressionOptimizer
	{
		// Token: 0x0600CB67 RID: 52071 RVA: 0x002B72EC File Offset: 0x002B54EC
		private PySparkExpressionOptimizer(IPySparkTranslationOptions options = null)
			: base(null)
		{
			this._options = options ?? new PySparkTranslationConstraint();
		}

		// Token: 0x0600CB68 RID: 52072 RVA: 0x002B7305 File Offset: 0x002B5505
		public static PythonProgram Optimize(PythonProgram program, IPySparkTranslationOptions options = null)
		{
			return new PySparkExpressionOptimizer(options).OptimizeInternal(program);
		}

		// Token: 0x0600CB69 RID: 52073 RVA: 0x002B7314 File Offset: 0x002B5514
		private PythonProgram OptimizeInternal(PythonProgram program)
		{
			PythonProgram pythonProgram = program;
			if (this._options.PySparkOptimizations.HasFlag(PySparkOptimizations.UseInlineFunctions))
			{
				pythonProgram = PythonExpressionOptimizer.UseInlineFunctions(pythonProgram);
			}
			return pythonProgram;
		}

		// Token: 0x04004FD4 RID: 20436
		private readonly IPySparkTranslationOptions _options;
	}
}
