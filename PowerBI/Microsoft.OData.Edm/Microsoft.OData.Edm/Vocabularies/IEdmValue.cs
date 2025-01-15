using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000121 RID: 289
	public interface IEdmValue : IEdmElement
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600078B RID: 1931
		IEdmTypeReference Type { get; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600078C RID: 1932
		EdmValueKind ValueKind { get; }
	}
}
