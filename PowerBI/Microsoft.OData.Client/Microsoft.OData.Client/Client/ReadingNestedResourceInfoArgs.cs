using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000042 RID: 66
	public sealed class ReadingNestedResourceInfoArgs
	{
		// Token: 0x06000203 RID: 515 RVA: 0x00008C38 File Offset: 0x00006E38
		public ReadingNestedResourceInfoArgs(ODataNestedResourceInfo link)
		{
			Util.CheckArgumentNull<ODataNestedResourceInfo>(link, "link");
			this.Link = link;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008C53 File Offset: 0x00006E53
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00008C5B File Offset: 0x00006E5B
		public ODataNestedResourceInfo Link { get; private set; }
	}
}
