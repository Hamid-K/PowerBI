using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F65 RID: 8037
	internal class PersistTypeInfo : InterfaceTypeInfo<IPersist>
	{
		// Token: 0x0600C476 RID: 50294 RVA: 0x00273FC0 File Offset: 0x002721C0
		private static int GetClassID(IntPtr objHandle, out Guid clsid)
		{
			try
			{
				InterfaceTypeInfo<IPersist>.FromIntPtr(objHandle).GetClassID(out clsid);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				clsid = Guid.Empty;
				return InterfaceTypeInfo<IPersist>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C477 RID: 50295 RVA: 0x0027400C File Offset: 0x0027220C
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new PersistTypeInfo.GetClassIDCallback(PersistTypeInfo.GetClassID)
			};
		}

		// Token: 0x02001F66 RID: 8038
		// (Invoke) Token: 0x0600C47A RID: 50298
		private delegate int GetClassIDCallback(IntPtr objHandle, out Guid clsid);
	}
}
