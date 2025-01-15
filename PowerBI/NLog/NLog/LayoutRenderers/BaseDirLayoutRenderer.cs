using System;
using System.IO;
using System.Text;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B6 RID: 182
	[LayoutRenderer("basedir")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class BaseDirLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0001E6B7 File Offset: 0x0001C8B7
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0001E6BF File Offset: 0x0001C8BF
		public bool ProcessDir { get; set; }

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public BaseDirLayoutRenderer()
			: this(LogFactory.CurrentAppDomain)
		{
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001E6D5 File Offset: 0x0001C8D5
		public BaseDirLayoutRenderer(IAppDomain appDomain)
		{
			this._baseDir = appDomain.BaseDirectory;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0001E6E9 File Offset: 0x0001C8E9
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0001E6F1 File Offset: 0x0001C8F1
		public string File { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0001E6FA File Offset: 0x0001C8FA
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0001E702 File Offset: 0x0001C902
		public string Dir { get; set; }

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001E70C File Offset: 0x0001C90C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = this._baseDir;
			if (this.ProcessDir)
			{
				string text2;
				if ((text2 = this._processDir) == null)
				{
					text2 = (this._processDir = Path.GetDirectoryName(ProcessIDHelper.Instance.CurrentProcessFilePath));
				}
				text = text2;
			}
			if (text != null)
			{
				string text3 = PathHelpers.CombinePaths(text, this.Dir, this.File);
				builder.Append(text3);
			}
		}

		// Token: 0x040002D4 RID: 724
		private readonly string _baseDir;

		// Token: 0x040002D5 RID: 725
		private string _processDir;
	}
}
