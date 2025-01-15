using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Specifications
{
	// Token: 0x02000A61 RID: 2657
	public class BisectSpec : Spec
	{
		// Token: 0x060041F4 RID: 16884 RVA: 0x000CE4C8 File Offset: 0x000CC6C8
		public BisectSpec(IEnumerable<State> positives, IEnumerable<State> negatives)
			: base(positives.Concat(negatives), true)
		{
			this._positives = positives.ConvertToHashSet<State>();
			this._negatives = negatives.ConvertToHashSet<State>();
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x000CE4F0 File Offset: 0x000CC6F0
		public IReadOnlyList<State> LikelyNegatives
		{
			get
			{
				return this._negatives.ToList<State>();
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x000CE4FD File Offset: 0x000CC6FD
		public IReadOnlyList<State> Positives
		{
			get
			{
				return this._positives.ToList<State>();
			}
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000CE50C File Offset: 0x000CC70C
		public static BisectSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "BisectSpec")
			{
				throw new InvalidOperationException();
			}
			XElement xelement = node.Element("Positives");
			IEnumerable<State> enumerable = ((xelement != null) ? (from sNode in xelement.Elements()
				select State.DeserializeFromXML(sNode, context, identityCache)) : null);
			if (enumerable == null)
			{
				throw new InvalidOperationException();
			}
			XElement xelement2 = node.Element("Negatives");
			IEnumerable<State> enumerable2 = ((xelement2 != null) ? (from sNode in xelement2.Elements()
				select State.DeserializeFromXML(sNode, context, identityCache)) : null);
			if (enumerable2 == null)
			{
				throw new InvalidOperationException();
			}
			return new BisectSpec(enumerable, enumerable2);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x0000A5FD File Offset: 0x000087FD
		protected override bool CorrectOnProvided(State state, object output)
		{
			return true;
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000CE5C0 File Offset: 0x000CC7C0
		protected override bool EqualsOnInput(State state, Spec other)
		{
			BisectSpec bisectSpec = other as BisectSpec;
			return bisectSpec != null && this._positives.SetEquals(bisectSpec.Positives) && this._negatives.SetEquals(bisectSpec._negatives);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x0003995D File Offset: 0x00037B5D
		protected override int GetHashCodeOnInput(State state)
		{
			return 397;
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000CE5FD File Offset: 0x000CC7FD
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			if (this._positives.Contains(input))
			{
				return new XElement("Positives");
			}
			if (this._negatives.Contains(input))
			{
				return new XElement("LikelyNegatives");
			}
			return null;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000CE63C File Offset: 0x000CC83C
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			return new XElement("BisectSpec", new object[]
			{
				new XElement("Positives", this._positives.Select((State state) => state.SerializeToXML(identityCache, context))),
				new XElement("Negatives", this._negatives.Select((State state) => state.SerializeToXML(identityCache, context)))
			});
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000CE6C4 File Offset: 0x000CC8C4
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new BisectSpec(this._positives.Select(transformer), this._negatives.Select(transformer));
		}

		// Token: 0x04001DAB RID: 7595
		private readonly HashSet<State> _negatives;

		// Token: 0x04001DAC RID: 7596
		private readonly HashSet<State> _positives;
	}
}
