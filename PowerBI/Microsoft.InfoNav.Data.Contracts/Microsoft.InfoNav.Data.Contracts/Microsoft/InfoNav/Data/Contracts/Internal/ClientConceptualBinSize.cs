using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200014B RID: 331
	[DataContract(Name = "BinSize", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualBinSize
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x00011B88 File Offset: 0x0000FD88
		internal ClientConceptualBinSize()
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00011B90 File Offset: 0x0000FD90
		internal ClientConceptualBinSize(double value, ConceptualBinUnit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x040003EF RID: 1007
		[DataMember(Name = "Value", IsRequired = true, Order = 0)]
		private double _value;

		// Token: 0x040003F0 RID: 1008
		[DataMember(Name = "Unit", IsRequired = true, Order = 1)]
		private ConceptualBinUnit _unit;
	}
}
