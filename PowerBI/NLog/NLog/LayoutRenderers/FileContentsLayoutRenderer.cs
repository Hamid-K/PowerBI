using System;
using System.IO;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C3 RID: 195
	[LayoutRenderer("file-contents")]
	public class FileContentsLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x0001F938 File Offset: 0x0001DB38
		public FileContentsLayoutRenderer()
		{
			this.Encoding = Encoding.Default;
			this._lastFileName = string.Empty;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0001F961 File Offset: 0x0001DB61
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0001F969 File Offset: 0x0001DB69
		[DefaultParameter]
		public Layout FileName { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0001F972 File Offset: 0x0001DB72
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0001F97A File Offset: 0x0001DB7A
		public Encoding Encoding { get; set; }

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001F984 File Offset: 0x0001DB84
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				string text = this.FileName.Render(logEvent);
				if (text != this._lastFileName)
				{
					this._currentFileContents = this.ReadFileContents(text);
					this._lastFileName = text;
				}
			}
			builder.Append(this._currentFileContents);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0001F9FC File Offset: 0x0001DBFC
		private string ReadFileContents(string fileName)
		{
			string text;
			try
			{
				using (StreamReader streamReader = new StreamReader(fileName, this.Encoding))
				{
					text = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Cannot read file contents of '{0}'.", new object[] { fileName });
				if (ex.MustBeRethrown())
				{
					throw;
				}
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x04000306 RID: 774
		private readonly object _lockObject = new object();

		// Token: 0x04000307 RID: 775
		private string _lastFileName;

		// Token: 0x04000308 RID: 776
		private string _currentFileContents;
	}
}
