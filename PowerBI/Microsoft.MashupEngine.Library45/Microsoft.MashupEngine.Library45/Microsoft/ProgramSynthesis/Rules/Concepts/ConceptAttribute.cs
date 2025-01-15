using System;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003B7 RID: 951
	public class ConceptAttribute : Attribute
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x0003E7DD File Offset: 0x0003C9DD
		public ConceptAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		public string Name { get; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0003E7FC File Offset: 0x0003C9FC
		public bool Lazy { get; set; }
	}
}
