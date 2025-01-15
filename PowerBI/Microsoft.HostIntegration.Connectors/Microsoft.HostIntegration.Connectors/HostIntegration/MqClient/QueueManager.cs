using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B58 RID: 2904
	public class QueueManager
	{
		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06005BF8 RID: 23544 RVA: 0x0017B26E File Offset: 0x0017946E
		public QueueManagerState State
		{
			get
			{
				if (this.connectType == QueueManagerConnectType.Undefined || this.connectType == QueueManagerConnectType.Connecting)
				{
					return QueueManagerState.Disconnected;
				}
				if (this.WrappedQueueManager.Failed)
				{
					return QueueManagerState.Failed;
				}
				if (this.connectType != QueueManagerConnectType.Connected)
				{
					return QueueManagerState.Disconnected;
				}
				return QueueManagerState.Connected;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06005BF9 RID: 23545 RVA: 0x0017B29E File Offset: 0x0017949E
		// (set) Token: 0x06005BFA RID: 23546 RVA: 0x0017B2D4 File Offset: 0x001794D4
		public string Name
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "Name returns " + this.name);
				}
				return this.name;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNameReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				this.name = Globals.CheckMaximumLengthTrimmedNonNullTrace(value, "Name", 48, this.tracePoint);
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting Name to " + this.name);
				}
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x0017B355 File Offset: 0x00179555
		internal string NonTracedName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x0017B35D File Offset: 0x0017955D
		// (set) Token: 0x06005BFD RID: 23549 RVA: 0x0017B394 File Offset: 0x00179594
		public string DynamicQueueNamePrefix
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "DynamicQueueNamePrefix returns " + this.dynamicQueueNamePrefix);
				}
				return this.dynamicQueueNamePrefix;
			}
			set
			{
				string text = Globals.CheckMaximumLengthTrimmedNonNullTrace(value, "DynamicQueueNamePrefix", 48, this.tracePoint);
				if (text.EndsWith("*", StringComparison.InvariantCultureIgnoreCase))
				{
					text = Globals.CheckMaximumLengthTrimmedNonNullTrace(value, "DynamicQueueNamePrefix", 33, this.tracePoint);
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting DynamicQueueNamePrefix to " + text);
				}
				this.dynamicQueueNamePrefix = text;
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06005BFE RID: 23550 RVA: 0x0017B403 File Offset: 0x00179603
		internal string NonTracedDynamicQueueNamePrefix
		{
			get
			{
				return this.dynamicQueueNamePrefix;
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x0017B40B File Offset: 0x0017960B
		// (set) Token: 0x06005C00 RID: 23552 RVA: 0x0017B440 File Offset: 0x00179640
		public string ChannelName
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "ChannelName returns " + this.channelName);
				}
				return this.channelName;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.ChannelNameReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				string text = Globals.CheckMaximumLengthTrimmedNonNullTrace(value, "ChannelName", 20, this.tracePoint);
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting ChannelName to " + text);
				}
				this.channelName = text;
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x0017B4BE File Offset: 0x001796BE
		// (set) Token: 0x06005C02 RID: 23554 RVA: 0x0017B4F4 File Offset: 0x001796F4
		public string Host
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "Host returns " + this.host);
				}
				return this.host;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					ArgumentNullException ex = new ArgumentNullException("Host");
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex2 = new CustomMqClientException(SR.HostReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting Host to " + value);
				}
				this.host = value;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x0017B58E File Offset: 0x0017978E
		// (set) Token: 0x06005C04 RID: 23556 RVA: 0x0017B5CC File Offset: 0x001797CC
		public int Port
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "Port returns " + this.port.ToString(CultureInfo.InvariantCulture));
				}
				return this.port;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.PortReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				this.port = Globals.CheckRangeTrace(value, "Port", 1, 65535, this.tracePoint);
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting Port to " + this.port.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x0017B65B File Offset: 0x0017985B
		// (set) Token: 0x06005C06 RID: 23558 RVA: 0x0017B69C File Offset: 0x0017989C
		public bool UseSsl
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "UseSsl returns " + this.useSsl.ToString(CultureInfo.InvariantCulture));
				}
				return this.useSsl;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.UseSslReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting UseSsl to " + value.ToString(CultureInfo.InvariantCulture));
				}
				this.useSsl = value;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x0017B711 File Offset: 0x00179911
		// (set) Token: 0x06005C08 RID: 23560 RVA: 0x0017B73C File Offset: 0x0017993C
		public X509CertificateCollection CertificateCollection
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "CertificateCollection being Queried");
				}
				return this.certificateCollection;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.CertificateCollectionReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "CertificateCollection being Set");
				}
				this.certificateCollection = value;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x0017B7A0 File Offset: 0x001799A0
		// (set) Token: 0x06005C0A RID: 23562 RVA: 0x0017B7CC File Offset: 0x001799CC
		public List<string> ServerCertificateThumbprints
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "ServerCertificateThumbprints being Queried");
				}
				return this.serverCertificateThumbprints;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.ServerCertificateThumbprintsReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "ServerCertificateThumbprints being Set");
				}
				this.serverCertificateThumbprints = value;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x0017B830 File Offset: 0x00179A30
		// (set) Token: 0x06005C0C RID: 23564 RVA: 0x0017B864 File Offset: 0x00179A64
		public string ConnectAs
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "ConnectAs returns " + this.connectAs);
				}
				return this.connectAs;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.ConnectAsReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				string text = null;
				if (!string.IsNullOrWhiteSpace(value))
				{
					text = value.Trim();
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting ConnectAs to '" + ((text == null) ? "null" : text) + "'");
				}
				this.connectAs = text;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x0017B8EE File Offset: 0x00179AEE
		// (set) Token: 0x06005C0E RID: 23566 RVA: 0x0017B924 File Offset: 0x00179B24
		public string AuthorizationUser
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "AuthorizationUser returns " + this.authorizationUser);
				}
				return this.authorizationUser;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.AuthorizationUserReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				string text = null;
				if (!string.IsNullOrWhiteSpace(value))
				{
					text = value.Trim();
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting AuthorizationUser to '" + ((text == null) ? "null" : text) + "'");
				}
				this.authorizationUser = text;
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x0017B9B0 File Offset: 0x00179BB0
		// (set) Token: 0x06005C10 RID: 23568 RVA: 0x0017BA00 File Offset: 0x00179C00
		public string AuthorizationPassword
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "AuthorizationPassword returns " + ((this.authorizationPassword == null) ? "a value" : "null"));
				}
				return this.authorizationPassword;
			}
			set
			{
				if (this.connectType == QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.AuthorizationPasswordReadonlyAfterConnect);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting AuthorizationPassword to '" + ((value == null) ? "a value" : "null"));
				}
				this.authorizationPassword = value;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x0017BA78 File Offset: 0x00179C78
		// (set) Token: 0x06005C12 RID: 23570 RVA: 0x0017BAC0 File Offset: 0x00179CC0
		public int FailedCount
		{
			get
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				return this.WrappedQueueManager.FailedCount;
			}
			set
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				this.WrappedQueueManager.FailedCount = value;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x0017BB0C File Offset: 0x00179D0C
		// (set) Token: 0x06005C14 RID: 23572 RVA: 0x0017BB54 File Offset: 0x00179D54
		public int SucceededCount
		{
			get
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				return this.WrappedQueueManager.SucceededCount;
			}
			set
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				this.WrappedQueueManager.SucceededCount = value;
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x0017BBA0 File Offset: 0x00179DA0
		// (set) Token: 0x06005C16 RID: 23574 RVA: 0x0017BBE8 File Offset: 0x00179DE8
		public int WarningCount
		{
			get
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				return this.WrappedQueueManager.WarningCount;
			}
			set
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				this.WrappedQueueManager.WarningCount = value;
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06005C17 RID: 23575 RVA: 0x0017BC34 File Offset: 0x00179E34
		internal int MaximumMessageSize
		{
			get
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.maximumMessageSize == -1)
				{
					this.maximumMessageSize = this.WrappedQueueManager.MaximumMessageSize;
				}
				return this.maximumMessageSize;
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06005C18 RID: 23576 RVA: 0x0017BC91 File Offset: 0x00179E91
		// (set) Token: 0x06005C19 RID: 23577 RVA: 0x0017BC99 File Offset: 0x00179E99
		internal IWrappedPooledQueueManager WrappedQueueManager { get; private set; }

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06005C1A RID: 23578 RVA: 0x0017BCA2 File Offset: 0x00179EA2
		// (set) Token: 0x06005C1B RID: 23579 RVA: 0x0017BCA9 File Offset: 0x00179EA9
		public static QueueManagerPooling Pooling { get; private set; } = new QueueManagerPooling(QueueManager.runtimeAdministration.PoolingInformation.QueueManagerBehavior);

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00006F04 File Offset: 0x00005104
		internal virtual bool IsTransactional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06005C1D RID: 23581 RVA: 0x0017BCB1 File Offset: 0x00179EB1
		// (set) Token: 0x06005C1E RID: 23582 RVA: 0x0017BCB8 File Offset: 0x00179EB8
		public static bool CallerLogsEvents { get; set; }

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06005C1F RID: 23583 RVA: 0x0017BCC0 File Offset: 0x00179EC0
		internal bool IsConnectedAndAvailable
		{
			get
			{
				object obj = this.operationLockObject;
				bool flag2;
				lock (obj)
				{
					if (this.WrappedQueueManager == null)
					{
						flag2 = false;
					}
					else
					{
						flag2 = !this.WrappedQueueManager.Failed;
					}
				}
				return flag2;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06005C20 RID: 23584 RVA: 0x0017BD18 File Offset: 0x00179F18
		internal long TraceCorrelator
		{
			get
			{
				return this.traceContainer.Correlator;
			}
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x0017BD63 File Offset: 0x00179F63
		public QueueManager(string queueManagerAlias)
			: this(false, queueManagerAlias)
		{
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x0017BD70 File Offset: 0x00179F70
		protected QueueManager(bool transactional, string queueManagerAlias)
		{
			this.indexToQueues = new Dictionary<int, Queue>();
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			base..ctor();
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new ApiTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectType] = ApiObjectTypes.QueueManager;
			if (string.IsNullOrWhiteSpace(queueManagerAlias))
			{
				ArgumentNullException ex = new ArgumentNullException("queueManagerAlias");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, "TransactionalQueueManager Constructor called with Alias: '" + queueManagerAlias + "'");
				}
				else
				{
					this.tracePoint.Trace(TraceFlags.Information, "QueueManager Constructor called with Alias: '" + queueManagerAlias + "'");
				}
			}
			QueueManagerInformation queueManager = QueueManager.runtimeAdministration.GetQueueManager(queueManagerAlias);
			if (queueManager == null)
			{
				ArgumentException ex2 = new ArgumentException(SR.InvalidQueueManagerAlias(queueManagerAlias));
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectName] = queueManager.Name;
			if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.tracePoint.Trace(TraceFlags.Verbose, queueManager.ToString());
			}
			this.InternalConstructor(transactional, queueManager.Name, queueManager.Channel, queueManager.Host, queueManager.Port, queueManager.UseSsl, queueManager.ConnectAs, queueManager.DynamicQueueNamePrefix);
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x0017BEED File Offset: 0x0017A0ED
		public QueueManager(string name, string channelName, string host)
			: this(false, name, channelName, host)
		{
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x0017BEFC File Offset: 0x0017A0FC
		protected QueueManager(bool transactional, string name, string channelName, string host)
		{
			this.indexToQueues = new Dictionary<int, Queue>();
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			base..ctor();
			this.InternalConstructor(transactional, name, channelName, host, 1414, false, null, "AMQ.*");
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[] { "TransactionalQueueManager Constructor called with Name: '", name, "', Channel: '", channelName, "', Host: '", host, "'" }));
					return;
				}
				this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[] { "QueueManager Constructor called with Name: '", name, "', Channel: '", channelName, "', Host: '", host, "'" }));
			}
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x0017BFE0 File Offset: 0x0017A1E0
		public QueueManager(string name, string channelName, string host, int port)
			: this(false, name, channelName, host, port)
		{
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x0017BFF0 File Offset: 0x0017A1F0
		protected QueueManager(bool transactional, string name, string channelName, string host, int port)
		{
			this.indexToQueues = new Dictionary<int, Queue>();
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			base..ctor();
			this.InternalConstructor(transactional, name, channelName, host, port, false, null, "AMQ.*");
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
					{
						"TransactionalQueueManager Constructor called with Name: '",
						name,
						"', Channel: '",
						channelName,
						"', Host: '",
						host,
						"', Port: ",
						port.ToString(CultureInfo.InvariantCulture)
					}));
					return;
				}
				this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
				{
					"QueueManager Constructor called with Name: '",
					name,
					"', Channel: '",
					channelName,
					"', Host: '",
					host,
					"', Port: ",
					port.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x0017C0EF File Offset: 0x0017A2EF
		public QueueManager(string name, string channelName, string host, int port, bool sslUse)
			: this(false, name, channelName, host, port, sslUse)
		{
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x0017C100 File Offset: 0x0017A300
		protected QueueManager(bool transactional, string name, string channelName, string host, int port, bool sslUse)
		{
			this.indexToQueues = new Dictionary<int, Queue>();
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			base..ctor();
			this.InternalConstructor(transactional, name, channelName, host, port, sslUse, null, "AMQ.*");
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
					{
						"TransactionalQueueManager Constructor called with Name: '",
						name,
						"', Channel: '",
						channelName,
						"', Host: '",
						host,
						"', Port: ",
						port.ToString(CultureInfo.InvariantCulture),
						", UseSsl: ",
						sslUse.ToString(CultureInfo.InvariantCulture)
					}));
					return;
				}
				this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
				{
					"QueueManager Constructor called with Name: '",
					name,
					"', Channel: '",
					channelName,
					"', Host: '",
					host,
					"', Port: ",
					port.ToString(CultureInfo.InvariantCulture),
					", UseSsl: ",
					sslUse.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x0017C232 File Offset: 0x0017A432
		public QueueManager(string name, string channelName, string host, int port, bool sslUse, string connectAs)
			: this(false, name, channelName, host, port, sslUse, connectAs)
		{
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x0017C244 File Offset: 0x0017A444
		protected QueueManager(bool transactional, string name, string channelName, string host, int port, bool sslUse, string connectAs)
		{
			this.indexToQueues = new Dictionary<int, Queue>();
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			base..ctor();
			if (string.IsNullOrWhiteSpace(connectAs))
			{
				throw new ArgumentNullException("connectAs");
			}
			this.InternalConstructor(transactional, name, channelName, host, port, sslUse, connectAs, "AMQ.*");
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
					{
						"TransactionalQueueManager Constructor called with Name: '",
						name,
						"', Channel: '",
						channelName,
						"', Host: '",
						host,
						"', Port: ",
						port.ToString(CultureInfo.InvariantCulture),
						", UseSsl: ",
						sslUse.ToString(CultureInfo.InvariantCulture),
						", ConnectAs: ",
						connectAs
					}));
					return;
				}
				this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
				{
					"QueueManager Constructor called with Name: '",
					name,
					"', Channel: '",
					channelName,
					"', Host: '",
					host,
					"', Port: ",
					port.ToString(CultureInfo.InvariantCulture),
					", UseSsl: ",
					sslUse.ToString(CultureInfo.InvariantCulture),
					", ConnectAs: ",
					connectAs
				}));
			}
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x0017C3AC File Offset: 0x0017A5AC
		public QueueManager(string name, string channelName, string host, int port, bool sslUse, string connectAs, string dynamicQueueNamePrefix)
			: this(false, name, channelName, host, port, sslUse, connectAs, dynamicQueueNamePrefix)
		{
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x0017C3CC File Offset: 0x0017A5CC
		protected QueueManager(bool transactional, string name, string channelName, string host, int port, bool sslUse, string connectAs, string dynamicQueueNamePrefix)
		{
			this.indexToQueues = new Dictionary<int, Queue>();
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			base..ctor();
			if (string.IsNullOrWhiteSpace(dynamicQueueNamePrefix))
			{
				throw new ArgumentNullException("dynamicQueueNamePrefix");
			}
			this.InternalConstructor(transactional, name, channelName, host, port, sslUse, connectAs, dynamicQueueNamePrefix);
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
					{
						"TransactionalQueueManager Constructor called with Name: '",
						name,
						"', Channel: '",
						channelName,
						"', Host: '",
						host,
						"', Port: ",
						port.ToString(CultureInfo.InvariantCulture),
						", UseSsl: ",
						sslUse.ToString(CultureInfo.InvariantCulture),
						", ConnectAs: '",
						connectAs,
						"', DynamicQueueNamePrefix: '",
						dynamicQueueNamePrefix,
						"'"
					}));
					return;
				}
				this.tracePoint.Trace(TraceFlags.Information, string.Concat(new string[]
				{
					"QueueManager Constructor called with Name: '",
					name,
					"', Channel: '",
					channelName,
					"', Host: '",
					host,
					"', Port: ",
					port.ToString(CultureInfo.InvariantCulture),
					", UseSsl: ",
					sslUse.ToString(CultureInfo.InvariantCulture),
					", ConnectAs: '",
					connectAs,
					"', DynamicQueueNamePrefix: '",
					dynamicQueueNamePrefix,
					"'"
				}));
			}
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x0017C564 File Offset: 0x0017A764
		~QueueManager()
		{
			if (this.traceContainer != null)
			{
				this.traceContainer.Release();
			}
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x0017C5A0 File Offset: 0x0017A7A0
		public void Connect()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "QueueManager Connect being called");
				}
				if (this.connectType != QueueManagerConnectType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerAlreadyConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.authorizationPassword != null && string.IsNullOrWhiteSpace(this.authorizationUser))
				{
					CustomMqClientException ex2 = new CustomMqClientException(SR.QueueManagerConnectMissingAuthorizationUser);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
				try
				{
					this.connectType = QueueManagerConnectType.Connecting;
					TcpConnectionParameters tcpConnectionParameters = new TcpConnectionParameters();
					tcpConnectionParameters.Port = this.port;
					tcpConnectionParameters.Server = this.host;
					tcpConnectionParameters.UseSsl = this.useSsl;
					tcpConnectionParameters.CertificateCollection = this.certificateCollection;
					tcpConnectionParameters.ServerCertificateThumbprints = this.serverCertificateThumbprints;
					QueueManagerConnectionParameters queueManagerConnectionParameters = new QueueManagerConnectionParameters();
					queueManagerConnectionParameters.Channel = this.channelName;
					queueManagerConnectionParameters.Name = this.name;
					queueManagerConnectionParameters.ConnectAs = this.connectAs;
					queueManagerConnectionParameters.AuthorizationUser = this.authorizationUser;
					queueManagerConnectionParameters.AuthorizationPassword = this.authorizationPassword;
					queueManagerConnectionParameters.IsTransactional = this.IsTransactional;
					queueManagerConnectionParameters.ResourceManagerId = 0;
					queueManagerConnectionParameters.InMsdtc = false;
					IWrappedPooledQueueManager wrappedPooledQueueManager;
					ReturnCode returnCode = QueueManager.iPooling.AcquireQueueManager(queueManagerConnectionParameters, tcpConnectionParameters, out wrappedPooledQueueManager);
					if (returnCode != ReturnCode.Ok)
					{
						throw new CustomMqClientException(SR.QueueManagerConnectFailed(this.Name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
					}
					this.WrappedQueueManager = wrappedPooledQueueManager;
					this.connectType = QueueManagerConnectType.Connected;
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "QueueManager Connect Succeeded");
					}
				}
				catch (CustomMqClientException ex3)
				{
					this.connectType = QueueManagerConnectType.Undefined;
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex3);
					}
					if (!QueueManager.CallerLogsEvents)
					{
						QueueManager.eventLogContainer.WriteEvent(SR.ConnectQueueManagerException(ex3.Message), EventLogEntryType.Error);
					}
					throw;
				}
			}
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x0017C7E4 File Offset: 0x0017A9E4
		public void Disconnect()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "QueueManager Disconnect being called");
				}
				if (this.connectType != QueueManagerConnectType.Connected)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				Dictionary<int, Queue> dictionary = this.indexToQueues;
				lock (dictionary)
				{
					foreach (Queue queue in this.indexToQueues.Values)
					{
						try
						{
							queue.CloseOnly();
						}
						catch (CustomMqClientException)
						{
						}
					}
					this.indexToQueues.Clear();
					this.queueCounter = 0;
				}
				try
				{
					QueueManager.iPooling.ReturnQueueManager(this.WrappedQueueManager);
					this.WrappedQueueManager = null;
					this.connectType = QueueManagerConnectType.Disconnected;
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "QueueManager Disconnect Succeeded");
					}
				}
				catch (CustomMqClientException ex2)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					if (!QueueManager.CallerLogsEvents)
					{
						QueueManager.eventLogContainer.WriteEvent(SR.DisconnectQueueManagerException(ex2.Message), EventLogEntryType.Error);
					}
					throw;
				}
			}
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x0017C9CC File Offset: 0x0017ABCC
		public void UpdateAsyncCounters()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.WrappedQueueManager == null)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueManagerNotConnected);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				try
				{
					this.WrappedQueueManager.UpdateAsyncCounters();
				}
				catch (CustomMqClientException ex2)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					if (!QueueManager.CallerLogsEvents)
					{
						QueueManager.eventLogContainer.WriteEvent(SR.UpdateAsyncCountersException(ex2.Message), EventLogEntryType.Error);
					}
					throw;
				}
			}
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x0017CA8C File Offset: 0x0017AC8C
		private void InternalConstructor(bool transactional, string passedName, string passedChannelName, string passedHost, int passedPort, bool passedUseSsl, string passedConnectAs, string passedDynamicQueueNamePrefix)
		{
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new ApiTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectType] = ApiObjectTypes.QueueManager;
			if (string.IsNullOrWhiteSpace(passedName))
			{
				ArgumentNullException ex = new ArgumentNullException("name");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectName] = passedName;
			if (string.IsNullOrWhiteSpace(passedChannelName))
			{
				ArgumentNullException ex2 = new ArgumentNullException("channelName");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			if (string.IsNullOrWhiteSpace(passedHost))
			{
				ArgumentNullException ex3 = new ArgumentNullException("host");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex3);
				}
				throw ex3;
			}
			passedPort = Globals.CheckRangeTrace(passedPort, "port", 1, 65535, this.tracePoint);
			if (string.IsNullOrWhiteSpace(passedConnectAs))
			{
				passedConnectAs = null;
			}
			if (string.IsNullOrWhiteSpace(passedDynamicQueueNamePrefix))
			{
				ArgumentNullException ex4 = new ArgumentNullException("dynamicQueueNamePrefix");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex4);
				}
				throw ex4;
			}
			this.name = passedName;
			this.channelName = passedChannelName;
			this.host = passedHost;
			this.port = passedPort;
			this.useSsl = passedUseSsl;
			this.connectAs = passedConnectAs;
			this.dynamicQueueNamePrefix = passedDynamicQueueNamePrefix;
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x0017CBFC File Offset: 0x0017ADFC
		internal int AddOpenQueue(Queue queue)
		{
			Dictionary<int, Queue> dictionary = this.indexToQueues;
			int num;
			lock (dictionary)
			{
				this.queueCounter++;
				this.indexToQueues.Add(this.queueCounter, queue);
				num = this.queueCounter;
			}
			return num;
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x0017CC60 File Offset: 0x0017AE60
		internal void RemoveOpenQueue(int index)
		{
			Dictionary<int, Queue> dictionary = this.indexToQueues;
			lock (dictionary)
			{
				this.indexToQueues.Remove(index);
			}
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x0017CCA8 File Offset: 0x0017AEA8
		internal virtual void CheckTransactions()
		{
			if (Transaction.Current != null)
			{
				CustomMqClientException ex = new CustomMqClientException(SR.NonTransactionalInTransaction);
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
		}

		// Token: 0x04004831 RID: 18481
		private static IPooling iPooling = Globals.GetIPooling();

		// Token: 0x04004832 RID: 18482
		private string name;

		// Token: 0x04004833 RID: 18483
		private string channelName;

		// Token: 0x04004834 RID: 18484
		private string host;

		// Token: 0x04004835 RID: 18485
		private int port;

		// Token: 0x04004836 RID: 18486
		private bool useSsl;

		// Token: 0x04004837 RID: 18487
		private X509CertificateCollection certificateCollection;

		// Token: 0x04004838 RID: 18488
		private List<string> serverCertificateThumbprints;

		// Token: 0x04004839 RID: 18489
		private string connectAs;

		// Token: 0x0400483A RID: 18490
		private string authorizationUser;

		// Token: 0x0400483B RID: 18491
		private string authorizationPassword;

		// Token: 0x0400483C RID: 18492
		private string dynamicQueueNamePrefix;

		// Token: 0x0400483D RID: 18493
		private static MqClientRuntimeAdministration runtimeAdministration = MqClientRuntimeAdministration.GetAdministration();

		// Token: 0x0400483E RID: 18494
		private QueueManagerConnectType connectType;

		// Token: 0x0400483F RID: 18495
		protected ApiTracePoint tracePoint;

		// Token: 0x04004840 RID: 18496
		private static EventLogContainer eventLogContainer = QueueManager.iPooling.EventLogContainer;

		// Token: 0x04004841 RID: 18497
		private int queueCounter;

		// Token: 0x04004842 RID: 18498
		private Dictionary<int, Queue> indexToQueues;

		// Token: 0x04004843 RID: 18499
		private object operationLockObject;

		// Token: 0x04004844 RID: 18500
		private int maximumMessageSize;

		// Token: 0x04004845 RID: 18501
		private MqTraceContainer traceContainer;
	}
}
