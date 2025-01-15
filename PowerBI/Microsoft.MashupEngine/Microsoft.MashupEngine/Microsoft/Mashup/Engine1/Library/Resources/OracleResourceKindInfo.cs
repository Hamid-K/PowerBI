using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000507 RID: 1287
	internal class OracleResourceKindInfo : DatabaseResourceKindInfo
	{
		// Token: 0x060029DF RID: 10719 RVA: 0x0007D42C File Offset: 0x0007B62C
		public OracleResourceKindInfo(IEnumerable<AuthenticationInfo> authenticationInfo)
			: base("Oracle", null, false, false, true, authenticationInfo, null, null, new DataSourceLocationFactory[] { OracleDataSourceLocation.Factory })
		{
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0007D45C File Offset: 0x0007B65C
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			string text;
			string text2;
			if (DatabaseResource.TryParsePath(resourcePath, out text, out text2))
			{
				hostName = text.Split(new char[] { '/' })[0];
				return true;
			}
			hostName = null;
			return false;
		}
	}
}
