using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000226 RID: 550
	internal sealed class DMSignalHandleContainer
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0003A062 File Offset: 0x00038262
		internal DMSignalHandleContainer(int maxContainerSize)
		{
			this._size = maxContainerSize;
			this.Init();
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003A082 File Offset: 0x00038282
		internal DMSignalHandleContainer()
		{
			this.Init();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003A09C File Offset: 0x0003829C
		private void Init()
		{
			this._arraySinalHandle = new VelocitySignalWaitHandle[this._size];
			for (int i = 0; i < this._size; i++)
			{
				this._arraySinalHandle[i] = new VelocitySignalWaitHandle();
			}
		}

		// Token: 0x170003E4 RID: 996
		internal VelocitySignalWaitHandle this[int key]
		{
			get
			{
				return this._arraySinalHandle[key % this._size];
			}
		}

		// Token: 0x04000B24 RID: 2852
		private VelocitySignalWaitHandle[] _arraySinalHandle;

		// Token: 0x04000B25 RID: 2853
		private readonly int _size = 256;
	}
}
