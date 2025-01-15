using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000108 RID: 264
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class RangeTermBinding : PropertyTermBaseBinding
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00009426 File Offset: 0x00007626
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0000942E File Offset: 0x0000762E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public Term LowerBoundTerm { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00009437 File Offset: 0x00007637
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0000943F File Offset: 0x0000763F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public Term UpperBoundTerm { get; set; }

		// Token: 0x06000532 RID: 1330 RVA: 0x00009448 File Offset: 0x00007648
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
