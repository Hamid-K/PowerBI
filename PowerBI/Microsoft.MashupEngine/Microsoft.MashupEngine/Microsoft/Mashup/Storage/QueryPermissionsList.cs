using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002089 RID: 8329
	[XmlRoot("QueryPermissionsList")]
	public class QueryPermissionsList : XmlRoot
	{
		// Token: 0x0600CBD7 RID: 52183 RVA: 0x002895F5 File Offset: 0x002877F5
		public QueryPermissionsList()
		{
			this.queryPermissions = new List<QueryPermissionXml>();
		}

		// Token: 0x0600CBD8 RID: 52184 RVA: 0x00289608 File Offset: 0x00287808
		public QueryPermissionsList(List<QueryPermissionXml> queryPermissions)
		{
			this.queryPermissions = queryPermissions;
		}

		// Token: 0x17003115 RID: 12565
		// (get) Token: 0x0600CBD9 RID: 52185 RVA: 0x00289617 File Offset: 0x00287817
		[XmlArray("QueryPermissions")]
		[XmlArrayItem("QueryPermission")]
		public List<QueryPermissionXml> List
		{
			get
			{
				return this.queryPermissions;
			}
		}

		// Token: 0x04006767 RID: 26471
		private List<QueryPermissionXml> queryPermissions;
	}
}
