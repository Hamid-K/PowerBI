using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200040F RID: 1039
	internal interface IQueueHandler<T>
	{
		// Token: 0x0600241B RID: 9243
		void ProcessItem(T item);
	}
}
