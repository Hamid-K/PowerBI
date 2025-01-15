using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001CB RID: 459
	[Schema]
	public class TaskSchema : ItemSchema
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x0003B358 File Offset: 0x0003A358
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(TaskSchema.ActualWork);
			base.RegisterProperty(TaskSchema.AssignedTime);
			base.RegisterProperty(TaskSchema.BillingInformation);
			base.RegisterProperty(TaskSchema.ChangeCount);
			base.RegisterProperty(TaskSchema.Companies);
			base.RegisterProperty(TaskSchema.CompleteDate);
			base.RegisterProperty(TaskSchema.Contacts);
			base.RegisterProperty(TaskSchema.DelegationState);
			base.RegisterProperty(TaskSchema.Delegator);
			base.RegisterProperty(TaskSchema.DueDate);
			base.RegisterProperty(TaskSchema.Mode);
			base.RegisterProperty(TaskSchema.IsComplete);
			base.RegisterProperty(TaskSchema.IsRecurring);
			base.RegisterProperty(TaskSchema.IsTeamTask);
			base.RegisterProperty(TaskSchema.Mileage);
			base.RegisterProperty(TaskSchema.Owner);
			base.RegisterProperty(TaskSchema.PercentComplete);
			base.RegisterProperty(TaskSchema.Recurrence);
			base.RegisterProperty(TaskSchema.StartDate);
			base.RegisterProperty(TaskSchema.Status);
			base.RegisterProperty(TaskSchema.StatusDescription);
			base.RegisterProperty(TaskSchema.TotalWork);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0003B45D File Offset: 0x0003A45D
		internal TaskSchema()
		{
		}

		// Token: 0x04000CA2 RID: 3234
		public static readonly PropertyDefinition ActualWork = new IntPropertyDefinition("ActualWork", "task:ActualWork", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000CA3 RID: 3235
		public static readonly PropertyDefinition AssignedTime = new DateTimePropertyDefinition("AssignedTime", "task:AssignedTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000CA4 RID: 3236
		public static readonly PropertyDefinition BillingInformation = new StringPropertyDefinition("BillingInformation", "task:BillingInformation", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CA5 RID: 3237
		public static readonly PropertyDefinition ChangeCount = new IntPropertyDefinition("ChangeCount", "task:ChangeCount", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CA6 RID: 3238
		public static readonly PropertyDefinition Companies = new ComplexPropertyDefinition<StringList>("Companies", "task:Companies", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new StringList());

		// Token: 0x04000CA7 RID: 3239
		public static readonly PropertyDefinition CompleteDate = new DateTimePropertyDefinition("CompleteDate", "task:CompleteDate", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000CA8 RID: 3240
		public static readonly PropertyDefinition Contacts = new ComplexPropertyDefinition<StringList>("Contacts", "task:Contacts", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new StringList());

		// Token: 0x04000CA9 RID: 3241
		public static readonly PropertyDefinition DelegationState = new TaskDelegationStatePropertyDefinition("DelegationState", "task:DelegationState", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CAA RID: 3242
		public static readonly PropertyDefinition Delegator = new StringPropertyDefinition("Delegator", "task:Delegator", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CAB RID: 3243
		public static readonly PropertyDefinition DueDate = new DateTimePropertyDefinition("DueDate", "task:DueDate", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000CAC RID: 3244
		public static readonly PropertyDefinition Mode = new GenericPropertyDefinition<TaskMode>("IsAssignmentEditable", "task:IsAssignmentEditable", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CAD RID: 3245
		public static readonly PropertyDefinition IsComplete = new BoolPropertyDefinition("IsComplete", "task:IsComplete", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CAE RID: 3246
		public static readonly PropertyDefinition IsRecurring = new BoolPropertyDefinition("IsRecurring", "task:IsRecurring", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CAF RID: 3247
		public static readonly PropertyDefinition IsTeamTask = new BoolPropertyDefinition("IsTeamTask", "task:IsTeamTask", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB0 RID: 3248
		public static readonly PropertyDefinition Mileage = new StringPropertyDefinition("Mileage", "task:Mileage", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB1 RID: 3249
		public static readonly PropertyDefinition Owner = new StringPropertyDefinition("Owner", "task:Owner", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB2 RID: 3250
		public static readonly PropertyDefinition PercentComplete = new DoublePropertyDefinition("PercentComplete", "task:PercentComplete", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB3 RID: 3251
		public static readonly PropertyDefinition Recurrence = new RecurrencePropertyDefinition("Recurrence", "task:Recurrence", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB4 RID: 3252
		public static readonly PropertyDefinition StartDate = new DateTimePropertyDefinition("StartDate", "task:StartDate", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000CB5 RID: 3253
		public static readonly PropertyDefinition Status = new GenericPropertyDefinition<TaskStatus>("Status", "task:Status", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB6 RID: 3254
		public static readonly PropertyDefinition StatusDescription = new StringPropertyDefinition("StatusDescription", "task:StatusDescription", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000CB7 RID: 3255
		public static readonly PropertyDefinition TotalWork = new IntPropertyDefinition("TotalWork", "task:TotalWork", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000CB8 RID: 3256
		internal new static readonly TaskSchema Instance = new TaskSchema();

		// Token: 0x020001CC RID: 460
		private static class FieldUris
		{
			// Token: 0x04000CBB RID: 3259
			public const string ActualWork = "task:ActualWork";

			// Token: 0x04000CBC RID: 3260
			public const string AssignedTime = "task:AssignedTime";

			// Token: 0x04000CBD RID: 3261
			public const string BillingInformation = "task:BillingInformation";

			// Token: 0x04000CBE RID: 3262
			public const string ChangeCount = "task:ChangeCount";

			// Token: 0x04000CBF RID: 3263
			public const string Companies = "task:Companies";

			// Token: 0x04000CC0 RID: 3264
			public const string CompleteDate = "task:CompleteDate";

			// Token: 0x04000CC1 RID: 3265
			public const string Contacts = "task:Contacts";

			// Token: 0x04000CC2 RID: 3266
			public const string DelegationState = "task:DelegationState";

			// Token: 0x04000CC3 RID: 3267
			public const string Delegator = "task:Delegator";

			// Token: 0x04000CC4 RID: 3268
			public const string DueDate = "task:DueDate";

			// Token: 0x04000CC5 RID: 3269
			public const string IsAssignmentEditable = "task:IsAssignmentEditable";

			// Token: 0x04000CC6 RID: 3270
			public const string IsComplete = "task:IsComplete";

			// Token: 0x04000CC7 RID: 3271
			public const string IsRecurring = "task:IsRecurring";

			// Token: 0x04000CC8 RID: 3272
			public const string IsTeamTask = "task:IsTeamTask";

			// Token: 0x04000CC9 RID: 3273
			public const string Mileage = "task:Mileage";

			// Token: 0x04000CCA RID: 3274
			public const string Owner = "task:Owner";

			// Token: 0x04000CCB RID: 3275
			public const string PercentComplete = "task:PercentComplete";

			// Token: 0x04000CCC RID: 3276
			public const string Recurrence = "task:Recurrence";

			// Token: 0x04000CCD RID: 3277
			public const string StartDate = "task:StartDate";

			// Token: 0x04000CCE RID: 3278
			public const string Status = "task:Status";

			// Token: 0x04000CCF RID: 3279
			public const string StatusDescription = "task:StatusDescription";

			// Token: 0x04000CD0 RID: 3280
			public const string TotalWork = "task:TotalWork";
		}
	}
}
