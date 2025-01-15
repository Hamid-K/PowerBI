using System;

namespace Microsoft.Identity.Client.AppConfig
{
	// Token: 0x020002CB RID: 715
	public class ManagedIdentityId
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00056CCE File Offset: 0x00054ECE
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00056CD6 File Offset: 0x00054ED6
		internal string UserAssignedId { get; private set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00056CDF File Offset: 0x00054EDF
		internal ManagedIdentityIdType IdType { get; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00056CE7 File Offset: 0x00054EE7
		internal bool IsUserAssigned { get; }

		// Token: 0x06001ABA RID: 6842 RVA: 0x00056CEF File Offset: 0x00054EEF
		private ManagedIdentityId(ManagedIdentityIdType idType)
		{
			this.IdType = idType;
			this.IsUserAssigned = idType > ManagedIdentityIdType.SystemAssigned;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00056D08 File Offset: 0x00054F08
		public static ManagedIdentityId SystemAssigned { get; } = new ManagedIdentityId(ManagedIdentityIdType.SystemAssigned);

		// Token: 0x06001ABC RID: 6844 RVA: 0x00056D0F File Offset: 0x00054F0F
		public static ManagedIdentityId WithUserAssignedClientId(string clientId)
		{
			if (string.IsNullOrEmpty(clientId))
			{
				throw new ArgumentNullException(clientId);
			}
			return new ManagedIdentityId(ManagedIdentityIdType.ClientId)
			{
				UserAssignedId = clientId
			};
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00056D2D File Offset: 0x00054F2D
		public static ManagedIdentityId WithUserAssignedResourceId(string resourceId)
		{
			if (string.IsNullOrEmpty(resourceId))
			{
				throw new ArgumentNullException(resourceId);
			}
			return new ManagedIdentityId(ManagedIdentityIdType.ResourceId)
			{
				UserAssignedId = resourceId
			};
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00056D4B File Offset: 0x00054F4B
		public static ManagedIdentityId WithUserAssignedObjectId(string objectId)
		{
			if (string.IsNullOrEmpty(objectId))
			{
				throw new ArgumentNullException(objectId);
			}
			return new ManagedIdentityId(ManagedIdentityIdType.ObjectId)
			{
				UserAssignedId = objectId
			};
		}
	}
}
