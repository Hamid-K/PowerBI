using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200006A RID: 106
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract(Name = "error")]
	public sealed class PowerBIApiErrorObject
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00004680 File Offset: 0x00002880
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00004688 File Offset: 0x00002888
		[DataMember(IsRequired = false, Order = 10, Name = "code", EmitDefaultValue = false)]
		public string Code { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00004691 File Offset: 0x00002891
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00004699 File Offset: 0x00002899
		[DataMember(IsRequired = false, Order = 20, Name = "message", EmitDefaultValue = false)]
		public string Message { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000046A2 File Offset: 0x000028A2
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000046AA File Offset: 0x000028AA
		[DataMember(IsRequired = false, Order = 30, Name = "target", EmitDefaultValue = false)]
		public string Target { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000046B3 File Offset: 0x000028B3
		// (set) Token: 0x0600030D RID: 781 RVA: 0x000046BB File Offset: 0x000028BB
		[Nullable(new byte[] { 2, 1 })]
		[DataMember(IsRequired = false, Order = 40, Name = "details", EmitDefaultValue = false)]
		public IList<PowerBIApiErrorResponseDetail> Details
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000046C4 File Offset: 0x000028C4
		// (set) Token: 0x0600030F RID: 783 RVA: 0x000046CC File Offset: 0x000028CC
		[DataMember(IsRequired = false, Order = 50, Name = "innererror", EmitDefaultValue = false)]
		public PowerBIApiErrorResponseInnerError InnerError { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000310 RID: 784 RVA: 0x000046D5 File Offset: 0x000028D5
		// (set) Token: 0x06000311 RID: 785 RVA: 0x000046DD File Offset: 0x000028DD
		[DataMember(IsRequired = false, Order = 60, Name = "pbi.error", EmitDefaultValue = false)]
		public PowerBIErrorDetails PowerBIErrorDetails { get; set; }
	}
}
