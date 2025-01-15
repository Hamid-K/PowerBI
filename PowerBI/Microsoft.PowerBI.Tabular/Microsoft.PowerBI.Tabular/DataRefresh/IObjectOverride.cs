using System;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000217 RID: 535
	internal interface IObjectOverride
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001E2E RID: 7726
		ObjectType ObjectType { get; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001E2F RID: 7727
		MetadataObject OriginalObject { get; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001E30 RID: 7728
		ObjectPath OriginalObjectPath { get; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001E31 RID: 7729
		ReplacementPropertiesCollection ReplacementProperties { get; }

		// Token: 0x06001E32 RID: 7730
		void EnsureAllReferencesResolved(Model model);

		// Token: 0x06001E33 RID: 7731
		bool ReadPropertyFromJson(JsonTextReader jsonReader);
	}
}
