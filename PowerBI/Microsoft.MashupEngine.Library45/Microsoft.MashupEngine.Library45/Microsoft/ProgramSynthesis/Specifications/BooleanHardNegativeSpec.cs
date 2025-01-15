using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000334 RID: 820
	public class BooleanHardNegativeSpec : BooleanExampleSpec
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x000356AD File Offset: 0x000338AD
		public BooleanHardNegativeSpec(IDictionary<State, bool> examples)
			: base(examples)
		{
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000356B6 File Offset: 0x000338B6
		public BooleanHardNegativeSpec(IDictionary<State, object> examples)
			: base(examples)
		{
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000356C0 File Offset: 0x000338C0
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new BooleanHardNegativeSpec(base.Selection.ToDictionary((KeyValuePair<State, bool> kvp) => transformer(kvp.Key), (KeyValuePair<State, bool> kvp) => kvp.Value));
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00035718 File Offset: 0x00033918
		internal new static BooleanHardNegativeSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			return new BooleanHardNegativeSpec(DisjunctiveExamplesSpec.DeserializeToDictionary(node, identityCache, context).ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => (bool)kvp.Value.Single<object>()));
		}
	}
}
