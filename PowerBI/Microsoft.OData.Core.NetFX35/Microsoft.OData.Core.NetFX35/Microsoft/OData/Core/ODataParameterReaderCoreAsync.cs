using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020000EB RID: 235
	internal abstract class ODataParameterReaderCoreAsync : ODataParameterReaderCore
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x00020BDF File Offset: 0x0001EDDF
		protected ODataParameterReaderCoreAsync(ODataInputContext inputContext, IEdmOperation operation)
			: base(inputContext, operation)
		{
		}
	}
}
