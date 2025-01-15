using System;
using System.IO;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001E2 RID: 482
	public sealed class DisposableFile : IDisposable
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x0002BDF6 File Offset: 0x00029FF6
		public DisposableFile(string path)
		{
			this.Path = path;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002BE05 File Offset: 0x0002A005
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0002BE0D File Offset: 0x0002A00D
		public string Path { get; private set; }

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002BE18 File Offset: 0x0002A018
		public static DisposableFile CreateUniqueTemporary(string extension)
		{
			return new DisposableFile(global::System.IO.Path.Combine(global::System.IO.Path.GetTempPath(), global::System.IO.Path.ChangeExtension(Guid.NewGuid().ToString(), extension)));
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002BE50 File Offset: 0x0002A050
		public void Dispose()
		{
			if (this.Path != null && TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
			{
				File.Delete(this.Path);
			}) != null)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Failed to delete: {0}.", new object[] { this.Path });
			}
			this.Path = null;
		}
	}
}
