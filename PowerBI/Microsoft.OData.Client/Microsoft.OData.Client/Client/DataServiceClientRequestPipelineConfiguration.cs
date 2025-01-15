using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000041 RID: 65
	public class DataServiceClientRequestPipelineConfiguration
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000887C File Offset: 0x00006A7C
		internal DataServiceClientRequestPipelineConfiguration()
		{
			this.writeEntityReferenceLinkActions = new List<Action<WritingEntityReferenceLinkArgs>>();
			this.writingEndResourceActions = new List<Action<WritingEntryArgs>>();
			this.writingEndNestedResourceInfoActions = new List<Action<WritingNestedResourceInfoArgs>>();
			this.writingStartResourceActions = new List<Action<WritingEntryArgs>>();
			this.writingStartNestedResourceInfoActions = new List<Action<WritingNestedResourceInfoArgs>>();
			this.messageWriterSettingsConfigurationActions = new List<Action<MessageWriterSettingsArgs>>();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000088D1 File Offset: 0x00006AD1
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000088D9 File Offset: 0x00006AD9
		public Func<DataServiceClientRequestMessageArgs, DataServiceClientRequestMessage> OnMessageCreating
		{
			get
			{
				return this.onmessageCreating;
			}
			set
			{
				if (this.ContextUsingSendingRequest)
				{
					throw new DataServiceClientException(Strings.Context_SendingRequest_InvalidWhenUsingOnMessageCreating);
				}
				this.onmessageCreating = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000088F5 File Offset: 0x00006AF5
		internal bool HasOnMessageCreating
		{
			get
			{
				return this.OnMessageCreating != null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008900 File Offset: 0x00006B00
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00008908 File Offset: 0x00006B08
		internal bool ContextUsingSendingRequest { get; set; }

		// Token: 0x060001F7 RID: 503 RVA: 0x00008911 File Offset: 0x00006B11
		public DataServiceClientRequestPipelineConfiguration OnMessageWriterSettingsCreated(Action<MessageWriterSettingsArgs> args)
		{
			WebUtil.CheckArgumentNull<Action<MessageWriterSettingsArgs>>(args, "args");
			this.messageWriterSettingsConfigurationActions.Add(args);
			return this;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000892C File Offset: 0x00006B2C
		public DataServiceClientRequestPipelineConfiguration OnEntryStarting(Action<WritingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingEntryArgs>>(action, "action");
			this.writingStartResourceActions.Add(action);
			return this;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008947 File Offset: 0x00006B47
		public DataServiceClientRequestPipelineConfiguration OnEntryEnding(Action<WritingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingEntryArgs>>(action, "action");
			this.writingEndResourceActions.Add(action);
			return this;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008962 File Offset: 0x00006B62
		public DataServiceClientRequestPipelineConfiguration OnEntityReferenceLink(Action<WritingEntityReferenceLinkArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingEntityReferenceLinkArgs>>(action, "action");
			this.writeEntityReferenceLinkActions.Add(action);
			return this;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000897D File Offset: 0x00006B7D
		public DataServiceClientRequestPipelineConfiguration OnNestedResourceInfoStarting(Action<WritingNestedResourceInfoArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingNestedResourceInfoArgs>>(action, "action");
			this.writingStartNestedResourceInfoActions.Add(action);
			return this;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008998 File Offset: 0x00006B98
		public DataServiceClientRequestPipelineConfiguration OnNestedResourceInfoEnding(Action<WritingNestedResourceInfoArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingNestedResourceInfoArgs>>(action, "action");
			this.writingEndNestedResourceInfoActions.Add(action);
			return this;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000089B4 File Offset: 0x00006BB4
		internal void ExecuteWriterSettingsConfiguration(ODataMessageWriterSettings writerSettings)
		{
			if (this.messageWriterSettingsConfigurationActions.Count > 0)
			{
				MessageWriterSettingsArgs messageWriterSettingsArgs = new MessageWriterSettingsArgs(writerSettings);
				foreach (Action<MessageWriterSettingsArgs> action in this.messageWriterSettingsConfigurationActions)
				{
					action(messageWriterSettingsArgs);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008A1C File Offset: 0x00006C1C
		internal void ExecuteOnEntryEndActions(ODataResource entry, object entity)
		{
			if (this.writingEndResourceActions.Count > 0)
			{
				WritingEntryArgs writingEntryArgs = new WritingEntryArgs(entry, entity);
				foreach (Action<WritingEntryArgs> action in this.writingEndResourceActions)
				{
					action(writingEntryArgs);
				}
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008A88 File Offset: 0x00006C88
		internal void ExecuteOnEntryStartActions(ODataResource entry, object entity)
		{
			if (this.writingStartResourceActions.Count > 0)
			{
				WritingEntryArgs writingEntryArgs = new WritingEntryArgs(entry, entity);
				foreach (Action<WritingEntryArgs> action in this.writingStartResourceActions)
				{
					action(writingEntryArgs);
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008AF4 File Offset: 0x00006CF4
		internal void ExecuteOnNestedResourceInfoEndActions(ODataNestedResourceInfo link, object source, object target)
		{
			if (this.writingEndNestedResourceInfoActions.Count > 0)
			{
				WritingNestedResourceInfoArgs writingNestedResourceInfoArgs = new WritingNestedResourceInfoArgs(link, source, target);
				foreach (Action<WritingNestedResourceInfoArgs> action in this.writingEndNestedResourceInfoActions)
				{
					action(writingNestedResourceInfoArgs);
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008B60 File Offset: 0x00006D60
		internal void ExecuteOnNestedResourceInfoStartActions(ODataNestedResourceInfo link, object source, object target)
		{
			if (this.writingStartNestedResourceInfoActions.Count > 0)
			{
				WritingNestedResourceInfoArgs writingNestedResourceInfoArgs = new WritingNestedResourceInfoArgs(link, source, target);
				foreach (Action<WritingNestedResourceInfoArgs> action in this.writingStartNestedResourceInfoActions)
				{
					action(writingNestedResourceInfoArgs);
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008BCC File Offset: 0x00006DCC
		internal void ExecuteEntityReferenceLinkActions(ODataEntityReferenceLink entityReferenceLink, object source, object target)
		{
			if (this.writeEntityReferenceLinkActions.Count > 0)
			{
				WritingEntityReferenceLinkArgs writingEntityReferenceLinkArgs = new WritingEntityReferenceLinkArgs(entityReferenceLink, source, target);
				foreach (Action<WritingEntityReferenceLinkArgs> action in this.writeEntityReferenceLinkActions)
				{
					action(writingEntityReferenceLinkArgs);
				}
			}
		}

		// Token: 0x040000A5 RID: 165
		private readonly List<Action<WritingEntryArgs>> writingStartResourceActions;

		// Token: 0x040000A6 RID: 166
		private readonly List<Action<WritingEntryArgs>> writingEndResourceActions;

		// Token: 0x040000A7 RID: 167
		private readonly List<Action<WritingEntityReferenceLinkArgs>> writeEntityReferenceLinkActions;

		// Token: 0x040000A8 RID: 168
		private readonly List<Action<WritingNestedResourceInfoArgs>> writingStartNestedResourceInfoActions;

		// Token: 0x040000A9 RID: 169
		private readonly List<Action<WritingNestedResourceInfoArgs>> writingEndNestedResourceInfoActions;

		// Token: 0x040000AA RID: 170
		private readonly List<Action<MessageWriterSettingsArgs>> messageWriterSettingsConfigurationActions;

		// Token: 0x040000AB RID: 171
		private Func<DataServiceClientRequestMessageArgs, DataServiceClientRequestMessage> onmessageCreating;
	}
}
