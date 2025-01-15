using System;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200013C RID: 316
	public sealed class TmdlFormattingOptions : MetadataFormattingOptions
	{
		// Token: 0x060014DD RID: 5341 RVA: 0x0008CBFF File Offset: 0x0008ADFF
		internal TmdlFormattingOptions()
		{
			this.CasingStyle = TmdlCasingStyle.CamelCase;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0008CC0E File Offset: 0x0008AE0E
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x0008CC16 File Offset: 0x0008AE16
		public TmdlCasingStyle CasingStyle { get; internal set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0008CC1F File Offset: 0x0008AE1F
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0008CC27 File Offset: 0x0008AE27
		public int BaseIndentationLevel { get; internal set; }

		// Token: 0x060014E2 RID: 5346 RVA: 0x0008CC30 File Offset: 0x0008AE30
		internal static void GetCurrentWriterSettings(out TmdlCasingStyle casingStyle, out Indentation baseIndentation, out string eol)
		{
			TmdlFormattingOptions tmdlFormattingOptions = MetadataFormattingOptions.Current as TmdlFormattingOptions;
			if (tmdlFormattingOptions != null)
			{
				casingStyle = tmdlFormattingOptions.CasingStyle;
				if (tmdlFormattingOptions.BaseIndentationLevel == 0)
				{
					baseIndentation = Indentation.Empty;
				}
				else
				{
					baseIndentation = Indentation.Empty.Increment(tmdlFormattingOptions.BaseIndentationLevel);
				}
			}
			else
			{
				casingStyle = TmdlCasingStyle.CamelCase;
				baseIndentation = Indentation.Empty;
			}
			eol = MetadataFormattingOptions.GetCurrentEndOfLine();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0008CC98 File Offset: 0x0008AE98
		private protected override MetadataFormattingOptions CreateOptionsOfSameType()
		{
			return new TmdlFormattingOptions();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0008CCA0 File Offset: 0x0008AEA0
		private protected override void CloneImpl(MetadataFormattingOptions options)
		{
			base.CloneImpl(options);
			TmdlFormattingOptions tmdlFormattingOptions = options as TmdlFormattingOptions;
			if (tmdlFormattingOptions == null)
			{
				throw TomInternalException.Create("This must be options of type {0}, but the argument is of type {1}!", new object[]
				{
					typeof(TmdlFormattingOptions).Name,
					options.GetType().Name
				});
			}
			tmdlFormattingOptions.CasingStyle = this.CasingStyle;
			tmdlFormattingOptions.BaseIndentationLevel = this.BaseIndentationLevel;
		}
	}
}
