using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000198 RID: 408
	public sealed class OperationSegmentParameter
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0003A8FB File Offset: 0x00038AFB
		public OperationSegmentParameter(string name, object value)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0003A91C File Offset: 0x00038B1C
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x0003A924 File Offset: 0x00038B24
		public string Name { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0003A92D File Offset: 0x00038B2D
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x0003A935 File Offset: 0x00038B35
		public object Value { get; private set; }
	}
}
