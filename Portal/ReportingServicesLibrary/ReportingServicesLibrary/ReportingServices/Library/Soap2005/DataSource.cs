using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000315 RID: 789
	public class DataSource
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x0006E86C File Offset: 0x0006CA6C
		internal static string ThisToXml(DataSource dataSource)
		{
			if (dataSource == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			DataSource.WriteToXml(dataSource, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0006E89B File Offset: 0x0006CA9B
		internal static void WriteToXml(DataSource dataSource, XmlTextWriter xml)
		{
			if (dataSource == null)
			{
				return;
			}
			DataSourceDefinitionOrReference.WriteToXml(dataSource.Item, dataSource.Name, xml);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
		internal static string ThisArrayToXml(DataSource[] dataSources)
		{
			if (dataSources == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartElement("DataSources");
			for (int i = 0; i < dataSources.Length; i++)
			{
				DataSource.WriteToXml(dataSources[i], xmlTextWriter);
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0006E906 File Offset: 0x0006CB06
		internal static DataSource[] XmlToThisArray(string dataSources, bool getPassword)
		{
			return DataSource.DataSourceInfoCollectionToThisArray(new DataSourceInfoCollection(dataSources, getPassword, DataProtection.Instance), getPassword, false);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0006E91C File Offset: 0x0006CB1C
		internal static DataSource DataSourceInfoToThis(DataSourceInfo dsi, bool getPassword, bool encrypted = false)
		{
			if (dsi == null)
			{
				return null;
			}
			DataSource dataSource = new DataSource();
			dataSource.Item = DataSourceDefinitionOrReference.DataSourceInfoToThis(dsi, out dataSource.Name, getPassword, encrypted);
			return dataSource;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0006E949 File Offset: 0x0006CB49
		internal static DataSourceInfo ThisToDataSourceInfo(DataSource dataSource)
		{
			if (dataSource.Item == null)
			{
				throw new MissingElementException("Item");
			}
			return DataSourceDefinitionOrReference.ThisToDataSourceInfo(dataSource.Item, dataSource.Name);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0006E970 File Offset: 0x0006CB70
		internal static DataSourceInfoCollection ThisArrayToDataSourceInfoCollection(DataSource[] dataSources)
		{
			if (dataSources == null)
			{
				return null;
			}
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			foreach (DataSource dataSource in dataSources)
			{
				if (dataSource == null)
				{
					throw new MissingElementException("DataSource");
				}
				DataSourceInfo dataSourceInfo = DataSource.ThisToDataSourceInfo(dataSource);
				if (dataSourceInfo.OriginalName == null)
				{
					throw new MissingElementException("Name");
				}
				if (dataSource.Item is InvalidDataSourceReference)
				{
					throw new InvalidParameterException("DataSources");
				}
				dataSourceInfoCollection.Add(dataSourceInfo);
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0006E9E0 File Offset: 0x0006CBE0
		internal static DataSource[] DataSourceInfoCollectionToThisArray(DataSourceInfoCollection dataSources, bool getPassword, bool encrypted = false)
		{
			if (dataSources == null)
			{
				return null;
			}
			DataSource[] array = new DataSource[dataSources.Count];
			int num = 0;
			foreach (object obj in dataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				array[num] = DataSource.DataSourceInfoToThis(dataSourceInfo, getPassword, encrypted);
				num++;
			}
			return array;
		}

		// Token: 0x04000A9E RID: 2718
		public string Name;

		// Token: 0x04000A9F RID: 2719
		[XmlElement(typeof(DataSourceDefinition))]
		[XmlElement(typeof(DataSourceReference))]
		[XmlElement(typeof(InvalidDataSourceReference))]
		public DataSourceDefinitionOrReference Item;
	}
}
