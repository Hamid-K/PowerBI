using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F76 RID: 8054
	public abstract class InterfaceTypeInfo<T> : InterfaceTypeInfoBase where T : class
	{
		// Token: 0x0600C4B9 RID: 50361 RVA: 0x00274492 File Offset: 0x00272692
		protected override InterfaceTypeInfoBase.SupportsInterfaceCallback CreateSupportsDelegate()
		{
			return (IntPtr objHandle) => InterfaceTypeInfo<T>.InvokeAndReturnHResult(() => InterfaceTypeInfo<T>.SupportsInterface(objHandle), objHandle);
		}

		// Token: 0x0600C4BA RID: 50362 RVA: 0x002744B3 File Offset: 0x002726B3
		protected static T FromIntPtr(IntPtr objHandle)
		{
			return MarshalledObjectHandle.FromIntPtr<T>(objHandle);
		}

		// Token: 0x0600C4BB RID: 50363 RVA: 0x002744BB File Offset: 0x002726BB
		protected static bool SupportsInterface(IntPtr objHandle)
		{
			return MarshalledObjectHandle.SupportsInterface<T>(objHandle);
		}

		// Token: 0x0600C4BC RID: 50364 RVA: 0x002744C4 File Offset: 0x002726C4
		protected static int InvokeAndReturnHResult(Action action, IntPtr objHandle)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				return InterfaceTypeInfo<T>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C4BD RID: 50365 RVA: 0x00274500 File Offset: 0x00272700
		protected static int InvokeAndReturnHResult(Func<int> func, IntPtr objHandle)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<T>.ValidateHResult(func(), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				num = InterfaceTypeInfo<T>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C4BE RID: 50366 RVA: 0x00274540 File Offset: 0x00272740
		protected static int InvokeAndReturnHResult(Func<bool> func, IntPtr objHandle)
		{
			return InterfaceTypeInfo<T>.InvokeAndReturnHResult(() => (!func()) ? 1 : 0, objHandle);
		}

		// Token: 0x0600C4BF RID: 50367 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		protected static int ValidateHResult(int result, IntPtr objHandle)
		{
			return result;
		}

		// Token: 0x0600C4C0 RID: 50368 RVA: 0x00274560 File Offset: 0x00272760
		protected static int ValidateException(Exception e, IntPtr objHandle)
		{
			int num = Marshal.GetHRForException(e);
			PInvokeInteropServices interopServices = MarshalledObjectHandle.FromIntPtr(objHandle).InteropServices;
			int num2;
			if (ErrorInfo.TryGetOverridenResult(e, out num2))
			{
				num = num2;
			}
			if (interopServices != null)
			{
				if (e.Data.Contains("OLEDB_ErrorInfo_IsReported"))
				{
					Guid guid = typeof(T).GUID;
					interopServices.SetErrorInfo(new ErrorInfo(e, ref guid));
				}
				else
				{
					ISupportErrorInfo supportErrorInfo = InterfaceTypeInfo<T>.FromIntPtr(objHandle) as ISupportErrorInfo;
					if (supportErrorInfo != null)
					{
						Guid guid2 = typeof(T).GUID;
						if (supportErrorInfo.InterfaceSupportsErrorInfo(ref guid2) == 0)
						{
							interopServices.SetErrorInfo(new ErrorInfo(e, ref guid2));
						}
					}
				}
			}
			return num;
		}
	}
}
