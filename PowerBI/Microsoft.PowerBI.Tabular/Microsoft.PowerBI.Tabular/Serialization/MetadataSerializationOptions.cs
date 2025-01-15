using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000188 RID: 392
	public class MetadataSerializationOptions : ICloneable
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x000A34D3 File Offset: 0x000A16D3
		internal MetadataSerializationOptions()
		{
			this.IncludeChildren = true;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000A34E2 File Offset: 0x000A16E2
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x000A34EA File Offset: 0x000A16EA
		public bool IncludeChildren { get; internal set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x000A34F3 File Offset: 0x000A16F3
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x000A34FB File Offset: 0x000A16FB
		public bool IncludeRestrictedInformation { get; internal set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x000A3504 File Offset: 0x000A1704
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x000A350C File Offset: 0x000A170C
		public MetadataFormattingOptions Formatting { get; internal set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x000A3515 File Offset: 0x000A1715
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x000A351D File Offset: 0x000A171D
		public MetadataCompatibilityOptions Compatibility { get; internal set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000A3526 File Offset: 0x000A1726
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x000A352E File Offset: 0x000A172E
		internal ISerializationStrategy Strategy { get; set; }

		// Token: 0x06001837 RID: 6199 RVA: 0x000A3538 File Offset: 0x000A1738
		internal MetadataSerializationOptions Clone()
		{
			MetadataSerializationOptions metadataSerializationOptions = this.CreateOptionsOfSameType();
			this.CloneImpl(metadataSerializationOptions);
			return metadataSerializationOptions;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000A3554 File Offset: 0x000A1754
		private protected virtual MetadataSerializationOptions CreateOptionsOfSameType()
		{
			return new MetadataSerializationOptions();
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000A355B File Offset: 0x000A175B
		private protected virtual void CloneImpl(MetadataSerializationOptions options)
		{
			options.IncludeChildren = this.IncludeChildren;
			options.IncludeRestrictedInformation = this.IncludeRestrictedInformation;
			options.Formatting = this.Formatting;
			options.Compatibility = this.Compatibility;
			options.Strategy = this.Strategy;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000A3599 File Offset: 0x000A1799
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
