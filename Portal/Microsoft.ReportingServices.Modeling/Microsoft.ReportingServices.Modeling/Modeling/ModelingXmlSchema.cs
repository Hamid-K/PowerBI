using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000053 RID: 83
	internal abstract class ModelingXmlSchema
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000358 RID: 856
		public abstract string TargetNamespace { get; }

		// Token: 0x06000359 RID: 857 RVA: 0x0000B61C File Offset: 0x0000981C
		public XmlReader WrapXmlReader(XmlReader xr)
		{
			XmlReaderSettings readerSettings = XmlRWFactory.GetReaderSettings();
			XmlSchemaSet schemaSet = this.GetSchemaSet();
			if (this.m_schemaSet != null)
			{
				readerSettings.ConformanceLevel = ConformanceLevel.Auto;
				readerSettings.Schemas = schemaSet;
				readerSettings.ValidationType = ValidationType.Schema;
			}
			if (readerSettings.CheckCharacters || (xr.Settings != null && xr.Settings.CheckCharacters))
			{
				throw new InvalidOperationException(DevExceptionMessages.XmlReaderCheckCharsTrue);
			}
			return XmlReader.Create(xr, readerSettings);
		}

		// Token: 0x0600035A RID: 858
		protected abstract void AddXmlSchemas(XmlSchemaSet schemaSet);

		// Token: 0x0600035B RID: 859 RVA: 0x0000B684 File Offset: 0x00009884
		internal XmlSchemaSet GetSchemaSet()
		{
			if (this.m_initialized)
			{
				return this.m_schemaSet;
			}
			XmlNameTable nameTable = ModelingXmlSchema.m_nameTable;
			lock (nameTable)
			{
				this.m_schemaSet = new XmlSchemaSet(ModelingXmlSchema.m_nameTable);
				this.AddXmlSchemas(this.m_schemaSet);
				if (this.m_schemaSet.Count > 0)
				{
					this.m_schemaSet.Compile();
				}
				else
				{
					this.m_schemaSet = null;
				}
				this.m_initialized = true;
			}
			return this.m_schemaSet;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B71C File Offset: 0x0000991C
		protected static XmlSchema ReadXmlSchema(string schemaName)
		{
			XmlSchema xmlSchema;
			using (StreamReader streamReader = new StreamReader(Internal.GetEmbeddedResource(schemaName)))
			{
				using (XmlReader xmlReader = ModelingXmlSchema.CreateSchemaReader(streamReader))
				{
					xmlSchema = XmlSchema.Read(xmlReader, null);
				}
			}
			return xmlSchema;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B778 File Offset: 0x00009978
		protected static XmlSchema ReadXmlSchemaWithTransform(string schemaName, string transformName, XsltArgumentList arguments)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(XmlReader.Create(Internal.GetEmbeddedResource(transformName)));
			XPathDocument xpathDocument = new XPathDocument(ModelingXmlSchema.CreateSchemaReader(new StreamReader(Internal.GetEmbeddedResource(schemaName))));
			XmlSchema xmlSchema;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
				{
					xslCompiledTransform.Transform(xpathDocument, arguments, xmlWriter);
				}
				xmlSchema = XmlSchema.Read(ModelingXmlSchema.CreateSchemaReader(new StringReader(stringWriter.ToString())), null);
			}
			return xmlSchema;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B81C File Offset: 0x00009A1C
		private static XmlReader CreateSchemaReader(TextReader reader)
		{
			return XmlReader.Create(reader, new XmlReaderSettings
			{
				NameTable = ModelingXmlSchema.m_nameTable
			});
		}

		// Token: 0x04000202 RID: 514
		private static readonly XmlNameTable m_nameTable = new NameTable();

		// Token: 0x04000203 RID: 515
		private volatile bool m_initialized;

		// Token: 0x04000204 RID: 516
		private XmlSchemaSet m_schemaSet;
	}
}
