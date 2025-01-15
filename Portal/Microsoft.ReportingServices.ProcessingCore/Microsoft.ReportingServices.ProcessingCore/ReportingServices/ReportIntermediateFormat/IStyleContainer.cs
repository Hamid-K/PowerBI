using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004CF RID: 1231
	internal interface IStyleContainer
	{
		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x06003E7B RID: 15995
		Style StyleClass { get; }

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x06003E7C RID: 15996
		IInstancePath InstancePath { get; }

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x06003E7D RID: 15997
		ObjectType ObjectType { get; }

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x06003E7E RID: 15998
		string Name { get; }
	}
}
