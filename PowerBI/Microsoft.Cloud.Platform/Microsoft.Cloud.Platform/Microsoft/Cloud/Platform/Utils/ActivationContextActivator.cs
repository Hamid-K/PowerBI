using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000164 RID: 356
	public sealed class ActivationContextActivator : IDisposable
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x000200FC File Offset: 0x0001E2FC
		public ActivationContextActivator([NotNull] string manifestFilePath, [NotNull] string assemblyFolder)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(manifestFilePath, "manifestFilePath");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(assemblyFolder, "assemblyFolder");
			if (ActivationContextActivator.sm_leakDetectionEnabledTweak.Value)
			{
				this.m_creationCallStack = CallStackRef.Capture(true);
			}
			this.m_safeActivationContextHandle = ActivationContextActivator.CreateContext(manifestFilePath, assemblyFolder);
			this.m_activatonContextCookie = ActivationContextActivator.ActivateContext(this.m_safeActivationContextHandle);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002015B File Offset: 0x0001E35B
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00020164 File Offset: 0x0001E364
		private void Dispose(bool disposing)
		{
			if (this.m_safeActivationContextHandle != null && !this.m_safeActivationContextHandle.IsInvalid && !this.m_safeActivationContextHandle.IsClosed)
			{
				ActivationContextActivator.DeactivateContext(this.m_activatonContextCookie);
				this.m_safeActivationContextHandle.Dispose();
				this.m_activatonContextCookie = IntPtr.Zero;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000201B4 File Offset: 0x0001E3B4
		private static ActivationContextActivator.SafeActivationContextHandle CreateContext(string manifestPath, string assemblyDirectory)
		{
			ActivationContextActivator.NativeMethods.ACTCTX actctx = default(ActivationContextActivator.NativeMethods.ACTCTX);
			actctx.cbSize = Marshal.SizeOf(typeof(ActivationContextActivator.NativeMethods.ACTCTX));
			actctx.dwFlags = 4U;
			actctx.lpSource = manifestPath;
			actctx.lpAssemblyDirectory = assemblyDirectory;
			ActivationContextActivator.SafeActivationContextHandle safeActivationContextHandle = ActivationContextActivator.NativeMethods.CreateActCtx(ref actctx);
			if (safeActivationContextHandle.IsInvalid)
			{
				throw new ActivationContextException(Marshal.GetLastWin32Error());
			}
			return safeActivationContextHandle;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00020214 File Offset: 0x0001E414
		private static IntPtr ActivateContext(ActivationContextActivator.SafeActivationContextHandle handle)
		{
			IntPtr intPtr;
			if (!ActivationContextActivator.NativeMethods.ActivateActCtx(handle, out intPtr))
			{
				throw new ActivationContextException(Marshal.GetLastWin32Error());
			}
			return intPtr;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00020237 File Offset: 0x0001E437
		private static void DeactivateContext(IntPtr cookie)
		{
			if (!ActivationContextActivator.NativeMethods.DeactivateActCtx(0, cookie))
			{
				throw new ActivationContextException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x04000380 RID: 896
		private readonly ActivationContextActivator.SafeActivationContextHandle m_safeActivationContextHandle;

		// Token: 0x04000381 RID: 897
		private IntPtr m_activatonContextCookie;

		// Token: 0x04000382 RID: 898
		private CallStackRef m_creationCallStack;

		// Token: 0x04000383 RID: 899
		private const string c_leakDetectionEnabledTweakName = "Microsoft.Cloud.Platform.Utils.ActivationContextActivator.LeakDetectionEnabled";

		// Token: 0x04000384 RID: 900
		private static Tweak<bool> sm_leakDetectionEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.ActivationContextActivator.LeakDetectionEnabled", "When set, Activation Context leaks (failure to call Dispose) are detected in debug builds", false);

		// Token: 0x02000632 RID: 1586
		private static class NativeMethods
		{
			// Token: 0x06002CC0 RID: 11456
			[DllImport("Kernel32.dll", SetLastError = true)]
			internal static extern ActivationContextActivator.SafeActivationContextHandle CreateActCtx(ref ActivationContextActivator.NativeMethods.ACTCTX pActCtx);

			// Token: 0x06002CC1 RID: 11457
			[DllImport("Kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool ActivateActCtx(ActivationContextActivator.SafeActivationContextHandle hActCtx, out IntPtr lpCookie);

			// Token: 0x06002CC2 RID: 11458
			[DllImport("Kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);

			// Token: 0x06002CC3 RID: 11459
			[DllImport("Kernel32.dll", SetLastError = true)]
			internal static extern void ReleaseActCtx(IntPtr hActCtx);

			// Token: 0x04001180 RID: 4480
			internal const uint ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4U;

			// Token: 0x02000876 RID: 2166
			internal struct ACTCTX
			{
				// Token: 0x040019E5 RID: 6629
				public int cbSize;

				// Token: 0x040019E6 RID: 6630
				public uint dwFlags;

				// Token: 0x040019E7 RID: 6631
				public string lpSource;

				// Token: 0x040019E8 RID: 6632
				public ushort wProcessorArchitecture;

				// Token: 0x040019E9 RID: 6633
				public ushort wLangId;

				// Token: 0x040019EA RID: 6634
				public string lpAssemblyDirectory;

				// Token: 0x040019EB RID: 6635
				public string lpResourceName;

				// Token: 0x040019EC RID: 6636
				public string lpApplicationName;
			}
		}

		// Token: 0x02000633 RID: 1587
		internal sealed class SafeActivationContextHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06002CC4 RID: 11460 RVA: 0x0009F691 File Offset: 0x0009D891
			public SafeActivationContextHandle()
				: base(true)
			{
			}

			// Token: 0x06002CC5 RID: 11461 RVA: 0x0009F69A File Offset: 0x0009D89A
			protected override bool ReleaseHandle()
			{
				if (!this.IsInvalid)
				{
					ActivationContextActivator.NativeMethods.ReleaseActCtx(this.handle);
				}
				return true;
			}
		}
	}
}
