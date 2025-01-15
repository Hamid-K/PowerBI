using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000187 RID: 391
	public class ODataBinaryValueMediaTypeMapping : ODataRawValueMediaTypeMapping
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x00032E67 File Offset: 0x00031067
		public ODataBinaryValueMediaTypeMapping()
			: base("application/octet-stream")
		{
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00032E74 File Offset: 0x00031074
		protected override bool IsMatch(PropertySegment propertySegment)
		{
			return propertySegment != null && propertySegment.Property.Type.IsBinary();
		}
	}
}
