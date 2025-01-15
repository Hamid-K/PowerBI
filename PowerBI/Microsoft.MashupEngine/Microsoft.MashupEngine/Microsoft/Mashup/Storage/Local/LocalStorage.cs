using System;

namespace Microsoft.Mashup.Storage.Local
{
	// Token: 0x020020A9 RID: 8361
	public abstract class LocalStorage : IDisposable
	{
		// Token: 0x17003146 RID: 12614
		// (get) Token: 0x0600CCB4 RID: 52404
		public abstract object ObjectLock { get; }

		// Token: 0x17003147 RID: 12615
		// (get) Token: 0x0600CCB5 RID: 52405
		public abstract LocalStorageCache Cache { get; }

		// Token: 0x0600CCB6 RID: 52406
		public abstract void SetPart(string path, byte[] content, string contentType, bool cache);

		// Token: 0x0600CCB7 RID: 52407
		public abstract void ClearPart(string path);

		// Token: 0x0600CCB8 RID: 52408
		public abstract bool TryGetPart(string path, out byte[] content);

		// Token: 0x0600CCB9 RID: 52409
		public abstract string[] GetPartPaths(string path);

		// Token: 0x0600CCBA RID: 52410
		public abstract void Dispose();
	}
}
