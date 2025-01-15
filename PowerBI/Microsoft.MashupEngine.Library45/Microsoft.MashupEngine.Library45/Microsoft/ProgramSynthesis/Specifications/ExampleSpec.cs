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
	// Token: 0x02000347 RID: 839
	public class ExampleSpec : DisjunctiveExamplesSpec
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x00036710 File Offset: 0x00034910
		public ExampleSpec(IDictionary<State, object> examples)
			: base(examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => new object[] { kvp.Value }.AsEnumerable<object>()))
		{
			this._examples = examples;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0003676E File Offset: 0x0003496E
		public ReadOnlyDictionary<State, object> Examples
		{
			get
			{
				return new ReadOnlyDictionary<State, object>(this._examples);
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003677C File Offset: 0x0003497C
		public override string ToString()
		{
			return this.Examples.Select((KeyValuePair<State, object> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> {1}", new object[]
			{
				kvp.Key,
				kvp.Value.ToLiteral(null)
			}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000367C9 File Offset: 0x000349C9
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("Output", this.Examples[input].InternedFormat(identityCache, ObjectFormatting.ToString));
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000367ED File Offset: 0x000349ED
		internal new static ExampleSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			return (ExampleSpec)DisjunctiveExamplesSpec.From(DisjunctiveExamplesSpec.DeserializeToDictionary(node, identityCache, context));
		}

		// Token: 0x0400093B RID: 2363
		protected IDictionary<State, object> _examples;
	}
}
