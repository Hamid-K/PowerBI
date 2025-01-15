using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000432 RID: 1074
	[Serializable]
	public class EncryptedConfigurationClass : ConfigurationClass
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0007C5DD File Offset: 0x0007A7DD
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x0007C5E5 File Offset: 0x0007A7E5
		[ConfigurationProperty]
		public string EncryptionCertificateThumbprint { get; set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x0007C5F0 File Offset: 0x0007A7F0
		[NonConfigurationProperty]
		[CanBeNull]
		protected X509Certificate2 ConfigurationEncryptionCertificate
		{
			get
			{
				if (this.EncryptionCertificateThumbprint == null)
				{
					TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("Serializing configuration of type {0} with no encryption since no certificate thumbprint was provided", new object[] { base.GetType() });
					return null;
				}
				if (this.m_certificate == null)
				{
					X509Certificate2Collection x509Certificate2Collection = CertificateUtilities.TryLoadPersonalCertificates(X509FindType.FindByThumbprint, this.EncryptionCertificateThumbprint);
					if (x509Certificate2Collection.Count == 0)
					{
						throw new CcsMissingCertificateException(this.EncryptionCertificateThumbprint);
					}
					this.m_certificate = x509Certificate2Collection[0];
				}
				return this.m_certificate;
			}
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0007C664 File Offset: 0x0007A864
		protected string Encrypt(object toEncrypt)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = true
			};
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
			{
				new XmlSerializer(toEncrypt.GetType()).Serialize(xmlWriter, toEncrypt);
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(stringWriter.ToString());
			EncryptedXml encryptedXml = new EncryptedXml();
			XmlElement documentElement = xmlDocument.DocumentElement;
			return encryptedXml.Encrypt(documentElement, this.ConfigurationEncryptionCertificate).GetXml().OuterXml;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0007C700 File Offset: 0x0007A900
		protected object Decrypt(Type type, string toDecrypt)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.LoadXml(toDecrypt);
			new EncryptedXml(xmlDocument).DecryptDocument();
			XmlSerializer xmlSerializer = new XmlSerializer(type, null);
			object obj;
			using (XmlReader xmlReader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(xmlDocument.OuterXml))))
			{
				obj = xmlSerializer.Deserialize(xmlReader);
			}
			return obj;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0007C774 File Offset: 0x0007A974
		protected override object ParsePropertyFromReader(PropertyInfo property, XmlReader xmlReader)
		{
			bool flag = ConfigurationPropertyAttribute.ShouldEncrypt(property) && this.EncryptionCertificateThumbprint != null;
			bool flag2 = false;
			if (flag && xmlReader.HasAttributes)
			{
				string attribute = xmlReader.GetAttribute("Encrypted");
				ExtendedDiagnostics.EnsureNotNull<string>(attribute, "isEncryptedInXml");
				flag2 = bool.Parse(attribute);
			}
			object obj;
			if (flag2)
			{
				obj = this.Decrypt(property.PropertyType, xmlReader.ReadElementContentAsString());
			}
			else
			{
				obj = EncryptedConfigurationClass.ParseEncryptablePropertyFromReaderAndSetThumbprint(property.PropertyType, this.EncryptionCertificateThumbprint, xmlReader);
			}
			return obj;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0007C7EB File Offset: 0x0007A9EB
		protected override void OnSerializationStarted(XmlWriter writer)
		{
			if (this.EncryptionCertificateThumbprint != null)
			{
				writer.WriteStartElement("EncryptionCertificateThumbprint");
				writer.WriteValue(this.EncryptionCertificateThumbprint);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0007C814 File Offset: 0x0007AA14
		protected override void WritePropertyToXml(PropertyInfo property, object value, XmlWriter writer)
		{
			string name = property.Name;
			Type type = value.GetType();
			if (name.Equals("EncryptionCertificateThumbprint", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			if (ConfigurationPropertyAttribute.ShouldEncrypt(property) && this.EncryptionCertificateThumbprint != null)
			{
				writer.WriteStartElement(name);
				writer.WriteAttributeString("Encrypted", "true");
				writer.WriteValue(this.Encrypt(value));
				writer.WriteEndElement();
				return;
			}
			if (typeof(EncryptedConfigurationClass).IsAssignableFrom(type))
			{
				((EncryptedConfigurationClass)value).EncryptionCertificateThumbprint = this.EncryptionCertificateThumbprint;
			}
			else if (type.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(type, typeof(ConfigurationCollection<>)))
			{
				type.GetProperty("EncryptionCertificateThumbprint").SetValue(value, this.EncryptionCertificateThumbprint, null);
			}
			else if (type.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(type, typeof(ConfigurationDictionary<, >)))
			{
				type.GetProperty("EncryptionCertificateThumbprint").SetValue(value, this.EncryptionCertificateThumbprint, null);
			}
			base.WritePropertyToXml(property, value, writer);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0007C914 File Offset: 0x0007AB14
		internal static object ParseEncryptablePropertyFromReaderAndSetThumbprint(Type propertyType, string encryptionCertificateThumbprint, XmlReader reader)
		{
			object obj2;
			if (typeof(IXmlSerializable).IsAssignableFrom(propertyType))
			{
				object obj = Activator.CreateInstance(propertyType, false);
				PropertyInfo property = propertyType.GetProperty("EncryptionCertificateThumbprint");
				if (property != null)
				{
					property.SetValue(obj, encryptionCertificateThumbprint, null);
				}
				propertyType.GetMethod("ReadXml").Invoke(obj, new object[] { reader });
				obj2 = obj;
			}
			else
			{
				obj2 = ConfigurationClass.ParseProperty(propertyType, reader);
			}
			return obj2;
		}

		// Token: 0x04000B56 RID: 2902
		private const string c_certificateThumbprintPropertyName = "EncryptionCertificateThumbprint";

		// Token: 0x04000B57 RID: 2903
		private X509Certificate2 m_certificate;
	}
}
