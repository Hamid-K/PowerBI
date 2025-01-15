using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000372 RID: 882
	public class TopSpec : Spec
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x00039938 File Offset: 0x00037B38
		private TopSpec()
			: base(Enumerable.Empty<State>(), true)
		{
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00039946 File Offset: 0x00037B46
		public static TopSpec Instance
		{
			get
			{
				return TopSpec.Lazy.Value;
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0000A5FD File Offset: 0x000087FD
		protected override bool CorrectOnProvided(State state, object output)
		{
			return true;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00039952 File Offset: 0x00037B52
		protected override bool EqualsOnInput(State state, Spec other)
		{
			return other is TopSpec;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0003995D File Offset: 0x00037B5D
		protected override int GetHashCodeOnInput(State state)
		{
			return 397;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00002188 File Offset: 0x00000388
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return null;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00004FAE File Offset: 0x000031AE
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return this;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00039964 File Offset: 0x00037B64
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			return new XElement("TopSpec");
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00039975 File Offset: 0x00037B75
		internal static TopSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "TopSpec")
			{
				throw new InvalidOperationException();
			}
			return TopSpec.Instance;
		}

		// Token: 0x040009D5 RID: 2517
		private static readonly Lazy<TopSpec> Lazy = new Lazy<TopSpec>(() => new TopSpec());
	}
}
