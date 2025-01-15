using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A0 RID: 416
	[ServiceObjectDefinition("Task")]
	[Attachable]
	public class Task : Item
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x00036BC6 File Offset: 0x00035BC6
		public Task(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00036BCF File Offset: 0x00035BCF
		internal Task(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00036BD8 File Offset: 0x00035BD8
		public new static Task Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<Task>(id, propertySet);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00036BE2 File Offset: 0x00035BE2
		public new static Task Bind(ExchangeService service, ItemId id)
		{
			return Task.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00036BF0 File Offset: 0x00035BF0
		internal override ServiceObjectSchema GetSchema()
		{
			return TaskSchema.Instance;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00036BF7 File Offset: 0x00035BF7
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00036BFA File Offset: 0x00035BFA
		internal override bool GetIsTimeZoneHeaderRequired(bool isUpdateOperation)
		{
			return true;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00036C00 File Offset: 0x00035C00
		public void DeleteCurrentOccurrence(DeleteMode deleteMode)
		{
			this.InternalDelete(deleteMode, default(SendCancellationsMode?), new AffectedTaskOccurrence?(AffectedTaskOccurrence.SpecifiedOccurrenceOnly));
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00036C24 File Offset: 0x00035C24
		public new Task Update(ConflictResolutionMode conflictResolutionMode)
		{
			return (Task)base.InternalUpdate(null, conflictResolutionMode, new MessageDisposition?(MessageDisposition.SaveOnly), default(SendInvitationsOrCancellationsMode?));
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00036C4D File Offset: 0x00035C4D
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x00036C64 File Offset: 0x00035C64
		public int? ActualWork
		{
			get
			{
				return (int?)base.PropertyBag[TaskSchema.ActualWork];
			}
			set
			{
				base.PropertyBag[TaskSchema.ActualWork] = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00036C7C File Offset: 0x00035C7C
		public DateTime? AssignedTime
		{
			get
			{
				return (DateTime?)base.PropertyBag[TaskSchema.AssignedTime];
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00036C93 File Offset: 0x00035C93
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x00036CAA File Offset: 0x00035CAA
		public string BillingInformation
		{
			get
			{
				return (string)base.PropertyBag[TaskSchema.BillingInformation];
			}
			set
			{
				base.PropertyBag[TaskSchema.BillingInformation] = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00036CBD File Offset: 0x00035CBD
		public int ChangeCount
		{
			get
			{
				return (int)base.PropertyBag[TaskSchema.ChangeCount];
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00036CD4 File Offset: 0x00035CD4
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x00036CEB File Offset: 0x00035CEB
		public StringList Companies
		{
			get
			{
				return (StringList)base.PropertyBag[TaskSchema.Companies];
			}
			set
			{
				base.PropertyBag[TaskSchema.Companies] = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00036CFE File Offset: 0x00035CFE
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00036D15 File Offset: 0x00035D15
		public DateTime? CompleteDate
		{
			get
			{
				return (DateTime?)base.PropertyBag[TaskSchema.CompleteDate];
			}
			set
			{
				base.PropertyBag[TaskSchema.CompleteDate] = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00036D2D File Offset: 0x00035D2D
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x00036D44 File Offset: 0x00035D44
		public StringList Contacts
		{
			get
			{
				return (StringList)base.PropertyBag[TaskSchema.Contacts];
			}
			set
			{
				base.PropertyBag[TaskSchema.Contacts] = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00036D57 File Offset: 0x00035D57
		public TaskDelegationState DelegationState
		{
			get
			{
				return (TaskDelegationState)base.PropertyBag[TaskSchema.DelegationState];
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00036D6E File Offset: 0x00035D6E
		public string Delegator
		{
			get
			{
				return (string)base.PropertyBag[TaskSchema.Delegator];
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00036D85 File Offset: 0x00035D85
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x00036D9C File Offset: 0x00035D9C
		public DateTime? DueDate
		{
			get
			{
				return (DateTime?)base.PropertyBag[TaskSchema.DueDate];
			}
			set
			{
				base.PropertyBag[TaskSchema.DueDate] = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00036DB4 File Offset: 0x00035DB4
		public TaskMode Mode
		{
			get
			{
				return (TaskMode)base.PropertyBag[TaskSchema.Mode];
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00036DCB File Offset: 0x00035DCB
		public bool IsComplete
		{
			get
			{
				return (bool)base.PropertyBag[TaskSchema.IsComplete];
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00036DE2 File Offset: 0x00035DE2
		public bool IsRecurring
		{
			get
			{
				return (bool)base.PropertyBag[TaskSchema.IsRecurring];
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00036DF9 File Offset: 0x00035DF9
		public bool IsTeamTask
		{
			get
			{
				return (bool)base.PropertyBag[TaskSchema.IsTeamTask];
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00036E10 File Offset: 0x00035E10
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x00036E27 File Offset: 0x00035E27
		public string Mileage
		{
			get
			{
				return (string)base.PropertyBag[TaskSchema.Mileage];
			}
			set
			{
				base.PropertyBag[TaskSchema.Mileage] = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00036E3A File Offset: 0x00035E3A
		public string Owner
		{
			get
			{
				return (string)base.PropertyBag[TaskSchema.Owner];
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00036E51 File Offset: 0x00035E51
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00036E68 File Offset: 0x00035E68
		public double PercentComplete
		{
			get
			{
				return (double)base.PropertyBag[TaskSchema.PercentComplete];
			}
			set
			{
				base.PropertyBag[TaskSchema.PercentComplete] = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00036E80 File Offset: 0x00035E80
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00036E97 File Offset: 0x00035E97
		public Recurrence Recurrence
		{
			get
			{
				return (Recurrence)base.PropertyBag[TaskSchema.Recurrence];
			}
			set
			{
				base.PropertyBag[TaskSchema.Recurrence] = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00036EAA File Offset: 0x00035EAA
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00036EC1 File Offset: 0x00035EC1
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)base.PropertyBag[TaskSchema.StartDate];
			}
			set
			{
				base.PropertyBag[TaskSchema.StartDate] = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00036ED9 File Offset: 0x00035ED9
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x00036EF0 File Offset: 0x00035EF0
		public TaskStatus Status
		{
			get
			{
				return (TaskStatus)base.PropertyBag[TaskSchema.Status];
			}
			set
			{
				base.PropertyBag[TaskSchema.Status] = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00036F08 File Offset: 0x00035F08
		public string StatusDescription
		{
			get
			{
				return (string)base.PropertyBag[TaskSchema.StatusDescription];
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00036F1F File Offset: 0x00035F1F
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x00036F36 File Offset: 0x00035F36
		public int? TotalWork
		{
			get
			{
				return (int?)base.PropertyBag[TaskSchema.TotalWork];
			}
			set
			{
				base.PropertyBag[TaskSchema.TotalWork] = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00036F4E File Offset: 0x00035F4E
		internal override AffectedTaskOccurrence? DefaultAffectedTaskOccurrences
		{
			get
			{
				return new AffectedTaskOccurrence?(AffectedTaskOccurrence.AllOccurrences);
			}
		}
	}
}
