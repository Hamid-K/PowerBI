using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x0200013C RID: 316
	internal class ReusableStreamCreator : ReusableObjectCreator<MemoryStream>, IDisposable
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x000279B6 File Offset: 0x00025BB6
		public ReusableStreamCreator(int capacity)
			: base(new MemoryStream(capacity), delegate(MemoryStream m)
			{
				m.Position = 0L;
				m.SetLength(0L);
			})
		{
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000279E3 File Offset: 0x00025BE3
		void IDisposable.Dispose()
		{
			this._reusableObject.Dispose();
		}
	}
}
