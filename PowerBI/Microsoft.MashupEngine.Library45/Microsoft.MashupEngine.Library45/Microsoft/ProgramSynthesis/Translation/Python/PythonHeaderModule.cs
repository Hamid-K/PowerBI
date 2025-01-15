using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x0200031B RID: 795
	public class PythonHeaderModule : Module
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x000337AC File Offset: 0x000319AC
		public PythonHeaderModule(string name, params string[] headerCode)
			: base(name)
		{
			Assembly assembly = typeof(PythonHeaderModule).GetTypeInfo().Assembly;
			string assemblyName = assembly.GetName().Name;
			IEnumerable<string> enumerable = from m in new string[] { "prose_concept_semantics.py", "text_semantics_common.py" }.Select((string m) => FormattableString.Invariant(FormattableStringFactory.Create("{0}.Translation.Python.{1}", new object[] { assemblyName, m }))).ToArray<string>()
				select AssemblyResourceUtils.LoadResourceFromAssembly(assembly, m);
			this._unisolatedCode = string.Join("\n\n", enumerable.Concat(headerCode));
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003384C File Offset: 0x00031A4C
		private CodeBuilder IsolateCode(string unisolatedCode)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine("import sys");
			codeBuilder.AppendLine();
			using (codeBuilder.NewScope("if sys.version_info.major < 3:", 1U))
			{
				codeBuilder.AppendLine("raise RuntimeError('Need Python 3 or greater')");
			}
			codeBuilder.AppendLine();
			using (codeBuilder.NewScope("if sys.version_info.minor < 5:", 1U))
			{
				codeBuilder.AppendLine("import imp");
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} = imp.new_module(\"{1}\")", new object[] { base.Name, base.Name })));
			}
			using (codeBuilder.NewScope("else:", 1U))
			{
				codeBuilder.AppendLine("import importlib");
				codeBuilder.AppendLine("import importlib.util");
				codeBuilder.AppendLine("import importlib.machinery");
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} = importlib.util.module_from_spec(importlib.machinery.ModuleSpec(\"{1}\", None))", new object[] { base.Name, base.Name })));
			}
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("sys.modules[\"{0}\"] = {1}", new object[] { base.Name, base.Name })));
			codeBuilder.AppendLine();
			codeBuilder.AppendLine();
			codeBuilder.AppendLine("exec(\"\"\"");
			codeBuilder.AppendIndented(unisolatedCode.Replace("\\", "\\\\").Replace("\"\"\"", "\\\"\\\"\\\""));
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("\"\"\", {0}.__dict__)", new object[] { base.Name })));
			codeBuilder.AppendLine();
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0}", new object[] { base.Name })));
			codeBuilder.AppendLine();
			return codeBuilder;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00033A3C File Offset: 0x00031C3C
		public override string GenerateCode(OptimizeFor optimization)
		{
			return this.IsolateCode(this.GenerateUnisolatedCode(optimization)).GetCode();
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00033A50 File Offset: 0x00031C50
		public override string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			return this._unisolatedCode;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00033A58 File Offset: 0x00031C58
		public override string GenerateQualifiedName(string functionName)
		{
			throw new InvalidOperationException("Calling GenerateQualifiedName() on PythonHeaderModule is not supported.");
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00033A64 File Offset: 0x00031C64
		public override void AppendAuxiliaryCode(string codeUnitName, string auxiliaryCode)
		{
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("{0} does not support {1}.", new object[]
			{
				base.GetType(),
				"AppendAuxiliaryCode"
			})));
		}

		// Token: 0x04000885 RID: 2181
		private readonly string _unisolatedCode;
	}
}
