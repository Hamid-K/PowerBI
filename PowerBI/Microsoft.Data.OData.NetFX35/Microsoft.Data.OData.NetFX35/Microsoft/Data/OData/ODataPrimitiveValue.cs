using System;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000158 RID: 344
	public sealed class ODataPrimitiveValue : ODataValue
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001CA2F File Offset: 0x0001AC2F
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001CA37 File Offset: 0x0001AC37
		public object Value { get; private set; }
	}
}
