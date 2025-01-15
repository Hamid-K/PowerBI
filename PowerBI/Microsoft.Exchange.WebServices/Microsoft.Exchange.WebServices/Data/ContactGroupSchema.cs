using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001B5 RID: 437
	[Schema]
	public class ContactGroupSchema : ItemSchema
	{
		// Token: 0x060014CD RID: 5325 RVA: 0x000395B9 File Offset: 0x000385B9
		internal ContactGroupSchema()
		{
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000395C1 File Offset: 0x000385C1
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ContactGroupSchema.DisplayName);
			base.RegisterProperty(ContactGroupSchema.FileAs);
			base.RegisterProperty(ContactGroupSchema.Members);
		}

		// Token: 0x04000B53 RID: 2899
		public static readonly PropertyDefinition DisplayName = ContactSchema.DisplayName;

		// Token: 0x04000B54 RID: 2900
		public static readonly PropertyDefinition FileAs = ContactSchema.FileAs;

		// Token: 0x04000B55 RID: 2901
		public static readonly PropertyDefinition Members = new ComplexPropertyDefinition<GroupMemberCollection>("Members", "distributionlist:Members", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2010, () => new GroupMemberCollection());

		// Token: 0x04000B56 RID: 2902
		internal new static readonly ContactGroupSchema Instance = new ContactGroupSchema();

		// Token: 0x020001B6 RID: 438
		private static class FieldUris
		{
			// Token: 0x04000B58 RID: 2904
			public const string Members = "distributionlist:Members";
		}
	}
}
