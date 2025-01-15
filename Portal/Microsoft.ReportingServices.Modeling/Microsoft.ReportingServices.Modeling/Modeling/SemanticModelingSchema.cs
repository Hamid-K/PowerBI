using System;
using System.Collections.ObjectModel;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000054 RID: 84
	internal sealed class SemanticModelingSchema : ModelingXmlSchema
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000B84D File Offset: 0x00009A4D
		private SemanticModelingSchema()
		{
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000B855 File Offset: 0x00009A55
		public override string TargetNamespace
		{
			get
			{
				return "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling";
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000B85C File Offset: 0x00009A5C
		protected override void AddXmlSchemas(XmlSchemaSet schemaSet)
		{
			schemaSet.Add(ModelingXmlSchema.ReadXmlSchema("Schema.DataSourceView.xsd"));
			if (this == SemanticModelingSchema.m_strictInstance)
			{
				schemaSet.Add(ModelingXmlSchema.ReadXmlSchema("Schema.SemanticModeling.xsd"));
				return;
			}
			if (this == SemanticModelingSchema.m_relaxedInstance)
			{
				schemaSet.Add(ModelingXmlSchema.ReadXmlSchemaWithTransform("Schema.SemanticModeling.xsd", "Schema.SemanticModelingRelaxed.xslt", null));
				return;
			}
			if (this == SemanticModelingSchema.m_fragmentInstance)
			{
				schemaSet.Add(ModelingXmlSchema.ReadXmlSchemaWithTransform("Schema.SemanticModeling.xsd", "Schema.SemanticModelingFragment.xslt", null));
				return;
			}
			if (this == SemanticModelingSchema.m_fragmentWithReplacementsInstance)
			{
				XsltArgumentList xsltArgumentList = new XsltArgumentList();
				xsltArgumentList.AddParam("allowReplacementTokens", "", true);
				schemaSet.Add(ModelingXmlSchema.ReadXmlSchemaWithTransform("Schema.SemanticModeling.xsd", "Schema.SemanticModelingFragment.xslt", xsltArgumentList));
				return;
			}
			throw new InternalModelingException("Unknown SemanticModelingSchema instance");
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000B91A File Offset: 0x00009B1A
		public static SemanticModelingSchema Strict
		{
			get
			{
				return SemanticModelingSchema.m_strictInstance;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B921 File Offset: 0x00009B21
		public static SemanticModelingSchema Relaxed
		{
			get
			{
				return SemanticModelingSchema.m_relaxedInstance;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000B928 File Offset: 0x00009B28
		public static SemanticModelingSchema Fragment
		{
			get
			{
				return SemanticModelingSchema.m_fragmentInstance;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000B92F File Offset: 0x00009B2F
		internal static SemanticModelingSchema FragmentWithReplacements
		{
			get
			{
				return SemanticModelingSchema.m_fragmentWithReplacementsInstance;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000B936 File Offset: 0x00009B36
		public static XmlNamespacePrefixCollection DefaultNamespacePrefixes
		{
			get
			{
				return SemanticModelingSchema.m_defaultNsPrefixes;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B93D File Offset: 0x00009B3D
		private static XmlNamespacePrefixCollection CreateDefaultNamespacePrefixes()
		{
			XmlNamespacePrefixCollection xmlNamespacePrefixCollection = new XmlNamespacePrefixCollection();
			xmlNamespacePrefixCollection.AddRange(XmlUtil.XmlSchemaNsPrefixes);
			xmlNamespacePrefixCollection.MakeReadOnly();
			return xmlNamespacePrefixCollection;
		}

		// Token: 0x04000205 RID: 517
		public const string Namespace = "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling";

		// Token: 0x04000206 RID: 518
		public static ReadOnlyCollection<string> PreviousNamespaces = new ReadOnlyCollection<string>(new string[0]);

		// Token: 0x04000207 RID: 519
		private const string SemanticModelingXsd = "Schema.SemanticModeling.xsd";

		// Token: 0x04000208 RID: 520
		private const string SemanticModelingRelaxedXslt = "Schema.SemanticModelingRelaxed.xslt";

		// Token: 0x04000209 RID: 521
		private const string SemanticModelingFragmentXslt = "Schema.SemanticModelingFragment.xslt";

		// Token: 0x0400020A RID: 522
		private const string DataSourceViewXsd = "Schema.DataSourceView.xsd";

		// Token: 0x0400020B RID: 523
		private static readonly SemanticModelingSchema m_strictInstance = new SemanticModelingSchema();

		// Token: 0x0400020C RID: 524
		private static readonly SemanticModelingSchema m_relaxedInstance = new SemanticModelingSchema();

		// Token: 0x0400020D RID: 525
		private static readonly SemanticModelingSchema m_fragmentInstance = new SemanticModelingSchema();

		// Token: 0x0400020E RID: 526
		private static readonly SemanticModelingSchema m_fragmentWithReplacementsInstance = new SemanticModelingSchema();

		// Token: 0x0400020F RID: 527
		private static readonly XmlNamespacePrefixCollection m_defaultNsPrefixes = SemanticModelingSchema.CreateDefaultNamespacePrefixes();
	}
}
