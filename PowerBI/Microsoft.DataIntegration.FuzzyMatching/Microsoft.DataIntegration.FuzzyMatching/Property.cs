using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Reflection;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class Property : IXmlSerializable, IBinarySerialize, IName
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000487D File Offset: 0x00002A7D
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004885 File Offset: 0x00002A85
		public string Name { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000488E File Offset: 0x00002A8E
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004896 File Offset: 0x00002A96
		public Type DataType { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000489F File Offset: 0x00002A9F
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000048A7 File Offset: 0x00002AA7
		public object Value { get; set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x000048B0 File Offset: 0x00002AB0
		public Property()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000048B8 File Offset: 0x00002AB8
		public Property(string name, object value)
		{
			this.Name = name;
			this.DataType = value.GetType();
			this.Value = value;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000048DC File Offset: 0x00002ADC
		public Property(Property property)
		{
			this.Name = ((property.Name != null) ? ((string)property.Name.Clone()) : null);
			this.DataType = property.DataType;
			this.Value = property.Value;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004928 File Offset: 0x00002B28
		public void Write(BinaryWriter w)
		{
			StringWriter stringWriter = new StringWriter();
			this.WriteXml(XmlWriter.Create(stringWriter));
			w.Write(stringWriter.ToString());
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004954 File Offset: 0x00002B54
		public void Read(BinaryReader r)
		{
			StringReader stringReader = new StringReader(r.ReadString());
			this.ReadXml(XmlReader.Create(stringReader));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004979 File Offset: 0x00002B79
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.Name, this.DataType, this.Value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004997 File Offset: 0x00002B97
		public override bool Equals(object obj)
		{
			if (obj is string)
			{
				return this.Name.Equals(obj as string);
			}
			return base.Equals(obj);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000049BA File Offset: 0x00002BBA
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000049C0 File Offset: 0x00002BC0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("Property"))
			{
				if (reader.MoveToAttribute("name"))
				{
					this.Name = reader.Value;
				}
				if (reader.MoveToAttribute("dataType"))
				{
					this.DataType = Type.GetType(reader.Value);
				}
				string text = reader.ReadString();
				if (string.IsNullOrEmpty(text))
				{
					this.Value = null;
					return;
				}
				if (this.DataType != null)
				{
					this.Value = ReflectionUtilities.InstantiateValue(this.DataType, text);
					return;
				}
				this.Value = text;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004A54 File Offset: 0x00002C54
		public static object TryGetPropertyValue(List<Property> properties, string propertyName)
		{
			foreach (Property property in properties)
			{
				if (property.Name.Equals(propertyName))
				{
					return property.Value;
				}
			}
			return null;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public static void SetProperties(object objWithProperties, List<Property> properties)
		{
			foreach (Property property in properties)
			{
				Property.SetProperty(objWithProperties, property.Name, property.Value);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004B14 File Offset: 0x00002D14
		public static void SetProperty(object objWithProperties, string propertyName, object propertyValue)
		{
			PropertyInfo property = objWithProperties.GetType().GetProperty(propertyName);
			if (property == null)
			{
				throw new InvalidOperationException(string.Format("Property '{0}' was not found on object of type '{1}'.", propertyName, objWithProperties.GetType().ToString()));
			}
			if (propertyValue != null && propertyValue.GetType() != property.PropertyType)
			{
				propertyValue = ReflectionUtilities.InstantiateValue(property.PropertyType, propertyValue.ToString());
			}
			property.SetValue(objWithProperties, propertyValue, null);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004B7C File Offset: 0x00002D7C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Property");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (this.DataType != null)
			{
				writer.WriteAttributeString("DataType", this.DataType.FullName);
			}
			if (this.Value != null)
			{
				writer.WriteString(this.Value.ToString());
			}
			writer.WriteEndElement();
		}
	}
}
