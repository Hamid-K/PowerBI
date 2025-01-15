using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Lucia.Core.DomainModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200004B RID: 75
	[DataContract(Name = "ConceptualSchema", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ContractConceptualSchema
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00003FC3 File Offset: 0x000021C3
		internal ContractConceptualSchema(IList<ContractConceptualEntitySet> entities)
		{
			this._entities = entities;
		}

		// Token: 0x040000D9 RID: 217
		[DataMember(Name = "Entities", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private IList<ContractConceptualEntitySet> _entities;
	}
}
