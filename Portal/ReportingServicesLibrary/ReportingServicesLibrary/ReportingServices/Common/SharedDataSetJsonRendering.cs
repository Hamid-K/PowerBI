using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.SharedDataSets;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000374 RID: 884
	internal static class SharedDataSetJsonRendering
	{
		// Token: 0x06001CF6 RID: 7414 RVA: 0x0007520C File Offset: 0x0007340C
		internal static string GetJsonSchemaDataType(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Boolean:
				return "Boolean";
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return "Double";
			case TypeCode.DateTime:
				return "DateTime";
			}
			return "String";
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0007527E File Offset: 0x0007347E
		internal static Microsoft.ReportingServices.RdlObjectModel.Report GetEmptyRdlReport()
		{
			return new Microsoft.ReportingServices.RdlObjectModel.Report
			{
				Language = "en-US",
				ReportUnitType = SizeTypes.Inch,
				ReportServerUrl = "http://localhost"
			};
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000752A8 File Offset: 0x000734A8
		internal static Microsoft.ReportingServices.RdlObjectModel.Report GetTablixBasedRdlReport(int? maxRows = null)
		{
			Microsoft.ReportingServices.RdlObjectModel.Report emptyRdlReport = SharedDataSetJsonRendering.GetEmptyRdlReport();
			ICollection<Microsoft.ReportingServices.RdlObjectModel.ReportItem> reportItems = emptyRdlReport.Body.ReportItems;
			Microsoft.ReportingServices.RdlObjectModel.Tablix tablix = new Microsoft.ReportingServices.RdlObjectModel.Tablix();
			tablix.Name = SharedDataSetJsonRendering.GetRandomizedItemName("Tablix");
			tablix.TablixBody = new TablixBody
			{
				TablixColumns = new TablixColumn[]
				{
					new TablixColumn
					{
						Width = new ReportSize(1.0)
					}
				},
				TablixRows = new TablixRow[]
				{
					new TablixRow
					{
						TablixCells = new TablixCell[]
						{
							new TablixCell
							{
								CellContents = new CellContents
								{
									ReportItem = new Microsoft.ReportingServices.RdlObjectModel.Rectangle
									{
										Name = SharedDataSetJsonRendering.GetRandomizedItemName("Rectangle"),
										KeepTogether = true
									}
								}
							}
						}
					}
				}
			};
			tablix.TablixColumnHierarchy = new TablixHierarchy
			{
				TablixMembers = new TablixMember[]
				{
					new TablixMember()
				}
			};
			Microsoft.ReportingServices.RdlObjectModel.Tablix tablix2 = tablix;
			TablixHierarchy tablixHierarchy = new TablixHierarchy();
			TablixHierarchy tablixHierarchy2 = tablixHierarchy;
			TablixMember[] array = new TablixMember[1];
			int num = 0;
			TablixMember tablixMember = new TablixMember();
			Group group = new Group();
			group.Name = "Details";
			List<ReportExpression> list;
			if (maxRows != null)
			{
				(list = new List<ReportExpression>()).Add(new ReportExpression
				{
					Value = string.Format("=IIf(RowNumber(Nothing) <= {0}, RowNumber(Nothing), 0)", maxRows ?? 0)
				});
			}
			else
			{
				list = null;
			}
			group.GroupExpressions = list;
			tablixMember.Group = group;
			array[num] = tablixMember;
			tablixHierarchy2.TablixMembers = array;
			tablix2.TablixRowHierarchy = tablixHierarchy;
			reportItems.Add(tablix);
			return emptyRdlReport;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00075420 File Offset: 0x00073620
		internal static string GetRandomizedItemName(string itemName)
		{
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[4];
			randomNumberGenerator.GetBytes(array);
			return string.Format("{0}_{1}", itemName, BitConverter.ToString(array, 0).Replace("-", string.Empty));
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00075460 File Offset: 0x00073660
		internal static Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet GetRdlSharedDataSet(byte[] dataSetDefinition)
		{
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet sharedDataSet;
			using (MemoryStream memoryStream = new MemoryStream(dataSetDefinition))
			{
				sharedDataSet = Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet.Deserialize(memoryStream);
			}
			return sharedDataSet;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00075498 File Offset: 0x00073698
		internal static Microsoft.ReportingServices.RdlObjectModel.DataSet CreateRdlDataSet(Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet sharedDataSet, string path, IEnumerable<string> createQueryParameterNames = null)
		{
			if (string.IsNullOrEmpty(sharedDataSet.DataSet.Name))
			{
				sharedDataSet.DataSet.Name = "DataSet1";
			}
			Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet = new Microsoft.ReportingServices.RdlObjectModel.DataSet
			{
				Name = sharedDataSet.DataSet.Name,
				Fields = sharedDataSet.DataSet.Fields
			};
			foreach (Microsoft.ReportingServices.RdlObjectModel.Field field in dataSet.Fields)
			{
				field.DataField = field.Name;
				if (field.Value.IsExpression)
				{
					field.Value = default(ReportExpression);
				}
			}
			dataSet.SharedDataSet = new Microsoft.ReportingServices.RdlObjectModel.SharedDataSet
			{
				SharedDataSetReference = path,
				ReportServerUrl = sharedDataSet.ReportServerUrl
			};
			if (createQueryParameterNames != null)
			{
				dataSet.SharedDataSet.QueryParameters = (from p in sharedDataSet.DataSet.Query.DataSetParameters.Select(new Func<DataSetParameter, QueryParameter>(SharedDataSetJsonRendering.CreateRdlQueryParameter))
					where createQueryParameterNames.Any((string n) => n.Equals(p.Name, StringComparison.OrdinalIgnoreCase))
					select p).ToList<QueryParameter>();
			}
			return dataSet;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000755CC File Offset: 0x000737CC
		internal static QueryParameter CreateRdlQueryParameter(DataSetParameter parameter)
		{
			return new QueryParameter
			{
				Name = parameter.Name,
				Value = ((parameter.DefaultValue != null) ? parameter.DefaultValue.Value : null)
			};
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00075618 File Offset: 0x00073818
		internal static void CreateRdlAggregatedFieldBindings(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet, string aggregation)
		{
			if (!report.DataSets.Contains(dataSet))
			{
				report.DataSets.Add(dataSet);
			}
			foreach (Microsoft.ReportingServices.RdlObjectModel.Field field in dataSet.Fields)
			{
				SharedDataSetJsonRendering.AddAggregatedDataField(report, field, aggregation);
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00075680 File Offset: 0x00073880
		internal static void CreateRdlFieldBindings(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet, bool useAsSpecificExpression)
		{
			int num = 0;
			if (!report.DataSets.Contains(dataSet))
			{
				report.DataSets.Add(dataSet);
			}
			Microsoft.ReportingServices.RdlObjectModel.Tablix reportDataTablix = SharedDataSetJsonRendering.GetReportDataTablix(report);
			reportDataTablix.DataSetName = dataSet.Name;
			Microsoft.ReportingServices.RdlObjectModel.Rectangle tablixDataRectangle = SharedDataSetJsonRendering.GetTablixDataRectangle(reportDataTablix);
			foreach (Microsoft.ReportingServices.RdlObjectModel.Field field in dataSet.Fields)
			{
				SharedDataSetJsonRendering.AddDataField(tablixDataRectangle, field, ++num, useAsSpecificExpression);
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00075708 File Offset: 0x00073908
		internal static Microsoft.ReportingServices.RdlObjectModel.Tablix GetReportDataTablix(Microsoft.ReportingServices.RdlObjectModel.Report report)
		{
			return report.Body.ReportItems[0] as Microsoft.ReportingServices.RdlObjectModel.Tablix;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00075720 File Offset: 0x00073920
		internal static Microsoft.ReportingServices.RdlObjectModel.Rectangle GetTablixDataRectangle(Microsoft.ReportingServices.RdlObjectModel.Tablix tablix)
		{
			return tablix.TablixBody.TablixRows[0].TablixCells[0].CellContents.ReportItem as Microsoft.ReportingServices.RdlObjectModel.Rectangle;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00075750 File Offset: 0x00073950
		internal static void AddDataField(Microsoft.ReportingServices.RdlObjectModel.Rectangle rectangle, Microsoft.ReportingServices.RdlObjectModel.Field field, int index, bool useAsSpecificExpression)
		{
			Textbox textbox = SharedDataSetJsonRendering.CreateRdlTextBox(field, index, useAsSpecificExpression);
			rectangle.ReportItems.Add(textbox);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00075774 File Offset: 0x00073974
		internal static void AddAggregatedDataField(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.Field field, string aggregation)
		{
			Textbox textbox = SharedDataSetJsonRendering.CreateRdlTextBox(field, aggregation);
			report.Body.ReportItems.Add(textbox);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0007579C File Offset: 0x0007399C
		internal static Textbox CreateRdlTextBox(Microsoft.ReportingServices.RdlObjectModel.Field field, string aggregation)
		{
			if (!SharedDataSetJsonRendering.AllowedAggregateFunctions.Contains(aggregation))
			{
				throw new ArgumentException("Invalid aggregation specified", "aggregation");
			}
			return new Textbox
			{
				Name = SharedDataSetJsonRendering.GetRandomizedItemName("TextBox_" + field.Name),
				DataElementStyle = DataElementStyles.Element,
				CustomProperties = new CustomProperty[]
				{
					new CustomProperty
					{
						Name = "OriginalFieldName",
						Value = field.Name
					}
				},
				Paragraphs = new Paragraph[]
				{
					new Paragraph
					{
						TextRuns = new TextRun[]
						{
							new TextRun
							{
								Value = new ReportExpression
								{
									Value = string.Format("={0}(Fields!{1}.Value)", aggregation, field.Name)
								}
							}
						}
					}
				},
				DefaultName = field.Name
			};
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00075884 File Offset: 0x00073A84
		internal static Textbox CreateRdlTextBox(Microsoft.ReportingServices.RdlObjectModel.Field field, int index, bool useAsSpecificExpression)
		{
			string text = (useAsSpecificExpression ? string.Format("=iif(IsNothing(Fields!{0}(\"MEMBER_VALUE\")) or Not(typeof(Fields!{0}(\"MEMBER_VALUE\")) is DateTime), Fields!{0}.Value, Fields!{0}(\"MEMBER_VALUE\"))", field.Name) : string.Format("=Fields!{0}.Value", field.Name));
			return new Textbox
			{
				Name = SharedDataSetJsonRendering.GetRandomizedItemName(string.Format("TextBox_{0:D4}_{1}", index, field.Name)),
				DataElementStyle = DataElementStyles.Element,
				CustomProperties = new CustomProperty[]
				{
					new CustomProperty
					{
						Name = "OriginalFieldName",
						Value = field.Name
					}
				},
				Paragraphs = new Paragraph[]
				{
					new Paragraph
					{
						TextRuns = new TextRun[]
						{
							new TextRun
							{
								Value = new ReportExpression
								{
									Value = text
								}
							}
						}
					}
				},
				DefaultName = field.Name
			};
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00075968 File Offset: 0x00073B68
		internal static RdlSerializer CreateRdlSerializer()
		{
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet.SharedDataSetSerializerHost sharedDataSetSerializerHost = new Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet.SharedDataSetSerializerHost();
			return new RdlSerializer(new RdlSerializerSettings
			{
				Host = sharedDataSetSerializerHost
			});
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0007598C File Offset: 0x00073B8C
		internal static byte[] GetRdlReportBytes(Microsoft.ReportingServices.RdlObjectModel.Report report)
		{
			RdlSerializer rdlSerializer = SharedDataSetJsonRendering.CreateRdlSerializer();
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				rdlSerializer.Serialize(memoryStream, report);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000759D4 File Offset: 0x00073BD4
		internal static IDictionary<string, bool> GetDataSetParameterCardinalities(XElement dataSetSchemaXml)
		{
			return dataSetSchemaXml.Descendants(XName.Get("DataSetParameter", "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")).Select(delegate(XElement x)
			{
				bool flag = false;
				XElement xelement = x.Element(XName.Get("IsMultiValued", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"));
				if (xelement != null)
				{
					bool.TryParse(xelement.Value, out flag);
				}
				return new KeyValuePair<string, bool>(x.Attribute("Name").Value, flag);
			}).ToDictionary((KeyValuePair<string, bool> k) => k.Key, (KeyValuePair<string, bool> v) => v.Value);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x00075A60 File Offset: 0x00073C60
		internal static string GetDesignerParameterPropertyValue(XElement dataSetSchemaXml, string parameterName, string designerPropertyName)
		{
			XElement xelement = dataSetSchemaXml.Descendants(XName.Get("DataSetParameter", "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")).FirstOrDefault((XElement x) => x.Attribute("Name").Value == parameterName);
			if (xelement != null)
			{
				return SharedDataSetJsonRendering.GetDesignerParameterPropertyValue(xelement, designerPropertyName);
			}
			return null;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00075AB0 File Offset: 0x00073CB0
		internal static string GetDesignerParameterPropertyValue(XElement parameterElement, string designerPropertyName)
		{
			XElement xelement = parameterElement.Element(XName.Get(designerPropertyName, "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"));
			if (xelement != null)
			{
				return xelement.Value;
			}
			return null;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00075ADC File Offset: 0x00073CDC
		internal static DataTypes GetRdlDataType(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return DataTypes.String;
			}
			typeName = typeName.Split(new char[] { '.' }).Last<string>();
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(typeName);
			if (num <= 2386971688U)
			{
				if (num <= 765439473U)
				{
					if (num <= 679076413U)
					{
						if (num != 423635464U)
						{
							if (num != 679076413U)
							{
								return DataTypes.String;
							}
							if (!(typeName == "Char"))
							{
								return DataTypes.String;
							}
							return DataTypes.String;
						}
						else
						{
							if (!(typeName == "SByte"))
							{
								return DataTypes.String;
							}
							return DataTypes.Integer;
						}
					}
					else if (num != 697196164U)
					{
						if (num != 765439473U)
						{
							return DataTypes.String;
						}
						if (!(typeName == "Int16"))
						{
							return DataTypes.String;
						}
						return DataTypes.Integer;
					}
					else
					{
						if (!(typeName == "Int64"))
						{
							return DataTypes.String;
						}
						return DataTypes.Integer;
					}
				}
				else if (num <= 1324880019U)
				{
					if (num != 1323747186U)
					{
						if (num != 1324880019U)
						{
							return DataTypes.String;
						}
						if (!(typeName == "UInt64"))
						{
							return DataTypes.String;
						}
						return DataTypes.Integer;
					}
					else
					{
						if (!(typeName == "UInt16"))
						{
							return DataTypes.String;
						}
						return DataTypes.Integer;
					}
				}
				else if (num != 1615808600U)
				{
					if (num != 2386971688U)
					{
						return DataTypes.String;
					}
					if (!(typeName == "Double"))
					{
						return DataTypes.String;
					}
				}
				else
				{
					if (!(typeName == "String"))
					{
						return DataTypes.String;
					}
					return DataTypes.String;
				}
			}
			else if (num <= 3409549631U)
			{
				if (num <= 2711245919U)
				{
					if (num != 2615964816U)
					{
						if (num != 2711245919U)
						{
							return DataTypes.String;
						}
						if (!(typeName == "Int32"))
						{
							return DataTypes.String;
						}
						return DataTypes.Integer;
					}
					else
					{
						if (!(typeName == "DateTime"))
						{
							return DataTypes.String;
						}
						return DataTypes.DateTime;
					}
				}
				else if (num != 2779444460U)
				{
					if (num != 3409549631U)
					{
						return DataTypes.String;
					}
					if (!(typeName == "Byte"))
					{
						return DataTypes.String;
					}
					return DataTypes.Integer;
				}
				else if (!(typeName == "Decimal"))
				{
					return DataTypes.String;
				}
			}
			else if (num <= 3538687084U)
			{
				if (num != 3512147854U)
				{
					if (num != 3538687084U)
					{
						return DataTypes.String;
					}
					if (!(typeName == "UInt32"))
					{
						return DataTypes.String;
					}
					return DataTypes.Integer;
				}
				else
				{
					if (!(typeName == "Empty"))
					{
						return DataTypes.String;
					}
					return DataTypes.String;
				}
			}
			else if (num != 3774159766U)
			{
				if (num != 3969205087U)
				{
					if (num != 4051133705U)
					{
						return DataTypes.String;
					}
					if (!(typeName == "Single"))
					{
						return DataTypes.String;
					}
				}
				else
				{
					if (!(typeName == "Boolean"))
					{
						return DataTypes.String;
					}
					return DataTypes.Boolean;
				}
			}
			else
			{
				if (!(typeName == "DBNull"))
				{
					return DataTypes.String;
				}
				return DataTypes.String;
			}
			return DataTypes.Float;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00075D6C File Offset: 0x00073F6C
		internal static ReportParameter CreateReportParameter(string value, QueryParameter queryParameter, out string name)
		{
			name = SharedDataSetJsonRendering.GetRandomizedItemName("Parameter");
			return new ReportParameter
			{
				Name = name,
				DataType = queryParameter.Value.DataType,
				Hidden = true,
				AllowBlank = true,
				DefaultValue = new DefaultValue
				{
					Values = new List<ReportExpression?>
					{
						new ReportExpression?(value)
					}
				}
			};
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00075DDC File Offset: 0x00073FDC
		internal static void AddReportParameter(Microsoft.ReportingServices.RdlObjectModel.Report report, ReportParameter reportParameter, int reportParameterIndex)
		{
			report.ReportParameters.Add(reportParameter);
			GridLayoutDefinition gridLayoutDefinition = report.ReportParametersLayout.GridLayoutDefinition;
			int numberOfRows = gridLayoutDefinition.NumberOfRows;
			gridLayoutDefinition.NumberOfRows = numberOfRows + 1;
			gridLayoutDefinition.CellDefinitions.Add(new CellDefinition
			{
				ColumnIndex = 0,
				RowIndex = reportParameterIndex,
				ParameterName = reportParameter.Name
			});
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00075E3C File Offset: 0x0007403C
		internal static bool TryAddDataSetParameterToReport(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet, string parameterName, string parameterValue, bool multiValue, int reportParameterIndex)
		{
			QueryParameter queryParameter = dataSet.SharedDataSet.QueryParameters.SingleOrDefault((QueryParameter x) => x.Name == parameterName);
			if (queryParameter != null)
			{
				string text;
				ReportParameter reportParameter = SharedDataSetJsonRendering.CreateReportParameter(parameterValue, queryParameter, out text);
				reportParameter.MultiValue = multiValue;
				SharedDataSetJsonRendering.AddReportParameter(report, reportParameter, reportParameterIndex);
				queryParameter.Value = string.Format(CultureInfo.InvariantCulture, "=Parameters!{0}.Value", text);
				return true;
			}
			return false;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00075EB0 File Offset: 0x000740B0
		internal static void BindDataSetParameters(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet, ParameterInfoCollection parameterInfoCollection)
		{
			int num = 0;
			foreach (object obj in parameterInfoCollection)
			{
				ParameterInfo parameterInfo = (ParameterInfo)obj;
				string text = null;
				if (parameterInfo.Values != null && parameterInfo.Values.Length != 0)
				{
					string[] array = parameterInfo.ValuesToStringArray(CultureInfo.InvariantCulture);
					if (parameterInfo.Values.Length == 1)
					{
						text = array[0];
					}
					else
					{
						text = "=new Object() {\"" + string.Join("\",\"", array) + "\"}";
					}
				}
				if (SharedDataSetJsonRendering.TryAddDataSetParameterToReport(report, dataSet, parameterInfo.Name, text, parameterInfo.MultiValue, num))
				{
					num++;
				}
			}
		}

		// Token: 0x04000C2E RID: 3118
		internal const int CurrentVersionCachedJson = 1;

		// Token: 0x04000C2F RID: 3119
		internal const string RenderFormat = "SHAREDDATASETJSON";

		// Token: 0x04000C30 RID: 3120
		internal const string DeviceInfo = "<DeviceInfo><SharedDataSetTable>true</SharedDataSetTable></DeviceInfo>";

		// Token: 0x04000C31 RID: 3121
		internal const string DIOutputSharedDataSetTable = "SharedDataSetTable";

		// Token: 0x04000C32 RID: 3122
		internal const string DIIndented = "Indented";

		// Token: 0x04000C33 RID: 3123
		internal const string DIUseMsDateFormat = "MsDateFormat";

		// Token: 0x04000C34 RID: 3124
		internal const string DIMaxRows = "MaxRows";

		// Token: 0x04000C35 RID: 3125
		internal const string RdlTextBoxOriginalFieldNameCustomPropertyKey = "OriginalFieldName";

		// Token: 0x04000C36 RID: 3126
		internal const string SchemaDataTypeDouble = "Double";

		// Token: 0x04000C37 RID: 3127
		internal const string SchemaDataTypeBoolan = "Boolean";

		// Token: 0x04000C38 RID: 3128
		internal const string SchemaDataTypeDateTime = "DateTime";

		// Token: 0x04000C39 RID: 3129
		internal const string SchemaDataTypeString = "String";

		// Token: 0x04000C3A RID: 3130
		private static readonly string[] AllowedAggregateFunctions = new string[] { "Sum", "Avg", "Min", "First", "Last", "Max" };
	}
}
