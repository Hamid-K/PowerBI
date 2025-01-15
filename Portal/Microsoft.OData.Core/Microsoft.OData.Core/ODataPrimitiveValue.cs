using System;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000E6 RID: 230
	public sealed class ODataPrimitiveValue : ODataValue
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001CB33 File Offset: 0x0001AD33
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0001CB6F File Offset: 0x0001AD6F
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0001CB77 File Offset: 0x0001AD77
		public object Value { get; private set; }
	}
}
