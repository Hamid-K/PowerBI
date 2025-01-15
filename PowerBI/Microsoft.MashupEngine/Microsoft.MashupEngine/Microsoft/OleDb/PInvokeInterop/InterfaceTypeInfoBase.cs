using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F7A RID: 8058
	public abstract class InterfaceTypeInfoBase : IInterfaceTypeInfo
	{
		// Token: 0x0600C4C9 RID: 50377 RVA: 0x00274668 File Offset: 0x00272868
		public unsafe int GetVTable(int size, IntPtr* vtable, bool includeSupportsInterfaceCallback)
		{
			try
			{
				if (this.delegates == null || (includeSupportsInterfaceCallback && this.supportsDelegate == null))
				{
					object obj = this.delegatesLock;
					lock (obj)
					{
						if (this.delegates == null)
						{
							this.delegates = this.CreateDelegates();
						}
						if (includeSupportsInterfaceCallback && this.supportsDelegate == null)
						{
							this.supportsDelegate = this.CreateSupportsDelegate();
						}
					}
				}
				int num = this.delegates.Length;
				if (size != num + ((includeSupportsInterfaceCallback > false) ? 1 : 0))
				{
					return -2147024809;
				}
				for (int i = 0; i < num; i++)
				{
					vtable[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = Marshal.GetFunctionPointerForDelegate(this.delegates[i]);
				}
				if (includeSupportsInterfaceCallback)
				{
					vtable[(IntPtr)num * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = Marshal.GetFunctionPointerForDelegate<InterfaceTypeInfoBase.SupportsInterfaceCallback>(this.supportsDelegate);
				}
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				return Marshal.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x0600C4CA RID: 50378
		protected abstract InterfaceTypeInfoBase.SupportsInterfaceCallback CreateSupportsDelegate();

		// Token: 0x0600C4CB RID: 50379
		protected abstract Delegate[] CreateDelegates();

		// Token: 0x040064C8 RID: 25800
		private Delegate[] delegates;

		// Token: 0x040064C9 RID: 25801
		private InterfaceTypeInfoBase.SupportsInterfaceCallback supportsDelegate;

		// Token: 0x040064CA RID: 25802
		private object delegatesLock = new object();

		// Token: 0x02001F7B RID: 8059
		// (Invoke) Token: 0x0600C4CE RID: 50382
		protected delegate int SupportsInterfaceCallback(IntPtr objHandle);
	}
}
