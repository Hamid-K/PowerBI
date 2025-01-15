using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000B2 RID: 178
	public abstract class BlockHost : IBlockHost, IBlockServiceManager
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x0001213F File Offset: 0x0001033F
		public void RequestShutdown(IBlock requestor)
		{
			this.RequestShutdown(requestor, 0);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001214C File Offset: 0x0001034C
		public void RequestShutdown(IBlock requestor, int returnCode)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RequestShutdown {0} with return code {1}", new object[]
			{
				(requestor == null) ? "" : (" (initiated by " + requestor.Name + ")"),
				returnCode
			});
			this.OnRequestShutdown(requestor, returnCode);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000121A2 File Offset: 0x000103A2
		public void AddBlock(IBlock block)
		{
			this.AddBlock(block, true);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000121AC File Offset: 0x000103AC
		public void AddBlocks(IEnumerable<IBlock> blocksToAdd)
		{
			foreach (IBlock block in blocksToAdd)
			{
				this.AddBlock(block);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000121F4 File Offset: 0x000103F4
		public virtual BlockServiceTicket GetService(RequestedBlockService request)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Services of Type {0} requested by {1}", new object[]
			{
				request.ServiceType.Name,
				(request.ServiceConsumer == null) ? "unidentified consumer" : request.ServiceConsumer.Name
			});
			this.EnsureCanCreateServices();
			BlockServiceTicket blockServiceTicket = this.m_serviceTicketManager.TryGetService(request);
			if (blockServiceTicket != null)
			{
				return blockServiceTicket;
			}
			throw new ServiceNotFoundException(request.ServiceType);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00012265 File Offset: 0x00010465
		public virtual BlockServiceTicket GetService(IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context)
		{
			return this.GetService(new RequestedBlockService(serviceConsumer, serviceType, serviceIdentity, context));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00012277 File Offset: 0x00010477
		[CanBeNull]
		public virtual BlockServiceTicket TryGetService(RequestedBlockService request)
		{
			if (!this.CanCreateServices())
			{
				return null;
			}
			return this.m_serviceTicketManager.TryGetService(request);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001228F File Offset: 0x0001048F
		public virtual bool PublishService(object service, Type serviceType, BlockServiceProviderIdentity serviceIdentity, IBlock serviceProvider)
		{
			return this.PublishService(null, service, serviceType, serviceIdentity, serviceProvider);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000122A0 File Offset: 0x000104A0
		public bool PublishService(string serviceIdentity, object service, [NotNull] Type serviceType, BlockServiceProviderIdentity serviceLevel, IBlock serviceProvider)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(serviceType, "serviceType");
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Service of Type {0} with the serviceIdentity {1} requested to publish itself by Block {2}", new object[]
			{
				serviceType.Name,
				serviceIdentity,
				(serviceProvider == null) ? "unidentifiedProvider" : serviceProvider.Name
			});
			if (this.m_stateMachine.State != BlockState.Initializing)
			{
				string text = "A service can only be published while system is initializing.";
				Block block = serviceProvider as Block;
				if (block != null && block.State == BlockState.Initializing)
				{
					text += "Late joiners are not allowed to publish services";
				}
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
				throw new IllegalBlockStateException(BlockState.Initializing, true);
			}
			bool flag = this.m_serviceTicketManager.PublishService(serviceIdentity, service, serviceType, serviceLevel, serviceProvider);
			if (flag)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Service of Type {0} published by Block {1}", new object[]
				{
					serviceType.Name,
					(serviceProvider == null) ? "unidentifiedProvider" : serviceProvider.Name
				});
			}
			else
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "Service of Type {0} was not published by Block {1} since it already exists", new object[]
				{
					serviceType.Name,
					(serviceProvider == null) ? "unidentifiedProvider" : serviceProvider.Name
				});
			}
			return flag;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000123C7 File Offset: 0x000105C7
		public BlockServiceTicket GetService(string name, IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context)
		{
			return this.GetService(new RequestedBlockService(name, serviceConsumer, serviceType, serviceIdentity, context));
		}

		// Token: 0x06000501 RID: 1281
		protected abstract void OnRequestShutdown(IBlock requestor, int returnCode);

		// Token: 0x06000502 RID: 1282 RVA: 0x000123DB File Offset: 0x000105DB
		protected virtual IEnumerable<IBlock> InterceptAddBlock(IBlock blockToAdd)
		{
			return new IBlock[] { blockToAdd };
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnBlockInitializationComplete(IBlock block)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnPostInitialize()
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnStart()
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnPostStart()
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnStop()
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnPostStop()
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnWaitForStopToComplete()
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnPostWaitForStopToComplete()
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnShutdown()
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnPostShutdown()
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000123E8 File Offset: 0x000105E8
		protected virtual void OnStateChangeFailed(BlockState stateBefore, BlockState stateDesired, string blockName, Exception exception)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "State transition '{0}'->'{1}' for block '{2}' failed due to '{3}'. Aborting", new object[]
			{
				stateBefore,
				stateDesired,
				blockName ?? "(application)",
				exception.Message
			});
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Stack trace: {0}", new object[] { exception });
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00012450 File Offset: 0x00010650
		internal virtual int GetTimeoutValue(BlockState blockState)
		{
			if (this.m_timeoutsPolicy == TimeoutsPolicy.Disabled)
			{
				return -1;
			}
			int num = 0;
			switch (blockState)
			{
			case BlockState.Initializing:
				num = this.m_initializeTimeoutTweak.Value;
				break;
			case BlockState.Starting:
				num = this.m_startTimeoutTweak.Value;
				break;
			case BlockState.Stopping:
				num = this.m_stopTimeoutTweak.Value;
				break;
			case BlockState.WaitingForStopToComplete:
				num = this.m_waitForStopToCompleteTimeoutTweak.Value;
				break;
			case BlockState.ShuttingDown:
				num = this.m_shutDownTimeoutTweak.Value;
				break;
			}
			if (this.m_timeoutsPolicy == TimeoutsPolicy.Enabled)
			{
				return num;
			}
			if (Debugger.IsAttached)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000124EC File Offset: 0x000106EC
		protected void AddBlockIfStarted(IBlock block)
		{
			if (this.m_stateMachine.State != BlockState.Started)
			{
				BlockHost.EnsureBlockState(this.m_stateMachine.State, BlockState.Started, "BlockHost.JoinBlock(" + block.Name + ") failed due to wrong block state. Use BlockHost.AddBlock to add blocks during initialization.");
			}
			this.AddBlockImpl(block, false);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001252C File Offset: 0x0001072C
		protected void AddBlock(IBlock block, bool validateBlockType)
		{
			if (this.m_stateMachine.State != BlockState.Uninitialized && this.m_stateMachine.State != BlockState.Initializing)
			{
				BlockHost.EnsureBlockState(this.m_stateMachine.State, BlockState.Initializing, "BlockHost.AddBlock(" + block.Name + ") failed due to wrong block state");
			}
			this.AddBlockImpl(block, validateBlockType);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00012582 File Offset: 0x00010782
		protected BlockHost([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(name, "name");
			this.m_name = name;
			this.m_creationCallStack = CallStackRef.Capture(true);
			this.Reset();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000125BE File Offset: 0x000107BE
		protected BlockState GetBlockHostStateForDebugging()
		{
			return this.m_stateMachine.State;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000125CC File Offset: 0x000107CC
		protected void RemoveBlockIfStarted(IBlock block)
		{
			if (this.m_stateMachine.State != BlockState.Started)
			{
				throw new IllegalBlockStateException(BlockState.Started, true, "A block can only be removed after Block Host has started and before it has started shutdown");
			}
			if (!this.m_blocks.Contains(block))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Block Host {0} does not have a block by the name of {1}", new object[] { this.m_name, block.Name }), "block");
			}
			if (this.m_serviceTicketManager.DidBlockPublishService(block))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Block {0} cannot be removed early since it has published a block service which may still be in use", new object[] { block.Name }));
			}
			this.StopBlocks(new IBlock[] { block }, false);
			this.WaitForStopToCompleteBlocks(new IBlock[] { block }, false);
			this.ShutdownBlocks(new IBlock[] { block }, false);
			this.m_blocks.Remove(block);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000126A6 File Offset: 0x000108A6
		public void Initialize()
		{
			this.Initialize(null);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000126B0 File Offset: 0x000108B0
		public void Initialize(IApplicationSwitches appSwitches)
		{
			IBlock curBlock = null;
			this.m_stateMachine.Initialize(delegate
			{
				this.InitStateTransitionsTimeouts(appSwitches);
				this.OnInitialize();
				int phase = 0;
				List<IBlock> list = new List<IBlock>(this.m_blocks);
				List<IBlock> list2 = new List<IBlock>(this.m_blocks);
				List<IBlock> list3;
				do
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceInformation("Initializing, phase number {0}. Num Blocks to Init - {1}", new object[] { phase, list.Count });
					list3 = new List<IBlock>();
					foreach (IBlock block in list)
					{
						BlockInitializationStatus initStatus = BlockInitializationStatus.PartiallyDone;
						using (this.m_activityFactory.CreateSyncActivity(new BlockInitializeActivity()))
						{
							using (new LifecycleEventTracer(block.Name, () => "status={0}, phase={1}".FormatWithInvariantCulture(new object[] { initStatus, phase })))
							{
								curBlock = block;
								initStatus = this.InitializeBlock(block);
								curBlock = null;
								if (initStatus == BlockInitializationStatus.PartiallyDone)
								{
									list3.Add(block);
								}
							}
						}
					}
					List<IBlock> list4 = this.FindBlocksNotInList(this.m_blocks, list2);
					list3.AddRange(list4);
					list2 = new List<IBlock>(this.m_blocks);
					if (list3.Count == 0)
					{
						return;
					}
					list = list3;
					int phase2 = phase;
					phase = phase2 + 1;
				}
				while (phase != 50);
				throw new InitializationErrorException(this.UnsatisfiedServices(list3));
			}, delegate(BlockState stateBefore, BlockState stateDesired, Exception ex)
			{
				this.OnStateChangeFailed(stateBefore, stateDesired, (curBlock == null) ? "(application)" : curBlock.Name, ex);
			});
			this.OnPostInitialize();
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceInformation("Initialization done, {0} blocks initialized", new object[] { this.m_blocks.Count });
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00012729 File Offset: 0x00010929
		public void Start()
		{
			this.Start(null);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00012732 File Offset: 0x00010932
		public void Start(Action beforeOnPostStart)
		{
			this.StartBlocks(this.m_blocks, true);
			if (beforeOnPostStart != null)
			{
				beforeOnPostStart();
			}
			this.OnPostStart();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00012750 File Offset: 0x00010950
		public void Stop()
		{
			this.StopBlocks(this.m_blocks, true);
			this.OnPostStop();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00012765 File Offset: 0x00010965
		public void WaitForStopToComplete()
		{
			this.WaitForStopToCompleteBlocks(this.m_blocks, true);
			this.OnPostWaitForStopToComplete();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001277A File Offset: 0x0001097A
		public void Shutdown()
		{
			this.ShutdownBlocks(this.m_blocks, true);
			if (this.m_creationCallStack != null)
			{
				this.m_creationCallStack.Dispose();
				this.m_creationCallStack = null;
			}
			this.Reset();
			this.OnPostShutdown();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000127B0 File Offset: 0x000109B0
		private void OnWatchdogTimeout(BlockState stateBefore, BlockState stateDesired, IBlock block)
		{
			if (stateDesired == BlockState.Stopped)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Block '{0}' state transition to 'Stopped' timed-out (WaitForStopToComplete)", new object[] { block.Name });
				Block block2 = block as Block;
				if (block2 != null)
				{
					block2.DumpPendingTicketsAsFatal(20);
				}
			}
			Exception ex = new BlockStateTransitionTimeoutException(stateBefore, stateDesired, block.Name);
			this.OnStateChangeFailed(stateBefore, stateDesired, block.Name, ex);
			BlockStateMachine.InvokeFailSlowDueToStateTransitionFailures(this, ex);
			throw ex;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00012818 File Offset: 0x00010A18
		private void InitStateTransitionsTimeouts(IApplicationSwitches appSwitches)
		{
			this.InitializeTimeoutsTweaks();
			if (appSwitches == null)
			{
				this.m_timeoutsPolicy = TimeoutsPolicy.Unspecified;
				return;
			}
			appSwitches.RegisterSwitch("stateTransTimeouts", "st", "Enables\\disables timeouts of Blocks State transitions. If disabled, all timeouts are set to Infinite. If enabled, timeouts are set to their default value. Value should be Enabled OR Disabled only", ParameterType.String, false, "unspecified");
			try
			{
				this.m_timeoutsPolicy = (TimeoutsPolicy)Enum.Parse(typeof(TimeoutsPolicy), appSwitches["st"], true);
			}
			catch (ArgumentException)
			{
				this.m_timeoutsPolicy = TimeoutsPolicy.Unspecified;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00012894 File Offset: 0x00010A94
		private BlockInitializationStatus InitializeBlock(IBlock block)
		{
			BlockInitializationStatus blockInitializationStatus2;
			using (WatchdogTimer.Start(this.GetTimeoutValue(BlockState.Initializing), delegate
			{
				this.OnWatchdogTimeout(BlockState.Initializing, BlockState.Initialized, block);
			}))
			{
				BlockInitializationStatus blockInitializationStatus = block.Initialize(this);
				if (blockInitializationStatus == BlockInitializationStatus.Done)
				{
					this.OnBlockInitializationComplete(block);
				}
				blockInitializationStatus2 = blockInitializationStatus;
			}
			return blockInitializationStatus2;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00012908 File Offset: 0x00010B08
		private void InvokeStateMachine<TActivity>(Action<BlockStateMachineCallback, BlockStateMachineFailureCallback, bool> stateMachineAction, IEnumerable<IBlock> blocks, Action<IBlock> advanceBlock, BlockState stateBefore, BlockState stateAfter, BlockState transitionState, Action preCallback, bool blockHostTransitioning, SingletonActivityType<TActivity> activityType) where TActivity : class, new()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "{0}", new object[] { "Advancing State machine from state " + stateBefore.ToString() + " to state " + stateAfter.ToString() });
			IBlock curBlock = null;
			stateMachineAction(delegate
			{
				if (blockHostTransitioning && preCallback != null)
				{
					preCallback();
				}
				using (IEnumerator<IBlock> enumerator = blocks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IBlock block = enumerator.Current;
						using (WatchdogTimer.Start(this.GetTimeoutValue(transitionState), delegate
						{
							this.OnWatchdogTimeout(stateBefore, stateAfter, block);
						}))
						{
							using (this.m_activityFactory.CreateSyncActivity(activityType))
							{
								using (new LifecycleEventTracer(block.Name))
								{
									curBlock = block;
									advanceBlock(block);
									curBlock = null;
								}
							}
						}
					}
				}
			}, delegate(BlockState before, BlockState desired, Exception ex)
			{
				this.OnStateChangeFailed(before, desired, (curBlock == null) ? "(application)" : curBlock.Name, ex);
			}, blockHostTransitioning);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000129D8 File Offset: 0x00010BD8
		private void StartBlocks(IEnumerable<IBlock> blocks, bool blockHostStarting)
		{
			this.InvokeStateMachine<BlockStartActivity>(new Action<BlockStateMachineCallback, BlockStateMachineFailureCallback, bool>(this.m_stateMachine.Start), blocks, delegate(IBlock block)
			{
				block.Start();
			}, BlockState.Initialized, BlockState.Started, BlockState.Starting, new Action(this.OnStart), blockHostStarting, new BlockStartActivity());
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00012A34 File Offset: 0x00010C34
		private void StopBlocks(IEnumerable<IBlock> blocks, bool blockHostStopping)
		{
			this.InvokeStateMachine<BlockStopActivity>(new Action<BlockStateMachineCallback, BlockStateMachineFailureCallback, bool>(this.m_stateMachine.Stop), blocks, delegate(IBlock block)
			{
				block.Stop();
			}, BlockState.Started, BlockState.Stopping, BlockState.Stopping, new Action(this.OnStop), blockHostStopping, new BlockStopActivity());
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00012A90 File Offset: 0x00010C90
		private void WaitForStopToCompleteBlocks(IEnumerable<IBlock> blocks, bool blockHostWaitingForStopToComplete)
		{
			this.InvokeStateMachine<BlockWaitForStopToCompleteActivity>(new Action<BlockStateMachineCallback, BlockStateMachineFailureCallback, bool>(this.m_stateMachine.WaitForStopToComplete), blocks, delegate(IBlock block)
			{
				block.WaitForStopToComplete();
			}, BlockState.Stopping, BlockState.Stopped, BlockState.WaitingForStopToComplete, new Action(this.OnWaitForStopToComplete), blockHostWaitingForStopToComplete, new BlockWaitForStopToCompleteActivity());
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00012AEC File Offset: 0x00010CEC
		private void ShutdownBlocks(IEnumerable<IBlock> blocks, bool blockHostShuttingDown)
		{
			this.InvokeStateMachine<BlockShutdownActivity>(new Action<BlockStateMachineCallback, BlockStateMachineFailureCallback, bool>(this.m_stateMachine.Shutdown), blocks, delegate(IBlock block)
			{
				block.Shutdown();
			}, BlockState.Stopped, BlockState.Uninitialized, BlockState.ShuttingDown, new Action(this.OnShutdown), blockHostShuttingDown, new BlockShutdownActivity());
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00012B48 File Offset: 0x00010D48
		private void AddBlockImpl(IBlock block, bool validateBlockType)
		{
			foreach (IBlock block2 in this.InterceptAddBlock(block))
			{
				this.TryAddBlock(block2, validateBlockType);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00012B98 File Offset: 0x00010D98
		private void TryAddBlock(IBlock block, bool validateBlockType)
		{
			Block block2 = block as Block;
			if (block2 != null)
			{
				BlockHost.EnsureBlockState(block2.State, BlockState.Uninitialized, string.Format(CultureInfo.CurrentCulture, "BlockHost.AddBlock({0}) failed due to wrong block state", new object[] { block.Name }));
			}
			if (this.BlockInList(block, this.m_blocks))
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "Block {0} was not added to list of blocks since same block already exists", new object[] { block.Name });
				return;
			}
			if (validateBlockType)
			{
				object executingAssembly = ExtendedAssembly.GetExecutingAssembly(typeof(BlockHost));
				Assembly assembly = block.GetType().Assembly;
				if (!executingAssembly.Equals(assembly))
				{
					Assembly firstForeignCallingAssembly = BlockHost.GetFirstForeignCallingAssembly(null);
					if (!(firstForeignCallingAssembly == null))
					{
						ExtendedDiagnostics.EnsureOperation(firstForeignCallingAssembly.Equals(assembly), string.Format(CultureInfo.CurrentCulture, "IBlockHost.AddBlock can only be called when the caller and the block it's adding are in the same assembly. The caller is in '{0}' and the block ({1}) is in '{2}", new object[] { firstForeignCallingAssembly.FullName, block, assembly.FullName }));
					}
				}
			}
			if (this.m_stateMachine.State == BlockState.Started)
			{
				this.AdvanceBlockToStartedState(block);
			}
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Block {0} was added to list of blocks", new object[] { block.Name });
			this.m_blocks.Add(block);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00012CB8 File Offset: 0x00010EB8
		private void AdvanceBlockToStartedState(IBlock block)
		{
			try
			{
				if (this.InitializeBlock(block) == BlockInitializationStatus.PartiallyDone)
				{
					throw new InitializationErrorException(this.GetUnsatisfiedServicesForBlock(block));
				}
			}
			catch (Exception ex)
			{
				this.OnStateChangeFailed(BlockState.Initializing, BlockState.Initialized, block.Name, ex);
				BlockStateMachine.InvokeFailSlowDueToStateTransitionFailures(this.m_stateMachine, ex);
				throw;
			}
			this.StartBlocks(new IBlock[] { block }, false);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00012D20 File Offset: 0x00010F20
		[CanBeNull]
		private static Assembly GetFirstForeignCallingAssembly(Assembly assembly)
		{
			StackTrace stackTrace = new StackTrace();
			assembly = assembly ?? Assembly.GetCallingAssembly();
			for (int i = 2; i < stackTrace.FrameCount; i++)
			{
				MethodBase method = stackTrace.GetFrame(i).GetMethod();
				Assembly assembly2 = method.Module.Assembly;
				if (!assembly.Equals(assembly2))
				{
					string text = method.DeclaringType.FullName + "." + method.Name;
					if (!(text == "Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters.TryFilterCatch") && !(text == "Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters.TryFilterCatchFaultFinally"))
					{
						return assembly2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00012DB0 File Offset: 0x00010FB0
		private static void EnsureBlockState(BlockState actualState, BlockState expectedState, string msg)
		{
			if (actualState != expectedState)
			{
				string text = string.Format(CultureInfo.CurrentCulture, "{0}: expected block state is {1}, but actual block state is {2}", new object[] { msg, expectedState, actualState });
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
				throw new IllegalBlockStateException(text);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012E10 File Offset: 0x00011010
		private bool BlockInList(IBlock block, List<IBlock> blocks)
		{
			if (blocks.Any((IBlock b) => b.Equals(block)))
			{
				return true;
			}
			if (block.Name != null)
			{
				if (blocks.Any((IBlock b) => string.Equals(b.Name, block.Name)))
				{
					string text = "A block with name: " + block.Name + " already exists";
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
					if (!BlockHost.s_TolerateRedundantAddBlockAndPublishService.Value)
					{
						TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
						{
							new StackTrace(0, true)
						});
						throw new InitializationErrorException(text);
					}
				}
			}
			else if (blocks.Any((IBlock b) => b.GetType() == block.GetType()))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00012EE0 File Offset: 0x000110E0
		private List<IBlock> FindBlocksNotInList(List<IBlock> lhs, List<IBlock> rhs)
		{
			return lhs.FindAll((IBlock b) => !this.BlockInList(b, rhs));
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00012F13 File Offset: 0x00011113
		private void Reset()
		{
			this.m_blocks = new List<IBlock>();
			this.m_stateMachine = new BlockStateMachine();
			this.m_serviceTicketManager = new BlockServiceManager(this.m_name + ".ServiceTicketManager");
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00012F46 File Offset: 0x00011146
		private void EnsureCanCreateServices()
		{
			if (!this.CanCreateServices())
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "A service can only be requested while system hasn't started shutdown");
				throw new IllegalBlockStateException(BlockState.Stopped, false);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00012F68 File Offset: 0x00011168
		private bool CanCreateServices()
		{
			return this.m_stateMachine.State <= BlockState.Stopping;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00012F7C File Offset: 0x0001117C
		private string UnsatisfiedServices(List<IBlock> blocksToInitNextPhase)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Exceeded max number of initialization phases allowed ({0}) for blockhost '{1}'", new object[]
			{
				50,
				base.GetType().Name
			});
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
			text += "\r\n";
			foreach (IBlock block in blocksToInitNextPhase)
			{
				text += this.GetUnsatisfiedServicesForBlock(block);
			}
			return text;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00013028 File Offset: 0x00011228
		private string GetUnsatisfiedServicesForBlock(IBlock block)
		{
			string text = "";
			foreach (RequestedBlockService requestedBlockService in this.m_serviceTicketManager.UnsatisfiedServiceRequests(block.Name))
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "Block {0} is missing service of ServiceType: '{1}', Level: '{2}', Identity:'{3}'. ", new object[]
				{
					block.Name,
					requestedBlockService.ServiceType.ToString(),
					requestedBlockService.ServiceIdentity.ToString(),
					requestedBlockService.Name ?? "(singleton)"
				});
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
				text = text + "\r\n" + text2;
			}
			return text;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001310C File Offset: 0x0001130C
		private void InitializeTimeoutsTweaks()
		{
			this.m_initializeTimeoutTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.ModularizationFramework.InitializeTimeout", "Modularization Framework initialization state transition timeout", 120000);
			this.m_startTimeoutTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.ModularizationFramework.StartTimeout", "Modularization Framework start state transition timeout", 90000);
			this.m_stopTimeoutTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.ModularizationFramework.StopTimeout", "Modularization Framework stop state transition timeout", 30000);
			this.m_waitForStopToCompleteTimeoutTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.ModularizationFramework.WaitForStopToCompleteTimeout", "Modularization Framework wait for stop to complete state transition timeout", 30000);
			this.m_shutDownTimeoutTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.ModularizationFramework.ShutDownTimeout", "Modularization Framework shut down state transition timeout", 90000);
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Timeout values: Initialize={0}, Start={1}, Stop={2}, WaitForStop={3}, ShutDown={4}", new object[]
			{
				this.m_initializeTimeoutTweak.Value,
				this.m_startTimeoutTweak.Value,
				this.m_stopTimeoutTweak.Value,
				this.m_waitForStopToCompleteTimeoutTweak.Value,
				this.m_shutDownTimeoutTweak.Value
			});
		}

		// Token: 0x040001B7 RID: 439
		private readonly string m_name;

		// Token: 0x040001B8 RID: 440
		private List<IBlock> m_blocks;

		// Token: 0x040001B9 RID: 441
		private BlockStateMachine m_stateMachine;

		// Token: 0x040001BA RID: 442
		private BlockServiceManager m_serviceTicketManager;

		// Token: 0x040001BB RID: 443
		private const int c_maxNumInitializationPhases = 50;

		// Token: 0x040001BC RID: 444
		private const int c_MaxNumberOfTicketsToDump = 20;

		// Token: 0x040001BD RID: 445
		private TimeoutsPolicy m_timeoutsPolicy;

		// Token: 0x040001BE RID: 446
		private CallStackRef m_creationCallStack;

		// Token: 0x040001BF RID: 447
		private const string cm_timeoutSwitchFullName = "stateTransTimeouts";

		// Token: 0x040001C0 RID: 448
		private const string cm_timeoutSwitchShortName = "st";

		// Token: 0x040001C1 RID: 449
		private Tweak<int> m_initializeTimeoutTweak;

		// Token: 0x040001C2 RID: 450
		private Tweak<int> m_startTimeoutTweak;

		// Token: 0x040001C3 RID: 451
		private Tweak<int> m_stopTimeoutTweak;

		// Token: 0x040001C4 RID: 452
		private Tweak<int> m_waitForStopToCompleteTimeoutTweak;

		// Token: 0x040001C5 RID: 453
		private Tweak<int> m_shutDownTimeoutTweak;

		// Token: 0x040001C6 RID: 454
		internal static Tweak<bool> s_TolerateRedundantAddBlockAndPublishService = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.ModularizationFramework.TolerateRedundantAddBlockAndPublishService", "When true, duplicated blocks added to the same block host and multi-publish of block service interfaces are tolerated", false);

		// Token: 0x040001C7 RID: 455
		private readonly ActivityFactory m_activityFactory = new ActivityFactory("BlockHostActivityFactory");

		// Token: 0x040001C8 RID: 456
		public const string TolerateRedundantAddBlockAndPublishServiceName = "Microsoft.Cloud.Platform.ModularizationFramework.TolerateRedundantAddBlockAndPublishService";
	}
}
