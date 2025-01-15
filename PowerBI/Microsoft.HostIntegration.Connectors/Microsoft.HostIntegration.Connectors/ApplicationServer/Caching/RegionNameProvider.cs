using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200036A RID: 874
	internal static class RegionNameProvider
	{
		// Token: 0x06001EBE RID: 7870 RVA: 0x0005E63D File Offset: 0x0005C83D
		static RegionNameProvider()
		{
			RegionNameProvider.GrowSystemRegionList(1024);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0005E664 File Offset: 0x0005C864
		private static void GrowSystemRegionList(int limit)
		{
			lock (RegionNameProvider._lock)
			{
				for (int i = RegionNameProvider._systemRegionList.Count; i < limit; i++)
				{
					string text = "Default_Region_" + i.ToString("d4", NumberFormatInfo.InvariantInfo);
					RegionNameProvider._systemRegionList.Add(text);
				}
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0005E6DC File Offset: 0x0005C8DC
		internal static string GetSystemRegionName(string key, int systemRegionCount)
		{
			uint num = CsHash32.ComputeString(key, 0U, false);
			return RegionNameProvider.GetSystemRegionName(key, num, systemRegionCount);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		internal static string GetSystemRegionName(string key, uint hash, int systemRegionCount)
		{
			int num = (int)((ulong)(hash >> 22) % (ulong)((long)systemRegionCount));
			string text;
			try
			{
				text = RegionNameProvider._systemRegionList[num];
			}
			catch (ArgumentOutOfRangeException)
			{
				RegionNameProvider.GrowSystemRegionList(systemRegionCount);
				text = RegionNameProvider.GetSystemRegionName(key, systemRegionCount);
			}
			return text;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0005E744 File Offset: 0x0005C944
		internal static bool IsSystemRegion(string regionName)
		{
			return regionName.StartsWith("Default_Region_", StringComparison.Ordinal);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0005E754 File Offset: 0x0005C954
		internal static string[] GetSystemRegionList(int systemRegionCount)
		{
			if (RegionNameProvider._systemRegionList.Count < systemRegionCount)
			{
				RegionNameProvider.GrowSystemRegionList(systemRegionCount);
			}
			string[] array = new string[systemRegionCount];
			lock (RegionNameProvider._lock)
			{
				RegionNameProvider._systemRegionList.CopyTo(0, array, 0, systemRegionCount);
			}
			return array;
		}

		// Token: 0x04001167 RID: 4455
		private static object _lock = new object();

		// Token: 0x04001168 RID: 4456
		private static List<string> _systemRegionList = new List<string>(1024);
	}
}
