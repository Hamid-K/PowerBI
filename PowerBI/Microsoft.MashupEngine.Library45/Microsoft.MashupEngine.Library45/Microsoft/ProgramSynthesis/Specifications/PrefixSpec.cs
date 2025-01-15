using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x0200035F RID: 863
	public class PrefixSpec : SubsequenceSpec
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x000376A4 File Offset: 0x000358A4
		public PrefixSpec(State input, IEnumerable<object> prefix, IEnumerable<object> negativeExamples = null)
			: base(input, prefix, negativeExamples)
		{
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000376AF File Offset: 0x000358AF
		public PrefixSpec(IEnumerable<KeyValuePair<State, IEnumerable<object>>> prefix)
			: base(prefix)
		{
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000376B8 File Offset: 0x000358B8
		public PrefixSpec(IEnumerable<KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>>> examples)
			: base(examples)
		{
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000376C1 File Offset: 0x000358C1
		private PrefixSpec(IReadOnlyList<KeyValuePair<State, IEnumerable<object>>> positiveExamples, IReadOnlyList<KeyValuePair<State, IEnumerable<object>>> negativeExamples)
			: base(positiveExamples, negativeExamples)
		{
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000376CC File Offset: 0x000358CC
		protected override bool CorrectOnProvided(State state, object output)
		{
			if (output == null || output is Bottom)
			{
				return false;
			}
			IEnumerator<object> enumerator = output.ToEnumerable<object>().GetEnumerator();
			IEnumerator<object> enumerator2 = base.PositiveExamples[state].GetEnumerator();
			HashSet<object> hashSet = base.NegativeExamples[state];
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			while (flag && (flag3 = enumerator2.MoveNext()) && (flag2 = enumerator.MoveNext()))
			{
				flag &= ValueEquality.Comparer.Equals(enumerator.Current, enumerator2.Current);
				if (hashSet.Contains(enumerator.Current, ValueEquality.Comparer))
				{
					return false;
				}
			}
			if (flag3 && !flag2)
			{
				return false;
			}
			while (enumerator.MoveNext())
			{
				if (hashSet.Contains(enumerator.Current, ValueEquality.Comparer))
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0003778C File Offset: 0x0003598C
		protected override bool EqualsOnInput(State state, Spec other)
		{
			if (!(other is PrefixSpec))
			{
				return false;
			}
			PrefixSpec prefixSpec = (PrefixSpec)other;
			return base.PositiveExamples[state].SequenceEqual(prefixSpec.PositiveExamples[state]) && base.NegativeExamples[state].SetEquals(prefixSpec.NegativeExamples[state]);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000377E8 File Offset: 0x000359E8
		protected override int GetHashCodeOnInput(State state)
		{
			return ValueEquality.Comparer.GetHashCode(base.PositiveExamples[state]) ^ ValueEquality.Comparer.GetHashCode(base.NegativeExamples[state]);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00037818 File Offset: 0x00035A18
		public override string ToString()
		{
			if (!base.NegativeExamples.IsEmpty<KeyValuePair<State, HashSet<object>>>())
			{
				return base.PositiveExamples.Select((KeyValuePair<State, IEnumerable<object>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> prefix {1} ∧ ∃! {2}", new object[]
				{
					kvp.Key,
					kvp.Value.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null),
					base.NegativeExamples[kvp.Key].DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
				}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
			}
			return base.PositiveExamples.Select((KeyValuePair<State, IEnumerable<object>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> prefix {1}", new object[]
			{
				kvp.Key,
				kvp.Value.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
			}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000378A0 File Offset: 0x00035AA0
		protected internal override Spec BottomToNull()
		{
			if (base.PositiveExamples.Values.All((IEnumerable<object> e) => !e.Contains(Bottom.Value)))
			{
				if (base.NegativeExamples.Values.All((HashSet<object> e) => !e.Contains(Bottom.Value)))
				{
					return this;
				}
			}
			return new PrefixSpec(base.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				IEnumerable<object> value = kvp.Value;
				Func<object, object> func;
				if ((func = PrefixSpec.<>O.<0>__BottomToNull) == null)
				{
					func = (PrefixSpec.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				IEnumerable<object> enumerable = value.Select(func);
				IEnumerable<object> enumerable2 = base.NegativeExamples[kvp.Key];
				Func<object, object> func2;
				if ((func2 = PrefixSpec.<>O.<0>__BottomToNull) == null)
				{
					func2 = (PrefixSpec.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				return Record.Create<IEnumerable<object>, IEnumerable<object>>(enumerable, enumerable2.Select(func2));
			}));
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0003794C File Offset: 0x00035B4C
		protected internal override Spec NullToBottom()
		{
			if (base.PositiveExamples.Values.All((IEnumerable<object> e) => !e.Contains(null)))
			{
				if (base.NegativeExamples.Values.All((HashSet<object> e) => !e.Contains(null)))
				{
					return this;
				}
			}
			return new PrefixSpec(base.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				IEnumerable<object> value = kvp.Value;
				Func<object, object> func;
				if ((func = PrefixSpec.<>O.<1>__NullToBottom) == null)
				{
					func = (PrefixSpec.<>O.<1>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				IEnumerable<object> enumerable = value.Select(func);
				IEnumerable<object> enumerable2 = base.NegativeExamples[kvp.Key];
				Func<object, object> func2;
				if ((func2 = PrefixSpec.<>O.<1>__NullToBottom) == null)
				{
					func2 = (PrefixSpec.<>O.<1>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				return Record.Create<IEnumerable<object>, IEnumerable<object>>(enumerable, enumerable2.Select(func2));
			}));
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000379F8 File Offset: 0x00035BF8
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("Prefix", new object[]
			{
				base.PositiveExamples[input].CollectionToXML("Positives", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<object, XAttribute>>()),
				base.NegativeExamples[input].CollectionToXML("Negatives", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<object, XAttribute>>())
			});
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00037A64 File Offset: 0x00035C64
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new PrefixSpec(base.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => transformer(kvp.Key), (KeyValuePair<State, IEnumerable<object>> kvp) => Record.Create<IEnumerable<object>, IEnumerable<object>>(kvp.Value, this.NegativeExamples[kvp.Key].ToEnumerable<HashSet<object>>())));
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00037AB0 File Offset: 0x00035CB0
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			IEnumerable<XElement> enumerable = base.SerializeImpl(identityCache, context).Elements();
			return new XElement("PrefixSpec", enumerable);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00037ADC File Offset: 0x00035CDC
		internal new static PrefixSpec DeserializeFromXML(XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx)
		{
			if (node.Name != "PrefixSpec")
			{
				throw new InvalidOperationException();
			}
			List<KeyValuePair<State, IEnumerable<object>>> list;
			List<KeyValuePair<State, IEnumerable<object>>> list2;
			SubsequenceSpec.GetExamples(node, idCache, ctx, out list, out list2);
			return new PrefixSpec(list, list2);
		}

		// Token: 0x02000360 RID: 864
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000975 RID: 2421
			public static Func<object, object> <0>__BottomToNull;

			// Token: 0x04000976 RID: 2422
			public static Func<object, object> <1>__NullToBottom;
		}
	}
}
