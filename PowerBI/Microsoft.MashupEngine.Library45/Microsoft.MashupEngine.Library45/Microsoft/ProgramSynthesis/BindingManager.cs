using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000076 RID: 118
	internal class BindingManager
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public static BindingManager Standard(Grammar grammar)
		{
			return new BindingManager(grammar, Generators.Standard);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A7ED File Offset: 0x000089ED
		public BindingManager(Grammar grammar, Dictionary<Type, LiteralGenerator> stdGenerators = null)
		{
			this._grammar = grammar;
			this._stdGenerators = stdGenerators ?? new Dictionary<Type, LiteralGenerator>();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A818 File Offset: 0x00008A18
		public LiteralGenerator Generator(TerminalRule rule)
		{
			Dictionary<TerminalRule, Record<LiteralGenerator, LiteralGenerator>> generatorsCache = this._generatorsCache;
			LiteralGenerator literalGenerator;
			lock (generatorsCache)
			{
				Record<LiteralGenerator, LiteralGenerator> orAdd = this._generatorsCache.GetOrAdd(rule, (TerminalRule _) => this.ResolveGenerator(rule));
				literalGenerator = orAdd.Item2 ?? orAdd.Item1;
			}
			return literalGenerator;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A898 File Offset: 0x00008A98
		public LiteralGenerator ExplicitGenerator(TerminalRule rule)
		{
			Dictionary<TerminalRule, Record<LiteralGenerator, LiteralGenerator>> generatorsCache = this._generatorsCache;
			LiteralGenerator item;
			lock (generatorsCache)
			{
				item = this._generatorsCache.GetOrAdd(rule, (TerminalRule _) => this.ResolveGenerator(rule)).Item2;
			}
			return item;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A90C File Offset: 0x00008B0C
		private Record<LiteralGenerator, LiteralGenerator> ResolveGenerator(TerminalRule rule)
		{
			LiteralGenerator literalGenerator = null;
			LiteralGenerator literalGenerator2 = null;
			if (rule.IsInput)
			{
				return Record.Create<LiteralGenerator, LiteralGenerator>(literalGenerator, literalGenerator2);
			}
			string genRef = rule.GeneratorReference;
			if (string.IsNullOrEmpty(genRef))
			{
				this._stdGenerators.TryGetValue(rule.Head.ResolvedType, out literalGenerator);
				if (rule.ReturnResolvedType.IsArray)
				{
					literalGenerator = () => CollectionUtils.BuildArrays(this._stdGenerators[rule.ReturnResolvedType.GetElementType()](), 3);
				}
				else if (rule.ReturnResolvedType.IsEnum)
				{
					literalGenerator = () => Enum.GetValues(rule.ReturnResolvedType).Cast<object>();
				}
			}
			else
			{
				MemberInfo[] array = rule.Grammar.LearnerLocations.SelectMany((GrammarType l) => l.GetMember(genRef)).ToArray<MemberInfo>();
				if (array.Length > 1)
				{
					throw new AmbiguousMatchException(FormattableString.Invariant(FormattableStringFactory.Create("Ambiguous generator reference: {0}. More than one match found", new object[] { genRef })));
				}
				if (array.Length == 1)
				{
					MemberInfo memberInfo = array[0];
					if (memberInfo is MethodInfo)
					{
						literalGenerator2 = (memberInfo as MethodInfo).ToDelegate(false);
					}
					else if (memberInfo is PropertyInfo)
					{
						literalGenerator2 = (memberInfo as PropertyInfo).GetGetMethod().ToDelegate(false);
					}
					else if (memberInfo is FieldInfo)
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (!fieldInfo.IsStatic)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Field {0} should be static to be used as a generator.", new object[] { genRef })));
						}
						IEnumerable enumerable = fieldInfo.GetValue(null) as IEnumerable;
						if (enumerable == null)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Field {0} should be enumerable to be used as a generator.", new object[] { genRef })));
						}
						object[] value = enumerable.Cast<object>().ToArray<object>();
						literalGenerator2 = () => value;
					}
				}
			}
			return Record.Create<LiteralGenerator, LiteralGenerator>(literalGenerator, literalGenerator2);
		}

		// Token: 0x04000149 RID: 329
		private readonly Grammar _grammar;

		// Token: 0x0400014A RID: 330
		private readonly Dictionary<Type, LiteralGenerator> _stdGenerators;

		// Token: 0x0400014B RID: 331
		private readonly Dictionary<TerminalRule, Record<LiteralGenerator, LiteralGenerator>> _generatorsCache = new Dictionary<TerminalRule, Record<LiteralGenerator, LiteralGenerator>>();
	}
}
