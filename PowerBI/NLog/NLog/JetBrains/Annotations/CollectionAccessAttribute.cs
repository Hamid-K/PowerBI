using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001EE RID: 494
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	internal sealed class CollectionAccessAttribute : Attribute
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x00036ABB File Offset: 0x00034CBB
		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.CollectionAccessType = collectionAccessType;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00036ACA File Offset: 0x00034CCA
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x00036AD2 File Offset: 0x00034CD2
		public CollectionAccessType CollectionAccessType { get; private set; }
	}
}
