using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C8 RID: 456
	internal class ResponseObjectSchema : ServiceObjectSchema
	{
		// Token: 0x0600150D RID: 5389 RVA: 0x0003B256 File Offset: 0x0003A256
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ResponseObjectSchema.ReferenceItemId);
		}

		// Token: 0x04000C99 RID: 3225
		public static readonly PropertyDefinition ReferenceItemId = new ComplexPropertyDefinition<ItemId>("ReferenceItemId", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet, ExchangeVersion.Exchange2007_SP1, () => new ItemId());

		// Token: 0x04000C9A RID: 3226
		public static readonly PropertyDefinition BodyPrefix = new ComplexPropertyDefinition<MessageBody>("NewBodyContent", PropertyDefinitionFlags.CanSet, ExchangeVersion.Exchange2007_SP1, () => new MessageBody());

		// Token: 0x04000C9B RID: 3227
		internal static readonly ResponseObjectSchema Instance = new ResponseObjectSchema();
	}
}
