using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001B3 RID: 435
	[Schema]
	public class ConversationSchema : ServiceObjectSchema
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x00038EBC File Offset: 0x00037EBC
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ConversationSchema.Id);
			base.RegisterProperty(ConversationSchema.Topic);
			base.RegisterProperty(ConversationSchema.UniqueRecipients);
			base.RegisterProperty(ConversationSchema.GlobalUniqueRecipients);
			base.RegisterProperty(ConversationSchema.UniqueUnreadSenders);
			base.RegisterProperty(ConversationSchema.GlobalUniqueUnreadSenders);
			base.RegisterProperty(ConversationSchema.UniqueSenders);
			base.RegisterProperty(ConversationSchema.GlobalUniqueSenders);
			base.RegisterProperty(ConversationSchema.LastDeliveryTime);
			base.RegisterProperty(ConversationSchema.GlobalLastDeliveryTime);
			base.RegisterProperty(ConversationSchema.Categories);
			base.RegisterProperty(ConversationSchema.GlobalCategories);
			base.RegisterProperty(ConversationSchema.FlagStatus);
			base.RegisterProperty(ConversationSchema.GlobalFlagStatus);
			base.RegisterProperty(ConversationSchema.HasAttachments);
			base.RegisterProperty(ConversationSchema.GlobalHasAttachments);
			base.RegisterProperty(ConversationSchema.MessageCount);
			base.RegisterProperty(ConversationSchema.GlobalMessageCount);
			base.RegisterProperty(ConversationSchema.UnreadCount);
			base.RegisterProperty(ConversationSchema.GlobalUnreadCount);
			base.RegisterProperty(ConversationSchema.Size);
			base.RegisterProperty(ConversationSchema.GlobalSize);
			base.RegisterProperty(ConversationSchema.ItemClasses);
			base.RegisterProperty(ConversationSchema.GlobalItemClasses);
			base.RegisterProperty(ConversationSchema.Importance);
			base.RegisterProperty(ConversationSchema.GlobalImportance);
			base.RegisterProperty(ConversationSchema.ItemIds);
			base.RegisterProperty(ConversationSchema.GlobalItemIds);
			base.RegisterProperty(ConversationSchema.LastModifiedTime);
			base.RegisterProperty(ConversationSchema.InstanceKey);
			base.RegisterProperty(ConversationSchema.Preview);
			base.RegisterProperty(ConversationSchema.IconIndex);
			base.RegisterProperty(ConversationSchema.GlobalIconIndex);
			base.RegisterProperty(ConversationSchema.DraftItemIds);
			base.RegisterProperty(ConversationSchema.HasIrm);
			base.RegisterProperty(ConversationSchema.GlobalHasIrm);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0003905B File Offset: 0x0003805B
		internal ConversationSchema()
		{
		}

		// Token: 0x04000AFC RID: 2812
		public static readonly PropertyDefinition Id = new ComplexPropertyDefinition<ConversationId>("ConversationId", "conversation:ConversationId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new ConversationId());

		// Token: 0x04000AFD RID: 2813
		public static readonly PropertyDefinition Topic = new StringPropertyDefinition("ConversationTopic", "conversation:ConversationTopic", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000AFE RID: 2814
		public static readonly PropertyDefinition UniqueRecipients = new ComplexPropertyDefinition<StringList>("UniqueRecipients", "conversation:UniqueRecipients", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000AFF RID: 2815
		public static readonly PropertyDefinition GlobalUniqueRecipients = new ComplexPropertyDefinition<StringList>("GlobalUniqueRecipients", "conversation:GlobalUniqueRecipients", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B00 RID: 2816
		public static readonly PropertyDefinition UniqueUnreadSenders = new ComplexPropertyDefinition<StringList>("UniqueUnreadSenders", "conversation:UniqueUnreadSenders", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B01 RID: 2817
		public static readonly PropertyDefinition GlobalUniqueUnreadSenders = new ComplexPropertyDefinition<StringList>("GlobalUniqueUnreadSenders", "conversation:GlobalUniqueUnreadSenders", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B02 RID: 2818
		public static readonly PropertyDefinition UniqueSenders = new ComplexPropertyDefinition<StringList>("UniqueSenders", "conversation:UniqueSenders", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B03 RID: 2819
		public static readonly PropertyDefinition GlobalUniqueSenders = new ComplexPropertyDefinition<StringList>("GlobalUniqueSenders", "conversation:GlobalUniqueSenders", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B04 RID: 2820
		public static readonly PropertyDefinition LastDeliveryTime = new DateTimePropertyDefinition("LastDeliveryTime", "conversation:LastDeliveryTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B05 RID: 2821
		public static readonly PropertyDefinition GlobalLastDeliveryTime = new DateTimePropertyDefinition("GlobalLastDeliveryTime", "conversation:GlobalLastDeliveryTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B06 RID: 2822
		public static readonly PropertyDefinition Categories = new ComplexPropertyDefinition<StringList>("Categories", "conversation:Categories", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B07 RID: 2823
		public static readonly PropertyDefinition GlobalCategories = new ComplexPropertyDefinition<StringList>("GlobalCategories", "conversation:GlobalCategories", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList());

		// Token: 0x04000B08 RID: 2824
		public static readonly PropertyDefinition FlagStatus = new GenericPropertyDefinition<ConversationFlagStatus>("FlagStatus", "conversation:FlagStatus", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B09 RID: 2825
		public static readonly PropertyDefinition GlobalFlagStatus = new GenericPropertyDefinition<ConversationFlagStatus>("GlobalFlagStatus", "conversation:GlobalFlagStatus", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B0A RID: 2826
		public static readonly PropertyDefinition HasAttachments = new BoolPropertyDefinition("HasAttachments", "conversation:HasAttachments", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B0B RID: 2827
		public static readonly PropertyDefinition GlobalHasAttachments = new BoolPropertyDefinition("GlobalHasAttachments", "conversation:GlobalHasAttachments", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B0C RID: 2828
		public static readonly PropertyDefinition MessageCount = new IntPropertyDefinition("MessageCount", "conversation:MessageCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B0D RID: 2829
		public static readonly PropertyDefinition GlobalMessageCount = new IntPropertyDefinition("GlobalMessageCount", "conversation:GlobalMessageCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B0E RID: 2830
		public static readonly PropertyDefinition UnreadCount = new IntPropertyDefinition("UnreadCount", "conversation:UnreadCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B0F RID: 2831
		public static readonly PropertyDefinition GlobalUnreadCount = new IntPropertyDefinition("GlobalUnreadCount", "conversation:GlobalUnreadCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B10 RID: 2832
		public static readonly PropertyDefinition Size = new IntPropertyDefinition("Size", "conversation:Size", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B11 RID: 2833
		public static readonly PropertyDefinition GlobalSize = new IntPropertyDefinition("GlobalSize", "conversation:GlobalSize", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B12 RID: 2834
		public static readonly PropertyDefinition ItemClasses = new ComplexPropertyDefinition<StringList>("ItemClasses", "conversation:ItemClasses", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList("ItemClass"));

		// Token: 0x04000B13 RID: 2835
		public static readonly PropertyDefinition GlobalItemClasses = new ComplexPropertyDefinition<StringList>("GlobalItemClasses", "conversation:GlobalItemClasses", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new StringList("ItemClass"));

		// Token: 0x04000B14 RID: 2836
		public static readonly PropertyDefinition Importance = new GenericPropertyDefinition<Importance>("Importance", "conversation:Importance", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B15 RID: 2837
		public static readonly PropertyDefinition GlobalImportance = new GenericPropertyDefinition<Importance>("GlobalImportance", "conversation:GlobalImportance", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B16 RID: 2838
		public static readonly PropertyDefinition ItemIds = new ComplexPropertyDefinition<ItemIdCollection>("ItemIds", "conversation:ItemIds", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new ItemIdCollection());

		// Token: 0x04000B17 RID: 2839
		public static readonly PropertyDefinition GlobalItemIds = new ComplexPropertyDefinition<ItemIdCollection>("GlobalItemIds", "conversation:GlobalItemIds", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new ItemIdCollection());

		// Token: 0x04000B18 RID: 2840
		public static readonly PropertyDefinition LastModifiedTime = new DateTimePropertyDefinition("LastModifiedTime", "conversation:LastModifiedTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B19 RID: 2841
		public static readonly PropertyDefinition InstanceKey = new ByteArrayPropertyDefinition("InstanceKey", "conversation:InstanceKey", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B1A RID: 2842
		public static readonly PropertyDefinition Preview = new StringPropertyDefinition("Preview", "conversation:Preview", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B1B RID: 2843
		public static readonly PropertyDefinition IconIndex = new GenericPropertyDefinition<IconIndex>("IconIndex", "conversation:IconIndex", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B1C RID: 2844
		public static readonly PropertyDefinition GlobalIconIndex = new GenericPropertyDefinition<IconIndex>("GlobalIconIndex", "conversation:GlobalIconIndex", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B1D RID: 2845
		public static readonly PropertyDefinition DraftItemIds = new ComplexPropertyDefinition<ItemIdCollection>("DraftItemIds", "conversation:DraftItemIds", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new ItemIdCollection());

		// Token: 0x04000B1E RID: 2846
		public static readonly PropertyDefinition HasIrm = new BoolPropertyDefinition("HasIrm", "conversation:HasIrm", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B1F RID: 2847
		public static readonly PropertyDefinition GlobalHasIrm = new BoolPropertyDefinition("GlobalHasIrm", "conversation:GlobalHasIrm", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000B20 RID: 2848
		internal static readonly ConversationSchema Instance = new ConversationSchema();

		// Token: 0x020001B4 RID: 436
		private static class FieldUris
		{
			// Token: 0x04000B2F RID: 2863
			public const string ConversationId = "conversation:ConversationId";

			// Token: 0x04000B30 RID: 2864
			public const string ConversationTopic = "conversation:ConversationTopic";

			// Token: 0x04000B31 RID: 2865
			public const string UniqueRecipients = "conversation:UniqueRecipients";

			// Token: 0x04000B32 RID: 2866
			public const string GlobalUniqueRecipients = "conversation:GlobalUniqueRecipients";

			// Token: 0x04000B33 RID: 2867
			public const string UniqueUnreadSenders = "conversation:UniqueUnreadSenders";

			// Token: 0x04000B34 RID: 2868
			public const string GlobalUniqueUnreadSenders = "conversation:GlobalUniqueUnreadSenders";

			// Token: 0x04000B35 RID: 2869
			public const string UniqueSenders = "conversation:UniqueSenders";

			// Token: 0x04000B36 RID: 2870
			public const string GlobalUniqueSenders = "conversation:GlobalUniqueSenders";

			// Token: 0x04000B37 RID: 2871
			public const string LastDeliveryTime = "conversation:LastDeliveryTime";

			// Token: 0x04000B38 RID: 2872
			public const string GlobalLastDeliveryTime = "conversation:GlobalLastDeliveryTime";

			// Token: 0x04000B39 RID: 2873
			public const string Categories = "conversation:Categories";

			// Token: 0x04000B3A RID: 2874
			public const string GlobalCategories = "conversation:GlobalCategories";

			// Token: 0x04000B3B RID: 2875
			public const string FlagStatus = "conversation:FlagStatus";

			// Token: 0x04000B3C RID: 2876
			public const string GlobalFlagStatus = "conversation:GlobalFlagStatus";

			// Token: 0x04000B3D RID: 2877
			public const string HasAttachments = "conversation:HasAttachments";

			// Token: 0x04000B3E RID: 2878
			public const string GlobalHasAttachments = "conversation:GlobalHasAttachments";

			// Token: 0x04000B3F RID: 2879
			public const string MessageCount = "conversation:MessageCount";

			// Token: 0x04000B40 RID: 2880
			public const string GlobalMessageCount = "conversation:GlobalMessageCount";

			// Token: 0x04000B41 RID: 2881
			public const string UnreadCount = "conversation:UnreadCount";

			// Token: 0x04000B42 RID: 2882
			public const string GlobalUnreadCount = "conversation:GlobalUnreadCount";

			// Token: 0x04000B43 RID: 2883
			public const string Size = "conversation:Size";

			// Token: 0x04000B44 RID: 2884
			public const string GlobalSize = "conversation:GlobalSize";

			// Token: 0x04000B45 RID: 2885
			public const string ItemClasses = "conversation:ItemClasses";

			// Token: 0x04000B46 RID: 2886
			public const string GlobalItemClasses = "conversation:GlobalItemClasses";

			// Token: 0x04000B47 RID: 2887
			public const string Importance = "conversation:Importance";

			// Token: 0x04000B48 RID: 2888
			public const string GlobalImportance = "conversation:GlobalImportance";

			// Token: 0x04000B49 RID: 2889
			public const string ItemIds = "conversation:ItemIds";

			// Token: 0x04000B4A RID: 2890
			public const string GlobalItemIds = "conversation:GlobalItemIds";

			// Token: 0x04000B4B RID: 2891
			public const string LastModifiedTime = "conversation:LastModifiedTime";

			// Token: 0x04000B4C RID: 2892
			public const string InstanceKey = "conversation:InstanceKey";

			// Token: 0x04000B4D RID: 2893
			public const string Preview = "conversation:Preview";

			// Token: 0x04000B4E RID: 2894
			public const string IconIndex = "conversation:IconIndex";

			// Token: 0x04000B4F RID: 2895
			public const string GlobalIconIndex = "conversation:GlobalIconIndex";

			// Token: 0x04000B50 RID: 2896
			public const string DraftItemIds = "conversation:DraftItemIds";

			// Token: 0x04000B51 RID: 2897
			public const string HasIrm = "conversation:HasIrm";

			// Token: 0x04000B52 RID: 2898
			public const string GlobalHasIrm = "conversation:GlobalHasIrm";
		}
	}
}
