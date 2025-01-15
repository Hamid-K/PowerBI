using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.RdlModel;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000392 RID: 914
	internal class RdlReader
	{
		// Token: 0x06001E1E RID: 7710 RVA: 0x0007B212 File Offset: 0x00079412
		public RdlReader(XmlAttributeOverrides attributesOverrides, bool dontForceSiteName)
		{
			this.attributesOverrides = attributesOverrides;
			this.dontForceSiteName = dontForceSiteName;
			this.mapper = new TypeMapper(false, attributesOverrides, RdlSerializerOptions.Default);
			if (dontForceSiteName)
			{
				this.deserializationStack = new Stack();
			}
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0007B24F File Offset: 0x0007944F
		public object DeserializeComponent(IDesignerSerializationManager manager, XmlReader reader, Type root)
		{
			this.manager = manager;
			this.reader = reader;
			this.deserializationCallbackList.Clear();
			object obj = this.ReadRoot(root);
			reader.Close();
			this.manager = null;
			this.CallOnDeserialization();
			return obj;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0007B284 File Offset: 0x00079484
		public object DeserializeComponent(IDesignerSerializationManager manager, XmlReader reader, Type rootType, object root)
		{
			this.manager = manager;
			this.reader = reader;
			this.deserializationCallbackList.Clear();
			object obj = this.ReadObjectContent(root, null);
			reader.Close();
			this.manager = null;
			this.CallOnDeserialization();
			return obj;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0007B2BC File Offset: 0x000794BC
		private void CallOnDeserialization()
		{
			for (int i = this.deserializationCallbackList.Count - 1; i >= 0; i--)
			{
				((IDeserializationCallback)this.deserializationCallbackList[i]).OnDeserialization(null);
			}
			this.deserializationCallbackList.Clear();
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0007B304 File Offset: 0x00079504
		private object ReadRoot(Type type)
		{
			object obj;
			try
			{
				this.reader.MoveToContent();
				if (this.reader.NamespaceURI != "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")
				{
					throw new XmlException(SRErrors.NoRootElement);
				}
				obj = this.ReadObject(type, null);
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
					text = SRErrors.DeserializationFailedMethod((targetSite != null) ? (targetSite.DeclaringType.Name + "." + targetSite.Name) : null);
				}
				else
				{
					text = SRErrors.DeserializationFailed(innerException.Message);
				}
				IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
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

		// Token: 0x06001E23 RID: 7715 RVA: 0x0007B40C File Offset: 0x0007960C
		private object ReadObject(Type proposedType, MemberMapping mapping)
		{
			Type type = proposedType;
			string attribute = this.reader.GetAttribute("Type", "");
			if (attribute != null)
			{
				type = Type.GetType(attribute);
			}
			if (this.IsPrimitiveType(type))
			{
				return this.ReadPrimitive(type, mapping);
			}
			return this.ReadClassObject(type, mapping);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0007B458 File Offset: 0x00079658
		private object ReadObjectContent(object value, MemberMapping member)
		{
			if (this.deserializationStack != null)
			{
				this.deserializationStack.Push(value);
			}
			TypeMapping typeModel = this.mapper.GetTypeModel(member, value);
			if (typeModel is ArrayMapping)
			{
				this.ReadArrayContent(value, (ArrayMapping)typeModel);
			}
			else if (typeModel is StructMapping)
			{
				this.ReadStructContent(value, (StructMapping)typeModel);
			}
			else if (typeModel is SpecialMapping)
			{
				this.ReadSpecialContent(value);
			}
			else
			{
				this.reader.Skip();
			}
			if (this.deserializationStack != null)
			{
				this.deserializationStack.Pop();
			}
			return value;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0007B4E9 File Offset: 0x000796E9
		private object ReadObject(MemberMapping mapping)
		{
			return this.ReadObject(mapping.MemberType, mapping);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0007B4F8 File Offset: 0x000796F8
		private object ReadPrimitive(Type type, MemberMapping mapping)
		{
			object obj = null;
			XmlAttributes xmlAttributes = ((mapping != null) ? mapping.XmlAttributes : null);
			if (xmlAttributes != null && xmlAttributes.XmlElements != null && xmlAttributes.XmlElements.Count > 0)
			{
				string dataType = xmlAttributes.XmlElements[0].DataType;
			}
			string text = this.reader.ReadString();
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
						obj = float.Parse(text, CultureInfo.InvariantCulture);
						break;
					case TypeCode.Double:
						obj = XmlConvert.ToDouble(text);
						break;
					}
				}
				else
				{
					obj = bool.Parse(text);
				}
			}
			else if (type == typeof(string))
			{
				obj = text;
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
			this.reader.Skip();
			return obj;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0007B668 File Offset: 0x00079868
		private object ReadClassObject(Type type, MemberMapping mapping)
		{
			string attribute = this.reader.GetAttribute("Name", "");
			object obj;
			if (attribute != null)
			{
				obj = this.CreateInstance(type, attribute);
			}
			else
			{
				obj = this.CreateInstance(type);
			}
			this.ReadObjectContent(obj, mapping);
			return obj;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0007B6AC File Offset: 0x000798AC
		private object ReadSpecialContent(object obj)
		{
			IXmlSerializable xmlSerializable = (IXmlSerializable)obj;
			if (xmlSerializable != null)
			{
				xmlSerializable.ReadXml(this.reader);
			}
			return obj;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0007B6D0 File Offset: 0x000798D0
		private object ReadArrayContent(object array, ArrayMapping mapping)
		{
			IList list = array as IList;
			MethodInfo methodInfo = null;
			if (list == null)
			{
				methodInfo = array.GetType().GetMethod("Add", new Type[] { mapping.ItemType });
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
				this.reader.MoveToContent();
				while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.NodeType != XmlNodeType.None)
				{
					if (this.reader.NodeType == XmlNodeType.Element)
					{
						string localName = this.reader.LocalName;
						string namespaceURI2 = this.reader.NamespaceURI;
						Type type = (Type)mapping.ElementTypes[localName];
						if (type != null)
						{
							object obj = this.ReadObject(type, null);
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
					}
					else
					{
						this.reader.Skip();
					}
					this.reader.MoveToContent();
				}
				this.reader.ReadEndElement();
			}
			return array;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0007B818 File Offset: 0x00079A18
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
				if (!(obj is IComponent))
				{
					MemberMapping element = mapping.GetElement("Name", "");
					if (element != null)
					{
						string attribute = this.reader.GetAttribute("Name", "");
						if (attribute != null)
						{
							element.SetValue(obj, attribute);
						}
					}
				}
				this.reader.ReadStartElement();
				this.reader.MoveToContent();
				while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.NodeType != XmlNodeType.None)
				{
					string localName = this.reader.LocalName;
					string text = this.reader.NamespaceURI;
					text = ((namespaceURI == text) ? string.Empty : text);
					if (mapping.GetParentXmlElement(localName, text) != null)
					{
						this.ReadStructContent(obj, mapping);
						this.reader.MoveToContent();
					}
					else
					{
						MemberMapping memberMapping = mapping.GetElement(localName, text);
						if (memberMapping == null)
						{
							memberMapping = mapping.GetTypeNameElement();
							if (memberMapping != null)
							{
								bool flag = false;
								foreach (object obj2 in memberMapping.XmlAttributes.XmlElements)
								{
									XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj2;
									if (xmlElementAttribute.ElementName == localName && xmlElementAttribute.Type != null)
									{
										memberMapping.MemberType = xmlElementAttribute.Type;
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									memberMapping = null;
								}
							}
						}
						if (memberMapping != null)
						{
							if (memberMapping.IsReadOnly)
							{
								if (!this.IsPrimitiveType(memberMapping.MemberType))
								{
									object value = memberMapping.GetValue(obj);
									if (value != null)
									{
										this.ReadObjectContent(value, memberMapping);
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
								object obj3 = this.ReadObject(memberMapping);
								if (obj3 != null)
								{
									memberMapping.SetValue(obj, obj3);
								}
							}
						}
						else
						{
							this.reader.Skip();
						}
						this.reader.MoveToContent();
					}
				}
				this.reader.ReadEndElement();
			}
			return obj;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0007BA6C File Offset: 0x00079C6C
		private bool IsPrimitiveType(Type type)
		{
			return type.IsEnum || type.IsPrimitive || type == typeof(string);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0007BA90 File Offset: 0x00079C90
		private object CreateInstance(Type type)
		{
			return this.CreateInstance(type, null);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0007BA9C File Offset: 0x00079C9C
		private object CreateInstance(Type type, string name)
		{
			object obj;
			if (this.manager != null && typeof(IComponent).IsAssignableFrom(type))
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
			if (obj != null && obj is IDeserializationCallback)
			{
				this.deserializationCallbackList.Add(obj);
			}
			return obj;
		}

		// Token: 0x04000CC2 RID: 3266
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x04000CC3 RID: 3267
		private bool dontForceSiteName;

		// Token: 0x04000CC4 RID: 3268
		private TypeMapper mapper;

		// Token: 0x04000CC5 RID: 3269
		private IDesignerSerializationManager manager;

		// Token: 0x04000CC6 RID: 3270
		private XmlReader reader;

		// Token: 0x04000CC7 RID: 3271
		private ArrayList deserializationCallbackList = new ArrayList();

		// Token: 0x04000CC8 RID: 3272
		private Stack deserializationStack;
	}
}
