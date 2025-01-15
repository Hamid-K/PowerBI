using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007D2 RID: 2002
	[DataContract]
	public class FeatureInfo
	{
		// Token: 0x06002AA6 RID: 10918 RVA: 0x00077855 File Offset: 0x00075A55
		public FeatureInfo(Type propertyType, string name, IEnumerable<Type> holders, bool isComplete)
			: this(propertyType, name, isComplete)
		{
			this.Holders = (holders as IReadOnlyList<Type>) ?? holders.ToList<Type>();
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00077877 File Offset: 0x00075A77
		protected FeatureInfo(Type propertyType, string name, bool isComplete)
		{
			this.Name = name;
			this.PropertyType = propertyType;
			this.IsComplete = isComplete;
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x00077894 File Offset: 0x00075A94
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x0007789C File Offset: 0x00075A9C
		[DataMember]
		public Type PropertyType { get; private set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000778A5 File Offset: 0x00075AA5
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x000778AD File Offset: 0x00075AAD
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000778B6 File Offset: 0x00075AB6
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000778BE File Offset: 0x00075ABE
		[DataMember]
		internal IReadOnlyList<Type> Holders { get; private set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000778C7 File Offset: 0x00075AC7
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000778CF File Offset: 0x00075ACF
		[DataMember]
		public bool IsComplete { get; private set; }

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000778D8 File Offset: 0x00075AD8
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000778E0 File Offset: 0x00075AE0
		internal virtual void Validate(DiagnosticsContext diagnosticsContext, Grammar grammar)
		{
			foreach (Type type in this.Holders)
			{
				this.ValidateHolder(diagnosticsContext, grammar, type);
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00077930 File Offset: 0x00075B30
		private void ValidateHolder(DiagnosticsContext diagnosticsContext, Grammar grammar, Type holder)
		{
			Location.Assembly assembly = new Location.Assembly(holder);
			Dictionary<GrammarRule, FeatureCalculator> fcs = new Dictionary<GrammarRule, FeatureCalculator>();
			FeatureCalculator fc;
			foreach (MethodInfo methodInfo in holder.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				foreach (FeatureCalculatorAttribute featureCalculatorAttribute in methodInfo.GetCustomAttributes<FeatureCalculatorAttribute>())
				{
					if (featureCalculatorAttribute.RuleName != null)
					{
						if (!CSharpUtils.IsValidIdentifier(featureCalculatorAttribute.RuleName))
						{
							diagnosticsContext.AddDiagnostic(new Diagnostic.Features_InvalidRuleName(new Location.Assembly(methodInfo), new object[] { this, methodInfo.Name, featureCalculatorAttribute.RuleName }));
						}
						else
						{
							GrammarRule grammarRule = grammar.Rule(featureCalculatorAttribute.RuleName);
							if (grammarRule == null)
							{
								diagnosticsContext.AddDiagnostic(new Diagnostic.Features_UnknownRuleName(new Location.Assembly(methodInfo), new object[] { methodInfo.Name, featureCalculatorAttribute.RuleName, grammar.Name }));
							}
							else if (!methodInfo.ReturnType.IsConvertibleTo(this.PropertyType))
							{
								diagnosticsContext.AddDiagnostic(new Diagnostic.Features_IncompatibleCalculatorReturnType(new Location.Assembly(methodInfo), new object[]
								{
									methodInfo.Name,
									methodInfo.ReturnType.Name,
									this.PropertyType.Name
								}));
							}
							else
							{
								if (featureCalculatorAttribute.Method == CalculationMethod.FromChildrenFeatureValues || featureCalculatorAttribute.Method == CalculationMethod.FromChildrenNodes)
								{
									int num = ((featureCalculatorAttribute.SupportsLearningInfo > false) ? 1 : 0);
									if (methodInfo.GetParameters().Length - num != grammarRule.Body.Count)
									{
										diagnosticsContext.AddDiagnostic(new Diagnostic.Features_IncompatibleCalculatorParameters(new Location.Assembly(methodInfo), new object[]
										{
											methodInfo.Name,
											featureCalculatorAttribute.RuleName,
											methodInfo.GetParameters().Length,
											grammarRule.Body.Count + num
										}));
										continue;
									}
								}
								if (fcs.TryGetValue(grammarRule, out fc))
								{
									Location location = assembly;
									object[] array = new object[4];
									array[0] = this;
									array[1] = grammarRule;
									int num2 = 2;
									MethodInfo method = fc.Method;
									array[num2] = ((method != null) ? method.FullName() : null);
									array[3] = methodInfo.FullName();
									diagnosticsContext.AddDiagnostic(new Diagnostic.Features_AmbiguousFeatureCalculator(location, array));
								}
								else
								{
									fcs[grammarRule] = CustomFeatureCalculator.Resolve(this, methodInfo, grammarRule, featureCalculatorAttribute);
								}
							}
						}
					}
				}
			}
			using (IEnumerator<GrammarRule> enumerator2 = grammar.Rules.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					GrammarRule rule = enumerator2.Current;
					if (!fcs.ContainsKey(rule))
					{
						rule.BuildDefaultFeatureCalculator(this).SomeIfNotNull<FeatureCalculator>().Select(delegate(FeatureCalculator defaultFC)
						{
							fcs[rule] = defaultFC;
						});
					}
				}
			}
			foreach (FeatureCalculator featureCalculator in fcs.Values)
			{
				featureCalculator.Validate(diagnosticsContext);
			}
			if (this.IsComplete)
			{
				MethodInfo method2 = typeof(Feature<>).MakeGenericType(new Type[] { this.PropertyType }).GetMethod("GetFeatureValueForVariable", BindingFlags.Instance | BindingFlags.NonPublic);
				MethodInfo method3 = holder.GetMethod("GetFeatureValueForVariable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, (from p in method2.GetParameters()
					select p.ParameterType).ToArray<Type>());
				if (((method3 != null) ? method3.DeclaringType : null) != holder)
				{
					diagnosticsContext.AddDiagnostic(new Diagnostic.Features_VarDefaultNotGiven(assembly, new object[]
					{
						holder.CsName(true),
						this.Name,
						"GetFeatureValueForVariable",
						TypeUtils.GetDefaultValue(this.PropertyType)
					}));
				}
				using (IEnumerator<GrammarRule> enumerator2 = grammar.Rules.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GrammarRule grammarRule2 = enumerator2.Current;
						if (!grammarRule2.Head.IsInput && !fcs.TryGetValue(grammarRule2, out fc))
						{
							diagnosticsContext.AddDiagnostic(new Diagnostic.Features_NoFeatureCalculator(assembly, new object[] { this, grammarRule2, holder.FullName }));
						}
					}
					return;
				}
			}
			Stack<NonterminalRule> stack = new Stack<NonterminalRule>(from r in grammar.Rules.OfType<NonterminalRule>()
				where fcs.TryGetValue(r, out fc) && fc is CustomFeatureCalculator.FromChildrenFeatureValues
				select r);
			HashSet<NonterminalRule> hashSet = new HashSet<NonterminalRule>();
			while (stack.Any<NonterminalRule>())
			{
				NonterminalRule nonterminalRule = stack.Pop();
				if (!hashSet.Contains(nonterminalRule))
				{
					hashSet.Add(nonterminalRule);
					if (fcs.TryGetValue(nonterminalRule, out fc))
					{
						foreach (GrammarRule grammarRule3 in nonterminalRule.RequiredChildrenForFeatureCalculator(fc).SelectMany((Symbol sym) => sym.RHS))
						{
							if (!grammarRule3.Head.IsInput && !fcs.ContainsKey(grammarRule3))
							{
								diagnosticsContext.AddDiagnostic(new Diagnostic.Features_NoFeatureCalculator(assembly, new object[] { this, grammarRule3, holder.FullName }));
							}
							else if (grammarRule3 is NonterminalRule)
							{
								stack.Push((NonterminalRule)grammarRule3);
							}
						}
					}
				}
			}
		}
	}
}
