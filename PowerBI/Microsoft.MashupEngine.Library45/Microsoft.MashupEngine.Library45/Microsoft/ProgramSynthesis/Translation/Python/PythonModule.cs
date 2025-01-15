using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x0200031D RID: 797
	public class PythonModule : Module
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00033AC3 File Offset: 0x00031CC3
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00033ACB File Offset: 0x00031CCB
		public PythonImports Imports { get; private set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00033AD4 File Offset: 0x00031CD4
		public string HeaderModuleName { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00033ADC File Offset: 0x00031CDC
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00033AE4 File Offset: 0x00031CE4
		public bool ModuleUsesHeader { get; private set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00033AED File Offset: 0x00031CED
		public string HeaderModuleAliasName
		{
			get
			{
				return this._headerModuleAliasName ?? this.HeaderModuleName;
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00033AFF File Offset: 0x00031CFF
		public PythonModule(string name, string headerModuleName, string headerModuleAliasName, params string[] imports)
			: this(name, headerModuleName, headerModuleAliasName, imports)
		{
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00033B0C File Offset: 0x00031D0C
		public PythonModule(string name, string headerModuleName, string headerModuleAliasName, IEnumerable<string> imports)
			: base(name)
		{
			this.HeaderModuleName = headerModuleName;
			this._headerModuleAliasName = headerModuleAliasName;
			this.Imports = new PythonImports();
			this.Imports.AddImports(imports);
			this.ModuleUsesHeader = false;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00033B42 File Offset: 0x00031D42
		public override void ClearBindings()
		{
			base.ClearBindings();
			this.Imports.Clear();
			this.ModuleUsesHeader = false;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00033B5C File Offset: 0x00031D5C
		public override string GenerateCode(OptimizeFor optimization)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}.execute_in_named_module(\"\"\"", new object[] { this.HeaderModuleName })));
			codeBuilder.AppendIndented(this.GenerateUnisolatedCode(optimization).Replace("\\", "\\\\").Replace("\"\"\"", "\\\"\\\"\\\""));
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("\"\"\", \"{0}\")", new object[] { base.Name })));
			codeBuilder.AppendLine();
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0}", new object[] { base.Name })));
			codeBuilder.AppendLine();
			return codeBuilder.GetCode();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00033C1C File Offset: 0x00031E1C
		public override string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			if (this.ModuleUsesHeader)
			{
				this.Imports.AddImports(new string[] { this.HeaderModuleName + " as " + this.HeaderModuleAliasName });
			}
			CodeBuilder code = this.Imports.GetCode(false, 0U);
			code.AppendLine();
			foreach (KeyValuePair<string, string> keyValuePair in base.AuxiliaryCode)
			{
				code.AppendIndented(keyValuePair.Value);
				code.AppendLine();
				code.AppendLine();
			}
			IReadOnlyList<Record<string, IGeneratedFunction, CodeForGeneratedFunction>> readOnlyList = base.Functions.Select((Record<string, IGeneratedFunction> nameAndFunction) => Record.Create<string, IGeneratedFunction, CodeForGeneratedFunction>(nameAndFunction.Item1, nameAndFunction.Item2, nameAndFunction.Item2.GenerateCode(this.HeaderModuleAliasName, optimization))).ToList<Record<string, IGeneratedFunction, CodeForGeneratedFunction>>();
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			foreach (CodeForGeneratedFunction codeForGeneratedFunction in readOnlyList.Select((Record<string, IGeneratedFunction, CodeForGeneratedFunction> r) => r.Item3))
			{
				if (!codeForGeneratedFunction.StaticCode.IsEmpty)
				{
					codeBuilder.Append(codeForGeneratedFunction.StaticCode);
					codeBuilder.AppendLine();
				}
			}
			bool flag = !codeBuilder.IsEmpty;
			string text = (flag ? "self, " : "");
			CodeBuilder codeBuilder2 = new CodeBuilder(4U);
			foreach (Record<string, IGeneratedFunction> record in base.Functions)
			{
				PythonGeneratedFunction pythonGeneratedFunction = record.Item2 as PythonGeneratedFunction;
				if (pythonGeneratedFunction != null)
				{
					pythonGeneratedFunction.SetUseClass(flag);
				}
			}
			foreach (Record<string, IGeneratedFunction, CodeForGeneratedFunction> record2 in readOnlyList)
			{
				string item = record2.Item1;
				IGeneratedFunction item2 = record2.Item2;
				CodeForGeneratedFunction item3 = record2.Item3;
				CodeBuilder codeBuilder3 = codeBuilder2;
				string text2 = "def {0}({1}{2}):";
				object[] array = new object[3];
				array[0] = item;
				array[1] = text;
				array[2] = string.Join(", ", item2.Parameters.Select((Record<string, Type> t) => t.Item1));
				using (codeBuilder3.NewScope(FormattableString.Invariant(FormattableStringFactory.Create(text2, array)), 1U))
				{
					codeBuilder2.Append(item3.DynamicCode);
					codeBuilder2.AppendLine();
					codeBuilder2.AppendLine();
				}
			}
			if (flag)
			{
				string text3 = FormattableString.Invariant(FormattableStringFactory.Create("_{0}_Module_Holder", new object[] { base.Name }));
				using (code.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("class {0}(object):", new object[] { text3 })), 1U))
				{
					code.AppendLine("_instance = None");
					code.AppendLine();
					code.AppendLine("@classmethod");
					using (code.NewScope("def get_instance(cls):", 1U))
					{
						using (code.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if cls._instance is None:", Array.Empty<object>())), 1U))
						{
							code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("cls._instance = cls()", Array.Empty<object>())));
						}
						code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return cls._instance", Array.Empty<object>())));
					}
					code.AppendLine();
					if (!codeBuilder.IsEmpty)
					{
						using (code.NewScope("def __init__(self):", 1U))
						{
							code.Append(codeBuilder);
						}
						code.AppendLine();
					}
					code.Append(codeBuilder2);
					code.AppendLine();
				}
				foreach (Record<string, IGeneratedFunction> record3 in base.Functions)
				{
					code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} = {1}.get_instance().{2}", new object[] { record3.Item1, text3, record3.Item1 })));
				}
				code.AppendLine();
			}
			else
			{
				code.Append(codeBuilder2);
				code.AppendLine();
			}
			return code.GetCode();
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000340D4 File Offset: 0x000322D4
		public override string GenerateQualifiedName(string functionName)
		{
			IGeneratedFunction generatedFunction;
			if (!base.BoundFunctions.TryGetValue(functionName, out generatedFunction))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve function named \"{0}\" in call to PythonModule.GenerateApplication().", new object[] { functionName })));
			}
			if (!(generatedFunction is PythonGeneratedFunction))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Internal Error: The function named \"{0}\" did not resolve to an Python Function object", new object[] { functionName })));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}.{1}", new object[] { base.Name, functionName }));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003415C File Offset: 0x0003235C
		protected CodeBuilder GeneratePysparkImports(IReadOnlyList<string> pysparkImports)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			if (pysparkImports.Count == 0)
			{
				return codeBuilder;
			}
			foreach (string text in pysparkImports)
			{
				codeBuilder.AppendLine(text);
			}
			return codeBuilder;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000341B8 File Offset: 0x000323B8
		public override void AppendAuxiliaryCode(string codeUnitName, string auxiliaryCode)
		{
			base._AppendAuxiliaryCodeImpl(codeUnitName, auxiliaryCode);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000341C2 File Offset: 0x000323C2
		public override void AddImports(params string[] imports)
		{
			this.Imports.AddImports(imports);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000341D0 File Offset: 0x000323D0
		public override void SetModuleUsesHeader()
		{
			this.ModuleUsesHeader = true;
		}

		// Token: 0x0400088B RID: 2187
		private readonly string _headerModuleAliasName;
	}
}
