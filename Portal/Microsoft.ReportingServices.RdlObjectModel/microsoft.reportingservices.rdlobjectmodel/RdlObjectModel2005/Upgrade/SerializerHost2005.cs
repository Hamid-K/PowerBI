using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2008.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005.Upgrade
{
	// Token: 0x02000057 RID: 87
	internal class SerializerHost2005 : SerializerHost2008
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00015018 File Offset: 0x00013218
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00015020 File Offset: 0x00013220
		public List<IUpgradeable> Upgradeable
		{
			get
			{
				return this.m_upgradeable;
			}
			set
			{
				this.m_upgradeable = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00015029 File Offset: 0x00013229
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00015031 File Offset: 0x00013231
		public List<DataSource2005> DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001503A File Offset: 0x0001323A
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00015042 File Offset: 0x00013242
		public Hashtable NameTable
		{
			get
			{
				return this.m_nameTable;
			}
			set
			{
				this.m_nameTable = value;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001504B File Offset: 0x0001324B
		public override Type GetSubstituteType(Type type)
		{
			return SerializerHost2005.GetSubstituteType(type, this.m_serializing);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001505C File Offset: 0x0001325C
		public static Type GetSubstituteType(Type type, bool serializing)
		{
			if (!serializing)
			{
				if (type == typeof(Field))
				{
					return typeof(FieldEx);
				}
				for (int i = 0; i < SerializerHost2005.m_substituteTypes.GetLength(0); i++)
				{
					if (type == SerializerHost2005.m_substituteTypes[i, 0])
					{
						return SerializerHost2005.m_substituteTypes[i, 1];
					}
				}
			}
			else
			{
				if (type.BaseType == typeof(Tablix))
				{
					return typeof(Tablix);
				}
				for (int j = 0; j < SerializerHost2005.m_substituteTypes.GetLength(0); j++)
				{
					if (type == SerializerHost2005.m_substituteTypes[j, 1])
					{
						return SerializerHost2005.m_substituteTypes[j, 0];
					}
				}
			}
			return type;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001511C File Offset: 0x0001331C
		public override void OnDeserialization(object value)
		{
			if (this.m_extraStringData != null)
			{
				if (value is IExpression)
				{
					IExpression expression = (IExpression)value;
					expression.Expression += this.m_extraStringData;
				}
				this.m_extraStringData = null;
			}
			if (this.m_nameTable != null && value is IGlobalNamedObject)
			{
				this.m_nameTable[((IGlobalNamedObject)value).Name] = value;
			}
			if (this.m_upgradeable != null)
			{
				if (this.m_dataSources != null && value is DataSource2005)
				{
					this.m_dataSources.Add((DataSource2005)value);
				}
				else if (value is IUpgradeable)
				{
					this.m_upgradeable.Add((IUpgradeable)value);
				}
			}
			base.OnDeserialization(value);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000151CE File Offset: 0x000133CE
		public override IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
		{
			return new ExtensionNamespace[]
			{
				new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false)
			};
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000151E9 File Offset: 0x000133E9
		public SerializerHost2005(bool serializing)
			: base(serializing)
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000151F4 File Offset: 0x000133F4
		// Note: this type is marked as 'beforefieldinit'.
		static SerializerHost2005()
		{
			Type[,] array = new Type[20, 2];
			array[0, 0] = typeof(Report);
			array[0, 1] = typeof(Report2005);
			array[1, 0] = typeof(Body);
			array[1, 1] = typeof(Body2005);
			array[2, 0] = typeof(Rectangle);
			array[2, 1] = typeof(Rectangle2005);
			array[3, 0] = typeof(Textbox);
			array[3, 1] = typeof(Textbox2005);
			array[4, 0] = typeof(Image);
			array[4, 1] = typeof(Image2005);
			array[5, 0] = typeof(Line);
			array[5, 1] = typeof(Line2005);
			array[6, 0] = typeof(Chart);
			array[6, 1] = typeof(Chart2005);
			array[7, 0] = typeof(CustomReportItem);
			array[7, 1] = typeof(CustomReportItem2005);
			array[8, 0] = typeof(Style);
			array[8, 1] = typeof(Style2005);
			array[9, 0] = typeof(BackgroundImage);
			array[9, 1] = typeof(BackgroundImage2005);
			array[10, 0] = typeof(Group);
			array[10, 1] = typeof(Grouping2005);
			array[11, 0] = typeof(SortExpression);
			array[11, 1] = typeof(SortBy2005);
			array[12, 0] = typeof(ChartDataPoint);
			array[12, 1] = typeof(DataPoint2005);
			array[13, 0] = typeof(CustomData);
			array[13, 1] = typeof(CustomData2005);
			array[14, 0] = typeof(DataHierarchy);
			array[14, 1] = typeof(DataGroupings2005);
			array[15, 0] = typeof(DataMember);
			array[15, 1] = typeof(DataGrouping2005);
			array[16, 0] = typeof(Subreport);
			array[16, 1] = typeof(Subreport2005);
			array[17, 0] = typeof(DataSource);
			array[17, 1] = typeof(DataSource2005);
			array[18, 0] = typeof(Query);
			array[18, 1] = typeof(Query2005);
			array[19, 0] = typeof(ReportParameter);
			array[19, 1] = typeof(ReportParameter2005);
			SerializerHost2005.m_substituteTypes = array;
		}

		// Token: 0x040000F7 RID: 247
		private List<IUpgradeable> m_upgradeable;

		// Token: 0x040000F8 RID: 248
		private List<DataSource2005> m_dataSources;

		// Token: 0x040000F9 RID: 249
		private Hashtable m_nameTable;

		// Token: 0x040000FA RID: 250
		private static readonly Type[,] m_substituteTypes;
	}
}
