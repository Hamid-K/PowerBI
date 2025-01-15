using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Translation.Python
{
	// Token: 0x020013F6 RID: 5110
	public class PythonModule : PythonModule
	{
		// Token: 0x06009DFB RID: 40443 RVA: 0x00217F5B File Offset: 0x0021615B
		public PythonModule(string name, string headerModuleName, string aliasName)
			: base(name, headerModuleName, aliasName, Array.Empty<string>())
		{
		}

		// Token: 0x06009DFC RID: 40444 RVA: 0x00217F76 File Offset: 0x00216176
		public override void Bind(string functionName, IGeneratedFunction function)
		{
			base.Bind(FormattableString.Invariant(FormattableStringFactory.Create("{0}_internal", new object[] { functionName })), function);
			this._userBoundFunctions.Add(functionName);
		}

		// Token: 0x06009DFD RID: 40445 RVA: 0x00217FA4 File Offset: 0x002161A4
		public override void ClearBindings()
		{
			base.ClearBindings();
			this._userBoundFunctions.Clear();
		}

		// Token: 0x06009DFE RID: 40446 RVA: 0x00217FB8 File Offset: 0x002161B8
		public override string GenerateCode(OptimizeFor optimization)
		{
			string text = this.GenerateUnisolatedCode(optimization).Replace("\\", "\\\\").Replace("\"\"\"", "\\\"\\\"\\\"");
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}.execute_in_named_module(\"\"\"\n{1}\n\"\"\", \"{2}\")\n\nimport {3}", new object[] { base.HeaderModuleName, text, base.Name, base.Name }));
		}

		// Token: 0x06009DFF RID: 40447 RVA: 0x00218020 File Offset: 0x00216220
		public override string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			string text = base.GenerateUnisolatedCode(optimization);
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendIndented(text);
			foreach (string text2 in this._userBoundFunctions)
			{
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}(v):", new object[] { text2 })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0} as {1}", new object[] { base.HeaderModuleName, base.HeaderModuleAliasName })));
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("try:", Array.Empty<object>())), 1U))
					{
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("region = {0}.Substring(v) if v is not None else {1}.Substring('')", new object[] { base.HeaderModuleAliasName, base.HeaderModuleAliasName })));
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("t = {0}_internal(region)", new object[] { text2 })));
						using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if t is None:", Array.Empty<object>())), 1U))
						{
							codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return None", Array.Empty<object>())));
						}
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return [x.value.get_value() if (x.value is not None) else None for x in t]", Array.Empty<object>())));
					}
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("except:", Array.Empty<object>())), 1U))
					{
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return None", Array.Empty<object>())));
					}
				}
			}
			codeBuilder.AppendLine();
			return codeBuilder.GetCode();
		}

		// Token: 0x04003FF4 RID: 16372
		private readonly List<string> _userBoundFunctions = new List<string>();
	}
}
