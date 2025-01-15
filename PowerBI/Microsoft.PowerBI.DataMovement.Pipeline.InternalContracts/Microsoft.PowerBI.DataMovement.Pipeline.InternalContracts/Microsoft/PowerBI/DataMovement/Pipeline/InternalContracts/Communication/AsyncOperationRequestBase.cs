using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200005C RID: 92
	[DataContract]
	public abstract class AsyncOperationRequestBase : OperationRequestBase
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000030B5 File Offset: 0x000012B5
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000030BD File Offset: 0x000012BD
		[DataMember(Name = "asyncOperationId", IsRequired = true, EmitDefaultValue = false)]
		public Guid AsyncOperationId { get; set; }
	}
}
