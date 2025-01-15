using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F6A RID: 8042
	internal class ErrorInfoTypeInfo : InterfaceTypeInfo<IErrorInfo>
	{
		// Token: 0x0600C491 RID: 50321 RVA: 0x00274250 File Offset: 0x00272450
		private static int GetGUID(IntPtr objHandle, out Guid pGUID)
		{
			try
			{
				InterfaceTypeInfo<IErrorInfo>.FromIntPtr(objHandle).GetGUID(out pGUID);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				pGUID = default(Guid);
				return InterfaceTypeInfo<IErrorInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C492 RID: 50322 RVA: 0x0027429C File Offset: 0x0027249C
		private static int GetSource(IntPtr objHandle, [MarshalAs(UnmanagedType.BStr)] out string pBstrSource)
		{
			try
			{
				InterfaceTypeInfo<IErrorInfo>.FromIntPtr(objHandle).GetSource(out pBstrSource);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				pBstrSource = string.Empty;
				return InterfaceTypeInfo<IErrorInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C493 RID: 50323 RVA: 0x002742E8 File Offset: 0x002724E8
		private static int GetDescription(IntPtr objHandle, [MarshalAs(UnmanagedType.BStr)] out string pBstrDescription)
		{
			try
			{
				InterfaceTypeInfo<IErrorInfo>.FromIntPtr(objHandle).GetDescription(out pBstrDescription);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				pBstrDescription = string.Empty;
				return InterfaceTypeInfo<IErrorInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C494 RID: 50324 RVA: 0x00274334 File Offset: 0x00272534
		private static int GetHelpFile(IntPtr objHandle, [MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile)
		{
			try
			{
				InterfaceTypeInfo<IErrorInfo>.FromIntPtr(objHandle).GetHelpFile(out pBstrHelpFile);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				pBstrHelpFile = string.Empty;
				return InterfaceTypeInfo<IErrorInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C495 RID: 50325 RVA: 0x00274380 File Offset: 0x00272580
		private static int GetHelpContext(IntPtr objHandle, out uint pdwHelpContext)
		{
			try
			{
				InterfaceTypeInfo<IErrorInfo>.FromIntPtr(objHandle).GetHelpContext(out pdwHelpContext);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				pdwHelpContext = 0U;
				return InterfaceTypeInfo<IErrorInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C496 RID: 50326 RVA: 0x002743C8 File Offset: 0x002725C8
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new ErrorInfoTypeInfo.GetGUIDCallback(ErrorInfoTypeInfo.GetGUID),
				new ErrorInfoTypeInfo.GetSourceCallback(ErrorInfoTypeInfo.GetSource),
				new ErrorInfoTypeInfo.GetDescriptionCallback(ErrorInfoTypeInfo.GetDescription),
				new ErrorInfoTypeInfo.GetHelpFileCallback(ErrorInfoTypeInfo.GetHelpFile),
				new ErrorInfoTypeInfo.GetHelpContextCallback(ErrorInfoTypeInfo.GetHelpContext)
			};
		}

		// Token: 0x02001F6B RID: 8043
		// (Invoke) Token: 0x0600C499 RID: 50329
		private delegate int GetGUIDCallback(IntPtr objHandle, out Guid pGUID);

		// Token: 0x02001F6C RID: 8044
		// (Invoke) Token: 0x0600C49D RID: 50333
		private delegate int GetSourceCallback(IntPtr objHandle, [MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

		// Token: 0x02001F6D RID: 8045
		// (Invoke) Token: 0x0600C4A1 RID: 50337
		private delegate int GetDescriptionCallback(IntPtr objHandle, [MarshalAs(UnmanagedType.BStr)] out string pBstrDescription);

		// Token: 0x02001F6E RID: 8046
		// (Invoke) Token: 0x0600C4A5 RID: 50341
		private delegate int GetHelpFileCallback(IntPtr objHandle, [MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		// Token: 0x02001F6F RID: 8047
		// (Invoke) Token: 0x0600C4A9 RID: 50345
		private delegate int GetHelpContextCallback(IntPtr objHandle, out uint pdwHelpContext);
	}
}
