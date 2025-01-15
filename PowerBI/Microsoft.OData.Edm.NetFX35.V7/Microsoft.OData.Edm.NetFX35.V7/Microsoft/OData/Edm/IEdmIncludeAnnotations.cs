using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A1 RID: 161
	public interface IEdmIncludeAnnotations
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600047A RID: 1146
		string TermNamespace { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600047B RID: 1147
		string Qualifier { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600047C RID: 1148
		string TargetNamespace { get; }
	}
}
