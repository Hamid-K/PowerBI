using System;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AFE RID: 2814
	public class XaRecovery : IXaRecoveryEnlistment
	{
		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06005957 RID: 22871 RVA: 0x00170E02 File Offset: 0x0016F002
		// (set) Token: 0x06005958 RID: 22872 RVA: 0x00170E0A File Offset: 0x0016F00A
		public int ResourceManagerId { get; set; }

		// Token: 0x06005959 RID: 22873 RVA: 0x00170E13 File Offset: 0x0016F013
		static XaRecovery()
		{
			XaRecovery.pooling.Pool = false;
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00170E50 File Offset: 0x0016F050
		public XaReturnCode Open(string xaInfo, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			if (!this.ParseRecoveryString(xaInfo))
			{
				return XaReturnCode.InvalidArguments;
			}
			QueueManagerConnectionParameters queueManagerConnectionParameters = new QueueManagerConnectionParameters();
			queueManagerConnectionParameters.Name = this.name;
			queueManagerConnectionParameters.Channel = this.channel;
			queueManagerConnectionParameters.ConnectAs = this.connectAs;
			queueManagerConnectionParameters.AuthorizationUser = this.authorizationUser;
			queueManagerConnectionParameters.AuthorizationPassword = this.authorizationPassword;
			queueManagerConnectionParameters.IsTransactional = true;
			queueManagerConnectionParameters.ResourceManagerId = this.ResourceManagerId;
			queueManagerConnectionParameters.InMsdtc = true;
			TcpConnectionParameters tcpConnectionParameters = new TcpConnectionParameters();
			tcpConnectionParameters.Server = this.host;
			tcpConnectionParameters.Port = this.port;
			tcpConnectionParameters.UseSsl = this.useSsl;
			if (XaRecovery.pooling.AcquireQueueManager(queueManagerConnectionParameters, tcpConnectionParameters, out this.iWrappedQueueManager) != ReturnCode.Ok)
			{
				return XaReturnCode.ResourceManagerError;
			}
			this.wrappedQueueManager = this.iWrappedQueueManager as WrappedPooledQueueManager;
			return XaReturnCode.Ok;
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00170F21 File Offset: 0x0016F121
		public XaReturnCode Close(string xaInfo, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			XaRecovery.pooling.ReturnQueueManager(this.iWrappedQueueManager);
			return XaReturnCode.Ok;
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00170F40 File Offset: 0x0016F140
		public XaReturnCode Start(Xid xid, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaStart;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.Xid = xid;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			return automatonQueueManagerContext.XaReturnCode;
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00170FB8 File Offset: 0x0016F1B8
		public XaReturnCode End(Xid xid, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaEnd;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.Xid = xid;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			return automatonQueueManagerContext.XaReturnCode;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x00171030 File Offset: 0x0016F230
		public XaReturnCode Rollback(Xid xid, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaRollback;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.Xid = xid;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			return automatonQueueManagerContext.XaReturnCode;
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x001710A8 File Offset: 0x0016F2A8
		public XaReturnCode Prepare(Xid xid, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaPrepare;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.Xid = xid;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			return automatonQueueManagerContext.XaReturnCode;
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00171120 File Offset: 0x0016F320
		public XaReturnCode Commit(Xid xid, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaCommit;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.Xid = xid;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			return automatonQueueManagerContext.XaReturnCode;
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00171198 File Offset: 0x0016F398
		public XaReturnCode Forget(Xid xid, XaFlags flags)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaForget;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.Xid = xid;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			return automatonQueueManagerContext.XaReturnCode;
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00171214 File Offset: 0x0016F414
		public XaReturnCode Recover(XaFlags flags, int maximumNumberOfXids, out Xid[] xids)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			xids = null;
			PooledQueueManager queueManager = this.wrappedQueueManager.QueueManager;
			AutomatonQueueManagerContext automatonQueueManagerContext = queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext.CommandReturnCode = ReturnCode.Ok;
			automatonQueueManagerContext.XaReturnCode = XaReturnCode.Ok;
			automatonQueueManagerContext.CommandType = MqCommandType.XaRecover;
			automatonQueueManagerContext.XaFlags = flags;
			automatonQueueManagerContext.MaximumNumberOfXids = maximumNumberOfXids;
			automatonQueueManagerContext.RecoveredXids = null;
			queueManager.Automaton.ProcessEvent(AutomatonQueueManagerEvent.MqCommand);
			automatonQueueManagerContext.CommandEvent.WaitOne();
			if (automatonQueueManagerContext.RecoveredXids == null)
			{
				return automatonQueueManagerContext.XaReturnCode;
			}
			xids = automatonQueueManagerContext.RecoveredXids;
			return XaReturnCode.Ok;
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x001712A9 File Offset: 0x0016F4A9
		public XaReturnCode Complete(int handle, XaFlags flags, out int returnValue)
		{
			flags &= ~XaFlags.OnePhaseOptimisation;
			returnValue = 0;
			return XaReturnCode.InvalidArguments;
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x001712BC File Offset: 0x0016F4BC
		private bool ParseRecoveryString(string xaInfo)
		{
			string[] array = xaInfo.Split(XaRecovery.greaterThans, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 7 && array.Length != 8)
			{
				return false;
			}
			if (!this.GetValue(array, 0, "Name", out this.name))
			{
				return false;
			}
			if (!this.GetValue(array, 1, "Channel", out this.channel))
			{
				return false;
			}
			if (!this.GetValue(array, 2, "Host", out this.host))
			{
				return false;
			}
			if (!this.GetValue(array, 3, "Port", out this.port))
			{
				return false;
			}
			if (!this.GetValue(array, 4, "UseSsl", out this.useSsl))
			{
				return false;
			}
			if (!this.GetValue(array, 5, "ConnectAs", out this.connectAs))
			{
				return false;
			}
			if (!this.GetDelimiterLessString(this.connectAs, out this.connectAs))
			{
				return false;
			}
			if (!this.GetValue(array, 6, "AuthorizationPassword", out this.authorizationPassword))
			{
				return false;
			}
			if (!this.GetDelimiterLessString(this.authorizationPassword, out this.authorizationPassword))
			{
				return false;
			}
			if (this.authorizationPassword != null)
			{
				if (!this.GetValue(array, 7, "AuthorizationUser", out this.authorizationUser))
				{
					return false;
				}
				if (!this.GetDelimiterLessString(this.authorizationUser, out this.authorizationUser))
				{
					return false;
				}
				if (string.IsNullOrWhiteSpace(this.authorizationUser))
				{
					return false;
				}
			}
			else
			{
				this.authorizationUser = null;
			}
			return true;
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x00171400 File Offset: 0x0016F600
		private bool GetDelimiterLessString(string delimitedString, out string delimiterLessString)
		{
			delimiterLessString = null;
			if (delimitedString.Length < 2)
			{
				return false;
			}
			if (delimitedString.Length == 2)
			{
				return delimitedString == "||";
			}
			if (!delimitedString.StartsWith("|") || !delimitedString.EndsWith("|"))
			{
				return false;
			}
			delimiterLessString = delimitedString.Substring(1, delimitedString.Length - 2);
			return true;
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x00171460 File Offset: 0x0016F660
		private bool GetValueElement(string[] pairs, int index, string expectedName, out string foundValue)
		{
			foundValue = null;
			string[] array = pairs[index].Split(XaRecovery.equal, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				return false;
			}
			if (string.CompareOrdinal(array[0], expectedName) != 0)
			{
				return false;
			}
			foundValue = array[1];
			return true;
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x0017149C File Offset: 0x0016F69C
		private bool GetValue(string[] pairs, int index, string expectedName, out string foundStringValue)
		{
			return this.GetValueElement(pairs, index, expectedName, out foundStringValue);
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x001714AC File Offset: 0x0016F6AC
		private bool GetValue(string[] pairs, int index, string expectedName, out int foundIntValue)
		{
			foundIntValue = -1;
			string text = null;
			return this.GetValueElement(pairs, index, expectedName, out text) && int.TryParse(text, out foundIntValue);
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x001714D8 File Offset: 0x0016F6D8
		private bool GetValue(string[] pairs, int index, string expectedName, out bool foundBoolValue)
		{
			foundBoolValue = false;
			string text = null;
			return this.GetValueElement(pairs, index, expectedName, out text) && bool.TryParse(text, out foundBoolValue);
		}

		// Token: 0x04004602 RID: 17922
		private string name;

		// Token: 0x04004603 RID: 17923
		private string channel;

		// Token: 0x04004604 RID: 17924
		private string host;

		// Token: 0x04004605 RID: 17925
		private int port;

		// Token: 0x04004606 RID: 17926
		private bool useSsl;

		// Token: 0x04004607 RID: 17927
		private string connectAs;

		// Token: 0x04004608 RID: 17928
		private string authorizationUser;

		// Token: 0x04004609 RID: 17929
		private string authorizationPassword;

		// Token: 0x0400460A RID: 17930
		private static string[] greaterThans = new string[] { ">>" };

		// Token: 0x0400460B RID: 17931
		private static char[] equal = new char[] { '=' };

		// Token: 0x0400460C RID: 17932
		private static Pooling pooling = new Pooling();

		// Token: 0x0400460D RID: 17933
		private IWrappedPooledQueueManager iWrappedQueueManager;

		// Token: 0x0400460E RID: 17934
		private WrappedPooledQueueManager wrappedQueueManager;
	}
}
