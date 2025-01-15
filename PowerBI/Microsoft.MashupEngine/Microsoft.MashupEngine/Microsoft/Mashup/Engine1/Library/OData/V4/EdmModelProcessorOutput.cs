using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000847 RID: 2119
	internal sealed class EdmModelProcessorOutput : EdmModelProcessorOutputBase
	{
		// Token: 0x06003D29 RID: 15657 RVA: 0x000C71EB File Offset: 0x000C53EB
		public EdmModelProcessorOutput()
		{
			this.EdmTypeValueLookup = new Dictionary<Microsoft.OData.Edm.IEdmType, TypeValue>();
			this.Annotations = new Annotations();
			this.BoundFunctionOverloads = new Dictionary<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>>();
			this.UnboundFunctionOverloads = new List<Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>();
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000C721F File Offset: 0x000C541F
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000C7227 File Offset: 0x000C5427
		public Dictionary<Microsoft.OData.Edm.IEdmType, TypeValue> EdmTypeValueLookup { get; private set; }

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000C7230 File Offset: 0x000C5430
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x000C7238 File Offset: 0x000C5438
		public Annotations Annotations { get; private set; }

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x000C7241 File Offset: 0x000C5441
		// (set) Token: 0x06003D2F RID: 15663 RVA: 0x000C7249 File Offset: 0x000C5449
		public Dictionary<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>> BoundFunctionOverloads { get; private set; }

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000C7252 File Offset: 0x000C5452
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000C725A File Offset: 0x000C545A
		public List<Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>> UnboundFunctionOverloads { get; private set; }
	}
}
