using System;
using System.Xml;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000019 RID: 25
	public class DefaultRowsetFactory : IRowsetFactory
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000042B4 File Offset: 0x000024B4
		public bool TryCreate(XmlNode rowsetNode, out IRowsetDefinition rowset)
		{
			bool flag = false;
			rowset = null;
			if (rowsetNode.Name.Equals("SqlRowset"))
			{
				rowset = new SqlRowsetDefinition(new XmlNodeReader(rowsetNode));
				flag = true;
			}
			else if (rowsetNode.Name.Equals("InlineRowset"))
			{
				rowset = new InlineRowset(new XmlNodeReader(rowsetNode));
				flag = true;
			}
			else if (rowsetNode.Name.Equals("CsvFileRowset"))
			{
				rowset = new CsvFileRowset(new XmlNodeReader(rowsetNode));
				flag = true;
			}
			return flag;
		}
	}
}
