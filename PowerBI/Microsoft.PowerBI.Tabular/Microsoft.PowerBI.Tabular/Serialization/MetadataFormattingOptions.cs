using System;
using System.Text;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000181 RID: 385
	public class MetadataFormattingOptions : ICloneable
	{
		// Token: 0x060017E9 RID: 6121 RVA: 0x000A2E90 File Offset: 0x000A1090
		internal MetadataFormattingOptions()
		{
			this.encoding = null;
			this.NewLineStyle = NewLineStyle.SystemDefault;
			this.IndentationMode = IndentationMode.Default;
			this.IndentationSize = 4;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x000A2EB4 File Offset: 0x000A10B4
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x000A2ED9 File Offset: 0x000A10D9
		public Encoding Encoding
		{
			get
			{
				if (this.encoding == null)
				{
					this.encoding = (Encoding)MetadataFormattingOptions.defaultEncoding.Clone();
				}
				return this.encoding;
			}
			internal set
			{
				this.encoding = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x000A2EE2 File Offset: 0x000A10E2
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x000A2EEA File Offset: 0x000A10EA
		public NewLineStyle NewLineStyle { get; internal set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x000A2EF3 File Offset: 0x000A10F3
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x000A2EFB File Offset: 0x000A10FB
		public IndentationMode IndentationMode { get; internal set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000A2F04 File Offset: 0x000A1104
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x000A2F0C File Offset: 0x000A110C
		public int IndentationSize { get; internal set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x000A2F15 File Offset: 0x000A1115
		internal static MetadataFormattingOptions Default
		{
			get
			{
				return MetadataFormattingOptions.@default;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000A2F1C File Offset: 0x000A111C
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x000A2F30 File Offset: 0x000A1130
		internal static MetadataFormattingOptions Current
		{
			get
			{
				if (MetadataFormattingOptions.current != null)
				{
					return MetadataFormattingOptions.current;
				}
				return MetadataFormattingOptions.@default;
			}
			set
			{
				MetadataFormattingOptions.current = value;
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000A2F38 File Offset: 0x000A1138
		internal static Encoding GetEffectiveEncoding()
		{
			MetadataFormattingOptions metadataFormattingOptions = MetadataFormattingOptions.Current;
			return ((metadataFormattingOptions != null) ? metadataFormattingOptions.encoding : null) ?? MetadataFormattingOptions.defaultEncoding;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000A2F54 File Offset: 0x000A1154
		internal static string GetCurrentEndOfLine()
		{
			MetadataFormattingOptions metadataFormattingOptions = MetadataFormattingOptions.Current;
			if (metadataFormattingOptions == null)
			{
				return Environment.NewLine;
			}
			switch (metadataFormattingOptions.NewLineStyle)
			{
			case NewLineStyle.SystemDefault:
				return Environment.NewLine;
			case NewLineStyle.WindowsStyle:
				return "\r\n";
			case NewLineStyle.UnixStyle:
				return "\n";
			default:
				return Environment.NewLine;
			}
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000A2FA2 File Offset: 0x000A11A2
		internal static IDisposable StartFormattingScope(MetadataFormattingOptions options)
		{
			return new MetadataFormattingOptions.FormatScope(options);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000A2FAA File Offset: 0x000A11AA
		internal static IDisposable StartFormattingScope(Encoding encoding, NewLineStyle? newLineStyle, IndentationMode? indentationMode, int? indentationLevelLength)
		{
			return new MetadataFormattingOptions.FormatScope(encoding, newLineStyle, indentationMode, indentationLevelLength);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000A2FB8 File Offset: 0x000A11B8
		internal MetadataFormattingOptions Clone()
		{
			MetadataFormattingOptions metadataFormattingOptions = this.CreateOptionsOfSameType();
			this.CloneImpl(metadataFormattingOptions);
			return metadataFormattingOptions;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000A2FD4 File Offset: 0x000A11D4
		private protected virtual MetadataFormattingOptions CreateOptionsOfSameType()
		{
			return new MetadataFormattingOptions();
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000A2FDB File Offset: 0x000A11DB
		private protected virtual void CloneImpl(MetadataFormattingOptions options)
		{
			options.encoding = this.encoding;
			options.NewLineStyle = this.NewLineStyle;
			options.IndentationMode = this.IndentationMode;
			options.IndentationSize = this.IndentationSize;
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000A300D File Offset: 0x000A120D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0400045C RID: 1116
		private const string WindowsStyleEOL = "\r\n";

		// Token: 0x0400045D RID: 1117
		private const string UnixStyleEOL = "\n";

		// Token: 0x0400045E RID: 1118
		private const int DEFAULT_INDENTATION_SIZE = 4;

		// Token: 0x0400045F RID: 1119
		private static readonly Encoding defaultEncoding = new UTF8Encoding(false);

		// Token: 0x04000460 RID: 1120
		private static readonly MetadataFormattingOptions @default = new MetadataFormattingOptions();

		// Token: 0x04000461 RID: 1121
		[ThreadStatic]
		private static MetadataFormattingOptions current;

		// Token: 0x04000462 RID: 1122
		private Encoding encoding;

		// Token: 0x020003B0 RID: 944
		private sealed class FormatScope : Disposable
		{
			// Token: 0x060026D3 RID: 9939 RVA: 0x000EBD93 File Offset: 0x000E9F93
			public FormatScope(MetadataFormattingOptions options)
			{
				this.prevOptions = MetadataFormattingOptions.current;
				MetadataFormattingOptions.current = options;
			}

			// Token: 0x060026D4 RID: 9940 RVA: 0x000EBDAC File Offset: 0x000E9FAC
			public FormatScope(Encoding encoding, NewLineStyle? newLineStyle, IndentationMode? indentationMode, int? indentationSize)
			{
				this.prevOptions = MetadataFormattingOptions.current;
				MetadataFormattingOptions metadataFormattingOptions = ((this.prevOptions != null) ? this.prevOptions : MetadataFormattingOptions.@default).Clone();
				if (encoding != null)
				{
					metadataFormattingOptions.encoding = encoding;
				}
				if (newLineStyle != null)
				{
					metadataFormattingOptions.NewLineStyle = newLineStyle.Value;
				}
				if (indentationMode != null)
				{
					metadataFormattingOptions.IndentationMode = indentationMode.Value;
				}
				if (indentationSize != null)
				{
					metadataFormattingOptions.IndentationSize = indentationSize.Value;
				}
				MetadataFormattingOptions.current = metadataFormattingOptions;
			}

			// Token: 0x060026D5 RID: 9941 RVA: 0x000EBE38 File Offset: 0x000EA038
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing)
					{
						MetadataFormattingOptions.current = this.prevOptions;
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x0400115D RID: 4445
			private MetadataFormattingOptions prevOptions;
		}
	}
}
