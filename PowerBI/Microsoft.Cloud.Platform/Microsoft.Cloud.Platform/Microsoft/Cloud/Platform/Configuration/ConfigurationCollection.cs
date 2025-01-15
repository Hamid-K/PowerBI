using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000429 RID: 1065
	[CannotApplyEqualityOperator]
	[Serializable]
	public sealed class ConfigurationCollection<T> : Collection<T>, ISupportedConfigurationType, IXmlSerializable
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0007B81D File Offset: 0x00079A1D
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x0007B825 File Offset: 0x00079A25
		public string EncryptionCertificateThumbprint { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x0007B82E File Offset: 0x00079A2E
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x0007B836 File Offset: 0x00079A36
		public string CollectionItemName { get; set; }

		// Token: 0x060020D7 RID: 8407 RVA: 0x0007B840 File Offset: 0x00079A40
		public ConfigurationCollection()
		{
			Type type = base.GetType().GetGenericArguments()[0];
			if (!ConfigurationTypes.IsSupportedGenericConfigurationArgument(type))
			{
				throw new CCSValidationException("Type '{0}' is not supported as a collection item".FormatWithCurrentCulture(new object[] { type }));
			}
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0007B883 File Offset: 0x00079A83
		public ConfigurationCollection([NotNull] params T[] values)
			: this()
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<T[]>(values, "values");
			this.AddRange(values);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0007B89D File Offset: 0x00079A9D
		public ConfigurationCollection([NotNull] IEnumerable<T> values)
			: this()
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<T>>(values, "values");
			this.AddRange(values);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0007B8B8 File Offset: 0x00079AB8
		public void AddRange(IEnumerable<T> values)
		{
			foreach (T t in values)
			{
				base.Add(t);
			}
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0007B900 File Offset: 0x00079B00
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.SequenceEqual((ConfigurationCollection<T>)obj);
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0007B928 File Offset: 0x00079B28
		public override int GetHashCode()
		{
			return this.Aggregate(base.GetType().GetHashCode(), (int h, T v) => h ^ ((v == null) ? 0 : v.GetHashCode()));
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0007B95A File Offset: 0x00079B5A
		public byte[] GetChecksum()
		{
			return this.Aggregate(ExtendedText.GetChecksum(base.GetType().Name), (byte[] h, T v) => ExtendedMath.Xor(h, (v == null) ? new byte[0] : ((v is ISupportedConfigurationType) ? ((ISupportedConfigurationType)((object)v)).GetChecksum() : ExtendedText.GetChecksum(v.ToString()))));
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00005EB7 File Offset: 0x000040B7
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0007B994 File Offset: 0x00079B94
		public void ReadXml(XmlReader reader)
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
					object obj = EncryptedConfigurationClass.ParseEncryptablePropertyFromReaderAndSetThumbprint(typeof(T), this.EncryptionCertificateThumbprint, reader);
					base.Add((T)((object)obj));
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0007BA14 File Offset: 0x00079C14
		public void WriteXml(XmlWriter writer)
		{
			foreach (T t in this)
			{
				Type type = t.GetType();
				if (typeof(EncryptedConfigurationClass).IsAssignableFrom(type))
				{
					EncryptedConfigurationClass encryptedConfigurationClass = t as EncryptedConfigurationClass;
					ExtendedDiagnostics.EnsureNotNull<EncryptedConfigurationClass>(encryptedConfigurationClass, "encryptedConfigurationClass");
					encryptedConfigurationClass.EncryptionCertificateThumbprint = this.EncryptionCertificateThumbprint;
				}
				else if (type.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(type, typeof(ConfigurationCollection<>)))
				{
					type.GetProperty("EncryptionCertificateThumbprint").SetValue(t, this.EncryptionCertificateThumbprint, null);
				}
				else if (type.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(type, typeof(ConfigurationDictionary<, >)))
				{
					type.GetProperty("EncryptionCertificateThumbprint").SetValue(t, this.EncryptionCertificateThumbprint, null);
				}
				ConfigurationClass.WriteProperty(type, this.CollectionItemName ?? type.Name, t, writer);
			}
		}

		// Token: 0x04000B42 RID: 2882
		private const string c_certificateThumbprintPropertyName = "EncryptionCertificateThumbprint";
	}
}
