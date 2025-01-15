using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C84 RID: 3204
	internal static class EssbaseXmlaParser
	{
		// Token: 0x060056BD RID: 22205 RVA: 0x0012CBE8 File Offset: 0x0012ADE8
		public static List<Tuple<string, string, string>> ParseSourcesResponse(TextReader reader)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			xmlReader.MoveToContent();
			List<Tuple<string, string, string>> list = new List<Tuple<string, string, string>>();
			while (xmlReader.ReadToFollowing("DataSourceName"))
			{
				string text = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("DataSourceDescription");
				string text2 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("DataSourceInfo");
				string text3 = xmlReader.ReadElementContentAsString();
				list.Add(new Tuple<string, string, string>(text3, text, text2));
			}
			if (list.Count == 0)
			{
				list.Add(new Tuple<string, string, string>("Provider=Essbase;Data Source=localhost", "OAC", null));
			}
			return list;
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x0012CC74 File Offset: 0x0012AE74
		public static IList<string> ParseApplicationsResponse(TextReader reader)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			IList<string> list = new List<string>();
			while (xmlReader.ReadToFollowing("CATALOG_NAME"))
			{
				list.Add(xmlReader.ReadElementContentAsString());
			}
			return list;
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x0012CCB4 File Offset: 0x0012AEB4
		public static IList<string> ParseCubesResponse(TextReader reader)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			IList<string> list = new List<string>();
			while (xmlReader.ReadToFollowing("CUBE_NAME"))
			{
				list.Add(xmlReader.ReadElementContentAsString());
			}
			return list;
		}

		// Token: 0x060056C0 RID: 22208 RVA: 0x0012CCF4 File Offset: 0x0012AEF4
		public static IList<MdxMeasureMetadata> ParseMeasuresResponse(TextReader reader)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			IList<MdxMeasureMetadata> list = new List<MdxMeasureMetadata>();
			while (xmlReader.ReadToFollowing("MEASURE_NAME"))
			{
				string text = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("MEASURE_UNIQUE_NAME");
				string text2 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("DATA_TYPE");
				int num = xmlReader.ReadElementContentAsInt();
				list.Add(new MdxMeasureMetadata
				{
					Caption = text,
					UniqueName = text2,
					DisplayFolder = string.Empty,
					DataType = (OleDbType)(Enum.IsDefined(typeof(OleDbType), num) ? num : 5)
				});
			}
			return list;
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x0012CDA0 File Offset: 0x0012AFA0
		private static MdxDimensionType FindDimensionType(string dimType)
		{
			if (dimType == "0")
			{
				return MdxDimensionType.Unknown;
			}
			if (dimType == "1")
			{
				return MdxDimensionType.Time;
			}
			if (dimType == "2")
			{
				return MdxDimensionType.Measure;
			}
			if (dimType == "3")
			{
				return MdxDimensionType.Other;
			}
			if (!(dimType == "4"))
			{
				return MdxDimensionType.Other;
			}
			return MdxDimensionType.OtherNoAggregation;
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x0012CDFC File Offset: 0x0012AFFC
		public static IList<MdxDimensionMetadata> ParseDimensionsResponse(TextReader reader, out MdxDimensionMetadata measuresDimension)
		{
			measuresDimension = null;
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			IList<MdxDimensionMetadata> list = new List<MdxDimensionMetadata>();
			while (xmlReader.ReadToFollowing("DIMENSION_NAME"))
			{
				string text = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("DIMENSION_UNIQUE_NAME");
				string text2 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("DIMENSION_TYPE");
				MdxDimensionMetadata mdxDimensionMetadata = new MdxDimensionMetadata
				{
					UniqueName = text2,
					Caption = text,
					DimensionType = EssbaseXmlaParser.FindDimensionType(xmlReader.ReadElementContentAsString()),
					DataType = OleDbType.BSTR
				};
				if (mdxDimensionMetadata.DimensionType != MdxDimensionType.Measure)
				{
					list.Add(mdxDimensionMetadata);
				}
				else
				{
					measuresDimension = mdxDimensionMetadata;
				}
			}
			return list;
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x0012CE9C File Offset: 0x0012B09C
		public static IList<MdxHierarchyMetadata> ParseHierarchiesResponse(TextReader reader, string measuresDimensionName)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			IList<MdxHierarchyMetadata> list = new List<MdxHierarchyMetadata>();
			while (xmlReader.ReadToFollowing("DIMENSION_UNIQUE_NAME"))
			{
				string text = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("HIERARCHY_UNIQUE_NAME");
				string text2 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("HIERARCHY_CAPTION");
				string text3 = xmlReader.ReadElementContentAsString();
				if (text2 != measuresDimensionName)
				{
					list.Add(new MdxHierarchyMetadata
					{
						UniqueName = text2,
						DimensionUniqueName = text,
						UniqueIdentifier = text2,
						Caption = text3,
						DisplayFolder = string.Empty,
						Origin = 2,
						IsVisible = true
					});
				}
			}
			return list;
		}

		// Token: 0x060056C4 RID: 22212 RVA: 0x0012CF48 File Offset: 0x0012B148
		public static IList<MdxLevelMetadata> ParseLevelsResponse(TextReader reader, string measuresDimensionName)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			IList<MdxLevelMetadata> list = new List<MdxLevelMetadata>();
			while (xmlReader.ReadToFollowing("DIMENSION_UNIQUE_NAME"))
			{
				string text = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("HIERARCHY_UNIQUE_NAME");
				string text2 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("LEVEL_UNIQUE_NAME");
				string text3 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("LEVEL_CAPTION");
				string text4 = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("LEVEL_NUMBER");
				int num = xmlReader.ReadElementContentAsInt();
				if (text3 != measuresDimensionName && num != 0)
				{
					list.Add(new MdxLevelMetadata
					{
						Caption = text4,
						UniqueName = text3,
						DimensionUniqueName = text,
						HierarchyUniqueName = text2,
						Number = num
					});
				}
			}
			return list;
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x0012D014 File Offset: 0x0012B214
		public static Dictionary<string, string> ParseMembersResponse(TextReader reader)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			while (xmlReader.ReadToFollowing("MEMBER_UNIQUE_NAME"))
			{
				string text = xmlReader.ReadElementContentAsString();
				if (xmlReader.ReadToNextSibling("MEMBER_ALIAS"))
				{
					dictionary[text] = xmlReader.ReadElementContentAsString();
				}
			}
			return dictionary;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x0012D068 File Offset: 0x0012B268
		private static TypeValue MatchType(string typeString)
		{
			TypeValue typeValue = TypeValue.Any;
			if (typeString == "xsd:boolean")
			{
				typeValue = TypeValue.Logical;
			}
			else if (typeString == "xsd:date")
			{
				typeValue = TypeValue.Date;
			}
			else if (typeString == "xsd:dateTime")
			{
				typeValue = TypeValue.DateTime;
			}
			else if (typeString == "xsd:double" || typeString == "xsd:int" || typeString == "xsd:short" || typeString == "xsd:unsignedInt" || typeString == "xsd:unsignedShort")
			{
				typeValue = TypeValue.Number;
			}
			else if (typeString == "xsd:string")
			{
				typeValue = TypeValue.Text;
			}
			return typeValue;
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x0012D11C File Offset: 0x0012B31C
		private static Value GetValueForType(TypeValue type, XmlReader xmlReader)
		{
			string text = xmlReader.ReadElementContentAsString();
			if (text == "")
			{
				return Value.Null;
			}
			if (type == TypeValue.Text)
			{
				return TextValue.New(text);
			}
			if (type == TypeValue.Number)
			{
				return NumberValue.New(Convert.ToDouble(text, CultureInfo.InvariantCulture));
			}
			if (type == TypeValue.Logical)
			{
				return LogicalValue.New(Convert.ToBoolean(text, CultureInfo.InvariantCulture));
			}
			if (type == TypeValue.Date)
			{
				return DateValue.New(Convert.ToDateTime(text, CultureInfo.InvariantCulture));
			}
			if (type == TypeValue.DateTime)
			{
				return DateTimeValue.New(Convert.ToDateTime(text, CultureInfo.InvariantCulture));
			}
			return Value.Null;
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x0012D1BC File Offset: 0x0012B3BC
		public static TableValue ParseMdxResponse(TextReader reader, Dictionary<string, string> aliasDict = null)
		{
			XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader);
			IList<TypeValue> list = new List<TypeValue>();
			IList<string> list2 = new List<string>();
			IList<RecordValue> list3 = new List<RecordValue>();
			IList<string> list4 = new List<string>();
			EssbaseXmlaParser.ParseResponseForError(xmlReader, null);
			xmlReader.ReadToFollowing("xsd:element");
			xmlReader.Skip();
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					if (xmlReader.Name == "row")
					{
						IList<Value> list5 = new List<Value>();
						xmlReader.ReadToDescendant("C0");
						for (int i = 0; i < list4.Count; i++)
						{
							if (xmlReader.Name == list4[i])
							{
								list5.Add(EssbaseXmlaParser.GetValueForType(list[i], xmlReader));
								xmlReader.Skip();
							}
							else
							{
								list5.Add(Value.Null);
							}
						}
						RecordTypeValue recordTypeValue = RecordTypeValue.New(Keys.New(list2.AsEnumerable<string>().ToArray<string>()));
						list3.Add(RecordValue.New(recordTypeValue, list5.AsEnumerable<Value>().ToArray<Value>()));
					}
					else if (xmlReader.Name == "xsd:element" && xmlReader.MoveToAttribute("type"))
					{
						TypeValue typeValue = EssbaseXmlaParser.MatchType(xmlReader.Value);
						list.Add(typeValue);
						xmlReader.MoveToAttribute("name");
						string value = xmlReader.Value;
						list4.Add(value);
						xmlReader.MoveToAttribute("sql:field");
						string text = xmlReader.Value;
						string text2;
						if (aliasDict != null && aliasDict.TryGetValue(text, out text2))
						{
							text = text2;
						}
						list2.Add(text);
					}
				}
			}
			RecordValue[] array = list.Select((TypeValue type) => RecordTypeAlgebra.NewField(type, false)).AsEnumerable<RecordValue>().ToArray<RecordValue>();
			Keys keys = Keys.New(list2.AsEnumerable<string>().ToArray<string>());
			Value[] array2 = array;
			return new EssbaseXmlaParser.MdxTableValue(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, array2)), new TableKey[]
			{
				new TableKey(new int[1], true)
			}), list3);
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x0012D3C4 File Offset: 0x0012B5C4
		private static void ParseResponseForError(XmlReader xmlReader, Func<int, string, bool> ignoreErrorMessageFunction = null)
		{
			xmlReader.MoveToContent();
			xmlReader.ReadToFollowing("SOAP-ENV:Body");
			while (xmlReader.Read() && xmlReader.NodeType != XmlNodeType.Element)
			{
			}
			if (!(xmlReader.Name == "SOAP-ENV:Fault"))
			{
				return;
			}
			xmlReader.ReadToFollowing("Error");
			xmlReader.MoveToAttribute("ErrorCode");
			int num = 0;
			int.TryParse(xmlReader.Value, out num);
			xmlReader.MoveToAttribute("Description");
			string value = xmlReader.Value;
			if (ignoreErrorMessageFunction != null && ignoreErrorMessageFunction(num, value))
			{
				return;
			}
			throw ValueException.NewExpressionError(value, null, null);
		}

		// Token: 0x02000C85 RID: 3205
		private sealed class MdxTableValue : TableValue
		{
			// Token: 0x060056CA RID: 22218 RVA: 0x0012D459 File Offset: 0x0012B659
			public MdxTableValue(TypeValue type, IEnumerable<RecordValue> records)
			{
				this.type = type;
				this.records = records;
			}

			// Token: 0x17001A37 RID: 6711
			// (get) Token: 0x060056CB RID: 22219 RVA: 0x0012D46F File Offset: 0x0012B66F
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x060056CC RID: 22220 RVA: 0x0012D477 File Offset: 0x0012B677
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (RecordValue recordValue in this.records)
				{
					yield return recordValue;
				}
				IEnumerator<RecordValue> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x040030E1 RID: 12513
			private readonly TypeValue type;

			// Token: 0x040030E2 RID: 12514
			private readonly IEnumerable<RecordValue> records;
		}
	}
}
