using System;
using System.Globalization;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C7 RID: 455
	public static class MetadataFormattingOptionsBuilderExtensions
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x000C308A File Offset: 0x000C128A
		public static TmdlCasingStyle GetCasingStyle(this MetadataFormattingOptionsBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlFormattingOptions tmdlFormattingOptions = builder.GetOptionsImpl(false, false) as TmdlFormattingOptions;
			if (tmdlFormattingOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidFormatBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			return tmdlFormattingOptions.CasingStyle;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000C30CA File Offset: 0x000C12CA
		public static int GetBaseIndentationLevel(this MetadataFormattingOptionsBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlFormattingOptions tmdlFormattingOptions = builder.GetOptionsImpl(false, false) as TmdlFormattingOptions;
			if (tmdlFormattingOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidFormatBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			return tmdlFormattingOptions.BaseIndentationLevel;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000C310C File Offset: 0x000C130C
		public static MetadataFormattingOptionsBuilder WithCasingStyle(this MetadataFormattingOptionsBuilder builder, TmdlCasingStyle style)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlFormattingOptions tmdlFormattingOptions = builder.GetOptionsImpl(false, true) as TmdlFormattingOptions;
			if (tmdlFormattingOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidFormatBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			tmdlFormattingOptions.CasingStyle = style;
			return builder;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000C315C File Offset: 0x000C135C
		public static MetadataFormattingOptionsBuilder WithBaseIndentationLevel(this MetadataFormattingOptionsBuilder builder, int level)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (level < 0)
			{
				throw new ArgumentOutOfRangeException("level", level, TomSR.Exception_InvalidBaseIndentationLevel(level.ToString(CultureInfo.InvariantCulture)));
			}
			TmdlFormattingOptions tmdlFormattingOptions = builder.GetOptionsImpl(false, true) as TmdlFormattingOptions;
			if (tmdlFormattingOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidFormatBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			tmdlFormattingOptions.BaseIndentationLevel = level;
			return builder;
		}
	}
}
