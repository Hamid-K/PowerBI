using System;
using System.IO;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x0200002A RID: 42
	public class PreShreddedPbixFilesServer
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000368E File Offset: 0x0000188E
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003696 File Offset: 0x00001896
		public string Pbix { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000369F File Offset: 0x0000189F
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000036A7 File Offset: 0x000018A7
		public string Model { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x000036B0 File Offset: 0x000018B0
		internal string GetErrorMessageIfExists()
		{
			if (string.IsNullOrWhiteSpace(this.Pbix))
			{
				string text = string.Format("Pbix file must be specified: {0}", this.Pbix);
				Logger.Debug(text, Array.Empty<object>());
				return text;
			}
			if (!this.ValidateFileDirectoryIsLocalTemp(this.Pbix))
			{
				string text2 = string.Format("Pbix file expected in temp directory but is in incorrect location: {0}", this.Pbix);
				Logger.Error(text2, Array.Empty<object>());
				return text2;
			}
			if (!File.Exists(this.Pbix))
			{
				string text3 = string.Format("Pbix file not found. {0}", this.Pbix);
				Logger.Debug(text3, Array.Empty<object>());
				return text3;
			}
			if (!string.IsNullOrWhiteSpace(this.Model) && !File.Exists(this.Model))
			{
				string text4 = string.Format("Model file not found. {0}", this.Pbix);
				Logger.Debug(text4, Array.Empty<object>());
				return text4;
			}
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003770 File Offset: 0x00001970
		internal bool ValidateFileDirectoryIsLocalTemp(string pbixPath)
		{
			try
			{
				string directoryName = new FileInfo(pbixPath).DirectoryName;
				string text = Path.GetTempPath().TrimEnd(new char[] { '\\' });
				if (directoryName.Equals(text, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037C8 File Offset: 0x000019C8
		public override string ToString()
		{
			return string.Format("PreShreddedPbixFiles: Pbix={0}, Model={1}", this.Pbix, this.Model);
		}
	}
}
