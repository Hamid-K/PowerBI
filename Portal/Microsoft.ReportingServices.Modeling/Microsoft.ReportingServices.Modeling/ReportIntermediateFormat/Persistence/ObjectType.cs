using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000014 RID: 20
	public enum ObjectType
	{
		// Token: 0x0400000E RID: 14
		Null,
		// Token: 0x0400000F RID: 15
		None,
		// Token: 0x04000010 RID: 16
		RIFObjectArray,
		// Token: 0x04000011 RID: 17
		RIFObjectList,
		// Token: 0x04000012 RID: 18
		PrimitiveArray,
		// Token: 0x04000013 RID: 19
		PrimitiveList,
		// Token: 0x04000014 RID: 20
		PrimitiveTypedArray,
		// Token: 0x04000015 RID: 21
		StringRIFObjectDictionary,
		// Token: 0x04000016 RID: 22
		StringRIFObjectHashtable,
		// Token: 0x04000017 RID: 23
		NameObjectCollection,
		// Token: 0x04000018 RID: 24
		Int32RIFObjectDictionary,
		// Token: 0x04000019 RID: 25
		Int32PrimitiveListHashtable,
		// Token: 0x0400001A RID: 26
		ObjectHashtableHashtable,
		// Token: 0x0400001B RID: 27
		StringObjectHashtable,
		// Token: 0x0400001C RID: 28
		IntermediateFormatVersion,
		// Token: 0x0400001D RID: 29
		String,
		// Token: 0x0400001E RID: 30
		CultureInfo,
		// Token: 0x0400001F RID: 31
		Declaration,
		// Token: 0x04000020 RID: 32
		RefHelper,
		// Token: 0x04000021 RID: 33
		DsvColumn,
		// Token: 0x04000022 RID: 34
		DsvColumnCollection,
		// Token: 0x04000023 RID: 35
		DsvTable,
		// Token: 0x04000024 RID: 36
		DsvRelation,
		// Token: 0x04000025 RID: 37
		DsvConstraint,
		// Token: 0x04000026 RID: 38
		DsvForeignKeyConstraint,
		// Token: 0x04000027 RID: 39
		DsvUniqueConstraint,
		// Token: 0x04000028 RID: 40
		DsvConstraintCollection,
		// Token: 0x04000029 RID: 41
		DsvRelationCollection,
		// Token: 0x0400002A RID: 42
		DsvTableCollection,
		// Token: 0x0400002B RID: 43
		DataSourceView,
		// Token: 0x0400002C RID: 44
		DataSourceViewSchema,
		// Token: 0x0400002D RID: 45
		ModelItem,
		// Token: 0x0400002E RID: 46
		PerspectiveModelItemCollection,
		// Token: 0x0400002F RID: 47
		ModelItemCollection,
		// Token: 0x04000030 RID: 48
		OwnedModelItemCollection,
		// Token: 0x04000031 RID: 49
		Perspective,
		// Token: 0x04000032 RID: 50
		Binding,
		// Token: 0x04000033 RID: 51
		TableBinding,
		// Token: 0x04000034 RID: 52
		ColumnBinding,
		// Token: 0x04000035 RID: 53
		RelationBinding,
		// Token: 0x04000036 RID: 54
		Expression,
		// Token: 0x04000037 RID: 55
		ResultTypePersistable,
		// Token: 0x04000038 RID: 56
		ModelingObject,
		// Token: 0x04000039 RID: 57
		ExtensibleModelingObject,
		// Token: 0x0400003A RID: 58
		ExpressionPath,
		// Token: 0x0400003B RID: 59
		ExpressionNode,
		// Token: 0x0400003C RID: 60
		FunctionNode,
		// Token: 0x0400003D RID: 61
		AttributeRefNode,
		// Token: 0x0400003E RID: 62
		EntityRefNode,
		// Token: 0x0400003F RID: 63
		LiteralNode,
		// Token: 0x04000040 RID: 64
		NullNode,
		// Token: 0x04000041 RID: 65
		ExpressionCollection,
		// Token: 0x04000042 RID: 66
		PathItem,
		// Token: 0x04000043 RID: 67
		RolePathItem,
		// Token: 0x04000044 RID: 68
		InheritancePathItem,
		// Token: 0x04000045 RID: 69
		ModelRole,
		// Token: 0x04000046 RID: 70
		QName,
		// Token: 0x04000047 RID: 71
		CustomProperty,
		// Token: 0x04000048 RID: 72
		CustomPropertyCollection,
		// Token: 0x04000049 RID: 73
		ModelFieldFolderItem,
		// Token: 0x0400004A RID: 74
		ModelFieldFolder,
		// Token: 0x0400004B RID: 75
		ModelField,
		// Token: 0x0400004C RID: 76
		ModelAttribute,
		// Token: 0x0400004D RID: 77
		AttributeReference,
		// Token: 0x0400004E RID: 78
		AttributeReferenceCollection,
		// Token: 0x0400004F RID: 79
		SortAttribute,
		// Token: 0x04000050 RID: 80
		SortAttributeCollection,
		// Token: 0x04000051 RID: 81
		EntityInheritance,
		// Token: 0x04000052 RID: 82
		ModelEntityFolderItem,
		// Token: 0x04000053 RID: 83
		ModelEntity,
		// Token: 0x04000054 RID: 84
		ModelEntityFolder,
		// Token: 0x04000055 RID: 85
		HiddenFieldCollection,
		// Token: 0x04000056 RID: 86
		SemanticModel,
		// Token: 0x04000057 RID: 87
		CheckedCollection,
		// Token: 0x04000058 RID: 88
		NamespacePrefixes,
		// Token: 0x04000059 RID: 89
		StringInt32Hashtable,
		// Token: 0x0400005A RID: 90
		StringStringHashtable,
		// Token: 0x0400005B RID: 91
		RIFObjectStringHashtable,
		// Token: 0x0400005C RID: 92
		StringListOfStringDictionary,
		// Token: 0x0400005D RID: 93
		VariantVariantHashtable,
		// Token: 0x0400005E RID: 94
		Int32StringHashtable,
		// Token: 0x0400005F RID: 95
		VariantRifObjectDictionary,
		// Token: 0x04000060 RID: 96
		VariantListOfRifObjectDictionary,
		// Token: 0x04000061 RID: 97
		VariantList,
		// Token: 0x04000062 RID: 98
		RIFObject,
		// Token: 0x04000063 RID: 99
		VariantListVariantDictionary,
		// Token: 0x04000064 RID: 100
		Array2D,
		// Token: 0x04000065 RID: 101
		StringVariantListDictionary,
		// Token: 0x04000066 RID: 102
		ByteVariantHashtable,
		// Token: 0x04000067 RID: 103
		StringBoolArrayDictionary,
		// Token: 0x04000068 RID: 104
		Int32SerializableDictionary,
		// Token: 0x04000069 RID: 105
		SerializableArray,
		// Token: 0x0400006A RID: 106
		NLevelVariantHashtable,
		// Token: 0x0400006B RID: 107
		MaxValue
	}
}
