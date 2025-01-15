using System;
using System.IO;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200014E RID: 334
	internal class TmdlTextReader : Disposable, ITmdlReader
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x00090112 File Offset: 0x0008E312
		public TmdlTextReader(Stream tmdl)
		{
			this.reader = new StreamReader(tmdl, MetadataFormattingOptions.GetEffectiveEncoding(), true, 1024, true);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00090132 File Offset: 0x0008E332
		public TmdlTextReader(TextReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00090141 File Offset: 0x0008E341
		public int LineNumber
		{
			get
			{
				return this.currentLine;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0009014C File Offset: 0x0008E34C
		public TmdlTextLine ReadLine()
		{
			string text = this.reader.ReadLine();
			if (text == null)
			{
				return default(TmdlTextLine);
			}
			this.currentLine++;
			return new TmdlTextLine(text);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00090188 File Offset: 0x0008E388
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.reader.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x040003BF RID: 959
		private readonly TextReader reader;

		// Token: 0x040003C0 RID: 960
		private int currentLine;
	}
}
