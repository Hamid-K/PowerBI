using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Eventing;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Eventing.Etw;
using Microsoft.Cloud.Platform.Eventing.Implementation;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Security;
using Microsoft.Cloud.Platform.Tracing;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003E9 RID: 1001
	public class ElementApplicationRoot : ApplicationRoot
	{
		// Token: 0x06001EBF RID: 7871 RVA: 0x000735C0 File Offset: 0x000717C0
		protected ElementApplicationRoot(string name)
			: base(name)
		{
			TraceSourceBase<CommonTrace>.Tracer.Trace(TraceVerbosity.Info, "ElementApplicationRoot({0}) created.", new object[] { name });
			this.m_eventsKitMock = new VoidEventsKitFactory().CreateEventsKit<IApplicationRootEvents>();
			this.m_eventsKit = this.m_eventsKitMock;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00073615 File Offset: 0x00071815
		internal ElementApplicationRoot()
			: base("InternalConstructor")
		{
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00073630 File Offset: 0x00071830
		protected override void OnInitialize()
		{
			base.OnInitialize();
			IBlock[] elementApplicationRootBlocks = this.GetElementApplicationRootBlocks();
			this.m_elementInstanceIdProvider = elementApplicationRootBlocks.OfType<IElementInstanceId>().First<IElementInstanceId>();
			this.m_eventsKitFactory = elementApplicationRootBlocks.OfType<IEventsKitFactory>().First<IEventsKitFactory>();
			base.AddBlocks(elementApplicationRootBlocks);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00073674 File Offset: 0x00071874
		internal IBlock[] GetElementApplicationRootBlocks()
		{
			return new IBlock[]
			{
				new ElementInstanceIdProvider(),
				new CommunicationServicesBlock(),
				new SecretManager(),
				new RouterFactory(),
				new PackageManager(),
				new LocalDirectoriesManager(),
				new EtwSessionsManager(),
				new EventsKitFactory(),
				new EventingServer(),
				new EventsKitExplorerFactory(),
				new OnBehalfOfEventService(),
				new MonitoredActivityCompletionModelFactory(),
				new TraceSourceConfigurationProvider(),
				new MonitoredTaskScheduler(),
				new DotNetController()
			};
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00073708 File Offset: 0x00071908
		protected override void OnBlockInitializationComplete(IBlock block)
		{
			if (block == this.m_eventsKitFactory)
			{
				this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<IApplicationRootEvents>();
				Process currentProcess = Process.GetCurrentProcess();
				this.m_eventsKit.FireApplicationProcessId(currentProcess.Id);
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string text = string.Join(";", from addr in NetworkInterface.GetAllNetworkInterfaces().SelectMany((NetworkInterface ni) => ni.GetIPProperties().UnicastAddresses)
					select addr.Address.ToString());
				this.m_eventsKit.FireApplicationStartInformationEvent(Environment.MachineName, Environment.OSVersion.ToString(), currentProcess.Id, currentProcess.ProcessName, Environment.Version.ToString(), (entryAssembly != null) ? entryAssembly.Location : "<no entry assembly>", (entryAssembly != null) ? entryAssembly.FullName : "<no entry assembly>", currentProcess.MainModule.FileVersionInfo.ToString(), text);
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00073814 File Offset: 0x00071A14
		protected override void OnPostInitialize()
		{
			base.OnPostInitialize();
			RequestedBlockService requestedBlockService = new RequestedBlockService(null, typeof(IElementInstanceId), BlockServiceProviderIdentity.Default, null);
			using (BlockServiceTicket blockServiceTicket = this.TryGetService(requestedBlockService))
			{
				this.m_elementInstanceIdProvider = (IElementInstanceId)blockServiceTicket.GetService();
				string elementInstanceId = this.ElementInstanceId;
				TraceSourceBase<CommonTrace>.Tracer.Trace(TraceVerbosity.Info, "ElementInstanceId({0}) completed initialization", new object[] { elementInstanceId });
			}
			this.m_eventsKit.FireApplicationStateChangeCompletedEvent(BlockState.Uninitialized, BlockState.Initialized);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000738A0 File Offset: 0x00071AA0
		protected override void OnPostStart()
		{
			base.OnPostStart();
			this.m_eventsKit.FireApplicationStateChangeCompletedEvent(BlockState.Initialized, BlockState.Started);
			this.m_eventsKit.FireApplicationStartedEvent(Convert.ToInt64(DateTime.Now.Subtract(Process.GetCurrentProcess().StartTime).TotalMilliseconds));
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000738EF File Offset: 0x00071AEF
		protected override void OnPostStop()
		{
			base.OnPostStop();
			this.m_eventsKit.FireApplicationStateChangeCompletedEvent(BlockState.Started, BlockState.Stopping);
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00073904 File Offset: 0x00071B04
		protected override void OnPostWaitForStopToComplete()
		{
			base.OnPostWaitForStopToComplete();
			this.m_eventsKit.FireApplicationStateChangeCompletedEvent(BlockState.Stopping, BlockState.Stopped);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00073919 File Offset: 0x00071B19
		protected override void OnRequestShutdown(IBlock requestor, int returnCode)
		{
			this.m_shutdownTimer = Stopwatch.StartNew();
			this.m_eventsKit.FireApplicationShutdownRequestedEvent(base.GetBlockHostStateForDebugging(), (requestor == null) ? "Requested by user" : requestor.Name, returnCode);
			base.OnRequestShutdown(requestor, returnCode);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00073950 File Offset: 0x00071B50
		protected override void OnPostShutdown()
		{
			if (this.m_shutdownTimer != null)
			{
				this.m_shutdownTimer.Stop();
				this.m_eventsKit.FireApplicationShutdownCompletedEvent(this.m_shutdownTimer.ElapsedMilliseconds);
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0007397B File Offset: 0x00071B7B
		protected override void OnStateChangeFailed(BlockState stateBefore, BlockState stateDesired, string blockName, Exception exception)
		{
			this.m_eventsKit.FireApplicationStateChangeFailedEvent(stateBefore, stateDesired, blockName, exception.Message);
			base.OnStateChangeFailed(stateBefore, stateDesired, blockName, exception);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000739A0 File Offset: 0x00071BA0
		protected override void OnCrash(object sender, CrashEventArgs args)
		{
			Exception exceptionObject = args.ExceptionObject;
			string text = ((exceptionObject != null) ? exceptionObject.ToString() : "(No exception object given)");
			if (string.IsNullOrEmpty(exceptionObject.StackTrace))
			{
				text = text + Environment.NewLine + ExtendedEnvironment.CrashStackTrace;
			}
			string text2 = this.m_elementInstanceId;
			if (string.IsNullOrEmpty(text2))
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					text2 = currentProcess.ProcessName;
				}
			}
			if (this.m_eventsKit == this.m_eventsKitMock)
			{
				string text3 = string.Format(CultureInfo.CurrentCulture, "Unhandled exception in {0} due to {1}", new object[] { text2, text });
				new WindowsEventLogWriter(WindowsEventLogConstants.DefaultEventLogSourceName).WriteEntry(text3, EventLogEntryType.Error, 500);
			}
			if (this.m_eventsKit != null)
			{
				int num = 24576;
				foreach (string text4 in ExtendedText.SplitTextByLength(text, num - text2.Length))
				{
					this.m_eventsKit.FireApplicationUnhandledExceptionEvent(text4, text2);
				}
			}
			base.OnCrash(sender, args);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00073AC8 File Offset: 0x00071CC8
		protected override ActivityFactory GetActivityFactoryInstance()
		{
			return new MonitoredActivityFactory();
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x00073ACF File Offset: 0x00071CCF
		public string ElementInstanceId
		{
			get
			{
				if (this.m_elementInstanceId == null)
				{
					this.DetermineElementInstance();
				}
				return this.m_elementInstanceId;
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00073AE8 File Offset: 0x00071CE8
		private void DetermineElementInstance()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				if (this.m_elementInstanceId == null)
				{
					this.m_elementInstanceId = this.m_elementInstanceIdProvider.GetElementInstanceId().ToString();
				}
			}
		}

		// Token: 0x04000AD4 RID: 2772
		private IElementInstanceId m_elementInstanceIdProvider;

		// Token: 0x04000AD5 RID: 2773
		private IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000AD6 RID: 2774
		private IApplicationRootEvents m_eventsKit;

		// Token: 0x04000AD7 RID: 2775
		private readonly IApplicationRootEvents m_eventsKitMock;

		// Token: 0x04000AD8 RID: 2776
		private string m_elementInstanceId;

		// Token: 0x04000AD9 RID: 2777
		private readonly object m_locker = new object();

		// Token: 0x04000ADA RID: 2778
		private const string c_noEntryAssembly = "<no entry assembly>";

		// Token: 0x04000ADB RID: 2779
		private Stopwatch m_shutdownTimer;
	}
}
