using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000505 RID: 1285
	internal class CurrentWorkbookResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x060029D7 RID: 10711 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
		public CurrentWorkbookResourceKindInfo()
			: base("CurrentWorkbook", null, false, false, true, false, false, false, new AuthenticationInfo[0], null, null, null, new IDataSourceLocationFactory[] { CurrentWorkbookDataSourceLocation.Factory })
		{
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0007D31A File Offset: 0x0007B51A
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			if (resourcePath != string.Empty)
			{
				errorMessage = Strings.Resource_CurrentWorkbookPath_Invalid;
				resource = null;
				return false;
			}
			errorMessage = null;
			resource = new Resource(base.Kind, string.Empty, string.Empty);
			return true;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0007D355 File Offset: 0x0007B555
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			hostName = null;
			return false;
		}
	}
}
