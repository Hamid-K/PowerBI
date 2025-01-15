using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200018C RID: 396
	public class ODataPrimitiveValueMediaTypeMapping : ODataRawValueMediaTypeMapping
	{
		// Token: 0x06000D00 RID: 3328 RVA: 0x00032E8B File Offset: 0x0003108B
		public ODataPrimitiveValueMediaTypeMapping()
			: base("text/plain")
		{
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00033DB4 File Offset: 0x00031FB4
		protected override bool IsMatch(PropertySegment propertySegment)
		{
			return propertySegment != null && propertySegment.Property.Type.IsPrimitive() && !propertySegment.Property.Type.IsBinary();
		}
	}
}
