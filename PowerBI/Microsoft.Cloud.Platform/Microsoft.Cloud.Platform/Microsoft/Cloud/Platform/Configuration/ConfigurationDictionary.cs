using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x0200042A RID: 1066
	[CannotApplyEqualityOperator]
	[Serializable]
	public class ConfigurationDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable, ISupportedConfigurationType
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0007BB2C File Offset: 0x00079D2C
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0007BB34 File Offset: 0x00079D34
		public string EncryptionCertificateThumbprint { get; set; }

		// Token: 0x060020E3 RID: 8419 RVA: 0x0007BB3D File Offset: 0x00079D3D
		public ConfigurationDictionary()
		{
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0007BB45 File Offset: 0x00079D45
		public ConfigurationDictionary(Dictionary<TKey, TValue> other)
			: base(other)
		{
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0007BB4E File Offset: 0x00079D4E
		public ConfigurationDictionary(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0007BB58 File Offset: 0x00079D58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0007BB64 File Offset: 0x00079D64
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			ConfigurationDictionary<TKey, TValue> configurationDictionary = (ConfigurationDictionary<TKey, TValue>)obj;
			return this.Equivalent(configurationDictionary);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0007BB99 File Offset: 0x00079D99
		public override int GetHashCode()
		{
			return this.Aggregate(base.GetType().Name.GetHashCode(), (int h, KeyValuePair<TKey, TValue> kvp) => h ^ kvp.GetHashCode() ^ ((kvp.Value == null) ? 0 : kvp.GetHashCode()));
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00005EB7 File Offset: 0x000040B7
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0007BBD0 File Offset: 0x00079DD0
		public virtual void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			string name = reader.Name;
			reader.ReadStartElement();
			for (;;)
			{
				if (reader.IsEmptyElement)
				{
					reader.Skip();
				}
				else
				{
					if (reader.Name.Equals(name) && reader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					reader.ReadStartElement("Item");
					if (reader.Name.Equals("Item") && reader.NodeType == XmlNodeType.EndElement)
					{
						reader.ReadEndElement();
					}
					else
					{
						object obj = EncryptedConfigurationClass.ParseEncryptablePropertyFromReaderAndSetThumbprint(typeof(TKey), this.EncryptionCertificateThumbprint, reader);
						object obj2 = EncryptedConfigurationClass.ParseEncryptablePropertyFromReaderAndSetThumbprint(typeof(TValue), this.EncryptionCertificateThumbprint, reader);
						reader.ReadEndElement();
						base.Add((TKey)((object)obj), (TValue)((object)obj2));
					}
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0007BCA4 File Offset: 0x00079EA4
		public virtual void WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				writer.WriteStartElement("Item");
				this.SetEncryptedConfigurationClassCertificate(typeof(TKey), keyValuePair.Key);
				ConfigurationClass.WriteProperty(typeof(TKey), "Key", keyValuePair.Key, writer);
				this.SetEncryptedConfigurationClassCertificate(typeof(TValue), keyValuePair.Value);
				ConfigurationClass.WriteProperty(typeof(TValue), "Value", keyValuePair.Value, writer);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x0007BD7C File Offset: 0x00079F7C
		private void SetEncryptedConfigurationClassCertificate(Type propertyType, object value)
		{
			if (typeof(EncryptedConfigurationClass).IsAssignableFrom(propertyType))
			{
				EncryptedConfigurationClass encryptedConfigurationClass = value as EncryptedConfigurationClass;
				ExtendedDiagnostics.EnsureNotNull<EncryptedConfigurationClass>(encryptedConfigurationClass, "encryptedConfigurationClass");
				encryptedConfigurationClass.EncryptionCertificateThumbprint = this.EncryptionCertificateThumbprint;
				return;
			}
			if (propertyType.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(propertyType, typeof(ConfigurationCollection<>)))
			{
				propertyType.GetProperty("EncryptionCertificateThumbprint").SetValue(value, this.EncryptionCertificateThumbprint, null);
				return;
			}
			if (propertyType.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(propertyType, typeof(ConfigurationDictionary<, >)))
			{
				propertyType.GetProperty("EncryptionCertificateThumbprint").SetValue(value, this.EncryptionCertificateThumbprint, null);
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0007BE1D File Offset: 0x0007A01D
		public byte[] GetChecksum()
		{
			return this.Aggregate(ExtendedText.GetChecksum(base.GetType().Name), delegate(byte[] h, KeyValuePair<TKey, TValue> kvp)
			{
				TKey key = kvp.Key;
				byte[] bytes = BitConverter.GetBytes(key.ToString().GetHashCode());
				byte[] array;
				if (kvp.Value != null)
				{
					if (!(kvp.Value is ISupportedConfigurationType))
					{
						TValue value = kvp.Value;
						array = ExtendedText.GetChecksum(value.ToString());
					}
					else
					{
						array = ((ISupportedConfigurationType)((object)kvp.Value)).GetChecksum();
					}
				}
				else
				{
					array = new byte[0];
				}
				return ExtendedMath.Xor(h, ExtendedMath.Xor(bytes, array));
			});
		}

		// Token: 0x04000B46 RID: 2886
		private const string c_item = "Item";

		// Token: 0x04000B47 RID: 2887
		private const string c_key = "Key";

		// Token: 0x04000B48 RID: 2888
		private const string c_value = "Value";

		// Token: 0x04000B49 RID: 2889
		private const string c_certificateThumbprintPropertyName = "EncryptionCertificateThumbprint";
	}
}
