using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200006B RID: 107
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public sealed class PowerBIApiErrorResponse
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000046EE File Offset: 0x000028EE
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000046F6 File Offset: 0x000028F6
		[DataMember(IsRequired = false, Order = 70, Name = "error", EmitDefaultValue = false)]
		public PowerBIApiErrorObject Error { get; set; }

		// Token: 0x06000315 RID: 789 RVA: 0x000046FF File Offset: 0x000028FF
		public bool IsValid()
		{
			PowerBIApiErrorObject error = this.Error;
			return ((error != null) ? error.Code : null) != null;
		}
	}
}
