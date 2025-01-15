using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000044 RID: 68
	public abstract class EntityTrackerBase
	{
		// Token: 0x06000209 RID: 521
		internal abstract object TryGetEntity(Uri resourceUri, out EntityStates state);

		// Token: 0x0600020A RID: 522
		internal abstract IEnumerable<LinkDescriptor> GetLinks(object source, string sourceProperty);

		// Token: 0x0600020B RID: 523
		internal abstract EntityDescriptor InternalAttachEntityDescriptor(EntityDescriptor entityDescriptorFromMaterializer, bool failIfDuplicated);

		// Token: 0x0600020C RID: 524
		internal abstract EntityDescriptor GetEntityDescriptor(object resource);

		// Token: 0x0600020D RID: 525
		internal abstract void DetachExistingLink(LinkDescriptor existingLink, bool targetDelete);

		// Token: 0x0600020E RID: 526
		internal abstract void AttachLink(object source, string sourceProperty, object target, MergeOption linkMerge);

		// Token: 0x0600020F RID: 527
		internal abstract void AttachIdentity(EntityDescriptor entityDescriptorFromMaterializer, MergeOption metadataMergeOption);
	}
}
