using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Utilities;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200017D RID: 381
	public sealed class MetadataCompatibilityOptionsBuilder
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x000A2A34 File Offset: 0x000A0C34
		public MetadataCompatibilityOptionsBuilder(MetadataSerializationStyle style = MetadataSerializationStyle.Default)
		{
			switch (style)
			{
			case MetadataSerializationStyle.Default:
				this.controller = new ImmutableObjectAccessController<MetadataCompatibilityOptions>(new MetadataCompatibilityOptions(), true);
				return;
			case MetadataSerializationStyle.Tmdl:
				this.controller = new ImmutableObjectAccessController<MetadataCompatibilityOptions>(new MetadataCompatibilityOptions(), true);
				return;
			case MetadataSerializationStyle.Json:
				throw new NotSupportedException(TomSR.Exception_JsonSerializationSupport);
			default:
				throw new ArgumentException(TomSR.Exception_InvalidContentStyle(style.ToString("G")), "style");
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000A2AA9 File Offset: 0x000A0CA9
		public MetadataCompatibilityOptionsBuilder(MetadataCompatibilityOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.controller = new ImmutableObjectAccessController<MetadataCompatibilityOptions>(options, false);
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000A2ACC File Offset: 0x000A0CCC
		public CompatibilityMode CompatibilityMode
		{
			get
			{
				return this.GetOptionsImpl(false, false).CompatibilityMode;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x000A2ADB File Offset: 0x000A0CDB
		public int CompatibilityLevel
		{
			get
			{
				return this.GetOptionsImpl(false, false).CompatibilityLevel;
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000A2AEA File Offset: 0x000A0CEA
		public MetadataCompatibilityOptionsBuilder WithTargetCompatibilityMode(CompatibilityMode mode)
		{
			if (!PropertyHelper.IsValidCompatibilityMode(mode, false))
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			this.GetOptionsImpl(false, true).CompatibilityMode = mode;
			return this;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000A2B0F File Offset: 0x000A0D0F
		public MetadataCompatibilityOptionsBuilder WithoutTargetCompatibilityMode()
		{
			this.GetOptionsImpl(false, true).CompatibilityMode = CompatibilityMode.Unknown;
			return this;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000A2B20 File Offset: 0x000A0D20
		public MetadataCompatibilityOptionsBuilder WithTargetCompatibilityLevel(int level)
		{
			if (level < 1200 || (level > 1000000 && level != 2147483647))
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.GetOptionsImpl(false, true).CompatibilityLevel = level;
			return this;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000A2B54 File Offset: 0x000A0D54
		public MetadataCompatibilityOptionsBuilder WithoutTargetCompatibilityLevel()
		{
			this.GetOptionsImpl(false, true).CompatibilityLevel = -1;
			return this;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000A2B65 File Offset: 0x000A0D65
		public MetadataCompatibilityOptions GetOptions()
		{
			return this.GetOptionsImpl(true, false);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000A2B6F File Offset: 0x000A0D6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal MetadataCompatibilityOptions GetOptionsImpl(bool isForUse, bool isForUpdate)
		{
			if (isForUse)
			{
				return ImmutableObjectAccessController<MetadataCompatibilityOptions>.GetObjectForUse(ref this.controller);
			}
			if (isForUpdate)
			{
				return ImmutableObjectAccessController<MetadataCompatibilityOptions>.GetObjectForUpdate(ref this.controller);
			}
			return ImmutableObjectAccessController<MetadataCompatibilityOptions>.GetObjectForView(ref this.controller);
		}

		// Token: 0x04000454 RID: 1108
		private ImmutableObjectAccessController<MetadataCompatibilityOptions> controller;
	}
}
