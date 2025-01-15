using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000166 RID: 358
	public sealed class MetadataSchemaSerializationOptionsBuilder
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x000946A4 File Offset: 0x000928A4
		public MetadataSchemaSerializationOptionsBuilder(MetadataSerializationStyle style = MetadataSerializationStyle.Default)
		{
			switch (style)
			{
			case MetadataSerializationStyle.Default:
				this.controller = new ImmutableObjectAccessController<MetadataSchemaSerializationOptions>(new MetadataSchemaSerializationOptions(), true);
				return;
			case MetadataSerializationStyle.Tmdl:
				this.controller = new ImmutableObjectAccessController<MetadataSchemaSerializationOptions>(new MetadataSchemaSerializationOptions(), true);
				return;
			case MetadataSerializationStyle.Json:
				throw new NotSupportedException(TomSR.Exception_JsonSerializationSupport);
			default:
				throw new ArgumentException(TomSR.Exception_InvalidContentStyle(style.ToString("G")), "style");
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00094719 File Offset: 0x00092919
		public MetadataSchemaSerializationOptionsBuilder(MetadataSchemaSerializationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.controller = new ImmutableObjectAccessController<MetadataSchemaSerializationOptions>(options, false);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0009473C File Offset: 0x0009293C
		public MetadataFormattingOptions Formatting
		{
			get
			{
				return this.GetOptionsImpl(false, false).Formatting;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0009474B File Offset: 0x0009294B
		public MetadataCompatibilityOptions Compatibility
		{
			get
			{
				return this.GetOptionsImpl(false, false).Compatibility;
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0009475A File Offset: 0x0009295A
		public MetadataSchemaSerializationOptionsBuilder WithFormattingOptions(MetadataFormattingOptions formattingOptions)
		{
			if (formattingOptions == null)
			{
				throw new ArgumentNullException("formattingOptions");
			}
			this.GetOptionsImpl(false, true).Formatting = formattingOptions;
			return this;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00094779 File Offset: 0x00092979
		public MetadataSchemaSerializationOptionsBuilder WithoutFormattingOptions()
		{
			this.GetOptionsImpl(false, true).Formatting = null;
			return this;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0009478A File Offset: 0x0009298A
		public MetadataSchemaSerializationOptionsBuilder WithCompatibilityOptions(MetadataCompatibilityOptions compatibilityOptions)
		{
			if (compatibilityOptions == null)
			{
				throw new ArgumentNullException("compatibilityOptions");
			}
			this.GetOptionsImpl(false, true).Compatibility = compatibilityOptions;
			return this;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000947A9 File Offset: 0x000929A9
		public MetadataSchemaSerializationOptionsBuilder WithoutCompatibilityOptions()
		{
			this.GetOptionsImpl(false, true).Compatibility = null;
			return this;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000947BA File Offset: 0x000929BA
		public MetadataSchemaSerializationOptions GetOptions()
		{
			return this.GetOptionsImpl(true, false);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000947C4 File Offset: 0x000929C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal MetadataSchemaSerializationOptions GetOptionsImpl(bool isForUse, bool isForUpdate)
		{
			if (isForUse)
			{
				return ImmutableObjectAccessController<MetadataSchemaSerializationOptions>.GetObjectForUse(ref this.controller);
			}
			if (isForUpdate)
			{
				return ImmutableObjectAccessController<MetadataSchemaSerializationOptions>.GetObjectForUpdate(ref this.controller);
			}
			return ImmutableObjectAccessController<MetadataSchemaSerializationOptions>.GetObjectForView(ref this.controller);
		}

		// Token: 0x0400041A RID: 1050
		private ImmutableObjectAccessController<MetadataSchemaSerializationOptions> controller;
	}
}
