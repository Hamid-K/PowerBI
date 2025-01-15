using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000156 RID: 342
	[DataContract(Name = "ConceptualEntitySource", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal class ClientConceptualEntitySource
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x000120BF File Offset: 0x000102BF
		internal ClientConceptualEntitySource()
		{
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000120C8 File Offset: 0x000102C8
		internal ClientConceptualEntitySource(ClientConceptualEntityMode mode = ClientConceptualEntityMode.Unknown, DateTime? refreshedTime = null, string directQuerySourceType = null, string directQuerySourceName = null)
		{
			this.Mode = mode;
			this.RefreshedTime = ((refreshedTime == null) ? null : refreshedTime.Value.ToString("O"));
			this.DirectQuerySourceType = directQuerySourceType;
			this.DirectQuerySourceName = directQuerySourceName;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00012117 File Offset: 0x00010317
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001211F File Offset: 0x0001031F
		[DataMember(Name = "Mode", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		internal ClientConceptualEntityMode Mode { get; private set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00012128 File Offset: 0x00010328
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00012130 File Offset: 0x00010330
		[DataMember(Name = "RefreshedTime", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		internal string RefreshedTime { get; private set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00012139 File Offset: 0x00010339
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00012141 File Offset: 0x00010341
		[DataMember(Name = "DirectQuerySourceType", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		internal string DirectQuerySourceType { get; private set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001214A File Offset: 0x0001034A
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00012152 File Offset: 0x00010352
		[DataMember(Name = "DirectQuerySourceName", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		internal string DirectQuerySourceName { get; private set; }
	}
}
