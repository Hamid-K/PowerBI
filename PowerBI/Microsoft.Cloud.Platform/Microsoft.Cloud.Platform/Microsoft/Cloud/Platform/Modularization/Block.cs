using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A7 RID: 167
	public abstract class Block : IBlock, IIdentifiable, IShuttable
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x0000E568 File Offset: 0x0000C768
		protected virtual BlockInitializationStatus OnInitialize()
		{
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnStart()
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnStop()
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnWaitForStopToComplete()
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnShutdown()
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnBlockServiceDependencySatisfied(Type type, object instance)
		{
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00011526 File Offset: 0x0000F726
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00011530 File Offset: 0x0000F730
		public BlockInitializationStatus Initialize([NotNull] IBlockHost blockHost)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} Initialize() invoked", new object[] { this.Name });
			ExtendedDiagnostics.EnsureArgumentNotNull<IBlockHost>(blockHost, "blockHost");
			BlockInitializationStatus blockInitializationStatus = this.m_stateMachine.Initialize(delegate
			{
				this.m_host = blockHost;
				if (!this.m_firstInitializeActionsDone)
				{
					this.m_firstInitializeActionsDone = true;
					this.m_workTicketManager = new WorkTicketManager(this.m_name);
					this.GatherDeclarativeBlockServiceDependencies();
					this.GatherDeclarativeBlockServiceProvidersByFields();
					this.PublishDeclarativeBlockServiceProviders(BlockServicePublish.PreBlockInitialization);
				}
				BlockInitializationStatus blockInitializationStatus2 = this.SatisfyDeclarativeBlockServiceDependencies();
				if (blockInitializationStatus2 == BlockInitializationStatus.PartiallyDone)
				{
					return blockInitializationStatus2;
				}
				this.PublishDeclarativeBlockServiceProviders(BlockServicePublish.ByDemand);
				blockInitializationStatus2 = this.OnInitialize();
				this.PublishDeclarativeBlockServiceProviders(BlockServicePublish.ByDemand);
				if (blockInitializationStatus2 == BlockInitializationStatus.Done)
				{
					this.PublishDeclarativeBlockServiceProviders(BlockServicePublish.PostBlockInitialization);
				}
				return blockInitializationStatus2;
			}, null);
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} Initialize returned {1}", new object[] { this.Name, blockInitializationStatus });
			return blockInitializationStatus;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000115C2 File Offset: 0x0000F7C2
		public void Start()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} Start() invoked", new object[] { this.Name });
			this.m_stateMachine.Start(delegate
			{
				this.OnStart();
			}, null, true);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000115FC File Offset: 0x0000F7FC
		public void Stop()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} Stop() invoked", new object[] { this.Name });
			this.m_stateMachine.Stop(delegate
			{
				this.m_workTicketManager.Stop();
				foreach (FieldAttributePair<AutoShuttableAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<AutoShuttableAttribute>(this))
				{
					IShuttable shuttable = (IShuttable)fieldAttributePair.Field.GetValue(this);
					if (shuttable != null)
					{
						shuttable.Stop();
					}
				}
				this.OnStop();
			}, null, true);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00011636 File Offset: 0x0000F836
		public void WaitForStopToComplete()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} WaitForStopToComplete() invoked", new object[] { this.Name });
			this.m_stateMachine.WaitForStopToComplete(delegate
			{
				this.m_workTicketManager.Stop();
				this.m_workTicketManager.WaitForStopToComplete();
				foreach (FieldAttributePair<AutoShuttableAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<AutoShuttableAttribute>(this))
				{
					IShuttable shuttable = (IShuttable)fieldAttributePair.Field.GetValue(this);
					if (shuttable != null)
					{
						shuttable.WaitForStopToComplete();
					}
				}
				this.OnWaitForStopToComplete();
			}, null, true);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00011670 File Offset: 0x0000F870
		public void Shutdown()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} Shutdown() invoked", new object[] { this.Name });
			this.m_stateMachine.Shutdown(delegate
			{
				this.m_workTicketManager.Shutdown();
				foreach (FieldAttributePair<AutoShuttableAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<AutoShuttableAttribute>(this))
				{
					IShuttable shuttable = (IShuttable)fieldAttributePair.Field.GetValue(this);
					if (shuttable != null)
					{
						shuttable.Shutdown();
					}
				}
				this.OnShutdown();
			}, null, true);
			this.Reset();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000116B0 File Offset: 0x0000F8B0
		protected Block([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			this.m_name = name;
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Block {0} created", new object[] { this.Name });
			Type typeFromHandle = typeof(IShuttable);
			foreach (FieldAttributePair<AutoShuttableAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<AutoShuttableAttribute>(this))
			{
				FieldInfo field = fieldAttributePair.Field;
				ExtendedDiagnostics.EnsureOperation(typeFromHandle.IsAssignableFrom(field.FieldType), string.Concat(new string[]
				{
					"Field ",
					base.GetType().Name,
					".",
					field.Name,
					" cannot be marked [AutoShuttable] as it does not implement IShuttable"
				}));
			}
			this.Reset();
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00011790 File Offset: 0x0000F990
		protected internal BlockState State
		{
			get
			{
				return this.m_stateMachine.State;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001179D File Offset: 0x0000F99D
		protected IBlockHost BlockHost
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000117A5 File Offset: 0x0000F9A5
		protected WorkTicketManager WorkTicketManager
		{
			get
			{
				return this.m_workTicketManager;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000117B0 File Offset: 0x0000F9B0
		protected T TryGetService<T>() where T : class
		{
			T t = default(T);
			BlockServiceTicket blockServiceTicket = this.BlockHost.TryGetService(new RequestedBlockService(this, typeof(T)));
			if (blockServiceTicket != null)
			{
				this.m_blockServiceDependencies.Add(new Block.BlockServiceDependencyRealization(blockServiceTicket));
				t = (T)((object)blockServiceTicket.GetService());
			}
			return t;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011804 File Offset: 0x0000FA04
		protected internal void DumpPendingTicketsAsFatal(int maxNumberOfTickets)
		{
			if (this.m_workTicketManager.TrackTickets)
			{
				TraceDump traceDump = new TraceDump();
				traceDump.Add("Following is a list of pending work tickets in block " + this.Name);
				int num = 0;
				foreach (WorkTicket workTicket in this.m_workTicketManager.EnumeratePendingTickets(maxNumberOfTickets))
				{
					num++;
					traceDump.Add("-- Work ticket " + workTicket.Id + " ---");
					workTicket.Dump(traceDump);
				}
				traceDump.Add("End of list of pending work tickets (" + num + " tickets)");
				using (IEnumerator<string> enumerator2 = traceDump.Lines.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string text = enumerator2.Current;
						TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
					}
					return;
				}
			}
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "(If you had enabled WorkTicketManager.TrackTickets, you would have got a dump of the pending ticket)");
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "(See 1. Microsoft.Cloud.Platform.Utils.WorkTicket.LeakDetectionEnabled)");
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "(See 2. Microsoft.Cloud.Platform.Utils.WorkTicket.TrackTickets)");
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "(See 3. Microsoft.Cloud.Platform.Utils.WorkTicket.CaptureTicketsCallStack)");
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001195C File Offset: 0x0000FB5C
		private void GatherDeclarativeBlockServiceProvidersByFields()
		{
			foreach (FieldAttributePair<BlockServiceProviderAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<BlockServiceProviderAttribute>(this))
			{
				FieldInfo field = fieldAttributePair.Field;
				BlockServiceProviderAttribute attribute = fieldAttributePair.Attribute;
				if (attribute.PublishWhen == BlockServicePublish.Default)
				{
					attribute.PublishWhen = BlockServicePublish.ByDemand;
				}
				else if (attribute.PublishWhen != BlockServicePublish.ByDemand)
				{
					string text = string.Concat(new object[] { "When a [BlockServiceProviderAttribute] is applied to a field it must have PublishWhen=BlockServicePublish.ByDemand (offender: ", field.DeclaringType, ".", field.Name, ".PublishWhen=", attribute.PublishWhen, ")" });
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
					throw new BadAttributeUsageException(text);
				}
				Block.BlockServiceProviderRealization blockServiceProviderRealization = new Block.BlockServiceProviderRealization
				{
					ServiceType = attribute.ServiceType,
					ServiceIdentity = attribute.ServiceIdentity,
					ServiceLevel = attribute.ServiceLevel,
					Field = field
				};
				this.m_blockServiceProviders.Add(blockServiceProviderRealization);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00011A7C File Offset: 0x0000FC7C
		private void PublishDeclarativeBlockServiceProviders(BlockServicePublish when)
		{
			if (when == BlockServicePublish.ByDemand)
			{
				this.PublishDeclarativeBlockServiceProvidersByDemand();
				return;
			}
			foreach (BlockServiceProviderAttribute blockServiceProviderAttribute in base.GetType().GetCustomAttributes(typeof(BlockServiceProviderAttribute), true))
			{
				if (blockServiceProviderAttribute.PublishWhen == when || (blockServiceProviderAttribute.PublishWhen == BlockServicePublish.Default && when == BlockServicePublish.PostBlockInitialization))
				{
					if (blockServiceProviderAttribute.ServiceType == null)
					{
						string text = "When a [BlockServiceProviderAttribute] is applied to a class it must have a non-null ServiceType (offender: " + base.GetType() + ")";
						TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
						throw new BadAttributeUsageException(text);
					}
					Block.BlockServiceProviderRealization blockServiceProviderRealization = new Block.BlockServiceProviderRealization
					{
						ServiceType = blockServiceProviderAttribute.ServiceType,
						ServiceIdentity = blockServiceProviderAttribute.ServiceIdentity,
						ServiceLevel = blockServiceProviderAttribute.ServiceLevel,
						Field = null
					};
					this.PublishDeclarativeBlockService(blockServiceProviderRealization, this);
				}
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00011B64 File Offset: 0x0000FD64
		private void PublishDeclarativeBlockServiceProvidersByDemand()
		{
			if (this.m_allDeclarativeBlockServiceProvidersSatisfied)
			{
				return;
			}
			this.m_allDeclarativeBlockServiceProvidersSatisfied = true;
			foreach (Block.BlockServiceProviderRealization blockServiceProviderRealization in this.m_blockServiceProviders)
			{
				if (!blockServiceProviderRealization.Published)
				{
					object value = blockServiceProviderRealization.Field.GetValue(this);
					if (value == null)
					{
						this.m_allDeclarativeBlockServiceProvidersSatisfied = false;
					}
					else
					{
						this.PublishDeclarativeBlockService(blockServiceProviderRealization, value);
						blockServiceProviderRealization.Published = true;
					}
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		private void PublishDeclarativeBlockService(Block.BlockServiceProviderRealization bsp, object service)
		{
			if (!this.BlockHost.PublishService(bsp.ServiceIdentity, service, bsp.ServiceType ?? bsp.Field.FieldType, bsp.ServiceLevel, this))
			{
				string text = base.GetType() + ": Failed to publish a block service (via BlockServiceProviderAttribute)";
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
				if (!Microsoft.Cloud.Platform.Modularization.BlockHost.s_TolerateRedundantAddBlockAndPublishService.Value)
				{
					throw new InvalidOperationException(text);
				}
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00011C6C File Offset: 0x0000FE6C
		private void GatherDeclarativeBlockServiceDependencies()
		{
			foreach (FieldAttributePair<BlockServiceDependencyAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<BlockServiceDependencyAttribute>(this))
			{
				FieldInfo field = fieldAttributePair.Field;
				if (field.GetValue(this) != null)
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Block.GatherDeclarativeBlockServiceDependencies: Skipping field '{0}.{1}' marked as [BlockServiceDependency] because it is already non-null", new object[]
					{
						base.GetType().FullName,
						field.Name
					});
				}
				else
				{
					BlockServiceDependencyAttribute attribute = fieldAttributePair.Attribute;
					Block.BlockServiceDependencyRealization blockServiceDependencyRealization = new Block.BlockServiceDependencyRealization();
					blockServiceDependencyRealization.ServiceType = attribute.ServiceType;
					if (blockServiceDependencyRealization.ServiceType == null)
					{
						blockServiceDependencyRealization.ServiceType = field.FieldType;
					}
					blockServiceDependencyRealization.Field = field;
					blockServiceDependencyRealization.ServiceIdentity = attribute.ServiceIdentity;
					this.m_blockServiceDependencies.Add(blockServiceDependencyRealization);
				}
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011D50 File Offset: 0x0000FF50
		private BlockInitializationStatus SatisfyDeclarativeBlockServiceDependencies()
		{
			if (this.m_allDeclarativeBlockServiceDependenciesSatisfied)
			{
				return BlockInitializationStatus.Done;
			}
			this.m_allDeclarativeBlockServiceDependenciesSatisfied = true;
			foreach (Block.BlockServiceDependencyRealization blockServiceDependencyRealization in this.m_blockServiceDependencies)
			{
				if (blockServiceDependencyRealization.Ticket == null)
				{
					blockServiceDependencyRealization.Ticket = this.BlockHost.TryGetService(new RequestedBlockService(blockServiceDependencyRealization.ServiceIdentity, this, blockServiceDependencyRealization.ServiceType, BlockServiceProviderIdentity.Default, null));
					if (blockServiceDependencyRealization.Ticket != null)
					{
						blockServiceDependencyRealization.Field.SetValue(this, blockServiceDependencyRealization.Ticket.GetService());
						this.OnBlockServiceDependencySatisfied(blockServiceDependencyRealization.ServiceType, blockServiceDependencyRealization.Ticket.GetService());
					}
					else
					{
						this.m_allDeclarativeBlockServiceDependenciesSatisfied = false;
					}
				}
			}
			if (!this.m_allDeclarativeBlockServiceDependenciesSatisfied)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00011E24 File Offset: 0x00010024
		private void Reset()
		{
			this.m_host = null;
			this.m_stateMachine = new BlockStateMachine();
			this.m_firstInitializeActionsDone = false;
			this.m_blockServiceProviders = new List<Block.BlockServiceProviderRealization>();
			this.m_allDeclarativeBlockServiceProvidersSatisfied = false;
			if (this.m_blockServiceDependencies != null)
			{
				foreach (Block.BlockServiceDependencyRealization blockServiceDependencyRealization in this.m_blockServiceDependencies)
				{
					blockServiceDependencyRealization.Ticket.Dispose();
				}
			}
			this.m_blockServiceDependencies = new List<Block.BlockServiceDependencyRealization>();
			this.m_allDeclarativeBlockServiceDependenciesSatisfied = false;
		}

		// Token: 0x0400019F RID: 415
		private string m_name;

		// Token: 0x040001A0 RID: 416
		private IBlockHost m_host;

		// Token: 0x040001A1 RID: 417
		private BlockStateMachine m_stateMachine;

		// Token: 0x040001A2 RID: 418
		private bool m_firstInitializeActionsDone;

		// Token: 0x040001A3 RID: 419
		private WorkTicketManager m_workTicketManager;

		// Token: 0x040001A4 RID: 420
		private List<Block.BlockServiceProviderRealization> m_blockServiceProviders;

		// Token: 0x040001A5 RID: 421
		private bool m_allDeclarativeBlockServiceProvidersSatisfied;

		// Token: 0x040001A6 RID: 422
		private List<Block.BlockServiceDependencyRealization> m_blockServiceDependencies;

		// Token: 0x040001A7 RID: 423
		private bool m_allDeclarativeBlockServiceDependenciesSatisfied;

		// Token: 0x020005BA RID: 1466
		private class BlockServiceProviderRealization
		{
			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x06002B58 RID: 11096 RVA: 0x0009A5BD File Offset: 0x000987BD
			// (set) Token: 0x06002B59 RID: 11097 RVA: 0x0009A5C5 File Offset: 0x000987C5
			public bool Published { get; set; }

			// Token: 0x170006FA RID: 1786
			// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0009A5CE File Offset: 0x000987CE
			// (set) Token: 0x06002B5B RID: 11099 RVA: 0x0009A5D6 File Offset: 0x000987D6
			public Type ServiceType { get; set; }

			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0009A5DF File Offset: 0x000987DF
			// (set) Token: 0x06002B5D RID: 11101 RVA: 0x0009A5E7 File Offset: 0x000987E7
			public string ServiceIdentity { get; set; }

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x06002B5E RID: 11102 RVA: 0x0009A5F0 File Offset: 0x000987F0
			// (set) Token: 0x06002B5F RID: 11103 RVA: 0x0009A5F8 File Offset: 0x000987F8
			public BlockServiceProviderIdentity ServiceLevel { get; set; }

			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0009A601 File Offset: 0x00098801
			// (set) Token: 0x06002B61 RID: 11105 RVA: 0x0009A609 File Offset: 0x00098809
			public FieldInfo Field { get; set; }
		}

		// Token: 0x020005BB RID: 1467
		private class BlockServiceDependencyRealization
		{
			// Token: 0x06002B62 RID: 11106 RVA: 0x0000460D File Offset: 0x0000280D
			public BlockServiceDependencyRealization()
			{
			}

			// Token: 0x06002B63 RID: 11107 RVA: 0x0009A612 File Offset: 0x00098812
			public BlockServiceDependencyRealization(BlockServiceTicket ticket)
			{
				this.Ticket = ticket;
			}

			// Token: 0x170006FE RID: 1790
			// (get) Token: 0x06002B64 RID: 11108 RVA: 0x0009A621 File Offset: 0x00098821
			// (set) Token: 0x06002B65 RID: 11109 RVA: 0x0009A629 File Offset: 0x00098829
			public Type ServiceType { get; set; }

			// Token: 0x170006FF RID: 1791
			// (get) Token: 0x06002B66 RID: 11110 RVA: 0x0009A632 File Offset: 0x00098832
			// (set) Token: 0x06002B67 RID: 11111 RVA: 0x0009A63A File Offset: 0x0009883A
			public BlockServiceTicket Ticket { get; set; }

			// Token: 0x17000700 RID: 1792
			// (get) Token: 0x06002B68 RID: 11112 RVA: 0x0009A643 File Offset: 0x00098843
			// (set) Token: 0x06002B69 RID: 11113 RVA: 0x0009A64B File Offset: 0x0009884B
			public FieldInfo Field { get; set; }

			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x06002B6A RID: 11114 RVA: 0x0009A654 File Offset: 0x00098854
			// (set) Token: 0x06002B6B RID: 11115 RVA: 0x0009A65C File Offset: 0x0009885C
			public string ServiceIdentity { get; set; }
		}
	}
}
