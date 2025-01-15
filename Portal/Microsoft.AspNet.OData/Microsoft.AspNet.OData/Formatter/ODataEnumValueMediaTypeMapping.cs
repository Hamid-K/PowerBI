using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000188 RID: 392
	public class ODataEnumValueMediaTypeMapping : ODataRawValueMediaTypeMapping
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00032E8B File Offset: 0x0003108B
		public ODataEnumValueMediaTypeMapping()
			: base("text/plain")
		{
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00032E98 File Offset: 0x00031098
		protected override bool IsMatch(PropertySegment propertySegment)
		{
			return propertySegment != null && propertySegment.Property.Type.IsEnum();
		}
	}
}
