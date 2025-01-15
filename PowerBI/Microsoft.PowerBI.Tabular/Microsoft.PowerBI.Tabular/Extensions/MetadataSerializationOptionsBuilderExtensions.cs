using System;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C9 RID: 457
	public static class MetadataSerializationOptionsBuilderExtensions
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x000C3205 File Offset: 0x000C1405
		public static TmdlExpressionTrimStyle GetExpressionTrimStyle(this MetadataSerializationOptionsBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlSerializationOptions tmdlSerializationOptions = builder.GetOptionsImpl(false, false) as TmdlSerializationOptions;
			if (tmdlSerializationOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidFormatBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			return tmdlSerializationOptions.ExpressionTrimStyle;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000C3245 File Offset: 0x000C1445
		public static bool IsMetadataOrderHintsExcluded(this MetadataSerializationOptionsBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlSerializationOptions tmdlSerializationOptions = builder.GetOptionsImpl(false, false) as TmdlSerializationOptions;
			if (tmdlSerializationOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidFormatBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			return tmdlSerializationOptions.ExcludeMetadataOrderHints;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000C3288 File Offset: 0x000C1488
		public static MetadataSerializationOptionsBuilder WithExpressionTrimStyle(this MetadataSerializationOptionsBuilder builder, TmdlExpressionTrimStyle style)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlSerializationOptions tmdlSerializationOptions = builder.GetOptionsImpl(false, true) as TmdlSerializationOptions;
			if (tmdlSerializationOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidSerializationBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			tmdlSerializationOptions.ExpressionTrimStyle = style;
			return builder;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000C32D8 File Offset: 0x000C14D8
		public static MetadataSerializationOptionsBuilder WithMetadataOrderHints(this MetadataSerializationOptionsBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlSerializationOptions tmdlSerializationOptions = builder.GetOptionsImpl(false, true) as TmdlSerializationOptions;
			if (tmdlSerializationOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidSerializationBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			tmdlSerializationOptions.ExcludeMetadataOrderHints = false;
			return builder;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000C3328 File Offset: 0x000C1528
		public static MetadataSerializationOptionsBuilder WithoutMetadataOrderHints(this MetadataSerializationOptionsBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			TmdlSerializationOptions tmdlSerializationOptions = builder.GetOptionsImpl(false, true) as TmdlSerializationOptions;
			if (tmdlSerializationOptions == null)
			{
				throw new InvalidOperationException(TomSR.Exception_InvalidSerializationBuilderContentStyleForSetOption(MetadataSerializationStyle.Tmdl.ToString("G")));
			}
			tmdlSerializationOptions.ExcludeMetadataOrderHints = true;
			return builder;
		}
	}
}
