using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000337 RID: 823
	public class BooleanSoftSpec : BooleanExampleSpec
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x000356AD File Offset: 0x000338AD
		public BooleanSoftSpec(IDictionary<State, bool> examples)
			: base(examples)
		{
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000356B6 File Offset: 0x000338B6
		public BooleanSoftSpec(IDictionary<State, object> examples)
			: base(examples)
		{
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000357BC File Offset: 0x000339BC
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new BooleanSoftSpec(base.Selection.ToDictionary((KeyValuePair<State, bool> kvp) => transformer(kvp.Key), (KeyValuePair<State, bool> kvp) => kvp.Value));
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00035814 File Offset: 0x00033A14
		internal new static BooleanSoftSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			return new BooleanSoftSpec(DisjunctiveExamplesSpec.DeserializeToDictionary(node, identityCache, context).ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => (bool)kvp.Value.Single<object>()));
		}
	}
}
