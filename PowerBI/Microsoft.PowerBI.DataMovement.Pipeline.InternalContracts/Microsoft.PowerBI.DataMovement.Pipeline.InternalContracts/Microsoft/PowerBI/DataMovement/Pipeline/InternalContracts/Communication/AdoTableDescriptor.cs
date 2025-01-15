using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class AdoTableDescriptor
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000229C File Offset: 0x0000049C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000022A4 File Offset: 0x000004A4
		[DataMember(Name = "columnDescriptors", IsRequired = true, EmitDefaultValue = false)]
		public AdoColumnDescriptor[] ColumnDescriptors { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000022B5 File Offset: 0x000004B5
		[DataMember(Name = "extendedProperties", IsRequired = false, EmitDefaultValue = false)]
		public AdoExtendedProperty[] ExtendedProperties { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000022C6 File Offset: 0x000004C6
		[DataMember(Name = "recordsAffected", IsRequired = false, EmitDefaultValue = false)]
		public int RecordsAffected { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000022D7 File Offset: 0x000004D7
		[DataMember(Name = "outputParameters", IsRequired = false, EmitDefaultValue = false)]
		public AdoOutputParameter[] OutputParameters { get; set; }
	}
}
