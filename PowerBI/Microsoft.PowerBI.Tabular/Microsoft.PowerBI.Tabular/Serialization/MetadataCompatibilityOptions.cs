using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200017C RID: 380
	public class MetadataCompatibilityOptions : ICloneable
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x000A29B4 File Offset: 0x000A0BB4
		internal MetadataCompatibilityOptions()
		{
			this.CompatibilityMode = CompatibilityMode.Unknown;
			this.CompatibilityLevel = -1;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000A29CA File Offset: 0x000A0BCA
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x000A29D2 File Offset: 0x000A0BD2
		public CompatibilityMode CompatibilityMode { get; internal set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000A29DB File Offset: 0x000A0BDB
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x000A29E3 File Offset: 0x000A0BE3
		public int CompatibilityLevel { get; internal set; }

		// Token: 0x060017C0 RID: 6080 RVA: 0x000A29EC File Offset: 0x000A0BEC
		internal MetadataCompatibilityOptions Clone()
		{
			MetadataCompatibilityOptions metadataCompatibilityOptions = this.CreateOptionsOfSameType();
			this.CloneImpl(metadataCompatibilityOptions);
			return metadataCompatibilityOptions;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000A2A08 File Offset: 0x000A0C08
		private protected virtual MetadataCompatibilityOptions CreateOptionsOfSameType()
		{
			return new MetadataCompatibilityOptions();
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000A2A0F File Offset: 0x000A0C0F
		private protected virtual void CloneImpl(MetadataCompatibilityOptions options)
		{
			options.CompatibilityMode = this.CompatibilityMode;
			options.CompatibilityLevel = this.CompatibilityLevel;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000A2A29 File Offset: 0x000A0C29
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
