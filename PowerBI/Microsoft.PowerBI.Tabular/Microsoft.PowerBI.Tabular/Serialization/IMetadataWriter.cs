using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000176 RID: 374
	internal interface IMetadataWriter
	{
		// Token: 0x060017A0 RID: 6048
		bool ShouldIncludeProperty(string propertyName, MetadataPropertyNature propertyNature);

		// Token: 0x060017A1 RID: 6049
		void WriteObjectIdProperty(string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object);

		// Token: 0x060017A2 RID: 6050
		void WriteObjectTypeProperty(string propertyName, MetadataPropertyNature propertyNature, ObjectType objectType);

		// Token: 0x060017A3 RID: 6051
		void WriteStringProperty(string propertyName, MetadataPropertyNature propertyNature, string value);

		// Token: 0x060017A4 RID: 6052
		void WriteInt32Property(string propertyName, MetadataPropertyNature propertyNature, int value);

		// Token: 0x060017A5 RID: 6053
		void WriteUInt32Property(string propertyName, MetadataPropertyNature propertyNature, uint value);

		// Token: 0x060017A6 RID: 6054
		void WriteInt64Property(string propertyName, MetadataPropertyNature propertyNature, long value);

		// Token: 0x060017A7 RID: 6055
		void WriteUInt64Property(string propertyName, MetadataPropertyNature propertyNature, ulong value);

		// Token: 0x060017A8 RID: 6056
		void WriteBooleanProperty(string propertyName, MetadataPropertyNature propertyNature, bool value);

		// Token: 0x060017A9 RID: 6057
		void WriteDoubleProperty(string propertyName, MetadataPropertyNature propertyNature, double value);

		// Token: 0x060017AA RID: 6058
		void WriteDateTimeProperty(string propertyName, MetadataPropertyNature propertyNature, DateTime value);

		// Token: 0x060017AB RID: 6059
		void WriteEnumProperty<TEnum>(string propertyName, MetadataPropertyNature propertyNature, TEnum value) where TEnum : Enum;

		// Token: 0x060017AC RID: 6060
		void WriteProperty(string propertyName, MetadataPropertyNature propertyNature, Type type, object value);

		// Token: 0x060017AD RID: 6061
		void WriteProperty<TValue>(string propertyName, MetadataPropertyNature propertyNature, TValue value);

		// Token: 0x060017AE RID: 6062
		void WriteCrossLinkProperty(string propertyName, MetadataPropertyNature propertyNature, ObjectPath value);

		// Token: 0x060017AF RID: 6063
		void WriteCustomJsonProperty(string propertyName, MetadataPropertyNature propertyNature, JToken token);

		// Token: 0x060017B0 RID: 6064
		void WriteSingleChild(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object);

		// Token: 0x060017B1 RID: 6065
		void WriteChildCollection(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, IEnumerable<MetadataObject> objects);

		// Token: 0x060017B2 RID: 6066
		void WriteComplexProperty(string propertyName, MetadataPropertyNature propertyNature, IEnumerable<MetadataProperty> properties);

		// Token: 0x060017B3 RID: 6067
		void StartComplexProperty(string propertyName, MetadataPropertyNature propertyNature);

		// Token: 0x060017B4 RID: 6068
		void CompleteComplexProperty();

		// Token: 0x060017B5 RID: 6069
		void StartComplexPropertyCollection(string collectionName, MetadataPropertyNature collectionNature);

		// Token: 0x060017B6 RID: 6070
		void CompleteComplexPropertyCollection();
	}
}
