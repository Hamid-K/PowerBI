using System;
using System.Data;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C5F RID: 3167
	internal static class HostExcelService
	{
		// Token: 0x0600560B RID: 22027 RVA: 0x0012A708 File Offset: 0x00128908
		public static IExcelService GetExcelService(IEngineHost hostEnvironment)
		{
			IExcelService excelService = hostEnvironment.QueryService<IExcelService>();
			if (excelService == null)
			{
				excelService = HostExcelService.NoExcelService.Instance;
			}
			return excelService;
		}

		// Token: 0x02000C60 RID: 3168
		private class NoExcelService : IExcelService
		{
			// Token: 0x0600560C RID: 22028 RVA: 0x000020FD File Offset: 0x000002FD
			private NoExcelService()
			{
			}

			// Token: 0x0600560D RID: 22029 RVA: 0x000E6755 File Offset: 0x000E4955
			public bool TryGetTableNames(out string[] tableNames)
			{
				tableNames = null;
				return false;
			}

			// Token: 0x0600560E RID: 22030 RVA: 0x0012A726 File Offset: 0x00128926
			public bool TryGetTable(string name, int skip, int take, out IDataReader dataReader, out bool columnNamesGenerated, out string errorMessage)
			{
				dataReader = null;
				columnNamesGenerated = false;
				errorMessage = null;
				return false;
			}

			// Token: 0x0600560F RID: 22031 RVA: 0x000912D6 File Offset: 0x0008F4D6
			public bool TryHandleGetTableException(string name, Exception exception, out string errorMessage)
			{
				errorMessage = null;
				return false;
			}

			// Token: 0x04003077 RID: 12407
			public static readonly HostExcelService.NoExcelService Instance = new HostExcelService.NoExcelService();
		}
	}
}
