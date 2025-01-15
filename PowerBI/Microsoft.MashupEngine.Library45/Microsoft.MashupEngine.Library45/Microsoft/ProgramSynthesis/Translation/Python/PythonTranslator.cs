using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000322 RID: 802
	public abstract class PythonTranslator<TPythonModule, TProgram, TProgramInput, TProgramOutput> : Translator<PythonHeaderModule, TPythonModule, TProgram, TProgramInput, TProgramOutput> where TPythonModule : PythonModule where TProgram : Program<TProgramInput, TProgramOutput>
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x00034560 File Offset: 0x00032760
		protected PythonTranslator(IEnumerable<string> boundNames = null)
			: base(TargetLanguage.Python, boundNames)
		{
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003456A File Offset: 0x0003276A
		protected override string NormalizeVariableName(string originalVariableName)
		{
			return PythonNameUtils.ConvertStringToSnakeCase(originalVariableName);
		}

		// Token: 0x060011B5 RID: 4533
		public abstract TPythonModule CreateModule(string moduleName, string headerModuleReferenceName, string aliasName);

		// Token: 0x060011B6 RID: 4534 RVA: 0x00034574 File Offset: 0x00032774
		public string Translate(TProgram root, string generatedFunctionName, string headerModuleName, OptimizeFor optimization)
		{
			TPythonModule tpythonModule = this.CreateModule(null, headerModuleName, "prose");
			IGeneratedFunction generatedFunction = this.Translate(root, tpythonModule, optimization);
			base.ApplyStandardOptimizations(generatedFunction, optimization);
			tpythonModule.Bind(generatedFunctionName, generatedFunction);
			return tpythonModule.GenerateUnisolatedCode(optimization);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000345BD File Offset: 0x000327BD
		public override PythonHeaderModule GenerateHeaderModule(TProgram p, string headerModuleName)
		{
			return new PythonHeaderModule(headerModuleName, Array.Empty<string>());
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000345CC File Offset: 0x000327CC
		protected override Optional<string> GenerateLiteralRepresentation(object literalValue, Type literalType)
		{
			if (literalType == typeof(string))
			{
				return (literalValue as string).ToPythonLiteral().Some<string>();
			}
			if (literalType == typeof(bool))
			{
				if (literalValue is bool)
				{
					return (((bool)literalValue) ? "True" : "False").Some<string>();
				}
				return Optional<string>.Nothing;
			}
			else
			{
				if (literalType.GetTypeInfo().IsPrimitive)
				{
					return literalValue.ToLiteral(null).Some<string>();
				}
				Type dictionaryType = literalType.GetDictionaryType();
				IDictionary dictionary = literalValue as IDictionary;
				if (dictionaryType != null && dictionary != null)
				{
					Type keyType = dictionaryType.GenericTypeArguments[0];
					Type valueType = dictionaryType.GenericTypeArguments[1];
					List<Optional<string>> list = (from k in dictionary.Keys.OfType<object>()
						select this.GenerateLiteralRepresentation(k, keyType)).ToList<Optional<string>>();
					if (list.AnyNothing<string>())
					{
						return Optional<string>.Nothing;
					}
					List<Optional<string>> list2 = (from k in dictionary.Values.OfType<object>()
						select this.GenerateLiteralRepresentation(k, valueType)).ToList<Optional<string>>();
					if (list2.AnyNothing<string>())
					{
						return Optional<string>.Nothing;
					}
					return ("{" + string.Join(", ", list.Zip(list2, (Optional<string> k, Optional<string> v) => k.Value + ": " + v.Value)) + "}").Some<string>();
				}
				else
				{
					IOptional optional = literalValue as IOptional;
					if (optional == null)
					{
						return Optional<string>.Nothing;
					}
					if (!optional.HasValue)
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}.Optional.nothing()", new object[] { base.HeaderModuleNameForCurrentTranslation })).Some<string>();
					}
					Optional<string> optional2 = this.GenerateLiteralRepresentation(optional.Value, literalType.GenericTypeArguments[0]);
					if (!optional2.HasValue)
					{
						return Optional<string>.Nothing;
					}
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}.Optional.some({1})", new object[] { base.HeaderModuleNameForCurrentTranslation, optional2.Value })).Some<string>();
				}
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000347D8 File Offset: 0x000329D8
		protected override SSAGeneratedFunction GenerateFunctionBody(IEnumerable<Record<string, Type>> formalParameters, Type returnType, ProgramNode functionBody, bool isLambda, OptimizeFor optimization)
		{
			Record<string, Type>[] array = formalParameters.ToArray<Record<string, Type>>();
			base.PushCodeContext();
			base.PushBindingScope(array.ToDictionary((Record<string, Type> p) => p.Item1, (Record<string, Type> p) => new SSAVariable(p.Item2, p.Item1)));
			functionBody.AcceptVisitor<SSAValue, OptimizeFor>(this, optimization);
			base.PopBindingScope();
			List<SSAStep> list = base.PopCodeContext();
			return new PythonGeneratedFunction(array, returnType, list);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003485C File Offset: 0x00032A5C
		protected GeneratedFunction TranslateProgramNode(ProgramNode node, TPythonModule translationModule, bool resetEverything, OptimizeFor optimization)
		{
			if (resetEverything)
			{
				base.Clear();
			}
			base.HeaderModuleNameForCurrentTranslation = translationModule.HeaderModuleAliasName;
			base.CurrentTranslationModule = translationModule;
			Record<string, Type>[] parameters = this.GetParameters(node);
			Type resolvedType = node.Grammar.StartSymbol.ResolvedType;
			GeneratedFunction generatedFunction = this.GenerateFunctionBody(parameters, resolvedType, node, node is LambdaNode, optimization);
			base.HeaderModuleNameForCurrentTranslation = null;
			return generatedFunction;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000348C0 File Offset: 0x00032AC0
		protected virtual Record<string, Type>[] GetParameters(ProgramNode node)
		{
			Symbol inputSymbol = node.Grammar.InputSymbol;
			return new Record<string, Type>[] { Record.Create<string, Type>(inputSymbol.Name, inputSymbol.ResolvedType) };
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000348F8 File Offset: 0x00032AF8
		public override GeneratedFunction Translate(TProgram root, TPythonModule translationModule, OptimizeFor optimization)
		{
			GeneratedFunction generatedFunction = this.TranslateProgramNode(root.ProgramNode, translationModule, true, optimization);
			base.ApplyStandardOptimizations(generatedFunction, optimization);
			return generatedFunction;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00034923 File Offset: 0x00032B23
		protected override bool TryResolveConcept(string conceptName, out string resolvedName)
		{
			if (ConceptRule.AllKnownConcepts.Contains(conceptName))
			{
				resolvedName = FormattableString.Invariant(FormattableStringFactory.Create("prose_{0}", new object[] { conceptName.ToLowerInvariant() }));
				return true;
			}
			resolvedName = null;
			return false;
		}
	}
}
