using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Xml;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E1 RID: 225
	internal sealed class TraceEventsReader : IDisposable
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00072ECB File Offset: 0x000710CB
		public bool IsWorking
		{
			get
			{
				return this.eventsReader != null;
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00072ED8 File Offset: 0x000710D8
		public void Start(ITrace trace, TraceEventHandler eventHandler, TraceStoppedEventHandler stopEventHandler)
		{
			object obj = this.@lock;
			lock (obj)
			{
				TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
				if (traceReaderStatus - TraceEventsReader.TraceReaderStatus.Starting <= 4)
				{
					throw new InvalidOperationException(SR.TraceEventsReader_AlreadyStarted);
				}
				if (traceReaderStatus == TraceEventsReader.TraceReaderStatus.Closed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				this.trace = trace;
				this.server = trace.Parent;
				this.eventHandler = eventHandler;
				this.stopEventHandler = stopEventHandler;
				this.isSessionTrace = trace is SessionTrace;
				this.status = TraceEventsReader.TraceReaderStatus.Starting;
			}
			this.StartImpl((trace is Trace) ? string.Format("AMO Trace [{0}]", ((Trace)trace).Name) : "AMO Session Trace", ThreadPriority.Normal);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00072FA4 File Offset: 0x000711A4
		public void Start(Server server, Action<TraceEventArgs> eventDispatcher)
		{
			object obj = this.@lock;
			lock (obj)
			{
				TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
				if (traceReaderStatus - TraceEventsReader.TraceReaderStatus.Starting <= 4)
				{
					throw new InvalidOperationException(SR.TraceEventsReader_AlreadyStarted);
				}
				if (traceReaderStatus == TraceEventsReader.TraceReaderStatus.Closed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				this.server = server;
				this.eventDispatcher = eventDispatcher;
				this.isSessionTrace = true;
				this.status = TraceEventsReader.TraceReaderStatus.Starting;
			}
			this.StartImpl("AMO Transactions Errors Tracking Trace", ThreadPriority.AboveNormal);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00073038 File Offset: 0x00071238
		public void Stop()
		{
			if (!this.Disconnect(true))
			{
				throw new AmoException(SR.TraceEventsReader_FailedStop);
			}
			try
			{
				if (this.eventsReader != null)
				{
					this.eventsReader.Join();
				}
			}
			finally
			{
				this.eventsReader = null;
			}
			if (this.stopEventHandler != null)
			{
				this.stopEventHandler(this.trace, new TraceStoppedEventArgs(TraceStopCause.StoppedByUser, null));
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000730A8 File Offset: 0x000712A8
		public void Dispose()
		{
			if (this.status == TraceEventsReader.TraceReaderStatus.Closed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.status > TraceEventsReader.TraceReaderStatus.Created)
			{
				if (this.status != TraceEventsReader.TraceReaderStatus.Stopped && this.status != TraceEventsReader.TraceReaderStatus.Error)
				{
					this.Disconnect(false);
				}
				if (this.eventsReader != null)
				{
					this.eventsReader.Join();
				}
			}
			this.status = TraceEventsReader.TraceReaderStatus.Closed;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00073114 File Offset: 0x00071314
		private static string GetTraceSubscriptionId(Server server)
		{
			if (!TraceEventsReader.IsServerSupportTraceUnsubscribe(server))
			{
				return null;
			}
			object obj = TraceEventsReader.subscriptionIdLock;
			Guid guid;
			lock (obj)
			{
				do
				{
					guid = Guid.NewGuid();
					if (guid == TraceEventsReader.lastSubscriptionId)
					{
						Thread.Sleep(1);
					}
				}
				while (guid == TraceEventsReader.lastSubscriptionId);
				TraceEventsReader.lastSubscriptionId = guid;
			}
			return string.Format(CultureInfo.InvariantCulture, "TRACE_SUBSCRIPTION_{0}", guid.ToString("D"));
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000731A0 File Offset: 0x000713A0
		private static bool IsServerSupportTraceUnsubscribe(Server server)
		{
			if (server == null)
			{
				return false;
			}
			string version = server.Version;
			if (string.IsNullOrEmpty(version))
			{
				return false;
			}
			bool flag;
			try
			{
				flag = TraceEventsReader.MINIMAL_SERVER_VERSION_FOR_UNSUBSCRIBE_SUPPORT.CompareTo(new Version(version)) <= 0;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000731F4 File Offset: 0x000713F4
		private void StartImpl(string name, ThreadPriority priority)
		{
			this.eventsReader = new Thread(TraceEventsReader.onRettrieveEvents);
			this.eventsReader.Name = name;
			if (priority != ThreadPriority.Normal)
			{
				this.eventsReader.Priority = priority;
			}
			this.eventsReader.Start(this);
			bool flag = false;
			Monitor.Enter(this.@lock, ref flag);
			try
			{
				if (flag)
				{
					if (this.status == TraceEventsReader.TraceReaderStatus.Starting || this.status == TraceEventsReader.TraceReaderStatus.Connecting)
					{
						flag = Monitor.Wait(this.@lock, 30000);
						if (!flag)
						{
							return;
						}
					}
					TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
					if (traceReaderStatus - TraceEventsReader.TraceReaderStatus.Connected > 1)
					{
						if (traceReaderStatus == TraceEventsReader.TraceReaderStatus.Error)
						{
							throw new AmoException(SR.TraceEventsReader_FailedStart);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.@lock);
				}
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000732B4 File Offset: 0x000714B4
		private void RetrieveEvents()
		{
			TraceStoppedEventArgs traceStoppedEventArgs = null;
			bool flag = false;
			try
			{
				lock (this.@lock)
				{
					TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
					if (traceReaderStatus != TraceEventsReader.TraceReaderStatus.Starting)
					{
						if (traceReaderStatus == TraceEventsReader.TraceReaderStatus.Stopping)
						{
							this.status = TraceEventsReader.TraceReaderStatus.Stopped;
							flag = true;
							Monitor.PulseAll(this.@lock);
						}
						return;
					}
					this.status = TraceEventsReader.TraceReaderStatus.Connecting;
				}
				bool flag2 = false;
				ConnectionInfo connectionInfo = this.server.ConnectionInfo.CloneForTraceChannel();
				this.asClient = new AnalysisServicesClient(this.server);
				if (this.isSessionTrace)
				{
					this.asClient.Connect(connectionInfo, this.server.SessionID);
				}
				else
				{
					this.asClient.Connect(connectionInfo, false);
				}
				lock (this.@lock)
				{
					TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
					if (traceReaderStatus != TraceEventsReader.TraceReaderStatus.Connecting)
					{
						if (traceReaderStatus == TraceEventsReader.TraceReaderStatus.Stopping)
						{
							this.status = TraceEventsReader.TraceReaderStatus.Stopped;
							Monitor.PulseAll(this.@lock);
						}
						return;
					}
					this.status = TraceEventsReader.TraceReaderStatus.Connected;
					Monitor.PulseAll(this.@lock);
				}
				string traceSubscriptionId = TraceEventsReader.GetTraceSubscriptionId(this.server);
				bool flag3 = false;
				do
				{
					XmlReader xmlReader = this.asClient.Subscribe(this.isSessionTrace ? null : ((Trace)this.trace).ID, traceSubscriptionId);
					flag3 = false;
					Hashtable hashtable = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					StringDictionary stringDictionary = new StringDictionary();
					XmlaResult xmlaResult = new XmlaResult();
					xmlReader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
					xmlReader.ReadStartElement("return");
					xmlReader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
					xmlReader.ReadStartElement("schema", "http://www.w3.org/2001/XMLSchema");
					xmlReader.MoveToContent();
					while (xmlReader.ReadState != ReadState.Closed && string.Compare(xmlReader.GetAttribute("name"), "row", false, CultureInfo.InvariantCulture) != 0)
					{
						xmlReader.Skip();
						xmlReader.MoveToContent();
					}
					if (xmlReader.ReadState == ReadState.Closed)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						xmlReader.ReadStartElement("complexType", "http://www.w3.org/2001/XMLSchema");
						xmlReader.ReadStartElement("sequence", "http://www.w3.org/2001/XMLSchema");
						foreach (string text in Enum.GetNames(typeof(TraceColumn)))
						{
							stringDictionary.Add(text, null);
						}
						while (xmlReader.ReadState != ReadState.Closed && xmlReader.IsStartElement("element", "http://www.w3.org/2001/XMLSchema"))
						{
							string attribute = xmlReader.GetAttribute("field", "urn:schemas-microsoft-com:xml-sql");
							string attribute2 = xmlReader.GetAttribute("name");
							if (stringDictionary.ContainsKey(attribute))
							{
								hashtable.Add(attribute2, Enum.Parse(typeof(TraceColumn), attribute, true));
							}
							xmlReader.Skip();
						}
						if (xmlReader.ReadState == ReadState.Closed)
						{
							flag2 = true;
						}
						if (!flag2)
						{
							xmlReader.ReadEndElement();
							xmlReader.ReadEndElement();
							flag2 = xmlReader.MoveToContent() != XmlNodeType.EndElement;
						}
					}
					lock (this.@lock)
					{
						TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
						if (traceReaderStatus != TraceEventsReader.TraceReaderStatus.Connected)
						{
							if (traceReaderStatus != TraceEventsReader.TraceReaderStatus.Stopping)
							{
								return;
							}
							this.status = TraceEventsReader.TraceReaderStatus.Stopped;
							flag = true;
							flag2 = true;
							Monitor.PulseAll(this.@lock);
						}
						else
						{
							this.subscriptionId = (flag2 ? null : traceSubscriptionId);
							this.status = (flag2 ? TraceEventsReader.TraceReaderStatus.Error : TraceEventsReader.TraceReaderStatus.Started);
						}
					}
					if (!flag2)
					{
						xmlReader.ReadEndElement();
						bool flag4 = false;
						while (xmlReader.ReadState != ReadState.Closed && xmlReader.IsStartElement("row"))
						{
							TraceEventArgs traceEventArgs = new TraceEventArgs();
							xmlaResult.Messages.Clear();
							xmlReader.ReadStartElement();
							while (xmlReader.IsStartElement())
							{
								object obj = hashtable[xmlReader.Name];
								if (obj == null)
								{
									if (xmlReader.ReadState == ReadState.Closed || xmlReader.ReadState == ReadState.Error)
									{
										break;
									}
									xmlReader.Skip();
								}
								else
								{
									if (flag4)
									{
										break;
									}
									if (xmlReader.IsEmptyElement)
									{
										xmlReader.Skip();
									}
									else
									{
										xmlReader.ReadStartElement();
										if (!XmlaClient.CheckForRowsetError(xmlReader, xmlaResult, false))
										{
											if (obj != null)
											{
												traceEventArgs[(TraceColumn)obj] = xmlReader.ReadString();
											}
											else
											{
												xmlReader.ReadString();
											}
										}
										xmlReader.ReadEndElement();
									}
								}
								if (xmlReader.ReadState == ReadState.Closed || xmlReader.ReadState == ReadState.Error)
								{
									flag4 = true;
								}
							}
							if (xmlaResult.Messages.Count > 0)
							{
								traceEventArgs.xmlaMessages = new XmlaMessageCollection();
								XmlaMessageCollection messages = xmlaResult.Messages;
								int j = 0;
								int count = messages.Count;
								while (j < count)
								{
									traceEventArgs.xmlaMessages.Add(messages[j]);
									j++;
								}
							}
							bool flag5 = traceEventArgs.Error == "-1052311437" && this.isSessionTrace;
							if (!flag5 && (this.eventHandler != null || this.eventDispatcher != null))
							{
								try
								{
									if (this.eventHandler != null)
									{
										this.eventHandler(this.GetObject(traceEventArgs, hashtable2), traceEventArgs);
									}
									if (this.eventDispatcher != null)
									{
										this.eventDispatcher(traceEventArgs);
									}
								}
								catch
								{
								}
							}
							xmlReader.ReadEndElement();
							if (flag5)
							{
								flag3 = true;
							}
						}
						XmlaClient.CheckForException(xmlReader, new XmlaResult(), true);
						xmlReader.ReadEndElement();
						if (flag3)
						{
							this.asClient.EndReceival(true);
							lock (this.@lock)
							{
								TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
								if (traceReaderStatus != TraceEventsReader.TraceReaderStatus.Started)
								{
									flag3 = traceReaderStatus != TraceEventsReader.TraceReaderStatus.Stopping && false;
								}
								else
								{
									this.status = TraceEventsReader.TraceReaderStatus.Connected;
								}
							}
						}
					}
				}
				while (flag3);
				if (!flag2)
				{
					lock (this.@lock)
					{
						TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
						if (traceReaderStatus != TraceEventsReader.TraceReaderStatus.Started)
						{
							if (traceReaderStatus == TraceEventsReader.TraceReaderStatus.Stopping)
							{
								flag = true;
								this.status = TraceEventsReader.TraceReaderStatus.Stopped;
								Monitor.PulseAll(this.@lock);
							}
						}
						else
						{
							this.status = TraceEventsReader.TraceReaderStatus.Stopped;
						}
					}
				}
				if (!flag)
				{
					traceStoppedEventArgs = new TraceStoppedEventArgs(TraceStopCause.Finished, null);
				}
			}
			catch (Exception ex)
			{
				lock (this.@lock)
				{
					switch (this.status)
					{
					case TraceEventsReader.TraceReaderStatus.Connecting:
						this.status = TraceEventsReader.TraceReaderStatus.Error;
						Monitor.PulseAll(this.@lock);
						break;
					case TraceEventsReader.TraceReaderStatus.Connected:
					case TraceEventsReader.TraceReaderStatus.Started:
						this.status = TraceEventsReader.TraceReaderStatus.Error;
						break;
					case TraceEventsReader.TraceReaderStatus.Stopping:
						flag = true;
						this.status = TraceEventsReader.TraceReaderStatus.Stopped;
						Monitor.PulseAll(this.@lock);
						break;
					}
				}
				if (!flag)
				{
					traceStoppedEventArgs = new TraceStoppedEventArgs(TraceStopCause.StoppedByException, ex);
				}
			}
			finally
			{
				if (!flag)
				{
					this.Disconnect(false);
				}
				if (traceStoppedEventArgs != null && this.stopEventHandler != null)
				{
					this.stopEventHandler(this.trace, traceStoppedEventArgs);
				}
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00073A14 File Offset: 0x00071C14
		private object GetObject(TraceEventArgs eventArgs, Hashtable jobIDsToObjects)
		{
			string objectReference = eventArgs.ObjectReference;
			string jobID = eventArgs.JobID;
			if (objectReference != null && objectReference.Length != 0)
			{
				object obj = null;
				using (XmlTextReader xmlTextReader = new XmlTextReader(objectReference, XmlNodeType.Document, null)
				{
					DtdProcessing = DtdProcessing.Prohibit
				})
				{
					xmlTextReader.MoveToContent();
					obj = ObjectReference.ResolveObjectReference(this.server, ObjectReference.Deserialize(xmlTextReader.ReadInnerXml(), false), false);
					xmlTextReader.Close();
				}
				if (obj != null && jobID != null)
				{
					jobIDsToObjects[jobID] = obj;
				}
				return obj;
			}
			if (jobID == null || jobID.Length == 0)
			{
				return null;
			}
			return jobIDsToObjects[jobID];
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00073AB4 File Offset: 0x00071CB4
		private bool Disconnect(bool isStopping)
		{
			bool flag;
			bool flag2;
			lock (this.@lock)
			{
				if (isStopping && this.status == TraceEventsReader.TraceReaderStatus.Closed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				flag = this.status == TraceEventsReader.TraceReaderStatus.Connected || this.status == TraceEventsReader.TraceReaderStatus.Started || (!isStopping && (this.status == TraceEventsReader.TraceReaderStatus.Stopped || this.status == TraceEventsReader.TraceReaderStatus.Error));
				flag2 = isStopping && this.status == TraceEventsReader.TraceReaderStatus.Started && !string.IsNullOrEmpty(this.subscriptionId);
				if (this.status <= TraceEventsReader.TraceReaderStatus.Started)
				{
					this.status = TraceEventsReader.TraceReaderStatus.Stopping;
				}
			}
			if (flag && this.asClient != null)
			{
				AnalysisServicesClient analysisServicesClient = this.asClient;
				try
				{
					if (flag2)
					{
						analysisServicesClient.Unsubscribe(this.subscriptionId);
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					analysisServicesClient.Disconnect(false);
				}
			}
			bool flag3 = false;
			Monitor.Enter(this.@lock, ref flag3);
			try
			{
				if (isStopping && this.status == TraceEventsReader.TraceReaderStatus.Stopping)
				{
					flag3 = Monitor.Wait(this.@lock, 30000);
					if (!flag3)
					{
						return false;
					}
				}
				TraceEventsReader.TraceReaderStatus traceReaderStatus = this.status;
				if (traceReaderStatus - TraceEventsReader.TraceReaderStatus.Stopped > 1)
				{
					return false;
				}
				if (!isStopping || this.status != TraceEventsReader.TraceReaderStatus.Error)
				{
					this.trace = null;
					this.server = null;
					this.asClient = null;
				}
			}
			finally
			{
				if (flag3)
				{
					Monitor.Exit(this.@lock);
				}
			}
			return true;
		}

		// Token: 0x040001B5 RID: 437
		private const string PFE_PBIDEDICATED_SESSION_MOVED = "-1052311437";

		// Token: 0x040001B6 RID: 438
		private static readonly Version MINIMAL_SERVER_VERSION_FOR_UNSUBSCRIBE_SUPPORT = new Version(15, 0, 2);

		// Token: 0x040001B7 RID: 439
		private static Guid lastSubscriptionId = Guid.Empty;

		// Token: 0x040001B8 RID: 440
		private static object subscriptionIdLock = new object();

		// Token: 0x040001B9 RID: 441
		private static ParameterizedThreadStart onRettrieveEvents = delegate(object ter)
		{
			((TraceEventsReader)ter).RetrieveEvents();
		};

		// Token: 0x040001BA RID: 442
		private ITrace trace;

		// Token: 0x040001BB RID: 443
		private Server server;

		// Token: 0x040001BC RID: 444
		private Thread eventsReader;

		// Token: 0x040001BD RID: 445
		private AnalysisServicesClient asClient;

		// Token: 0x040001BE RID: 446
		private TraceEventHandler eventHandler;

		// Token: 0x040001BF RID: 447
		private TraceStoppedEventHandler stopEventHandler;

		// Token: 0x040001C0 RID: 448
		private Action<TraceEventArgs> eventDispatcher;

		// Token: 0x040001C1 RID: 449
		private bool isSessionTrace;

		// Token: 0x040001C2 RID: 450
		private string subscriptionId;

		// Token: 0x040001C3 RID: 451
		private TraceEventsReader.TraceReaderStatus status;

		// Token: 0x040001C4 RID: 452
		private object @lock = new object();

		// Token: 0x020002F0 RID: 752
		private enum TraceReaderStatus
		{
			// Token: 0x04000ACB RID: 2763
			Created,
			// Token: 0x04000ACC RID: 2764
			Starting,
			// Token: 0x04000ACD RID: 2765
			Connecting,
			// Token: 0x04000ACE RID: 2766
			Connected,
			// Token: 0x04000ACF RID: 2767
			Started,
			// Token: 0x04000AD0 RID: 2768
			Stopping,
			// Token: 0x04000AD1 RID: 2769
			Stopped,
			// Token: 0x04000AD2 RID: 2770
			Error,
			// Token: 0x04000AD3 RID: 2771
			Closed
		}
	}
}
