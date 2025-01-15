using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200006F RID: 111
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class PowerBIErrorDetails
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000047BB File Offset: 0x000029BB
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000047C3 File Offset: 0x000029C3
		[DataMember(IsRequired = true, Order = 10, Name = "code")]
		public string PowerBIErrorCode { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000047CC File Offset: 0x000029CC
		// (set) Token: 0x0600032B RID: 811 RVA: 0x000047D4 File Offset: 0x000029D4
		[Nullable(new byte[] { 2, 1, 1 })]
		[DataMember(IsRequired = false, Order = 20, Name = "parameters", EmitDefaultValue = false)]
		public IDictionary<string, string> ErrorParameters
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000047DD File Offset: 0x000029DD
		// (set) Token: 0x0600032D RID: 813 RVA: 0x000047E5 File Offset: 0x000029E5
		[Nullable(new byte[] { 2, 1 })]
		[DataMember(IsRequired = false, Order = 30, Name = "details", EmitDefaultValue = false)]
		public IList<PowerBIErrorDetail> Details
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000047EE File Offset: 0x000029EE
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000047F6 File Offset: 0x000029F6
		[DataMember(IsRequired = false, Order = 40, Name = "exceptionCulprit", EmitDefaultValue = false)]
		public ExceptionCulprit ExceptionCulprit { get; set; }
	}
}
