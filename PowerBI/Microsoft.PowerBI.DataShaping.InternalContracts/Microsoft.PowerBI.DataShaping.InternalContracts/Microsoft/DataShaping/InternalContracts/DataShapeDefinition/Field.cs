using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200011C RID: 284
	[DataContract]
	internal sealed class Field
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0000F893 File Offset: 0x0000DA93
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0000F89B File Offset: 0x0000DA9B
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string DataField { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0000F8B5 File Offset: 0x0000DAB5
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0000F8BD File Offset: 0x0000DABD
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal string TargetRole { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000F8C6 File Offset: 0x0000DAC6
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0000F8CE File Offset: 0x0000DACE
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal SortInformation SortInformation { get; set; }
	}
}
