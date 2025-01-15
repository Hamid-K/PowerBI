using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200007E RID: 126
	internal interface IAttributeProvider
	{
		// Token: 0x06000679 RID: 1657
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x0600067A RID: 1658
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
