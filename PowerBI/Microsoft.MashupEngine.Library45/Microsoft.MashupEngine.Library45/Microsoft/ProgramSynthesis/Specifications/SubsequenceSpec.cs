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
	// Token: 0x0200036C RID: 876
	public class SubsequenceSpec : Spec
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x00038AEC File Offset: 0x00036CEC
		public SubsequenceSpec(State input, IEnumerable<object> positiveExamples, IEnumerable<object> negativeExamples = null)
			: this(new State[] { input })
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			dictionary[input] = positiveExamples.ToArray<object>();
			this.PositiveExamples = dictionary;
			this.PositiveSets = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
			Dictionary<State, HashSet<object>> dictionary2;
			if (negativeExamples != null)
			{
				(dictionary2 = new Dictionary<State, HashSet<object>>())[input] = negativeExamples.ConvertToHashSet(ValueEquality.Comparer);
			}
			else
			{
				dictionary2 = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => new HashSet<object>(ValueEquality.Comparer));
			}
			this.NegativeExamples = dictionary2;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00035F6A File Offset: 0x0003416A
		protected SubsequenceSpec(IEnumerable<State> inputs)
			: base(inputs, true)
		{
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00038BE0 File Offset: 0x00036DE0
		public SubsequenceSpec(IEnumerable<KeyValuePair<State, IEnumerable<object>>> positiveExamples)
			: this(positiveExamples.Select((KeyValuePair<State, IEnumerable<object>> e) => e.Key))
		{
			this.PositiveExamples = positiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value);
			this.PositiveSets = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
			this.NegativeExamples = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => new HashSet<object>(ValueEquality.Comparer));
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00038D00 File Offset: 0x00036F00
		public SubsequenceSpec(IEnumerable<KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>>> examples)
			: this(examples.Select((KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>> e) => e.Key))
		{
			KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>>[] array = (examples as KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>>[]) ?? examples.ToArray<KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>>>();
			this.PositiveExamples = array.ToDictionary((KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>> kvp) => kvp.Value.Item1);
			this.PositiveSets = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
			this.NegativeExamples = array.ToDictionary((KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>> kvp) => kvp.Key, delegate(KeyValuePair<State, Record<IEnumerable<object>, IEnumerable<object>>> kvp)
			{
				if (kvp.Value.Item2 != null)
				{
					return kvp.Value.Item2.ConvertToHashSet(ValueEquality.Comparer);
				}
				return new HashSet<object>(ValueEquality.Comparer);
			});
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00038E2C File Offset: 0x0003702C
		protected SubsequenceSpec(IReadOnlyList<KeyValuePair<State, IEnumerable<object>>> positiveExamples, IReadOnlyList<KeyValuePair<State, IEnumerable<object>>> negativeExamples)
			: this((from kvp in positiveExamples.Concat(negativeExamples)
				select kvp.Key).Distinct<State>())
		{
			this.PositiveExamples = positiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value);
			this.PositiveSets = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
			this.NegativeExamples = negativeExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00038F54 File Offset: 0x00037154
		public SubsequenceSpec(IDictionary<State, IEnumerable<object>> positiveExamples, IDictionary<State, HashSet<object>> negativeExamples)
			: base(positiveExamples.Keys.Concat(negativeExamples.Keys).Distinct<State>(), true)
		{
			this.PositiveExamples = positiveExamples;
			this.NegativeExamples = negativeExamples;
			this.PositiveSets = this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00038FDB File Offset: 0x000371DB
		protected IDictionary<State, HashSet<object>> PositiveSets { get; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00038FE3 File Offset: 0x000371E3
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x00038FEB File Offset: 0x000371EB
		public IDictionary<State, IEnumerable<object>> PositiveExamples { get; protected set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00038FF4 File Offset: 0x000371F4
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x00038FFC File Offset: 0x000371FC
		public IDictionary<State, HashSet<object>> NegativeExamples { get; protected set; }

		// Token: 0x0600136B RID: 4971 RVA: 0x00039008 File Offset: 0x00037208
		protected override bool CorrectOnProvided(State state, object output)
		{
			return output != null && !(output is Bottom) && this.PositiveSets[state].IsSubsetOf(output.ToEnumerable<object>()) && this.NegativeExamples[state].Intersect(output.ToEnumerable<object>()).IsEmpty<object>();
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0003905C File Offset: 0x0003725C
		protected override bool EqualsOnInput(State state, Spec other)
		{
			if (!(other is SubsequenceSpec))
			{
				return false;
			}
			SubsequenceSpec subsequenceSpec = (SubsequenceSpec)other;
			return this.PositiveSets[state].SetEquals(subsequenceSpec.PositiveSets[state]) && this.NegativeExamples[state].SetEquals(subsequenceSpec.NegativeExamples[state]);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000390B8 File Offset: 0x000372B8
		protected override int GetHashCodeOnInput(State state)
		{
			return ValueEquality.Comparer.GetHashCode(this.PositiveSets[state]) ^ ValueEquality.Comparer.GetHashCode(this.NegativeExamples[state]);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000390E8 File Offset: 0x000372E8
		public override string ToString()
		{
			if (!this.NegativeExamples.IsEmpty<KeyValuePair<State, HashSet<object>>>())
			{
				return this.PositiveExamples.Select((KeyValuePair<State, IEnumerable<object>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> ? ⊃ {1} ∧ ∃! {2}", new object[]
				{
					kvp.Key,
					kvp.Value.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null),
					this.NegativeExamples[kvp.Key].DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
				}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
			}
			return this.PositiveExamples.Select((KeyValuePair<State, IEnumerable<object>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> ? ⊃ {1}", new object[]
			{
				kvp.Key,
				kvp.Value.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
			}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00039170 File Offset: 0x00037370
		protected internal override Spec BottomToNull()
		{
			if (this.PositiveExamples.Values.All((IEnumerable<object> e) => !e.Contains(Bottom.Value)))
			{
				if (this.NegativeExamples.Values.All((HashSet<object> e) => !e.Contains(Bottom.Value)))
				{
					return this;
				}
			}
			return new SubsequenceSpec(this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				IEnumerable<object> value = kvp.Value;
				Func<object, object> func;
				if ((func = SubsequenceSpec.<>O.<0>__BottomToNull) == null)
				{
					func = (SubsequenceSpec.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				IEnumerable<object> enumerable = value.Select(func);
				IEnumerable<object> enumerable2 = this.NegativeExamples[kvp.Key];
				Func<object, object> func2;
				if ((func2 = SubsequenceSpec.<>O.<0>__BottomToNull) == null)
				{
					func2 = (SubsequenceSpec.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				return Record.Create<IEnumerable<object>, IEnumerable<object>>(enumerable, enumerable2.Select(func2));
			}));
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0003921C File Offset: 0x0003741C
		protected internal override Spec NullToBottom()
		{
			if (this.PositiveExamples.Values.All((IEnumerable<object> e) => !e.Contains(null)))
			{
				if (this.NegativeExamples.Values.All((HashSet<object> e) => !e.Contains(null)))
				{
					return this;
				}
			}
			return new SubsequenceSpec(this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				IEnumerable<object> value = kvp.Value;
				Func<object, object> func;
				if ((func = SubsequenceSpec.<>O.<1>__NullToBottom) == null)
				{
					func = (SubsequenceSpec.<>O.<1>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				IEnumerable<object> enumerable = value.Select(func);
				IEnumerable<object> enumerable2 = this.NegativeExamples[kvp.Key];
				Func<object, object> func2;
				if ((func2 = SubsequenceSpec.<>O.<1>__NullToBottom) == null)
				{
					func2 = (SubsequenceSpec.<>O.<1>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				return Record.Create<IEnumerable<object>, IEnumerable<object>>(enumerable, enumerable2.Select(func2));
			}));
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000392C8 File Offset: 0x000374C8
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("Seq", new object[]
			{
				this.PositiveExamples[input].CollectionToXML("Positives", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<object, XAttribute>>()),
				this.NegativeExamples[input].CollectionToXML("Negatives", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<object, XAttribute>>())
			});
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00039334 File Offset: 0x00037534
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new SubsequenceSpec(this.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => transformer(kvp.Key), (KeyValuePair<State, IEnumerable<object>> kvp) => Record.Create<IEnumerable<object>, IEnumerable<object>>(kvp.Value, this.NegativeExamples[kvp.Key].ToEnumerable<HashSet<object>>())));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00039380 File Offset: 0x00037580
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			Func<object, XElement> <>9__2;
			Func<object, XElement> <>9__3;
			return new XElement("SubsequenceSpec", new object[]
			{
				new XElement("PositiveExamples", this.PositiveExamples.Select(delegate(KeyValuePair<State, IEnumerable<object>> kvp)
				{
					XName xname = "Example";
					object[] array = new object[2];
					array[0] = kvp.Key.SerializeToXML(identityCache, context);
					int num = 1;
					XName xname2 = "Output";
					IEnumerable<object> value = kvp.Value;
					Func<object, XElement> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (object v) => context.SerializeObject(v, identityCache));
					}
					array[num] = new XElement(xname2, value.Select(func));
					return new XElement(xname, array);
				})),
				new XElement("NegativeExamples", this.NegativeExamples.Select(delegate(KeyValuePair<State, HashSet<object>> kvp)
				{
					XName xname3 = "Example";
					object[] array2 = new object[2];
					array2[0] = kvp.Key.SerializeToXML(identityCache, context);
					int num2 = 1;
					XName xname4 = "Output";
					IEnumerable<object> value2 = kvp.Value;
					Func<object, XElement> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = (object v) => context.SerializeObject(v, identityCache));
					}
					array2[num2] = new XElement(xname4, value2.Select(func2));
					return new XElement(xname3, array2);
				}))
			});
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00039408 File Offset: 0x00037608
		protected static void GetExamples(XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx, out List<KeyValuePair<State, IEnumerable<object>>> positives, out List<KeyValuePair<State, IEnumerable<object>>> negatives)
		{
			XElement xelement = node.Element("PositiveExamples");
			List<XElement> list = ((xelement != null) ? xelement.Elements().ToList<XElement>() : null);
			XElement xelement2 = node.Element("NegativeExamples");
			List<XElement> list2 = ((xelement2 != null) ? xelement2.Elements().ToList<XElement>() : null);
			if (list == null || list2 == null)
			{
				throw new InvalidOperationException();
			}
			Func<XElement, object> <>9__2;
			positives = list.Select(delegate(XElement exNode)
			{
				State state = State.DeserializeFromXML(exNode.Elements().First<XElement>(), ctx, idCache);
				XElement xelement3 = exNode.Element("Output");
				object obj;
				if (xelement3 == null)
				{
					obj = null;
				}
				else
				{
					IEnumerable<XElement> enumerable = xelement3.Elements();
					Func<XElement, object> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (XElement o) => ctx.DeserializeObject(o, idCache));
					}
					obj = enumerable.Select(func);
				}
				object obj2 = obj;
				if (obj2 == null)
				{
					throw new InvalidOperationException();
				}
				return new KeyValuePair<State, IEnumerable<object>>(state, obj2);
			}).ToList<KeyValuePair<State, IEnumerable<object>>>();
			Func<XElement, object> <>9__3;
			negatives = list2.Select(delegate(XElement exNode)
			{
				State state2 = State.DeserializeFromXML(exNode.Elements().First<XElement>(), ctx, idCache);
				XElement xelement4 = exNode.Element("Output");
				object obj3;
				if (xelement4 == null)
				{
					obj3 = null;
				}
				else
				{
					IEnumerable<XElement> enumerable2 = xelement4.Elements();
					Func<XElement, object> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = (XElement o) => ctx.DeserializeObject(o, idCache));
					}
					obj3 = enumerable2.Select(func2);
				}
				object obj4 = obj3;
				if (obj4 == null)
				{
					throw new InvalidOperationException();
				}
				return new KeyValuePair<State, IEnumerable<object>>(state2, obj4);
			}).ToList<KeyValuePair<State, IEnumerable<object>>>();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000394AC File Offset: 0x000376AC
		public static SubsequenceSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "SubsequenceSpec")
			{
				throw new InvalidOperationException();
			}
			List<KeyValuePair<State, IEnumerable<object>>> list;
			List<KeyValuePair<State, IEnumerable<object>>> list2;
			SubsequenceSpec.GetExamples(node, identityCache, context, out list, out list2);
			return new SubsequenceSpec(list, list2);
		}

		// Token: 0x0200036D RID: 877
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040009A6 RID: 2470
			public static Func<object, object> <0>__BottomToNull;

			// Token: 0x040009A7 RID: 2471
			public static Func<object, object> <1>__NullToBottom;
		}
	}
}
