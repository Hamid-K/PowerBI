using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F58 RID: 8024
	internal class DBInfoTypeInfo : InterfaceTypeInfo<IDBInfo>
	{
		// Token: 0x0600C447 RID: 50247 RVA: 0x00273CF0 File Offset: 0x00271EF0
		private unsafe static int GetKeywords(IntPtr objHandle, out char* keywords)
		{
			try
			{
				InterfaceTypeInfo<IDBInfo>.FromIntPtr(objHandle).GetKeywords(out keywords);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				keywords = (IntPtr)((UIntPtr)0);
				return InterfaceTypeInfo<IDBInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C448 RID: 50248 RVA: 0x00273D38 File Offset: 0x00271F38
		private unsafe static int GetLiteralInfo(IntPtr objHandle, uint cLiterals, DBLITERAL* nativeLiterals, out uint cLiteralInfo, out DBLITERALINFO* nativeLiteralInfos, out char* strings)
		{
			try
			{
				InterfaceTypeInfo<IDBInfo>.FromIntPtr(objHandle).GetLiteralInfo(cLiterals, nativeLiterals, out cLiteralInfo, out nativeLiteralInfos, out strings);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				cLiteralInfo = 0U;
				nativeLiteralInfos = (IntPtr)((UIntPtr)0);
				strings = (IntPtr)((UIntPtr)0);
				return InterfaceTypeInfo<IDBInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C449 RID: 50249 RVA: 0x00273D90 File Offset: 0x00271F90
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBInfoTypeInfo.GetKeywordsCallback(DBInfoTypeInfo.GetKeywords),
				new DBInfoTypeInfo.GetLiteralInfoCallback(DBInfoTypeInfo.GetLiteralInfo)
			};
		}

		// Token: 0x02001F59 RID: 8025
		// (Invoke) Token: 0x0600C44C RID: 50252
		private unsafe delegate int GetKeywordsCallback(IntPtr objHandle, out char* keywords);

		// Token: 0x02001F5A RID: 8026
		// (Invoke) Token: 0x0600C450 RID: 50256
		private unsafe delegate int GetLiteralInfoCallback(IntPtr objHandle, uint cLiterals, DBLITERAL* nativeLiterals, out uint cLiteralInfo, out DBLITERALINFO* nativeLiteralInfos, out char* strings);
	}
}
