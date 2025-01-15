using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E20 RID: 7712
	public interface IPackage
	{
		// Token: 0x17002EC8 RID: 11976
		// (get) Token: 0x0600BE0F RID: 48655
		IEnumerable<string> SectionNames { get; }

		// Token: 0x0600BE10 RID: 48656
		IPackageSection GetSection(string sectionName);

		// Token: 0x0600BE11 RID: 48657
		IPackage ApplyEdits(IEnumerable<PackageEdit> edits);
	}
}
