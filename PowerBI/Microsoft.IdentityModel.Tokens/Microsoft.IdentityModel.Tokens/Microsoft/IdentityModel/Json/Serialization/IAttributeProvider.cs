using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	internal interface IAttributeProvider
	{
		// Token: 0x06000683 RID: 1667
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x06000684 RID: 1668
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
