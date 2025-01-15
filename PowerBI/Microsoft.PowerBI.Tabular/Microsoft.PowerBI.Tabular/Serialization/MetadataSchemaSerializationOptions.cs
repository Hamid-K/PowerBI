using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000165 RID: 357
	public class MetadataSchemaSerializationOptions : ICloneable
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0009462F File Offset: 0x0009282F
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x00094637 File Offset: 0x00092837
		public MetadataFormattingOptions Formatting { get; internal set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00094640 File Offset: 0x00092840
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x00094648 File Offset: 0x00092848
		public MetadataCompatibilityOptions Compatibility { get; internal set; }

		// Token: 0x06001648 RID: 5704 RVA: 0x00094654 File Offset: 0x00092854
		internal MetadataSchemaSerializationOptions Clone()
		{
			MetadataSchemaSerializationOptions metadataSchemaSerializationOptions = this.CreateOptionsOfSameType();
			this.CloneImpl(metadataSchemaSerializationOptions);
			return metadataSchemaSerializationOptions;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00094670 File Offset: 0x00092870
		private protected virtual MetadataSchemaSerializationOptions CreateOptionsOfSameType()
		{
			return new MetadataSchemaSerializationOptions();
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00094677 File Offset: 0x00092877
		private protected virtual void CloneImpl(MetadataSchemaSerializationOptions options)
		{
			options.Formatting = this.Formatting;
			options.Compatibility = this.Compatibility;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00094691 File Offset: 0x00092891
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
