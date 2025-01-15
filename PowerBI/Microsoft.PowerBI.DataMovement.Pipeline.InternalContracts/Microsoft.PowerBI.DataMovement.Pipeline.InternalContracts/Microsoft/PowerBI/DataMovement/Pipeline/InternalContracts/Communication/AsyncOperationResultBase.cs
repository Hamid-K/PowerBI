using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000064 RID: 100
	[DataContract]
	public abstract class AsyncOperationResultBase : OperationResultBase
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000031C5 File Offset: 0x000013C5
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000031CD File Offset: 0x000013CD
		[DataMember(Name = "asyncOperationId", IsRequired = true, EmitDefaultValue = false)]
		public Guid AsyncOperationId { get; set; }
	}
}
