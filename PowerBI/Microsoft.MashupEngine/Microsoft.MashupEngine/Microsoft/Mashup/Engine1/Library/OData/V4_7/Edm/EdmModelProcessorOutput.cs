using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm
{
	// Token: 0x0200080E RID: 2062
	internal sealed class EdmModelProcessorOutput : EdmModelProcessorOutputBase
	{
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000C1B37 File Offset: 0x000BFD37
		public EdmModelProcessorOutput()
		{
			this.EdmTypeValueLookup = new Dictionary<Microsoft.OData.Edm.IEdmType, TypeValue>();
			this.Annotations = new Annotations();
			this.BoundFunctionOverloads = new Dictionary<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>>();
			this.UnboundFunctionOverloads = new Dictionary<string, List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>>();
			this.EntityTypeKeys = new Dictionary<Microsoft.OData.Edm.IEdmEntityType, IList<TableKey>>();
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000C1B76 File Offset: 0x000BFD76
		// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x000C1B7E File Offset: 0x000BFD7E
		public Dictionary<Microsoft.OData.Edm.IEdmType, TypeValue> EdmTypeValueLookup { get; private set; }

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000C1B87 File Offset: 0x000BFD87
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000C1B8F File Offset: 0x000BFD8F
		public Annotations Annotations { get; private set; }

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000C1B98 File Offset: 0x000BFD98
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000C1BA0 File Offset: 0x000BFDA0
		public Dictionary<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>> BoundFunctionOverloads { get; private set; }

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000C1BA9 File Offset: 0x000BFDA9
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x000C1BB1 File Offset: 0x000BFDB1
		public Dictionary<string, List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>> UnboundFunctionOverloads { get; private set; }

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000C1BBA File Offset: 0x000BFDBA
		// (set) Token: 0x06003BAA RID: 15274 RVA: 0x000C1BC2 File Offset: 0x000BFDC2
		public Dictionary<Microsoft.OData.Edm.IEdmEntityType, IList<TableKey>> EntityTypeKeys { get; private set; }

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000C1BCB File Offset: 0x000BFDCB
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x000C1BD3 File Offset: 0x000BFDD3
		public HashSet<Microsoft.OData.Edm.IEdmStructuredType> BaseTypes { get; set; }
	}
}
