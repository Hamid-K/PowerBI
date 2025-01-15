using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Translation.Python
{
	// Token: 0x02000B99 RID: 2969
	public class PythonModule : PythonModule
	{
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x000EDAEB File Offset: 0x000EBCEB
		private TranslationOptions Options { get; }

		// Token: 0x06004B70 RID: 19312 RVA: 0x000EDAF3 File Offset: 0x000EBCF3
		public PythonModule(string name, string headerModuleName, string aliasName, TranslationOptions options)
			: base(name, headerModuleName, aliasName, Array.Empty<string>())
		{
			this.Options = options;
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x000EDB18 File Offset: 0x000EBD18
		public override void Bind(string functionName, IGeneratedFunction function)
		{
			if (!(function is JsonExtractionPythonGeneratedFunction))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("The GeneratedFunction object bound in {0} is not of type {1}.", new object[]
				{
					base.GetType(),
					typeof(JsonExtractionPythonGeneratedFunction)
				})));
			}
			base.Bind(FormattableString.Invariant(FormattableStringFactory.Create("{0}_internal", new object[] { functionName })), function);
			this._userBoundFunctions.Add(functionName);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x000EDB8A File Offset: 0x000EBD8A
		public override void ClearBindings()
		{
			base.ClearBindings();
			this._userBoundFunctions.Clear();
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x000EDBA0 File Offset: 0x000EBDA0
		public override string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			string text = base.GenerateUnisolatedCode(optimization);
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendIndented(text);
			foreach (string text2 in this._userBoundFunctions)
			{
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				JsonExtractionPythonGeneratedFunction jsonExtractionPythonGeneratedFunction = (JsonExtractionPythonGeneratedFunction)base.BoundFunctions[FormattableString.Invariant(FormattableStringFactory.Create("{0}_internal", new object[] { text2 }))];
				string text3 = TreeToTableSemantics.OuterJoin.ToString().ToLiteral(null);
				string generateSchemaCode = jsonExtractionPythonGeneratedFunction.GenerateSchemaCode;
				string text4 = PythonModule.GenerateString(jsonExtractionPythonGeneratedFunction.Program.StartDelimiter);
				string text5 = PythonModule.GenerateString(jsonExtractionPythonGeneratedFunction.Program.EndDelimiter);
				string text6 = jsonExtractionPythonGeneratedFunction.Program.HandleInvalidJson.ToString();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}(json_str, tree_to_table_semantics={1}):", new object[] { text2, text3 })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0} as {1}", new object[] { base.HeaderModuleName, base.HeaderModuleAliasName })));
					codeBuilder.AppendLine("import functools");
					codeBuilder.AppendLine("import json");
					using (codeBuilder.NewScope("try:", 1U))
					{
						codeBuilder.AppendLine("result = []");
						if (jsonExtractionPythonGeneratedFunction.Program.AcceptNdJson)
						{
							using (codeBuilder.NewScope("for line in json_str.splitlines():", 1U))
							{
								PythonModule.GenerateFunctionCode(codeBuilder, base.HeaderModuleAliasName, text2, "line", generateSchemaCode, text4, text5, text6);
								goto IL_01A5;
							}
						}
						PythonModule.GenerateFunctionCode(codeBuilder, base.HeaderModuleAliasName, text2, "json_str", generateSchemaCode, text4, text5, text6);
						IL_01A5:
						codeBuilder.AppendLine("return result");
					}
					using (codeBuilder.NewScope("except Exception as e:", 1U))
					{
						codeBuilder.AppendLine("return None");
					}
				}
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}_incremental(json_str, tree_to_table_semantics={1}):", new object[] { text2, text3 })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0} as {1}", new object[] { base.HeaderModuleName, base.HeaderModuleAliasName })));
					codeBuilder.AppendLine("import functools");
					codeBuilder.AppendLine("import json");
					using (codeBuilder.NewScope("try:", 1U))
					{
						if (jsonExtractionPythonGeneratedFunction.Program.AcceptNdJson)
						{
							using (codeBuilder.NewScope("for line in json_str.splitlines():", 1U))
							{
								PythonModule.GenerateIncrementalFunctionCode(codeBuilder, base.HeaderModuleAliasName, text2, "line", generateSchemaCode, text4, text5, text6);
								goto IL_02E1;
							}
						}
						PythonModule.GenerateIncrementalFunctionCode(codeBuilder, base.HeaderModuleAliasName, text2, "json_str", generateSchemaCode, text4, text5, text6);
					}
					IL_02E1:
					using (codeBuilder.NewScope("except Exception as e:", 1U))
					{
						using (codeBuilder.NewScope("if isinstance(e, StopIteration):", 1U))
						{
							codeBuilder.AppendLine("raise e");
						}
						codeBuilder.AppendLine("return None");
					}
				}
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}_values_only(json_str, tree_to_table_semantics={1}):", new object[] { text2, text3 })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0} as {1}", new object[] { base.HeaderModuleName, base.HeaderModuleAliasName })));
					codeBuilder.AppendLine("import functools");
					codeBuilder.AppendLine("import json");
					using (codeBuilder.NewScope("try:", 1U))
					{
						if (jsonExtractionPythonGeneratedFunction.Program.AcceptNdJson)
						{
							using (codeBuilder.NewScope("for line in json_str.splitlines():", 1U))
							{
								PythonModule.GenerateValueOnlyFunctionCode(codeBuilder, base.HeaderModuleAliasName, text2, "line", generateSchemaCode, text4, text5, text6);
								goto IL_042B;
							}
						}
						PythonModule.GenerateValueOnlyFunctionCode(codeBuilder, base.HeaderModuleAliasName, text2, "json_str", generateSchemaCode, text4, text5, text6);
					}
					IL_042B:
					using (codeBuilder.NewScope("except Exception as e:", 1U))
					{
						using (codeBuilder.NewScope("if isinstance(e, StopIteration):", 1U))
						{
							codeBuilder.AppendLine("raise e");
						}
						codeBuilder.AppendLine("return None");
					}
				}
			}
			codeBuilder.AppendLine();
			codeBuilder.AppendLine();
			return codeBuilder.GetCode();
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x000EE1CC File Offset: 0x000EC3CC
		private static void GenerateFunctionCode(CodeBuilder code, string headerModuleReference, string userBoundFunction, string inputVariable, string schemaCode, string start, string end, string handleInvalid)
		{
			code.Append(FormattableString.Invariant(FormattableStringFactory.Create("json_region = {0}.JsonRegion.create({1}.load_lazy_json_tree_from_string", new object[] { headerModuleReference, headerModuleReference })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("({0}, {1}, {2}, {3}))", new object[] { inputVariable, start, end, handleInvalid })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("t = {0}_internal(json_region)", new object[] { userBoundFunction })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("r = t.to_table({0}, tree_to_table_semantics)", new object[] { schemaCode })));
			using (code.NewScope("for row in r:", 1U))
			{
				code.AppendLine("result.append([(a, b.get_value() if b is not None else None) for a, b in row])");
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x000EE2A0 File Offset: 0x000EC4A0
		private static void GenerateIncrementalFunctionCode(CodeBuilder code, string headerModuleReference, string userBoundFunction, string inputVariable, string schemaCode, string start, string end, string handleInvalid)
		{
			code.Append(FormattableString.Invariant(FormattableStringFactory.Create("json_region = {0}.JsonRegion.create({1}.load_lazy_json_tree_from_string", new object[] { headerModuleReference, headerModuleReference })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("({0}, {1}, {2}, {3}))", new object[] { inputVariable, start, end, handleInvalid })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("t = {0}_internal(json_region)", new object[] { userBoundFunction })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("r = t.to_table({0}, tree_to_table_semantics)", new object[] { schemaCode })));
			using (code.NewScope("for row in r:", 1U))
			{
				code.AppendLine("yield [(a, b.get_value() if b is not None else None) for a, b in row]");
			}
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x000EE374 File Offset: 0x000EC574
		private static void GenerateValueOnlyFunctionCode(CodeBuilder code, string headerModuleReference, string userBoundFunction, string inputVariable, string schemaCode, string start, string end, string handleInvalid)
		{
			code.Append(FormattableString.Invariant(FormattableStringFactory.Create("json_region = {0}.JsonRegion.create({1}.load_lazy_json_tree_from_string", new object[] { headerModuleReference, headerModuleReference })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("({0}, {1}, {2}, {3}))", new object[] { inputVariable, start, end, handleInvalid })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("t = {0}_internal(json_region)", new object[] { userBoundFunction })));
			code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("r = t.to_table_values_only({0}, tree_to_table_semantics)", new object[] { schemaCode })));
			using (code.NewScope("for row in r:", 1U))
			{
				code.AppendLine("yield [a for a in row]");
			}
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x000EE448 File Offset: 0x000EC648
		private static string GenerateString(string s)
		{
			if (s != null)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { s }));
			}
			return "None";
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x000EE46C File Offset: 0x000EC66C
		public override string GenerateCode(OptimizeFor optimization)
		{
			return this.GenerateCode(PythonTarget.Library, optimization);
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x000EE478 File Offset: 0x000EC678
		public string GenerateCode(PythonTarget target, OptimizeFor optimization)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}.execute_in_named_module(\"\"\"", new object[] { base.HeaderModuleName })));
			string text = this.GenerateUnisolatedCode(target, optimization);
			codeBuilder.AppendIndented(text.Replace("\\", "\\\\").Replace("\"\"\"", "\\\"\\\"\\\""));
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("\"\"\", \"{0}\")", new object[] { base.Name })));
			codeBuilder.AppendLine();
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0}", new object[] { base.Name })));
			return codeBuilder.GetCode();
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x000EE534 File Offset: 0x000EC734
		public string GenerateUnisolatedCode(PythonTarget target, OptimizeFor optimization)
		{
			if (target <= PythonTarget.Library)
			{
				return this.GenerateUnisolatedCode(optimization);
			}
			if (target - PythonTarget.Pandas > 1)
			{
				throw new NotImplementedException("target");
			}
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			foreach (string text in this._userBoundFunctions)
			{
				JsonExtractionPythonGeneratedFunction jsonExtractionPythonGeneratedFunction = (JsonExtractionPythonGeneratedFunction)base.BoundFunctions[FormattableString.Invariant(FormattableStringFactory.Create("{0}_internal", new object[] { text }))];
				string text2 = ((target == PythonTarget.Pandas) ? jsonExtractionPythonGeneratedFunction.Program.GetPandasCode(this.Options.Input, this.Options.Encoding, true) : jsonExtractionPythonGeneratedFunction.Program.GetPySparkCode(this.Options.Input));
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}({1}):", new object[]
				{
					text,
					this.Options.Input
				})), 1U))
				{
					text2 = text2.Remove(text2.TrimEnd(Array.Empty<char>()).LastIndexOf(Environment.NewLine));
					codeBuilder.AppendIndented(text2);
					codeBuilder.AppendLine();
					codeBuilder.AppendLine("return df");
				}
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x040021FD RID: 8701
		private readonly List<string> _userBoundFunctions = new List<string>();
	}
}
