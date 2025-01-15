using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007E RID: 126
	[NullableContext(1)]
	public interface IAttributeProvider
	{
		// Token: 0x06000682 RID: 1666
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x06000683 RID: 1667
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
