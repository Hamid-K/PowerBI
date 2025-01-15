using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000154 RID: 340
	internal struct TmdlWriterSettings
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x00090C5D File Offset: 0x0008EE5D
		private TmdlWriterSettings(TmdlCasingStyle keywordStyle, Indentation baseIndentation, Indentation indentation, string eol)
		{
			this.KeywordStyle = keywordStyle;
			this.BaseIndentation = baseIndentation;
			this.Indentation = indentation;
			this.EOL = eol;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00090C7C File Offset: 0x0008EE7C
		internal bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.EOL);
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00090C8C File Offset: 0x0008EE8C
		public static TmdlWriterSettings Create()
		{
			TmdlCasingStyle tmdlCasingStyle;
			Indentation indentation;
			string text;
			TmdlFormattingOptions.GetCurrentWriterSettings(out tmdlCasingStyle, out indentation, out text);
			return new TmdlWriterSettings(tmdlCasingStyle, indentation, Indentation.Empty.Increment(1), text);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00090CBC File Offset: 0x0008EEBC
		internal static TmdlWriterSettings Create(TmdlCasingStyle keywordStyle)
		{
			TmdlCasingStyle tmdlCasingStyle;
			Indentation indentation;
			string text;
			TmdlFormattingOptions.GetCurrentWriterSettings(out tmdlCasingStyle, out indentation, out text);
			return new TmdlWriterSettings(keywordStyle, indentation, Indentation.Empty.Increment(1), text);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00090CEC File Offset: 0x0008EEEC
		internal static TmdlWriterSettings Create(Indentation indentation)
		{
			TmdlCasingStyle tmdlCasingStyle;
			Indentation indentation2;
			string text;
			TmdlFormattingOptions.GetCurrentWriterSettings(out tmdlCasingStyle, out indentation2, out text);
			return new TmdlWriterSettings(tmdlCasingStyle, indentation2, indentation, text);
		}

		// Token: 0x040003D8 RID: 984
		public readonly TmdlCasingStyle KeywordStyle;

		// Token: 0x040003D9 RID: 985
		public readonly Indentation BaseIndentation;

		// Token: 0x040003DA RID: 986
		public readonly Indentation Indentation;

		// Token: 0x040003DB RID: 987
		public readonly string EOL;
	}
}
