using System;
using System.Collections.Generic;
using Microsoft.OData.Client.Materialization;

namespace Microsoft.OData.Client
{
	// Token: 0x02000046 RID: 70
	public class DataServiceClientResponsePipelineConfiguration
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00008CB0 File Offset: 0x00006EB0
		internal DataServiceClientResponsePipelineConfiguration(object sender)
		{
			this.sender = sender;
			this.readingEndResourceActions = new List<Action<ReadingEntryArgs>>();
			this.readingEndFeedActions = new List<Action<ReadingFeedArgs>>();
			this.readingEndNestedResourceInfoActions = new List<Action<ReadingNestedResourceInfoArgs>>();
			this.readingStartResourceActions = new List<Action<ReadingEntryArgs>>();
			this.readingStartFeedActions = new List<Action<ReadingFeedArgs>>();
			this.readingStartNestedResourceInfoActions = new List<Action<ReadingNestedResourceInfoArgs>>();
			this.materializedEntityActions = new List<Action<MaterializedEntityArgs>>();
			this.messageReaderSettingsConfigurationActions = new List<Action<MessageReaderSettingsArgs>>();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008D24 File Offset: 0x00006F24
		internal bool HasConfigurations
		{
			get
			{
				return this.readingStartResourceActions.Count > 0 || this.readingEndResourceActions.Count > 0 || this.readingStartFeedActions.Count > 0 || this.readingEndFeedActions.Count > 0 || this.readingStartNestedResourceInfoActions.Count > 0 || this.readingEndNestedResourceInfoActions.Count > 0;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008D87 File Offset: 0x00006F87
		internal bool HasReadingEntityHandlers
		{
			get
			{
				return this.materializedEntityActions.Count > 0;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008D9A File Offset: 0x00006F9A
		public DataServiceClientResponsePipelineConfiguration OnMessageReaderSettingsCreated(Action<MessageReaderSettingsArgs> messageReaderSettingsAction)
		{
			WebUtil.CheckArgumentNull<Action<MessageReaderSettingsArgs>>(messageReaderSettingsAction, "messageReaderSettingsAction");
			this.messageReaderSettingsConfigurationActions.Add(messageReaderSettingsAction);
			return this;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008DB5 File Offset: 0x00006FB5
		public DataServiceClientResponsePipelineConfiguration OnEntryStarted(Action<ReadingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingEntryArgs>>(action, "action");
			this.readingStartResourceActions.Add(action);
			return this;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008DD0 File Offset: 0x00006FD0
		public DataServiceClientResponsePipelineConfiguration OnEntryEnded(Action<ReadingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingEntryArgs>>(action, "action");
			this.readingEndResourceActions.Add(action);
			return this;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008DEB File Offset: 0x00006FEB
		public DataServiceClientResponsePipelineConfiguration OnFeedStarted(Action<ReadingFeedArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingFeedArgs>>(action, "action");
			this.readingStartFeedActions.Add(action);
			return this;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008E06 File Offset: 0x00007006
		public DataServiceClientResponsePipelineConfiguration OnFeedEnded(Action<ReadingFeedArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingFeedArgs>>(action, "action");
			this.readingEndFeedActions.Add(action);
			return this;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008E21 File Offset: 0x00007021
		public DataServiceClientResponsePipelineConfiguration OnNestedResourceInfoStarted(Action<ReadingNestedResourceInfoArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingNestedResourceInfoArgs>>(action, "action");
			this.readingStartNestedResourceInfoActions.Add(action);
			return this;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008E3C File Offset: 0x0000703C
		public DataServiceClientResponsePipelineConfiguration OnNestedResourceInfoEnded(Action<ReadingNestedResourceInfoArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingNestedResourceInfoArgs>>(action, "action");
			this.readingEndNestedResourceInfoActions.Add(action);
			return this;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008E57 File Offset: 0x00007057
		public DataServiceClientResponsePipelineConfiguration OnEntityMaterialized(Action<MaterializedEntityArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<MaterializedEntityArgs>>(action, "action");
			this.materializedEntityActions.Add(action);
			return this;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008E74 File Offset: 0x00007074
		internal void ExecuteReaderSettingsConfiguration(ODataMessageReaderSettings readerSettings)
		{
			if (this.messageReaderSettingsConfigurationActions.Count > 0)
			{
				MessageReaderSettingsArgs messageReaderSettingsArgs = new MessageReaderSettingsArgs(readerSettings);
				foreach (Action<MessageReaderSettingsArgs> action in this.messageReaderSettingsConfigurationActions)
				{
					action(messageReaderSettingsArgs);
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008EDC File Offset: 0x000070DC
		internal void ExecuteOnEntryEndActions(ODataResource entry)
		{
			if (this.readingEndResourceActions.Count > 0)
			{
				ReadingEntryArgs readingEntryArgs = new ReadingEntryArgs(entry);
				foreach (Action<ReadingEntryArgs> action in this.readingEndResourceActions)
				{
					action(readingEntryArgs);
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008F44 File Offset: 0x00007144
		internal void ExecuteOnEntryStartActions(ODataResource entry)
		{
			if (this.readingStartResourceActions.Count > 0)
			{
				ReadingEntryArgs readingEntryArgs = new ReadingEntryArgs(entry);
				foreach (Action<ReadingEntryArgs> action in this.readingStartResourceActions)
				{
					action(readingEntryArgs);
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008FAC File Offset: 0x000071AC
		internal void ExecuteOnFeedEndActions(ODataResourceSet feed)
		{
			if (this.readingEndFeedActions.Count > 0)
			{
				ReadingFeedArgs readingFeedArgs = new ReadingFeedArgs(feed);
				foreach (Action<ReadingFeedArgs> action in this.readingEndFeedActions)
				{
					action(readingFeedArgs);
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009014 File Offset: 0x00007214
		internal void ExecuteOnFeedStartActions(ODataResourceSet feed)
		{
			if (this.readingStartFeedActions.Count > 0)
			{
				ReadingFeedArgs readingFeedArgs = new ReadingFeedArgs(feed);
				foreach (Action<ReadingFeedArgs> action in this.readingStartFeedActions)
				{
					action(readingFeedArgs);
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000907C File Offset: 0x0000727C
		internal void ExecuteOnNavigationEndActions(ODataNestedResourceInfo link)
		{
			if (this.readingEndNestedResourceInfoActions.Count > 0)
			{
				ReadingNestedResourceInfoArgs readingNestedResourceInfoArgs = new ReadingNestedResourceInfoArgs(link);
				foreach (Action<ReadingNestedResourceInfoArgs> action in this.readingEndNestedResourceInfoActions)
				{
					action(readingNestedResourceInfoArgs);
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000090E4 File Offset: 0x000072E4
		internal void ExecuteOnNavigationStartActions(ODataNestedResourceInfo link)
		{
			if (this.readingStartNestedResourceInfoActions.Count > 0)
			{
				ReadingNestedResourceInfoArgs readingNestedResourceInfoArgs = new ReadingNestedResourceInfoArgs(link);
				foreach (Action<ReadingNestedResourceInfoArgs> action in this.readingStartNestedResourceInfoActions)
				{
					action(readingNestedResourceInfoArgs);
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000914C File Offset: 0x0000734C
		internal void ExecuteEntityMaterializedActions(ODataResource entry, object entity)
		{
			if (this.materializedEntityActions.Count > 0)
			{
				MaterializedEntityArgs materializedEntityArgs = new MaterializedEntityArgs(entry, entity);
				foreach (Action<MaterializedEntityArgs> action in this.materializedEntityActions)
				{
					action(materializedEntityArgs);
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000091B8 File Offset: 0x000073B8
		internal void FireEndEntryEvents(MaterializerEntry entry)
		{
			if (this.HasReadingEntityHandlers)
			{
				this.ExecuteEntityMaterializedActions(entry.Entry, entry.ResolvedObject);
			}
		}

		// Token: 0x040000B0 RID: 176
		private readonly List<Action<ReadingEntryArgs>> readingStartResourceActions;

		// Token: 0x040000B1 RID: 177
		private readonly List<Action<ReadingEntryArgs>> readingEndResourceActions;

		// Token: 0x040000B2 RID: 178
		private readonly List<Action<ReadingFeedArgs>> readingStartFeedActions;

		// Token: 0x040000B3 RID: 179
		private readonly List<Action<ReadingFeedArgs>> readingEndFeedActions;

		// Token: 0x040000B4 RID: 180
		private readonly List<Action<ReadingNestedResourceInfoArgs>> readingStartNestedResourceInfoActions;

		// Token: 0x040000B5 RID: 181
		private readonly List<Action<ReadingNestedResourceInfoArgs>> readingEndNestedResourceInfoActions;

		// Token: 0x040000B6 RID: 182
		private readonly List<Action<MaterializedEntityArgs>> materializedEntityActions;

		// Token: 0x040000B7 RID: 183
		private readonly List<Action<MessageReaderSettingsArgs>> messageReaderSettingsConfigurationActions;

		// Token: 0x040000B8 RID: 184
		private readonly object sender;
	}
}
