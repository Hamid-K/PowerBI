using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200074A RID: 1866
	[Serializable]
	internal sealed class OWCChartInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006784 RID: 26500 RVA: 0x00193F64 File Offset: 0x00192164
		internal OWCChartInstanceInfo(ReportProcessing.ProcessingContext pc, OWCChart reportItemDef, OWCChartInstance owner)
			: base(pc, reportItemDef, owner, false)
		{
			this.m_chartData = new VariantList[reportItemDef.ChartData.Count];
			for (int i = 0; i < reportItemDef.ChartData.Count; i++)
			{
				this.m_chartData[i] = new VariantList();
			}
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x00193FD7 File Offset: 0x001921D7
		internal OWCChartInstanceInfo(ReportProcessing.ProcessingContext pc, OWCChart reportItemDef, OWCChartInstance owner, VariantList[] chartData)
			: base(pc, reportItemDef, owner, false)
		{
			this.m_chartData = chartData;
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x0019400E File Offset: 0x0019220E
		internal OWCChartInstanceInfo(OWCChart reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17002490 RID: 9360
		internal VariantList this[int index]
		{
			get
			{
				if (0 <= index && index < this.m_chartData.Length)
				{
					return this.m_chartData[index];
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002491 RID: 9361
		// (get) Token: 0x06006788 RID: 26504 RVA: 0x00194036 File Offset: 0x00192236
		// (set) Token: 0x06006789 RID: 26505 RVA: 0x0019403E File Offset: 0x0019223E
		internal VariantList[] ChartData
		{
			get
			{
				return this.m_chartData;
			}
			set
			{
				this.m_chartData = value;
			}
		}

		// Token: 0x17002492 RID: 9362
		// (get) Token: 0x0600678A RID: 26506 RVA: 0x00194047 File Offset: 0x00192247
		internal int Size
		{
			get
			{
				Global.Tracer.Assert(this.m_chartData.Length != 0);
				return this.m_chartData[0].Count;
			}
		}

		// Token: 0x17002493 RID: 9363
		// (get) Token: 0x0600678B RID: 26507 RVA: 0x0019406A File Offset: 0x0019226A
		// (set) Token: 0x0600678C RID: 26508 RVA: 0x00194072 File Offset: 0x00192272
		internal string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x0019407C File Offset: 0x0019227C
		internal void ChartDataXML(IChartStream chartStream)
		{
			OWCChart owcchart = (OWCChart)this.m_reportItemDef;
			int count = this.m_chartData[0].Count;
			string text = string.Empty;
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartElement("xml");
			xmlTextWriter.WriteAttributeString("xmlns", "s", null, "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			xmlTextWriter.WriteAttributeString("xmlns", "dt", null, "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882");
			xmlTextWriter.WriteAttributeString("xmlns", "rs", null, "urn:schemas-microsoft-com:rowset");
			xmlTextWriter.WriteAttributeString("xmlns", "z", null, "#RowsetSchema");
			xmlTextWriter.WriteStartElement("s", "Schema", null);
			xmlTextWriter.WriteAttributeString("id", "RowsetSchema");
			xmlTextWriter.WriteStartElement("s", "ElementType", null);
			xmlTextWriter.WriteAttributeString("name", "row");
			xmlTextWriter.WriteAttributeString("content", "eltOnly");
			for (int i = 0; i < owcchart.ChartData.Count; i++)
			{
				xmlTextWriter.WriteStartElement("s", "AttributeType", null);
				xmlTextWriter.WriteAttributeString("name", "c" + i.ToString(CultureInfo.InvariantCulture));
				xmlTextWriter.WriteAttributeString("rs", "name", null, owcchart.ChartData[i].Name);
				xmlTextWriter.WriteAttributeString("rs", "nullable", null, "true");
				xmlTextWriter.WriteAttributeString("rs", "writeunknown", null, "true");
				xmlTextWriter.WriteStartElement("s", "datatype", null);
				int num = 0;
				while (num < this.m_chartData[i].Count && this.m_chartData[i][num] == null)
				{
					num++;
				}
				if (num < this.m_chartData[i].Count)
				{
					switch (Type.GetTypeCode(this.m_chartData[i][num].GetType()))
					{
					case TypeCode.Object:
						if (this.m_chartData[i][num] is TimeSpan)
						{
							text = "time";
							goto IL_0324;
						}
						if (this.m_chartData[i][num] is byte[])
						{
							text = "bin.hex";
							goto IL_0324;
						}
						goto IL_0324;
					case TypeCode.Boolean:
						text = "boolean";
						goto IL_0324;
					case TypeCode.Char:
						text = "char";
						goto IL_0324;
					case TypeCode.SByte:
						text = "i1";
						goto IL_0324;
					case TypeCode.Byte:
						text = "ui1";
						goto IL_0324;
					case TypeCode.Int16:
						text = "i2";
						goto IL_0324;
					case TypeCode.UInt16:
						text = "ui2";
						goto IL_0324;
					case TypeCode.Int32:
						text = "i4";
						goto IL_0324;
					case TypeCode.UInt32:
						text = "ui4";
						goto IL_0324;
					case TypeCode.Int64:
						text = "i8";
						goto IL_0324;
					case TypeCode.UInt64:
						text = "ui8";
						goto IL_0324;
					case TypeCode.Single:
						text = "r4";
						goto IL_0324;
					case TypeCode.Double:
						text = "float";
						goto IL_0324;
					case TypeCode.Decimal:
						text = "r8";
						goto IL_0324;
					case TypeCode.DateTime:
						text = "dateTime";
						goto IL_0324;
					}
					text = "string";
				}
				else
				{
					text = "string";
				}
				IL_0324:
				xmlTextWriter.WriteAttributeString("dt", "type", null, text);
				xmlTextWriter.WriteAttributeString("rs", "fixedlength", null, "true");
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteStartElement("s", "extends", null);
			xmlTextWriter.WriteAttributeString("type", "rs:rowbase");
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteStartElement("rs", "data", null);
			bool flag = true;
			for (int j = 0; j < count; j++)
			{
				for (int i = 0; i < owcchart.ChartData.Count; i++)
				{
					if (this.m_chartData[i][j] != null)
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					xmlTextWriter.WriteStartElement("z", "row", null);
					for (int i = 0; i < owcchart.ChartData.Count; i++)
					{
						object obj = this.m_chartData[i][j];
						if (obj != null)
						{
							string text2 = ((obj is IFormattable) ? ((IFormattable)obj).ToString(null, CultureInfo.InvariantCulture) : obj.ToString());
							xmlTextWriter.WriteAttributeString("c" + i.ToString(CultureInfo.InvariantCulture), text2);
						}
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteWhitespace("\r\n");
					flag = true;
				}
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			chartStream.Write(stringWriter.ToString());
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x00194548 File Offset: 0x00192748
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ChartData, Token.Array, ObjectType.VariantList),
				new MemberInfo(MemberName.NoRows, Token.String)
			});
		}

		// Token: 0x04003350 RID: 13136
		private VariantList[] m_chartData;

		// Token: 0x04003351 RID: 13137
		private string m_noRows;
	}
}
