using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200004B RID: 75
	internal sealed class DsrWriterValueEncodingConfiguration
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00004DB8 File Offset: 0x00002FB8
		internal DsrWriterValueEncodingConfiguration(DsrWriterValueEncodingOptions boolEncoding, DsrWriterValueEncodingOptions longEncoding, DsrWriterValueEncodingOptions doubleEncoding, DsrWriterValueEncodingOptions decimalEncoding, DsrWriterValueEncodingOptions textEncoding, DsrWriterValueEncodingOptions dateTimeEncoding, DsrWriterValueEncodingOptions byteArrayEncoding, LongEncodingAnalyzer longFallbackAnalyzer, DoubleEncodingAnalyzer doubleFallbackAnalyzer, DecimalEncodingAnalyzer decimalFallbackAnalyzer)
		{
			this.BoolEncoding = boolEncoding;
			this.LongEncoding = longEncoding;
			this.DoubleEncoding = doubleEncoding;
			this.DecimalEncoding = decimalEncoding;
			this.TextEncoding = textEncoding;
			this.DateTimeEncoding = dateTimeEncoding;
			this.ByteArrayEncoding = byteArrayEncoding;
			this.LongFallbackAnalyzer = longFallbackAnalyzer;
			this.DoubleFallbackAnalyzer = doubleFallbackAnalyzer;
			this.DecimalFallbackAnalyzer = decimalFallbackAnalyzer;
		}

		// Token: 0x040000C8 RID: 200
		internal static readonly DsrWriterValueEncodingConfiguration V2 = new DsrWriterValueEncodingConfiguration(DsrWriterValueEncodingOptions.Optimized, DsrWriterValueEncodingOptions.Optimized, DsrWriterValueEncodingOptions.Optimized, DsrWriterValueEncodingOptions.Optimized, DsrWriterValueEncodingOptions.SimpleEncoded, DsrWriterValueEncodingOptions.Optimized, DsrWriterValueEncodingOptions.SimpleEncoded, new LongEncodingAnalyzer(false), new DoubleEncodingAnalyzer(false), new DecimalEncodingAnalyzer(false));

		// Token: 0x040000C9 RID: 201
		internal readonly DsrWriterValueEncodingOptions BoolEncoding;

		// Token: 0x040000CA RID: 202
		internal readonly DsrWriterValueEncodingOptions LongEncoding;

		// Token: 0x040000CB RID: 203
		internal readonly DsrWriterValueEncodingOptions DoubleEncoding;

		// Token: 0x040000CC RID: 204
		internal readonly DsrWriterValueEncodingOptions DecimalEncoding;

		// Token: 0x040000CD RID: 205
		internal readonly DsrWriterValueEncodingOptions TextEncoding;

		// Token: 0x040000CE RID: 206
		internal readonly DsrWriterValueEncodingOptions DateTimeEncoding;

		// Token: 0x040000CF RID: 207
		internal readonly DsrWriterValueEncodingOptions ByteArrayEncoding;

		// Token: 0x040000D0 RID: 208
		internal readonly LongEncodingAnalyzer LongFallbackAnalyzer;

		// Token: 0x040000D1 RID: 209
		internal readonly DoubleEncodingAnalyzer DoubleFallbackAnalyzer;

		// Token: 0x040000D2 RID: 210
		internal readonly DecimalEncodingAnalyzer DecimalFallbackAnalyzer;
	}
}
