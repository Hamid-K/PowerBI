using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000359 RID: 857
	public class OutputNonEmptySpec : Spec
	{
		// Token: 0x060012E3 RID: 4835 RVA: 0x0003741F File Offset: 0x0003561F
		public OutputNonEmptySpec(List<State> inputs)
			: base(inputs, true)
		{
			this._inputs = inputs;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00037430 File Offset: 0x00035630
		protected override bool CorrectOnProvided(State state, object output)
		{
			return output != null && output.ToEnumerable<object>().IsAny<object>();
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00037444 File Offset: 0x00035644
		protected override bool EqualsOnInput(State state, Spec other)
		{
			OutputNonEmptySpec outputNonEmptySpec = other as OutputNonEmptySpec;
			return !(outputNonEmptySpec == null) && outputNonEmptySpec.ProvidedInputs.Contains(state);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0003746F File Offset: 0x0003566F
		protected override int GetHashCodeOnInput(State state)
		{
			return 1000007;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00037476 File Offset: 0x00035676
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("OutputNonEmptySpec");
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00037487 File Offset: 0x00035687
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new OutputNonEmptySpec(this._inputs.Select(transformer).ToList<State>());
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000374A0 File Offset: 0x000356A0
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			return new XElement("OutputNonEmptySpec", this._inputs.Select((State state) => state.SerializeToXML(identityCache, context)));
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000374E8 File Offset: 0x000356E8
		internal static OutputNonEmptySpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "OutputNonEmptySpec")
			{
				throw new InvalidOperationException();
			}
			return new OutputNonEmptySpec((from v in node.Elements()
				select State.DeserializeFromXML(v, context, identityCache)).ToList<State>());
		}

		// Token: 0x0400096C RID: 2412
		private readonly List<State> _inputs;
	}
}
