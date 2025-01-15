using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000374 RID: 884
	public class WithInputTopSpec : Spec
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x000399C8 File Offset: 0x00037BC8
		public WithInputTopSpec(IEnumerable<State> states)
			: base(states, false)
		{
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0000A5FD File Offset: 0x000087FD
		protected override bool CorrectOnProvided(State state, object output)
		{
			return true;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000399D2 File Offset: 0x00037BD2
		protected override bool EqualsOnInput(State state, Spec other)
		{
			return other is WithInputTopSpec;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0003995D File Offset: 0x00037B5D
		protected override int GetHashCodeOnInput(State state)
		{
			return 397;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00002188 File Offset: 0x00000388
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return null;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x000399DD File Offset: 0x00037BDD
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new WithInputTopSpec(base.ProvidedInputs.Select(transformer));
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000399F0 File Offset: 0x00037BF0
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			return new XElement("WithInputTopSpec", new XElement("Inputs", base.ProvidedInputs.Select((State v) => v.SerializeToXML(identityCache, context))));
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00039A48 File Offset: 0x00037C48
		internal static WithInputTopSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "WithInputTopSpec")
			{
				throw new InvalidOperationException();
			}
			XElement xelement = node.Element("Inputs");
			return new WithInputTopSpec((xelement != null) ? (from stateNode in xelement.Elements()
				select State.DeserializeFromXML(stateNode, context)) : null);
		}
	}
}
