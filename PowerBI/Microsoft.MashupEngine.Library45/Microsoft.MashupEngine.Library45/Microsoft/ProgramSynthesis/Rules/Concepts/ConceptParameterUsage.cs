using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003B5 RID: 949
	[DataContract]
	public struct ConceptParameterUsage
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0003E799 File Offset: 0x0003C999
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0003E7A1 File Offset: 0x0003C9A1
		[DataMember]
		public Optional<int> RecursionLimit { readonly get; internal set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0003E7AA File Offset: 0x0003C9AA
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0003E7B2 File Offset: 0x0003C9B2
		[DataMember]
		public Symbol DSLParameter { readonly get; internal set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0003E7BB File Offset: 0x0003C9BB
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0003E7C3 File Offset: 0x0003C9C3
		[DataMember]
		public int ConceptIndex { readonly get; internal set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0003E7D4 File Offset: 0x0003C9D4
		[DataMember]
		public ParameterUsage Usage { readonly get; internal set; }
	}
}
