using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001C9F RID: 7327
	internal class PrefixOutputSpec : Spec
	{
		// Token: 0x0600F7B0 RID: 63408 RVA: 0x0034C0AC File Offset: 0x0034A2AC
		public PrefixOutputSpec(IDictionary<State, IEnumerable<StringPrefixSet>> examples)
			: base(examples.Keys, true)
		{
			this._examples = examples.ToDictionary((KeyValuePair<State, IEnumerable<StringPrefixSet>> t) => t.Key, (KeyValuePair<State, IEnumerable<StringPrefixSet>> t) => t.Value.ConvertToHashSet<StringPrefixSet>());
		}

		// Token: 0x0600F7B1 RID: 63409 RVA: 0x0034C110 File Offset: 0x0034A310
		private PrefixOutputSpec(Dictionary<State, HashSet<StringPrefixSet>> examples)
			: base(examples.Keys, true)
		{
			this._examples = examples;
		}

		// Token: 0x0600F7B2 RID: 63410 RVA: 0x0034C128 File Offset: 0x0034A328
		protected override bool CorrectOnProvided(State state, object output)
		{
			ValueSubstring vs = output as ValueSubstring;
			return vs != null && this._examples[state].Any((StringPrefixSet ps) => ps.Contains(vs));
		}

		// Token: 0x0600F7B3 RID: 63411 RVA: 0x0034C170 File Offset: 0x0034A370
		protected override bool EqualsOnInput(State state, Spec other)
		{
			PrefixOutputSpec prefixOutputSpec = other as PrefixOutputSpec;
			return prefixOutputSpec != null && this._examples[state].SetEquals(prefixOutputSpec._examples[state]);
		}

		// Token: 0x0600F7B4 RID: 63412 RVA: 0x0034C1A6 File Offset: 0x0034A3A6
		protected override int GetHashCodeOnInput(State state)
		{
			return this._examples[state].OrderIndependentHashCode<StringPrefixSet>();
		}

		// Token: 0x0600F7B5 RID: 63413 RVA: 0x0034C1B9 File Offset: 0x0034A3B9
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("PrefixSets", this._examples[input]);
		}

		// Token: 0x0600F7B6 RID: 63414 RVA: 0x0034C1D8 File Offset: 0x0034A3D8
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new PrefixOutputSpec(this._examples.ToDictionary((KeyValuePair<State, HashSet<StringPrefixSet>> ex) => transformer(ex.Key), (KeyValuePair<State, HashSet<StringPrefixSet>> ex) => ex.Value));
		}

		// Token: 0x0600F7B7 RID: 63415 RVA: 0x0034C230 File Offset: 0x0034A430
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			Func<StringPrefixSet, XElement> <>9__1;
			return new XElement("PrefixOutputSpec", this._examples.Select(delegate(KeyValuePair<State, HashSet<StringPrefixSet>> ex)
			{
				XName xname = "Example";
				object[] array = new object[2];
				array[0] = ex.Key.SerializeToXML(identityCache, context);
				int num = 1;
				XName xname2 = "Prefixes";
				IEnumerable<StringPrefixSet> value = ex.Value;
				Func<StringPrefixSet, XElement> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (StringPrefixSet prefix) => prefix.SerializeToXML(identityCache));
				}
				array[num] = new XElement(xname2, value.Select(func));
				return new XElement(xname, array);
			}));
		}

		// Token: 0x0600F7B8 RID: 63416 RVA: 0x0034C278 File Offset: 0x0034A478
		internal static PrefixOutputSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "PrefixOutputSpec")
			{
				throw new InvalidOperationException();
			}
			return new PrefixOutputSpec(node.Elements("Example").ToList<XElement>().ToDictionary((XElement exNode) => State.DeserializeFromXML(exNode.Elements().First<XElement>(), context), delegate(XElement exNode)
			{
				XElement xelement = exNode.Element("Prefixes");
				object obj;
				if (xelement == null)
				{
					obj = null;
				}
				else
				{
					obj = from vNode in xelement.Elements()
						select new StringPrefixSet(vNode.Value);
				}
				object obj2 = obj;
				if (obj2 == null)
				{
					throw new InvalidOperationException();
				}
				return obj2;
			}));
		}

		// Token: 0x04005BD0 RID: 23504
		private readonly Dictionary<State, HashSet<StringPrefixSet>> _examples;
	}
}
