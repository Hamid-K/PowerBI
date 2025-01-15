using System;

namespace Microsoft.OData
{
	// Token: 0x0200005D RID: 93
	public sealed class ODataEnumValue : ODataValue
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00009F0A File Offset: 0x0000810A
		public ODataEnumValue(string value)
		{
			this.Value = value;
			this.TypeName = null;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009F20 File Offset: 0x00008120
		public ODataEnumValue(string value, string typeName)
		{
			this.Value = value;
			this.TypeName = typeName;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00009F36 File Offset: 0x00008136
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00009F3E File Offset: 0x0000813E
		public string Value { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00009F47 File Offset: 0x00008147
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00009F4F File Offset: 0x0000814F
		public string TypeName { get; private set; }
	}
}
