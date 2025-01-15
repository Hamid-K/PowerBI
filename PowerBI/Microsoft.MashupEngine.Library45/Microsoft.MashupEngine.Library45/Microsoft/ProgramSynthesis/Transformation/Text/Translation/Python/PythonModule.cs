using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DB0 RID: 7600
	public class PythonModule : PythonModule
	{
		// Token: 0x17002A75 RID: 10869
		// (get) Token: 0x0600FEEA RID: 65258 RVA: 0x00367768 File Offset: 0x00365968
		// (set) Token: 0x0600FEEB RID: 65259 RVA: 0x00367770 File Offset: 0x00365970
		internal bool AddInternalToNameWhenBinding { private get; set; }

		// Token: 0x0600FEEC RID: 65260 RVA: 0x00367779 File Offset: 0x00365979
		public PythonModule(string name, string headerModuleName, string aliasName)
			: base(name, headerModuleName, aliasName, Array.Empty<string>())
		{
			this.AddInternalToNameWhenBinding = true;
		}

		// Token: 0x17002A76 RID: 10870
		// (get) Token: 0x0600FEED RID: 65261 RVA: 0x0036779B File Offset: 0x0036599B
		public Record<string, Type>[] Parameters
		{
			get
			{
				IReadOnlyList<PythonModule.ColumnNameMapping> columnNameMappings = this._columnNameMappings;
				if (columnNameMappings == null)
				{
					return null;
				}
				return columnNameMappings.Select(delegate(PythonModule.ColumnNameMapping m)
				{
					SSAVariable ssavariable = m.SSAVariable;
					return Record.Create<string, Type>(ssavariable.VariableName, ssavariable.ValueType);
				}).ToArray<Record<string, Type>>();
			}
		}

		// Token: 0x0600FEEE RID: 65262 RVA: 0x003677D4 File Offset: 0x003659D4
		public override void Bind(string functionName, IGeneratedFunction function)
		{
			if (!(function is TransformationTextPythonGeneratedFunction) && !(function is OpaqueGeneratedFunction))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Attempted to bind name \"{0}\" to a generated function which was not an ", new object[] { functionName })) + FormattableString.Invariant(FormattableStringFactory.Create("instance of {0} or {1}", new object[] { "TransformationTextPythonGeneratedFunction", "OpaqueGeneratedFunction" })));
			}
			if (function is TransformationTextPythonGeneratedFunction)
			{
				string text = (this.AddInternalToNameWhenBinding ? "_internal" : string.Empty);
				base.Bind(FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", new object[] { functionName, text })), function);
				this._userBoundFunctions.Add(functionName);
				return;
			}
			if (!base.BoundFunctions.ContainsKey(functionName))
			{
				base.BindLambda(functionName, function);
			}
		}

		// Token: 0x0600FEEF RID: 65263 RVA: 0x0036789E File Offset: 0x00365A9E
		public override void ClearBindings()
		{
			base.ClearBindings();
			this._userBoundFunctions.Clear();
			this._columnNameMappings = null;
			this._usedChooseInput = false;
			this.AddInternalToNameWhenBinding = true;
		}

		// Token: 0x0600FEF0 RID: 65264 RVA: 0x003678C8 File Offset: 0x00365AC8
		internal IEnumerable<KeyValuePair<string, SSAValue>> BindColumnNameMappings(IEnumerable<string> columnNames)
		{
			if (this._usedChooseInput)
			{
				throw new InvalidOperationException("Cannot bind columnsUsed after code has already been generated.");
			}
			if (this._columnNameMappings != null)
			{
				throw new InvalidOperationException("Cannot bind columnsUsed multiple times.");
			}
			this._columnNameMappings = PythonModule.ColumnNameMapping.Build(columnNames);
			return this._columnNameMappings.Select((PythonModule.ColumnNameMapping m) => KVP.Create<string, SSAValue>(m.VariableName, m.SSAVariable));
		}

		// Token: 0x0600FEF1 RID: 65265 RVA: 0x00367934 File Offset: 0x00365B34
		public override string GenerateCode(OptimizeFor optimization)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}.execute_in_named_module(\"\"\"", new object[] { base.HeaderModuleName })));
			codeBuilder.AppendIndented(this.GenerateUnisolatedCode(optimization).Replace("\\", "\\\\").Replace("\"\"\"", "\\\"\\\"\\\""));
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("\"\"\", \"{0}\")", new object[] { base.Name })));
			codeBuilder.AppendLine();
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("import {0}", new object[] { base.Name })));
			return codeBuilder.GetCode();
		}

		// Token: 0x0600FEF2 RID: 65266 RVA: 0x003679EC File Offset: 0x00365BEC
		public override string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			string text = base.GenerateUnisolatedCode(optimization);
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendIndented(text);
			foreach (string text2 in this._userBoundFunctions)
			{
				if (this.AddInternalToNameWhenBinding)
				{
					string text3 = FormattableString.Invariant(FormattableStringFactory.Create("{0}_internal", new object[] { text2 }));
					ITransformationTextGeneratedFunction transformationTextGeneratedFunction = base.BoundFunctions[text3] as ITransformationTextGeneratedFunction;
					if (transformationTextGeneratedFunction == null)
					{
						throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Function named \"{0}\" could not be resolved during Python translation.", new object[] { text2 })));
					}
					IEnumerable<string> usedColumns = transformationTextGeneratedFunction.UsedColumns;
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}(vs):", new object[] { text2 })), 1U))
					{
						using (codeBuilder.NewScope("try:", 1U))
						{
							string text4;
							if (usedColumns.Count<string>() <= 2)
							{
								text4 = string.Join(", ", usedColumns.Select((string c) => FormattableString.Invariant(FormattableStringFactory.Create("vs.get({0})", new object[] { c.ToPythonLiteral() }))));
							}
							else
							{
								string text5 = "[{0}]";
								object[] array = new object[1];
								array[0] = string.Join(", ", usedColumns.Select((string c) => c.ToPythonLiteral()));
								string text6 = FormattableString.Invariant(FormattableStringFactory.Create(text5, array));
								text4 = FormattableString.Invariant(FormattableStringFactory.Create("*(vs.get(k) for k in {0})", new object[] { text6 }));
							}
							codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("t = {0}_internal({1})", new object[] { text2, text4 })));
						}
						using (codeBuilder.NewScope("except Exception as e:", 1U))
						{
							codeBuilder.AppendLine("return None");
						}
						codeBuilder.AppendLine("return t");
					}
				}
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x0600FEF3 RID: 65267 RVA: 0x00367C5C File Offset: 0x00365E5C
		public override SSARValue GenerateFunctionApplication(NonterminalNode node, string functionName, OptimizeFor optimization, ProgramNodeVisitor<SSAValue, OptimizeFor> childTranslationVisitor)
		{
			ProgramNode programNode = null;
			ChooseInput chooseInput;
			SelectInput selectInput;
			LookupInput lookupInput;
			if (Language.Build.Node.IsRule.ChooseInput(node, out chooseInput))
			{
				programNode = chooseInput.columnName.Node;
			}
			else if (Language.Build.Node.IsRule.SelectInput(node, out selectInput))
			{
				programNode = selectInput.name.Node;
			}
			else if (Language.Build.Node.IsRule.LookupInput(node, out lookupInput))
			{
				programNode = lookupInput.columnName.Node;
			}
			ConstStr constStr;
			if (programNode != null)
			{
				if (this._columnNameMappings != null)
				{
					string pythonColumnName = ((SSALiteral)programNode.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization).AllDependencies.Single<SSAValue>()).LiteralString;
					return this._columnNameMappings.Single((PythonModule.ColumnNameMapping m) => m.ColumnNameLiteralString == pythonColumnName).SSAVariable;
				}
				this._usedChooseInput = true;
			}
			else if (Language.Build.Node.IsRule.ConstStr(node, out constStr))
			{
				SSAValue ssavalue = constStr.s.Node.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization);
				SSARValue ssarvalue = ssavalue as SSARValue;
				if (ssarvalue != null)
				{
					return ssarvalue;
				}
				SSARegister ssaregister = ssavalue as SSARegister;
				if (ssaregister != null)
				{
					return ssaregister.StepWhereDefined.RValue;
				}
			}
			else if (Language.Build.Node.IsRule.Concat(node) || Language.Build.Node.IsRule.Add(node))
			{
				functionName = "operators.__add__";
			}
			else
			{
				SubStr subStr;
				if (Language.Build.Node.IsRule.SubStr(node, out subStr))
				{
					SSAValue ssavalue2 = subStr.x.Node.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization);
					PosPair posPair;
					SSAValue ssavalue3;
					SSAValue ssavalue4;
					if (subStr.PP.Is_PosPair(Language.Build, out posPair))
					{
						ssavalue3 = posPair.pos1.Node.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization);
						ssavalue4 = posPair.pos2.Node.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization);
					}
					else
					{
						SSAValue ssavalue5 = subStr.PP.Node.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization);
						ssavalue3 = new SSAFunctionApplication(typeof(int), "operators.__getitem__", new SSAValue[]
						{
							ssavalue5,
							new SSALiteral(typeof(int), "0")
						});
						ssavalue4 = new SSAFunctionApplication(typeof(int), "operators.__getitem__", new SSAValue[]
						{
							ssavalue5,
							new SSALiteral(typeof(int), "1")
						});
					}
					return new SSAFunctionApplication(node.Rule.ReturnResolvedType, "operators.__ite__", new SSAValue[]
					{
						new SSAFunctionApplication(typeof(bool), "operators.__lte__", new SSAValue[] { ssavalue3, ssavalue4 }),
						new SSAFunctionApplication(node.Rule.ReturnResolvedType, "operators.__getitem_slice2__", new SSAValue[] { ssavalue2, ssavalue3, ssavalue4 }),
						new SSALiteral(node.Rule.ReturnResolvedType, "None")
					});
				}
				if (Language.Build.Node.IsRule.RSubStr(node))
				{
					functionName = "operators.__getitem_slice_start_only__";
					if (optimization == OptimizeFor.Performance)
					{
						return new SSAFunctionApplication(node.Rule.ReturnResolvedType, functionName, node.Children.Select((ProgramNode child) => child.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization)).MutateFirst((SSAValue first) => new SSAFunctionApplication(first.ValueType, "str_to_substring", new SSAValue[] { first })).ToArray<SSAValue>());
					}
				}
				else if (optimization == OptimizeFor.Performance && PythonModule.SubstringRules.Contains(node.Rule))
				{
					return new SSAFunctionApplication(node.Rule.ReturnResolvedType, functionName + "_from_ss", node.Children.Select((ProgramNode child) => child.AcceptVisitor<SSAValue, OptimizeFor>(childTranslationVisitor, optimization)).MutateFirst(delegate(SSAValue first)
					{
						SSARegister ssaregister2 = first as SSARegister;
						SSAFunctionApplication ssafunctionApplication = (((ssaregister2 != null) ? ssaregister2.StepWhereDefined.RValue : null) ?? first) as SSAFunctionApplication;
						if (ssafunctionApplication != null)
						{
							if (ssafunctionApplication.FunctionName == "operators.__getitem_slice_start_only__")
							{
								return first;
							}
							if (PythonModule._build.Node.IsRule.SubStr(node.Children[0]) && ssafunctionApplication.FunctionName == "operators.__ite__")
							{
								SSAFunctionApplication ssafunctionApplication2 = ssafunctionApplication.FunctionArguments[1] as SSAFunctionApplication;
								if (ssafunctionApplication2 != null && ssafunctionApplication2.FunctionName == "operators.__getitem_slice2__")
								{
									return new SSAFunctionApplication(ssafunctionApplication2.ValueType, "operators.__getitem_slice2__", ssafunctionApplication2.FunctionArguments.MutateFirst((SSAValue firstSliceArg) => new SSAFunctionApplication(firstSliceArg.ValueType, "str_to_substring", new SSAValue[] { firstSliceArg })).ToArray<SSAValue>());
								}
							}
						}
						return new SSAFunctionApplication(first.ValueType, "str_to_substring", new SSAValue[] { first });
					}).ToArray<SSAValue>());
				}
			}
			return base.GenerateFunctionApplication(node, functionName, optimization, childTranslationVisitor);
		}

		// Token: 0x04005FA1 RID: 24481
		private readonly List<string> _userBoundFunctions = new List<string>();

		// Token: 0x04005FA2 RID: 24482
		private IReadOnlyList<PythonModule.ColumnNameMapping> _columnNameMappings;

		// Token: 0x04005FA3 RID: 24483
		private bool _usedChooseInput;

		// Token: 0x04005FA5 RID: 24485
		private static readonly GrammarBuilders _build = GrammarBuilders.Instance(Language.Grammar);

		// Token: 0x04005FA6 RID: 24486
		private static readonly HashSet<NonterminalRule> SubstringRules = new HashSet<NonterminalRule>
		{
			Language.Build.Rule.RegexPosition,
			Language.Build.Rule.RegexPositionRelative,
			Language.Build.Rule.ParsePartialDateTime,
			Language.Build.Rule.ExternalExtractorPositionPair,
			Language.Build.Rule.Matches,
			Language.Build.Rule.Contains,
			Language.Build.Rule.StartsWith,
			Language.Build.Rule.EndsWith
		};

		// Token: 0x02001DB1 RID: 7601
		private struct ColumnNameMapping
		{
			// Token: 0x0600FEF5 RID: 65269 RVA: 0x003681FA File Offset: 0x003663FA
			private ColumnNameMapping(string columnName, string variableName)
			{
				this.ColumnName = columnName;
				this.ColumnNameLiteralString = columnName.ToPythonLiteral();
				this.VariableName = variableName;
				this.SSAVariable = new SSAVariable(typeof(string), this.VariableName);
			}

			// Token: 0x17002A77 RID: 10871
			// (get) Token: 0x0600FEF6 RID: 65270 RVA: 0x00368231 File Offset: 0x00366431
			public readonly string ColumnName { get; }

			// Token: 0x17002A78 RID: 10872
			// (get) Token: 0x0600FEF7 RID: 65271 RVA: 0x00368239 File Offset: 0x00366439
			public readonly string ColumnNameLiteralString { get; }

			// Token: 0x17002A79 RID: 10873
			// (get) Token: 0x0600FEF8 RID: 65272 RVA: 0x00368241 File Offset: 0x00366441
			public readonly string VariableName { get; }

			// Token: 0x17002A7A RID: 10874
			// (get) Token: 0x0600FEF9 RID: 65273 RVA: 0x00368249 File Offset: 0x00366449
			public readonly SSAVariable SSAVariable { get; }

			// Token: 0x0600FEFA RID: 65274 RVA: 0x00368254 File Offset: 0x00366454
			internal static IReadOnlyList<PythonModule.ColumnNameMapping> Build(IEnumerable<string> columnNames)
			{
				Func<string, string> func;
				if ((func = PythonModule.ColumnNameMapping.<>O.<0>__NearestValidIdentifier) == null)
				{
					func = (PythonModule.ColumnNameMapping.<>O.<0>__NearestValidIdentifier = new Func<string, string>(PythonNameUtils.NearestValidIdentifier));
				}
				List<string> list = columnNames.Select(func).ToList<string>();
				foreach (IGrouping<string, string> grouping in (from id in list
					group id by id into g
					where g.Count<string>() > 1
					select g).ToList<IGrouping<string, string>>())
				{
					string key = grouping.Key;
					int num = 0;
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] == key)
						{
							string text;
							do
							{
								text = string.Format("{0}_{1}", key, ++num);
							}
							while (list.Contains(text));
							list[i] = text;
						}
					}
				}
				return columnNames.Zip(list, (string colName, string id) => new PythonModule.ColumnNameMapping(colName, id)).ToList<PythonModule.ColumnNameMapping>();
			}

			// Token: 0x02001DB2 RID: 7602
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04005FAB RID: 24491
				public static Func<string, string> <0>__NearestValidIdentifier;
			}
		}
	}
}
