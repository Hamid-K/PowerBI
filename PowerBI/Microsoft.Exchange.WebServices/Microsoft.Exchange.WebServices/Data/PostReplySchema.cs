using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C6 RID: 454
	internal sealed class PostReplySchema : ServiceObjectSchema
	{
		// Token: 0x06001507 RID: 5383 RVA: 0x0003B184 File Offset: 0x0003A184
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ItemSchema.Subject);
			base.RegisterProperty(ItemSchema.Body);
			base.RegisterProperty(ResponseObjectSchema.ReferenceItemId);
			base.RegisterProperty(ResponseObjectSchema.BodyPrefix);
		}

		// Token: 0x04000C97 RID: 3223
		internal static readonly PostReplySchema Instance = new PostReplySchema();
	}
}
