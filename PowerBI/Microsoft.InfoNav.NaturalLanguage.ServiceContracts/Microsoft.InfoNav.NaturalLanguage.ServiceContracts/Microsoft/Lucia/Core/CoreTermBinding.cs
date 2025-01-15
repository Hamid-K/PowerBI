using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000FA RID: 250
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class CoreTermBinding : CoreTermBaseBinding
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00008ECC File Offset: 0x000070CC
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00008ED5 File Offset: 0x000070D5
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00008EDD File Offset: 0x000070DD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public CoverageKind CoverageKind { get; set; }
	}
}
