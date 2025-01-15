using System;
using System.Collections.Generic;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x020000A0 RID: 160
	internal class ReflectionAttributeProvider : IAttributeProvider
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x00023B49 File Offset: 0x00021D49
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00023B63 File Offset: 0x00021D63
		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00023B72 File Offset: 0x00021D72
		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		// Token: 0x040002C9 RID: 713
		private readonly object _attributeProvider;
	}
}
