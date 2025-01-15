using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200009B RID: 155
	internal enum IndexAffectingStatement
	{
		// Token: 0x040003B5 RID: 949
		AlterTableAddElement,
		// Token: 0x040003B6 RID: 950
		AlterTableRebuildOnePartition,
		// Token: 0x040003B7 RID: 951
		AlterTableRebuildAllPartitions,
		// Token: 0x040003B8 RID: 952
		AlterIndexSet,
		// Token: 0x040003B9 RID: 953
		AlterIndexRebuildOnePartition,
		// Token: 0x040003BA RID: 954
		AlterIndexRebuildAllPartitions,
		// Token: 0x040003BB RID: 955
		AlterIndexReorganize,
		// Token: 0x040003BC RID: 956
		CreateColumnStoreIndex,
		// Token: 0x040003BD RID: 957
		CreateIndex,
		// Token: 0x040003BE RID: 958
		CreateTable,
		// Token: 0x040003BF RID: 959
		CreateType,
		// Token: 0x040003C0 RID: 960
		CreateXmlIndex,
		// Token: 0x040003C1 RID: 961
		CreateOrAlterFunction,
		// Token: 0x040003C2 RID: 962
		DeclareTableVariable,
		// Token: 0x040003C3 RID: 963
		CreateSpatialIndex
	}
}
