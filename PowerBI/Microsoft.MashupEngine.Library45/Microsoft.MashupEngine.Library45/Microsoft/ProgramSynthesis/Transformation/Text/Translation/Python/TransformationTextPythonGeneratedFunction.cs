using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DAF RID: 7599
	internal class TransformationTextPythonGeneratedFunction : PythonGeneratedFunction, ITransformationTextGeneratedFunction, IGeneratedFunction
	{
		// Token: 0x17002A72 RID: 10866
		// (get) Token: 0x0600FEE3 RID: 65251 RVA: 0x00367390 File Offset: 0x00365590
		public IEnumerable<string> UsedColumns { get; }

		// Token: 0x17002A73 RID: 10867
		// (get) Token: 0x0600FEE4 RID: 65252 RVA: 0x00367398 File Offset: 0x00365598
		public IReadOnlyList<Record<GeneratedFunction, GeneratedFunction>> TranslatedBranches { get; }

		// Token: 0x17002A74 RID: 10868
		// (get) Token: 0x0600FEE5 RID: 65253 RVA: 0x003673A0 File Offset: 0x003655A0
		public IReadOnlyList<SSAStep> BeforeBranchesSsaSequence { get; }

		// Token: 0x0600FEE6 RID: 65254 RVA: 0x003673A8 File Offset: 0x003655A8
		public TransformationTextPythonGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, IEnumerable<string> usedColumns, IEnumerable<SSAStep> ssaSequence = null, IReadOnlyList<Record<GeneratedFunction, GeneratedFunction>> translatedBranches = null, IReadOnlyList<SSAStep> beforeBranchesSsaSequence = null)
			: base(parameters, returnType, ssaSequence)
		{
			this.UsedColumns = usedColumns;
			this.TranslatedBranches = translatedBranches;
			this.BeforeBranchesSsaSequence = beforeBranchesSsaSequence;
		}

		// Token: 0x0600FEE7 RID: 65255 RVA: 0x003673CC File Offset: 0x003655CC
		public override void SetUseClass(bool val)
		{
			base.SetUseClass(val);
			if (this.TranslatedBranches == null)
			{
				return;
			}
			foreach (Record<GeneratedFunction, GeneratedFunction> record in this.TranslatedBranches)
			{
				PythonGeneratedFunction pythonGeneratedFunction = record.Item1 as PythonGeneratedFunction;
				if (pythonGeneratedFunction != null)
				{
					pythonGeneratedFunction.SetUseClass(val);
				}
				PythonGeneratedFunction pythonGeneratedFunction2 = record.Item2 as PythonGeneratedFunction;
				if (pythonGeneratedFunction2 != null)
				{
					pythonGeneratedFunction2.SetUseClass(val);
				}
			}
		}

		// Token: 0x0600FEE8 RID: 65256 RVA: 0x0036744C File Offset: 0x0036564C
		private CodeForGeneratedFunction GenerateSubFunction(OptimizeFor optimization, string headerModuleName, string name, string parameterString, GeneratedFunction functionBody)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			CodeForGeneratedFunction codeForGeneratedFunction = ((functionBody != null) ? functionBody.GenerateCode(headerModuleName, optimization) : null);
			if (codeForGeneratedFunction == null)
			{
				CodeBuilder codeBuilder2 = new CodeBuilder(4U);
				codeBuilder2.AppendLine("return True");
				codeForGeneratedFunction = new CodeForGeneratedFunction(new CodeBuilder(4U), codeBuilder2);
			}
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}({1}):", new object[] { name, parameterString })), 1U))
			{
				codeBuilder.Append(codeForGeneratedFunction.DynamicCode);
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
			}
			return new CodeForGeneratedFunction(codeForGeneratedFunction.StaticCode, codeBuilder);
		}

		// Token: 0x0600FEE9 RID: 65257 RVA: 0x003674FC File Offset: 0x003656FC
		public override CodeForGeneratedFunction GenerateCode(string headerModuleName, OptimizeFor optimization)
		{
			if (this.TranslatedBranches == null)
			{
				return base.GenerateCode(headerModuleName, optimization);
			}
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			CodeBuilder codeBuilder2 = new CodeBuilder(4U);
			int num = 0;
			bool flag = false;
			IReadOnlyList<SSAStep> beforeBranchesSsaSequence = this.BeforeBranchesSsaSequence;
			if (beforeBranchesSsaSequence != null && beforeBranchesSsaSequence.Any<SSAStep>())
			{
				PythonGeneratedFunction pythonGeneratedFunction = new PythonGeneratedFunction(new Record<string, Type>[0], typeof(void), this.BeforeBranchesSsaSequence);
				pythonGeneratedFunction.SetUseClass(base._useClass);
				codeBuilder.Append(pythonGeneratedFunction.GenerateCode(headerModuleName, OptimizeFor.Nothing).DynamicCode);
			}
			foreach (Record<GeneratedFunction, GeneratedFunction> record in this.TranslatedBranches)
			{
				GeneratedFunction item = record.Item1;
				if (item != null)
				{
					item.Optimize(Inliner.Instance);
				}
				GeneratedFunction item2 = record.Item1;
				CodeForGeneratedFunction codeForGeneratedFunction = ((item2 != null) ? item2.GenerateCode(headerModuleName, optimization) : null);
				CodeForGeneratedFunction codeForGeneratedFunction2 = record.Item2.GenerateCode(headerModuleName, optimization);
				if (record.Item1 == null)
				{
					codeBuilder.AppendLine("else:");
					flag = true;
				}
				else
				{
					codeBuilder2.Append(codeForGeneratedFunction.StaticCode);
					string[] array = codeForGeneratedFunction.DynamicCode.GetCode().Trim().Split(new char[] { '\n' });
					for (int i = 0; i < array.Length - 1; i++)
					{
						codeBuilder.AppendLine(array[i]);
					}
					string text = ((num > 0 && array.Length == 1) ? "elif" : "if");
					string text2 = array[array.Length - 1].Trim().Substring("return ".Length);
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} {1}:", new object[] { text, text2 })));
				}
				using (codeBuilder.NewScope(null, 1U))
				{
					codeBuilder2.Append(codeForGeneratedFunction2.StaticCode);
					codeBuilder.Append(codeForGeneratedFunction2.DynamicCode);
				}
				num++;
			}
			if (!flag)
			{
				using (codeBuilder.NewScope("else:", 1U))
				{
					codeBuilder.AppendLine("return None");
				}
			}
			return new CodeForGeneratedFunction(codeBuilder2, codeBuilder);
		}
	}
}
