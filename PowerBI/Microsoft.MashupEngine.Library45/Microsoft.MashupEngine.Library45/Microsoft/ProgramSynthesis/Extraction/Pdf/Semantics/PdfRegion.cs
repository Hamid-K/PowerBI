using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics
{
	// Token: 0x02000C0D RID: 3085
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PdfRegion : IEquatable<PdfRegion>
	{
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06004FC5 RID: 20421 RVA: 0x000FB356 File Offset: 0x000F9556
		internal ILoadedPdf Pdf { get; }

		// Token: 0x06004FC6 RID: 20422 RVA: 0x000FB35E File Offset: 0x000F955E
		internal PdfRegion(ILoadedPdf pdf)
		{
			this.Pdf = pdf;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x000FB36D File Offset: 0x000F956D
		public bool SamePdfAs(PdfRegion other)
		{
			return this.Pdf == other.Pdf;
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x000FB380 File Offset: 0x000F9580
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			PdfRegion pdfRegion = obj as PdfRegion;
			return pdfRegion != null && this.Equals(pdfRegion);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x000FB3A6 File Offset: 0x000F95A6
		public virtual bool Equals(PdfRegion other)
		{
			return this.SamePdfAs(other);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x000FB3AF File Offset: 0x000F95AF
		public override int GetHashCode()
		{
			return this.Pdf.GetHashCode();
		}
	}
}
