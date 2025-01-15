using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008CA RID: 2250
	internal class ODataComplexValueWrapper : IODataComplexValueWrapper
	{
		// Token: 0x06004059 RID: 16473 RVA: 0x000D6DDF File Offset: 0x000D4FDF
		public ODataComplexValueWrapper(ODataComplexValue complexValue)
		{
			this.complexValue = complexValue;
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x000D6DEE File Offset: 0x000D4FEE
		public string TypeName
		{
			get
			{
				return this.complexValue.TypeName;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x000D6DFB File Offset: 0x000D4FFB
		public IEnumerable<IODataPropertyWrapper> Properties
		{
			get
			{
				return this.complexValue.Properties.Select((ODataProperty p) => new ODataPropertyWrapper(p));
			}
		}

		// Token: 0x040021CD RID: 8653
		private readonly ODataComplexValue complexValue;
	}
}
