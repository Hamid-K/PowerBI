using System;
using System.Data;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000045 RID: 69
	public interface IExcelService
	{
		// Token: 0x0600013B RID: 315
		bool TryGetTableNames(out string[] tableNames);

		// Token: 0x0600013C RID: 316
		bool TryGetTable(string name, int skip, int take, out IDataReader dataReader, out bool columnNamesGenerated, out string errorMessage);

		// Token: 0x0600013D RID: 317
		bool TryHandleGetTableException(string name, Exception exception, out string errorMessage);
	}
}
