using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A37 RID: 6711
	internal sealed class PackageSectionConfigValidator : IPackageSectionConfigValidator
	{
		// Token: 0x0600A9B9 RID: 43449 RVA: 0x0000336E File Offset: 0x0000156E
		public void ValidatePackageSectionConfig(IPackageSectionConfig packageSectionConfig)
		{
		}

		// Token: 0x04005842 RID: 22594
		public static readonly IPackageSectionConfigValidator Instance = new PackageSectionConfigValidator();
	}
}
