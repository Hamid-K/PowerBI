using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200014F RID: 335
	[DataContract(Name = "ConceptualChangeDetectionMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualChangeDetectionMetadata
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x00011CEA File Offset: 0x0000FEEA
		internal ClientConceptualChangeDetectionMetadata()
		{
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00011CF2 File Offset: 0x0000FEF2
		internal ClientConceptualChangeDetectionMetadata(string refreshInterval)
		{
			this._refreshInterval = refreshInterval;
		}

		// Token: 0x04000416 RID: 1046
		[DataMember(Name = "RefreshInterval", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private readonly string _refreshInterval;
	}
}
