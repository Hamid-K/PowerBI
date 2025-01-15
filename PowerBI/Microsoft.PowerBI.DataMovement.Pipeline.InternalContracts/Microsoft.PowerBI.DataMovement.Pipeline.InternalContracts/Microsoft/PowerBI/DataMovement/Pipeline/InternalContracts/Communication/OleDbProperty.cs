using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal abstract class OleDbProperty : OperationDataContract
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600018E RID: 398
		// (set) Token: 0x0600018F RID: 399
		[IgnoreDataMember]
		internal abstract object Value { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002E8A File Offset: 0x0000108A
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00002E92 File Offset: 0x00001092
		[DataMember(Name = "propertyGroup", IsRequired = true, EmitDefaultValue = false)]
		internal Guid PropertyGroup { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002E9B File Offset: 0x0000109B
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00002EA3 File Offset: 0x000010A3
		[DataMember(Name = "propertyId", IsRequired = true, EmitDefaultValue = false)]
		internal DBPROPID PropertyId { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00002EAC File Offset: 0x000010AC
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00002EB4 File Offset: 0x000010B4
		[DataMember(Name = "required", IsRequired = true, EmitDefaultValue = true)]
		internal bool Required { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002EBD File Offset: 0x000010BD
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00002EC5 File Offset: 0x000010C5
		[DataMember(Name = "readOnly", IsRequired = false, EmitDefaultValue = false)]
		internal bool ReadOnly { get; set; }

		// Token: 0x06000198 RID: 408
		internal abstract OleDbProperty Clone();
	}
}
