using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000201 RID: 513
	internal interface IOperation
	{
		// Token: 0x060010AF RID: 4271
		void PreProcess(IBaseDataNode oldData, ref IBaseDataNode newItem, ref DMOperationInfo OperationInfo);

		// Token: 0x060010B0 RID: 4272
		BaseHashTable GetParentHashTable();
	}
}
