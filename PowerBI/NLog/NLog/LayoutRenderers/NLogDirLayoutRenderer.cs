using System;
using System.IO;
using System.Reflection;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DC RID: 220
	[LayoutRenderer("nlogdir")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class NLogDirLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x000215A4 File Offset: 0x0001F7A4
		static NLogDirLayoutRenderer()
		{
			Assembly assembly = typeof(LogManager).GetAssembly();
			NLogDirLayoutRenderer.NLogDir = Path.GetDirectoryName((!string.IsNullOrEmpty(assembly.Location)) ? assembly.Location : new Uri(assembly.CodeBase).LocalPath);
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000215F0 File Offset: 0x0001F7F0
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x000215F8 File Offset: 0x0001F7F8
		public string File { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00021601 File Offset: 0x0001F801
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00021609 File Offset: 0x0001F809
		public string Dir { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00021612 File Offset: 0x0001F812
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00021619 File Offset: 0x0001F819
		private static string NLogDir { get; set; }

		// Token: 0x06000D15 RID: 3349 RVA: 0x00021621 File Offset: 0x0001F821
		protected override void InitializeLayoutRenderer()
		{
			this._nlogCombinedPath = null;
			base.InitializeLayoutRenderer();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00021630 File Offset: 0x0001F830
		protected override void CloseLayoutRenderer()
		{
			this._nlogCombinedPath = null;
			base.CloseLayoutRenderer();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00021640 File Offset: 0x0001F840
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text;
			if ((text = this._nlogCombinedPath) == null)
			{
				text = (this._nlogCombinedPath = PathHelpers.CombinePaths(NLogDirLayoutRenderer.NLogDir, this.Dir, this.File));
			}
			string text2 = text;
			builder.Append(text2);
		}

		// Token: 0x04000354 RID: 852
		private string _nlogCombinedPath;
	}
}
