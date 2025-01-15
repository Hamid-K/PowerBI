using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000003 RID: 3
	internal sealed class DesignXmlReader
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public DesignXmlReader(XmlAttributeOverrides attributesOverrides, DesignXmlSerializerOptions options, bool dontForceSiteName)
		{
			this.attributesOverrides = attributesOverrides;
			this.dontForceSiteName = dontForceSiteName;
			this.mapper = new TypeMapper(attributesOverrides, options, null);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207F File Offset: 0x0000027F
		public DesignXmlReader(XmlAttributeOverrides attributesOverrides, bool dontForceSiteName)
			: this(attributesOverrides, DesignXmlSerializerOptions.Default, dontForceSiteName)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208A File Offset: 0x0000028A
		public object DeserializeComponent(IDesignerSerializationManager manager, XmlReader reader, Type root)
		{
			this.manager = manager;
			this.reader = reader;
			this.deserializationCallbackList.Clear();
			object obj = this.ReadRoot(root);
			this.manager = null;
			this.CallOnDeserialization();
			return obj;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020BC File Offset: 0x000002BC
		private void CallOnDeserialization()
		{
			for (int i = this.deserializationCallbackList.Count - 1; i >= 0; i--)
			{
				((IDeserializationCallback)this.deserializationCallbackList[i]).OnDeserialization(null);
			}
			this.deserializationCallbackList.Clear();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		private object ReadRoot(Type type)
		{
			this.CheckForXmlaException();
			object[] customAttributes = type.GetCustomAttributes(typeof(XmlRootAttribute), true);
			if (customAttributes.Length != 0)
			{
				XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)customAttributes[0];
				string text = xmlRootAttribute.ElementName;
				string @namespace = xmlRootAttribute.Namespace;
				if (text == null || text.Length == 0)
				{
					text = type.Name;
				}
				if ((@namespace == null || @namespace.Length == 0) ? (!this.reader.IsStartElement(text)) : (!this.reader.IsStartElement(text, @namespace)))
				{
					throw this.CreateXmlSerializationException(SR.Deserialization_UnexpectedRoot(this.reader.Name, this.reader.NamespaceURI, type.FullName), null);
				}
			}
			object obj;
			try
			{
				obj = this.ReadObject(type, null);
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException && innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
				}
				string text2;
				if (innerException is TargetInvocationException)
				{
					MethodBase targetSite = ((TargetInvocationException)innerException).TargetSite;
					text2 = SR.Deserialization_FailedInMethod((targetSite != null) ? (targetSite.DeclaringType.Name + "." + targetSite.Name) : null);
				}
				else
				{
					text2 = SR.Deserialization_Failed(innerException.Message);
				}
				throw this.CreateXmlSerializationException(text2, innerException);
			}
			return obj;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000225C File Offset: 0x0000045C
		private object ReadObject(Type proposedType, MemberMapping mm)
		{
			XmlAttributes xmlAttributes = ((mm != null) ? mm.XmlAttributes : null);
			Type type = proposedType;
			this.mapper.IncludeType(proposedType);
			if (this.reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance") == "true")
			{
				return this.ReadNil();
			}
			string attribute = this.reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null)
			{
				Type includedType = this.mapper.GetIncludedType(attribute, this.reader);
				if (includedType == null)
				{
					throw new XmlSerializationException(SR.Serialization_UnexpectedType(attribute));
				}
				if (!proposedType.IsAssignableFrom(includedType))
				{
					throw new XmlSerializationException(SR.Serialization_InvalidType(attribute));
				}
				type = includedType;
			}
			else if (type == typeof(object))
			{
				type = typeof(string);
			}
			if (this.IsPrimitiveType(type))
			{
				return this.ReadPrimitive(type, xmlAttributes);
			}
			if (typeof(XmlNode).IsAssignableFrom(type))
			{
				return this.ReadXmlNode();
			}
			return this.ReadClassObject(type, mm);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002356 File Offset: 0x00000556
		private object ReadNil()
		{
			this.reader.Skip();
			return null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002364 File Offset: 0x00000564
		private object ReadObjectContent(object value, Type type)
		{
			TypeMapping typeModel = this.mapper.GetTypeModel(type, value);
			return this.ReadObjectContent(value, type, typeModel);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002388 File Offset: 0x00000588
		private object ReadObjectContent(object value, Type type, TypeMapping mapping)
		{
			this.CheckForXmlaException();
			ArrayMapping arrayMapping = mapping as ArrayMapping;
			if (arrayMapping != null)
			{
				this.ReadArrayContent(value, arrayMapping);
			}
			else
			{
				StructMapping structMapping = mapping as StructMapping;
				if (structMapping != null)
				{
					this.ReadStructContent(value, structMapping);
				}
				else
				{
					if (!(mapping is SpecialMapping))
					{
						throw new InvalidOperationException(SR.Serialization_UnexpectedMapping);
					}
					this.ReadSpecialContent(value);
				}
			}
			return value;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023E3 File Offset: 0x000005E3
		private object ReadObject(MemberMapping mapping)
		{
			return this.ReadObject(mapping.MemberType, mapping);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023F4 File Offset: 0x000005F4
		private object ReadPrimitive(Type type, XmlAttributes attributes)
		{
			this.reader.MoveToContent();
			if (this.reader.IsEmptyElement)
			{
				this.reader.Skip();
				return null;
			}
			string text = null;
			if (attributes != null && attributes.XmlElements != null && attributes.XmlElements.Count > 0)
			{
				text = attributes.XmlElements[0].DataType;
			}
			this.reader.ReadStartElement();
			string text2 = string.Empty;
			if (this.reader.MoveToContent() == XmlNodeType.Text)
			{
				text2 = this.reader.Value;
				this.reader.Skip();
			}
			this.CheckForXmlaException();
			this.reader.ReadEndElement();
			if (text == "NMTOKEN")
			{
				text2 = XmlConvert.DecodeName(text2);
			}
			else if (text == "NCName")
			{
				text2 = XmlConvert.DecodeName(text2);
			}
			if (type.IsPrimitive || type == typeof(string))
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					return XmlConvert.ToBoolean(text2);
				case TypeCode.Char:
					return XmlConvert.ToChar(text2);
				case TypeCode.SByte:
					return XmlConvert.ToSByte(text2);
				case TypeCode.Byte:
					return XmlConvert.ToByte(text2);
				case TypeCode.Int16:
					return XmlConvert.ToInt16(text2);
				case TypeCode.UInt16:
					return XmlConvert.ToUInt16(text2);
				case TypeCode.Int32:
					return XmlConvert.ToInt32(text2);
				case TypeCode.UInt32:
					return XmlConvert.ToUInt32(text2);
				case TypeCode.Int64:
					return XmlConvert.ToInt64(text2);
				case TypeCode.UInt64:
					return XmlConvert.ToUInt64(text2);
				case TypeCode.Single:
					return XmlConvert.ToSingle(text2);
				case TypeCode.Double:
					return XmlConvert.ToDouble(text2);
				case TypeCode.Decimal:
					return XmlConvert.ToDecimal(text2);
				case TypeCode.String:
					return text2;
				}
				throw new XmlSerializationException(SR.Serialization_UnexpectedPrimitive);
			}
			object obj;
			if (type.IsEnum)
			{
				obj = TypeMapper.ParseDeserializedEnumValue(type, text2);
			}
			else if (type == typeof(Guid))
			{
				obj = XmlConvert.ToGuid(text2);
			}
			else if (type == typeof(DateTime))
			{
				obj = XmlConvert.ToDateTime(text2, XmlDateTimeSerializationMode.Utc).ToLocalTime();
			}
			else
			{
				if (!(type == typeof(TimeSpan)))
				{
					throw new XmlSerializationException(SR.Serialization_UnexpectedPrimitive);
				}
				obj = this.mapper.ConvertToTimeSpan(text2);
			}
			return obj;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026B8 File Offset: 0x000008B8
		private object ReadClassObject(Type type, MemberMapping mm)
		{
			object obj;
			if (typeof(IComponent).IsAssignableFrom(type))
			{
				string attribute = this.reader.GetAttribute("design-time-name", "http://schemas.microsoft.com/DataWarehouse/Designer/1.0");
				obj = this.CreateInstance(type, attribute);
			}
			else
			{
				obj = this.CreateInstance(type);
			}
			if (mm != null)
			{
				TypeMapping typeModel = this.mapper.GetTypeModel(mm, obj);
				return this.ReadObjectContent(obj, type, typeModel);
			}
			return this.ReadObjectContent(obj, type);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002724 File Offset: 0x00000924
		private object ReadSpecialContent(object obj)
		{
			IXmlSerializable xmlSerializable = (IXmlSerializable)obj;
			if (xmlSerializable != null)
			{
				DataSet dataSet = obj as DataSet;
				if (dataSet != null)
				{
					dataSet.ReadXml(this.reader);
				}
				else
				{
					xmlSerializable.ReadXml(this.reader);
				}
				if (dataSet != null && this.manager != null)
				{
					string text = (string)dataSet.ExtendedProperties["design-time-name"];
					string name = this.manager.GetName(dataSet);
					if (text != null && text != name)
					{
						this.manager.SetName(dataSet, text);
					}
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable = (DataTable)obj2;
						this.manager.SetName(dataTable, (string)dataTable.ExtendedProperties["design-time-name"]);
						foreach (object obj3 in dataTable.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj3;
							this.manager.SetName(dataColumn, (string)dataColumn.ExtendedProperties["design-time-name"]);
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002894 File Offset: 0x00000A94
		private object ReadArrayContent(object array, ArrayMapping mapping)
		{
			IList list = array as IList;
			MethodInfo methodInfo;
			if (list == null)
			{
				methodInfo = array.GetType().GetMethod("Add", new Type[] { mapping.MemberType });
			}
			else
			{
				methodInfo = null;
			}
			string name = this.reader.Name;
			string namespaceURI = this.reader.NamespaceURI;
			if (this.reader.IsEmptyElement)
			{
				this.reader.Skip();
			}
			else
			{
				this.reader.ReadStartElement();
				this.CheckForXmlaException();
				string memberName = mapping.MemberName;
				string text = ((mapping.MemberNamespace != null) ? mapping.MemberNamespace : namespaceURI);
				while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.NodeType != XmlNodeType.None)
				{
					if (this.reader.NodeType == XmlNodeType.Element)
					{
						string localName = this.reader.LocalName;
						string namespaceURI2 = this.reader.NamespaceURI;
						if (!(localName == memberName) || !(namespaceURI2 == text))
						{
							throw this.CreateXmlSerializationException(SR.Serialization_UnexpectedElement(localName, namespaceURI2), null);
						}
						object obj = this.ReadObject(mapping.MemberType, null);
						if (list != null)
						{
							list.Add(obj);
						}
						else
						{
							methodInfo.Invoke(array, new object[] { obj });
						}
					}
					else
					{
						this.reader.Skip();
					}
					this.reader.MoveToContent();
					this.CheckForXmlaException();
				}
				this.reader.ReadEndElement();
			}
			return array;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000029FC File Offset: 0x00000BFC
		private object ReadStructContent(object obj, StructMapping mapping)
		{
			this.reader.MoveToContent();
			string name = this.reader.Name;
			string namespaceURI = this.reader.NamespaceURI;
			if (this.reader.IsEmptyElement)
			{
				this.reader.Skip();
			}
			else
			{
				this.reader.ReadStartElement();
				this.reader.MoveToContent();
				while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.NodeType != XmlNodeType.None)
				{
					this.CheckForXmlaException();
					string localName = this.reader.LocalName;
					string namespaceURI2 = this.reader.NamespaceURI;
					MemberMapping memberMapping = mapping.GetElement(localName, (namespaceURI == namespaceURI2) ? string.Empty : namespaceURI2);
					if (memberMapping == null)
					{
						memberMapping = mapping.GetElement(localName, namespaceURI2);
					}
					if (memberMapping == null)
					{
						throw this.CreateXmlSerializationException(SR.Serialization_UnexpectedElement(localName, namespaceURI2), null);
					}
					if (memberMapping.IsReadOnly)
					{
						if (!this.IsPrimitiveType(memberMapping.MemberType))
						{
							object value = memberMapping.GetValue(obj);
							if (value != null)
							{
								TypeMapping typeModel = this.mapper.GetTypeModel(memberMapping, value);
								this.ReadObjectContent(value, memberMapping.MemberType, typeModel);
							}
							else
							{
								this.reader.Skip();
							}
						}
						else
						{
							this.reader.Skip();
						}
					}
					else
					{
						object obj2 = this.ReadObject(memberMapping);
						if (obj2 != null)
						{
							memberMapping.SetValue(obj, obj2);
						}
					}
					this.reader.MoveToContent();
				}
				this.reader.ReadEndElement();
			}
			return obj;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002B68 File Offset: 0x00000D68
		private bool IsPrimitiveType(Type type)
		{
			return TypeMapper.IsPrimitiveType(type);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B70 File Offset: 0x00000D70
		private object CreateInstance(Type type)
		{
			return this.CreateInstance(type, null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B7C File Offset: 0x00000D7C
		private object CreateInstance(Type type, string name)
		{
			object obj;
			if (this.manager != null)
			{
				if (name != null && this.dontForceSiteName && this.manager.GetInstance(name) != null)
				{
					name = null;
				}
				obj = this.manager.CreateInstance(type, null, name, true);
			}
			else
			{
				obj = Activator.CreateInstance(type);
			}
			if (obj == null)
			{
				throw new InvalidOperationException(SR.Serialization_InternalError);
			}
			this.ProcessSerializationCallback(obj);
			return obj;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private void ProcessSerializationCallback(object obj)
		{
			this.ProcessSerializationCallback2(obj);
			IComponent component = obj as IComponent;
			if (component != null)
			{
				ISite site = component.Site;
				if (site != null)
				{
					IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null)
					{
						IDesigner designer = designerHost.GetDesigner(component);
						if (designer != null)
						{
							this.ProcessSerializationCallback2(designer);
						}
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C34 File Offset: 0x00000E34
		private void ProcessSerializationCallback2(object obj)
		{
			Type @interface = obj.GetType().GetInterface("IDeserializationStartCallback", false);
			if (null != @interface)
			{
				MethodInfo method = @interface.GetMethod("OnDeserializationBegin");
				if (null != method)
				{
					method.Invoke(obj, new object[1]);
				}
			}
			if (obj is IDeserializationCallback)
			{
				this.deserializationCallbackList.Add(obj);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C94 File Offset: 0x00000E94
		private void CheckForXmlaException()
		{
			if (this.reader.IsStartElement("Exception", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				throw this.CreateXmlSerializationException(null, null);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002CB8 File Offset: 0x00000EB8
		private XmlSerializationException CreateXmlSerializationException(string message, Exception innerException)
		{
			XmlSerializationException ex = new XmlSerializationException(message, innerException);
			IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				ex.LineNumber = xmlLineInfo.LineNumber;
				ex.LinePosition = xmlLineInfo.LinePosition;
			}
			return ex;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002CF8 File Offset: 0x00000EF8
		private XmlNode ReadXmlNode()
		{
			this.reader.MoveToContent();
			if (this.reader.IsEmptyElement)
			{
				this.reader.Skip();
				return null;
			}
			this.reader.ReadStartElement();
			this.reader.MoveToContent();
			XmlNodeType nodeType = this.reader.NodeType;
			XmlNode xmlNode = null;
			XmlDocument xmlDocument = new XmlDocument(this.reader.NameTable);
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType == XmlNodeType.Text)
				{
					string value = this.reader.Value;
					this.reader.Skip();
					this.CheckForXmlaException();
					xmlNode = xmlDocument.CreateTextNode(value);
				}
			}
			else
			{
				this.CheckForXmlaException();
				xmlDocument.LoadXml(this.reader.ReadOuterXml());
				this.CheckForXmlaException();
				xmlNode = xmlDocument.DocumentElement;
			}
			this.reader.ReadEndElement();
			return xmlNode;
		}

		// Token: 0x04000028 RID: 40
		private const string XmlaExceptionElement = "Exception";

		// Token: 0x04000029 RID: 41
		private const string XmlaExceptionNamespace = "urn:schemas-microsoft-com:xml-analysis:exception";

		// Token: 0x0400002A RID: 42
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x0400002B RID: 43
		private bool dontForceSiteName;

		// Token: 0x0400002C RID: 44
		private TypeMapper mapper;

		// Token: 0x0400002D RID: 45
		private IDesignerSerializationManager manager;

		// Token: 0x0400002E RID: 46
		private XmlReader reader;

		// Token: 0x0400002F RID: 47
		private ArrayList deserializationCallbackList = new ArrayList();
	}
}
