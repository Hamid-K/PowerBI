using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.HostIntegration.CounterTelemetry;
using Microsoft.HostIntegration.CounterTelemetry.MQ;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.PerformanceCounters;
using Microsoft.HostIntegration.PerformanceCounters.MqClient;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B56 RID: 2902
	public class Queue
	{
		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x0017986C File Offset: 0x00177A6C
		public int MaximumMessageSize
		{
			get
			{
				if (this.State != QueueState.OpenSend || this.State != QueueState.OpenReceive)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueNotOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				return this.maximumMessageSize;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06005BDA RID: 23514 RVA: 0x001798B9 File Offset: 0x00177AB9
		public QueueState State
		{
			get
			{
				if (this.openType == QueueOpenType.Undefined || this.openType == QueueOpenType.Opening)
				{
					return QueueState.Closed;
				}
				if (this.queue.Failed)
				{
					return QueueState.Failed;
				}
				if (this.openType != QueueOpenType.Receive)
				{
					return QueueState.OpenSend;
				}
				return QueueState.OpenReceive;
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x001798E9 File Offset: 0x00177AE9
		// (set) Token: 0x06005BDC RID: 23516 RVA: 0x00179920 File Offset: 0x00177B20
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
				if (this.openType != QueueOpenType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueNameReadonlyAfterOpen);
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

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x001799A0 File Offset: 0x00177BA0
		public QueueManager QueueManager
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "QueueManager returns the Queue Manager with Name " + this.queueManager.NonTracedName);
				}
				this.managerBelongsToQueue = false;
				return this.queueManager;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06005BDE RID: 23518 RVA: 0x001799E0 File Offset: 0x00177BE0
		// (set) Token: 0x06005BDF RID: 23519 RVA: 0x00179A1F File Offset: 0x00177C1F
		public OpenOption Options
		{
			get
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "Options returns " + this.options.ToString());
				}
				return this.options;
			}
			set
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Setting Options to " + value.ToString());
				}
				this.options = value;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06005BE0 RID: 23520 RVA: 0x00179A59 File Offset: 0x00177C59
		// (set) Token: 0x06005BE1 RID: 23521 RVA: 0x00179A61 File Offset: 0x00177C61
		internal bool SavesContext { get; private set; }

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x00179A6A File Offset: 0x00177C6A
		// (set) Token: 0x06005BE3 RID: 23523 RVA: 0x00179A72 File Offset: 0x00177C72
		internal int ObjectHandle { get; private set; }

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00179B97 File Offset: 0x00177D97
		public Queue(string queueAlias)
			: this(queueAlias, false)
		{
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x00179BA4 File Offset: 0x00177DA4
		protected Queue(string queueAlias, bool transactional)
		{
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			this.options = OpenOption.Inquire | OpenOption.FailOnQuiesce;
			base..ctor();
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new ApiTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectType] = ApiObjectTypes.Queue;
			if (string.IsNullOrWhiteSpace(queueAlias))
			{
				ArgumentNullException ex = new ArgumentNullException("queueAlias");
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
					this.tracePoint.Trace(TraceFlags.Information, "TransactionalQueue Constructor called with Alias: '" + queueAlias + "'");
				}
				else
				{
					this.tracePoint.Trace(TraceFlags.Information, "Queue Constructor called with Alias: '" + queueAlias + "'");
				}
			}
			QueueInformation queueInformation = Queue.runtimeAdministration.GetQueue(queueAlias);
			if (queueInformation == null)
			{
				ArgumentException ex2 = new ArgumentException(SR.InvalidQueueAlias(queueAlias));
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectName] = queueInformation.Name;
			this.name = Globals.CheckMaximumLengthTrimmedNonNullTrace(queueInformation.Name, "Name", 48, this.tracePoint);
			QueueManagerInformation queueManagerInformation = queueInformation.QueueManager;
			if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.tracePoint.Trace(TraceFlags.Verbose, string.Concat(new string[] { "Alias resolves to - Queue Name: '", this.name, "', Queue Manager Name: '", queueManagerInformation.Name, "'" }));
			}
			if (transactional)
			{
				this.queueManager = new TransactionalQueueManager(queueManagerInformation.Name, queueManagerInformation.Channel, queueManagerInformation.Host, queueManagerInformation.Port, queueManagerInformation.UseSsl, queueManagerInformation.ConnectAs, queueManagerInformation.DynamicQueueNamePrefix);
			}
			else
			{
				this.queueManager = new QueueManager(queueManagerInformation.Name, queueManagerInformation.Channel, queueManagerInformation.Host, queueManagerInformation.Port, queueManagerInformation.UseSsl, queueManagerInformation.ConnectAs, queueManagerInformation.DynamicQueueNamePrefix);
			}
			this.managerBelongsToQueue = true;
			if (this.tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this.tracePoint.Trace(TraceFlags.Debug, "Queue Manager Trace Correlator: " + this.queueManager.TraceCorrelator.ToString(CultureInfo.InvariantCulture));
			}
			this.cumulativePutsToUse = (transactional ? Queue.cumulativeTransactionalPuts : Queue.cumulativePuts);
			this.cumulativeGetsToUse = (transactional ? Queue.cumulativeTransactionalGets : Queue.cumulativeGets);
			this.putsPerSecondToUse = (transactional ? Queue.transactionalPutsPerSecond : Queue.putsPerSecond);
			this.getsPerSecondToUse = (transactional ? Queue.transactionalGetsPerSecond : Queue.getsPerSecond);
			this.averagePutTimeToUse = (transactional ? MqClientPerformanceCounter.AverageTransactionalPutTime : MqClientPerformanceCounter.AveragePutTime);
			this.averageGetTimeToUse = (transactional ? MqClientPerformanceCounter.AverageTransactionalGetTime : MqClientPerformanceCounter.AverageGetTime);
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x00179E6A File Offset: 0x0017806A
		public Queue(string name, QueueManager queueManager)
			: this(name, queueManager, false)
		{
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00179E78 File Offset: 0x00178078
		protected Queue(string name, QueueManager queueManager, bool transactional)
		{
			this.operationLockObject = new object();
			this.maximumMessageSize = -1;
			this.options = OpenOption.Inquire | OpenOption.FailOnQuiesce;
			base..ctor();
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new ApiTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectType] = ApiObjectTypes.Queue;
			if (string.IsNullOrWhiteSpace(name))
			{
				ArgumentNullException ex = new ArgumentNullException("name");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
			this.tracePoint[TracePointPropertyIdentifiers.ApiObjectName] = name;
			if (queueManager == null)
			{
				ArgumentNullException ex2 = new ArgumentNullException("queueManager");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			this.CheckTransactions(queueManager);
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				if (transactional)
				{
					this.tracePoint.Trace(TraceFlags.Information, "TransactionalQueue Constructor called with Name: '" + name + "'");
				}
				else
				{
					this.tracePoint.Trace(TraceFlags.Information, "Queue Constructor called with Name: '" + name + "'");
				}
			}
			this.name = Globals.CheckMaximumLengthTrimmedNonNullTrace(name, "Name", 48, this.tracePoint);
			this.queueManager = queueManager;
			this.managerBelongsToQueue = false;
			if (this.tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this.tracePoint.Trace(TraceFlags.Debug, "Queue Manager Trace Correlator: " + queueManager.TraceCorrelator.ToString(CultureInfo.InvariantCulture));
			}
			this.cumulativePutsToUse = (transactional ? Queue.cumulativeTransactionalPuts : Queue.cumulativePuts);
			this.cumulativeGetsToUse = (transactional ? Queue.cumulativeTransactionalGets : Queue.cumulativeGets);
			this.putsPerSecondToUse = (transactional ? Queue.transactionalPutsPerSecond : Queue.putsPerSecond);
			this.getsPerSecondToUse = (transactional ? Queue.transactionalGetsPerSecond : Queue.getsPerSecond);
			this.averagePutTimeToUse = (transactional ? MqClientPerformanceCounter.AverageTransactionalPutTime : MqClientPerformanceCounter.AveragePutTime);
			this.averageGetTimeToUse = (transactional ? MqClientPerformanceCounter.AverageTransactionalGetTime : MqClientPerformanceCounter.AverageGetTime);
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0017A068 File Offset: 0x00178268
		~Queue()
		{
			if (this.traceContainer != null)
			{
				this.traceContainer.Release();
			}
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x0017A0A4 File Offset: 0x001782A4
		public void OpenForSend()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Queue being opened for Send");
				}
				if (this.openType != QueueOpenType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueAlreadyOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.queueManager == null)
				{
					CustomMqClientException ex2 = new CustomMqClientException(SR.OpenWithNoQueueManager);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
				if ((this.options & OpenOption.Shared) == OpenOption.Shared)
				{
					CustomMqClientException ex3 = new CustomMqClientException(SR.OpenSendWithShared);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex3);
					}
					throw ex3;
				}
				try
				{
					if (this.managerBelongsToQueue)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, "Calling the self-generated Queue Manager's Connect");
						}
						this.queueManager.Connect();
					}
					if (!this.queueManager.IsConnectedAndAvailable)
					{
						throw new CustomMqClientException(SR.QueueOpenFailedQmDisconnected(this.name, this.queueManager.NonTracedName));
					}
					this.maximumMessageSize = this.queueManager.MaximumMessageSize;
					this.openType = QueueOpenType.Opening;
					QueueConnectionParameters queueConnectionParameters = new QueueConnectionParameters();
					queueConnectionParameters.Name = this.name;
					this.queue = Queue.iPooling.AcquireQueue(queueConnectionParameters, this.queueManager.WrappedQueueManager);
					OpenOption openOption = this.options | (OpenOption)16;
					string text;
					ReturnCode returnCode = this.queue.Open(true, (int)openOption, this.queueManager.NonTracedDynamicQueueNamePrefix, out text);
					if (returnCode != ReturnCode.Ok)
					{
						throw new CustomMqClientException(SR.QueueOpenFailed(this.name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
					}
					if (string.CompareOrdinal(text, this.name) != 0)
					{
						this.name = text;
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, "Changing the Queue Name to: " + this.name);
						}
					}
					this.openType = QueueOpenType.Send;
					this.ObjectHandle = this.queue.ObjectHandle;
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Queue Open Succeeded - Object Handle: " + this.ObjectHandle.ToString(CultureInfo.InvariantCulture));
					}
					Queue.mqClientCounterTelemetry.Increment(QueueProcess.Open);
				}
				catch (Exception ex4)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex4);
					}
					if (this.openType == QueueOpenType.Opening && this.managerBelongsToQueue)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, "Calling the self-generated Queue Manager's Disconnect");
						}
						try
						{
							this.queueManager.Disconnect();
						}
						catch (CustomMqClientException)
						{
						}
						finally
						{
							this.queueManager = null;
						}
					}
					this.openType = QueueOpenType.Undefined;
					if (!QueueManager.CallerLogsEvents)
					{
						Queue.eventLogContainer.WriteEvent(SR.OpenQueueException(ex4.Message), EventLogEntryType.Error);
					}
					throw;
				}
				this.queueIndex = this.queueManager.AddOpenQueue(this);
			}
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x0017A420 File Offset: 0x00178620
		public void OpenForReceive()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Queue being opened for Receive");
				}
				if (this.openType != QueueOpenType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueAlreadyOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.queueManager == null)
				{
					CustomMqClientException ex2 = new CustomMqClientException(SR.OpenWithNoQueueManager);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
				try
				{
					if (this.managerBelongsToQueue)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, "Calling the self-generated Queue Manager's Connect");
						}
						this.queueManager.Connect();
					}
					if (!this.queueManager.IsConnectedAndAvailable)
					{
						throw new CustomMqClientException(SR.QueueOpenFailedQmDisconnected(this.name, this.queueManager.NonTracedName));
					}
					this.maximumMessageSize = this.queueManager.MaximumMessageSize;
					this.openType = QueueOpenType.Opening;
					QueueConnectionParameters queueConnectionParameters = new QueueConnectionParameters();
					queueConnectionParameters.Name = this.name;
					this.queue = Queue.iPooling.AcquireQueue(queueConnectionParameters, this.queueManager.WrappedQueueManager);
					OpenOption openOption = this.options;
					if ((openOption & OpenOption.Shared) != OpenOption.Shared && (openOption & OpenOption.Exclusive) != OpenOption.Exclusive)
					{
						openOption |= (OpenOption)1;
					}
					this.SavesContext = (openOption & OpenOption.SaveContext) == OpenOption.SaveContext;
					string text;
					ReturnCode returnCode = this.queue.Open(false, (int)openOption, this.queueManager.NonTracedDynamicQueueNamePrefix, out text);
					if (returnCode != ReturnCode.Ok)
					{
						throw new CustomMqClientException(SR.QueueOpenFailed(this.name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
					}
					if (string.CompareOrdinal(text, this.name) != 0)
					{
						this.name = text;
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, "Changing the Queue Name to: " + this.name);
						}
					}
					this.openType = QueueOpenType.Receive;
					this.ObjectHandle = this.queue.ObjectHandle;
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Queue Open Succeeded - Object Handle: " + this.ObjectHandle.ToString(CultureInfo.InvariantCulture));
					}
					if ((openOption & OpenOption.ReadAhead) == OpenOption.ReadAhead || (openOption & OpenOption.Shared) == OpenOption.Shared || (openOption & OpenOption.Exclusive) == OpenOption.Exclusive)
					{
						this.openAllowsReceiveWithoutOptions = true;
					}
					Queue.mqClientCounterTelemetry.Increment(QueueProcess.Open);
				}
				catch (CustomMqClientException ex3)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex3);
					}
					if (this.openType == QueueOpenType.Opening && this.managerBelongsToQueue)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this.tracePoint.Trace(TraceFlags.Verbose, "Calling the self-generated Queue Manager's Disconnect");
						}
						try
						{
							this.queueManager.Disconnect();
						}
						catch (CustomMqClientException)
						{
						}
						finally
						{
							this.queueManager = null;
						}
					}
					this.openType = QueueOpenType.Undefined;
					if (!QueueManager.CallerLogsEvents)
					{
						Queue.eventLogContainer.WriteEvent(SR.OpenQueueException(ex3.Message), EventLogEntryType.Error);
					}
					throw;
				}
				this.queueIndex = this.queueManager.AddOpenQueue(this);
			}
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x0017A7B0 File Offset: 0x001789B0
		public void Close()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Queue being closed");
				}
				if (this.openType == QueueOpenType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueNotOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				try
				{
					ReturnCode returnCode = this.queue.Close();
					if (returnCode != ReturnCode.Ok)
					{
						throw new CustomMqClientException(SR.QueueCloseFailed(this.name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
					}
					this.queue = null;
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Close Succeeded");
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
						Queue.eventLogContainer.WriteEvent(SR.CloseQueueException(ex2.Message), EventLogEntryType.Error);
					}
					throw;
				}
				this.openType = QueueOpenType.Undefined;
				this.queueManager.RemoveOpenQueue(this.queueIndex);
				if (this.managerBelongsToQueue)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Calling the self-generated Queue Manager's Disconnect");
					}
					try
					{
						this.queueManager.Disconnect();
					}
					finally
					{
						this.queueManager = null;
					}
				}
			}
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x0017A95C File Offset: 0x00178B5C
		internal void CloseOnly()
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Queue being closed due to Queue Manager Disconnect");
				}
				if (this.openType == QueueOpenType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueNotOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				try
				{
					ReturnCode returnCode = this.queue.Close();
					if (returnCode != ReturnCode.Ok)
					{
						throw new CustomMqClientException(SR.QueueCloseFailed(this.name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
					}
					this.queue = null;
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Close Succeeded");
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
						Queue.eventLogContainer.WriteEvent(SR.CloseQueueException(ex2.Message), EventLogEntryType.Error);
					}
					throw;
				}
				this.openType = QueueOpenType.Undefined;
			}
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0017AA88 File Offset: 0x00178C88
		public void Send(SendMessage message)
		{
			object obj = this.operationLockObject;
			lock (obj)
			{
				this.cumulativePutsToUse.Increment();
				if (message == null)
				{
					ArgumentNullException ex = new ArgumentNullException("message");
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				object obj2 = message.Data == null || message.Data.Length == 0;
				bool flag2 = message.HeadersContainPcf();
				object obj3 = obj2;
				if (obj3 != null && !flag2)
				{
					ArgumentNullException ex2 = new ArgumentNullException("message.Data");
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
				if (obj3 == 0 && flag2)
				{
					ArgumentOutOfRangeException ex3 = new ArgumentOutOfRangeException("message.Data");
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex3);
					}
					throw ex3;
				}
				if (message.Data == null)
				{
					message.Data = Queue.emptyData;
				}
				if (this.openType == QueueOpenType.Undefined)
				{
					CustomMqClientException ex4 = new CustomMqClientException(SR.QueueNotOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex4);
					}
					throw ex4;
				}
				if (this.openType != QueueOpenType.Send)
				{
					CustomMqClientException ex5 = new CustomMqClientException(SR.QueueNotOpenedForSend);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex5);
					}
					throw ex5;
				}
				this.queueManager.CheckTransactions();
				if (message.ReceivedMessage != null && (message.ContextOption == ContextOption.PassAll || message.ContextOption == ContextOption.PassIdentity) && message.ReceivedMessage.ReceiveQueue.State != QueueState.OpenReceive)
				{
					CustomMqClientException ex6 = new CustomMqClientException(SR.ReceiveQueueNotAvailableForContextState);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex6);
					}
					throw ex6;
				}
				if (message.ReceivedMessage != null && (message.ContextOption == ContextOption.PassAll || message.ContextOption == ContextOption.PassIdentity))
				{
					IWrappedPooledQueueManager wrappedQueueManager = this.queueManager.WrappedQueueManager;
					IWrappedPooledQueueManager wrappedQueueManager2 = message.ReceivedMessage.ReceiveQueue.QueueManager.WrappedQueueManager;
					if (wrappedQueueManager != wrappedQueueManager2)
					{
						CustomMqClientException ex7 = new CustomMqClientException(SR.ReceiveQueueNotAvailableForContextQm);
						if (this.tracePoint.IsEnabled(TraceFlags.Error))
						{
							this.tracePoint.Trace(TraceFlags.Error, ex7);
						}
						throw ex7;
					}
				}
				if (message.Data.Length > this.maximumMessageSize)
				{
					CustomMqClientException ex8 = new CustomMqClientException(SR.QueueSendDataTooLong);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex8);
					}
					throw ex8;
				}
				if (!this.queueManager.IsConnectedAndAvailable)
				{
					CustomMqClientException ex9 = new CustomMqClientException(SR.QueueSendFailedQmDisconnected(this.queueManager.NonTracedName));
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex9);
					}
					throw ex9;
				}
				message.PrepareHeaders();
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, "Send being called, Message Options: " + message.Options.ToString());
				}
				try
				{
					AverageTimeCounter averageTimeCounter = Queue.performanceCountersContainer.GetPerformanceCounter(this.averagePutTimeToUse) as AverageTimeCounter;
					averageTimeCounter.StartOperation();
					ReturnCode returnCode = this.queue.Send(message);
					if (returnCode != ReturnCode.Ok)
					{
						throw new CustomMqClientException(SR.QueueSendFailed(this.name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
					}
					averageTimeCounter.StopOperationAndReport();
					this.putsPerSecondToUse.Increment();
					Queue.mqClientCounterTelemetry.Increment(QueueProcess.SendAndReceive);
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Send Succeeded");
					}
				}
				catch (CustomMqClientException ex10)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex10);
					}
					if (!QueueManager.CallerLogsEvents)
					{
						Queue.eventLogContainer.WriteEvent(SR.SendToQueueException(ex10.Message), EventLogEntryType.Error);
					}
					throw;
				}
			}
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0017AE68 File Offset: 0x00179068
		public ReceiveMessage Receive()
		{
			return this.InternalReceive(null, true);
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0017AE72 File Offset: 0x00179072
		public ReceiveMessage Receive(ReceiveOptions receiveOptions)
		{
			return this.InternalReceive(receiveOptions, true);
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0017AE7C File Offset: 0x0017907C
		public ReceiveMessage Receive(bool throwOn2033)
		{
			return this.InternalReceive(null, throwOn2033);
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0017AE86 File Offset: 0x00179086
		public ReceiveMessage Receive(ReceiveOptions receiveOptions, bool throwOn2033)
		{
			return this.InternalReceive(receiveOptions, throwOn2033);
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0017AE90 File Offset: 0x00179090
		private ReceiveMessage InternalReceive(ReceiveOptions receiveOptions, bool throwOn2033)
		{
			object obj = this.operationLockObject;
			ReceiveMessage receiveMessage2;
			lock (obj)
			{
				this.cumulativeGetsToUse.Increment();
				if (this.openType == QueueOpenType.Undefined)
				{
					CustomMqClientException ex = new CustomMqClientException(SR.QueueNotOpened);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex);
					}
					throw ex;
				}
				if (this.openType != QueueOpenType.Receive)
				{
					CustomMqClientException ex2 = new CustomMqClientException(SR.QueueNotOpenedForReceive);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
				if (!this.openAllowsReceiveWithoutOptions && receiveOptions == null)
				{
					CustomMqClientException ex3 = new CustomMqClientException(SR.OpenDoesntAllowReceiveWithoutOptions);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex3);
					}
					throw ex3;
				}
				if (receiveOptions != null && (this.options & OpenOption.Browse) != OpenOption.Browse && ((receiveOptions.Options & ReceiveOption.BrowseFirst) == ReceiveOption.BrowseFirst || (receiveOptions.Options & ReceiveOption.BrowseNext) == ReceiveOption.BrowseNext || (receiveOptions.Options & ReceiveOption.BrowseMessageUnderCursor) == ReceiveOption.BrowseMessageUnderCursor))
				{
					Exception ex4 = new CustomMqClientException(SR.QueueReceiveFailed(this.name, ExceptionHelpers.FormatReturnCode((ReturnCode)2036)), 2036);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex4);
					}
					throw ex4;
				}
				this.queueManager.CheckTransactions();
				if (receiveOptions != null && receiveOptions.Wait && receiveOptions.Timeout == 0)
				{
					CustomMqClientException ex5 = new CustomMqClientException(SR.ReceiveWaitWith0Timeout);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex5);
					}
					throw ex5;
				}
				if (!this.queueManager.IsConnectedAndAvailable)
				{
					CustomMqClientException ex6 = new CustomMqClientException(SR.QueueReceiveFailedQmDisconnected(this.queueManager.NonTracedName));
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex6);
					}
					throw ex6;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					if (receiveOptions != null)
					{
						this.tracePoint.Trace(TraceFlags.Information, "Receive being called, Message Options: " + receiveOptions.ToString());
					}
					else
					{
						this.tracePoint.Trace(TraceFlags.Information, "Receive being called, No Options");
					}
				}
				ReceiveMessage receiveMessage;
				try
				{
					AverageTimeCounter averageTimeCounter = Queue.performanceCountersContainer.GetPerformanceCounter(this.averageGetTimeToUse) as AverageTimeCounter;
					averageTimeCounter.StartOperation();
					object obj2;
					ReturnCode returnCode = this.queue.Receive(receiveOptions, out obj2);
					if (returnCode == (ReturnCode)2033 && !throwOn2033)
					{
						receiveMessage = null;
					}
					else
					{
						if (returnCode != ReturnCode.Ok)
						{
							throw new CustomMqClientException(SR.QueueReceiveFailed(this.Name, ExceptionHelpers.FormatReturnCode(returnCode)), (int)returnCode);
						}
						receiveMessage = (ReceiveMessage)obj2;
						receiveMessage.ReceiveQueue = this;
					}
					averageTimeCounter.StopOperationAndReport();
					this.getsPerSecondToUse.Increment();
					Queue.mqClientCounterTelemetry.Increment(QueueProcess.SendAndReceive);
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, "Receive Succeeded");
					}
				}
				catch (CustomMqClientException ex7)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex7);
					}
					if (ex7.ReasonCode != 2033 && !QueueManager.CallerLogsEvents)
					{
						Queue.eventLogContainer.WriteEvent(SR.ReceiveFromQueueException(ex7.Message), EventLogEntryType.Error);
					}
					throw;
				}
				receiveMessage2 = receiveMessage;
			}
			return receiveMessage2;
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x0017B1D4 File Offset: 0x001793D4
		internal virtual void CheckTransactions(QueueManager queueManager)
		{
			if (queueManager is TransactionalQueueManager)
			{
				ArgumentException ex = new ArgumentException(SR.NonTransactionalQNeedsNonTransactionalQm, "queueManager");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
		}

		// Token: 0x0400480F RID: 18447
		private static MqClientPerformanceCountersContainer performanceCountersContainer = new MqClientPerformanceCountersContainer("MqClient" + Process.GetCurrentProcess().Id.ToString("D5"));

		// Token: 0x04004810 RID: 18448
		private static CumulativeCounter cumulativePuts = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.CumulativePuts) as CumulativeCounter;

		// Token: 0x04004811 RID: 18449
		private static CumulativeCounter cumulativeGets = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.CumulativeGets) as CumulativeCounter;

		// Token: 0x04004812 RID: 18450
		private static PerSecondCounter putsPerSecond = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.PutsPerSecond) as PerSecondCounter;

		// Token: 0x04004813 RID: 18451
		private static PerSecondCounter getsPerSecond = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.GetsPerSecond) as PerSecondCounter;

		// Token: 0x04004814 RID: 18452
		private static CumulativeCounter cumulativeTransactionalPuts = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.CumulativeTransactionalPuts) as CumulativeCounter;

		// Token: 0x04004815 RID: 18453
		private static CumulativeCounter cumulativeTransactionalGets = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.CumulativeTransactionalGets) as CumulativeCounter;

		// Token: 0x04004816 RID: 18454
		private static PerSecondCounter transactionalPutsPerSecond = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.TransactionalPutsPerSecond) as PerSecondCounter;

		// Token: 0x04004817 RID: 18455
		private static PerSecondCounter transactionalGetsPerSecond = Queue.performanceCountersContainer.GetPerformanceCounter(MqClientPerformanceCounter.TransactionalGetsPerSecond) as PerSecondCounter;

		// Token: 0x04004818 RID: 18456
		private CumulativeCounter cumulativePutsToUse;

		// Token: 0x04004819 RID: 18457
		private CumulativeCounter cumulativeGetsToUse;

		// Token: 0x0400481A RID: 18458
		private PerSecondCounter putsPerSecondToUse;

		// Token: 0x0400481B RID: 18459
		private PerSecondCounter getsPerSecondToUse;

		// Token: 0x0400481C RID: 18460
		private MqClientPerformanceCounter averagePutTimeToUse;

		// Token: 0x0400481D RID: 18461
		private MqClientPerformanceCounter averageGetTimeToUse;

		// Token: 0x0400481E RID: 18462
		private static IPooling iPooling = Globals.GetIPooling();

		// Token: 0x0400481F RID: 18463
		private string name;

		// Token: 0x04004820 RID: 18464
		private QueueManager queueManager;

		// Token: 0x04004821 RID: 18465
		private static MqClientRuntimeAdministration runtimeAdministration = MqClientRuntimeAdministration.GetAdministration();

		// Token: 0x04004822 RID: 18466
		private QueueOpenType openType;

		// Token: 0x04004823 RID: 18467
		protected ApiTracePoint tracePoint;

		// Token: 0x04004824 RID: 18468
		private static EventLogContainer eventLogContainer = Queue.iPooling.EventLogContainer;

		// Token: 0x04004825 RID: 18469
		private static MqClientCounterTelemetryContainer mqClientCounterTelemetry = new MqClientCounterTelemetryContainer();

		// Token: 0x04004826 RID: 18470
		private int queueIndex;

		// Token: 0x04004827 RID: 18471
		private bool openAllowsReceiveWithoutOptions;

		// Token: 0x04004828 RID: 18472
		private object operationLockObject;

		// Token: 0x04004829 RID: 18473
		private static byte[] emptyData = new byte[0];

		// Token: 0x0400482A RID: 18474
		private int maximumMessageSize;

		// Token: 0x0400482B RID: 18475
		private bool managerBelongsToQueue;

		// Token: 0x0400482C RID: 18476
		private OpenOption options;

		// Token: 0x0400482D RID: 18477
		private IPooledQueue queue;

		// Token: 0x04004830 RID: 18480
		private MqTraceContainer traceContainer;
	}
}
