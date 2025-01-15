using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001AD RID: 429
	[Schema]
	public class ItemSchema : ServiceObjectSchema
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x00037CA4 File Offset: 0x00036CA4
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ItemSchema.MimeContent);
			base.RegisterProperty(ItemSchema.Id);
			base.RegisterProperty(ItemSchema.ParentFolderId);
			base.RegisterProperty(ItemSchema.ItemClass);
			base.RegisterProperty(ItemSchema.Subject);
			base.RegisterProperty(ItemSchema.Sensitivity);
			base.RegisterProperty(ItemSchema.Body);
			base.RegisterProperty(ItemSchema.Attachments);
			base.RegisterProperty(ItemSchema.DateTimeReceived);
			base.RegisterProperty(ItemSchema.Size);
			base.RegisterProperty(ItemSchema.Categories);
			base.RegisterProperty(ItemSchema.Importance);
			base.RegisterProperty(ItemSchema.InReplyTo);
			base.RegisterProperty(ItemSchema.IsSubmitted);
			base.RegisterProperty(ItemSchema.IsDraft);
			base.RegisterProperty(ItemSchema.IsFromMe);
			base.RegisterProperty(ItemSchema.IsResend);
			base.RegisterProperty(ItemSchema.IsUnmodified);
			base.RegisterProperty(ItemSchema.InternetMessageHeaders);
			base.RegisterProperty(ItemSchema.DateTimeSent);
			base.RegisterProperty(ItemSchema.DateTimeCreated);
			base.RegisterProperty(ItemSchema.AllowedResponseActions);
			base.RegisterProperty(ItemSchema.ReminderDueBy);
			base.RegisterProperty(ItemSchema.IsReminderSet);
			base.RegisterProperty(ItemSchema.ReminderMinutesBeforeStart);
			base.RegisterProperty(ItemSchema.DisplayCc);
			base.RegisterProperty(ItemSchema.DisplayTo);
			base.RegisterProperty(ItemSchema.HasAttachments);
			base.RegisterProperty(ServiceObjectSchema.ExtendedProperties);
			base.RegisterProperty(ItemSchema.Culture);
			base.RegisterProperty(ItemSchema.EffectiveRights);
			base.RegisterProperty(ItemSchema.LastModifiedName);
			base.RegisterProperty(ItemSchema.LastModifiedTime);
			base.RegisterProperty(ItemSchema.IsAssociated);
			base.RegisterProperty(ItemSchema.WebClientReadFormQueryString);
			base.RegisterProperty(ItemSchema.WebClientEditFormQueryString);
			base.RegisterProperty(ItemSchema.ConversationId);
			base.RegisterProperty(ItemSchema.UniqueBody);
			base.RegisterProperty(ItemSchema.Flag);
			base.RegisterProperty(ItemSchema.StoreEntryId);
			base.RegisterProperty(ItemSchema.InstanceKey);
			base.RegisterProperty(ItemSchema.NormalizedBody);
			base.RegisterProperty(ItemSchema.EntityExtractionResult);
			base.RegisterProperty(ItemSchema.PolicyTag);
			base.RegisterProperty(ItemSchema.ArchiveTag);
			base.RegisterProperty(ItemSchema.RetentionDate);
			base.RegisterProperty(ItemSchema.Preview);
			base.RegisterProperty(ItemSchema.TextBody);
			base.RegisterProperty(ItemSchema.IconIndex);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00037ED2 File Offset: 0x00036ED2
		internal ItemSchema()
		{
		}

		// Token: 0x04000A1D RID: 2589
		public static readonly PropertyDefinition Id = new ComplexPropertyDefinition<ItemId>("ItemId", "item:ItemId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new ItemId());

		// Token: 0x04000A1E RID: 2590
		public static readonly PropertyDefinition Body = new ComplexPropertyDefinition<MessageBody>("Body", "item:Body", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new MessageBody());

		// Token: 0x04000A1F RID: 2591
		public static readonly PropertyDefinition ItemClass = new StringPropertyDefinition("ItemClass", "item:ItemClass", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A20 RID: 2592
		public static readonly PropertyDefinition Subject = new StringPropertyDefinition("Subject", "item:Subject", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A21 RID: 2593
		public static readonly PropertyDefinition MimeContent = new ComplexPropertyDefinition<MimeContent>("MimeContent", "item:MimeContent", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.MustBeExplicitlyLoaded, ExchangeVersion.Exchange2007_SP1, () => new MimeContent());

		// Token: 0x04000A22 RID: 2594
		public static readonly PropertyDefinition ParentFolderId = new ComplexPropertyDefinition<FolderId>("ParentFolderId", "item:ParentFolderId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new FolderId());

		// Token: 0x04000A23 RID: 2595
		public static readonly PropertyDefinition Sensitivity = new GenericPropertyDefinition<Sensitivity>("Sensitivity", "item:Sensitivity", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A24 RID: 2596
		public static readonly PropertyDefinition Attachments = new AttachmentsPropertyDefinition();

		// Token: 0x04000A25 RID: 2597
		public static readonly PropertyDefinition DateTimeReceived = new DateTimePropertyDefinition("DateTimeReceived", "item:DateTimeReceived", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A26 RID: 2598
		public static readonly PropertyDefinition Size = new IntPropertyDefinition("Size", "item:Size", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A27 RID: 2599
		public static readonly PropertyDefinition Categories = new ComplexPropertyDefinition<StringList>("Categories", "item:Categories", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new StringList());

		// Token: 0x04000A28 RID: 2600
		public static readonly PropertyDefinition Importance = new GenericPropertyDefinition<Importance>("Importance", "item:Importance", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A29 RID: 2601
		public static readonly PropertyDefinition InReplyTo = new StringPropertyDefinition("InReplyTo", "item:InReplyTo", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A2A RID: 2602
		public static readonly PropertyDefinition IsSubmitted = new BoolPropertyDefinition("IsSubmitted", "item:IsSubmitted", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A2B RID: 2603
		public static readonly PropertyDefinition IsAssociated = new BoolPropertyDefinition("IsAssociated", "item:IsAssociated", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010);

		// Token: 0x04000A2C RID: 2604
		public static readonly PropertyDefinition IsDraft = new BoolPropertyDefinition("IsDraft", "item:IsDraft", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A2D RID: 2605
		public static readonly PropertyDefinition IsFromMe = new BoolPropertyDefinition("IsFromMe", "item:IsFromMe", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A2E RID: 2606
		public static readonly PropertyDefinition IsResend = new BoolPropertyDefinition("IsResend", "item:IsResend", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A2F RID: 2607
		public static readonly PropertyDefinition IsUnmodified = new BoolPropertyDefinition("IsUnmodified", "item:IsUnmodified", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A30 RID: 2608
		public static readonly PropertyDefinition InternetMessageHeaders = new ComplexPropertyDefinition<InternetMessageHeaderCollection>("InternetMessageHeaders", "item:InternetMessageHeaders", ExchangeVersion.Exchange2007_SP1, () => new InternetMessageHeaderCollection());

		// Token: 0x04000A31 RID: 2609
		public static readonly PropertyDefinition DateTimeSent = new DateTimePropertyDefinition("DateTimeSent", "item:DateTimeSent", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A32 RID: 2610
		public static readonly PropertyDefinition DateTimeCreated = new DateTimePropertyDefinition("DateTimeCreated", "item:DateTimeCreated", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A33 RID: 2611
		public static readonly PropertyDefinition AllowedResponseActions = new ResponseObjectsPropertyDefinition("ResponseObjects", "item:ResponseObjects", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A34 RID: 2612
		public static readonly PropertyDefinition ReminderDueBy = new ScopedDateTimePropertyDefinition("ReminderDueBy", "item:ReminderDueBy", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, (ExchangeVersion version) => AppointmentSchema.StartTimeZone);

		// Token: 0x04000A35 RID: 2613
		public static readonly PropertyDefinition IsReminderSet = new BoolPropertyDefinition("ReminderIsSet", "item:ReminderIsSet", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A36 RID: 2614
		public static readonly PropertyDefinition ReminderMinutesBeforeStart = new IntPropertyDefinition("ReminderMinutesBeforeStart", "item:ReminderMinutesBeforeStart", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A37 RID: 2615
		public static readonly PropertyDefinition DisplayCc = new StringPropertyDefinition("DisplayCc", "item:DisplayCc", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A38 RID: 2616
		public static readonly PropertyDefinition DisplayTo = new StringPropertyDefinition("DisplayTo", "item:DisplayTo", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A39 RID: 2617
		public static readonly PropertyDefinition HasAttachments = new BoolPropertyDefinition("HasAttachments", "item:HasAttachments", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A3A RID: 2618
		public static readonly PropertyDefinition Culture = new StringPropertyDefinition("Culture", "item:Culture", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A3B RID: 2619
		public static readonly PropertyDefinition EffectiveRights = new EffectiveRightsPropertyDefinition("EffectiveRights", "item:EffectiveRights", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A3C RID: 2620
		public static readonly PropertyDefinition LastModifiedName = new StringPropertyDefinition("LastModifiedName", "item:LastModifiedName", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A3D RID: 2621
		public static readonly PropertyDefinition LastModifiedTime = new DateTimePropertyDefinition("LastModifiedTime", "item:LastModifiedTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A3E RID: 2622
		public static readonly PropertyDefinition WebClientReadFormQueryString = new StringPropertyDefinition("WebClientReadFormQueryString", "item:WebClientReadFormQueryString", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010);

		// Token: 0x04000A3F RID: 2623
		public static readonly PropertyDefinition WebClientEditFormQueryString = new StringPropertyDefinition("WebClientEditFormQueryString", "item:WebClientEditFormQueryString", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010);

		// Token: 0x04000A40 RID: 2624
		public static readonly PropertyDefinition ConversationId = new ComplexPropertyDefinition<ConversationId>("ConversationId", "item:ConversationId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010, () => new ConversationId());

		// Token: 0x04000A41 RID: 2625
		public static readonly PropertyDefinition UniqueBody = new ComplexPropertyDefinition<UniqueBody>("UniqueBody", "item:UniqueBody", PropertyDefinitionFlags.MustBeExplicitlyLoaded, ExchangeVersion.Exchange2010, () => new UniqueBody());

		// Token: 0x04000A42 RID: 2626
		public static readonly PropertyDefinition StoreEntryId = new ByteArrayPropertyDefinition("StoreEntryId", "item:StoreEntryId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP2);

		// Token: 0x04000A43 RID: 2627
		public static readonly PropertyDefinition InstanceKey = new ByteArrayPropertyDefinition("InstanceKey", "item:InstanceKey", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000A44 RID: 2628
		public static readonly PropertyDefinition NormalizedBody = new ComplexPropertyDefinition<NormalizedBody>("NormalizedBody", "item:NormalizedBody", PropertyDefinitionFlags.MustBeExplicitlyLoaded, ExchangeVersion.Exchange2013, () => new NormalizedBody());

		// Token: 0x04000A45 RID: 2629
		public static readonly PropertyDefinition EntityExtractionResult = new ComplexPropertyDefinition<EntityExtractionResult>("EntityExtractionResult", "item:EntityExtractionResult", PropertyDefinitionFlags.MustBeExplicitlyLoaded, ExchangeVersion.Exchange2013, () => new EntityExtractionResult());

		// Token: 0x04000A46 RID: 2630
		public static readonly PropertyDefinition Flag = new ComplexPropertyDefinition<Flag>("Flag", "item:Flag", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new Flag());

		// Token: 0x04000A47 RID: 2631
		public static readonly PropertyDefinition PolicyTag = new ComplexPropertyDefinition<PolicyTag>("PolicyTag", "item:PolicyTag", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new PolicyTag());

		// Token: 0x04000A48 RID: 2632
		public static readonly PropertyDefinition ArchiveTag = new ComplexPropertyDefinition<ArchiveTag>("ArchiveTag", "item:ArchiveTag", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new ArchiveTag());

		// Token: 0x04000A49 RID: 2633
		public static readonly PropertyDefinition RetentionDate = new DateTimePropertyDefinition("RetentionDate", "item:RetentionDate", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, true);

		// Token: 0x04000A4A RID: 2634
		public static readonly PropertyDefinition Preview = new StringPropertyDefinition("Preview", "item:Preview", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000A4B RID: 2635
		public static readonly PropertyDefinition TextBody = new ComplexPropertyDefinition<TextBody>("TextBody", "item:TextBody", PropertyDefinitionFlags.MustBeExplicitlyLoaded, ExchangeVersion.Exchange2013, () => new TextBody());

		// Token: 0x04000A4C RID: 2636
		public static readonly PropertyDefinition IconIndex = new GenericPropertyDefinition<IconIndex>("IconIndex", "item:IconIndex", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000A4D RID: 2637
		internal static readonly ItemSchema Instance = new ItemSchema();

		// Token: 0x020001AE RID: 430
		private static class FieldUris
		{
			// Token: 0x04000A5D RID: 2653
			public const string ItemId = "item:ItemId";

			// Token: 0x04000A5E RID: 2654
			public const string ParentFolderId = "item:ParentFolderId";

			// Token: 0x04000A5F RID: 2655
			public const string ItemClass = "item:ItemClass";

			// Token: 0x04000A60 RID: 2656
			public const string MimeContent = "item:MimeContent";

			// Token: 0x04000A61 RID: 2657
			public const string Attachments = "item:Attachments";

			// Token: 0x04000A62 RID: 2658
			public const string Subject = "item:Subject";

			// Token: 0x04000A63 RID: 2659
			public const string DateTimeReceived = "item:DateTimeReceived";

			// Token: 0x04000A64 RID: 2660
			public const string Size = "item:Size";

			// Token: 0x04000A65 RID: 2661
			public const string Categories = "item:Categories";

			// Token: 0x04000A66 RID: 2662
			public const string HasAttachments = "item:HasAttachments";

			// Token: 0x04000A67 RID: 2663
			public const string Importance = "item:Importance";

			// Token: 0x04000A68 RID: 2664
			public const string InReplyTo = "item:InReplyTo";

			// Token: 0x04000A69 RID: 2665
			public const string InternetMessageHeaders = "item:InternetMessageHeaders";

			// Token: 0x04000A6A RID: 2666
			public const string IsAssociated = "item:IsAssociated";

			// Token: 0x04000A6B RID: 2667
			public const string IsDraft = "item:IsDraft";

			// Token: 0x04000A6C RID: 2668
			public const string IsFromMe = "item:IsFromMe";

			// Token: 0x04000A6D RID: 2669
			public const string IsResend = "item:IsResend";

			// Token: 0x04000A6E RID: 2670
			public const string IsSubmitted = "item:IsSubmitted";

			// Token: 0x04000A6F RID: 2671
			public const string IsUnmodified = "item:IsUnmodified";

			// Token: 0x04000A70 RID: 2672
			public const string DateTimeSent = "item:DateTimeSent";

			// Token: 0x04000A71 RID: 2673
			public const string DateTimeCreated = "item:DateTimeCreated";

			// Token: 0x04000A72 RID: 2674
			public const string Body = "item:Body";

			// Token: 0x04000A73 RID: 2675
			public const string ResponseObjects = "item:ResponseObjects";

			// Token: 0x04000A74 RID: 2676
			public const string Sensitivity = "item:Sensitivity";

			// Token: 0x04000A75 RID: 2677
			public const string ReminderDueBy = "item:ReminderDueBy";

			// Token: 0x04000A76 RID: 2678
			public const string ReminderIsSet = "item:ReminderIsSet";

			// Token: 0x04000A77 RID: 2679
			public const string ReminderMinutesBeforeStart = "item:ReminderMinutesBeforeStart";

			// Token: 0x04000A78 RID: 2680
			public const string DisplayTo = "item:DisplayTo";

			// Token: 0x04000A79 RID: 2681
			public const string DisplayCc = "item:DisplayCc";

			// Token: 0x04000A7A RID: 2682
			public const string Culture = "item:Culture";

			// Token: 0x04000A7B RID: 2683
			public const string EffectiveRights = "item:EffectiveRights";

			// Token: 0x04000A7C RID: 2684
			public const string LastModifiedName = "item:LastModifiedName";

			// Token: 0x04000A7D RID: 2685
			public const string LastModifiedTime = "item:LastModifiedTime";

			// Token: 0x04000A7E RID: 2686
			public const string WebClientReadFormQueryString = "item:WebClientReadFormQueryString";

			// Token: 0x04000A7F RID: 2687
			public const string WebClientEditFormQueryString = "item:WebClientEditFormQueryString";

			// Token: 0x04000A80 RID: 2688
			public const string ConversationId = "item:ConversationId";

			// Token: 0x04000A81 RID: 2689
			public const string UniqueBody = "item:UniqueBody";

			// Token: 0x04000A82 RID: 2690
			public const string StoreEntryId = "item:StoreEntryId";

			// Token: 0x04000A83 RID: 2691
			public const string InstanceKey = "item:InstanceKey";

			// Token: 0x04000A84 RID: 2692
			public const string NormalizedBody = "item:NormalizedBody";

			// Token: 0x04000A85 RID: 2693
			public const string EntityExtractionResult = "item:EntityExtractionResult";

			// Token: 0x04000A86 RID: 2694
			public const string Flag = "item:Flag";

			// Token: 0x04000A87 RID: 2695
			public const string PolicyTag = "item:PolicyTag";

			// Token: 0x04000A88 RID: 2696
			public const string ArchiveTag = "item:ArchiveTag";

			// Token: 0x04000A89 RID: 2697
			public const string RetentionDate = "item:RetentionDate";

			// Token: 0x04000A8A RID: 2698
			public const string Preview = "item:Preview";

			// Token: 0x04000A8B RID: 2699
			public const string TextBody = "item:TextBody";

			// Token: 0x04000A8C RID: 2700
			public const string IconIndex = "item:IconIndex";
		}
	}
}
