using System;

namespace Microsoft.OData
{
	// Token: 0x020000E4 RID: 228
	public sealed class ODataEnumValue : ODataValue
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001CAE5 File Offset: 0x0001ACE5
		public ODataEnumValue(string value)
		{
			this.Value = value;
			this.TypeName = null;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001CAFB File Offset: 0x0001ACFB
		public ODataEnumValue(string value, string typeName)
		{
			this.Value = value;
			this.TypeName = typeName;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001CB11 File Offset: 0x0001AD11
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0001CB19 File Offset: 0x0001AD19
		public string Value { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0001CB22 File Offset: 0x0001AD22
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0001CB2A File Offset: 0x0001AD2A
		public string TypeName { get; private set; }
	}
}
