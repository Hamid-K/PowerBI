using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Transactions;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF8 RID: 2808
	internal class PooledQueueManager : IXaClientEnlistment
	{
		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x0016D8AC File Offset: 0x0016BAAC
		// (set) Token: 0x060058DF RID: 22751 RVA: 0x0016D8B4 File Offset: 0x0016BAB4
		public string Name { get; private set; }

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x060058E0 RID: 22752 RVA: 0x0016D8BD File Offset: 0x0016BABD
		// (set) Token: 0x060058E1 RID: 22753 RVA: 0x0016D8C5 File Offset: 0x0016BAC5
		public AutomatonQueueManager Automaton { get; private set; }

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x060058E2 RID: 22754 RVA: 0x0016D8CE File Offset: 0x0016BACE
		// (set) Token: 0x060058E3 RID: 22755 RVA: 0x0016D8D6 File Offset: 0x0016BAD6
		public PooledConnection Connection { get; private set; }

		// Token: 0x17001546 RID: 5446
		// (set) Token: 0x060058E4 RID: 22756 RVA: 0x0016D8DF File Offset: 0x0016BADF
		public WrappedPooledQueueManager WrappedPooledQueueManager
		{
			set
			{
				(this.Automaton.Automaton.Context as AutomatonQueueManagerContext).WrappedQueueManager = value;
			}
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0016D8FC File Offset: 0x0016BAFC
		public PooledQueueManager(int id, QueueManagerConnectionParameters parameters, PooledConnection connection)
		{
			if (id < 1)
			{
				throw new InvalidOperationException("id < 1");
			}
			if (parameters == null)
			{
				throw new InvalidOperationException("parameters null");
			}
			if (parameters.Channel == null)
			{
				throw new InvalidOperationException("parameters.Channel null");
			}
			if (parameters.Name == null)
			{
				throw new InvalidOperationException("parameters.Name null");
			}
			if (connection == null)
			{
				throw new InvalidOperationException("connection null");
			}
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new QueueManagerTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.QueueManagerName] = parameters.Name;
			this.tracePoint[TracePointPropertyIdentifiers.Channel] = parameters.Channel;
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Queue Manager Name: {0}, via Channel: {1}", parameters.Name, parameters.Channel));
			}
			this.Name = parameters.Name;
			this.Automaton = new AutomatonQueueManager(this.tracePoint, Pooling.SingletonEventLogContainer);
			AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.ConnectionParameters = parameters;
			automatonQueueManagerContext.DeterminantForTcp = id;
			automatonQueueManagerContext.AutomatonTcp = connection.Automaton;
			this.Connection = connection;
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0016DA40 File Offset: 0x0016BC40
		~PooledQueueManager()
		{
			this.traceContainer.Release();
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x0016DA74 File Offset: 0x0016BC74
		public ReturnCode Connect(WindowsIdentity identity)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.ConnectReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.InFailedState = false;
			automatonQueueManagerContext.WindowsIdentity = identity;
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Connecting", Array.Empty<object>()));
			}
			if (automatonQueueManagerContext.DeterminantForTcp != 1)
			{
				this.Connection.ChannelParameters.FillContext(automatonQueueManagerContext);
			}
			this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.Connect);
			automatonQueueManagerContext.ConnectedEvent.WaitOne();
			if (automatonQueueManagerContext.ConnectReturnCode == ReturnCode.Ok)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Connect Succeeded", Array.Empty<object>()));
				}
			}
			else
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Connect Failed, Return Code: {0}", automatonQueueManagerContext.ConnectReturnCode));
				}
				this.Automaton.ClearBuffers();
				if (automatonQueueManagerContext.WrappedQueueManager != null)
				{
					automatonQueueManagerContext.WrappedQueueManager.QueueManager = null;
					automatonQueueManagerContext.WrappedQueueManager = null;
				}
				automatonQueueManagerContext.AutomatonTcp = null;
				this.WrappedPooledQueueManager = null;
			}
			return automatonQueueManagerContext.ConnectReturnCode;
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x0016DBB8 File Offset: 0x0016BDB8
		public ReturnCode Disconnect()
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.DisconnectReturnCode = ReturnCode.Ok;
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Disconnecting", Array.Empty<object>()));
			}
			this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.Disconnect);
			automatonQueueManagerContext.DisconnectedEvent.WaitOne();
			if (automatonQueueManagerContext.DisconnectReturnCode == ReturnCode.Ok)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Disconnect Succeeded", Array.Empty<object>()));
				}
			}
			else if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Disconnect Failed, Return Code: {0}", automatonQueueManagerContext.DisconnectReturnCode));
			}
			this.Automaton.ClearBuffers();
			if (automatonQueueManagerContext.WrappedQueueManager != null)
			{
				automatonQueueManagerContext.WrappedQueueManager.QueueManager = null;
				automatonQueueManagerContext.WrappedQueueManager = null;
			}
			automatonQueueManagerContext.AutomatonTcp = null;
			this.WrappedPooledQueueManager = null;
			return automatonQueueManagerContext.DisconnectReturnCode;
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0016DCD4 File Offset: 0x0016BED4
		public ReturnCode UpdateAsyncCounters()
		{
			object obj = this.lockObject;
			ReturnCode commandReturnCode;
			lock (obj)
			{
				AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
				automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Updating Async Counters", Array.Empty<object>()));
				}
				automatonQueueManagerContext.CommandType = MqCommandType.AsyncStatus;
				this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
				automatonQueueManagerContext.CommandEvent.WaitOne();
				if (automatonQueueManagerContext.CommandReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Update Succeeded", Array.Empty<object>()));
					}
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Update Failed, Return Code: {0}", automatonQueueManagerContext.CommandReturnCode));
				}
				commandReturnCode = automatonQueueManagerContext.CommandReturnCode;
			}
			return commandReturnCode;
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x0016DDF0 File Offset: 0x0016BFF0
		internal ReturnCode EnlistIfNeeded()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.enlisted)
				{
					return ReturnCode.Ok;
				}
			}
			if (Transaction.Current.TransactionInformation.Status != TransactionStatus.Active)
			{
				return ReturnCode.StaleTransaction;
			}
			if (this.WaitForEnlist() != ReturnCode.Ok)
			{
				return ReturnCode.XaEnlistmentFailed;
			}
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Enlisting in a transaction", Array.Empty<object>()));
			}
			Xid xid = null;
			try
			{
				xid = XaClient.EnlistXa(this, this.BuildRecoveryString());
				if (this.tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this.tracePoint.Trace(TraceFlags.Debug, string.Format(CultureInfo.InvariantCulture, "Enlisting in a transaction succeeded", Array.Empty<object>()));
				}
			}
			catch (CustomXaClientException ex)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				return ReturnCode.XaEnlistmentFailed;
			}
			return this.Start(xid);
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x0016DF08 File Offset: 0x0016C108
		public void Commit()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
				automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
				automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Commiting the XA transaction", Array.Empty<object>()));
				}
				automatonQueueManagerContext.CommandType = MqCommandType.XaCommit;
				automatonQueueManagerContext.XaFlags = XaFlags.None;
				this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
				automatonQueueManagerContext.CommandEvent.WaitOne();
				if (automatonQueueManagerContext.CommandReturnCode != ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "XA_COMMIT failed, Return Code: {0}, XA Code: {1}", automatonQueueManagerContext.CommandReturnCode.ToString(), automatonQueueManagerContext.XaReturnCode.ToString()));
					}
					throw new InvalidOperationException();
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_COMMIT Succeeded", Array.Empty<object>()));
				}
			}
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0016E05C File Offset: 0x0016C25C
		public XaReturnCode Prepare(bool singlePhaseOptimisation)
		{
			singlePhaseOptimisation = false;
			object obj = this.lockObject;
			XaReturnCode xaReturnCode;
			lock (obj)
			{
				this.enlisted = false;
				AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
				automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
				automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Ending the XA transaction", Array.Empty<object>()));
				}
				automatonQueueManagerContext.CommandType = MqCommandType.XaEnd;
				automatonQueueManagerContext.XaFlags = XaFlags.None;
				this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
				automatonQueueManagerContext.CommandEvent.WaitOne();
				if (automatonQueueManagerContext.CommandReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_END Succeeded", Array.Empty<object>()));
					}
					if (this.tracePoint.IsEnabled(TraceFlags.Information))
					{
						if (singlePhaseOptimisation)
						{
							this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Preparing the XA transaction, OnePhase-Commit Optimisation", Array.Empty<object>()));
						}
						else
						{
							this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Preparing the XA transaction", Array.Empty<object>()));
						}
					}
					automatonQueueManagerContext.CommandType = MqCommandType.XaPrepare;
					automatonQueueManagerContext.XaFlags = (singlePhaseOptimisation ? XaFlags.OnePhaseOptimisation : XaFlags.None);
					this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
					automatonQueueManagerContext.CommandEvent.WaitOne();
					if (automatonQueueManagerContext.CommandReturnCode == ReturnCode.Ok)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_PREPARE Succeeded", Array.Empty<object>()));
						}
					}
					else if (this.tracePoint.IsEnabled(TraceFlags.Information))
					{
						this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "XA_PREPARE failed, Return Code: {0}, XA Code: {1}", automatonQueueManagerContext.CommandReturnCode.ToString(), automatonQueueManagerContext.XaReturnCode.ToString()));
					}
					xaReturnCode = automatonQueueManagerContext.XaReturnCode;
				}
				else
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						FlagBasedTracePoint flagBasedTracePoint = this.tracePoint;
						TraceFlags traceFlags = TraceFlags.Error;
						IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
						string text = "XA_END failed, Return Code: {0}, XA Code: {1}";
						object obj2 = automatonQueueManagerContext.CommandReturnCode.ToString();
						xaReturnCode = automatonQueueManagerContext.XaReturnCode;
						flagBasedTracePoint.Trace(traceFlags, string.Format(invariantCulture, text, obj2, xaReturnCode.ToString()));
					}
					xaReturnCode = automatonQueueManagerContext.XaReturnCode;
				}
			}
			return xaReturnCode;
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0016E2DC File Offset: 0x0016C4DC
		public void Rollback()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.enlisted = false;
				AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
				automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
				automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Rolling Back the XA transaction", Array.Empty<object>()));
				}
				automatonQueueManagerContext.CommandType = MqCommandType.XaRollback;
				automatonQueueManagerContext.XaFlags = XaFlags.None;
				this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
				automatonQueueManagerContext.CommandEvent.WaitOne();
				if (automatonQueueManagerContext.CommandReturnCode != ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "XA_ROLLBACK failed, Return Code: {0}, XA Code: {1}", automatonQueueManagerContext.CommandReturnCode.ToString(), automatonQueueManagerContext.XaReturnCode.ToString()));
					}
					throw new InvalidOperationException();
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_ROLLBACK Succeeded", Array.Empty<object>()));
				}
			}
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0016E434 File Offset: 0x0016C634
		private ReturnCode WaitForEnlist()
		{
			object obj = this.lockObject;
			AutomatonQueueManagerContext automatonQueueManagerContext;
			lock (obj)
			{
				automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
				automatonQueueManagerContext.EnlistReturnCode = ReturnCode.Ok;
			}
			if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Waiting to be able to Enlist", Array.Empty<object>()));
			}
			this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.Enlist);
			automatonQueueManagerContext.EnlistEvent.WaitOne();
			obj = this.lockObject;
			ReturnCode returnCode;
			lock (obj)
			{
				if (automatonQueueManagerContext.EnlistReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Waiting Succeeded", Array.Empty<object>()));
					}
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					FlagBasedTracePoint flagBasedTracePoint = this.tracePoint;
					TraceFlags traceFlags = TraceFlags.Error;
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string text = "Waiting failed, Return Code: {0}";
					returnCode = automatonQueueManagerContext.EnlistReturnCode;
					flagBasedTracePoint.Trace(traceFlags, string.Format(invariantCulture, text, returnCode.ToString()));
				}
				returnCode = automatonQueueManagerContext.EnlistReturnCode;
			}
			return returnCode;
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0016E57C File Offset: 0x0016C77C
		private ReturnCode Start(Xid xid)
		{
			object obj = this.lockObject;
			ReturnCode returnCode;
			lock (obj)
			{
				AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
				automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
				automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Starting the XA transaction", Array.Empty<object>()));
				}
				automatonQueueManagerContext.CommandType = MqCommandType.XaStart;
				automatonQueueManagerContext.XaFlags = XaFlags.None;
				automatonQueueManagerContext.Xid = xid;
				this.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
				automatonQueueManagerContext.CommandEvent.WaitOne();
				if (automatonQueueManagerContext.CommandReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_START Succeeded", Array.Empty<object>()));
					}
					this.enlisted = true;
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					FlagBasedTracePoint flagBasedTracePoint = this.tracePoint;
					TraceFlags traceFlags = TraceFlags.Information;
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string text = "XA_START failed, Return Code: {0}, XA Code: {1}";
					returnCode = automatonQueueManagerContext.CommandReturnCode;
					flagBasedTracePoint.Trace(traceFlags, string.Format(invariantCulture, text, returnCode.ToString(), automatonQueueManagerContext.XaReturnCode.ToString()));
				}
				returnCode = automatonQueueManagerContext.CommandReturnCode;
			}
			return returnCode;
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0016E6E0 File Offset: 0x0016C8E0
		private string BuildRecoveryString()
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = this.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			QueueManagerConnectionParameters connectionParameters = automatonQueueManagerContext.ConnectionParameters;
			TcpConnectionParameters connectionParameters2 = (automatonQueueManagerContext.AutomatonTcp.Automaton.Context as AutomatonTcpContext).ConnectionParameters;
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.AppendFormat("MQ>>Name={0}>>Channel={1}>>Host={2}>>Port={3}", new object[]
			{
				connectionParameters.Name,
				connectionParameters.Channel,
				connectionParameters2.Server,
				connectionParameters2.Port.ToString(CultureInfo.InvariantCulture)
			});
			if (connectionParameters2.UseSsl)
			{
				stringBuilder.Append(">>UseSsl=true");
			}
			else
			{
				stringBuilder.Append(">>UseSsl=false");
			}
			string text;
			string text2;
			PooledQueueManager.GetEffectiveUserId(automatonQueueManagerContext.WindowsIdentity, connectionParameters.ConnectAs, out text, out text2);
			stringBuilder.AppendFormat(">>ConnectAs=|{0}|", text2);
			if (connectionParameters.AuthorizationPassword != null)
			{
				stringBuilder.AppendFormat(">>AuthorizationPassword=|{0}|", connectionParameters.AuthorizationPassword);
				if (connectionParameters.AuthorizationUser != null)
				{
					stringBuilder.AppendFormat(">>AuthorizationUser=|{0}|", connectionParameters.AuthorizationUser);
				}
				else
				{
					stringBuilder.AppendFormat(">>AuthorizationUser=|{0}|", automatonQueueManagerContext.WindowsIdentity.Name);
				}
			}
			else
			{
				stringBuilder.Append(">>AuthorizationPassword=||");
			}
			string text3 = stringBuilder.ToString();
			if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Recovery Information: {0}", text3));
			}
			return text3;
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0016E84C File Offset: 0x0016CA4C
		public static bool GetEffectiveUserId(WindowsIdentity identity, string connectAs, out string userId, out string upperUserId)
		{
			bool flag = false;
			if (connectAs != null)
			{
				userId = connectAs;
				flag = true;
			}
			else
			{
				userId = identity.Name;
			}
			int num = userId.IndexOf('\\');
			if (num != -1)
			{
				userId = userId.Substring(num + 1);
			}
			upperUserId = userId.ToUpperInvariant();
			return flag;
		}

		// Token: 0x040045DB RID: 17883
		private bool enlisted;

		// Token: 0x040045DC RID: 17884
		private object lockObject = new object();

		// Token: 0x040045DF RID: 17887
		private QueueManagerTracePoint tracePoint;

		// Token: 0x040045E0 RID: 17888
		private MqTraceContainer traceContainer;
	}
}
