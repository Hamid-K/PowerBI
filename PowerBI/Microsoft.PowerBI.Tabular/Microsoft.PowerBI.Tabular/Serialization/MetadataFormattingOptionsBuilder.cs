using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000182 RID: 386
	public sealed class MetadataFormattingOptionsBuilder
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x000A302C File Offset: 0x000A122C
		public MetadataFormattingOptionsBuilder(MetadataSerializationStyle style = MetadataSerializationStyle.Default)
		{
			switch (style)
			{
			case MetadataSerializationStyle.Default:
				this.controller = new ImmutableObjectAccessController<MetadataFormattingOptions>(new MetadataFormattingOptions(), true);
				return;
			case MetadataSerializationStyle.Tmdl:
				this.controller = new ImmutableObjectAccessController<MetadataFormattingOptions>(new TmdlFormattingOptions(), true);
				return;
			case MetadataSerializationStyle.Json:
				throw new NotSupportedException(TomSR.Exception_JsonSerializationSupport);
			default:
				throw new ArgumentException(TomSR.Exception_InvalidContentStyle(style.ToString("G")), "style");
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000A30A1 File Offset: 0x000A12A1
		public MetadataFormattingOptionsBuilder(MetadataFormattingOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.controller = new ImmutableObjectAccessController<MetadataFormattingOptions>(options, false);
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x000A30C4 File Offset: 0x000A12C4
		public Encoding Encoding
		{
			get
			{
				return this.GetOptionsImpl(false, false).Encoding;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x000A30D3 File Offset: 0x000A12D3
		public NewLineStyle NewLineStyle
		{
			get
			{
				return this.GetOptionsImpl(false, false).NewLineStyle;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x000A30E2 File Offset: 0x000A12E2
		public IndentationMode IndentationMode
		{
			get
			{
				return this.GetOptionsImpl(false, false).IndentationMode;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x000A30F1 File Offset: 0x000A12F1
		public int IndentationSize
		{
			get
			{
				return this.GetOptionsImpl(false, false).IndentationSize;
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000A3100 File Offset: 0x000A1300
		public MetadataFormattingOptionsBuilder WithEncoding(Encoding encoding)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.GetOptionsImpl(false, true).Encoding = encoding;
			return this;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000A311F File Offset: 0x000A131F
		public MetadataFormattingOptionsBuilder WithNewLineStyle(NewLineStyle style)
		{
			this.GetOptionsImpl(false, true).NewLineStyle = style;
			return this;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000A3130 File Offset: 0x000A1330
		public MetadataFormattingOptionsBuilder WithTabsIndentationMode()
		{
			this.GetOptionsImpl(false, true).IndentationMode = IndentationMode.Tabs;
			return this;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000A3141 File Offset: 0x000A1341
		public MetadataFormattingOptionsBuilder WithSpacesIndentationMode(int indentationSize)
		{
			if (indentationSize <= 0)
			{
				throw new ArgumentException(TomSR.Exception_InvalidIndentationLength(indentationSize.ToString(CultureInfo.InvariantCulture)), "indentationSize");
			}
			MetadataFormattingOptions optionsImpl = this.GetOptionsImpl(false, true);
			optionsImpl.IndentationMode = IndentationMode.Spaces;
			optionsImpl.IndentationSize = indentationSize;
			return this;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000A3179 File Offset: 0x000A1379
		public MetadataFormattingOptions GetOptions()
		{
			return this.GetOptionsImpl(true, false);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000A3183 File Offset: 0x000A1383
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal MetadataFormattingOptions GetOptionsImpl(bool isForUse, bool isForUpdate)
		{
			if (isForUse)
			{
				return ImmutableObjectAccessController<MetadataFormattingOptions>.GetObjectForUse(ref this.controller);
			}
			if (isForUpdate)
			{
				return ImmutableObjectAccessController<MetadataFormattingOptions>.GetObjectForUpdate(ref this.controller);
			}
			return ImmutableObjectAccessController<MetadataFormattingOptions>.GetObjectForView(ref this.controller);
		}

		// Token: 0x04000466 RID: 1126
		private ImmutableObjectAccessController<MetadataFormattingOptions> controller;
	}
}
