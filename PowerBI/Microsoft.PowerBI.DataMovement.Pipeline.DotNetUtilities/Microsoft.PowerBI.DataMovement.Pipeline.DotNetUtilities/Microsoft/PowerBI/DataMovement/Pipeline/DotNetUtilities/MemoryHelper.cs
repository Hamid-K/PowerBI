using System;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.DotNetUtilities
{
	// Token: 0x02000007 RID: 7
	internal static class MemoryHelper
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		internal static ulong GetInstalledMemoryInKilobytes()
		{
			return MemoryHelper.GetInstalledMemoryInKilobytesInternal(true, "SELECT Capacity FROM Win32_PhysicalMemory");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		[NullableContext(1)]
		internal static ulong GetInstalledMemoryInKilobytesInternal(bool callWin32 = true, string wmiQuery = "SELECT Capacity FROM Win32_PhysicalMemory")
		{
			ulong num;
			if (callWin32 && MemoryHelper.NativeMethods.GetPhysicallyInstalledSystemMemory(out num) && num > 0UL)
			{
				return num;
			}
			ulong num2;
			if (MemoryHelper.TryGetMemoryCapacityFromWmiInKilobytes(wmiQuery, out num2))
			{
				return num2;
			}
			return 0UL;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		[NullableContext(1)]
		internal static bool TryGetMemoryCapacityFromWmiInKilobytes(string wmiQuery, out ulong capacity)
		{
			capacity = 0UL;
			try
			{
				ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher(wmiQuery).Get();
				if (managementObjectCollection.Count > 0)
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ulong num;
							if (!ulong.TryParse(enumerator.Current.GetPropertyValue("Capacity").ToString(), out num))
							{
								return false;
							}
							capacity += num / 1024UL;
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0200000F RID: 15
		private static class NativeMethods
		{
			// Token: 0x0600001C RID: 28
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetPhysicallyInstalledSystemMemory(out ulong TotalMemoryInKilobytes);
		}
	}
}
