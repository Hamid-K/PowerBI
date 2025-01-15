using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200017E RID: 382
	public class MetadataDeserializationOptions : ICloneable
	{
		// Token: 0x060017CE RID: 6094 RVA: 0x000A2B9A File Offset: 0x000A0D9A
		internal MetadataDeserializationOptions()
		{
			this.RaiseErrorOnUnresolvedLinks = true;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000A2BA9 File Offset: 0x000A0DA9
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x000A2BB1 File Offset: 0x000A0DB1
		public bool RaiseErrorOnUnresolvedLinks { get; internal set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000A2BBA File Offset: 0x000A0DBA
		// (set) Token: 0x060017D2 RID: 6098 RVA: 0x000A2BC2 File Offset: 0x000A0DC2
		public MetadataCompatibilityOptions Compatibility { get; internal set; }

		// Token: 0x060017D3 RID: 6099 RVA: 0x000A2BCC File Offset: 0x000A0DCC
		internal MetadataDeserializationOptions Clone()
		{
			MetadataDeserializationOptions metadataDeserializationOptions = this.CreateOptionsOfSameType();
			this.CloneImpl(metadataDeserializationOptions);
			return metadataDeserializationOptions;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000A2BE8 File Offset: 0x000A0DE8
		private protected virtual MetadataDeserializationOptions CreateOptionsOfSameType()
		{
			return new MetadataDeserializationOptions();
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000A2BEF File Offset: 0x000A0DEF
		private protected virtual void CloneImpl(MetadataDeserializationOptions options)
		{
			options.RaiseErrorOnUnresolvedLinks = this.RaiseErrorOnUnresolvedLinks;
			options.Compatibility = this.Compatibility;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000A2C09 File Offset: 0x000A0E09
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
