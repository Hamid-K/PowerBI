using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F8 RID: 760
	public abstract class Translator<THeaderModule, TModule, TProgram, TProgramInput, TProgramOutput> : ProgramNodeVisitor<SSAValue, OptimizeFor> where THeaderModule : Module where TModule : Module where TProgram : Program<TProgramInput, TProgramOutput>
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0002F0C5 File Offset: 0x0002D2C5
		public TargetLanguage TargetLanguage { get; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0002F0CD File Offset: 0x0002D2CD
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0002F0D5 File Offset: 0x0002D2D5
		public string HeaderModuleNameForCurrentTranslation { get; protected set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0002F0DE File Offset: 0x0002D2DE
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0002F0E6 File Offset: 0x0002D2E6
		private Stack<List<SSAStep>> CodeContextStack { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0002F0EF File Offset: 0x0002D2EF
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0002F0F7 File Offset: 0x0002D2F7
		protected TModule CurrentTranslationModule { get; set; }

		// Token: 0x0600107A RID: 4218 RVA: 0x0002F100 File Offset: 0x0002D300
		protected string GenerateNewName(string prefix, string suffix = null)
		{
			prefix = this.NormalizeVariableName(prefix ?? "t");
			suffix = this.NormalizeVariableName(suffix ?? "");
			string text;
			do
			{
				ulong nextUniqueIdentifier = this._nextUniqueIdentifier;
				this._nextUniqueIdentifier = nextUniqueIdentifier + 1UL;
				ulong num = nextUniqueIdentifier;
				text = FormattableString.Invariant(FormattableStringFactory.Create("{0}_{1:D8}{2}", new object[] { prefix, num, suffix }));
			}
			while (this.IsBoundIdentifier(text));
			return text;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002F175 File Offset: 0x0002D375
		protected Translator(TargetLanguage targetLanguage, IEnumerable<string> boundNames)
		{
			this.TargetLanguage = targetLanguage;
			this._boundNames = boundNames;
			this.Clear();
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0002F194 File Offset: 0x0002D394
		protected void Clear()
		{
			this.CodeContextStack = new Stack<List<SSAStep>>();
			this._nextUniqueIdentifier = 0UL;
			this._lambdaToNameMapping = new Dictionary<LambdaNode, SSAValue>();
			this._bindingStack = new Stack<IDictionary<string, SSAValue>>();
			if (this._boundNames != null)
			{
				this._bindingStack.Push(this._boundNames.ToDictionary((string name) => name, (string name) => null));
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002F226 File Offset: 0x0002D426
		protected void PushCodeContext()
		{
			this.CodeContextStack.Push(new List<SSAStep>());
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002F238 File Offset: 0x0002D438
		protected List<SSAStep> PopCodeContext()
		{
			return this.CodeContextStack.Pop();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002F245 File Offset: 0x0002D445
		protected List<SSAStep> PeekCodeContext()
		{
			return this.CodeContextStack.Peek();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002F252 File Offset: 0x0002D452
		protected void AddStep(SSAStep step)
		{
			this.PeekCodeContext().Add(step);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002F260 File Offset: 0x0002D460
		protected int GetContextLevel()
		{
			return this.CodeContextStack.Count;
		}

		// Token: 0x06001082 RID: 4226
		protected abstract string NormalizeVariableName(string originalVariableName);

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001083 RID: 4227
		protected abstract IReadOnlyDictionary<string, string> OperatorMapping { get; }

		// Token: 0x06001084 RID: 4228
		public abstract THeaderModule GenerateHeaderModule(TProgram p, string headerModuleName);

		// Token: 0x06001085 RID: 4229 RVA: 0x0002F270 File Offset: 0x0002D470
		public THeaderModule GenerateHeaderModule(string headerModuleName = "prose_semantics")
		{
			return this.GenerateHeaderModule(default(TProgram), headerModuleName);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002F28D File Offset: 0x0002D48D
		public string GenerateHeader(TProgram p, string headerModuleName, OptimizeFor optimization)
		{
			return this.GenerateHeaderModule(p, headerModuleName).GenerateCode(optimization);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002F2A2 File Offset: 0x0002D4A2
		public string GenerateUnisolatedHeader(TProgram p, OptimizeFor optimization)
		{
			return this.GenerateHeaderModule(p, null).GenerateUnisolatedCode(optimization);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002F2B7 File Offset: 0x0002D4B7
		public string GenerateHeader(string headerModuleName, OptimizeFor optimization)
		{
			return this.GenerateHeaderModule(headerModuleName).GenerateCode(optimization);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0002F2CC File Offset: 0x0002D4CC
		public string GenerateUnisolatedHeader(OptimizeFor optimization)
		{
			return this.GenerateUnisolatedHeader(default(TProgram), optimization);
		}

		// Token: 0x0600108A RID: 4234
		protected abstract Optional<string> GenerateLiteralRepresentation(object literalValue, Type literalType);

		// Token: 0x0600108B RID: 4235
		protected abstract SSAGeneratedFunction GenerateFunctionBody(IEnumerable<Record<string, Type>> formalParameters, Type returnType, ProgramNode functionBody, bool isLambda, OptimizeFor optimization);

		// Token: 0x0600108C RID: 4236 RVA: 0x0002F2E9 File Offset: 0x0002D4E9
		public void PushBindingScope(IDictionary<string, SSAValue> newBindings = null)
		{
			this._bindingStack.Push(newBindings ?? new Dictionary<string, SSAValue>());
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0002F300 File Offset: 0x0002D500
		public IDictionary<string, SSAValue> PopBindingScope()
		{
			return this._bindingStack.Pop();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0002F310 File Offset: 0x0002D510
		public SSAValue ResolveVariable(string variableName)
		{
			foreach (IDictionary<string, SSAValue> dictionary in this._bindingStack)
			{
				SSAValue ssavalue;
				dictionary.TryGetValue(variableName, out ssavalue);
				if (ssavalue != null)
				{
					return ssavalue;
				}
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve variable \"{0}\" during translation of generated program.", new object[] { variableName })));
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002F38C File Offset: 0x0002D58C
		public bool IsBoundIdentifier(string variableName)
		{
			return this._bindingStack.Any((IDictionary<string, SSAValue> dict) => dict.ContainsKey(variableName));
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002F3BD File Offset: 0x0002D5BD
		protected void BindVariable(string variableName, SSAValue binding, bool ignoreIfAlreadyBoundInScope = false)
		{
			if (!ignoreIfAlreadyBoundInScope || !this._bindingStack.Peek().ContainsKey(variableName))
			{
				this._bindingStack.Peek()[variableName] = binding;
			}
		}

		// Token: 0x06001091 RID: 4241
		public abstract GeneratedFunction Translate(TProgram root, TModule translationModule, OptimizeFor optimization);

		// Token: 0x06001092 RID: 4242 RVA: 0x00002188 File Offset: 0x00000388
		protected virtual ISubprogramTranslator<THeaderModule, TModule, TProgram, TProgramInput, TProgramOutput> GetSubprogramTranslatorFor(OptimizeFor optimization)
		{
			return null;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002F3E7 File Offset: 0x0002D5E7
		public void ApplyStandardOptimizations(IGeneratedFunction function, OptimizeFor optimization)
		{
			Translator<THeaderModule, TModule, TProgram, TProgramInput, TProgramOutput>.ApplyStaticStandardOptimizations(function, optimization);
			if (optimization == OptimizeFor.Readability)
			{
				function.Optimize(new VariableNameOptimizer(this._boundNames));
			}
			function.Optimize(new VariableNamesNormalizer(this._boundNames));
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002F418 File Offset: 0x0002D618
		private static void ApplyStaticStandardOptimizations(IGeneratedFunction function, OptimizeFor optimization)
		{
			IList<IOptimizer> list = new List<IOptimizer>();
			switch (optimization)
			{
			case OptimizeFor.Nothing:
				break;
			case OptimizeFor.Readability:
				list.Add(Inliner.ShallowOperatorsOnly);
				list.Add(ValueProp.Instance);
				list.Add(DeadCodeEliminator.Instance);
				list.Add(ConstantExpressionEvaluator.Instance);
				break;
			case OptimizeFor.Performance:
				list.Add(Uninliner.Instance);
				list.Add(ValueProp.Instance);
				list.Add(Inliner.Instance);
				list.Add(ValueProp.Instance);
				list.Add(DeadCodeEliminator.Instance);
				break;
			default:
				throw new ArgumentOutOfRangeException("optimization", optimization, null);
			}
			list.ForEach(delegate(IOptimizer opt)
			{
				function.Optimize(opt);
			});
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
		protected string ResolveOperator(string operatorName)
		{
			string text;
			if (this.OperatorMapping.TryGetValue(operatorName, out text))
			{
				return text;
			}
			if (this.TryResolveConcept(operatorName, out text))
			{
				return text;
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve operator named \"{0}\" during translation.", new object[] { operatorName })));
		}

		// Token: 0x06001096 RID: 4246
		protected abstract bool TryResolveConcept(string conceptName, out string resolvedName);

		// Token: 0x06001097 RID: 4247 RVA: 0x0002F524 File Offset: 0x0002D724
		protected virtual SSARValue GenerateFunctionApplication(NonterminalNode node, OptimizeFor optimization)
		{
			OperatorRule operatorRule = (OperatorRule)node.Rule;
			string text = this.ResolveOperator(operatorRule.Name);
			return this.CurrentTranslationModule.GenerateFunctionApplication(node, text, optimization, this);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002F560 File Offset: 0x0002D760
		private SSAValue TrySubProgramTranslation(ProgramNode subprogram, OptimizeFor optimization)
		{
			ISubprogramTranslator<THeaderModule, TModule, TProgram, TProgramInput, TProgramOutput> subprogramTranslatorFor = this.GetSubprogramTranslatorFor(optimization);
			if (subprogramTranslatorFor == null)
			{
				return null;
			}
			Optional<SubprogramTranslationResult> optional = subprogramTranslatorFor.Translate(subprogram, optimization, this);
			if (!optional.HasValue)
			{
				return null;
			}
			SubprogramTranslationResult value = optional.Value;
			foreach (Record<string, IGeneratedFunction> record in value.FunctionBindings)
			{
				this.CurrentTranslationModule.Bind(record.Item1, record.Item2);
			}
			foreach (SSAStep ssastep in value.SSASteps)
			{
				this.AddStep(ssastep);
			}
			Module module = this.CurrentTranslationModule;
			ImmutableSortedSet<string> imports = value.Imports;
			module.AddImports(((imports != null) ? imports.ToArray<string>() : null) ?? new string[0]);
			return value.ComputedValue;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002F668 File Offset: 0x0002D868
		public override SSAValue VisitNonterminal(NonterminalNode node, OptimizeFor optimization)
		{
			SSAValue ssavalue = this.TrySubProgramTranslation(node, optimization);
			if (ssavalue != null)
			{
				return ssavalue;
			}
			ConversionRule conversionRule = node.Rule as ConversionRule;
			if (conversionRule != null)
			{
				this.PushBindingScope(conversionRule.Substitutions.ToDictionary((KeyValuePair<Symbol, Symbol> p) => p.Key.Name, (KeyValuePair<Symbol, Symbol> p) => this.ResolveVariable(p.Value.Name)));
				SSAValue ssavalue2 = node.Children[0].AcceptVisitor<SSAValue, OptimizeFor>(this, optimization);
				this.PopBindingScope();
				return ssavalue2;
			}
			if (node.Rule is OperatorRule)
			{
				SSARegister ssaregister = new SSARegister(this.GenerateNewName(node.Symbol.ToString(), null), node.Rule.ReturnResolvedType, null);
				SSAStep ssastep = new SSAStep(ssaregister, this.GenerateFunctionApplication(node, optimization), "");
				this.AddStep(ssastep);
				return ssaregister;
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported Rule encountered while translating generated program\nRule: {0}", new object[] { node.Rule })));
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002F754 File Offset: 0x0002D954
		public override SSAValue VisitLet(LetNode node, OptimizeFor optimization)
		{
			SSAValue ssavalue = this.TrySubProgramTranslation(node, optimization);
			if (ssavalue != null)
			{
				return ssavalue;
			}
			this.PushBindingScope(null);
			SSAValue ssavalue2 = node.ValueNode.AcceptVisitor<SSAValue, OptimizeFor>(this, optimization);
			this.BindVariable(node.LetRule.Variable.Name, ssavalue2, false);
			SSAValue ssavalue3 = node.BodyNode.AcceptVisitor<SSAValue, OptimizeFor>(this, optimization);
			this.PopBindingScope();
			return ssavalue3;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
		public override SSAValue VisitLambda(LambdaNode node, OptimizeFor optimization)
		{
			SSAValue ssavalue = this.TrySubProgramTranslation(node, optimization);
			if (ssavalue != null)
			{
				return ssavalue;
			}
			LambdaRule lambdaRule = (LambdaRule)node.Rule;
			string name = lambdaRule.Variable.Name;
			Type resolvedType = lambdaRule.Variable.ResolvedType;
			Record<string, Type>[] array = new Record<string, Type>[]
			{
				new Record<string, Type>(name, resolvedType)
			};
			SSAValue ssavalue2;
			if (this._lambdaToNameMapping.TryGetValue(node, out ssavalue2))
			{
				return ssavalue2;
			}
			string text = this.GenerateNewName("t_lambda", null);
			SSAGeneratedFunction ssageneratedFunction = this.GenerateFunctionBody(array, node.BodyNode.GrammarRule.ReturnResolvedType, node.BodyNode, true, optimization);
			ssavalue2 = new SSARegister(text, node.BodyNode.GrammarRule.ReturnResolvedType, null);
			this.CurrentTranslationModule.BindLambda(text, ssageneratedFunction);
			this._lambdaToNameMapping[node] = ssavalue2;
			return ssavalue2;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002F884 File Offset: 0x0002DA84
		public override SSAValue VisitLiteral(LiteralNode node, OptimizeFor optimization)
		{
			SSAValue ssavalue = this.TrySubProgramTranslation(node, optimization);
			if (ssavalue != null)
			{
				return ssavalue;
			}
			Type returnResolvedType = node.GrammarRule.ReturnResolvedType;
			Optional<string> optional = this.GenerateLiteralRepresentation(node.Value, returnResolvedType);
			SSARegister ssaregister = new SSARegister(this.GenerateNewName(node.Symbol.ToString(), null), node.GrammarRule.ReturnResolvedType, null);
			if (optional.HasValue)
			{
				this.AddStep(new SSAStep(ssaregister, new SSALiteral(returnResolvedType, optional.Value), ""));
				return ssaregister;
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Could not translate literal {0} during translation of generated program.", new object[] { node })));
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002F924 File Offset: 0x0002DB24
		public override SSAValue VisitVariable(VariableNode node, OptimizeFor optimization)
		{
			SSAValue ssavalue = this.TrySubProgramTranslation(node, optimization);
			if (ssavalue != null)
			{
				return ssavalue;
			}
			SSAValue ssavalue2 = this.ResolveVariable(node.Symbol.Name);
			if (ssavalue2 == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve variable named \"{0}\" during translation of generated code.", new object[] { node.Symbol.Name })));
			}
			return ssavalue2;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002F97E File Offset: 0x0002DB7E
		public override SSAValue VisitHole(Hole node, OptimizeFor optimization)
		{
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Translation of holes is not supported.\nEncountered Hole: {0}", new object[] { node })));
		}

		// Token: 0x040007FB RID: 2043
		private Dictionary<LambdaNode, SSAValue> _lambdaToNameMapping;

		// Token: 0x040007FC RID: 2044
		private Stack<IDictionary<string, SSAValue>> _bindingStack;

		// Token: 0x040007FD RID: 2045
		private IEnumerable<string> _boundNames;

		// Token: 0x040007FE RID: 2046
		protected const string DefaultHeaderModuleName = "prose_semantics";

		// Token: 0x04000801 RID: 2049
		private ulong _nextUniqueIdentifier;
	}
}
