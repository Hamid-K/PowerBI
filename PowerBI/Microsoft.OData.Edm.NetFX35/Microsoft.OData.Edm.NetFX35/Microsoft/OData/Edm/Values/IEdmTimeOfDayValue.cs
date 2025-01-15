using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000C1 RID: 193
	public interface IEdmTimeOfDayValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600033C RID: 828
		TimeOfDay Value { get; }
	}
}
