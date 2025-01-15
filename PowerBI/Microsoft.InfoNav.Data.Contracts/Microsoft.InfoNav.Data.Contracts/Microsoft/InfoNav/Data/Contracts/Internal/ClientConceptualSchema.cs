using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000164 RID: 356
	[DataContract(Name = "ConceptualSchema", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ClientConceptualSchema
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x000125E4 File Offset: 0x000107E4
		internal ClientConceptualSchema()
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000125EC File Offset: 0x000107EC
		internal ClientConceptualSchema(IList<ClientConceptualEntity> entities, bool canEdit, ClientConceptualCapabilities capabilities)
		{
			this.Entities = entities;
			this.CanEdit = canEdit;
			this.Capabilities = capabilities;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00012609 File Offset: 0x00010809
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00012611 File Offset: 0x00010811
		[DataMember(Name = "Entities", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		internal IList<ClientConceptualEntity> Entities { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001261A File Offset: 0x0001081A
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00012622 File Offset: 0x00010822
		[DataMember(Name = "canEdit", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		internal bool CanEdit { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001262B File Offset: 0x0001082B
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00012633 File Offset: 0x00010833
		[DataMember(Name = "Capabilities", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		internal ClientConceptualCapabilities Capabilities { get; private set; }
	}
}
