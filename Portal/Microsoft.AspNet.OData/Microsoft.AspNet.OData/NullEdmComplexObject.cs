using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000041 RID: 65
	public class NullEdmComplexObject : IEdmComplexObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00007A5F File Offset: 0x00005C5F
		public NullEdmComplexObject(IEdmComplexTypeReference edmType)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			this._edmType = edmType;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007A7C File Offset: 0x00005C7C
		public bool TryGetPropertyValue(string propertyName, out object value)
		{
			throw Error.InvalidOperation(SRResources.EdmComplexObjectNullRef, new object[]
			{
				propertyName,
				this._edmType.ToTraceString()
			});
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public IEdmTypeReference GetEdmType()
		{
			return this._edmType;
		}

		// Token: 0x0400006B RID: 107
		private IEdmComplexTypeReference _edmType;
	}
}
