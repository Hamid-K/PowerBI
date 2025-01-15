using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000856 RID: 2134
	public class PackageBindListener : IPackageBindListener
	{
		// Token: 0x0600440B RID: 17419 RVA: 0x000E5214 File Offset: 0x000E3414
		public virtual void OnPackageBound(string packageXMLString, string rdbNam, string collid, out List<string> sqlScripts)
		{
			sqlScripts = null;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000E5214 File Offset: 0x000E3414
		public virtual void OnPackageBound(XmlDocument xmldoc, string rdbNam, string collid, out List<string> sqlScripts)
		{
			sqlScripts = null;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000036A9 File Offset: 0x000018A9
		public virtual void OnPackageBound(string packageXMLString, string rdbNam, string collid)
		{
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000036A9 File Offset: 0x000018A9
		public virtual void OnPackageBound(XmlDocument xmldoc, string rdbNam, string collid)
		{
		}
	}
}
