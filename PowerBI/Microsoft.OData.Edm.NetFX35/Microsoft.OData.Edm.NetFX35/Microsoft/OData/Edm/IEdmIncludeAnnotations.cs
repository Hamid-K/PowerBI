using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000EF RID: 239
	public interface IEdmIncludeAnnotations
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004C6 RID: 1222
		string TermNamespace { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004C7 RID: 1223
		string Qualifier { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060004C8 RID: 1224
		string TargetNamespace { get; }
	}
}
