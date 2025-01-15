using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200014A RID: 330
	[DataContract(Name = "ConceptualBinningMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ClientConceptualBinningMetadata
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00011B71 File Offset: 0x0000FD71
		internal ClientConceptualBinningMetadata()
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00011B79 File Offset: 0x0000FD79
		internal ClientConceptualBinningMetadata(ClientConceptualBinSize binSize)
		{
			this._binSize = binSize;
		}

		// Token: 0x040003EE RID: 1006
		[DataMember(Name = "BinSize", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private ClientConceptualBinSize _binSize;
	}
}
