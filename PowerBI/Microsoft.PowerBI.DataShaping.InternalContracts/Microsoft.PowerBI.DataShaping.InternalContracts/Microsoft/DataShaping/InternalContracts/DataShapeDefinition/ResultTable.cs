using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200011B RID: 283
	[DataContract]
	internal sealed class ResultTable
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0000F858 File Offset: 0x0000DA58
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0000F860 File Offset: 0x0000DA60
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0000F869 File Offset: 0x0000DA69
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0000F871 File Offset: 0x0000DA71
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal bool IsReusable { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0000F87A File Offset: 0x0000DA7A
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0000F882 File Offset: 0x0000DA82
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<Field> Fields { get; set; }
	}
}
