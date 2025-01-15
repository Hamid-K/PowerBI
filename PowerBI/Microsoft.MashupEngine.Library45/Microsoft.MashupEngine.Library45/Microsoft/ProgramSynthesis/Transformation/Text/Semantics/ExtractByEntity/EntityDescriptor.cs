using System;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D6F RID: 7535
	public class EntityDescriptor
	{
		// Token: 0x17002A3A RID: 10810
		// (get) Token: 0x0600FD86 RID: 64902 RVA: 0x00362AB0 File Offset: 0x00360CB0
		public string EntityName { get; }

		// Token: 0x17002A3B RID: 10811
		// (get) Token: 0x0600FD87 RID: 64903 RVA: 0x00362AB8 File Offset: 0x00360CB8
		public Type Type { get; }

		// Token: 0x17002A3C RID: 10812
		// (get) Token: 0x0600FD88 RID: 64904 RVA: 0x00362AC0 File Offset: 0x00360CC0
		public EntityType EntityType { get; }

		// Token: 0x17002A3D RID: 10813
		// (get) Token: 0x0600FD89 RID: 64905 RVA: 0x00362AC8 File Offset: 0x00360CC8
		public EntityDescriptor.ExtractorFactoryDelegate ExtractorFactory { get; }

		// Token: 0x17002A3E RID: 10814
		// (get) Token: 0x0600FD8A RID: 64906 RVA: 0x00362AD0 File Offset: 0x00360CD0
		public string PythonTypeName { get; }

		// Token: 0x17002A3F RID: 10815
		// (get) Token: 0x0600FD8B RID: 64907 RVA: 0x00362AD8 File Offset: 0x00360CD8
		public Type ExtractorType { get; }

		// Token: 0x17002A40 RID: 10816
		// (get) Token: 0x0600FD8C RID: 64908 RVA: 0x00362AE0 File Offset: 0x00360CE0
		public string PythonExtractorTypeName { get; }

		// Token: 0x0600FD8D RID: 64909 RVA: 0x00362AE8 File Offset: 0x00360CE8
		public EntityDescriptor(string entityName, Type type, EntityType entityType, string pythonTypeName, Type extractorType, string pythonExtractorTypeName, EntityDescriptor.ExtractorFactoryDelegate extractorFactory)
		{
			this.EntityName = entityName;
			this.Type = type;
			this.EntityType = entityType;
			this.PythonTypeName = pythonTypeName;
			this.ExtractorType = extractorType;
			this.PythonExtractorTypeName = pythonExtractorTypeName;
			this.ExtractorFactory = extractorFactory;
		}

		// Token: 0x0600FD8E RID: 64910 RVA: 0x00362B28 File Offset: 0x00360D28
		public EntityDescriptor(string entityName, Type type, EntityType entityType, Type extractorType)
		{
			this.EntityName = entityName;
			this.Type = type;
			this.EntityType = entityType;
			this.ExtractorType = extractorType;
			this.ExtractorFactory = () => (EntityBasedTokenizer)Activator.CreateInstance(this.ExtractorType);
			this.PythonExtractorTypeName = this.ExtractorType.Name;
			this.PythonTypeName = this.Type.Name;
		}

		// Token: 0x02001D70 RID: 7536
		// (Invoke) Token: 0x0600FD91 RID: 64913
		public delegate EntityBasedTokenizer ExtractorFactoryDelegate();
	}
}
