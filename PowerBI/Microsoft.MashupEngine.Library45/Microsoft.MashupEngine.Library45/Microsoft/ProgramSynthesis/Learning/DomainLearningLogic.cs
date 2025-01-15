using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006B5 RID: 1717
	public abstract class DomainLearningLogic
	{
		// Token: 0x06002527 RID: 9511 RVA: 0x00067278 File Offset: 0x00065478
		protected DomainLearningLogic(Grammar grammar)
		{
			this.Grammar = grammar;
			foreach (MethodInfo methodInfo in base.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				foreach (WitnessFunctionAttribute witnessFunctionAttribute in methodInfo.GetCustomAttributes<WitnessFunctionAttribute>())
				{
					GrammarRule grammarRule = grammar.Rule(witnessFunctionAttribute.RuleName);
					if (grammarRule != null)
					{
						WitnessFunction witnessFunction = (methodInfo.IsStatic ? new WitnessFunction.Static(methodInfo, witnessFunctionAttribute, grammarRule) : new WitnessFunction.Instance(methodInfo, witnessFunctionAttribute, grammarRule, this));
						MultiValueDictionary<Type, WitnessFunction>[] array;
						if (!this._witnessFunctions.TryGetValue(grammarRule, out array))
						{
							array = (this._witnessFunctions[grammarRule] = new MultiValueDictionary<Type, WitnessFunction>[grammarRule.Body.Count]);
						}
						if (array[witnessFunction.ParameterIndex] == null)
						{
							array[witnessFunction.ParameterIndex] = new MultiValueDictionary<Type, WitnessFunction>();
						}
						array[witnessFunction.ParameterIndex].Add(witnessFunction.RuleSpecType, witnessFunction);
					}
				}
				foreach (TacticAttribute tacticAttribute in methodInfo.GetCustomAttributes<TacticAttribute>())
				{
					Symbol symbol = grammar.Symbol(tacticAttribute.Symbol);
					if (symbol == null)
					{
						throw new InvalidOperationException("Tactic symbol " + tacticAttribute.Symbol + " not found!");
					}
					if (this._tactics.ContainsKey(symbol))
					{
						throw new InvalidOperationException("A tactic for symbol " + tacticAttribute.Symbol + " already exists!");
					}
					this._tactics[symbol] = new CustomTactic(methodInfo, this);
				}
				foreach (DefaultTacticAttribute defaultTacticAttribute in methodInfo.GetCustomAttributes<DefaultTacticAttribute>())
				{
					if (this.DefaultTactics != null)
					{
						throw new InvalidOperationException("A default tactic already exists! Only one default tactic is allowed.");
					}
					this.DefaultTactics = new CustomTactic(methodInfo, this);
				}
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000674C4 File Offset: 0x000656C4
		public Grammar Grammar { get; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x000674CC File Offset: 0x000656CC
		internal ITactic DefaultTactics { get; }

		// Token: 0x0600252A RID: 9514 RVA: 0x000674D4 File Offset: 0x000656D4
		internal ITactic TacticFor(Symbol symbol)
		{
			return this._tactics.MaybeGet(symbol).OrElse(this.DefaultTactics);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000674F0 File Offset: 0x000656F0
		public IEnumerable<WitnessFunction> AllWitnessFunctionsFor(GrammarRule rule, int parameter)
		{
			return this._witnessFunctions.MaybeGet(rule).SelectMany(delegate(MultiValueDictionary<Type, WitnessFunction>[] wfs)
			{
				MultiValueDictionary<Type, WitnessFunction> multiValueDictionary = wfs[parameter];
				IEnumerable<WitnessFunction> enumerable;
				if (multiValueDictionary == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = multiValueDictionary.Values.SelectMany((IReadOnlyCollection<WitnessFunction> g) => g);
				}
				return enumerable ?? new WitnessFunction[0];
			});
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00067527 File Offset: 0x00065727
		public IEnumerable<WitnessFunction> WitnessFunctionsFor(GrammarRule rule, int parameter, Spec spec)
		{
			return this.WitnessFunctionsFor(rule, parameter, spec.GetType());
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00067537 File Offset: 0x00065737
		internal IEnumerable<WitnessFunction> WitnessFunctionsFor(GrammarRule rule, int parameter, Type specType)
		{
			Type t = specType;
			while (t != null)
			{
				MultiValueDictionary<Type, WitnessFunction>[] array;
				if (this._witnessFunctions.TryGetValue(rule, out array))
				{
					IReadOnlyCollection<WitnessFunction> readOnlyCollection = null;
					MultiValueDictionary<Type, WitnessFunction> multiValueDictionary = array[parameter];
					if (multiValueDictionary != null && multiValueDictionary.TryGetValue(t, out readOnlyCollection))
					{
						foreach (WitnessFunction witnessFunction in readOnlyCollection)
						{
							yield return witnessFunction;
						}
						IEnumerator<WitnessFunction> enumerator = null;
					}
				}
				t = t.GetTypeInfo().BaseType;
			}
			t = null;
			yield break;
			yield break;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x0006755C File Offset: 0x0006575C
		public DomainLearningLogic GetExternLearningLogicFor(GrammarRule rule)
		{
			DomainLearningLogic domainLearningLogic;
			if (this._externLogicCache.TryGetValue(rule, out domainLearningLogic))
			{
				return domainLearningLogic;
			}
			return this._externLogicCache[rule] = this._GetExternLearningLogicFor(rule);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00067594 File Offset: 0x00065794
		private DomainLearningLogic _GetExternLearningLogicFor(GrammarRule rule)
		{
			foreach (MemberInfo memberInfo in base.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ExternLearningLogicMappingAttribute customAttribute = memberInfo.GetCustomAttribute<ExternLearningLogicMappingAttribute>();
				if (!(((customAttribute != null) ? customAttribute.Rule : null) != rule.Id))
				{
					return memberInfo.GetMemberValue(this) as DomainLearningLogic;
				}
			}
			return null;
		}

		// Token: 0x040011AE RID: 4526
		private readonly Dictionary<GrammarRule, MultiValueDictionary<Type, WitnessFunction>[]> _witnessFunctions = new Dictionary<GrammarRule, MultiValueDictionary<Type, WitnessFunction>[]>();

		// Token: 0x040011AF RID: 4527
		private readonly Dictionary<Symbol, ITactic> _tactics = new Dictionary<Symbol, ITactic>();

		// Token: 0x040011B0 RID: 4528
		private readonly Dictionary<GrammarRule, DomainLearningLogic> _externLogicCache = new Dictionary<GrammarRule, DomainLearningLogic>();
	}
}
