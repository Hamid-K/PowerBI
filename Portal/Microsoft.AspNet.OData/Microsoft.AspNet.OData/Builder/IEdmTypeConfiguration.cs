using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000142 RID: 322
	public interface IEdmTypeConfiguration
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000BF4 RID: 3060
		Type ClrType { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000BF5 RID: 3061
		string FullName { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000BF6 RID: 3062
		string Namespace { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000BF7 RID: 3063
		string Name { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000BF8 RID: 3064
		EdmTypeKind Kind { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000BF9 RID: 3065
		ODataModelBuilder ModelBuilder { get; }
	}
}
