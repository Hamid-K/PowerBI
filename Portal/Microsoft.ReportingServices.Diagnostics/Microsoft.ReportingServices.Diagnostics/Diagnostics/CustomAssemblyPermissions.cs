using System;
using System.Security.Policy;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000026 RID: 38
	internal class CustomAssemblyPermissions
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000033A9 File Offset: 0x000015A9
		internal CustomAssemblyPermissions(string xml)
		{
			this.Xml = xml;
		}

		// Token: 0x040000ED RID: 237
		internal string Xml;

		// Token: 0x040000EE RID: 238
		internal Evidence Evidence;
	}
}
