using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019CA RID: 6602
	public sealed class DocumentValidator : IDocumentValidator
	{
		// Token: 0x0600A729 RID: 42793 RVA: 0x00229563 File Offset: 0x00227763
		public DocumentValidator(IPackageSectionConfigValidator sectionConfigValidator)
		{
			this.sectionConfigValidator = sectionConfigValidator;
		}

		// Token: 0x0600A72A RID: 42794 RVA: 0x00229574 File Offset: 0x00227774
		public void ValidateDocument(IPartitionedDocument document)
		{
			foreach (string text in document.Package.SectionNames)
			{
				IPackageSection section = document.Package.GetSection(text);
				this.sectionConfigValidator.ValidatePackageSectionConfig(section.Config);
			}
		}

		// Token: 0x04005703 RID: 22275
		private readonly IPackageSectionConfigValidator sectionConfigValidator;
	}
}
