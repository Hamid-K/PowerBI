using System;
using System.Xml.Schema;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000055 RID: 85
	internal sealed class ModelGenerationSchema : ModelingXmlSchema
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		private ModelGenerationSchema()
		{
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000B9AF File Offset: 0x00009BAF
		public override string TargetNamespace
		{
			get
			{
				return "http://schemas.microsoft.com/sqlserver/2004/10/modelgeneration";
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B9B6 File Offset: 0x00009BB6
		protected override void AddXmlSchemas(XmlSchemaSet schemaSet)
		{
			schemaSet.Add(SemanticModelingSchema.FragmentWithReplacements.GetSchemaSet());
			schemaSet.Add(ModelingXmlSchema.ReadXmlSchema("Schema.ModelGeneration.xsd"));
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000B9D9 File Offset: 0x00009BD9
		public static ModelGenerationSchema Instance
		{
			get
			{
				return ModelGenerationSchema.m_instance;
			}
		}

		// Token: 0x04000210 RID: 528
		public const string Namespace = "http://schemas.microsoft.com/sqlserver/2004/10/modelgeneration";

		// Token: 0x04000211 RID: 529
		private const string ModelGenerationXsd = "Schema.ModelGeneration.xsd";

		// Token: 0x04000212 RID: 530
		private static readonly ModelGenerationSchema m_instance = new ModelGenerationSchema();
	}
}
