using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001BB RID: 443
	[Schema]
	public class FolderSchema : ServiceObjectSchema
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x0003A698 File Offset: 0x00039698
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(FolderSchema.Id);
			base.RegisterProperty(FolderSchema.ParentFolderId);
			base.RegisterProperty(FolderSchema.FolderClass);
			base.RegisterProperty(FolderSchema.DisplayName);
			base.RegisterProperty(FolderSchema.TotalCount);
			base.RegisterProperty(FolderSchema.ChildFolderCount);
			base.RegisterProperty(ServiceObjectSchema.ExtendedProperties);
			base.RegisterProperty(FolderSchema.ManagedFolderInformation);
			base.RegisterProperty(FolderSchema.EffectiveRights);
			base.RegisterProperty(FolderSchema.Permissions);
			base.RegisterProperty(FolderSchema.UnreadCount);
			base.RegisterProperty(FolderSchema.WellKnownFolderName);
			base.RegisterProperty(FolderSchema.PolicyTag);
			base.RegisterProperty(FolderSchema.ArchiveTag);
		}

		// Token: 0x04000C18 RID: 3096
		public static readonly PropertyDefinition Id = new ComplexPropertyDefinition<FolderId>("FolderId", "folder:FolderId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new FolderId());

		// Token: 0x04000C19 RID: 3097
		public static readonly PropertyDefinition FolderClass = new StringPropertyDefinition("FolderClass", "folder:FolderClass", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C1A RID: 3098
		public static readonly PropertyDefinition ParentFolderId = new ComplexPropertyDefinition<FolderId>("ParentFolderId", "folder:ParentFolderId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new FolderId());

		// Token: 0x04000C1B RID: 3099
		public static readonly PropertyDefinition ChildFolderCount = new IntPropertyDefinition("ChildFolderCount", "folder:ChildFolderCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C1C RID: 3100
		public static readonly PropertyDefinition DisplayName = new StringPropertyDefinition("DisplayName", "folder:DisplayName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C1D RID: 3101
		public static readonly PropertyDefinition UnreadCount = new IntPropertyDefinition("UnreadCount", "folder:UnreadCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C1E RID: 3102
		public static readonly PropertyDefinition TotalCount = new IntPropertyDefinition("TotalCount", "folder:TotalCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C1F RID: 3103
		public static readonly PropertyDefinition ManagedFolderInformation = new ComplexPropertyDefinition<ManagedFolderInformation>("ManagedFolderInformation", "folder:ManagedFolderInformation", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new ManagedFolderInformation());

		// Token: 0x04000C20 RID: 3104
		public static readonly PropertyDefinition EffectiveRights = new EffectiveRightsPropertyDefinition("EffectiveRights", "folder:EffectiveRights", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C21 RID: 3105
		public static readonly PropertyDefinition Permissions = new PermissionSetPropertyDefinition("PermissionSet", "folder:PermissionSet", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.MustBeExplicitlyLoaded, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C22 RID: 3106
		public static readonly PropertyDefinition WellKnownFolderName = new GenericPropertyDefinition<WellKnownFolderName>("DistinguishedFolderId", "folder:DistinguishedFolderId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, true);

		// Token: 0x04000C23 RID: 3107
		public static readonly PropertyDefinition PolicyTag = new ComplexPropertyDefinition<PolicyTag>("PolicyTag", "folder:PolicyTag", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new PolicyTag());

		// Token: 0x04000C24 RID: 3108
		public static readonly PropertyDefinition ArchiveTag = new ComplexPropertyDefinition<ArchiveTag>("ArchiveTag", "folder:ArchiveTag", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new ArchiveTag());

		// Token: 0x04000C25 RID: 3109
		internal static readonly FolderSchema Instance = new FolderSchema();

		// Token: 0x020001BC RID: 444
		private static class FieldUris
		{
			// Token: 0x04000C2B RID: 3115
			public const string FolderId = "folder:FolderId";

			// Token: 0x04000C2C RID: 3116
			public const string ParentFolderId = "folder:ParentFolderId";

			// Token: 0x04000C2D RID: 3117
			public const string DisplayName = "folder:DisplayName";

			// Token: 0x04000C2E RID: 3118
			public const string UnreadCount = "folder:UnreadCount";

			// Token: 0x04000C2F RID: 3119
			public const string TotalCount = "folder:TotalCount";

			// Token: 0x04000C30 RID: 3120
			public const string ChildFolderCount = "folder:ChildFolderCount";

			// Token: 0x04000C31 RID: 3121
			public const string FolderClass = "folder:FolderClass";

			// Token: 0x04000C32 RID: 3122
			public const string ManagedFolderInformation = "folder:ManagedFolderInformation";

			// Token: 0x04000C33 RID: 3123
			public const string EffectiveRights = "folder:EffectiveRights";

			// Token: 0x04000C34 RID: 3124
			public const string PermissionSet = "folder:PermissionSet";

			// Token: 0x04000C35 RID: 3125
			public const string PolicyTag = "folder:PolicyTag";

			// Token: 0x04000C36 RID: 3126
			public const string ArchiveTag = "folder:ArchiveTag";

			// Token: 0x04000C37 RID: 3127
			public const string DistinguishedFolderId = "folder:DistinguishedFolderId";
		}
	}
}
