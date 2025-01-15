using System;
using System.Globalization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000BA RID: 186
	internal class BlockStateMachine
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x000139F4 File Offset: 0x00011BF4
		public BlockStateMachine()
		{
			this.Reset();
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00013A02 File Offset: 0x00011C02
		public BlockState State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00013A0C File Offset: 0x00011C0C
		public void Initialize(BlockStateMachineCallback callback, BlockStateMachineFailureCallback failureCallback)
		{
			ExceptionFilters.TryFilterCatch(delegate
			{
				this.AdvanceState(BlockState.Uninitialized, BlockState.Initializing, "Initialize");
				callback();
				this.m_state = BlockState.Initialized;
			}, delegate(Exception ex)
			{
				if (failureCallback == null)
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeFailureCallback(failureCallback, BlockState.Initializing, BlockState.Initialized, ex);
				throw new IllegalBlockStateException(ex);
			});
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013A64 File Offset: 0x00011C64
		public BlockInitializationStatus Initialize(BlockStateMachineCallbackBool callback, BlockStateMachineFailureCallback failureCallback)
		{
			BlockInitializationStatus fRequiresAdditionalPhase = BlockInitializationStatus.PartiallyDone;
			ExceptionFilters.TryFilterCatch(delegate
			{
				if (this.m_state != BlockState.Uninitialized && this.m_state != BlockState.Initializing)
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "Initialize() called unexpectedly");
					throw new IllegalBlockStateException("Initialize() called unexpectedly; " + this.GetStateBanner());
				}
				this.m_state = BlockState.Initializing;
				fRequiresAdditionalPhase = callback();
				if (fRequiresAdditionalPhase == BlockInitializationStatus.Done)
				{
					this.m_state = BlockState.Initialized;
				}
			}, delegate(Exception ex)
			{
				if (failureCallback == null)
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeFailureCallback(failureCallback, BlockState.Initializing, BlockState.Initialized, ex);
				throw new IllegalBlockStateException(ex);
			});
			return fRequiresAdditionalPhase;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00013AC8 File Offset: 0x00011CC8
		public void Start(BlockStateMachineCallback callback, BlockStateMachineFailureCallback failureCallback, bool validateAndAdvanceState)
		{
			ExceptionFilters.TryFilterCatch(delegate
			{
				if (validateAndAdvanceState)
				{
					this.AdvanceState(BlockState.Initialized, BlockState.Starting, "Start");
				}
				callback();
				if (validateAndAdvanceState)
				{
					this.m_state = BlockState.Started;
				}
			}, delegate(Exception ex)
			{
				if (failureCallback == null)
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeFailureCallback(failureCallback, BlockState.Starting, BlockState.Started, ex);
				throw new IllegalBlockStateException(ex);
			});
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00013B28 File Offset: 0x00011D28
		public void Stop(BlockStateMachineCallback callback, BlockStateMachineFailureCallback failureCallback, bool validateAndAdvanceState)
		{
			ExceptionFilters.TryFilterCatch(delegate
			{
				if (validateAndAdvanceState)
				{
					this.AdvanceState(BlockState.Started, BlockState.Stopping, "Stop");
				}
				callback();
			}, delegate(Exception ex)
			{
				if (failureCallback == null)
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeFailureCallback(failureCallback, BlockState.Started, BlockState.Stopping, ex);
				throw new IllegalBlockStateException(ex);
			});
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00013B88 File Offset: 0x00011D88
		public void WaitForStopToComplete(BlockStateMachineCallback callback, BlockStateMachineFailureCallback failureCallback, bool validateAndAdvanceState)
		{
			ExceptionFilters.TryFilterCatch(delegate
			{
				if (validateAndAdvanceState)
				{
					this.AdvanceState(BlockState.Stopping, BlockState.WaitingForStopToComplete, "WaitForStopToComplete");
				}
				callback();
				if (validateAndAdvanceState)
				{
					this.m_state = BlockState.Stopped;
				}
			}, delegate(Exception ex)
			{
				if (failureCallback == null)
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeFailureCallback(failureCallback, BlockState.WaitingForStopToComplete, BlockState.Stopped, ex);
				throw new IllegalBlockStateException(ex);
			});
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00013BE8 File Offset: 0x00011DE8
		public void Shutdown(BlockStateMachineCallback callback, BlockStateMachineFailureCallback failureCallback, bool validateAndAdvanceState)
		{
			ExceptionFilters.TryFilterCatch(delegate
			{
				if (validateAndAdvanceState)
				{
					this.AdvanceState(BlockState.Stopped, BlockState.ShuttingDown, "Shutdown");
				}
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "BlockStateMachine.Shutdown() changes state from '{0}' to '{1}'", new object[]
				{
					BlockState.Stopped,
					BlockState.ShuttingDown
				});
				callback();
				if (validateAndAdvanceState)
				{
					this.Reset();
				}
			}, delegate(Exception ex)
			{
				if (failureCallback == null)
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeFailureCallback(failureCallback, BlockState.ShuttingDown, BlockState.Uninitialized, ex);
				throw new IllegalBlockStateException(ex);
			});
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00013C48 File Offset: 0x00011E48
		private void AdvanceState(BlockState expected, BlockState toAdvanceTo, string calledUnexpectedly)
		{
			if (this.m_state != expected)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "Start() called unexpectedly");
				string text = string.Format(CultureInfo.InvariantCulture, "{0}() called unexpectedly; {1}", new object[]
				{
					calledUnexpectedly,
					this.GetStateBanner()
				});
				throw new IllegalBlockStateException(expected, true, text);
			}
			this.m_state = toAdvanceTo;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00013CA3 File Offset: 0x00011EA3
		private void Reset()
		{
			this.m_state = BlockState.Uninitialized;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013CAC File Offset: 0x00011EAC
		private string GetStateBanner()
		{
			return "current state is " + this.m_state;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00013CC3 File Offset: 0x00011EC3
		private void InvokeFailureCallback(BlockStateMachineFailureCallback failureCallback, BlockState stateBefore, BlockState stateDesired, Exception ex)
		{
			if (failureCallback != null)
			{
				failureCallback(stateBefore, stateDesired, ex);
			}
			BlockStateMachine.InvokeFailSlowDueToStateTransitionFailures(this, ex);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00013CDA File Offset: 0x00011EDA
		internal static void InvokeFailSlowDueToStateTransitionFailures(object sender, Exception ex)
		{
			ExtendedEnvironment.FailSlow(sender, ex);
		}

		// Token: 0x040001D9 RID: 473
		private BlockState m_state;
	}
}
