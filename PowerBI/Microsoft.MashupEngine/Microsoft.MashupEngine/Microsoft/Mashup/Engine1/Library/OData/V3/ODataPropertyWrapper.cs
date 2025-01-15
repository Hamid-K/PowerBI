using System;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008C9 RID: 2249
	internal class ODataPropertyWrapper : IODataPropertyWrapper
	{
		// Token: 0x06004055 RID: 16469 RVA: 0x000D6DB1 File Offset: 0x000D4FB1
		public ODataPropertyWrapper(ODataProperty property)
		{
			this.property = property;
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x000D6DC0 File Offset: 0x000D4FC0
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000D6DCD File Offset: 0x000D4FCD
		public object Value
		{
			get
			{
				return ODataWrapperHelper.WrapValueIfNecessary(this.property.Value);
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x00019E61 File Offset: 0x00018061
		public RecordValue Annotations
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x040021CC RID: 8652
		private readonly ODataProperty property;
	}
}
