using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000293 RID: 659
	public static class ExtendedServices
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x0003DA74 File Offset: 0x0003BC74
		[CanBeNull]
		public static Process GetProcessOfWindowsService(ServiceController serviceController)
		{
			ExtendedServices.ServiceStatus serviceStatus = ExtendedServices.QueryService(serviceController);
			if (serviceStatus.processId == 0)
			{
				return null;
			}
			int i = 10;
			while (i > 0)
			{
				i--;
				Process processById = Process.GetProcessById(serviceStatus.processId);
				using (DisposeController disposeController = new DisposeController(processById))
				{
					serviceStatus = ExtendedServices.QueryService(serviceController);
					if (serviceStatus.processId == processById.Id)
					{
						disposeController.PreventDispose();
						return processById;
					}
				}
			}
			return null;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0003DAF4 File Offset: 0x0003BCF4
		public static int KillWindowsService(ServiceController serviceController)
		{
			using (Process processOfWindowsService = ExtendedServices.GetProcessOfWindowsService(serviceController))
			{
				if (processOfWindowsService != null)
				{
					processOfWindowsService.Kill();
					return processOfWindowsService.Id;
				}
			}
			throw new InvalidOperationException("Could not allocate process Id of service " + serviceController.ServiceName + ". Service is not running");
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003DB54 File Offset: 0x0003BD54
		public static int KillWindowsService(string windowsServiceName)
		{
			int num;
			using (ServiceController serviceController = new ServiceController(windowsServiceName))
			{
				num = ExtendedServices.KillWindowsService(serviceController);
			}
			return num;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003DB8C File Offset: 0x0003BD8C
		private static ExtendedServices.ServiceStatus QueryService(ServiceController sc)
		{
			IntPtr intPtr = sc.ServiceHandle.DangerousGetHandle();
			ExtendedServices.NativeMethods.SERVICE_STATUS_PROCESS service_STATUS_PROCESS = new ExtendedServices.NativeMethods.SERVICE_STATUS_PROCESS();
			GCHandle gchandle = GCHandle.Alloc(service_STATUS_PROCESS, GCHandleType.Pinned);
			ExtendedServices.ServiceStatus serviceStatus;
			try
			{
				int num = 0;
				if (ExtendedServices.NativeMethods.QueryServiceStatusEx(intPtr, ExtendedServices.NativeMethods.SC_STATUS_PROCESS_INFO, gchandle.AddrOfPinnedObject(), Marshal.SizeOf<ExtendedServices.NativeMethods.SERVICE_STATUS_PROCESS>(service_STATUS_PROCESS), out num) == 0)
				{
					throw new InvalidOperationException("Query service " + sc.ServiceName + " failed");
				}
				serviceStatus = new ExtendedServices.ServiceStatus
				{
					processId = service_STATUS_PROCESS.processID
				};
			}
			finally
			{
				gchandle.Free();
			}
			return serviceStatus;
		}

		// Token: 0x0200073F RID: 1855
		internal struct ServiceStatus
		{
			// Token: 0x04001566 RID: 5478
			internal int processId;
		}

		// Token: 0x02000740 RID: 1856
		private static class NativeMethods
		{
			// Token: 0x06002FE7 RID: 12263
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern int QueryServiceStatusEx(IntPtr serviceHandle, int infoLevel, IntPtr buffer, int bufferSize, out int bytesNeeded);

			// Token: 0x04001567 RID: 5479
			internal static int SC_STATUS_PROCESS_INFO;

			// Token: 0x0200087D RID: 2173
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal class SERVICE_STATUS_PROCESS
			{
				// Token: 0x040019FB RID: 6651
				public int serviceType;

				// Token: 0x040019FC RID: 6652
				public int currentState;

				// Token: 0x040019FD RID: 6653
				public int controlsAccepted;

				// Token: 0x040019FE RID: 6654
				public int win32ExitCode;

				// Token: 0x040019FF RID: 6655
				public int serviceSpecificExitCode;

				// Token: 0x04001A00 RID: 6656
				public int checkPoint;

				// Token: 0x04001A01 RID: 6657
				public int waitHint;

				// Token: 0x04001A02 RID: 6658
				public int processID;

				// Token: 0x04001A03 RID: 6659
				public int serviceFlags;
			}

			// Token: 0x0200087E RID: 2174
			internal enum ServiceState
			{
				// Token: 0x04001A05 RID: 6661
				SERVICE_STOPPED = 1,
				// Token: 0x04001A06 RID: 6662
				SERVICE_START_PENDING,
				// Token: 0x04001A07 RID: 6663
				SERVICE_STOP_PENDING,
				// Token: 0x04001A08 RID: 6664
				SERVICE_RUNNING,
				// Token: 0x04001A09 RID: 6665
				SERVICE_CONTINUE_PENDING,
				// Token: 0x04001A0A RID: 6666
				SERVICE_PAUSE_PENDING,
				// Token: 0x04001A0B RID: 6667
				SERVICE_PAUSED
			}
		}
	}
}
