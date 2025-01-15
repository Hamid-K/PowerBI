using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000340 RID: 832
	public class DisjunctiveSubsequenceSpec : Spec
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x00035F48 File Offset: 0x00034148
		public DisjunctiveSubsequenceSpec(State input, IEnumerable<IEnumerable<object>> elements)
		{
			Dictionary<State, IEnumerable<IEnumerable<object>>> dictionary = new Dictionary<State, IEnumerable<IEnumerable<object>>>();
			dictionary[input] = elements;
			this..ctor(dictionary);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00035F6A File Offset: 0x0003416A
		protected DisjunctiveSubsequenceSpec(IEnumerable<State> inputs)
			: base(inputs, true)
		{
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00035F74 File Offset: 0x00034174
		public DisjunctiveSubsequenceSpec(IEnumerable<KeyValuePair<State, IEnumerable<IEnumerable<object>>>> examples)
			: base(examples.Select((KeyValuePair<State, IEnumerable<IEnumerable<object>>> e) => e.Key), true)
		{
			this.Examples = examples.ToDictionary((KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Value);
			this._exampleSets = this.Examples.ToDictionary((KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Value.Select((IEnumerable<object> v) => v.ConvertToHashSet(ValueEquality.Comparer)).ConvertToHashSet<HashSet<object>>());
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00036046 File Offset: 0x00034246
		public IDictionary<State, IEnumerable<IEnumerable<object>>> Examples { get; }

		// Token: 0x06001262 RID: 4706 RVA: 0x00036050 File Offset: 0x00034250
		protected override bool CorrectOnProvided(State state, object output)
		{
			return output != null && !(output is Bottom) && this._exampleSets[state].Any((HashSet<object> subset) => subset.IsSubsetOf(output.ToEnumerable<object>()));
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000360A0 File Offset: 0x000342A0
		protected override bool EqualsOnInput(State state, Spec other)
		{
			if (!(other is DisjunctiveSubsequenceSpec))
			{
				return false;
			}
			DisjunctiveSubsequenceSpec disjunctiveSubsequenceSpec = (DisjunctiveSubsequenceSpec)other;
			return this._exampleSets[state].SetEquals(disjunctiveSubsequenceSpec._exampleSets[state]);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000360DB File Offset: 0x000342DB
		protected override int GetHashCodeOnInput(State state)
		{
			return ValueEquality.Comparer.GetHashCode(this.Examples[state]);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000360F4 File Offset: 0x000342F4
		public override string ToString()
		{
			return this.Examples.Select((KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> (", new object[] { kvp.Key })) + string.Join(" ∨ ", kvp.Value.Select((IEnumerable<object> set) => FormattableString.Invariant(FormattableStringFactory.Create("? ⊃ {0}", new object[] { set.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null) })))) + ")").DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00036144 File Offset: 0x00034344
		protected internal override Spec BottomToNull()
		{
			if (!this.Examples.Values.Any((IEnumerable<IEnumerable<object>> s) => s.Any((IEnumerable<object> e) => e.Contains(Bottom.Value))))
			{
				return this;
			}
			return new DisjunctiveSubsequenceSpec(this.Examples.ToDictionary((KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Value.Select(delegate(IEnumerable<object> v)
			{
				Func<object, object> func;
				if ((func = DisjunctiveSubsequenceSpec.<>O.<0>__BottomToNull) == null)
				{
					func = (DisjunctiveSubsequenceSpec.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				return v.Select(func);
			})));
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000361D2 File Offset: 0x000343D2
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return this.Examples[input].CollectionToXML("Seq", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<IEnumerable<object>, XAttribute>>());
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000361F8 File Offset: 0x000343F8
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new DisjunctiveSubsequenceSpec(this.Examples.ToDictionary((KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => transformer(kvp.Key), (KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Value));
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00036250 File Offset: 0x00034450
		protected internal override Spec NullToBottom()
		{
			if (!this.Examples.Values.Any((IEnumerable<IEnumerable<object>> e) => e.Contains(null)))
			{
				return this;
			}
			return new DisjunctiveSubsequenceSpec(this.Examples.ToDictionary((KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<IEnumerable<object>>> kvp) => kvp.Value.Select(delegate(IEnumerable<object> v)
			{
				Func<object, object> func;
				if ((func = DisjunctiveSubsequenceSpec.<>O.<1>__NullToBottom) == null)
				{
					func = (DisjunctiveSubsequenceSpec.<>O.<1>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				return v.Select(func);
			})));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000362E0 File Offset: 0x000344E0
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			Func<object, XElement> <>9__2;
			Func<HashSet<object>, XElement> <>9__1;
			return new XElement("DisjunctiveSubsequenceSpec", this._exampleSets.Select(delegate(KeyValuePair<State, HashSet<HashSet<object>>> kvp)
			{
				XName xname = "Example";
				object[] array = new object[2];
				array[0] = kvp.Key.SerializeToXML(identityCache, context);
				int num = 1;
				XName xname2 = "OutputOptions";
				IEnumerable<HashSet<object>> value = kvp.Value;
				Func<HashSet<object>, XElement> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(HashSet<object> set)
					{
						XName xname3 = "Contains";
						Func<object, XElement> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (object v) => context.SerializeObject(v, identityCache));
						}
						return new XElement(xname3, set.Select(func2));
					});
				}
				array[num] = new XElement(xname2, value.Select(func));
				return new XElement(xname, array);
			}));
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00036328 File Offset: 0x00034528
		internal static DisjunctiveSubsequenceSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "DisjunctiveExamplesSpec")
			{
				throw new InvalidOperationException();
			}
			Func<XElement, object> <>9__2;
			Func<XElement, List<object>> <>9__1;
			return new DisjunctiveSubsequenceSpec(node.Elements().Select(delegate(XElement exNode)
			{
				List<XElement> list = exNode.Elements().ToList<XElement>();
				State state = State.DeserializeFromXML(list[0], context);
				IEnumerable<XElement> enumerable = list[1].Elements();
				Func<XElement, List<object>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(XElement option)
					{
						IEnumerable<XElement> enumerable2 = option.Elements();
						Func<XElement, object> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (XElement v) => context.DeserializeObject(v, identityCache));
						}
						return enumerable2.Select(func2).ToList<object>();
					});
				}
				List<List<object>> list2 = enumerable.Select(func).ToList<List<object>>();
				return new KeyValuePair<State, IEnumerable<IEnumerable<object>>>(state, list2);
			}).ToDictionary<State, IEnumerable<IEnumerable<object>>>());
		}

		// Token: 0x0400091A RID: 2330
		private readonly IDictionary<State, HashSet<HashSet<object>>> _exampleSets;

		// Token: 0x02000341 RID: 833
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400091C RID: 2332
			public static Func<object, object> <0>__BottomToNull;

			// Token: 0x0400091D RID: 2333
			public static Func<object, object> <1>__NullToBottom;
		}
	}
}
