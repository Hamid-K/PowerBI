using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000104 RID: 260
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PhrasingTermBinding : TermBinding
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00009260 File Offset: 0x00007460
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00009268 File Offset: 0x00007468
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public PartOfSpeech PartOfSpeech { get; set; }

		// Token: 0x06000514 RID: 1300 RVA: 0x00009271 File Offset: 0x00007471
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000927A File Offset: 0x0000747A
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00009282 File Offset: 0x00007482
		internal CoverageKind CoverageKind { get; set; }
	}
}
