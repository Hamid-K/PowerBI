using System;
using Microsoft.OData.Core.Metadata;

namespace Microsoft.OData.Core
{
	// Token: 0x02000192 RID: 402
	public sealed class ODataPrimitiveValue : ODataValue
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x00035101 File Offset: 0x00033301
		public ODataPrimitiveValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(Strings.ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull, null);
			}
			if (!EdmLibraryExtensions.IsPrimitiveType(value.GetType()))
			{
				throw new ODataException(Strings.ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType(value.GetType()));
			}
			this.Value = value;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0003513D File Offset: 0x0003333D
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x00035145 File Offset: 0x00033345
		public object Value { get; private set; }
	}
}
