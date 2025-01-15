using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x02000084 RID: 132
	public interface IEdmGuidValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000227 RID: 551
		Guid Value { get; }
	}
}
