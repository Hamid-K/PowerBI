using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200007D RID: 125
	public static class Extensions
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		public static IImmutableList<ConversionRule> ConversionRulesTo(this Symbol original, Symbol target)
		{
			if (original.Equals(target))
			{
				return ImmutableList<ConversionRule>.Empty;
			}
			List<IImmutableList<Record<Symbol, ConversionRule>>> list = new List<IImmutableList<Record<Symbol, ConversionRule>>> { ImmutableList<Record<Symbol, ConversionRule>>.Empty.Add(Record.Create<Symbol, ConversionRule>(target, null)) };
			while (list.Any<IImmutableList<Record<Symbol, ConversionRule>>>() && list.All((IImmutableList<Record<Symbol, ConversionRule>> p) => p.Last<Record<Symbol, ConversionRule>>().Item1 != original))
			{
				list = list.SelectMany((IImmutableList<Record<Symbol, ConversionRule>> p) => from r in p.Last<Record<Symbol, ConversionRule>>().Item1.RHS.OfType<ConversionRule>()
					select p.Add(Record.Create<Symbol, ConversionRule>(r.Body.Single<Symbol>(), r))).ToList<IImmutableList<Record<Symbol, ConversionRule>>>();
			}
			if (list.IsEmpty<IImmutableList<Record<Symbol, ConversionRule>>>())
			{
				return null;
			}
			return (from tup in list.First((IImmutableList<Record<Symbol, ConversionRule>> p) => p.Last<Record<Symbol, ConversionRule>>().Item1 == original).Skip(1).Reverse<Record<Symbol, ConversionRule>>()
				select tup.Item2).ToImmutableList<ConversionRule>();
		}
	}
}
