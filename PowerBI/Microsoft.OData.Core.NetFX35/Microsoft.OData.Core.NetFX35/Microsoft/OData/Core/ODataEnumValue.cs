using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200016F RID: 367
	public sealed class ODataEnumValue : ODataValue
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0003144F File Offset: 0x0002F64F
		public ODataEnumValue(string value)
		{
			this.Value = value;
			this.TypeName = null;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00031465 File Offset: 0x0002F665
		public ODataEnumValue(string value, string typeName)
		{
			this.Value = value;
			this.TypeName = typeName;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003147B File Offset: 0x0002F67B
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x00031483 File Offset: 0x0002F683
		public string Value { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003148C File Offset: 0x0002F68C
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x00031494 File Offset: 0x0002F694
		public string TypeName { get; private set; }
	}
}
