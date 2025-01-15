using System;
using System.Xml;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000312 RID: 786
	public class DataSourceDefinitionOrReference
	{
		// Token: 0x06001B28 RID: 6952 RVA: 0x0006E71C File Offset: 0x0006C91C
		internal static DataSourceInfo ThisToDataSourceInfo(DataSourceDefinitionOrReference item, string name)
		{
			if (item == null)
			{
				return null;
			}
			DataSourceInfo dataSourceInfo;
			if (item is DataSourceDefinition)
			{
				DataSourceDefinition dataSourceDefinition = (DataSourceDefinition)item;
				dataSourceInfo = DataSourceDefinition.ThisToDataSourceInfo(name, name, dataSourceDefinition);
			}
			else if (item is DataSourceReference)
			{
				DataSourceReference dataSourceReference = (DataSourceReference)item;
				dataSourceInfo = new DataSourceInfo(name, dataSourceReference.Reference, Guid.Empty);
			}
			else
			{
				if (!(item is InvalidDataSourceReference))
				{
					throw new InternalCatalogException("DataSourceDefinitionOrReference.ThisToDataSourceInfo - unknown type");
				}
				dataSourceInfo = new DataSourceInfo(name);
			}
			return dataSourceInfo;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0006E788 File Offset: 0x0006C988
		internal static DataSourceDefinitionOrReference DataSourceInfoToThis(DataSourceInfo dsi, out string name, bool getPassword, bool encrypted = false)
		{
			if (dsi == null)
			{
				name = null;
				return null;
			}
			DataSourceDefinitionOrReference dataSourceDefinitionOrReference;
			if (!dsi.IsReference)
			{
				name = dsi.Name;
				dataSourceDefinitionOrReference = DataSourceDefinition.DataSourceInfoToThis(dsi, getPassword, encrypted);
			}
			else if (dsi.DataSourceReference != null)
			{
				name = dsi.OriginalName;
				dataSourceDefinitionOrReference = new DataSourceReference
				{
					Reference = dsi.DataSourceReference
				};
			}
			else
			{
				name = dsi.OriginalName;
				dataSourceDefinitionOrReference = new InvalidDataSourceReference();
			}
			return dataSourceDefinitionOrReference;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0006E7EC File Offset: 0x0006C9EC
		internal static void WriteToXml(DataSourceDefinitionOrReference item, string name, XmlTextWriter xml)
		{
			if (item == null)
			{
				return;
			}
			xml.WriteStartElement("DataSource");
			if (name != null)
			{
				xml.WriteElementString("Name", name);
			}
			if (item is DataSourceDefinition)
			{
				DataSourceDefinition.WriteToXml((DataSourceDefinition)item, xml);
			}
			else if (item is DataSourceReference)
			{
				xml.WriteElementString("DataSourceReference", ((DataSourceReference)item).Reference);
			}
			else if (item is InvalidDataSourceReference)
			{
				xml.WriteElementString("InvalidReference", "True");
			}
			xml.WriteEndElement();
		}
	}
}
