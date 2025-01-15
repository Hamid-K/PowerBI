using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x0200035C RID: 860
	public class OutputNotNullSpec : Spec
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x00035F6A File Offset: 0x0003416A
		public OutputNotNullSpec(IEnumerable<State> inputs)
			: base(inputs, true)
		{
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003756F File Offset: 0x0003576F
		protected override bool CorrectOnProvided(State state, object output)
		{
			return output != null;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00037578 File Offset: 0x00035778
		protected override bool EqualsOnInput(State state, Spec other)
		{
			OutputNotNullSpec outputNotNullSpec = other as OutputNotNullSpec;
			return !(outputNotNullSpec == null) && outputNotNullSpec.ProvidedInputs.Contains(state);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000375A3 File Offset: 0x000357A3
		protected override int GetHashCodeOnInput(State state)
		{
			return 999331;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000375AC File Offset: 0x000357AC
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			return new XElement("OutputNotNullSpec", base.ProvidedInputs.Select((State state) => state.SerializeToXML(identityCache, context)));
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000375F4 File Offset: 0x000357F4
		internal static OutputNotNullSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "OutputNotNullSpec")
			{
				throw new InvalidOperationException();
			}
			return new OutputNotNullSpec((from stateNode in node.Elements()
				select State.DeserializeFromXML(stateNode, context, identityCache)).ToList<State>());
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00037653 File Offset: 0x00035853
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return new XElement("OutputNotNullSpec");
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00037664 File Offset: 0x00035864
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new OutputNotNullSpec(base.ProvidedInputs.Select(transformer).ToList<State>());
		}
	}
}
