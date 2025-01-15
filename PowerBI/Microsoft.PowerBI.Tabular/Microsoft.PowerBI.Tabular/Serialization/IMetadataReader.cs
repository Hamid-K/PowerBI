using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000172 RID: 370
	internal interface IMetadataReader
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600176F RID: 5999
		string PropertyName { get; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001770 RID: 6000
		bool CanReset { get; }

		// Token: 0x06001771 RID: 6001
		ObjectId ReadObjectIdProperty();

		// Token: 0x06001772 RID: 6002
		ObjectType ReadObjectTypeProperty();

		// Token: 0x06001773 RID: 6003
		string ReadStringProperty();

		// Token: 0x06001774 RID: 6004
		int ReadInt32Property();

		// Token: 0x06001775 RID: 6005
		uint ReadUInt32Property();

		// Token: 0x06001776 RID: 6006
		long ReadInt64Property();

		// Token: 0x06001777 RID: 6007
		ulong ReadUInt64Property();

		// Token: 0x06001778 RID: 6008
		bool ReadBooleanProperty();

		// Token: 0x06001779 RID: 6009
		double ReadDoubleProperty();

		// Token: 0x0600177A RID: 6010
		DateTime ReadDateTimeProperty();

		// Token: 0x0600177B RID: 6011
		TEnum ReadEnumProperty<TEnum>();

		// Token: 0x0600177C RID: 6012
		TPropertyValue ReadProperty<TPropertyValue>();

		// Token: 0x0600177D RID: 6013
		ObjectPath ReadCrossLinkProperty();

		// Token: 0x0600177E RID: 6014
		ObjectPath ReadCrossLinkProperty(Func<string, ObjectPath> nameToPathConverter);

		// Token: 0x0600177F RID: 6015
		bool TryReadCustomJsonProperty(out JToken token);

		// Token: 0x06001780 RID: 6016
		TMetadataObject ReadSingleChildProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject;

		// Token: 0x06001781 RID: 6017
		IEnumerable<TMetadataObject> ReadChildCollectionProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject;

		// Token: 0x06001782 RID: 6018
		IMetadataReader ReadComplexProperty(bool canReset);

		// Token: 0x06001783 RID: 6019
		IEnumerable<IMetadataReader> ReadComplexPropertyCollection(bool canReset);

		// Token: 0x06001784 RID: 6020
		void Skip();

		// Token: 0x06001785 RID: 6021
		void Reset();

		// Token: 0x06001786 RID: 6022
		Exception CreateUnexpectedPropertyException(SerializationActivityContext context, UnexpectedPropertyClassification classification);

		// Token: 0x06001787 RID: 6023
		Exception CreateInvalidDataException(SerializationActivityContext context, string error, Exception e = null);

		// Token: 0x06001788 RID: 6024
		Exception CreateInvalidChildException(SerializationActivityContext context, MetadataObject child, string error, Exception e = null);
	}
}
