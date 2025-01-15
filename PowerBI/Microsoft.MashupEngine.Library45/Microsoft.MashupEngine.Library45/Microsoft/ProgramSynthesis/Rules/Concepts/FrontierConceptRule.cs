using System;
using System.Runtime.Serialization;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003C5 RID: 965
	[DataContract]
	public abstract class FrontierConceptRule : ConceptRule
	{
		// Token: 0x06001592 RID: 5522 RVA: 0x0003EA31 File Offset: 0x0003CC31
		protected FrontierConceptRule(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}
	}
}
