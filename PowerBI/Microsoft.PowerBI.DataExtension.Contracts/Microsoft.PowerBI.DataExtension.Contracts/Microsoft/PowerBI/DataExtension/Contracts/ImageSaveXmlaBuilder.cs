using System;
using System.Globalization;
using System.Security;

namespace Microsoft.PowerBI.DataExtension.Contracts
{
	// Token: 0x02000006 RID: 6
	public class ImageSaveXmlaBuilder
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public string BuildSaveDatabaseXmla(string dbName)
		{
			return string.Format(CultureInfo.InvariantCulture, "<ImageSave xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                <Object>\r\n                    <DatabaseID>{0}</DatabaseID>\r\n                </Object>\r\n                <DataSource>true</DataSource>\r\n            </ImageSave>", new object[] { SecurityElement.Escape(dbName) });
		}

		// Token: 0x0400003C RID: 60
		private const string imageSaveXmla = "<ImageSave xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                <Object>\r\n                    <DatabaseID>{0}</DatabaseID>\r\n                </Object>\r\n                <DataSource>true</DataSource>\r\n            </ImageSave>";
	}
}
