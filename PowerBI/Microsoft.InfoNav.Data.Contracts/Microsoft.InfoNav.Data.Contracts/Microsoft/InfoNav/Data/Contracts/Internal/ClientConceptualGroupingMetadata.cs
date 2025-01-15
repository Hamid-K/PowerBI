using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000158 RID: 344
	[DataContract(Name = "ConceptualGroupingMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualGroupingMetadata
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00012181 File Offset: 0x00010381
		internal ClientConceptualGroupingMetadata()
		{
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00012189 File Offset: 0x00010389
		internal ClientConceptualGroupingMetadata(IList<ClientConceptualGroupedColumnContainer> groupedColumns, ClientConceptualBinningMetadata binningDefinition)
		{
			this._groupedColumns = groupedColumns;
			this._binningDefinition = binningDefinition;
		}

		// Token: 0x04000450 RID: 1104
		[DataMember(Name = "GroupedColumns", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private IList<ClientConceptualGroupedColumnContainer> _groupedColumns;

		// Token: 0x04000451 RID: 1105
		[DataMember(Name = "BinningMetadata", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private ClientConceptualBinningMetadata _binningDefinition;
	}
}
