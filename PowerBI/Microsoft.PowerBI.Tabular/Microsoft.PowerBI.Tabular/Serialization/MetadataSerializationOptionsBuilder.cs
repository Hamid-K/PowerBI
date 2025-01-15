using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000189 RID: 393
	public sealed class MetadataSerializationOptionsBuilder
	{
		// Token: 0x0600183B RID: 6203 RVA: 0x000A35A4 File Offset: 0x000A17A4
		public MetadataSerializationOptionsBuilder(MetadataSerializationStyle style = MetadataSerializationStyle.Default)
		{
			switch (style)
			{
			case MetadataSerializationStyle.Default:
				this.controller = new ImmutableObjectAccessController<MetadataSerializationOptions>(new MetadataSerializationOptions(), true);
				return;
			case MetadataSerializationStyle.Tmdl:
				this.controller = new ImmutableObjectAccessController<MetadataSerializationOptions>(new TmdlSerializationOptions(), true);
				return;
			case MetadataSerializationStyle.Json:
				throw new NotSupportedException(TomSR.Exception_JsonSerializationSupport);
			default:
				throw new ArgumentException(TomSR.Exception_InvalidContentStyle(style.ToString("G")), "style");
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000A3619 File Offset: 0x000A1819
		public MetadataSerializationOptionsBuilder(MetadataSerializationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.controller = new ImmutableObjectAccessController<MetadataSerializationOptions>(options, false);
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000A363C File Offset: 0x000A183C
		public bool IncludeChildren
		{
			get
			{
				return this.GetOptionsImpl(false, false).IncludeChildren;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x000A364B File Offset: 0x000A184B
		public bool IncludeRestrictedInformation
		{
			get
			{
				return this.GetOptionsImpl(false, false).IncludeRestrictedInformation;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x000A365A File Offset: 0x000A185A
		public MetadataFormattingOptions Formatting
		{
			get
			{
				return this.GetOptionsImpl(false, false).Formatting;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x000A3669 File Offset: 0x000A1869
		public MetadataCompatibilityOptions Compatibility
		{
			get
			{
				return this.GetOptionsImpl(false, false).Compatibility;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x000A3678 File Offset: 0x000A1878
		internal ISerializationStrategy Strategy
		{
			get
			{
				return this.GetOptionsImpl(false, false).Strategy;
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000A3687 File Offset: 0x000A1887
		public MetadataSerializationOptionsBuilder WithChildrenMetadata()
		{
			this.GetOptionsImpl(false, true).IncludeChildren = true;
			return this;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000A3698 File Offset: 0x000A1898
		public MetadataSerializationOptionsBuilder WithoutChildrenMetadata()
		{
			this.GetOptionsImpl(false, true).IncludeChildren = false;
			return this;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000A36A9 File Offset: 0x000A18A9
		public MetadataSerializationOptionsBuilder WithRestrictedInformation()
		{
			this.GetOptionsImpl(false, true).IncludeRestrictedInformation = true;
			return this;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000A36BA File Offset: 0x000A18BA
		public MetadataSerializationOptionsBuilder WithoutRestrictedInformation()
		{
			this.GetOptionsImpl(false, true).IncludeRestrictedInformation = false;
			return this;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000A36CB File Offset: 0x000A18CB
		public MetadataSerializationOptionsBuilder WithFormattingOptions(MetadataFormattingOptions formattingOptions)
		{
			if (formattingOptions == null)
			{
				throw new ArgumentNullException("formattingOptions");
			}
			this.GetOptionsImpl(false, true).Formatting = formattingOptions;
			return this;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000A36EA File Offset: 0x000A18EA
		public MetadataSerializationOptionsBuilder WithoutFormattingOptions()
		{
			this.GetOptionsImpl(false, true).Formatting = null;
			return this;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000A36FB File Offset: 0x000A18FB
		public MetadataSerializationOptionsBuilder WithCompatibilityOptions(MetadataCompatibilityOptions compatibilityOptions)
		{
			if (compatibilityOptions == null)
			{
				throw new ArgumentNullException("compatibilityOptions");
			}
			this.GetOptionsImpl(false, true).Compatibility = compatibilityOptions;
			return this;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000A371A File Offset: 0x000A191A
		public MetadataSerializationOptionsBuilder WithoutCompatibilityOptions()
		{
			this.GetOptionsImpl(false, true).Compatibility = null;
			return this;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000A372B File Offset: 0x000A192B
		public MetadataSerializationOptions GetOptions()
		{
			return this.GetOptionsImpl(true, false);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000A3735 File Offset: 0x000A1935
		internal MetadataSerializationOptionsBuilder WithStrategy(ISerializationStrategy strategy)
		{
			this.GetOptionsImpl(false, true).Strategy = strategy;
			return this;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000A3746 File Offset: 0x000A1946
		internal MetadataSerializationOptionsBuilder WithoutStrategy()
		{
			this.GetOptionsImpl(false, true).Strategy = null;
			return this;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000A3757 File Offset: 0x000A1957
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal MetadataSerializationOptions GetOptionsImpl(bool isForUse, bool isForUpdate)
		{
			if (isForUse)
			{
				return ImmutableObjectAccessController<MetadataSerializationOptions>.GetObjectForUse(ref this.controller);
			}
			if (isForUpdate)
			{
				return ImmutableObjectAccessController<MetadataSerializationOptions>.GetObjectForUpdate(ref this.controller);
			}
			return ImmutableObjectAccessController<MetadataSerializationOptions>.GetObjectForView(ref this.controller);
		}

		// Token: 0x0400048C RID: 1164
		private ImmutableObjectAccessController<MetadataSerializationOptions> controller;
	}
}
