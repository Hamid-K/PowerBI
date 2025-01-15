using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200081C RID: 2076
	public interface IPackageBindListener
	{
		// Token: 0x060041BC RID: 16828
		void OnPackageBound(string packageXMLString, string rdbNam, string collid);

		// Token: 0x060041BD RID: 16829
		void OnPackageBound(XmlDocument xmldoc, string rdbNam, string collid);

		// Token: 0x060041BE RID: 16830
		void OnPackageBound(string packageXMLString, string rdbNam, string collid, out List<string> sqlScripts);

		// Token: 0x060041BF RID: 16831
		void OnPackageBound(XmlDocument xmldoc, string rdbNam, string collid, out List<string> sqlScripts);
	}
}
