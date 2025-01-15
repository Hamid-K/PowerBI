using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000085 RID: 133
	internal sealed class ODataEdmNullValue : EdmValue, IEdmNullValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00013838 File Offset: 0x00011A38
		internal ODataEdmNullValue(IEdmTypeReference type)
			: base(type)
		{
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00013841 File Offset: 0x00011A41
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Null;
			}
		}

		// Token: 0x04000240 RID: 576
		internal static ODataEdmNullValue UntypedInstance = new ODataEdmNullValue(null);
	}
}
