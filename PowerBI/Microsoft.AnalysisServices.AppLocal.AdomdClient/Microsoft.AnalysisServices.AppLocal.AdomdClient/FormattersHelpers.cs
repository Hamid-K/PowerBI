using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000027 RID: 39
	internal static class FormattersHelpers
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000AE3C File Offset: 0x0000903C
		static FormattersHelpers()
		{
			FormattersHelpers.FillInTypeConvertersHash();
			FormattersHelpers.FillInStringConvertersHash();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AE8C File Offset: 0x0000908C
		public static string ConvertToXml(object objVal)
		{
			FormattersHelpers.CovertToStringDelegate covertToStringDelegate = FormattersHelpers.stringConvertersHash[objVal.GetType()] as FormattersHelpers.CovertToStringDelegate;
			if (covertToStringDelegate != null)
			{
				return covertToStringDelegate(objVal);
			}
			return objVal.ToString();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public static object ReadRowsetProperty(XmlReader reader, string elementName, string elementNamespace, Type type, bool throwOnInlineError, bool isArray, bool convertToLocalTime)
		{
			object obj = null;
			if (isArray)
			{
				ArrayList arrayList = new ArrayList();
				Type elementType = type.GetElementType();
				while (reader.IsStartElement(elementName, elementNamespace))
				{
					object obj2 = FormattersHelpers.ReadRowsetProperty(reader, elementName, elementNamespace, elementType, throwOnInlineError, false, convertToLocalTime);
					XmlaError xmlaError = obj2 as XmlaError;
					if (xmlaError != null)
					{
						if (throwOnInlineError)
						{
							throw XmlaResultCollection.ExceptionOnError(new XmlaResult
							{
								Messages = { xmlaError }
							});
						}
						while (reader.IsStartElement(elementName, elementNamespace))
						{
							reader.Skip();
						}
						obj = obj2;
						break;
					}
					else
					{
						arrayList.Add(obj2);
					}
				}
				Array array = Array.CreateInstance(elementType, arrayList.Count);
				arrayList.CopyTo(array);
				obj = array;
			}
			else
			{
				if (!reader.IsEmptyElement)
				{
					using (FormattersHelpers.GetWhitespaceHandlingRestorer(reader, WhitespaceHandling.All))
					{
						reader.ReadStartElement(elementName, elementNamespace);
						if (reader.NodeType == XmlNodeType.Text)
						{
							obj = FormattersHelpers.ConvertFromXml(reader.ReadString(), type, convertToLocalTime);
						}
						else
						{
							string text = FormattersHelpers.ReadWhiteSpace(reader);
							if (reader.NodeType == XmlNodeType.EndElement)
							{
								obj = text;
							}
							else
							{
								bool flag = false;
								bool flag2 = false;
								if (type == typeof(object) || type == typeof(string))
								{
									flag = true;
									flag2 = ((XmlaReader)reader).SkipElements;
									((XmlaReader)reader).SkipElements = false;
								}
								try
								{
									if ((obj = XmlaClient.CheckAndGetRowsetError(reader, throwOnInlineError)) == null)
									{
										FormattersHelpers.CheckException(reader);
										if (flag)
										{
											obj = FormattersHelpers.ReadPropertyXml(reader);
										}
									}
								}
								finally
								{
									if (flag)
									{
										((XmlaReader)reader).SkipElements = flag2;
									}
								}
							}
						}
						reader.ReadEndElement();
						return obj;
					}
				}
				reader.Skip();
				obj = FormattersHelpers.ConvertFromXml(string.Empty, type, convertToLocalTime);
			}
			return obj;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B070 File Offset: 0x00009270
		public static object ReadDataSetProperty(XmlReader reader, Type type)
		{
			object obj = null;
			if (!reader.IsEmptyElement)
			{
				using (FormattersHelpers.GetWhitespaceHandlingRestorer(reader, WhitespaceHandling.All))
				{
					reader.ReadStartElement();
					if (reader.NodeType == XmlNodeType.Text)
					{
						obj = ((FormattersHelpers.GetValueDelegate)FormattersHelpers.typeConvertersHash[type])(reader.ReadString());
					}
					else
					{
						string text = FormattersHelpers.ReadWhiteSpace(reader);
						if (reader.NodeType == XmlNodeType.EndElement)
						{
							obj = text;
						}
						else if ((obj = XmlaClient.CheckAndGetDatasetError(reader)) == null)
						{
							FormattersHelpers.CheckException(reader);
							if (type == typeof(object))
							{
								obj = FormattersHelpers.ReadPropertyXml(reader);
							}
						}
					}
					reader.ReadEndElement();
					return obj;
				}
			}
			reader.Read();
			obj = ((FormattersHelpers.GetValueDelegate)FormattersHelpers.typeConvertersHash[type])(string.Empty);
			return obj;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B144 File Offset: 0x00009344
		public static Type GetElementType(XmlReader reader)
		{
			return FormattersHelpers.GetElementType(reader, null, typeof(object));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B158 File Offset: 0x00009358
		public static Type GetElementType(XmlReader reader, string theNamespace, Type typeIfNotSpecified)
		{
			Type type = FormattersHelpers.GetElementTypeInternal(reader, theNamespace);
			if (type == null)
			{
				type = typeIfNotSpecified;
			}
			return type;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B17C File Offset: 0x0000937C
		public static Type GetElementType(XmlReader reader, string theNamespace, DataColumn column)
		{
			Type type = FormattersHelpers.GetElementTypeInternal(reader, theNamespace);
			if (type == null)
			{
				type = FormattersHelpers.GetColumnType(column);
			}
			return type;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B1A4 File Offset: 0x000093A4
		public static Type GetColumnType(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			Type type = column.ExtendedProperties["type"] as Type;
			if (type == null)
			{
				type = column.DataType;
			}
			return type;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B1E6 File Offset: 0x000093E6
		public static void SetColumnType(DataColumn column, Type type)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			if (type != null)
			{
				column.ExtendedProperties["type"] = type;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000B210 File Offset: 0x00009410
		public static string GetColumnNamespace(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			return column.ExtendedProperties["namespace"] as string;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000B235 File Offset: 0x00009435
		public static void SetColumnNamespace(DataColumn column, string theNamespace)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			column.ExtendedProperties["namespace"] = theNamespace;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B256 File Offset: 0x00009456
		public static string GetColumnXsdTypeName(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			return column.ExtendedProperties["xsdTypeName"] as string;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B27B File Offset: 0x0000947B
		public static void SetColumnXsdTypeName(DataColumn column, string theXsdTypeName)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			column.ExtendedProperties["xsdTypeName"] = theXsdTypeName;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B29C File Offset: 0x0000949C
		public static string GetColumnCaptionFromSchemaElement(XmlSchemaElement element)
		{
			XmlAttribute unhandledAttributeByName = FormattersHelpers.GetUnhandledAttributeByName(element, "field");
			if (unhandledAttributeByName != null)
			{
				return unhandledAttributeByName.Value;
			}
			return element.Name;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public static void GetSchemaElementTypeAndName(XmlSchemaElement element, out Type valueType, out string typeName)
		{
			valueType = typeof(object);
			typeName = string.Empty;
			if (!element.SchemaTypeName.IsEmpty && element.MaxOccurs > 0m)
			{
				XmlSchemaDatatype xmlSchemaDatatype;
				if (element.SchemaTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					xmlSchemaDatatype = element.ElementSchemaType.Datatype;
				}
				else
				{
					xmlSchemaDatatype = null;
				}
				XmlSchemaType elementSchemaType = element.ElementSchemaType;
				if (xmlSchemaDatatype != null)
				{
					valueType = xmlSchemaDatatype.ValueType;
					typeName = element.SchemaTypeName.Name;
				}
				else if (elementSchemaType != null)
				{
					if (elementSchemaType is XmlSchemaSimpleType)
					{
						XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)elementSchemaType;
						if (xmlSchemaSimpleType.Name == "uuid")
						{
							valueType = typeof(Guid);
							typeName = xmlSchemaSimpleType.Name;
						}
					}
					else if (elementSchemaType is XmlSchemaComplexType)
					{
						XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)elementSchemaType;
						if (xmlSchemaComplexType.Name == "xmlDocument")
						{
							valueType = typeof(string);
							typeName = xmlSchemaComplexType.Name;
						}
					}
				}
				if (element.MaxOccursString == "unbounded")
				{
					valueType = Array.CreateInstance(valueType, 0).GetType();
					typeName = valueType.ToString();
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B3EE File Offset: 0x000095EE
		public static bool IsNullContentElement(XmlReader reader)
		{
			return reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance") == "true";
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B40A File Offset: 0x0000960A
		public static void CheckException(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B418 File Offset: 0x00009618
		public static bool IsNestedRowsetColumn(XmlSchemaElement xmlSchemaElement)
		{
			if (xmlSchemaElement != null && xmlSchemaElement.SchemaType is XmlSchemaComplexType && ((XmlSchemaComplexType)xmlSchemaElement.SchemaType).Particle is XmlSchemaSequence)
			{
				using (XmlSchemaObjectEnumerator enumerator = (((XmlSchemaComplexType)xmlSchemaElement.SchemaType).Particle as XmlSchemaSequence).Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!(enumerator.Current is XmlSchemaElement))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static void LoadSchema(XmlReader xmlReader, ColumnDefinitionDelegate definitionDelegate)
		{
			FormattersHelpers.LoadSchema(xmlReader, definitionDelegate, false);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B4BC File Offset: 0x000096BC
		public static void LoadSchema(XmlReader xmlReader, ColumnDefinitionDelegate definitionDelegate, bool requirePrePostInit)
		{
			try
			{
				xmlReader.MoveToContent();
				XmlaReader xmlaReader = xmlReader as XmlaReader;
				XmlSchema xmlSchema;
				if (xmlaReader != null)
				{
					xmlSchema = xmlaReader.ReadSchema();
				}
				else
				{
					xmlSchema = XmlSchema.Read(xmlReader, null);
					xmlReader.ReadEndElement();
				}
				xmlReader.MoveToContent();
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				xmlSchemaSet.Add(xmlSchema);
				xmlSchemaSet.Compile();
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
				{
					if (xmlSchemaObject is XmlSchemaComplexType)
					{
						XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)xmlSchemaObject;
						if (xmlSchemaComplexType.Name == FormattersHelpers.RowElement)
						{
							FormattersHelpers.LoadComplexType(xmlSchemaComplexType, definitionDelegate, null, 0, requirePrePostInit);
							break;
						}
					}
				}
			}
			catch (XmlSchemaException ex)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B59C File Offset: 0x0000979C
		internal static bool GetConvertToLocalTime(DataColumn column)
		{
			return column != null && column.ExtendedProperties.ContainsKey("convertTolocalTime") && (bool)column.ExtendedProperties["convertTolocalTime"];
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B5CA File Offset: 0x000097CA
		internal static void SetConvertToLocalTime(DataColumn column, bool convert)
		{
			if (column != null)
			{
				column.ExtendedProperties["convertTolocalTime"] = convert;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B5E5 File Offset: 0x000097E5
		internal static string GetColumnNameHashtableKey(DataTable table, string columnName)
		{
			return string.Format("{0}-{1}", table.TableName, columnName);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B5F8 File Offset: 0x000097F8
		private static object ConvertFromXml(string xmlValue, Type targetType, bool convertLocalTime)
		{
			object obj;
			try
			{
				if (!convertLocalTime || targetType != FormattersHelpers.dateTimeType)
				{
					obj = ((FormattersHelpers.GetValueDelegate)FormattersHelpers.typeConvertersHash[targetType])(xmlValue);
				}
				else
				{
					obj = ((DateTime)((FormattersHelpers.GetValueDelegate)FormattersHelpers.typeConvertersHash[targetType])(xmlValue)).ToLocalTime();
				}
			}
			catch (ArgumentException obj)
			{
			}
			catch (ArithmeticException obj)
			{
			}
			catch (FormatException obj)
			{
			}
			return obj;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B68C File Offset: 0x0000988C
		private static Type GetElementTypeInternal(XmlReader reader, string theNamespace)
		{
			Type type = null;
			string attribute = reader.GetAttribute("type", theNamespace);
			if (attribute != null)
			{
				string[] array = attribute.Split(new char[] { ':' });
				if (array.Length == 2)
				{
					if (!(reader.LookupNamespace(array[0]) == "http://www.w3.org/2001/XMLSchema"))
					{
						throw new AdomdUnknownResponseException(XmlaSR.UnexpectedXsiType(attribute), "");
					}
					type = XmlaTypeHelper.GetNetType(array[1]);
				}
				else
				{
					type = XmlaTypeHelper.GetNetType(attribute);
				}
				if (type == null)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnexpectedXsiType(attribute), "");
				}
			}
			return type;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B718 File Offset: 0x00009918
		private static XmlAttribute GetUnhandledAttributeByName(XmlSchemaElement element, string name)
		{
			for (int i = 0; i < element.UnhandledAttributes.Length; i++)
			{
				if (element.UnhandledAttributes[i].LocalName == name)
				{
					return element.UnhandledAttributes[i];
				}
			}
			return null;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B758 File Offset: 0x00009958
		private static string ReadPropertyXml(XmlReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			XmlaReader xmlaReader = reader as XmlaReader;
			bool flag = true;
			try
			{
				if (xmlaReader != null)
				{
					flag = xmlaReader.SkipElements;
					xmlaReader.SkipElements = false;
				}
				while (reader.IsStartElement())
				{
					stringBuilder.Append(reader.ReadOuterXml());
				}
			}
			finally
			{
				if (xmlaReader != null)
				{
					xmlaReader.SkipElements = flag;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B7C0 File Offset: 0x000099C0
		private static IDisposable GetWhitespaceHandlingRestorer(XmlReader reader, WhitespaceHandling handling)
		{
			XmlaReader xmlaReader = reader as XmlaReader;
			if (xmlaReader != null)
			{
				return xmlaReader.GetWhitespaceHandlingRestorer(handling);
			}
			return FormattersHelpers.emptyRestorer;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B7E4 File Offset: 0x000099E4
		private static string ReadWhiteSpace(XmlReader reader)
		{
			string text = string.Empty;
			while (reader.NodeType == XmlNodeType.Whitespace || reader.NodeType == XmlNodeType.SignificantWhitespace)
			{
				text += reader.Value;
				reader.Read();
			}
			return text;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B824 File Offset: 0x00009A24
		private static void LoadComplexType(XmlSchemaComplexType complexType, ColumnDefinitionDelegate definitionDelegate, object parent, int ordinal, bool requirePrePostInit)
		{
			int num = ordinal;
			XmlSchemaSequence xmlSchemaSequence = (XmlSchemaSequence)complexType.Particle;
			if (requirePrePostInit)
			{
				definitionDelegate(-1, null, null, null, null, false, xmlSchemaSequence.Items.Count, null);
			}
			foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSequence.Items)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
				string text = ((xmlSchemaElement.Name != null) ? xmlSchemaElement.Name : xmlSchemaElement.QualifiedName.Name);
				string columnCaptionFromSchemaElement = FormattersHelpers.GetColumnCaptionFromSchemaElement(xmlSchemaElement);
				string @namespace = xmlSchemaElement.QualifiedName.Namespace;
				if (FormattersHelpers.IsNestedRowsetColumn(xmlSchemaElement))
				{
					object obj = definitionDelegate(num, text, @namespace, columnCaptionFromSchemaElement, null, true, parent, null);
					ColumnDefinitionDelegate columnDefinitionDelegate = obj as ColumnDefinitionDelegate;
					if (columnDefinitionDelegate == null)
					{
						columnDefinitionDelegate = definitionDelegate;
					}
					else
					{
						obj = parent;
					}
					FormattersHelpers.LoadComplexType((XmlSchemaComplexType)xmlSchemaElement.ElementSchemaType, columnDefinitionDelegate, obj, 0, requirePrePostInit);
				}
				else
				{
					Type type = null;
					string text2 = null;
					FormattersHelpers.GetSchemaElementTypeAndName(xmlSchemaElement, out type, out text2);
					definitionDelegate(num, text, @namespace, columnCaptionFromSchemaElement, type, false, parent, text2);
				}
				num++;
			}
			if (requirePrePostInit)
			{
				definitionDelegate(-2, null, null, null, null, false, xmlSchemaSequence.Items.Count, null);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B978 File Offset: 0x00009B78
		private static void FillInTypeConvertersHash()
		{
			FormattersHelpers.typeConvertersHash = new Hashtable(19);
			FormattersHelpers.typeConvertersHash[typeof(bool)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToBoolean);
			FormattersHelpers.typeConvertersHash[typeof(byte)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToByte);
			FormattersHelpers.typeConvertersHash[typeof(char)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToChar);
			FormattersHelpers.typeConvertersHash[typeof(DateTime)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToDateTime);
			FormattersHelpers.typeConvertersHash[typeof(decimal)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToDecimal);
			FormattersHelpers.typeConvertersHash[typeof(double)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToDouble);
			FormattersHelpers.typeConvertersHash[typeof(Guid)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToGuid);
			FormattersHelpers.typeConvertersHash[typeof(short)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToInt16);
			FormattersHelpers.typeConvertersHash[typeof(int)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToInt32);
			FormattersHelpers.typeConvertersHash[typeof(long)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToInt64);
			FormattersHelpers.typeConvertersHash[typeof(sbyte)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToSByte);
			FormattersHelpers.typeConvertersHash[typeof(float)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToSingle);
			FormattersHelpers.typeConvertersHash[typeof(string)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToString);
			FormattersHelpers.typeConvertersHash[typeof(TimeSpan)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToTimeSpan);
			FormattersHelpers.typeConvertersHash[typeof(ushort)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToUInt16);
			FormattersHelpers.typeConvertersHash[typeof(uint)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToUInt32);
			FormattersHelpers.typeConvertersHash[typeof(ulong)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToUInt64);
			FormattersHelpers.typeConvertersHash[typeof(byte[])] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToBase64);
			FormattersHelpers.typeConvertersHash[typeof(object)] = new FormattersHelpers.GetValueDelegate(FormattersHelpers.ConvertToObject);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		private static void FillInStringConvertersHash()
		{
			FormattersHelpers.stringConvertersHash = new Hashtable(17);
			FormattersHelpers.stringConvertersHash[typeof(bool)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromBoolean);
			FormattersHelpers.stringConvertersHash[typeof(byte)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromByte);
			FormattersHelpers.stringConvertersHash[typeof(char)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromChar);
			FormattersHelpers.stringConvertersHash[typeof(DateTime)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromDateTime);
			FormattersHelpers.stringConvertersHash[typeof(decimal)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromDecimal);
			FormattersHelpers.stringConvertersHash[typeof(double)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromDouble);
			FormattersHelpers.stringConvertersHash[typeof(Guid)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromGuid);
			FormattersHelpers.stringConvertersHash[typeof(short)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromInt16);
			FormattersHelpers.stringConvertersHash[typeof(int)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromInt32);
			FormattersHelpers.stringConvertersHash[typeof(long)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromInt64);
			FormattersHelpers.stringConvertersHash[typeof(sbyte)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromSByte);
			FormattersHelpers.stringConvertersHash[typeof(float)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromSingle);
			FormattersHelpers.stringConvertersHash[typeof(TimeSpan)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromTimeSpan);
			FormattersHelpers.stringConvertersHash[typeof(ushort)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromUInt16);
			FormattersHelpers.stringConvertersHash[typeof(uint)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromUInt32);
			FormattersHelpers.stringConvertersHash[typeof(ulong)] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromUInt64);
			FormattersHelpers.stringConvertersHash[typeof(byte[])] = new FormattersHelpers.CovertToStringDelegate(FormattersHelpers.ConvertFromBase64);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000BE2D File Offset: 0x0000A02D
		private static object ConvertToBoolean(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000BE3A File Offset: 0x0000A03A
		private static object ConvertToByte(string s)
		{
			return XmlConvert.ToByte(s);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000BE47 File Offset: 0x0000A047
		private static object ConvertToChar(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000BE54 File Offset: 0x0000A054
		private static object ConvertToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000BE62 File Offset: 0x0000A062
		private static object ConvertToDecimal(string s)
		{
			return XmlConvert.ToDecimal(s);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000BE6F File Offset: 0x0000A06F
		private static object ConvertToDouble(string s)
		{
			return XmlConvert.ToDouble(s);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000BE7C File Offset: 0x0000A07C
		private static object ConvertToGuid(string s)
		{
			return XmlConvert.ToGuid(s);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000BE89 File Offset: 0x0000A089
		private static object ConvertToInt16(string s)
		{
			return XmlConvert.ToInt16(s);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000BE96 File Offset: 0x0000A096
		private static object ConvertToInt32(string s)
		{
			return XmlConvert.ToInt32(s);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BEA3 File Offset: 0x0000A0A3
		private static object ConvertToInt64(string s)
		{
			return XmlConvert.ToInt64(s);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		private static object ConvertToSByte(string s)
		{
			return XmlConvert.ToSByte(s);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000BEBD File Offset: 0x0000A0BD
		private static object ConvertToSingle(string s)
		{
			return XmlConvert.ToSingle(s);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BECA File Offset: 0x0000A0CA
		private static object ConvertToString(string s)
		{
			return s;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BECD File Offset: 0x0000A0CD
		private static object ConvertToTimeSpan(string s)
		{
			return XmlConvert.ToTimeSpan(s);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BEDA File Offset: 0x0000A0DA
		private static object ConvertToUInt16(string s)
		{
			return XmlConvert.ToUInt16(s);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BEE7 File Offset: 0x0000A0E7
		private static object ConvertToUInt32(string s)
		{
			return XmlConvert.ToUInt32(s);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		private static object ConvertToUInt64(string s)
		{
			return XmlConvert.ToUInt64(s);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BF01 File Offset: 0x0000A101
		private static object ConvertToBase64(string s)
		{
			return Convert.FromBase64String(s);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000BF09 File Offset: 0x0000A109
		private static object ConvertToObject(string s)
		{
			return s;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BF0C File Offset: 0x0000A10C
		private static string ConvertFromBoolean(object val)
		{
			return XmlConvert.ToString((bool)val);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BF19 File Offset: 0x0000A119
		private static string ConvertFromByte(object val)
		{
			return XmlConvert.ToString((byte)val);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BF26 File Offset: 0x0000A126
		private static string ConvertFromChar(object val)
		{
			return XmlConvert.ToString((char)val);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BF33 File Offset: 0x0000A133
		private static string ConvertFromDateTime(object val)
		{
			return XmlConvert.ToString((DateTime)val, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BF41 File Offset: 0x0000A141
		private static string ConvertFromDecimal(object val)
		{
			return XmlConvert.ToString((decimal)val);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BF4E File Offset: 0x0000A14E
		private static string ConvertFromDouble(object val)
		{
			return XmlConvert.ToString((double)val);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BF5B File Offset: 0x0000A15B
		private static string ConvertFromGuid(object val)
		{
			return XmlConvert.ToString((Guid)val);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BF68 File Offset: 0x0000A168
		private static string ConvertFromInt16(object val)
		{
			return XmlConvert.ToString((short)val);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BF75 File Offset: 0x0000A175
		private static string ConvertFromInt32(object val)
		{
			return XmlConvert.ToString((int)val);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BF82 File Offset: 0x0000A182
		private static string ConvertFromInt64(object val)
		{
			return XmlConvert.ToString((long)val);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BF8F File Offset: 0x0000A18F
		private static string ConvertFromSByte(object val)
		{
			return XmlConvert.ToString((sbyte)val);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BF9C File Offset: 0x0000A19C
		private static string ConvertFromSingle(object val)
		{
			return XmlConvert.ToString((float)val);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BFA9 File Offset: 0x0000A1A9
		private static string ConvertFromTimeSpan(object val)
		{
			return XmlConvert.ToString((TimeSpan)val);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BFB6 File Offset: 0x0000A1B6
		private static string ConvertFromUInt16(object val)
		{
			return XmlConvert.ToString((ushort)val);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000BFC3 File Offset: 0x0000A1C3
		private static string ConvertFromUInt32(object val)
		{
			return XmlConvert.ToString((uint)val);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		private static string ConvertFromUInt64(object val)
		{
			return XmlConvert.ToString((ulong)val);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BFDD File Offset: 0x0000A1DD
		private static string ConvertFromBase64(object val)
		{
			return Convert.ToBase64String((byte[])val);
		}

		// Token: 0x040001DE RID: 478
		public const string GuidColumnType = "uuid";

		// Token: 0x040001DF RID: 479
		public const string XmlDocumentColumnType = "xmlDocument";

		// Token: 0x040001E0 RID: 480
		public const string XmlDocumentColumnDataset = "urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset";

		// Token: 0x040001E1 RID: 481
		public const string BinaryDataTypeName = "base64Binary";

		// Token: 0x040001E2 RID: 482
		public const int PreFirstColumn = -1;

		// Token: 0x040001E3 RID: 483
		public const int PostLastColumn = -2;

		// Token: 0x040001E4 RID: 484
		public static string RowElement = "row";

		// Token: 0x040001E5 RID: 485
		public static string RowElementNamespace = "urn:schemas-microsoft-com:xml-analysis:rowset";

		// Token: 0x040001E6 RID: 486
		private const string typeKeyStr = "type";

		// Token: 0x040001E7 RID: 487
		private const string namespaceKeyStr = "namespace";

		// Token: 0x040001E8 RID: 488
		private const string xsdTypeNameKeyStr = "xsdTypeName";

		// Token: 0x040001E9 RID: 489
		private const string convertTolocalTimeStr = "convertTolocalTime";

		// Token: 0x040001EA RID: 490
		private const string typeAttribute = "type";

		// Token: 0x040001EB RID: 491
		private const string occurenceUnbounded = "unbounded";

		// Token: 0x040001EC RID: 492
		private const string fieldAttributeName = "field";

		// Token: 0x040001ED RID: 493
		private const string nilAttributeName = "nil";

		// Token: 0x040001EE RID: 494
		private const string trueConstant = "true";

		// Token: 0x040001EF RID: 495
		private static readonly FormattersHelpers.EmptyRestorer emptyRestorer = new FormattersHelpers.EmptyRestorer();

		// Token: 0x040001F0 RID: 496
		private static readonly Type dateTimeType = typeof(DateTime);

		// Token: 0x040001F1 RID: 497
		private static Hashtable typeConvertersHash = null;

		// Token: 0x040001F2 RID: 498
		private static Hashtable stringConvertersHash = null;

		// Token: 0x0200017E RID: 382
		// (Invoke) Token: 0x0600119F RID: 4511
		private delegate object GetValueDelegate(string s);

		// Token: 0x0200017F RID: 383
		// (Invoke) Token: 0x060011A3 RID: 4515
		private delegate string CovertToStringDelegate(object val);

		// Token: 0x02000180 RID: 384
		private class EmptyRestorer : Disposable
		{
		}
	}
}
