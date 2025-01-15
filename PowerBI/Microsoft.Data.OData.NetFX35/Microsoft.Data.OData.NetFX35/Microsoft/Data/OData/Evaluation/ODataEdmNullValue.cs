using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200017E RID: 382
	internal sealed class ODataEdmNullValue : EdmValue, IEdmNullValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x00023404 File Offset: 0x00021604
		internal ODataEdmNullValue(IEdmTypeReference type)
			: base(type)
		{
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002340D File Offset: 0x0002160D
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Null;
			}
		}

		// Token: 0x040003FE RID: 1022
		internal static ODataEdmNullValue UntypedInstance = new ODataEdmNullValue(null);
	}
}
