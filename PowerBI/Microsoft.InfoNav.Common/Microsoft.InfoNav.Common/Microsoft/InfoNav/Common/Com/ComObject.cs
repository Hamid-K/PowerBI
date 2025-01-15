using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Microsoft.InfoNav.Common.WindowsInterop;

namespace Microsoft.InfoNav.Common.Com
{
	// Token: 0x0200008F RID: 143
	internal sealed class ComObject : RealProxy, IDisposable
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0000D05F File Offset: 0x0000B25F
		internal ComObject(string dllPath, Guid clsid, Guid guid, Type type, LogCallback logger)
			: base(type)
		{
			this._dllPath = dllPath;
			this._clsid = clsid;
			this._guid = guid;
			this._type = type;
			this._logger = logger;
			this.CreateComInstanceFromDll();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000D094 File Offset: 0x0000B294
		static ComObject()
		{
			NativeMethods.SetErrorMode(1U);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		~ComObject()
		{
			this.DisposeInternal();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public void Dispose()
		{
			this.DisposeInternal();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000D0DA File Offset: 0x0000B2DA
		private void DisposeInternal()
		{
			if (this._dllHandle != IntPtr.Zero)
			{
				this.UnloadDll();
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public override IMessage Invoke(IMessage message)
		{
			MethodCallMessageWrapper methodCallMessageWrapper = new MethodCallMessageWrapper((IMethodCallMessage)message);
			IMessage message2;
			try
			{
				message2 = new ReturnMessage(this._type.GetMethod(methodCallMessageWrapper.MethodName, (Type[])methodCallMessageWrapper.MethodSignature).Invoke(this._target, methodCallMessageWrapper.Args), methodCallMessageWrapper.Args, methodCallMessageWrapper.ArgCount, methodCallMessageWrapper.LogicalCallContext, methodCallMessageWrapper);
			}
			catch (TargetInvocationException ex)
			{
				message2 = new ReturnMessage(ex, methodCallMessageWrapper);
			}
			return message2;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000D170 File Offset: 0x0000B370
		private void CreateComInstanceFromDll()
		{
			this._dllHandle = ComLoader.LoadLibrary(this._dllPath);
			if (this._dllHandle == IntPtr.Zero)
			{
				string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), this._dllPath);
				this._dllHandle = ComLoader.LoadLibrary(text);
				if (this._dllHandle == IntPtr.Zero)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			try
			{
				IntPtr procAddress = NativeMethods.GetProcAddress(this._dllHandle, "DllGetClassObject");
				if (procAddress == IntPtr.Zero)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				ComObject.DllGetClassObject dllGetClassObject = (ComObject.DllGetClassObject)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(ComObject.DllGetClassObject));
				Guid clsid = this._clsid;
				Guid guid = new Guid("00000001-0000-0000-C000-000000000046");
				object obj;
				int num = dllGetClassObject(ref clsid, ref guid, out obj);
				if ((long)num != 0L)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				Guid guid2 = this._guid;
				try
				{
					IntPtr intPtr;
					num = ((IClassFactory)obj).CreateInstance(null, ref guid2, out intPtr);
					if ((long)num != 0L)
					{
						Marshal.ThrowExceptionForHR(num);
					}
					this._target = Marshal.GetObjectForIUnknown(intPtr);
				}
				catch (InvalidCastException ex)
				{
					throw new InvalidCastException("Check for invalid GUID.", ex);
				}
			}
			catch (COMException)
			{
				this.UnloadDll();
				throw;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		private void UnloadDll()
		{
			if (this._dllHandle != IntPtr.Zero)
			{
				if (!NativeMethods.FreeLibrary(this._dllHandle))
				{
					if (this._logger != null)
					{
						this._logger(string.Format(CultureInfo.InvariantCulture, "Failed to unload COM Library {0} (CLSID: \"{1}\", GUID: \"{2}\") Error: {3}", new object[]
						{
							this._dllPath,
							this._clsid,
							this._guid,
							Marshal.GetLastWin32Error()
						}));
						return;
					}
				}
				else
				{
					this._dllHandle = IntPtr.Zero;
				}
			}
		}

		// Token: 0x04000129 RID: 297
		private const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x0400012A RID: 298
		private readonly string _dllPath;

		// Token: 0x0400012B RID: 299
		private readonly Guid _clsid;

		// Token: 0x0400012C RID: 300
		private readonly Guid _guid;

		// Token: 0x0400012D RID: 301
		private readonly LogCallback _logger;

		// Token: 0x0400012E RID: 302
		private readonly Type _type;

		// Token: 0x0400012F RID: 303
		private object _target;

		// Token: 0x04000130 RID: 304
		private IntPtr _dllHandle;

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x0600060D RID: 1549
		private delegate int DllGetClassObject(ref Guid classId, ref Guid interfaceId, [MarshalAs(UnmanagedType.Interface)] out object ppunk);
	}
}
