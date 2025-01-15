using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003DE RID: 990
	internal sealed class FileEventSink : IEventSink, IDisposable
	{
		// Token: 0x0600229C RID: 8860 RVA: 0x0006AC3C File Offset: 0x00068E3C
		public void Dispose()
		{
			this.cleanup();
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0006AC44 File Offset: 0x00068E44
		private void cleanup()
		{
			if (this.m_fs != null)
			{
				try
				{
					this.m_fs.Close();
				}
				catch (IOException)
				{
				}
				catch (ObjectDisposedException)
				{
				}
				this.m_fs = null;
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0006AC90 File Offset: 0x00068E90
		public void WriteEntry(string src, TraceEventType msgType, string msgText)
		{
			if (msgText == null)
			{
				return;
			}
			lock (this)
			{
				if (this.m_fs == null || this.m_formatString != null)
				{
					string text;
					if (this.m_formatString != null)
					{
						text = this.m_logName + DateTime.Now.ToString(this.m_formatString, CultureInfo.InvariantCulture) + ".log";
						if (text != this.m_fileName)
						{
							this.cleanup();
							this.m_fileName = text;
						}
					}
					else
					{
						text = this.m_logName + ".log";
					}
					if (text.Contains("$"))
					{
						text = text.Replace("$", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture));
					}
					if (this.m_fs == null)
					{
						FileMode fileMode = ((this.m_formatString != null) ? FileMode.Create : FileMode.Append);
						this.m_fs = new StreamWriter(File.Open(text, fileMode, FileAccess.Write, FileShare.ReadWrite));
					}
				}
				msgText = msgText.Replace("\n", "\t");
				try
				{
					this.m_fs.WriteLine("{0},{1},{2},{3}", new object[]
					{
						msgText,
						src,
						msgType,
						Utility.FormatTime(DateTime.Now)
					});
					this.m_fs.Flush();
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0006AE10 File Offset: 0x00069010
		public bool Load(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			string[] array = id.Split(new char[] { '/' });
			if (array.Length == 2)
			{
				this.m_logName = array[0];
				this.m_formatString = array[1];
			}
			else
			{
				this.m_logName = id;
			}
			return true;
		}

		// Token: 0x040015BE RID: 5566
		private TextWriter m_fs;

		// Token: 0x040015BF RID: 5567
		private string m_fileName;

		// Token: 0x040015C0 RID: 5568
		private string m_formatString;

		// Token: 0x040015C1 RID: 5569
		private string m_logName;
	}
}
