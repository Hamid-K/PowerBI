using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000173 RID: 371
	internal interface IMetadataSchemaWriter
	{
		// Token: 0x06001789 RID: 6025
		void StartMetadataObjectScope(ObjectType objectType, string choiceOption, string description);

		// Token: 0x0600178A RID: 6026
		void CompleteMetadataObjectScope(bool? additionalProperties);

		// Token: 0x0600178B RID: 6027
		void StartComplexPropertyScope(string propertyName, MetadataPropertyNature propertyNature, string choiceOption, string description);

		// Token: 0x0600178C RID: 6028
		void CompleteComplexPropertyScope(bool? additionalProperties);

		// Token: 0x0600178D RID: 6029
		void StartCollectionScope(string collectionName, MetadataPropertyNature collectionNature);

		// Token: 0x0600178E RID: 6030
		void CompleteCollectionScope();

		// Token: 0x0600178F RID: 6031
		void StartChoiceScope();

		// Token: 0x06001790 RID: 6032
		void CompleteChoiceScope();

		// Token: 0x06001791 RID: 6033
		bool ShouldIncludeProperty(string propertyName, MetadataPropertyNature propertyNature);

		// Token: 0x06001792 RID: 6034
		void WriteProperty(string propertyName, MetadataPropertyNature propertyNature, Type type);

		// Token: 0x06001793 RID: 6035
		void WriteEnumProperty<TEnum>(string propertyName, MetadataPropertyNature propertyNature, IEnumerable<TEnum> values) where TEnum : Enum;

		// Token: 0x06001794 RID: 6036
		void WriteSingleChild(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, ObjectType objectType);

		// Token: 0x06001795 RID: 6037
		void WriteChildCollection(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, ObjectType objectType);
	}
}
