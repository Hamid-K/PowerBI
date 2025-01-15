using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000023 RID: 35
	public interface IEdmIncludeAnnotations
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A8 RID: 168
		string TermNamespace { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A9 RID: 169
		string Qualifier { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AA RID: 170
		string TargetNamespace { get; }
	}
}
