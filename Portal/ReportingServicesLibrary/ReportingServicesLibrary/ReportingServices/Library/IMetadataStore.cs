using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E2 RID: 738
	internal interface IMetadataStore
	{
		// Token: 0x06001A46 RID: 6726
		void AddMetadata(string name, string value);

		// Token: 0x06001A47 RID: 6727
		string GetMetadata(string name);

		// Token: 0x06001A48 RID: 6728
		void SetMetadata(string name, string value);
	}
}
