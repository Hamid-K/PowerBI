using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000721 RID: 1825
	internal abstract class EdmModelProcessorOutputBase
	{
		// Token: 0x0600365A RID: 13914 RVA: 0x000AD47D File Offset: 0x000AB67D
		protected EdmModelProcessorOutputBase()
		{
			this.TypeCatalog = new SortedDictionary<ODataSchemaItem, TypeValue>();
			this.CollectionUrls = new Dictionary<ODataSchemaItem, string>();
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000AD49B File Offset: 0x000AB69B
		// (set) Token: 0x0600365C RID: 13916 RVA: 0x000AD4A3 File Offset: 0x000AB6A3
		public IDictionary<ODataSchemaItem, TypeValue> TypeCatalog { get; set; }

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000AD4AC File Offset: 0x000AB6AC
		// (set) Token: 0x0600365E RID: 13918 RVA: 0x000AD4B4 File Offset: 0x000AB6B4
		public IDictionary<ODataSchemaItem, string> CollectionUrls { get; set; }

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000AD4BD File Offset: 0x000AB6BD
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x000AD4C5 File Offset: 0x000AB6C5
		public RecordValue TypeMetaValue { get; set; }
	}
}
