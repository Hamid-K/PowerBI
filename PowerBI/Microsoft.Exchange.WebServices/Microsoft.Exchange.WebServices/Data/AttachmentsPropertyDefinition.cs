using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C7 RID: 711
	internal sealed class AttachmentsPropertyDefinition : ComplexPropertyDefinition<AttachmentCollection>
	{
		// Token: 0x06001956 RID: 6486 RVA: 0x00044D84 File Offset: 0x00043D84
		public AttachmentsPropertyDefinition()
			: base("Attachments", "item:Attachments", PropertyDefinitionFlags.AutoInstantiateOnRead, ExchangeVersion.Exchange2007_SP1, () => new AttachmentCollection())
		{
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00044DB8 File Offset: 0x00043DB8
		internal override bool HasFlag(PropertyDefinitionFlags flag, ExchangeVersion? version)
		{
			if (version != null && version >= ExchangeVersion.Exchange2010_SP2)
			{
				return (flag & AttachmentsPropertyDefinition.Exchange2010SP2PropertyDefinitionFlags) == flag;
			}
			return base.HasFlag(flag, version);
		}

		// Token: 0x040013F3 RID: 5107
		private static readonly PropertyDefinitionFlags Exchange2010SP2PropertyDefinitionFlags = PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.ReuseInstance | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.UpdateCollectionItems;
	}
}
