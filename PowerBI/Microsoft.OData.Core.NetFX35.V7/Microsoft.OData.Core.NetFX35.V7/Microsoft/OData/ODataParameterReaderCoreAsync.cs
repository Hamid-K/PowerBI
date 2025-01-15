using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200007F RID: 127
	internal abstract class ODataParameterReaderCoreAsync : ODataParameterReaderCore
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000DB72 File Offset: 0x0000BD72
		protected ODataParameterReaderCoreAsync(ODataInputContext inputContext, IEdmOperation operation)
			: base(inputContext, operation)
		{
		}
	}
}
