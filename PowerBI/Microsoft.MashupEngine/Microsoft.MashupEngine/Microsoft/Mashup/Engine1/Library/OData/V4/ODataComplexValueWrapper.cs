using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000872 RID: 2162
	internal class ODataComplexValueWrapper : IODataComplexValueWrapper
	{
		// Token: 0x06003E3D RID: 15933 RVA: 0x000CB7A8 File Offset: 0x000C99A8
		public ODataComplexValueWrapper(ODataComplexValue complexValue)
		{
			this.complexValue = complexValue;
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000CB7B7 File Offset: 0x000C99B7
		public string TypeName
		{
			get
			{
				return this.complexValue.TypeName;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000CB7C4 File Offset: 0x000C99C4
		public IEnumerable<IODataPropertyWrapper> Properties
		{
			get
			{
				return this.complexValue.Properties.Select((ODataProperty p) => new ODataPropertyWrapper(p));
			}
		}

		// Token: 0x040020BC RID: 8380
		private readonly ODataComplexValue complexValue;
	}
}
