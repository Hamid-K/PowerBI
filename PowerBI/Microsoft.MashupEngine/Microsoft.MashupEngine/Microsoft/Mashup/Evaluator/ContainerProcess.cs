using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Shims.Interprocess;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C5F RID: 7263
	internal class ContainerProcess : IDisposable
	{
		// Token: 0x0600B506 RID: 46342 RVA: 0x0024BCE8 File Offset: 0x00249EE8
		public ContainerProcess(EvaluatorConfiguration configuration, string arguments, ContainerJob job)
		{
			this.configuration = configuration;
			this.arguments = arguments;
			this.job = job;
			this.logFilePath = null;
			this.id = -1;
		}

		// Token: 0x17002D3D RID: 11581
		// (get) Token: 0x0600B507 RID: 46343 RVA: 0x0024BD13 File Offset: 0x00249F13
		public ContainerProcess.SafeProcessHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17002D3E RID: 11582
		// (get) Token: 0x0600B508 RID: 46344 RVA: 0x0024BD1B File Offset: 0x00249F1B
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17002D3F RID: 11583
		// (get) Token: 0x0600B509 RID: 46345 RVA: 0x0024BD23 File Offset: 0x00249F23
		public uint ExitCode
		{
			get
			{
				if (this.HasExited)
				{
					return this.exitCode.Value;
				}
				throw new InvalidOperationException("Process hasn't exited.");
			}
		}

		// Token: 0x17002D40 RID: 11584
		// (get) Token: 0x0600B50A RID: 46346 RVA: 0x0024BD43 File Offset: 0x00249F43
		public Stream HostToContainerStream
		{
			get
			{
				return this.hostToContainer;
			}
		}

		// Token: 0x17002D41 RID: 11585
		// (get) Token: 0x0600B50B RID: 46347 RVA: 0x0024BD4B File Offset: 0x00249F4B
		public Stream ContainerToHostStream
		{
			get
			{
				return this.containerToHost;
			}
		}

		// Token: 0x17002D42 RID: 11586
		// (get) Token: 0x0600B50C RID: 46348 RVA: 0x0024BD53 File Offset: 0x00249F53
		private string WorkingDirectory
		{
			get
			{
				return Path.GetDirectoryName(this.configuration.ContainerExe);
			}
		}

		// Token: 0x17002D43 RID: 11587
		// (get) Token: 0x0600B50D RID: 46349 RVA: 0x0024BD65 File Offset: 0x00249F65
		private bool UsesNativeContainerLoader
		{
			get
			{
				return Path.GetFileName(this.configuration.ContainerExe) == "Microsoft.Mashup.Container.Loader.exe";
			}
		}

		// Token: 0x0600B50E RID: 46350 RVA: 0x0024BD81 File Offset: 0x00249F81
		public void Start()
		{
			this.CreateProcess();
		}

		// Token: 0x0600B50F RID: 46351 RVA: 0x0024BD8C File Offset: 0x00249F8C
		private void WaitForContainerReady(Waitable readyEvent, Waitable exitEvent)
		{
			int num = Waitable.WaitAny(ContainerProcess.maxProcessStartTime, new Waitable[] { readyEvent, exitEvent });
			if (num < 0)
			{
				SystemException.LogSystemError("ContainerProcess/WaitForContainerReady", "Timeout expired while waiting for container to become ready.");
				this.Kill();
				num = Waitable.WaitAny(new Waitable[] { readyEvent, exitEvent });
			}
			if (num == 1)
			{
				throw this.ExitException(null);
			}
		}

		// Token: 0x0600B510 RID: 46352 RVA: 0x0024BDEC File Offset: 0x00249FEC
		public void WaitForExit()
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerProcess/WaitForExit", null, TraceEventType.Information, null))
			{
				if (!this.WaitForExit(hostTrace, (int)ContainerProcess.maxProcessExitTime.TotalMilliseconds) && !this.HasExited)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Container process did not exit within {0} milliseconds.", (int)ContainerProcess.maxProcessExitTime.TotalMilliseconds));
				}
			}
		}

		// Token: 0x0600B511 RID: 46353 RVA: 0x0024BE6C File Offset: 0x0024A06C
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.CloseProcessHandle();
				if (this.hostToContainer != null)
				{
					this.hostToContainer.Close();
				}
				if (this.containerToHost != null)
				{
					this.containerToHost.Close();
				}
				if (this.processWaitHandle != null)
				{
					this.processWaitHandle.Dispose();
				}
				if (this.logFilePath != null)
				{
					try
					{
						if (File.Exists(this.logFilePath))
						{
							File.Delete(this.logFilePath);
						}
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
					}
				}
				this.hostToContainer = null;
				this.containerToHost = null;
				this.processWaitHandle = null;
				this.disposed = true;
			}
		}

		// Token: 0x0600B512 RID: 46354 RVA: 0x0024BF1C File Offset: 0x0024A11C
		public string ReadContainerLog()
		{
			string text = null;
			if (this.logFilePath != null)
			{
				try
				{
					if (File.Exists(this.logFilePath))
					{
						text = File.ReadAllText(this.logFilePath);
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
			}
			return text;
		}

		// Token: 0x0600B513 RID: 46355 RVA: 0x0024BF6C File Offset: 0x0024A16C
		private bool WaitForExit(IHostTrace trace, int msTimeout)
		{
			DateTime utcNow = DateTime.UtcNow;
			bool flag = this.processWaitHandle.Wait(msTimeout);
			TimeSpan timeSpan = DateTime.UtcNow - utcNow;
			if (timeSpan > ContainerProcess.expectedProcessExitTime)
			{
				trace.Add("lengthyExitTime", timeSpan, false);
			}
			return flag;
		}

		// Token: 0x0600B514 RID: 46356 RVA: 0x0024BFB6 File Offset: 0x0024A1B6
		private Exception ExitException(Exception ex = null)
		{
			if (this.HasExited)
			{
				return new ContainerExitException(this.id, this.ExitCode, this.ReadContainerLog(), ex);
			}
			return new ContainerExitException(this.id, this.ReadContainerLog(), ex);
		}

		// Token: 0x17002D44 RID: 11588
		// (get) Token: 0x0600B515 RID: 46357 RVA: 0x0024BFEC File Offset: 0x0024A1EC
		public bool HasExited
		{
			get
			{
				if (this.exitCode != null)
				{
					return true;
				}
				uint num;
				if (!ContainerProcess.Interop.GetExitCodeProcess(this.handle, out num))
				{
					throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/GetExitCodeProcess", "Cannot get exit code.");
				}
				if (num != 259U)
				{
					this.exitCode = new uint?(num);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600B516 RID: 46358 RVA: 0x0024C040 File Offset: 0x0024A240
		public SafeHandle GetProcessHandle()
		{
			ContainerProcess.SafeProcessHandle safeProcessHandle = this.handle;
			if (!this.disposed && safeProcessHandle != null && !safeProcessHandle.IsClosed && !safeProcessHandle.IsInvalid)
			{
				return ProcessHelpers.DuplicateHandle(safeProcessHandle);
			}
			return null;
		}

		// Token: 0x0600B517 RID: 46359 RVA: 0x0024C078 File Offset: 0x0024A278
		public void Kill()
		{
			using (EvaluatorTracing.CreateTrace("ContainerProcess/Kill", null, TraceEventType.Information, null))
			{
				if (!ContainerProcess.Interop.TerminateProcess(this.handle, -467599358) && !this.HasExited)
				{
					throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/TerminateProcess", "Cannot kill process.");
				}
			}
		}

		// Token: 0x0600B518 RID: 46360 RVA: 0x0024C0DC File Offset: 0x0024A2DC
		public void SetProcessWorkingSetSize(int maxWorkingSetInMB)
		{
			UIntPtr uintPtr;
			UIntPtr uintPtr2;
			uint num;
			if (!ContainerProcess.Interop.GetProcessWorkingSetSizeEx(this.handle.Handle, out uintPtr, out uintPtr2, out num))
			{
				SystemException.LogWin32SystemError("ContainerProcess/Interop/GetProcessWorkingSetSizeEx", "Unable to get process working set size.");
				return;
			}
			ulong num2 = (ulong)(1048576L * (long)maxWorkingSetInMB);
			if (IntPtr.Size == 4 && num2 > (ulong)(-1))
			{
				throw new ArgumentOutOfRangeException("maxWorkingSetInMB");
			}
			uintPtr2 = (UIntPtr)num2;
			num = (num & 4294967287U) | 4U;
			if (!ContainerProcess.Interop.SetProcessWorkingSetSizeEx(this.handle.Handle, uintPtr, uintPtr2, num))
			{
				SystemException.LogWin32SystemError("ContainerProcess/Interop/SetProcessWorkingSetSizeEx", "Unable to set process working set size.");
			}
		}

		// Token: 0x0600B519 RID: 46361 RVA: 0x0024C168 File Offset: 0x0024A368
		private void CreateProcess()
		{
			StringBuilder stringBuilder = new StringBuilder(this.arguments);
			if (this.configuration.ContainerLogFolderPath != null)
			{
				this.logFilePath = Path.Combine(this.configuration.ContainerLogFolderPath, Guid.NewGuid().ToString() + ".log");
				stringBuilder.Append(" ");
				stringBuilder.Append("--logfile");
				stringBuilder.Append(" \"");
				stringBuilder.Append(this.logFilePath);
				stringBuilder.Append("\"");
			}
			string targetFrameworkName = this.configuration.GetTargetFrameworkName();
			if (!string.IsNullOrEmpty(targetFrameworkName))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("--targetframework");
				stringBuilder.Append(" \"");
				stringBuilder.Append(targetFrameworkName);
				stringBuilder.Append("\"");
			}
			string text = stringBuilder.ToString();
			AnonymousPipeServerStream anonymousPipeServerStream = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable, 4096);
			AnonymousPipeServerStream anonymousPipeServerStream2 = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable, 4096);
			this.containerToHost = anonymousPipeServerStream;
			this.hostToContainer = anonymousPipeServerStream2;
			string clientHandleAsString = anonymousPipeServerStream2.GetClientHandleAsString();
			using (ContainerProcess.Interop.StartupInfoEx startupInfo = new ContainerProcess.Interop.StartupInfoEx())
			{
				ContainerProcess.Interop.ProcessInformation processInfo = default(ContainerProcess.Interop.ProcessInformation);
				string commandLine = string.Concat(new string[]
				{
					"\"",
					this.configuration.ContainerExe,
					"\" ",
					clientHandleAsString,
					" ",
					anonymousPipeServerStream.GetClientHandleAsString(),
					" ",
					text
				});
				bool flag = false;
				bool flag2 = false;
				SafeHandle clientSafePipeHandle = anonymousPipeServerStream2.ClientSafePipeHandle;
				SafeHandle clientSafePipeHandle2 = anonymousPipeServerStream.ClientSafePipeHandle;
				IntPtr intPtr = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					IntPtr zero = IntPtr.Zero;
					if (!ContainerProcess.Interop.InitializeProcThreadAttributeList(IntPtr.Zero, 1, 0, ref zero) && Marshal.GetLastWin32Error() != 122)
					{
						throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/InitializeProcThreadAttributeList", "Cannot get process attribute list size.");
					}
					startupInfo.lpAttributeList = Marshal.AllocHGlobal(zero);
					if (!ContainerProcess.Interop.InitializeProcThreadAttributeList(startupInfo.lpAttributeList, 1, 0, ref zero))
					{
						throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/InitializeProcThreadAttributeList", "Cannot initialize process attribute list.");
					}
					try
					{
						clientSafePipeHandle.DangerousAddRef(ref flag);
						clientSafePipeHandle2.DangerousAddRef(ref flag2);
						int num = IntPtr.Size * 3;
						intPtr = Marshal.AllocHGlobal(num);
						Marshal.WriteIntPtr(intPtr, 0, anonymousPipeServerStream2.ClientSafePipeHandle.DangerousGetHandle());
						Marshal.WriteIntPtr(intPtr, IntPtr.Size, anonymousPipeServerStream.ClientSafePipeHandle.DangerousGetHandle());
						Marshal.WriteIntPtr(intPtr, 2 * IntPtr.Size, ProcessHelpers.ProcessHandle.DangerousGetHandle());
						if (!ContainerProcess.Interop.UpdateProcThreadAttribute(startupInfo.lpAttributeList, 0U, (IntPtr)131074, intPtr, (UIntPtr)((ulong)((long)num)), IntPtr.Zero, IntPtr.Zero))
						{
							throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/UpdateProcThreadAttribute", "Cannot update startup info attribute list.");
						}
						this.WithClientPipesAttachedEvent(clientHandleAsString, delegate
						{
							uint num2 = 168296480U;
							if (this.job.RequireProcessAttach)
							{
								num2 |= 4U;
							}
							if (!ContainerProcess.Interop.CreateProcess(null, commandLine, null, null, true, num2, IntPtr.Zero, this.WorkingDirectory, startupInfo, out processInfo) || processInfo.hProcess == IntPtr.Zero)
							{
								throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/CreateProcess", "Cannot create process.");
							}
							this.handle = new ContainerProcess.SafeProcessHandle(processInfo.hProcess);
							this.id = (int)processInfo.dwProcessId;
							if (this.job.RequireProcessAttach)
							{
								this.job.AssociateProcess(this);
								if (ContainerProcess.Interop.ResumeThread(processInfo.hThread) < 0U)
								{
									throw SystemException.CreateWin32SystemException("ContainerProcess/Interop/ResumeThread", "Cannot resume main thread");
								}
							}
							return this.handle.AsWaitable();
						});
						anonymousPipeServerStream.DisposeLocalCopyOfClientHandle();
						anonymousPipeServerStream2.DisposeLocalCopyOfClientHandle();
					}
					finally
					{
						ContainerProcess.Interop.DeleteProcThreadAttributeList(startupInfo.lpAttributeList);
					}
				}
				catch (Exception)
				{
					if (processInfo.hProcess != IntPtr.Zero && this.handle == null)
					{
						ContainerProcess.Interop.CloseHandle(processInfo.hProcess);
					}
					throw;
				}
				finally
				{
					if (flag)
					{
						clientSafePipeHandle.DangerousRelease();
					}
					if (flag2)
					{
						clientSafePipeHandle2.DangerousRelease();
					}
					if (processInfo.hThread != IntPtr.Zero)
					{
						ContainerProcess.Interop.CloseHandle(processInfo.hThread);
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}
		}

		// Token: 0x0600B51A RID: 46362 RVA: 0x0024C56C File Offset: 0x0024A76C
		private void WithClientPipesAttachedEvent(string hostToContainerPipeName, Func<Waitable> func)
		{
			using (ManualResetWaitableEvent manualResetWaitableEvent = ClientPipesAttachedEvent.Create(hostToContainerPipeName))
			{
				this.processWaitHandle = func();
				this.WaitForContainerReady(manualResetWaitableEvent, this.processWaitHandle);
			}
		}

		// Token: 0x0600B51B RID: 46363 RVA: 0x0024C5B8 File Offset: 0x0024A7B8
		private void CloseProcessHandle()
		{
			if (this.handle != null && !this.handle.IsInvalid)
			{
				this.handle.Close();
			}
			this.handle = null;
		}

		// Token: 0x04005C3A RID: 23610
		private const int pipesBufferSize = 4096;

		// Token: 0x04005C3B RID: 23611
		private const int MBInBytes = 1048576;

		// Token: 0x04005C3C RID: 23612
		private static readonly TimeSpan maxProcessStartTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x04005C3D RID: 23613
		private static readonly TimeSpan maxProcessExitTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x04005C3E RID: 23614
		private static readonly TimeSpan expectedProcessExitTime = TimeSpan.FromSeconds(1.0);

		// Token: 0x04005C3F RID: 23615
		private ContainerProcess.SafeProcessHandle handle;

		// Token: 0x04005C40 RID: 23616
		private Waitable processWaitHandle;

		// Token: 0x04005C41 RID: 23617
		private PipeStream hostToContainer;

		// Token: 0x04005C42 RID: 23618
		private PipeStream containerToHost;

		// Token: 0x04005C43 RID: 23619
		private bool disposed;

		// Token: 0x04005C44 RID: 23620
		private uint? exitCode;

		// Token: 0x04005C45 RID: 23621
		private readonly EvaluatorConfiguration configuration;

		// Token: 0x04005C46 RID: 23622
		private readonly string arguments;

		// Token: 0x04005C47 RID: 23623
		private readonly ContainerJob job;

		// Token: 0x04005C48 RID: 23624
		private string logFilePath;

		// Token: 0x04005C49 RID: 23625
		private int id;

		// Token: 0x02001C60 RID: 7264
		public class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x0600B51D RID: 46365 RVA: 0x0011064C File Offset: 0x0010E84C
			public SafeProcessHandle(IntPtr processHandle)
				: base(true)
			{
				base.SetHandle(processHandle);
			}

			// Token: 0x0600B51E RID: 46366 RVA: 0x0024C61C File Offset: 0x0024A81C
			public Waitable AsWaitable()
			{
				return new ContainerProcess.SafeProcessHandle.InteropWaitable(this.handle);
			}

			// Token: 0x17002D45 RID: 11589
			// (get) Token: 0x0600B51F RID: 46367 RVA: 0x0024AC45 File Offset: 0x00248E45
			public IntPtr Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x0600B520 RID: 46368 RVA: 0x0024C629 File Offset: 0x0024A829
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			protected override bool ReleaseHandle()
			{
				return ContainerProcess.Interop.CloseHandle(this.handle);
			}

			// Token: 0x02001C61 RID: 7265
			private class InteropWaitable : Waitable
			{
				// Token: 0x0600B521 RID: 46369 RVA: 0x0024C636 File Offset: 0x0024A836
				public InteropWaitable(IntPtr handle)
				{
					this.interopWaitHandle = new ContainerProcess.SafeProcessHandle.InteropWaitable.InteropWaitHandle(handle);
				}

				// Token: 0x17002D46 RID: 11590
				// (get) Token: 0x0600B522 RID: 46370 RVA: 0x0024C64A File Offset: 0x0024A84A
				protected override WaitHandle WaitHandle
				{
					get
					{
						return this.interopWaitHandle;
					}
				}

				// Token: 0x04005C4A RID: 23626
				private readonly ContainerProcess.SafeProcessHandle.InteropWaitable.InteropWaitHandle interopWaitHandle;

				// Token: 0x02001C62 RID: 7266
				private class InteropWaitHandle : WaitHandle
				{
					// Token: 0x0600B523 RID: 46371 RVA: 0x0024C652 File Offset: 0x0024A852
					public InteropWaitHandle(IntPtr handle)
					{
						base.SafeWaitHandle = new SafeWaitHandle(handle, false);
					}
				}
			}
		}

		// Token: 0x02001C63 RID: 7267
		[SuppressUnmanagedCodeSecurity]
		private static class Interop
		{
			// Token: 0x0600B524 RID: 46372
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, ContainerProcess.Interop.SecurityAttributes lpProcessAttributes, ContainerProcess.Interop.SecurityAttributes lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ContainerProcess.Interop.StartupInfoEx lpStartupInfo, out ContainerProcess.Interop.ProcessInformation lpProcessInformation);

			// Token: 0x0600B525 RID: 46373
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool UpdateProcThreadAttribute(IntPtr lpAttributeList, uint dwFlags, IntPtr Attribute, IntPtr lpValue, UIntPtr cbSize, IntPtr lpPreviousValue, IntPtr lpReturnSize);

			// Token: 0x0600B526 RID: 46374
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool InitializeProcThreadAttributeList(IntPtr lpAttributeList, int dwAttributeCount, int dwFlags, ref IntPtr lpSize);

			// Token: 0x0600B527 RID: 46375
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern uint ResumeThread(IntPtr hThread);

			// Token: 0x0600B528 RID: 46376
			[DllImport("kernel32.dll")]
			public static extern void DeleteProcThreadAttributeList(IntPtr lpAttributeList);

			// Token: 0x0600B529 RID: 46377
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool CloseHandle(IntPtr hObject);

			// Token: 0x0600B52A RID: 46378
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool TerminateProcess(ContainerProcess.SafeProcessHandle processHandle, int exitCode);

			// Token: 0x0600B52B RID: 46379
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool GetExitCodeProcess(ContainerProcess.SafeProcessHandle processHandle, out uint exitCode);

			// Token: 0x0600B52C RID: 46380
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool GetProcessWorkingSetSizeEx(IntPtr hProcess, out UIntPtr lpMinimumWorkingSetSize, out UIntPtr lpMaximumWorkingSetSize, out uint flags);

			// Token: 0x0600B52D RID: 46381
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool SetProcessWorkingSetSizeEx(IntPtr hProcess, UIntPtr dwMinimumWorkingSetSize, UIntPtr dwMaximumWorkingSetSize, uint flags);

			// Token: 0x04005C4B RID: 23627
			public const int ERROR_INSUFFICIENT_BUFFER = 122;

			// Token: 0x04005C4C RID: 23628
			public const int PROC_THREAD_ATTRIBUTE_HANDLE_LIST = 131074;

			// Token: 0x04005C4D RID: 23629
			public const int PROCESS_STILL_ACTIVE = 259;

			// Token: 0x04005C4E RID: 23630
			public const uint QUOTA_LIMITS_HARDWS_MIN_DISABLE = 2U;

			// Token: 0x04005C4F RID: 23631
			public const uint QUOTA_LIMITS_HARDWS_MIN_ENABLE = 1U;

			// Token: 0x04005C50 RID: 23632
			public const uint QUOTA_LIMITS_HARDWS_MAX_DISABLE = 8U;

			// Token: 0x04005C51 RID: 23633
			public const uint QUOTA_LIMITS_HARDWS_MAX_ENABLE = 4U;

			// Token: 0x02001C64 RID: 7268
			[Flags]
			internal enum CreateProcessFlags : uint
			{
				// Token: 0x04005C53 RID: 23635
				DEBUG_PROCESS = 1U,
				// Token: 0x04005C54 RID: 23636
				DEBUG_ONLY_THIS_PROCESS = 2U,
				// Token: 0x04005C55 RID: 23637
				CREATE_SUSPENDED = 4U,
				// Token: 0x04005C56 RID: 23638
				DETACHED_PROCESS = 8U,
				// Token: 0x04005C57 RID: 23639
				CREATE_NEW_CONSOLE = 16U,
				// Token: 0x04005C58 RID: 23640
				NORMAL_PRIORITY_CLASS = 32U,
				// Token: 0x04005C59 RID: 23641
				IDLE_PRIORITY_CLASS = 64U,
				// Token: 0x04005C5A RID: 23642
				HIGH_PRIORITY_CLASS = 128U,
				// Token: 0x04005C5B RID: 23643
				REALTIME_PRIORITY_CLASS = 256U,
				// Token: 0x04005C5C RID: 23644
				CREATE_NEW_PROCESS_GROUP = 512U,
				// Token: 0x04005C5D RID: 23645
				CREATE_UNICODE_ENVIRONMENT = 1024U,
				// Token: 0x04005C5E RID: 23646
				CREATE_SEPARATE_WOW_VDM = 2048U,
				// Token: 0x04005C5F RID: 23647
				CREATE_SHARED_WOW_VDM = 4096U,
				// Token: 0x04005C60 RID: 23648
				CREATE_FORCEDOS = 8192U,
				// Token: 0x04005C61 RID: 23649
				BELOW_NORMAL_PRIORITY_CLASS = 16384U,
				// Token: 0x04005C62 RID: 23650
				ABOVE_NORMAL_PRIORITY_CLASS = 32768U,
				// Token: 0x04005C63 RID: 23651
				INHERIT_PARENT_AFFINITY = 65536U,
				// Token: 0x04005C64 RID: 23652
				INHERIT_CALLER_PRIORITY = 131072U,
				// Token: 0x04005C65 RID: 23653
				CREATE_PROTECTED_PROCESS = 262144U,
				// Token: 0x04005C66 RID: 23654
				EXTENDED_STARTUPINFO_PRESENT = 524288U,
				// Token: 0x04005C67 RID: 23655
				PROCESS_MODE_BACKGROUND_BEGIN = 1048576U,
				// Token: 0x04005C68 RID: 23656
				PROCESS_MODE_BACKGROUND_END = 2097152U,
				// Token: 0x04005C69 RID: 23657
				CREATE_BREAKAWAY_FROM_JOB = 16777216U,
				// Token: 0x04005C6A RID: 23658
				CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 33554432U,
				// Token: 0x04005C6B RID: 23659
				CREATE_DEFAULT_ERROR_MODE = 67108864U,
				// Token: 0x04005C6C RID: 23660
				CREATE_NO_WINDOW = 134217728U,
				// Token: 0x04005C6D RID: 23661
				PROFILE_USER = 268435456U,
				// Token: 0x04005C6E RID: 23662
				PROFILE_KERNEL = 536870912U,
				// Token: 0x04005C6F RID: 23663
				PROFILE_SERVER = 1073741824U,
				// Token: 0x04005C70 RID: 23664
				CREATE_IGNORE_SYSTEM_DEFAULT = 2147483648U,
				// Token: 0x04005C71 RID: 23665
				Container = 168296480U
			}

			// Token: 0x02001C65 RID: 7269
			[StructLayout(LayoutKind.Sequential)]
			public class SecurityAttributes
			{
				// Token: 0x0600B52E RID: 46382 RVA: 0x0024C667 File Offset: 0x0024A867
				public SecurityAttributes()
				{
					this.nLength = (uint)Marshal.SizeOf(typeof(ContainerProcess.Interop.SecurityAttributes));
					this.lpSecurityDescriptor = IntPtr.Zero;
					this.bInheritHandle = true;
				}

				// Token: 0x04005C72 RID: 23666
				public uint nLength;

				// Token: 0x04005C73 RID: 23667
				public IntPtr lpSecurityDescriptor;

				// Token: 0x04005C74 RID: 23668
				public bool bInheritHandle;
			}

			// Token: 0x02001C66 RID: 7270
			internal struct ProcessInformation
			{
				// Token: 0x04005C75 RID: 23669
				public IntPtr hProcess;

				// Token: 0x04005C76 RID: 23670
				public IntPtr hThread;

				// Token: 0x04005C77 RID: 23671
				public uint dwProcessId;

				// Token: 0x04005C78 RID: 23672
				public uint dwThreadId;
			}

			// Token: 0x02001C67 RID: 7271
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public class StartupInfoEx : IDisposable
			{
				// Token: 0x0600B52F RID: 46383 RVA: 0x0024C698 File Offset: 0x0024A898
				public StartupInfoEx()
				{
					this.lpReserved = IntPtr.Zero;
					this.lpDesktop = IntPtr.Zero;
					this.lpTitle = IntPtr.Zero;
					this.lpReserved2 = IntPtr.Zero;
					this.hStdInput = new SafeFileHandle(IntPtr.Zero, false);
					this.hStdOutput = new SafeFileHandle(IntPtr.Zero, false);
					this.hStdError = new SafeFileHandle(IntPtr.Zero, false);
					this.cb = (uint)Marshal.SizeOf(typeof(ContainerProcess.Interop.StartupInfoEx));
					this.lpAttributeList = IntPtr.Zero;
				}

				// Token: 0x0600B530 RID: 46384 RVA: 0x0024C72C File Offset: 0x0024A92C
				public void Dispose()
				{
					if (this.hStdInput != null && !this.hStdInput.IsInvalid)
					{
						this.hStdInput.Close();
					}
					if (this.hStdOutput != null && !this.hStdOutput.IsInvalid)
					{
						this.hStdOutput.Close();
					}
					if (this.hStdError != null && !this.hStdError.IsInvalid)
					{
						this.hStdError.Close();
					}
					if (this.lpAttributeList != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.lpAttributeList);
						this.lpAttributeList = IntPtr.Zero;
					}
					this.hStdInput = null;
					this.hStdOutput = null;
					this.hStdError = null;
				}

				// Token: 0x04005C79 RID: 23673
				public uint cb;

				// Token: 0x04005C7A RID: 23674
				public IntPtr lpReserved;

				// Token: 0x04005C7B RID: 23675
				public IntPtr lpDesktop;

				// Token: 0x04005C7C RID: 23676
				public IntPtr lpTitle;

				// Token: 0x04005C7D RID: 23677
				public uint dwX;

				// Token: 0x04005C7E RID: 23678
				public uint dwY;

				// Token: 0x04005C7F RID: 23679
				public uint dwXSize;

				// Token: 0x04005C80 RID: 23680
				public uint dwYSize;

				// Token: 0x04005C81 RID: 23681
				public uint dwXCountChars;

				// Token: 0x04005C82 RID: 23682
				public uint dwYCountChars;

				// Token: 0x04005C83 RID: 23683
				public uint dwFillAttribute;

				// Token: 0x04005C84 RID: 23684
				public uint dwFlags;

				// Token: 0x04005C85 RID: 23685
				public ushort wShowWindow;

				// Token: 0x04005C86 RID: 23686
				public ushort cbReserved2;

				// Token: 0x04005C87 RID: 23687
				public IntPtr lpReserved2;

				// Token: 0x04005C88 RID: 23688
				public SafeFileHandle hStdInput;

				// Token: 0x04005C89 RID: 23689
				public SafeFileHandle hStdOutput;

				// Token: 0x04005C8A RID: 23690
				public SafeFileHandle hStdError;

				// Token: 0x04005C8B RID: 23691
				public IntPtr lpAttributeList;
			}
		}
	}
}
