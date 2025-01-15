using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x020000A1 RID: 161
	internal class ReflectionAttributeProvider : IAttributeProvider
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x000241AD File Offset: 0x000223AD
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000241C7 File Offset: 0x000223C7
		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000241D6 File Offset: 0x000223D6
		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		// Token: 0x040002E4 RID: 740
		private readonly object _attributeProvider;
	}
}
