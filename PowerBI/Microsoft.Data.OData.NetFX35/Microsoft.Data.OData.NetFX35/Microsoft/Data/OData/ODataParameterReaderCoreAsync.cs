using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000157 RID: 343
	internal abstract class ODataParameterReaderCoreAsync : ODataParameterReaderCore
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x0001C9E9 File Offset: 0x0001ABE9
		protected ODataParameterReaderCoreAsync(ODataInputContext inputContext, IEdmFunctionImport functionImport)
			: base(inputContext, functionImport)
		{
		}
	}
}
