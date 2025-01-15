using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000070 RID: 112
	internal class ODataParameterValue
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0000DC37 File Offset: 0x0000BE37
		public ODataParameterValue(object paramValue, IEdmTypeReference paramType)
		{
			if (paramType == null)
			{
				throw Error.ArgumentNull("paramType");
			}
			this.Value = paramValue;
			this.EdmType = paramType;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000DC5B File Offset: 0x0000BE5B
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000DC63 File Offset: 0x0000BE63
		public IEdmTypeReference EdmType { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000DC74 File Offset: 0x0000BE74
		public object Value { get; private set; }

		// Token: 0x040000DF RID: 223
		public const string ParameterValuePrefix = "DF908045-6922-46A0-82F2-2F6E7F43D1B1_";
	}
}
