using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ODataRoutePrefixAttribute : Attribute
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000F29F File Offset: 0x0000D49F
		public ODataRoutePrefixAttribute(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw Error.ArgumentNullOrEmpty("prefix");
			}
			this.Prefix = prefix;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000F2C1 File Offset: 0x0000D4C1
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0000F2C9 File Offset: 0x0000D4C9
		public string Prefix { get; private set; }
	}
}
