using System;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000088 RID: 136
	public sealed class ODataPrimitiveValue : ODataValue
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000EE01 File Offset: 0x0000D001
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000EE09 File Offset: 0x0000D009
		public object Value { get; private set; }
	}
}
