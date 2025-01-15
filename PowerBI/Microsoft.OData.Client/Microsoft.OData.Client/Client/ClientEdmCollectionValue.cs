using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x02000025 RID: 37
	internal sealed class ClientEdmCollectionValue : IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00006F1C File Offset: 0x0000511C
		public ClientEdmCollectionValue(IEdmTypeReference type, IEnumerable<IEdmValue> elements)
		{
			this.Type = type;
			this.Elements = elements.Select((IEdmValue v) => new ClientEdmCollectionValue.NullEdmDelayedValue(v));
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006F56 File Offset: 0x00005156
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006F5E File Offset: 0x0000515E
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006F67 File Offset: 0x00005167
		public EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006F6A File Offset: 0x0000516A
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006F72 File Offset: 0x00005172
		public IEnumerable<IEdmDelayedValue> Elements { get; private set; }

		// Token: 0x02000156 RID: 342
		private class NullEdmDelayedValue : IEdmDelayedValue
		{
			// Token: 0x06000D2F RID: 3375 RVA: 0x0002E4DF File Offset: 0x0002C6DF
			public NullEdmDelayedValue(IEdmValue value)
			{
				this.Value = value;
			}

			// Token: 0x17000346 RID: 838
			// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002E4EE File Offset: 0x0002C6EE
			// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0002E4F6 File Offset: 0x0002C6F6
			public IEdmValue Value { get; private set; }
		}
	}
}
