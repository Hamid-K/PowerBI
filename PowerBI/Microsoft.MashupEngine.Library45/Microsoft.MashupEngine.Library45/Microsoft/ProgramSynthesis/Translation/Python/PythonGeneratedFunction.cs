using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000321 RID: 801
	public class PythonGeneratedFunction : SSAGeneratedFunction
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00034224 File Offset: 0x00032424
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0003422C File Offset: 0x0003242C
		protected bool _useClass { get; set; }

		// Token: 0x060011B0 RID: 4528 RVA: 0x00034235 File Offset: 0x00032435
		public PythonGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, IEnumerable<SSAStep> ssaSequence = null)
			: base(parameters, returnType, ssaSequence)
		{
			this._useClass = true;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00034247 File Offset: 0x00032447
		public virtual void SetUseClass(bool val)
		{
			this._useClass = val;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00034250 File Offset: 0x00032450
		public override CodeForGeneratedFunction GenerateCode(string headerModuleName, OptimizeFor optimization)
		{
			string text = (this._useClass ? "self." : "");
			ConstantLifter constantLifter = new ConstantLifter();
			List<SSAStep> list = constantLifter.Optimize(base.SSASequence, optimization);
			IReadOnlyList<SSAStep> constantDefinitions = constantLifter.ConstantDefinitions;
			HashSet<SSARegister> hashSet = new HashSet<SSARegister>();
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			foreach (SSAStep ssastep in constantDefinitions)
			{
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}{1} = {2}", new object[]
				{
					text,
					ssastep.LValue.ToPython(headerModuleName, null, this._useClass),
					ssastep.RValue.ToPython(headerModuleName, hashSet, this._useClass)
				})));
				if (this._useClass)
				{
					hashSet.Add(ssastep.LValue);
				}
			}
			CodeBuilder codeBuilder2 = new CodeBuilder(4U);
			bool flag = base.ReturnType == typeof(void);
			IEnumerable<SSAStep> enumerable;
			if (!flag)
			{
				enumerable = list.DropLast<SSAStep>();
			}
			else
			{
				IEnumerable<SSAStep> enumerable2 = list;
				enumerable = enumerable2;
			}
			foreach (SSAStep ssastep2 in enumerable)
			{
				string text2 = FormattableString.Invariant(FormattableStringFactory.Create("{0} = {1}", new object[]
				{
					ssastep2.LValue.ToPython(headerModuleName, null, this._useClass),
					ssastep2.RValue.ToPython(headerModuleName, hashSet, this._useClass)
				}));
				if (ssastep2.Comment == "")
				{
					codeBuilder2.AppendLine(text2);
				}
				else if (text2.Length + ssastep2.Comment.Length + 2 <= 100)
				{
					codeBuilder2.AppendLine(text2 + " # " + ssastep2.Comment);
				}
				else
				{
					codeBuilder2.AppendLine("# " + ssastep2.Comment);
					codeBuilder2.AppendLine(text2);
				}
			}
			if (!flag)
			{
				string text3 = (list.Any<SSAStep>() ? list.Last<SSAStep>().Comment : constantDefinitions.Last<SSAStep>().Comment);
				SSAValue ssavalue = (list.Any<SSAStep>() ? list.Last<SSAStep>().RValue : constantDefinitions.Last<SSAStep>().LValue);
				if (text3 == "")
				{
					codeBuilder2.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}", new object[] { ssavalue.ToPython(headerModuleName, hashSet, this._useClass) })));
				}
				else
				{
					string text4 = FormattableString.Invariant(FormattableStringFactory.Create("return {0}", new object[] { ssavalue.ToPython(headerModuleName, hashSet, this._useClass) }));
					if (text4.Length + text3.Length + 2 <= 100)
					{
						codeBuilder2.AppendLine(text4 + " # " + text3);
					}
					else
					{
						codeBuilder2.AppendLine("# " + text3);
						codeBuilder2.AppendLine(text4);
					}
				}
			}
			return new CodeForGeneratedFunction(codeBuilder, codeBuilder2);
		}

		// Token: 0x04000897 RID: 2199
		private const int MaxLineLength = 100;
	}
}
