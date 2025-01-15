using System;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x0200002E RID: 46
	[SystemResourceType(SystemResourceType.UniversalBrand)]
	internal sealed class UniversalBrandContentValidator : SystemResourcePackageContentRequiredValidator
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000E285 File Offset: 0x0000C485
		internal UniversalBrandContentValidator()
			: base(new SystemResourcePackageContentValidationItem[]
			{
				new SystemResourcePackageContentValidationItem("colors", "application/json", ".json")
			})
		{
		}
	}
}
