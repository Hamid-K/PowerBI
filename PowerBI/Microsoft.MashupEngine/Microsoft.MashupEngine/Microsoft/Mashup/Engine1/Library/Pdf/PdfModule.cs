using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.Pdf
{
	// Token: 0x02000271 RID: 625
	public sealed class PdfModule : Module45
	{
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00034CE3 File Offset: 0x00032EE3
		public override string Name
		{
			get
			{
				return "Pdf";
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00034CEA File Offset: 0x00032EEA
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Pdf.Tables";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040007A9 RID: 1961
		public const string PdfTablesName = "Pdf.Tables";

		// Token: 0x040007AA RID: 1962
		private Keys exportKeys;

		// Token: 0x02000272 RID: 626
		private enum Exports
		{
			// Token: 0x040007AC RID: 1964
			Pdf_Tables,
			// Token: 0x040007AD RID: 1965
			Count
		}
	}
}
