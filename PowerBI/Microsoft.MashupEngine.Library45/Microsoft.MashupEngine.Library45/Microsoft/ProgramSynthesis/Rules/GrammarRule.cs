using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Features.InputTransformation;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x02000388 RID: 904
	[DataContract]
	[KnownType("GetKnownSubclassesOfGrammarRule")]
	public abstract class GrammarRule : ILanguage
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x0003ADBA File Offset: 0x00038FBA
		protected GrammarRule(Symbol head, IEnumerable<Symbol> body)
		{
			this.Head = head;
			this._body = new List<Symbol>(body);
			this.RecursionLimit = new Optional<int>[this._body.Count];
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0003ADF6 File Offset: 0x00038FF6
		protected GrammarRule(Symbol head, params Symbol[] body)
			: this(head, body.AsEnumerable<Symbol>())
		{
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0003AE05 File Offset: 0x00039005
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x0003AE0D File Offset: 0x0003900D
		[DataMember]
		public Symbol Head { get; internal set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0003AE16 File Offset: 0x00039016
		public ReadOnlyCollection<Symbol> Body
		{
			get
			{
				return this._body.AsReadOnly();
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0003AE23 File Offset: 0x00039023
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0003AE2B File Offset: 0x0003902B
		[DataMember]
		public string Id { get; internal set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0003AE34 File Offset: 0x00039034
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0003AE3C File Offset: 0x0003903C
		[DataMember]
		public bool Deprecated { get; internal set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0003AE45 File Offset: 0x00039045
		public GrammarType ReturnGrammarType
		{
			get
			{
				return this.Head.GrammarType;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0003AE52 File Offset: 0x00039052
		public Type ReturnResolvedType
		{
			get
			{
				return this.Head.ResolvedType;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0003AE5F File Offset: 0x0003905F
		public Grammar Grammar
		{
			get
			{
				return this.Head.Grammar;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003AE6C File Offset: 0x0003906C
		public IEnumerable<Symbol> Symbols
		{
			get
			{
				yield return this.Head;
				foreach (Symbol symbol in this.Body)
				{
					yield return symbol;
				}
				IEnumerator<Symbol> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0003AE7C File Offset: 0x0003907C
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x0003AE84 File Offset: 0x00039084
		internal Location OriginLocation { get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0003AE8D File Offset: 0x0003908D
		public Symbol LanguageSymbol
		{
			get
			{
				return this.Head;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001431 RID: 5169
		public abstract IEnumerable<ProgramNode> AllElements { get; }

		// Token: 0x06001432 RID: 5170 RVA: 0x0003AE95 File Offset: 0x00039095
		public ILanguage Intersect(ILanguage other)
		{
			if (!(other.LanguageSymbol != this.Head))
			{
				return other;
			}
			return null;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0003AEAD File Offset: 0x000390AD
		public ProgramSet Intersect(ProgramSet other)
		{
			if (!(other.LanguageSymbol != this.Head))
			{
				return other;
			}
			return ProgramSet.Empty(other.LanguageSymbol);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0003AECF File Offset: 0x000390CF
		private static IEnumerable<Type> GetKnownSubclassesOfGrammarRule()
		{
			return from type in typeof(GrammarRule).GetTypeInfo().Assembly.GetTypes()
				where typeof(GrammarRule).IsAssignableFrom(type)
				select type;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0003AF10 File Offset: 0x00039110
		internal virtual void Validate(DiagnosticsContext diagnosticsContext)
		{
			foreach (MethodInfo methodInfo in from learner in this.Grammar.LearnerLocations
				from method in learner.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				select method)
			{
				foreach (WitnessFunctionAttribute witnessFunctionAttribute in methodInfo.GetCustomAttributes<WitnessFunctionAttribute>())
				{
					if (witnessFunctionAttribute != null && !(witnessFunctionAttribute.RuleName != this.Id))
					{
						Location.Assembly assembly = new Location.Assembly(methodInfo);
						try
						{
							witnessFunctionAttribute.ParameterIndex(this);
						}
						catch (WitnessFunctionAttribute.ParameterOutOfRangeException)
						{
							diagnosticsContext.AddDiagnostic(new Diagnostic.DeductiveLearning_IncompatibleWitnessParameter(assembly, new object[] { methodInfo.Name, this }));
							continue;
						}
						Type parameterType = methodInfo.GetParameters()[0].ParameterType;
						Type parameterType2 = methodInfo.GetParameters()[1].ParameterType;
						if (!typeof(GrammarRule).IsAssignableFrom(parameterType) || !typeof(Spec).IsAssignableFrom(parameterType2))
						{
							diagnosticsContext.AddDiagnostic(new Diagnostic.DeductiveLearning_IncompatibleWitnessSignature(assembly, new object[] { methodInfo.Name }));
						}
						if (!parameterType.IsAssignableFrom(base.GetType()))
						{
							diagnosticsContext.AddDiagnostic(new Diagnostic.DeductiveLearning_IncompatibleWitnessRuleType(assembly, new object[]
							{
								methodInfo.Name,
								parameterType.Name,
								this,
								base.GetType().Name
							}));
						}
						if (methodInfo.GetParameters().Skip(2).Any((ParameterInfo p) => !typeof(Spec).IsAssignableFrom(p.ParameterType)))
						{
							diagnosticsContext.AddDiagnostic(new Diagnostic.DeductiveLearning_IncompatibleWitnessPrereqTypes(assembly, new object[]
							{
								methodInfo.Name,
								methodInfo.GetParameters().Length - 2
							}));
						}
					}
				}
			}
		}

		// Token: 0x06001436 RID: 5174
		public abstract TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args);

		// Token: 0x06001437 RID: 5175
		public abstract ProgramNode BuildASTNode(object data, params ProgramNode[] children);

		// Token: 0x06001438 RID: 5176 RVA: 0x0003B16C File Offset: 0x0003936C
		public ProgramNode BuildASTNode(params ProgramNode[] children)
		{
			switch (children.Length)
			{
			case 0:
				return this.BuildASTNode(null);
			case 1:
				return this.BuildASTNode(null, children[0]);
			case 2:
				return this.BuildASTNode(null, children[0], children[1]);
			default:
				return this.BuildASTNode(null, children);
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003B1BA File Offset: 0x000393BA
		public virtual ProgramNode BuildASTNode(object data, ProgramNode child1, ProgramNode child2, ProgramNode child3)
		{
			return this.BuildASTNode(data, new ProgramNode[] { child1, child2, child3 });
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003B1D6 File Offset: 0x000393D6
		public virtual ProgramNode BuildASTNode(object data, ProgramNode child1, ProgramNode child2)
		{
			return this.BuildASTNode(data, new ProgramNode[] { child1, child2 });
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003B1ED File Offset: 0x000393ED
		public virtual ProgramNode BuildASTNode(object data, ProgramNode child)
		{
			return this.BuildASTNode(data, new ProgramNode[] { child });
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003B200 File Offset: 0x00039400
		public virtual ProgramNode BuildASTNode(ProgramNode child)
		{
			return this.BuildASTNode(null, child);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0003B20A File Offset: 0x0003940A
		public virtual ProgramNode BuildASTNode(ProgramNode child1, ProgramNode child2)
		{
			return this.BuildASTNode(null, child1, child2);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0003B215 File Offset: 0x00039415
		public virtual ProgramNode BuildASTNode(ProgramNode child1, ProgramNode child2, ProgramNode child3)
		{
			return this.BuildASTNode(null, child1, child2, child3);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0003B221 File Offset: 0x00039421
		public virtual ProgramNode BuildASTNode(object data)
		{
			return this.BuildASTNode(data, GrammarRule.NoNodes);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0003B230 File Offset: 0x00039430
		private static ExampleSpec WitnessVariable(GrammarRule rule, int parameter, Spec spec)
		{
			IDictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				object obj;
				if (!state.TryGetValue(rule.Body[parameter], out obj))
				{
					return null;
				}
				dictionary.Add(state, obj);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003B2A8 File Offset: 0x000394A8
		private static TopSpec WitnessAll(GrammarRule rule, Spec spec)
		{
			return TopSpec.Instance;
		}

		// Token: 0x06001442 RID: 5186
		internal abstract FeatureCalculator BuildDefaultFeatureCalculator(FeatureInfo feature);

		// Token: 0x06001443 RID: 5187 RVA: 0x00002188 File Offset: 0x00000388
		public virtual IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			return null;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0003B2AF File Offset: 0x000394AF
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0003B2B7 File Offset: 0x000394B7
		[DataMember]
		public Optional<int>[] RecursionLimit { get; internal set; }

		// Token: 0x06001446 RID: 5190 RVA: 0x0003B2C0 File Offset: 0x000394C0
		internal IEnumerable<WitnessFunction> AllWitnessFunctionsFor(int parameter, DomainLearningLogic domainLogic = null)
		{
			this.InitializeWitnessFunctions(parameter);
			IEnumerable<WitnessFunction> enumerable = ((domainLogic != null) ? domainLogic.AllWitnessFunctionsFor(this, parameter) : null) ?? new WitnessFunction[0];
			Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> stdWitnessFunctions = this._stdWitnessFunctions;
			IEnumerable<WitnessFunction> enumerable3;
			lock (stdWitnessFunctions)
			{
				IEnumerable<WitnessFunction> enumerable2 = this._stdWitnessFunctions[parameter].Values.SelectMany((IReadOnlyCollection<WitnessFunction> g) => g);
				enumerable3 = enumerable.Concat(enumerable2).ToList<WitnessFunction>();
			}
			return enumerable3;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0003B360 File Offset: 0x00039560
		internal IEnumerable<WitnessFunction> WitnessFunctionsFor(int parameter, Spec spec, DomainLearningLogic domainLogic = null)
		{
			this.InitializeWitnessFunctions(parameter);
			Type specType = spec.GetType();
			if (domainLogic != null)
			{
				foreach (WitnessFunction witnessFunction in domainLogic.WitnessFunctionsFor(this, parameter, specType))
				{
					yield return witnessFunction;
				}
				IEnumerator<WitnessFunction> enumerator = null;
			}
			Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> dictionary = this._stdWitnessFunctions;
			lock (dictionary)
			{
				Type t = specType;
				while (t != null)
				{
					if (this._stdWitnessFunctions[parameter].ContainsKey(t))
					{
						foreach (WitnessFunction witnessFunction2 in this._stdWitnessFunctions[parameter][t])
						{
							yield return witnessFunction2;
						}
						IEnumerator<WitnessFunction> enumerator = null;
					}
					t = t.GetTypeInfo().BaseType;
				}
				t = null;
			}
			dictionary = null;
			yield break;
			yield break;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0003B388 File Offset: 0x00039588
		private void InitializeWitnessFunctions(int parameter)
		{
			if (parameter < 0 || parameter >= this.Body.Count)
			{
				throw new ArgumentOutOfRangeException("parameter");
			}
			Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> stdWitnessFunctions = this._stdWitnessFunctions;
			lock (stdWitnessFunctions)
			{
				if (!this._stdWitnessFunctions.ContainsKey(parameter))
				{
					this.InitializeStandardWitnessFunctions(parameter);
				}
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0003B3F8 File Offset: 0x000395F8
		protected virtual void InitializeStandardWitnessFunctions(int parameter)
		{
			Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> stdWitnessFunctions = this._stdWitnessFunctions;
			lock (stdWitnessFunctions)
			{
				this._stdWitnessFunctions[parameter] = new MultiValueDictionary<Type, WitnessFunction>();
			}
			if (this.Body[parameter].IsVariable)
			{
				this.AddStandardWitnessFunction((GrammarRule rule, Spec spec, Spec[] _) => GrammarRule.WitnessVariable(rule, parameter, spec), parameter, new WitnessFunctionAttribute(parameter));
			}
			this.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(GrammarRule.WitnessAll(GrammarRule, Spec)), new Expression[]
			{
				Expression.Constant(null, typeof(GrammarRule)),
				Expression.Constant(null, typeof(Spec))
			}), Array.Empty<ParameterExpression>()), parameter, new WitnessFunctionAttribute(parameter)
			{
				Verify = true
			});
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0003B4FC File Offset: 0x000396FC
		protected void AddStandardWitnessFunction(WitnessFunction.Static.Delegate witnessFunction, int parameter, WitnessFunctionAttribute attr)
		{
			MethodInfo methodInfo = witnessFunction.GetMethodInfo();
			WitnessFunction.Static @static = new WitnessFunction.Static(witnessFunction, methodInfo, attr, this);
			Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> stdWitnessFunctions = this._stdWitnessFunctions;
			lock (stdWitnessFunctions)
			{
				this._stdWitnessFunctions[parameter].Add(@static.RuleSpecType, @static);
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0003B560 File Offset: 0x00039760
		protected void AddStandardWitnessFunction(Expression<Action> surroundWitnessFunction, int parameter, WitnessFunctionAttribute attr = null)
		{
			this.AddStandardWitnessFunction(MethodUtils.GetInfo(surroundWitnessFunction), parameter, attr);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0003B570 File Offset: 0x00039770
		private void AddStandardWitnessFunction(MethodInfo method, int parameter, WitnessFunctionAttribute attr)
		{
			WitnessFunctionAttribute witnessFunctionAttribute;
			if ((witnessFunctionAttribute = attr) == null)
			{
				witnessFunctionAttribute = method.GetCustomAttributes<WitnessFunctionAttribute>().SingleOrDefault<WitnessFunctionAttribute>() ?? new WitnessFunctionAttribute(parameter);
			}
			attr = witnessFunctionAttribute;
			WitnessFunction.Static @static = new WitnessFunction.Static(method, attr, this);
			Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> stdWitnessFunctions = this._stdWitnessFunctions;
			lock (stdWitnessFunctions)
			{
				this._stdWitnessFunctions[parameter].Add(@static.RuleSpecType, @static);
			}
		}

		// Token: 0x040009F7 RID: 2551
		private static readonly ProgramNode[] NoNodes = new ProgramNode[0];

		// Token: 0x040009F8 RID: 2552
		[DataMember]
		private List<Symbol> _body;

		// Token: 0x040009FD RID: 2557
		[DataMember]
		private Dictionary<int, MultiValueDictionary<Type, WitnessFunction>> _stdWitnessFunctions = new Dictionary<int, MultiValueDictionary<Type, WitnessFunction>>();
	}
}
