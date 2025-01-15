using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000349 RID: 841
	public class ExampleWithNegativesSpec : ExampleSpec
	{
		// Token: 0x06001296 RID: 4758 RVA: 0x00036858 File Offset: 0x00034A58
		public ExampleWithNegativesSpec(IEnumerable<KeyValuePair<State, Record<object, IEnumerable<object>>>> examples)
			: base(examples.ToDictionary((KeyValuePair<State, Record<object, IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, Record<object, IEnumerable<object>>> kvp) => kvp.Value.Item1))
		{
			this.ExcludedElements = examples.ToDictionary((KeyValuePair<State, Record<object, IEnumerable<object>>> kvp) => kvp.Key, (KeyValuePair<State, Record<object, IEnumerable<object>>> kvp) => kvp.Value.Item2);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000368F9 File Offset: 0x00034AF9
		private ExampleWithNegativesSpec(Dictionary<State, object> examples, Dictionary<State, IEnumerable<object>> exclusions)
			: base(examples)
		{
			this.ExcludedElements = exclusions;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00036909 File Offset: 0x00034B09
		public IReadOnlyDictionary<State, IEnumerable<object>> ExcludedElements { get; }

		// Token: 0x06001299 RID: 4761 RVA: 0x00036914 File Offset: 0x00034B14
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new ExampleWithNegativesSpec(base.Examples.ToDictionary((KeyValuePair<State, object> kvp) => transformer(kvp.Key), (KeyValuePair<State, object> kvp) => Record.Create<object, IEnumerable<object>>(kvp.Value, this.ExcludedElements.MaybeGet(kvp.Key).OrElse(Enumerable.Empty<object>()))));
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00036960 File Offset: 0x00034B60
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			IEnumerable<XElement> enumerable = base.SerializeImpl(identityCache, context).Elements();
			Func<object, XElement> <>9__1;
			IEnumerable<XElement> enumerable2 = this.ExcludedElements.Select(delegate(KeyValuePair<State, IEnumerable<object>> kvp)
			{
				XName xname = "NegativeExample";
				object[] array = new object[2];
				array[0] = kvp.Key.SerializeToXML(identityCache, context);
				int num = 1;
				XName xname2 = "Exclusions";
				IEnumerable<object> value = kvp.Value;
				Func<object, XElement> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (object v) => context.SerializeObject(v, identityCache));
				}
				array[num] = new XElement(xname2, value.Select(func));
				return new XElement(xname, array);
			});
			return new XElement(base.GetType().Name, enumerable.Concat(enumerable2));
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000369D0 File Offset: 0x00034BD0
		internal new static ExampleWithNegativesSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			Dictionary<State, object> dictionary = DisjunctiveExamplesSpec.DeserializeToDictionary(node, identityCache, context).ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.Single<object>());
			Func<XElement, object> <>9__4;
			Dictionary<State, IEnumerable<object>> dictionary2 = node.Elements("NegativeExample").Select(delegate(XElement nex)
			{
				XElement xelement = nex.Elements().Single((XElement el) => el.Name != "Exclusions");
				IEnumerable<XElement> enumerable = nex.Elements("Exclusions");
				State state = State.DeserializeFromXML(xelement, context, identityCache);
				IEnumerable<XElement> enumerable2 = enumerable;
				Func<XElement, object> func;
				if ((func = <>9__4) == null)
				{
					func = (<>9__4 = (XElement ex) => context.DeserializeObject(ex, identityCache));
				}
				return new KeyValuePair<State, IEnumerable<object>>(state, enumerable2.Select(func));
			}).ToDictionary<State, IEnumerable<object>>();
			return new ExampleWithNegativesSpec(dictionary, dictionary2);
		}
	}
}
