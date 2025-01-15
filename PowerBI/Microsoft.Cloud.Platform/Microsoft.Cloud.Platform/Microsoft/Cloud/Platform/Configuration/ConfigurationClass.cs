using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000427 RID: 1063
	[Serializable]
	public abstract class ConfigurationClass : IConfigurationClass, ICloneable, IXmlSerializable, ISupportedConfigurationType
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0007AD6C File Offset: 0x00078F6C
		[NonConfigurationProperty]
		public string Xml
		{
			get
			{
				return this.ToXml();
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0007AD74 File Offset: 0x00078F74
		protected ConfigurationClass()
		{
			this.TryVerifyClassDefinition();
			this.InitializeProperties();
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0007AD88 File Offset: 0x00078F88
		private void TryVerifyClassDefinition()
		{
			Type type = base.GetType();
			Dictionary<Type, bool> dictionary = ConfigurationClass.s_verifiedTypes;
			lock (dictionary)
			{
				bool flag2;
				if (ConfigurationClass.s_verifiedTypes.TryGetValue(type, out flag2))
				{
					if (!flag2)
					{
						throw new CCSValidationException("Configuration class {0} is invalid.".FormatWithCurrentCulture(new object[] { type }));
					}
				}
				else
				{
					try
					{
						this.VerifyClassDefinition();
						ConfigurationClass.s_verifiedTypes.Add(type, true);
					}
					catch (CCSValidationException)
					{
						ConfigurationClass.s_verifiedTypes.Add(type, false);
						throw;
					}
				}
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0007AE24 File Offset: 0x00079024
		private void VerifyClassDefinition()
		{
			Type type = base.GetType();
			if (!type.IsSerializable)
			{
				throw new CCSValidationException("Configuration class {0} is not serializable. Please mark it as [Serializale].".FormatWithCurrentCulture(new object[] { type }));
			}
			if (!type.IsSealed)
			{
				throw new CCSValidationException("Configuration class {0} is not sealed. Currently, only sealed configuration classes are supported.".FormatWithCurrentCulture(new object[] { type }));
			}
			ConfigurationClass.VerifyProperties(type);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0007AE84 File Offset: 0x00079084
		private static void VerifyProperties(Type type)
		{
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			IEnumerable<PropertyInfo> enumerable = properties.Where((PropertyInfo p) => !ConfigurationPropertyAttribute.IsDefined(p) && !NonConfigurationPropertyAttribute.IsDefined(p));
			if (enumerable.Any<PropertyInfo>())
			{
				string text = "The following properties of Configuration Class {0} should be marked with either [ConfigurationProperty] or [NonConfigurationProperty]: {1}";
				object[] array = new object[2];
				array[0] = type.FullName;
				array[1] = enumerable.Select((PropertyInfo p) => p.Name).StringJoin(", ");
				throw new CCSValidationException(text.FormatWithCurrentCulture(array));
			}
			IEnumerable<PropertyInfo> enumerable2 = from p in properties.Where(new Func<PropertyInfo, bool>(ConfigurationPropertyAttribute.IsDefined))
				where !ConfigurationTypes.IsSupportedConfigurationType(p.PropertyType)
				select p;
			if (enumerable2.Any<PropertyInfo>())
			{
				throw new CCSValidationException("The following properties are not supported in Configuration Class '{0}': {1}. ".FormatWithCurrentCulture(new object[]
				{
					type.FullName,
					enumerable2.StringJoin(", ")
				}) + "Only the following are supported: {0}".FormatWithCurrentCulture(new object[] { ConfigurationTypes.SupportedConfigurationTypes }));
			}
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0007AFA4 File Offset: 0x000791A4
		private void InitializeProperties()
		{
			this.m_suppressValidation = true;
			Type type = base.GetType();
			foreach (PropertyInfo propertyInfo in ConfigurationPropertyAttribute.GetConfigurationProperties(type))
			{
				if (!NullConfigurationPropertyAttribute.IsDefined(propertyInfo))
				{
					object obj = null;
					if (propertyInfo.PropertyType != typeof(string))
					{
						try
						{
							obj = Activator.CreateInstance(propertyInfo.PropertyType);
						}
						catch (Exception ex)
						{
							string text = ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
							throw new CCSValidationException(string.Format(CultureInfo.InvariantCulture, "Error initializing property {0} of type {1}: {2}", new object[] { propertyInfo.Name, type, text }), ex);
						}
					}
					propertyInfo.SetValue(this, obj, null);
				}
			}
			this.m_suppressValidation = false;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0007B0A4 File Offset: 0x000792A4
		public sealed override int GetHashCode()
		{
			return (from p in ConfigurationPropertyAttribute.GetConfigurationProperties(base.GetType())
				select p.GetValue(this, null)).Aggregate(base.GetType().GetHashCode(), (int h, object v) => h ^ ((v == null) ? 0 : v.GetHashCode()));
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0007B0FC File Offset: 0x000792FC
		public sealed override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Type type = base.GetType();
			Type type2 = obj.GetType();
			return !(type != type2) && ConfigurationPropertyAttribute.GetConfigurationProperties(type).All((PropertyInfo p) => ConfigurationClass.PropertyEqual(p, this, obj));
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0007B15C File Offset: 0x0007935C
		private static bool PropertyEqual(PropertyInfo property, object obj1, object obj2)
		{
			object value = property.GetValue(obj1, null);
			object value2 = property.GetValue(obj2, null);
			return object.Equals(value, value2);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0007B180 File Offset: 0x00079380
		public string ToXml()
		{
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add(string.Empty, string.Empty);
			XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = true
			};
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
			{
				xmlSerializer.Serialize(xmlWriter, this, xmlSerializerNamespaces);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0007B208 File Offset: 0x00079408
		private void ThrowValidationException(string message)
		{
			if (this.m_suppressValidation)
			{
				return;
			}
			throw new CCSValidationException(message);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0007B219 File Offset: 0x00079419
		protected void ValidateMoreOrEqual(double value, double minimum)
		{
			if (value < minimum)
			{
				this.ThrowValidationException("Range Validation Failed (value {0} is lower than {1})".FormatWithCurrentCulture(new object[] { value, minimum }));
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0007B247 File Offset: 0x00079447
		protected void ValidateLessOrEqual(double value, double maximum)
		{
			if (value > maximum)
			{
				this.ThrowValidationException("Range Validation Failed (value {0} is higher than {1})".FormatWithCurrentCulture(new object[] { value, maximum }));
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0007B275 File Offset: 0x00079475
		protected void ValidateStringNotNullEmptyOrWhiteSpace(string value, string valueName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				this.ThrowValidationException("String not empty, null or white spaceValidation Failed for value with name {0}".FormatWithCurrentCulture(new object[] { valueName }));
			}
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0007B299 File Offset: 0x00079499
		protected void ValidateRegexMatching(string value, string pattern)
		{
			if (this.m_suppressValidation)
			{
				return;
			}
			if (!Regex.IsMatch(value, pattern))
			{
				this.ThrowValidationException("Regular Expression Validation Failed (string '{0}' does not match regex pattern '{1}')".FormatWithCurrentCulture(new object[] { value, pattern }));
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0007B2CC File Offset: 0x000794CC
		public object Clone()
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this);
				memoryStream.Position = 0L;
				obj = binaryFormatter.Deserialize(memoryStream);
			}
			return obj;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0007B318 File Offset: 0x00079518
		public byte[] GetChecksum()
		{
			return (from p in ConfigurationPropertyAttribute.GetConfigurationProperties(base.GetType())
				select p.GetValue(this, null)).Select(delegate(object v)
			{
				if (v == null)
				{
					return new byte[0];
				}
				if (v is ISupportedConfigurationType)
				{
					return ((ISupportedConfigurationType)v).GetChecksum();
				}
				return ExtendedText.GetChecksum(v.ToString());
			}).Aggregate(ExtendedText.GetChecksum(base.GetType().Name), (byte[] h, byte[] v) => ExtendedMath.Xor(ExtendedMath.AddWithOverflow(h, v), v));
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00005EB7 File Offset: 0x000040B7
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0007B39C File Offset: 0x0007959C
		public virtual void WriteXml(XmlWriter writer)
		{
			List<PropertyInfo> configurationProperties = ConfigurationPropertyAttribute.GetConfigurationProperties(base.GetType());
			this.OnSerializationStarted(writer);
			foreach (PropertyInfo propertyInfo in configurationProperties)
			{
				object value = propertyInfo.GetValue(this, null);
				if (value != null)
				{
					bool flag = true;
					Type type = value.GetType();
					if (value is ConfigurationClass)
					{
						if (((IConfigurationClass)Activator.CreateInstance(type, false)).Equals(value))
						{
							flag = false;
						}
					}
					else if (ConfigurationTypes.IsConfigurationCollection(value) || ConfigurationTypes.IsConfigurationDictionary(value))
					{
						flag = ((ICollection)value).Count > 0;
					}
					if (flag)
					{
						this.WritePropertyToXml(propertyInfo, value, writer);
					}
				}
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0007B458 File Offset: 0x00079658
		public virtual void ReadXml(XmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				Type type = base.GetType();
				List<PropertyInfo> configurationProperties = ConfigurationPropertyAttribute.GetConfigurationProperties(type);
				string name2 = reader.Name;
				reader.ReadStartElement();
				for (;;)
				{
					if (reader.IsEmptyElement)
					{
						reader.Skip();
					}
					else
					{
						if (reader.Name.Equals(name2, StringComparison.OrdinalIgnoreCase) && reader.NodeType == XmlNodeType.EndElement)
						{
							break;
						}
						string name = reader.Name;
						ExtendedDiagnostics.EnsureOperation(configurationProperties.Any((PropertyInfo p) => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)), "Could not find matching property [{0}] in ConfigurationClass {1}".FormatWithInvariantCulture(new object[] { name, type }));
						PropertyInfo propertyInfo = configurationProperties.First((PropertyInfo p) => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
						Type propertyType = propertyInfo.PropertyType;
						ExtendedDiagnostics.EnsureOperation(ConfigurationTypes.IsSupportedConfigurationType(propertyType), "Type {0} is not supported".FormatWithInvariantCulture(new object[] { propertyType.FullName }));
						object obj = this.ParsePropertyFromReader(propertyInfo, reader);
						propertyInfo.SetValue(this, obj, null);
					}
				}
				reader.ReadEndElement();
				return;
			}
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0007B55F File Offset: 0x0007975F
		protected virtual object ParsePropertyFromReader(PropertyInfo property, XmlReader xmlReader)
		{
			return ConfigurationClass.ParseProperty(property.PropertyType, xmlReader);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0007B570 File Offset: 0x00079770
		protected virtual void WritePropertyToXml(PropertyInfo property, object value, XmlWriter writer)
		{
			Type propertyType = property.PropertyType;
			if (propertyType.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(propertyType, typeof(ConfigurationCollection<>)))
			{
				string itemName = CollectionItemNameAttribute.GetItemName(property);
				if (!string.IsNullOrEmpty(itemName))
				{
					propertyType.GetProperty("CollectionItemName").SetValue(value, itemName, null);
				}
			}
			ConfigurationClass.WriteProperty(property.PropertyType, property.Name, value, writer);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnSerializationStarted(XmlWriter writer)
		{
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0007B5D4 File Offset: 0x000797D4
		internal static object ParseProperty(Type propertyType, XmlReader xmlReader)
		{
			object obj2;
			if (typeof(IXmlSerializable).IsAssignableFrom(propertyType))
			{
				object obj = Activator.CreateInstance(propertyType, false);
				propertyType.GetMethod("ReadXml").Invoke(obj, new object[] { xmlReader });
				obj2 = obj;
			}
			else if (propertyType.Equals(typeof(Guid)))
			{
				obj2 = Guid.Parse(xmlReader.ReadElementContentAsString());
			}
			else if (propertyType.IsEnum)
			{
				obj2 = Enum.Parse(propertyType, xmlReader.ReadElementContentAsString());
			}
			else if (propertyType.Equals(typeof(TimeSpan)))
			{
				obj2 = TimeSpan.Parse(xmlReader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
			}
			else
			{
				obj2 = xmlReader.ReadElementContentAs(propertyType, null);
			}
			return obj2;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0007B68C File Offset: 0x0007988C
		internal static void WriteProperty(Type propertyType, string tagName, object value, XmlWriter writer)
		{
			writer.WriteStartElement(tagName);
			if (typeof(IXmlSerializable).IsAssignableFrom(propertyType))
			{
				propertyType.GetMethod("WriteXml").Invoke(value, new object[] { writer });
			}
			else if (propertyType.IsPrimitive || propertyType.Equals(typeof(string)))
			{
				writer.WriteValue(value);
			}
			else if (propertyType.Equals(typeof(Guid)) || propertyType.IsEnum || propertyType.Equals(typeof(TimeSpan)))
			{
				writer.WriteValue(value.ToString());
			}
			writer.WriteEndElement();
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0007B731 File Offset: 0x00079931
		internal static void WriteRawStringProperty(string tagName, string value, XmlWriter writer)
		{
			writer.WriteStartElement(tagName);
			writer.WriteRaw(value);
			writer.WriteEndElement();
		}

		// Token: 0x04000B3F RID: 2879
		private bool m_suppressValidation;

		// Token: 0x04000B40 RID: 2880
		private static Dictionary<Type, bool> s_verifiedTypes = new Dictionary<Type, bool>();
	}
}
