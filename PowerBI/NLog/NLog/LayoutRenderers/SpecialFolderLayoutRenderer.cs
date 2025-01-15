using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E7 RID: 231
	[LayoutRenderer("specialfolder")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class SpecialFolderLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0002226A File Offset: 0x0002046A
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00022272 File Offset: 0x00020472
		[DefaultParameter]
		public Environment.SpecialFolder Folder { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002227B File Offset: 0x0002047B
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00022283 File Offset: 0x00020483
		public string File { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002228C File Offset: 0x0002048C
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00022294 File Offset: 0x00020494
		public string Dir { get; set; }

		// Token: 0x06000D73 RID: 3443 RVA: 0x000222A0 File Offset: 0x000204A0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = PathHelpers.CombinePaths(Environment.GetFolderPath(this.Folder), this.Dir, this.File);
			builder.Append(text);
		}
	}
}
