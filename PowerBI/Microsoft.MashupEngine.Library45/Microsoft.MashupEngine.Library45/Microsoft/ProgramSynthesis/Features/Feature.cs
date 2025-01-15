using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007C3 RID: 1987
	public abstract class Feature<T> : IFeature
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x00076E0C File Offset: 0x0007500C
		public bool UseComputedInputsForFccEquality { get; }

		// Token: 0x06002A53 RID: 10835 RVA: 0x00076E14 File Offset: 0x00075014
		protected Feature(Grammar grammar, string featureName, bool useComputedInputsForFccEquality = false, bool isComplete = false, Type[] holders = null, Feature<T>.FeatureInfoResolution resolution = Feature<T>.FeatureInfoResolution.Declared)
		{
			FeatureInfo featureInfo = grammar.Feature.MaybeGet(featureName).OrElseDefault<FeatureInfo>();
			this.UseComputedInputsForFccEquality = useComputedInputsForFccEquality;
			switch (resolution)
			{
			case Feature<T>.FeatureInfoResolution.Declared:
				if (featureInfo == null)
				{
					throw new FeatureMissingException(grammar, featureName);
				}
				goto IL_0099;
			case Feature<T>.FeatureInfoResolution.CreateIfMissing:
				if (featureInfo != null)
				{
					goto IL_0099;
				}
				break;
			case Feature<T>.FeatureInfoResolution.AlwaysCreate:
				break;
			default:
				throw new ArgumentOutOfRangeException("resolution", resolution, null);
			}
			Type[] array;
			if ((array = holders) == null)
			{
				(array = new Type[1])[0] = base.GetType();
			}
			holders = array;
			featureInfo = new FeatureInfo(typeof(T), featureName, holders, isComplete);
			IL_0099:
			this.Initialize(grammar, featureInfo);
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x00076EC2 File Offset: 0x000750C2
		// (set) Token: 0x06002A55 RID: 10837 RVA: 0x00076ECA File Offset: 0x000750CA
		public Grammar Grammar { get; private set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x00076ED3 File Offset: 0x000750D3
		public Dictionary<GrammarRule, FeatureCalculator> Calculator { get; } = new Dictionary<GrammarRule, FeatureCalculator>();

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00076EDB File Offset: 0x000750DB
		private Lazy<T> NullValue
		{
			get
			{
				return new Lazy<T>(() => (T)((object)TypeUtils.GetDefaultValue(typeof(T))), LazyThreadSafetyMode.ExecutionAndPublication);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x00076F02 File Offset: 0x00075102
		// (set) Token: 0x06002A59 RID: 10841 RVA: 0x00076F0A File Offset: 0x0007510A
		public FeatureInfo Info { get; private set; }

		// Token: 0x06002A5A RID: 10842 RVA: 0x00076F14 File Offset: 0x00075114
		public IFeature GetExternFeature(GrammarRule rule, int parameter)
		{
			return (from m in this._externMapping.MaybeGet(rule)
				select m[parameter]).OrElse(this);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00076F51 File Offset: 0x00075151
		object IFeature.GetFeatureValueForVariable(VariableNode variable)
		{
			return this.GetFeatureValueForVariable(variable);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00076F5F File Offset: 0x0007515F
		object IFeature.Calculate(ProgramNode program, LearningInfo learningInfo)
		{
			return this.Calculate(program, learningInfo);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x00076F6E File Offset: 0x0007516E
		protected virtual T GetFeatureValueForVariable(VariableNode variable)
		{
			return this.NullValue.Value;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x00076F7C File Offset: 0x0007517C
		public virtual T Calculate(ProgramNode program, LearningInfo learningInfo)
		{
			VariableNode variableNode = program as VariableNode;
			if (variableNode != null)
			{
				return this.GetFeatureValueForVariable(variableNode);
			}
			GrammarRule grammarRule = program.GrammarRule;
			if (grammarRule == null)
			{
				throw new FeatureUndefinedException(this.Info, program);
			}
			FeatureCalculator featureCalculator;
			if (!this.Calculator.TryGetValue(grammarRule, out featureCalculator))
			{
				throw new FeatureUndefinedException(this.Info, grammarRule, null);
			}
			ProgramNode programNode = program;
			ConceptRule conceptRule = grammarRule as ConceptRule;
			if (conceptRule != null)
			{
				program = conceptRule.BuildDslASTFromConceptAST(program);
			}
			return (T)((object)featureCalculator.Calculate(program, (learningInfo != null) ? learningInfo.WithProgramNode(programNode) : null, this));
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00077008 File Offset: 0x00075208
		private void Initialize(Grammar grammar, FeatureInfo info)
		{
			if (!typeof(T).IsConvertibleTo(info.PropertyType))
			{
				throw new ArgumentOutOfRangeException("info", FormattableString.Invariant(FormattableStringFactory.Create("Declared type {0} of feature {1} is incompatible with the result type {2} of the current feature calculator.", new object[]
				{
					info.PropertyType.CsName(true),
					info.Name,
					typeof(T).CsName(true)
				})));
			}
			this.Grammar = grammar;
			this.Info = info;
			using (IEnumerator<GrammarRule> enumerator = this.Grammar.Rules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GrammarRule rule = enumerator.Current;
					rule.BuildDefaultFeatureCalculator(this.Info).SomeIfNotNull<FeatureCalculator>().Select(delegate(FeatureCalculator defaultFC)
					{
						this.Calculator[rule] = defaultFC;
					});
				}
			}
			foreach (MethodInfo methodInfo in base.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				foreach (FeatureCalculatorAttribute featureCalculatorAttribute in methodInfo.GetCustomAttributes<FeatureCalculatorAttribute>())
				{
					string ruleName = featureCalculatorAttribute.RuleName;
					GrammarRule grammarRule = this.Grammar.Rule(ruleName);
					if (grammarRule != null)
					{
						this.Calculator[grammarRule] = CustomFeatureCalculator.Resolve(this.Info, methodInfo, grammarRule, featureCalculatorAttribute);
					}
				}
			}
			foreach (MemberInfo memberInfo in base.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				ExternFeatureMappingAttribute customAttribute = memberInfo.GetCustomAttribute<ExternFeatureMappingAttribute>();
				if (customAttribute != null)
				{
					GrammarRule grammarRule2 = this.Grammar.Rule(customAttribute.Rule);
					if (grammarRule2 != null)
					{
						IFeature[] orAdd = this._externMapping.GetOrAdd(grammarRule2, (GrammarRule r) => new IFeature[r.Body.Count]);
						MemberTypes memberType = memberInfo.MemberType;
						if (memberType != MemberTypes.Field)
						{
							if (memberType != MemberTypes.Method)
							{
								if (memberType != MemberTypes.Property)
								{
									throw new ArgumentOutOfRangeException();
								}
								IFeature[] array = orAdd;
								int parameter = customAttribute.Parameter;
								PropertyInfo propertyInfo = memberInfo as PropertyInfo;
								array[parameter] = ((propertyInfo != null) ? propertyInfo.GetValue(this) : null) as IFeature;
							}
							else
							{
								IFeature[] array2 = orAdd;
								int parameter2 = customAttribute.Parameter;
								MethodInfo methodInfo2 = memberInfo as MethodInfo;
								array2[parameter2] = ((methodInfo2 != null) ? methodInfo2.Invoke(this, new object[0]) : null) as IFeature;
							}
						}
						else
						{
							IFeature[] array3 = orAdd;
							int parameter3 = customAttribute.Parameter;
							FieldInfo fieldInfo = memberInfo as FieldInfo;
							array3[parameter3] = ((fieldInfo != null) ? fieldInfo.GetValue(this) : null) as IFeature;
						}
					}
				}
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000772AC File Offset: 0x000754AC
		public override string ToString()
		{
			return this.Info.Name;
		}

		// Token: 0x04001465 RID: 5221
		private readonly Dictionary<GrammarRule, IFeature[]> _externMapping = new Dictionary<GrammarRule, IFeature[]>();

		// Token: 0x020007C4 RID: 1988
		protected enum FeatureInfoResolution
		{
			// Token: 0x0400146B RID: 5227
			Declared,
			// Token: 0x0400146C RID: 5228
			CreateIfMissing,
			// Token: 0x0400146D RID: 5229
			AlwaysCreate
		}
	}
}
