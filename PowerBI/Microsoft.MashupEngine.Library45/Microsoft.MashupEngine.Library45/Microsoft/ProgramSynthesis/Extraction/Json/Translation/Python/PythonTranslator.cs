using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Json.Semantics;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Translation.Python
{
	// Token: 0x02000B9A RID: 2970
	public class PythonTranslator : PythonTranslator<PythonModule, Program, string, ITable<string>>
	{
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x000EE6A4 File Offset: 0x000EC8A4
		protected override IReadOnlyDictionary<string, string> OperatorMapping
		{
			get
			{
				return PythonTranslator.OperatorMappingDictionary;
			}
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x000EE6AC File Offset: 0x000EC8AC
		public override PythonHeaderModule GenerateHeaderModule(Program p, string headerModuleName)
		{
			Assembly assembly = typeof(PythonTranslator).GetTypeInfo().Assembly;
			string name = assembly.GetName().Name;
			string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}.Translation.Python.json_extraction.py", new object[] { name }));
			return new PythonHeaderModule(headerModuleName, new string[]
			{
				SchemaSemanticsLoader.Load,
				AssemblyResourceUtils.LoadResourceFromAssembly(assembly, text)
			});
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x000EE714 File Offset: 0x000EC914
		public override GeneratedFunction Translate(Program root, PythonModule translationModule, OptimizeFor optimization)
		{
			SSAGeneratedFunction ssageneratedFunction = base.Translate(root, translationModule, optimization) as SSAGeneratedFunction;
			base.ApplyStandardOptimizations(ssageneratedFunction, optimization);
			return ssageneratedFunction.ToJsonExtractionPythonGeneratedFunction(root, translationModule.HeaderModuleAliasName);
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x000EE745 File Offset: 0x000EC945
		public override PythonModule CreateModule(string moduleName, string headerModuleReferenceName, string aliasName = "ejson")
		{
			return this.CreateModule(moduleName, headerModuleReferenceName, aliasName, new TranslationOptions("read_json", "file", "utf-8"));
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x000EE764 File Offset: 0x000EC964
		public PythonModule CreateModule(string moduleName, string headerModuleReferenceName, string aliasName, TranslationOptions options)
		{
			return new PythonModule(moduleName, headerModuleReferenceName, aliasName, options);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x000EE770 File Offset: 0x000EC970
		protected override Optional<string> GenerateLiteralRepresentation(object literalValue, Type literalType)
		{
			if (literalType == typeof(JPath))
			{
				JPath jpath = (JPath)literalValue;
				string text = FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { string.Join(", ", jpath.Steps.Select(new Func<JPathStep, string>(this.TranslatePathStep))) }));
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.JPath({1})", new object[] { base.HeaderModuleNameForCurrentTranslation, text })).Some<string>();
			}
			return base.GenerateLiteralRepresentation(literalValue, literalType);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x000EE800 File Offset: 0x000ECA00
		private string TranslatePathStep(JPathStep step)
		{
			switch (step.Kind)
			{
			case JPathStepKind.Access:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.AccessStep({1})", new object[]
				{
					base.HeaderModuleNameForCurrentTranslation,
					((AccessStep)step).Key.ToLiteral(null)
				}));
			case JPathStepKind.Current:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.CurrentStep()", new object[] { base.HeaderModuleNameForCurrentTranslation }));
			case JPathStepKind.Index:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.IndexStep({1})", new object[]
				{
					base.HeaderModuleNameForCurrentTranslation,
					((IndexStep)step).K
				}));
			case JPathStepKind.Parent:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.ParentStep()", new object[] { base.HeaderModuleNameForCurrentTranslation }));
			case JPathStepKind.PropertyKey:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.PropertyKeyStep()", new object[] { base.HeaderModuleNameForCurrentTranslation }));
			case JPathStepKind.PropertyValue:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.PropertyValueStep()", new object[] { base.HeaderModuleNameForCurrentTranslation }));
			case JPathStepKind.SingleProperty:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.SinglePropertyStep()", new object[] { base.HeaderModuleNameForCurrentTranslation }));
			case JPathStepKind.Star:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.StarStep()", new object[] { base.HeaderModuleNameForCurrentTranslation }));
			default:
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported JPathStep subclass: {0}", new object[] { step.GetType() })));
			}
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x000EE981 File Offset: 0x000ECB81
		public PythonTranslator()
			: base(null)
		{
		}

		// Token: 0x040021FF RID: 8703
		private static readonly Dictionary<string, string> OperatorMappingDictionary = typeof(Semantics).GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public).ToDictionary((MethodInfo m) => m.Name, (MethodInfo m) => FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { PythonNameUtils.ConvertStringToSnakeCase(m.Name) })));
	}
}
