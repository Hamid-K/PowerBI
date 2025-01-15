using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Schema;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x020003F7 RID: 1015
	public static class ConfigurationManagerUtilities
	{
		// Token: 0x06001F1C RID: 7964 RVA: 0x000742C9 File Offset: 0x000724C9
		public static Pair<Type, IConfigurationClass> ReadXmlFromContent([NotNull] string content)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(content, "content");
			return ConfigurationManagerUtilities.ReadXml(ConfigurationManagerUtilities.LoadDocumentFromContent(content));
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000742E4 File Offset: 0x000724E4
		public static bool TryReadXmlFromContent([NotNull] string content, out Pair<Type, IConfigurationClass> configPair)
		{
			configPair = null;
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(content, "content");
			XmlDocument xmlDocument = ConfigurationManagerUtilities.LoadDocumentFromContent(content);
			bool flag;
			try
			{
				configPair = ConfigurationManagerUtilities.ReadXml(xmlDocument);
				flag = true;
			}
			catch (CCSUnknownTypeOrAssemblyException ex)
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.TraceWarning("Swallow exception {0}", new object[] { ex });
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00074344 File Offset: 0x00072544
		public static Pair<Type, IConfigurationClass> ReadXmlFromContent([NotNull] string content, [NotNull] string xsdFilePath, [NotNull] ValidationEventHandler onValidationFailed)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(content, "content");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xsdFilePath, "xsdFilePath");
			ExtendedDiagnostics.EnsureArgumentNotNull<ValidationEventHandler>(onValidationFailed, "onValidationFailed");
			XmlDocument xmlDocument = ConfigurationManagerUtilities.LoadDocumentFromContent(content);
			ConfigurationManagerUtilities.ValidateXmlSchema(xmlDocument, xsdFilePath, onValidationFailed);
			return ConfigurationManagerUtilities.ReadXml(xmlDocument);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0007437A File Offset: 0x0007257A
		public static Pair<Type, IConfigurationClass> ReadXml([NotNull] string xmlFile)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xmlFile, "xmlFile");
			return ConfigurationManagerUtilities.ReadXml(ConfigurationManagerUtilities.LoadDocumentFromFile(xmlFile));
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00074394 File Offset: 0x00072594
		public static Pair<Type, TConfigurationObject> ReadXml<TConfigurationObject>(string xmlFile) where TConfigurationObject : class, IConfigurationClass
		{
			Pair<Type, IConfigurationClass> pair = ConfigurationManagerUtilities.ReadXml(xmlFile);
			TConfigurationObject tconfigurationObject = pair.Second as TConfigurationObject;
			ExtendedDiagnostics.EnsureNotNull<TConfigurationObject>(tconfigurationObject, "typedConfigObject");
			return new Pair<Type, TConfigurationObject>(pair.First, tconfigurationObject);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000743CE File Offset: 0x000725CE
		public static Pair<Type, IConfigurationClass> ReadXml([NotNull] string xmlFile, [NotNull] string xsdFilePath, [NotNull] ValidationEventHandler onValidationFailed)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xmlFile, "xmlFile");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xsdFilePath, "xsdFilePath");
			ExtendedDiagnostics.EnsureArgumentNotNull<ValidationEventHandler>(onValidationFailed, "onValidationFailed");
			XmlDocument xmlDocument = ConfigurationManagerUtilities.LoadDocumentFromFile(xmlFile);
			ConfigurationManagerUtilities.ValidateXmlSchema(xmlDocument, xsdFilePath, onValidationFailed);
			return ConfigurationManagerUtilities.ReadXml(xmlDocument);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00074404 File Offset: 0x00072604
		public static void ValidateXmlSchema([NotNull] string xmlFilePath, [NotNull] string xsdFilePath, ValidationEventHandler onValidationFailed)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xmlFilePath, "xmlFilePath");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(xsdFilePath, "xsdFilePath");
			ConfigurationManagerUtilities.ValidateXmlSchema(ConfigurationManagerUtilities.LoadDocumentFromFile(xmlFilePath), xsdFilePath, onValidationFailed);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0007442C File Offset: 0x0007262C
		public static void WriteConfigurationXml([NotNull] Type type, [NotNull] IConfigurationClass configClass, [NotNull] string targetDirectory, X509Certificate2 certificate, string filename)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			ExtendedDiagnostics.EnsureArgumentNotNull<IConfigurationClass>(configClass, "configClass");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(targetDirectory, "targetDirectory");
			string text;
			if (string.IsNullOrEmpty(filename))
			{
				text = Path.Combine(targetDirectory, ConfigurationManagerUtilities.GetConfigurationClassFileName(type));
			}
			else
			{
				text = Path.Combine(targetDirectory, filename);
			}
			try
			{
				if (!Directory.Exists(targetDirectory))
				{
					Directory.CreateDirectory(targetDirectory);
				}
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
				{
					Indent = true
				};
				using (XmlWriter xmlWriter = XmlWriter.Create(text, xmlWriterSettings))
				{
					ConfigurationManagerUtilities.WriteConfigurationClass(xmlWriter, type, configClass, certificate);
				}
			}
			catch (NotSupportedException ex)
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Error, "Caught exception of type {0} while encrypting XML document {1}", new object[]
				{
					ex.GetType(),
					text
				});
				throw new ConfigurationException("Caught exception of type {0} while encrypting XML document {1}".FormatWithInvariantCulture(new object[]
				{
					ex.GetType(),
					text
				}), ex);
			}
			catch (IOException ex2)
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Error, "Caught exception of type {0} while trying to write configuration class of type {1} to directory {2}", new object[]
				{
					ex2.GetType(),
					type,
					targetDirectory
				});
				throw new ConfigurationException("Caught exception of type {0} while trying to write configuration class of type {1} to directory {2}".FormatWithInvariantCulture(new object[]
				{
					ex2.GetType(),
					type,
					targetDirectory
				}), ex2);
			}
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00074580 File Offset: 0x00072780
		public static string GetConfigurationXml(Type type, IConfigurationClass configClass, X509Certificate2 certificate)
		{
			string text;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
					{
						Indent = true
					};
					using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
					{
						ConfigurationManagerUtilities.WriteConfigurationClass(xmlWriter, type, configClass, certificate);
					}
					memoryStream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						text = streamReader.ReadToEnd();
					}
				}
			}
			catch (NotSupportedException ex)
			{
				throw new ConfigurationException("Error while encrypting XML document for '{0}'".FormatWithInvariantCulture(new object[] { type.Name }), ex);
			}
			return text;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00074644 File Offset: 0x00072844
		public static void WriteConfigurationXml(Type type, IConfigurationClass configClass, string targetDirectory)
		{
			ConfigurationManagerUtilities.WriteConfigurationXml(type, configClass, targetDirectory, null, null);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00074650 File Offset: 0x00072850
		public static void WriteConfigurationXml(Type type, IConfigurationClass configClass, string targetDirectory, string filename)
		{
			ConfigurationManagerUtilities.WriteConfigurationXml(type, configClass, targetDirectory, null, filename);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0007465C File Offset: 0x0007285C
		public static void WriteConfigurationXml(Type type, IConfigurationClass configClass, string targetDirectory, X509Certificate2 certificate)
		{
			ConfigurationManagerUtilities.WriteConfigurationXml(type, configClass, targetDirectory, certificate, null);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00074668 File Offset: 0x00072868
		public static string GetConfigurationClassFileName(Type type)
		{
			return "{0}.{1}".FormatWithInvariantCulture(new object[]
			{
				type.Name,
				ConfigurationConstants.ClassFileSuffix
			});
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0007468C File Offset: 0x0007288C
		private static XmlDocument LoadDocumentFromContent(string content)
		{
			XmlDocument xmlDocument2;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.LoadXml(content);
				xmlDocument2 = xmlDocument;
			}
			catch (XmlException ex)
			{
				throw new ConfigurationException("Cannot load XML document from content '{0}'".FormatWithInvariantCulture(new object[] { content }), ex);
			}
			return xmlDocument2;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000746DC File Offset: 0x000728DC
		private static XmlDocument LoadDocumentFromFile(string path)
		{
			XmlDocument xmlDocument2;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.SecureLoadDocument(path);
				xmlDocument2 = xmlDocument;
			}
			catch (XmlException ex)
			{
				throw new ConfigurationException("Could not load XML content from '{0}".FormatWithInvariantCulture(new object[] { path }), ex);
			}
			catch (IOException ex2)
			{
				throw new ConfigurationException("Could not load XML content from '{0}".FormatWithInvariantCulture(new object[] { path }), ex2);
			}
			return xmlDocument2;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00074750 File Offset: 0x00072950
		private static void ValidateXmlSchema(XmlDocument doc, string xsdFilePath, ValidationEventHandler onValidationFailed)
		{
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(xsdFilePath))
				{
					XmlSchema xmlSchema = XmlSchema.Read(xmlReader, null);
					doc.Schemas.Add(xmlSchema);
					doc.Validate(onValidationFailed);
				}
			}
			catch (XmlException ex)
			{
				throw new ConfigurationException("Failed to validate XML content with XSD in '{0}'".FormatWithInvariantCulture(new object[] { xsdFilePath }), ex);
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000747C8 File Offset: 0x000729C8
		private static Pair<Type, IConfigurationClass> ReadXml(XmlDocument xmlDoc)
		{
			string text = null;
			string text2 = null;
			Type type;
			object obj;
			try
			{
				text = xmlDoc.GetElementsByTagName(ConfigurationConstants.ClassTypeFullName)[0].FirstChild.Value;
				text2 = xmlDoc.GetElementsByTagName(ConfigurationConstants.AssemblyName)[0].FirstChild.Value;
				Assembly assembly = null;
				try
				{
					assembly = Assembly.Load(text2);
				}
				catch (FileLoadException)
				{
					AssemblyName assemblyName = new AssemblyName(text2);
					AssemblyName assemblyName2 = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
					if (assemblyName.Version.Major == assemblyName2.Version.Major)
					{
						throw;
					}
					assemblyName.Version = new Version(assemblyName2.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
					text2 = assemblyName.ToString();
					assembly = Assembly.Load(text2);
				}
				if (assembly == null)
				{
					throw new CCSAssemblyNotLoadedException("The assembly name defined for type '{0}' could not be loaded".FormatWithInvariantCulture(new object[] { text }));
				}
				type = assembly.GetType(text);
				if (type == null)
				{
					throw new CCSTypeNotFoundException("Type {0} was not found in assembly {1}".FormatWithInvariantCulture(new object[] { text, assembly.FullName }));
				}
				XmlNode xmlNode = xmlDoc.SelectSingleNode("//" + type.Name);
				if (xmlNode == null)
				{
					throw new CCSValidationException("Could not read configuration of type {0}".FormatWithInvariantCulture(new object[] { type }));
				}
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlNode.OuterXml), new XmlReaderSettings
				{
					IgnoreComments = true
				}))
				{
					xmlReader.Read();
					obj = new ConfigurationClassSerializer(type).Deserialize(xmlReader);
					xmlReader.Close();
				}
			}
			catch (CryptographicException ex)
			{
				throw new CcsCryptographicException(null, ex);
			}
			catch (XmlException ex2)
			{
				throw new CCSValidationException(null, ex2);
			}
			catch (InvalidOperationException ex3)
			{
				throw new ConfigurationException(null, ex3);
			}
			catch (TypeLoadException ex4)
			{
				throw new CCSAssemblyNotLoadedException("Failed to load type {0}".FormatWithInvariantCulture(new object[] { text }), ex4);
			}
			catch (FileLoadException ex5)
			{
				throw new CCSAssemblyNotLoadedException("Failed to load assembly file for {0}, assembly name {1}".FormatWithInvariantCulture(new object[] { text, text2 }), ex5);
			}
			catch (FileNotFoundException ex6)
			{
				throw new CCSAssemblyNotLoadedException("Failed to find containing assembly file for {0}, assembly name {1}".FormatWithInvariantCulture(new object[] { text, text2 }), ex6);
			}
			return new Pair<Type, IConfigurationClass>(type, (IConfigurationClass)obj);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00074AD8 File Offset: 0x00072CD8
		private static void WriteConfigurationClass(XmlWriter xmlWriter, Type type, IConfigurationClass configClass, X509Certificate2 certificate)
		{
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement(ConfigurationConstants.ConfigurationData);
			xmlWriter.WriteElementString(ConfigurationConstants.ClassTypeFullName, type.FullName);
			xmlWriter.WriteElementString(ConfigurationConstants.AssemblyName, type.Assembly.FullName);
			EncryptedConfigurationClass encryptedConfigurationClass = configClass as EncryptedConfigurationClass;
			if (encryptedConfigurationClass != null && certificate != null)
			{
				encryptedConfigurationClass.EncryptionCertificateThumbprint = certificate.Thumbprint;
			}
			new ConfigurationClassSerializer(type).Serialize(xmlWriter, configClass);
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
	}
}
