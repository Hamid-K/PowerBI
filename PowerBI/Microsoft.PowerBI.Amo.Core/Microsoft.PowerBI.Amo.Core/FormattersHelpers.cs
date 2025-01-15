using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200003F RID: 63
	internal static class FormattersHelpers
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000DD38 File Offset: 0x0000BF38
		static FormattersHelpers()
		{
			FormattersHelpers.FillInTypeConvertersHash();
			FormattersHelpers.FillInStringConvertersHash();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public static string ConvertToXml(object objVal)
		{
			FormattersHelpers.CovertToStringDelegate covertToStringDelegate = FormattersHelpers.stringConvertersHash[objVal.GetType()] as FormattersHelpers.CovertToStringDelegate;
			if (covertToStringDelegate != null)
			{
				return covertToStringDelegate(objVal);
			}
			return objVal.ToString();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000DDBC File Offset: 0x0000BFBC
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

		// Token: 0x060002C4 RID: 708 RVA: 0x0000DF6C File Offset: 0x0000C16C
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

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E040 File Offset: 0x0000C240
		public static Type GetElementType(XmlReader reader)
		{
			return FormattersHelpers.GetElementType(reader, null, typeof(object));
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E054 File Offset: 0x0000C254
		public static Type GetElementType(XmlReader reader, string theNamespace, Type typeIfNotSpecified)
		{
			Type type = FormattersHelpers.GetElementTypeInternal(reader, theNamespace);
			if (type == null)
			{
				type = typeIfNotSpecified;
			}
			return type;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E078 File Offset: 0x0000C278
		public static Type GetElementType(XmlReader reader, string theNamespace, DataColumn column)
		{
			Type type = FormattersHelpers.GetElementTypeInternal(reader, theNamespace);
			if (type == null)
			{
				type = FormattersHelpers.GetColumnType(column);
			}
			return type;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
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

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E0E2 File Offset: 0x0000C2E2
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

		// Token: 0x060002CA RID: 714 RVA: 0x0000E10C File Offset: 0x0000C30C
		public static string GetColumnNamespace(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			return column.ExtendedProperties["namespace"] as string;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E131 File Offset: 0x0000C331
		public static void SetColumnNamespace(DataColumn column, string theNamespace)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			column.ExtendedProperties["namespace"] = theNamespace;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E152 File Offset: 0x0000C352
		public static string GetColumnXsdTypeName(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			return column.ExtendedProperties["xsdTypeName"] as string;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E177 File Offset: 0x0000C377
		public static void SetColumnXsdTypeName(DataColumn column, string theXsdTypeName)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			column.ExtendedProperties["xsdTypeName"] = theXsdTypeName;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E198 File Offset: 0x0000C398
		public static string GetColumnCaptionFromSchemaElement(XmlSchemaElement element)
		{
			XmlAttribute unhandledAttributeByName = FormattersHelpers.GetUnhandledAttributeByName(element, "field");
			if (unhandledAttributeByName != null)
			{
				return unhandledAttributeByName.Value;
			}
			return element.Name;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
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

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E2EA File Offset: 0x0000C4EA
		public static bool IsNullContentElement(XmlReader reader)
		{
			return reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance") == "true";
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E306 File Offset: 0x0000C506
		public static void CheckException(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E314 File Offset: 0x0000C514
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

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public static void LoadSchema(XmlReader xmlReader, ColumnDefinitionDelegate definitionDelegate)
		{
			FormattersHelpers.LoadSchema(xmlReader, definitionDelegate, false);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
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
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E498 File Offset: 0x0000C698
		internal static bool GetConvertToLocalTime(DataColumn column)
		{
			return column != null && column.ExtendedProperties.ContainsKey("convertTolocalTime") && (bool)column.ExtendedProperties["convertTolocalTime"];
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E4C6 File Offset: 0x0000C6C6
		internal static void SetConvertToLocalTime(DataColumn column, bool convert)
		{
			if (column != null)
			{
				column.ExtendedProperties["convertTolocalTime"] = convert;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E4E1 File Offset: 0x0000C6E1
		internal static string GetColumnNameHashtableKey(DataTable table, string columnName)
		{
			return string.Format("{0}-{1}", table.TableName, columnName);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
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

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E588 File Offset: 0x0000C788
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
						throw new ResponseFormatException(XmlaSR.UnexpectedXsiType(attribute), "");
					}
					type = XmlaTypeHelper.GetNetType(array[1]);
				}
				else
				{
					type = XmlaTypeHelper.GetNetType(attribute);
				}
				if (type == null)
				{
					throw new ResponseFormatException(XmlaSR.UnexpectedXsiType(attribute), "");
				}
			}
			return type;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E614 File Offset: 0x0000C814
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

		// Token: 0x060002DB RID: 731 RVA: 0x0000E654 File Offset: 0x0000C854
		private static string ReadPropertyXml(XmlReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (reader.IsStartElement())
			{
				stringBuilder.Append(reader.ReadOuterXml());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E684 File Offset: 0x0000C884
		private static IDisposable GetWhitespaceHandlingRestorer(XmlReader reader, WhitespaceHandling handling)
		{
			XmlaReader xmlaReader = reader as XmlaReader;
			if (xmlaReader != null)
			{
				return xmlaReader.GetWhitespaceHandlingRestorer(handling);
			}
			return FormattersHelpers.emptyRestorer;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
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

		// Token: 0x060002DE RID: 734 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
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

		// Token: 0x060002DF RID: 735 RVA: 0x0000E83C File Offset: 0x0000CA3C
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

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
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

		// Token: 0x060002E1 RID: 737 RVA: 0x0000ECF1 File Offset: 0x0000CEF1
		private static object ConvertToBoolean(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000ECFE File Offset: 0x0000CEFE
		private static object ConvertToByte(string s)
		{
			return XmlConvert.ToByte(s);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000ED0B File Offset: 0x0000CF0B
		private static object ConvertToChar(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000ED18 File Offset: 0x0000CF18
		private static object ConvertToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000ED26 File Offset: 0x0000CF26
		private static object ConvertToDecimal(string s)
		{
			return XmlConvert.ToDecimal(s);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000ED33 File Offset: 0x0000CF33
		private static object ConvertToDouble(string s)
		{
			return XmlConvert.ToDouble(s);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000ED40 File Offset: 0x0000CF40
		private static object ConvertToGuid(string s)
		{
			return XmlConvert.ToGuid(s);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000ED4D File Offset: 0x0000CF4D
		private static object ConvertToInt16(string s)
		{
			return XmlConvert.ToInt16(s);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000ED5A File Offset: 0x0000CF5A
		private static object ConvertToInt32(string s)
		{
			return XmlConvert.ToInt32(s);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000ED67 File Offset: 0x0000CF67
		private static object ConvertToInt64(string s)
		{
			return XmlConvert.ToInt64(s);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000ED74 File Offset: 0x0000CF74
		private static object ConvertToSByte(string s)
		{
			return XmlConvert.ToSByte(s);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000ED81 File Offset: 0x0000CF81
		private static object ConvertToSingle(string s)
		{
			return XmlConvert.ToSingle(s);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		private static object ConvertToString(string s)
		{
			return s;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000ED91 File Offset: 0x0000CF91
		private static object ConvertToTimeSpan(string s)
		{
			return XmlConvert.ToTimeSpan(s);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000ED9E File Offset: 0x0000CF9E
		private static object ConvertToUInt16(string s)
		{
			return XmlConvert.ToUInt16(s);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000EDAB File Offset: 0x0000CFAB
		private static object ConvertToUInt32(string s)
		{
			return XmlConvert.ToUInt32(s);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		private static object ConvertToUInt64(string s)
		{
			return XmlConvert.ToUInt64(s);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		private static object ConvertToBase64(string s)
		{
			return Convert.FromBase64String(s);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000EDCD File Offset: 0x0000CFCD
		private static object ConvertToObject(string s)
		{
			return s;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		private static string ConvertFromBoolean(object val)
		{
			return XmlConvert.ToString((bool)val);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		private static string ConvertFromByte(object val)
		{
			return XmlConvert.ToString((byte)val);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EDEA File Offset: 0x0000CFEA
		private static string ConvertFromChar(object val)
		{
			return XmlConvert.ToString((char)val);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000EDF7 File Offset: 0x0000CFF7
		private static string ConvertFromDateTime(object val)
		{
			return XmlConvert.ToString((DateTime)val, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000EE05 File Offset: 0x0000D005
		private static string ConvertFromDecimal(object val)
		{
			return XmlConvert.ToString((decimal)val);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000EE12 File Offset: 0x0000D012
		private static string ConvertFromDouble(object val)
		{
			return XmlConvert.ToString((double)val);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000EE1F File Offset: 0x0000D01F
		private static string ConvertFromGuid(object val)
		{
			return XmlConvert.ToString((Guid)val);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000EE2C File Offset: 0x0000D02C
		private static string ConvertFromInt16(object val)
		{
			return XmlConvert.ToString((short)val);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EE39 File Offset: 0x0000D039
		private static string ConvertFromInt32(object val)
		{
			return XmlConvert.ToString((int)val);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000EE46 File Offset: 0x0000D046
		private static string ConvertFromInt64(object val)
		{
			return XmlConvert.ToString((long)val);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000EE53 File Offset: 0x0000D053
		private static string ConvertFromSByte(object val)
		{
			return XmlConvert.ToString((sbyte)val);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EE60 File Offset: 0x0000D060
		private static string ConvertFromSingle(object val)
		{
			return XmlConvert.ToString((float)val);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000EE6D File Offset: 0x0000D06D
		private static string ConvertFromTimeSpan(object val)
		{
			return XmlConvert.ToString((TimeSpan)val);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EE7A File Offset: 0x0000D07A
		private static string ConvertFromUInt16(object val)
		{
			return XmlConvert.ToString((ushort)val);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000EE87 File Offset: 0x0000D087
		private static string ConvertFromUInt32(object val)
		{
			return XmlConvert.ToString((uint)val);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EE94 File Offset: 0x0000D094
		private static string ConvertFromUInt64(object val)
		{
			return XmlConvert.ToString((ulong)val);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000EEA1 File Offset: 0x0000D0A1
		private static string ConvertFromBase64(object val)
		{
			return Convert.ToBase64String((byte[])val);
		}

		// Token: 0x04000223 RID: 547
		public const string GuidColumnType = "uuid";

		// Token: 0x04000224 RID: 548
		public const string XmlDocumentColumnType = "xmlDocument";

		// Token: 0x04000225 RID: 549
		public const string XmlDocumentColumnDataset = "urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset";

		// Token: 0x04000226 RID: 550
		public const string BinaryDataTypeName = "base64Binary";

		// Token: 0x04000227 RID: 551
		public const int PreFirstColumn = -1;

		// Token: 0x04000228 RID: 552
		public const int PostLastColumn = -2;

		// Token: 0x04000229 RID: 553
		public static string RowElement = "row";

		// Token: 0x0400022A RID: 554
		public static string RowElementNamespace = "urn:schemas-microsoft-com:xml-analysis:rowset";

		// Token: 0x0400022B RID: 555
		private const string typeKeyStr = "type";

		// Token: 0x0400022C RID: 556
		private const string namespaceKeyStr = "namespace";

		// Token: 0x0400022D RID: 557
		private const string xsdTypeNameKeyStr = "xsdTypeName";

		// Token: 0x0400022E RID: 558
		private const string convertTolocalTimeStr = "convertTolocalTime";

		// Token: 0x0400022F RID: 559
		private const string typeAttribute = "type";

		// Token: 0x04000230 RID: 560
		private const string occurenceUnbounded = "unbounded";

		// Token: 0x04000231 RID: 561
		private const string fieldAttributeName = "field";

		// Token: 0x04000232 RID: 562
		private const string nilAttributeName = "nil";

		// Token: 0x04000233 RID: 563
		private const string trueConstant = "true";

		// Token: 0x04000234 RID: 564
		private static readonly FormattersHelpers.EmptyRestorer emptyRestorer = new FormattersHelpers.EmptyRestorer();

		// Token: 0x04000235 RID: 565
		private static readonly Type dateTimeType = typeof(DateTime);

		// Token: 0x04000236 RID: 566
		private static Hashtable typeConvertersHash = null;

		// Token: 0x04000237 RID: 567
		private static Hashtable stringConvertersHash = null;

		// Token: 0x0200017A RID: 378
		// (Invoke) Token: 0x0600123D RID: 4669
		private delegate object GetValueDelegate(string s);

		// Token: 0x0200017B RID: 379
		// (Invoke) Token: 0x06001241 RID: 4673
		private delegate string CovertToStringDelegate(object val);

		// Token: 0x0200017C RID: 380
		private class EmptyRestorer : Disposable
		{
		}
	}
}
