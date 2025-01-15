using System;
using System.Runtime.InteropServices;
using Microsoft.OleDb.PInvokeInterop.Test;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F7D RID: 8061
	internal class MarshalledObjectHandle
	{
		// Token: 0x0600C4D3 RID: 50387 RVA: 0x002747BF File Offset: 0x002729BF
		public MarshalledObjectHandle(PInvokeInteropServices interopServices, object obj)
		{
			this.interopServices = interopServices;
			this.obj = obj;
			this.handle = GCHandle.Alloc(this);
		}

		// Token: 0x0600C4D4 RID: 50388 RVA: 0x002747E4 File Offset: 0x002729E4
		~MarshalledObjectHandle()
		{
			if (this.obj != null && this.interopServices != null && this.PUnknown != IntPtr.Zero)
			{
				PInvokeInteropServices.CleanupManagedRefs(this.PUnknown);
			}
		}

		// Token: 0x17002FD3 RID: 12243
		// (get) Token: 0x0600C4D5 RID: 50389 RVA: 0x00274838 File Offset: 0x00272A38
		public IntPtr PUnknown
		{
			get
			{
				return this.pUnknown;
			}
		}

		// Token: 0x0600C4D6 RID: 50390 RVA: 0x00274840 File Offset: 0x00272A40
		public void AttachPUnknown(IntPtr punk)
		{
			IManagedInterop managedInterop = this.obj as IManagedInterop;
			if (managedInterop != null)
			{
				managedInterop.AttachPUnknown(punk);
			}
			this.pUnknown = punk;
		}

		// Token: 0x17002FD4 RID: 12244
		// (get) Token: 0x0600C4D7 RID: 50391 RVA: 0x0027486A File Offset: 0x00272A6A
		public PInvokeInteropServices InteropServices
		{
			get
			{
				return this.interopServices;
			}
		}

		// Token: 0x0600C4D8 RID: 50392 RVA: 0x00274874 File Offset: 0x00272A74
		public void Release()
		{
			IManagedDisposable managedDisposable = this.obj as IManagedDisposable;
			if (managedDisposable != null)
			{
				managedDisposable.Dispose();
			}
			this.obj = null;
			this.pUnknown = IntPtr.Zero;
			this.interopServices = null;
			this.handle.Free();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600C4D9 RID: 50393 RVA: 0x002748C0 File Offset: 0x00272AC0
		public static bool SupportsInterface<T>(IntPtr objHandle) where T : class
		{
			return MarshalledObjectHandle.FromIntPtr(objHandle).obj is T;
		}

		// Token: 0x0600C4DA RID: 50394 RVA: 0x002748D5 File Offset: 0x00272AD5
		public static T FromIntPtr<T>(IntPtr objHandle) where T : class
		{
			return (T)((object)MarshalledObjectHandle.FromIntPtr(objHandle).obj);
		}

		// Token: 0x0600C4DB RID: 50395 RVA: 0x002748E8 File Offset: 0x00272AE8
		public static MarshalledObjectHandle FromIntPtr(IntPtr objHandle)
		{
			return (MarshalledObjectHandle)GCHandle.FromIntPtr(objHandle).Target;
		}

		// Token: 0x0600C4DC RID: 50396 RVA: 0x00274908 File Offset: 0x00272B08
		public IntPtr ToIntPtr()
		{
			return GCHandle.ToIntPtr(this.handle);
		}

		// Token: 0x0600C4DD RID: 50397 RVA: 0x00274915 File Offset: 0x00272B15
		internal static ITestAccessorMarshalledObjectHandle GetTestAccessor()
		{
			return new MarshalledObjectHandle.TestAccessorMarshalledObjectHandle();
		}

		// Token: 0x040064D0 RID: 25808
		private PInvokeInteropServices interopServices;

		// Token: 0x040064D1 RID: 25809
		private object obj;

		// Token: 0x040064D2 RID: 25810
		private GCHandle handle;

		// Token: 0x040064D3 RID: 25811
		private IntPtr pUnknown;

		// Token: 0x02001F7E RID: 8062
		private class TestAccessorMarshalledObjectHandle : ITestAccessorMarshalledObjectHandle
		{
			// Token: 0x0600C4DE RID: 50398 RVA: 0x0027491C File Offset: 0x00272B1C
			public void FreeHandle(MarshalledObjectHandle marshalledObjectHandle)
			{
				marshalledObjectHandle.handle.Free();
				marshalledObjectHandle = null;
			}
		}
	}
}
