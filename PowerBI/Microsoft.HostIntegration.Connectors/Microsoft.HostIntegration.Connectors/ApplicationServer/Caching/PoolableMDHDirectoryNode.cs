using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000249 RID: 585
	internal class PoolableMDHDirectoryNode : MDHDirectoryNode
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0003DAB1 File Offset: 0x0003BCB1
		public PoolableMDHDirectoryNode(MDHDirectoryNodePool parentPool)
		{
			this._parentPool = parentPool;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0003DAC0 File Offset: 0x0003BCC0
		~PoolableMDHDirectoryNode()
		{
			if (!AppDomain.CurrentDomain.IsFinalizingForUnload() && base.IsInUse())
			{
				base.Clean();
				this._parentPool.PutObjectInPool(this);
				GC.ReRegisterForFinalize(this);
			}
		}

		// Token: 0x04000BCC RID: 3020
		private MDHDirectoryNodePool _parentPool;
	}
}
