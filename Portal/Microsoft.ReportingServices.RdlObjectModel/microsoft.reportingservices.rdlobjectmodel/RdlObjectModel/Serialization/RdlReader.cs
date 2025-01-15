using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D9 RID: 729
	public class RdlReader : RdlReaderWriterBase
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x00033759 File Offset: 0x00031959
		public RdlReader(RdlSerializerSettings settings)
			: base(settings)
		{
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00033785 File Offset: 0x00031985
		public object Deserialize(Stream stream, Type rootType)
		{
			this.m_reader = XmlReader.Create(stream, this.GetXmlReaderSettings());
			return this.Deserialize(rootType);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000337A0 File Offset: 0x000319A0
		public object Deserialize(TextReader textReader, Type rootType)
		{
			this.m_reader = XmlReader.Create(textReader, this.GetXmlReaderSettings());
			return this.Deserialize(rootType);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000337BB File Offset: 0x000319BB
		public object Deserialize(XmlReader xmlReader, Type rootType)
		{
			this.m_reader = XmlReader.Create(xmlReader, this.GetXmlReaderSettings());
			return this.Deserialize(rootType);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000337D8 File Offset: 0x000319D8
		private object Deserialize(Type rootType)
		{
			List<string> list = new List<string>(this.m_validNamespaces);
			if (this.m_schema != null)
			{
				list.Add(this.m_schema.TargetNamespace);
			}
			if (base.Settings.ValidateXml)
			{
				this.m_validator = new RdlValidator(this.m_reader, list);
			}
			object obj = this.ReadRoot(rootType);
			this.m_reader.Close();
			return obj;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0003383C File Offset: 0x00031A3C
		private XmlReaderSettings GetXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.CheckCharacters = false;
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.IgnoreProcessingInstructions = true;
			xmlReaderSettings.IgnoreWhitespace = base.Settings.IgnoreWhitespace;
			if (base.Settings.ValidateXml)
			{
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				XmlSchema xmlSchema = XmlUtils.LoadSchemaFromResourceWithNullResolver("Microsoft.ReportingServices.RdlObjectModel.Serialization.ReportDefinition.xsd");
				xmlReaderSettings.Schemas.Add(xmlSchema);
				if (base.Settings.XmlSchema != null)
				{
					if (base.Settings.XmlSchema.TargetNamespace.EndsWith("/reportdefinition", StringComparison.Ordinal))
					{
						this.m_schema = base.Settings.XmlSchema;
					}
					xmlReaderSettings.Schemas.Add(base.Settings.XmlSchema);
				}
				if (this.m_schema == null)
				{
					this.m_schema = xmlSchema;
				}
				if (base.Settings.XmlValidationEventHandler != null)
				{
					xmlReaderSettings.ValidationEventHandler += base.Settings.XmlValidationEventHandler;
				}
			}
			return xmlReaderSettings;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00033924 File Offset: 0x00031B24
		private object ReadRoot(Type type)
		{
			object obj;
			try
			{
				this.m_reader.MoveToContent();
				TypeMapping typeMapping = TypeMapper.GetTypeMapping(type);
				if (this.m_reader.NamespaceURI != typeMapping.Namespace)
				{
					throw new XmlException(SRErrors.NoRootElement);
				}
				obj = this.ReadObject(type, null, 0);
			}
			catch (XmlException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException && innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
				}
				string text;
				if (innerException is TargetInvocationException)
				{
					MethodBase targetSite = ((TargetInvocationException)innerException).TargetSite;
					text = SRErrorsWrapper.DeserializationFailedMethod((targetSite != null) ? (targetSite.DeclaringType.Name + "." + targetSite.Name) : null);
				}
				else
				{
					text = SRErrorsWrapper.DeserializationFailed(innerException.Message);
				}
				IXmlLineInfo xmlLineInfo = this.m_reader as IXmlLineInfo;
				XmlException ex;
				if (xmlLineInfo != null)
				{
					ex = new XmlException(text, innerException, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
				else
				{
					ex = new XmlException(text, innerException);
				}
				throw ex;
			}
			return obj;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00033A38 File Offset: 0x00031C38
		private object ReadObject(Type type, MemberMapping member, int nestingLevel)
		{
			this.ValidateStartElement();
			object obj;
			if (TypeMapper.IsPrimitiveType(type))
			{
				obj = this.ReadPrimitive(type);
			}
			else
			{
				obj = this.ReadClassObject(type, member, nestingLevel);
			}
			this.ValidateEndElement();
			return obj;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00033A70 File Offset: 0x00031C70
		private object ReadObjectContent(object value, MemberMapping member, int nestingLevel)
		{
			TypeMapping typeMapping = TypeMapper.GetTypeMapping(value.GetType());
			if (typeMapping is ArrayMapping)
			{
				this.ReadArrayContent(value, (ArrayMapping)typeMapping, member, nestingLevel);
			}
			else if (typeMapping is StructMapping)
			{
				this.ReadStructContent(value, (StructMapping)typeMapping);
			}
			else if (typeMapping is SpecialMapping)
			{
				this.ReadSpecialContent(value);
			}
			else
			{
				this.m_reader.Skip();
			}
			if (base.Host != null)
			{
				base.Host.OnDeserialization(value);
			}
			return value;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00033AEC File Offset: 0x00031CEC
		private object ReadPrimitive(Type type)
		{
			object obj = null;
			string text = this.m_reader.ReadString();
			if (type.IsPrimitive)
			{
				TypeCode typeCode = Type.GetTypeCode(type);
				if (typeCode != TypeCode.Boolean)
				{
					switch (typeCode)
					{
					case TypeCode.Int16:
						obj = XmlConvert.ToInt16(text);
						break;
					case TypeCode.Int32:
						obj = XmlConvert.ToInt32(text);
						break;
					case TypeCode.Int64:
						obj = XmlConvert.ToInt64(text);
						break;
					case TypeCode.Single:
						obj = XmlConvert.ToSingle(text);
						break;
					case TypeCode.Double:
						obj = XmlConvert.ToDouble(text);
						break;
					}
				}
				else
				{
					obj = XmlConvert.ToBoolean(text);
				}
			}
			else if (type == typeof(string))
			{
				obj = text;
				if (base.Settings.Normalize)
				{
					obj = Regex.Replace(text, "(?<!\r)\n", "\r\n");
				}
			}
			else if (type.IsEnum)
			{
				obj = Enum.Parse(type, text, true);
			}
			else if (type == typeof(Guid))
			{
				obj = new Guid(text);
			}
			else if (type == typeof(DateTime))
			{
				obj = XmlCustomFormatter.ToDateTime(text);
			}
			this.m_reader.Skip();
			return obj;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00033C44 File Offset: 0x00031E44
		private object ReadClassObject(Type type, MemberMapping member, int nestingLevel)
		{
			type = base.GetSerializationType(type);
			object obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
			this.ReadObjectContent(obj, member, nestingLevel);
			return obj;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00033C74 File Offset: 0x00031E74
		private object ReadSpecialContent(object obj)
		{
			IXmlSerializable xmlSerializable = (IXmlSerializable)obj;
			if (xmlSerializable != null)
			{
				xmlSerializable.ReadXml(this.m_reader);
			}
			return obj;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00033C98 File Offset: 0x00031E98
		private object ReadArrayContent(object array, ArrayMapping mapping, MemberMapping member, int nestingLevel)
		{
			IList list = (IList)array;
			if (this.m_reader.IsEmptyElement)
			{
				this.m_reader.Skip();
			}
			else
			{
				this.m_reader.ReadStartElement();
				this.m_reader.MoveToContent();
				while (this.m_reader.NodeType != XmlNodeType.EndElement && this.m_reader.NodeType != XmlNodeType.None)
				{
					if (this.m_reader.NodeType == XmlNodeType.Element)
					{
						string localName = this.m_reader.LocalName;
						string namespaceURI = this.m_reader.NamespaceURI;
						Type type = null;
						bool flag = false;
						if (member != null && member.XmlAttributes.XmlArrayItems.Count > nestingLevel)
						{
							if (localName == member.XmlAttributes.XmlArrayItems[nestingLevel].ElementName)
							{
								XmlArrayItemAttribute xmlArrayItemAttribute = member.XmlAttributes.XmlArrayItems[nestingLevel];
								type = xmlArrayItemAttribute.Type;
								flag = xmlArrayItemAttribute.IsNullable;
							}
						}
						else
						{
							XmlElementAttributes xmlElementAttributes = null;
							if (base.XmlOverrides != null)
							{
								XmlAttributes xmlAttributes = base.XmlOverrides[mapping.ItemType];
								if (xmlAttributes != null && xmlAttributes.XmlElements != null)
								{
									xmlElementAttributes = xmlAttributes.XmlElements;
								}
							}
							if (xmlElementAttributes == null)
							{
								mapping.ElementTypes.TryGetValue(localName, out type);
							}
							else
							{
								foreach (object obj in xmlElementAttributes)
								{
									XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj;
									if (localName == xmlElementAttribute.ElementName)
									{
										type = xmlElementAttribute.Type;
										break;
									}
								}
							}
						}
						if (type != null)
						{
							object obj2;
							if (flag && this.m_reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance") == "true")
							{
								this.m_reader.Skip();
								obj2 = null;
							}
							else
							{
								obj2 = this.ReadObject(type, member, nestingLevel + 1);
							}
							list.Add(obj2);
						}
						else
						{
							this.m_reader.Skip();
						}
					}
					else
					{
						this.m_reader.Skip();
					}
					this.m_reader.MoveToContent();
				}
				this.m_reader.ReadEndElement();
			}
			return array;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00033EC0 File Offset: 0x000320C0
		private void ReadStructContent(object obj, StructMapping mapping)
		{
			this.m_reader.MoveToContent();
			string name = this.m_reader.Name;
			string namespaceURI = this.m_reader.NamespaceURI;
			this.ReadStructAttributes(obj, mapping);
			if (this.m_reader.IsEmptyElement)
			{
				this.m_reader.Skip();
				return;
			}
			this.m_reader.ReadStartElement();
			this.m_reader.MoveToContent();
			while (this.m_reader.NodeType != XmlNodeType.EndElement && this.m_reader.NodeType != XmlNodeType.None)
			{
				string localName = this.m_reader.LocalName;
				string text = this.m_reader.NamespaceURI;
				text = ((namespaceURI == text) ? string.Empty : text);
				MemberMapping memberMapping = mapping.GetElement(localName, text);
				if (memberMapping == null)
				{
					memberMapping = mapping.GetElement(localName, namespaceURI);
				}
				Type type = null;
				if (memberMapping != null)
				{
					type = memberMapping.Type;
				}
				else
				{
					List<MemberMapping> typeNameElements = mapping.GetTypeNameElements();
					if (typeNameElements != null)
					{
						bool flag = false;
						for (int i = 0; i < typeNameElements.Count; i++)
						{
							memberMapping = typeNameElements[i];
							XmlElementAttributes xmlElementAttributes = memberMapping.XmlAttributes.XmlElements;
							if (base.XmlOverrides != null)
							{
								XmlAttributes xmlAttributes = base.XmlOverrides[obj.GetType()];
								if (xmlAttributes == null)
								{
									xmlAttributes = base.XmlOverrides[memberMapping.Type];
								}
								if (xmlAttributes != null && xmlAttributes.XmlElements != null)
								{
									xmlElementAttributes = xmlAttributes.XmlElements;
								}
							}
							foreach (object obj2 in xmlElementAttributes)
							{
								XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj2;
								if (xmlElementAttribute.ElementName == localName && xmlElementAttribute.Type != null)
								{
									type = xmlElementAttribute.Type;
									flag = true;
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
				}
				if (type != null)
				{
					if (memberMapping.ChildAttributes != null)
					{
						foreach (MemberMapping memberMapping2 in memberMapping.ChildAttributes)
						{
							this.ReadChildAttribute(obj, mapping, memberMapping2);
						}
					}
					if (memberMapping.IsReadOnly)
					{
						if (!TypeMapper.IsPrimitiveType(type))
						{
							object value = memberMapping.GetValue(obj);
							if (value != null)
							{
								this.ReadObjectContent(value, memberMapping, 0);
							}
							else
							{
								this.m_reader.Skip();
							}
						}
						else
						{
							this.m_reader.Skip();
						}
					}
					else
					{
						object obj3 = this.ReadObject(type, memberMapping, 0);
						if (obj3 != null)
						{
							memberMapping.SetValue(obj, obj3);
						}
					}
				}
				else
				{
					if (text != string.Empty && this.m_validNamespaces.Contains(text))
					{
						IXmlLineInfo xmlLineInfo = (IXmlLineInfo)this.m_reader;
						throw new XmlException(RDLValidatingReaderStringsWrapper.rdlValidationInvalidMicroVersionedElement(this.m_reader.Name, name, xmlLineInfo.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), xmlLineInfo.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat)));
					}
					this.m_reader.Skip();
				}
				this.m_reader.MoveToContent();
			}
			this.m_reader.ReadEndElement();
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00034214 File Offset: 0x00032414
		private void ReadStructAttributes(object obj, StructMapping mapping)
		{
			if (this.m_reader.HasAttributes)
			{
				foreach (MemberMapping memberMapping in mapping.Attributes.Values)
				{
					if (memberMapping.Type == typeof(string))
					{
						string attribute = this.m_reader.GetAttribute(memberMapping.Name, memberMapping.Namespace);
						if (attribute != null)
						{
							memberMapping.SetValue(obj, attribute);
						}
					}
				}
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000342A8 File Offset: 0x000324A8
		private void ReadChildAttribute(object obj, StructMapping mapping, MemberMapping childMapping)
		{
			XmlAttributeAttribute xmlAttribute = childMapping.XmlAttributes.XmlAttribute;
			string attribute = this.m_reader.GetAttribute(xmlAttribute.AttributeName, xmlAttribute.Namespace);
			if (attribute != null)
			{
				childMapping.SetValue(obj, attribute);
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000342E4 File Offset: 0x000324E4
		private void ValidateStartElement()
		{
			string text;
			if (base.Settings.ValidateXml && !this.m_validator.ValidateStartElement(out text))
			{
				throw new XmlSchemaException(text + "\r\n");
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00034320 File Offset: 0x00032520
		private void ValidateEndElement()
		{
			string text;
			if (base.Settings.ValidateXml && !this.m_validator.ValidateEndElement(out text))
			{
				throw new XmlSchemaException(text + "\r\n");
			}
		}

		// Token: 0x040006EE RID: 1774
		private XmlReader m_reader;

		// Token: 0x040006EF RID: 1775
		private RdlValidator m_validator;

		// Token: 0x040006F0 RID: 1776
		private XmlSchema m_schema;

		// Token: 0x040006F1 RID: 1777
		private const string m_xsdResourceId = "Microsoft.ReportingServices.RdlObjectModel.Serialization.ReportDefinition.xsd";

		// Token: 0x040006F2 RID: 1778
		private readonly HashSet<string> m_validNamespaces = new HashSet<string> { "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily" };
	}
}
