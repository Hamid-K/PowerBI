using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000BB RID: 187
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class FilterMetadata
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00006F2D File Offset: 0x0000512D
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00006F35 File Offset: 0x00005135
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public string Restatement { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00006F3E File Offset: 0x0000513E
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00006F46 File Offset: 0x00005146
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public FilterKind Kind { get; set; }

		// Token: 0x060003C8 RID: 968 RVA: 0x00006F4F File Offset: 0x0000514F
		public override string ToString()
		{
			if (this.Kind == FilterKind.Default)
			{
				return this.Restatement;
			}
			return StringUtil.FormatInvariant("{0} ({1})", this.Restatement, this.Kind);
		}
	}
}
