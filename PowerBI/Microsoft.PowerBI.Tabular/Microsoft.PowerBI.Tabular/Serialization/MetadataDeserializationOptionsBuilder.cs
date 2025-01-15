using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200017F RID: 383
	public sealed class MetadataDeserializationOptionsBuilder
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x000A2C14 File Offset: 0x000A0E14
		public MetadataDeserializationOptionsBuilder(MetadataSerializationStyle style = MetadataSerializationStyle.Default)
		{
			switch (style)
			{
			case MetadataSerializationStyle.Default:
				this.controller = new ImmutableObjectAccessController<MetadataDeserializationOptions>(new MetadataDeserializationOptions(), true);
				return;
			case MetadataSerializationStyle.Tmdl:
				this.controller = new ImmutableObjectAccessController<MetadataDeserializationOptions>(new MetadataDeserializationOptions(), true);
				return;
			case MetadataSerializationStyle.Json:
				throw new NotSupportedException(TomSR.Exception_JsonSerializationSupport);
			default:
				throw new ArgumentException(TomSR.Exception_InvalidContentStyle(style.ToString("G")), "style");
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000A2C89 File Offset: 0x000A0E89
		public MetadataDeserializationOptionsBuilder(MetadataDeserializationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.controller = new ImmutableObjectAccessController<MetadataDeserializationOptions>(options, false);
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000A2CAC File Offset: 0x000A0EAC
		public bool RaiseErrorOnUnresolvedLinks
		{
			get
			{
				return this.GetOptionsImpl(false, false).RaiseErrorOnUnresolvedLinks;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000A2CBB File Offset: 0x000A0EBB
		public MetadataCompatibilityOptions Compatibility
		{
			get
			{
				return this.GetOptionsImpl(false, false).Compatibility;
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000A2CCA File Offset: 0x000A0ECA
		public MetadataDeserializationOptionsBuilder WithErrorOnUnresolvedLinks()
		{
			this.GetOptionsImpl(false, true).RaiseErrorOnUnresolvedLinks = true;
			return this;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000A2CDB File Offset: 0x000A0EDB
		public MetadataDeserializationOptionsBuilder WithoutErrorOnUnresolvedLinks()
		{
			this.GetOptionsImpl(false, true).RaiseErrorOnUnresolvedLinks = false;
			return this;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000A2CEC File Offset: 0x000A0EEC
		public MetadataDeserializationOptionsBuilder WithCompatibilityOptions(MetadataCompatibilityOptions compatibilityOptions)
		{
			if (compatibilityOptions == null)
			{
				throw new ArgumentNullException("compatibilityOptions");
			}
			this.GetOptionsImpl(false, true).Compatibility = compatibilityOptions;
			return this;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000A2D0B File Offset: 0x000A0F0B
		public MetadataDeserializationOptionsBuilder WithoutCompatibilityOptions()
		{
			this.GetOptionsImpl(false, true).Compatibility = null;
			return this;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000A2D1C File Offset: 0x000A0F1C
		public MetadataDeserializationOptions GetOptions()
		{
			return this.GetOptionsImpl(true, false);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000A2D26 File Offset: 0x000A0F26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal MetadataDeserializationOptions GetOptionsImpl(bool isForUse, bool isForUpdate)
		{
			if (isForUse)
			{
				return ImmutableObjectAccessController<MetadataDeserializationOptions>.GetObjectForUse(ref this.controller);
			}
			if (isForUpdate)
			{
				return ImmutableObjectAccessController<MetadataDeserializationOptions>.GetObjectForUpdate(ref this.controller);
			}
			return ImmutableObjectAccessController<MetadataDeserializationOptions>.GetObjectForView(ref this.controller);
		}

		// Token: 0x04000457 RID: 1111
		private ImmutableObjectAccessController<MetadataDeserializationOptions> controller;
	}
}
