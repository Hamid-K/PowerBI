using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014C RID: 332
	public sealed class OperationSegmentParameter
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002AADF File Offset: 0x00028CDF
		public OperationSegmentParameter(string name, object value)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0002AB00 File Offset: 0x00028D00
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0002AB08 File Offset: 0x00028D08
		public string Name { get; private set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0002AB11 File Offset: 0x00028D11
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0002AB19 File Offset: 0x00028D19
		public object Value { get; private set; }
	}
}
