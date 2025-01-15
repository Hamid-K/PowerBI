using System;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000033 RID: 51
	[SystemResourceType(SystemResourceType.Brand)]
	internal sealed class BrandContentValidator : SystemResourcePackageContentRequiredValidator
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		internal BrandContentValidator()
			: base(new SystemResourcePackageContentValidationItem[]
			{
				new SystemResourcePackageContentValidationItem("stylesheet", "text/css", ".css")
			})
		{
		}
	}
}
