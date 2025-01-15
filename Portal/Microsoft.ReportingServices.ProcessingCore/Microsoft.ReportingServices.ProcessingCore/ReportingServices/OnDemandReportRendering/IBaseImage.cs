using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200033A RID: 826
	internal interface IBaseImage
	{
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06001ED5 RID: 7893
		Image.SourceType Source { get; }

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06001ED6 RID: 7894
		ReportProperty Value { get; }

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06001ED7 RID: 7895
		ReportStringProperty MIMEType { get; }

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06001ED8 RID: 7896
		string ImageDataPropertyName { get; }

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06001ED9 RID: 7897
		string ImageValuePropertyName { get; }

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06001EDA RID: 7898
		string MIMETypePropertyName { get; }

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06001EDB RID: 7899
		Image.EmbeddingModes EmbeddingMode { get; }

		// Token: 0x06001EDC RID: 7900
		byte[] GetImageData(out List<string> fieldsUsedInValue, out bool errorOccurred);

		// Token: 0x06001EDD RID: 7901
		string GetMIMETypeValue();

		// Token: 0x06001EDE RID: 7902
		string GetValueAsString(out List<string> fieldsUsedInValue, out bool errorOccured);

		// Token: 0x06001EDF RID: 7903
		string GetTransparentImageProperties(out string mimeType, out byte[] imageData);

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06001EE0 RID: 7904
		ObjectType ObjectType { get; }

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06001EE1 RID: 7905
		string ObjectName { get; }
	}
}
