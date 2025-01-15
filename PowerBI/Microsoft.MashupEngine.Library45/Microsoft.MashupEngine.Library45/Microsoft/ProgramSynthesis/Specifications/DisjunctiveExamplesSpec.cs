using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x0200033A RID: 826
	public class DisjunctiveExamplesSpec : Spec
	{
		// Token: 0x06001237 RID: 4663 RVA: 0x00035894 File Offset: 0x00033A94
		public DisjunctiveExamplesSpec(IDictionary<State, IEnumerable<object>> examples)
			: base(examples.Keys, true)
		{
			this._examples = examples;
			this._exampleSets = examples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.ConvertToHashSet(ValueEquality.Comparer));
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x000358FF File Offset: 0x00033AFF
		public ReadOnlyDictionary<State, IEnumerable<object>> DisjunctiveExamples
		{
			get
			{
				return new ReadOnlyDictionary<State, IEnumerable<object>>(this._examples);
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0003590C File Offset: 0x00033B0C
		public static DisjunctiveExamplesSpec From(IDictionary<State, IEnumerable<object>> examples)
		{
			if (examples.Values.Any((IEnumerable<object> l) => !l.Any<object>()))
			{
				return null;
			}
			if (examples.Values.All((IEnumerable<object> l) => l.Count<object>() == 1))
			{
				return new ExampleSpec(examples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.First<object>()));
			}
			return new DisjunctiveExamplesSpec(examples);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000359C3 File Offset: 0x00033BC3
		protected override bool CorrectOnProvided(State state, object output)
		{
			return this._examples[state].Contains(output, ValueEquality.Comparer);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000359DC File Offset: 0x00033BDC
		protected override bool EqualsOnInput(State state, Spec other)
		{
			DisjunctiveExamplesSpec disjunctiveExamplesSpec = other as DisjunctiveExamplesSpec;
			return disjunctiveExamplesSpec != null && this._exampleSets[state].SetEquals(disjunctiveExamplesSpec.DisjunctiveExamples[state]);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00035A18 File Offset: 0x00033C18
		protected override int GetHashCodeOnInput(State state)
		{
			return this._examples[state].OrderIndependentHashCode(ValueEquality.Comparer);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00035A30 File Offset: 0x00033C30
		public override string ToString()
		{
			return this.DisjunctiveExamples.Select((KeyValuePair<State, IEnumerable<object>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> {1}", new object[]
			{
				kvp.Key,
				kvp.Value.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
			}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00035A80 File Offset: 0x00033C80
		protected internal override Spec NullToBottom()
		{
			if (!this.DisjunctiveExamples.Values.Any((IEnumerable<object> d) => d.Contains(null)))
			{
				return this;
			}
			return DisjunctiveExamplesSpec.From(this.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				IEnumerable<object> value = kvp.Value;
				Func<object, object> func;
				if ((func = DisjunctiveExamplesSpec.<>O.<0>__NullToBottom) == null)
				{
					func = (DisjunctiveExamplesSpec.<>O.<0>__NullToBottom = new Func<object, object>(ObjectUtils.NullToBottom));
				}
				return value.Select(func);
			}));
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00035B10 File Offset: 0x00033D10
		protected internal override Spec BottomToNull()
		{
			if (!this.DisjunctiveExamples.Values.Any((IEnumerable<object> d) => d.Contains(Bottom.Value)))
			{
				return this;
			}
			return DisjunctiveExamplesSpec.From(this.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				IEnumerable<object> value = kvp.Value;
				Func<object, object> func;
				if ((func = DisjunctiveExamplesSpec.<>O.<1>__BottomToNull) == null)
				{
					func = (DisjunctiveExamplesSpec.<>O.<1>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
				}
				return value.Select(func);
			}));
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00035BA0 File Offset: 0x00033DA0
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			Func<object, XElement> <>9__1;
			return new XElement(base.GetType().Name, this._exampleSets.Select(delegate(KeyValuePair<State, HashSet<object>> kvp)
			{
				XName xname = "Example";
				object[] array = new object[2];
				array[0] = kvp.Key.SerializeToXML(identityCache, context);
				int num = 1;
				XName xname2 = "Outputs";
				IEnumerable<object> value = kvp.Value;
				Func<object, XElement> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (object v) => context.SerializeObject(v, identityCache));
				}
				array[num] = new XElement(xname2, value.Select(func));
				return new XElement(xname, array);
			}));
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00035BF0 File Offset: 0x00033DF0
		protected static Dictionary<State, IEnumerable<object>> DeserializeToDictionary(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			Func<XElement, object> <>9__2;
			return node.Elements("Example").ToList<XElement>().Select(delegate(XElement ex)
			{
				XElement xelement = ex.Element("Outputs");
				if (xelement == null)
				{
					throw new InvalidOperationException();
				}
				XElement xelement2 = xelement;
				State state = State.DeserializeFromXML(ex.Elements().Single((XElement el) => el.Name != "Outputs"), context, identityCache);
				IEnumerable<XElement> enumerable = xelement2.Elements();
				Func<XElement, object> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (XElement n) => context.DeserializeObject(n, identityCache));
				}
				return new KeyValuePair<State, IEnumerable<object>>(state, enumerable.Select(func).ToList<object>());
			})
				.ToDictionary<State, IEnumerable<object>>();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00035C3C File Offset: 0x00033E3C
		internal static DisjunctiveExamplesSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "DisjunctiveExamplesSpec")
			{
				throw new InvalidOperationException();
			}
			return new DisjunctiveExamplesSpec(DisjunctiveExamplesSpec.DeserializeToDictionary(node, identityCache, context));
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00035C68 File Offset: 0x00033E68
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return this.DisjunctiveExamples[input].CollectionToXML("AnyOf", "Item", ObjectFormatting.ToString, identityCache, Array.Empty<Func<object, XAttribute>>());
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00035C8C File Offset: 0x00033E8C
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return DisjunctiveExamplesSpec.From(this.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => transformer(kvp.Key), (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value));
		}

		// Token: 0x040008FF RID: 2303
		private readonly IDictionary<State, IEnumerable<object>> _examples;

		// Token: 0x04000900 RID: 2304
		private readonly IDictionary<State, HashSet<object>> _exampleSets;

		// Token: 0x0200033B RID: 827
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000901 RID: 2305
			public static Func<object, object> <0>__NullToBottom;

			// Token: 0x04000902 RID: 2306
			public static Func<object, object> <1>__BottomToNull;
		}
	}
}
