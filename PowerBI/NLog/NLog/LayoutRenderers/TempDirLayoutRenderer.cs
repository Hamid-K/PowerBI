using System;
using System.IO;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EA RID: 234
	[LayoutRenderer("tempdir")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class TempDirLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00022505 File Offset: 0x00020705
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x0002250D File Offset: 0x0002070D
		public string File { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00022516 File Offset: 0x00020716
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0002251E File Offset: 0x0002071E
		public string Dir { get; set; }

		// Token: 0x06000D87 RID: 3463 RVA: 0x00022527 File Offset: 0x00020727
		protected override void InitializeLayoutRenderer()
		{
			if (TempDirLayoutRenderer.tempDir == null)
			{
				TempDirLayoutRenderer.tempDir = Path.GetTempPath();
			}
			base.InitializeLayoutRenderer();
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00022540 File Offset: 0x00020740
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = PathHelpers.CombinePaths(TempDirLayoutRenderer.tempDir, this.Dir, this.File);
			builder.Append(text);
		}

		// Token: 0x040003A7 RID: 935
		private static string tempDir;
	}
}
