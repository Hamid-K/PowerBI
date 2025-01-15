using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F41 RID: 8001
	internal class CommandPropertiesTypeInfo : InterfaceTypeInfo<ICommandProperties>
	{
		// Token: 0x0600C3F2 RID: 50162 RVA: 0x0027379C File Offset: 0x0027199C
		private unsafe static int GetProperties(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ICommandProperties>.ValidateHResult(InterfaceTypeInfo<ICommandProperties>.FromIntPtr(objHandle).GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countPropertySets = 0U;
				nativePropertySets = (IntPtr)((UIntPtr)0);
				num = InterfaceTypeInfo<ICommandProperties>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C3F3 RID: 50163 RVA: 0x002737F0 File Offset: 0x002719F0
		private unsafe static int SetProperties(IntPtr objHandle, uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			return InterfaceTypeInfo<ICommandProperties>.InvokeAndReturnHResult(() => InterfaceTypeInfo<ICommandProperties>.FromIntPtr(objHandle).SetProperties(countPropertySets, nativePropertySets), objHandle);
		}

		// Token: 0x0600C3F4 RID: 50164 RVA: 0x0027382F File Offset: 0x00271A2F
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new CommandPropertiesTypeInfo.GetPropertiesCallback(CommandPropertiesTypeInfo.GetProperties),
				new CommandPropertiesTypeInfo.SetPropertiesCallback(CommandPropertiesTypeInfo.SetProperties)
			};
		}

		// Token: 0x02001F42 RID: 8002
		// (Invoke) Token: 0x0600C3F7 RID: 50167
		private unsafe delegate int GetPropertiesCallback(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x02001F43 RID: 8003
		// (Invoke) Token: 0x0600C3FB RID: 50171
		private unsafe delegate int SetPropertiesCallback(IntPtr objHandle, uint countPropertySets, DBPROPSET* nativePropertySets);
	}
}
