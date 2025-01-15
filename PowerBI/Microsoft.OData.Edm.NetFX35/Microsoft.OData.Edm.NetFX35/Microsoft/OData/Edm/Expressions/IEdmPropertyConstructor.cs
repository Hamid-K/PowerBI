using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x020000B1 RID: 177
	public interface IEdmPropertyConstructor : IEdmElement
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060002FF RID: 767
		string Name { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000300 RID: 768
		IEdmExpression Value { get; }
	}
}
