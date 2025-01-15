using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D0D RID: 3341
	internal class CubeObjectTableBuilder
	{
		// Token: 0x06005A38 RID: 23096 RVA: 0x0013B5DA File Offset: 0x001397DA
		public static CubeObjectTableBuilder New()
		{
			return new CubeObjectTableBuilder(true, true);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x0013B5E3 File Offset: 0x001397E3
		public static CubeObjectTableBuilder NewWithoutKey()
		{
			return new CubeObjectTableBuilder(false, true);
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x0013B5EC File Offset: 0x001397EC
		public static CubeObjectTableBuilder NewWithoutLink()
		{
			return new CubeObjectTableBuilder(true, false);
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x0013B5F8 File Offset: 0x001397F8
		public static TableValue NewLazy(Action<CubeObjectTableBuilder> populate)
		{
			CubeObjectTableBuilder builder = CubeObjectTableBuilder.New();
			return ListValue.New(DeferredEnumerable.From<IValueReference>(delegate
			{
				populate(builder);
				return builder.objects;
			})).ToTable(builder.GetTableType());
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0013B643 File Offset: 0x00139843
		private CubeObjectTableBuilder(bool haveKey, bool haveLink)
		{
			this.haveKey = haveKey;
			this.recordType = (haveLink ? CubeObjectTableBuilder.ObjectTableRowType : CubeObjectTableBuilder.ObjectTableRowTypeWithoutLink);
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x06005A3D RID: 23101 RVA: 0x0013B672 File Offset: 0x00139872
		public int Count
		{
			get
			{
				return this.objects.Count;
			}
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x0013B67F File Offset: 0x0013987F
		public void AddObject(string id, string name, string description, TextValue kind, IValueReference obj)
		{
			this.AddObject(TextValue.New(id), TextValue.New(name), TextValue.New(description), kind, obj);
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x0013B69D File Offset: 0x0013989D
		public void AddObject(TextValue id, TextValue name, TextValue description, TextValue kind, IValueReference obj)
		{
			this.objects.Add(RecordValue.New(this.recordType, new IValueReference[] { id, name, description, kind, obj }));
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x0013B6D1 File Offset: 0x001398D1
		public void AddObject(string id, string name, TextValue kind, IValueReference obj)
		{
			this.AddObject(id, name, "", kind, obj);
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x0013B6E3 File Offset: 0x001398E3
		public void AddCube(string id, string name, IValueReference cube)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.CubeKind, cube);
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x0013B6F3 File Offset: 0x001398F3
		public void AddSubcube(string id, string name, IValueReference cube)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.SubcubeKind, cube);
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x0013B703 File Offset: 0x00139903
		public void AddDimension(string id, string name, IValueReference dimension)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.DimensionKind, dimension);
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x0013B713 File Offset: 0x00139913
		public void AddDimensionFolder(string id, string name, string description, Value dimension)
		{
			this.AddObject(id, name, description, CubeObjectTableBuilder.DimensionFolderKind, dimension);
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x0013B725 File Offset: 0x00139925
		public void AddDimensionFolder(string id, string name, Value dimension)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.DimensionFolderKind, dimension);
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x0013B738 File Offset: 0x00139938
		public void AddDimensionAttribute(string id, string name, string description, string dimensionId)
		{
			Value value = Value.Null;
			if (dimensionId != null)
			{
				value = BinaryOperator.AddMeta.Invoke(value, RecordValue.New(CubeObjectTableBuilder.DimensionIdKeys, new Value[] { TextValue.New(dimensionId) }));
			}
			this.AddObject(id, name, description, CubeObjectTableBuilder.DimensionAttributeKind, value);
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x0013B784 File Offset: 0x00139984
		public void AddDimensionAttribute(string id, string name, string description)
		{
			this.AddDimensionAttribute(id, name, description, null);
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x0013B790 File Offset: 0x00139990
		public void AddDimensionAttribute(string id, string name)
		{
			this.AddDimensionAttribute(id, name, "");
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x0013B79F File Offset: 0x0013999F
		public void AddDimensionHierarchyFolder(string id, string name, Value levels)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.DimensionHierarchyFolderKind, levels);
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x0013B7AF File Offset: 0x001399AF
		public void AddFilter(string id, string name, FunctionValue filter)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.FilterKind, filter);
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x0013B7BF File Offset: 0x001399BF
		public void AddFolder(string name, IValueReference folder)
		{
			this.AddFolder(name, name, folder);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0013B7CA File Offset: 0x001399CA
		public void AddFolder(string id, string name, IValueReference folder)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.FolderKind, folder);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x0013B7DA File Offset: 0x001399DA
		public void AddFolder(string id, string name, TextValue kind, IValueReference folder)
		{
			this.AddObject(id, name, kind, folder);
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x0013B7E7 File Offset: 0x001399E7
		public void AddMeasure(string id, string name, string description, MeasureValue measure)
		{
			this.AddObject(id, name, description, CubeObjectTableBuilder.MeasureKind, measure);
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x0013B7F9 File Offset: 0x001399F9
		public void AddMeasure(string id, string name, MeasureValue measure)
		{
			this.AddMeasure(id, name, "", measure);
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x0013B809 File Offset: 0x00139A09
		public void AddMeasureGroup(string id, string name, TableValue measureGroup)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.MeasureGroupKind, measureGroup);
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x0013B819 File Offset: 0x00139A19
		public void AddMeasurePropertiesFolder(string id, string name, Value measureProperties)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.MeasurePropertyFolderKind, measureProperties);
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x0013B829 File Offset: 0x00139A29
		public void AddCubeViewFolder(string id, string name, IValueReference displayFolders)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.CubeViewFolderKind, displayFolders);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0013B839 File Offset: 0x00139A39
		public void AddCubeView(string id, string name, IValueReference displayFolders)
		{
			this.AddObject(id, name, CubeObjectTableBuilder.CubeViewKind, displayFolders);
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x0013B849 File Offset: 0x00139A49
		public TableValue ToTable()
		{
			return ListValue.New(this.objects).ToTable(this.GetTableType());
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0013B864 File Offset: 0x00139A64
		private TableTypeValue GetTableType()
		{
			TableKey[] array;
			if (!this.haveKey)
			{
				array = EmptyArray<TableKey>.Instance;
			}
			else
			{
				(array = new TableKey[1])[0] = new TableKey(new int[1], true);
			}
			TableKey[] array2 = array;
			TableTypeValue tableTypeValue = TableTypeValue.New(this.recordType, array2);
			NavigationTableTypeValueBuilder navigationTableTypeValueBuilder = new NavigationTableTypeValueBuilder(tableTypeValue, 5);
			navigationTableTypeValueBuilder.AddNameColumnName("Name");
			navigationTableTypeValueBuilder.AddDataColumnName("Data");
			navigationTableTypeValueBuilder.AddIdColumnName("Id");
			navigationTableTypeValueBuilder.AddKindColumnName("Kind");
			navigationTableTypeValueBuilder.AddDescriptionColumnName("Description");
			return navigationTableTypeValueBuilder.ToTypeValue();
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0013B8EF File Offset: 0x00139AEF
		public static Value GetObjectById(TableValue table, TextValue id)
		{
			return table[RecordValue.New(CubeObjectTableBuilder.IdKeys, new Value[] { id })]["Data"];
		}

		// Token: 0x0400326D RID: 12909
		public static readonly TextValue CubeKind = TextValue.New("Cube");

		// Token: 0x0400326E RID: 12910
		public static readonly TextValue DimensionKind = TextValue.New("Dimension");

		// Token: 0x0400326F RID: 12911
		public static readonly TextValue DimensionFolderKind = TextValue.New("DimensionFolder");

		// Token: 0x04003270 RID: 12912
		public static readonly TextValue DimensionAttributeKind = TextValue.New("DimensionAttribute");

		// Token: 0x04003271 RID: 12913
		public static readonly TextValue DimensionPropertyKind = TextValue.New("DimensionProperty");

		// Token: 0x04003272 RID: 12914
		public static readonly TextValue DimensionPropertyFolderKind = TextValue.New("DimensionPropertyFolder");

		// Token: 0x04003273 RID: 12915
		public static readonly TextValue MeasurePropertyKind = TextValue.New("MeasureProperty");

		// Token: 0x04003274 RID: 12916
		public static readonly TextValue MeasurePropertyFolderKind = TextValue.New("MeasurePropertyFolder");

		// Token: 0x04003275 RID: 12917
		public static readonly TextValue DimensionHierarchyFolderKind = TextValue.New("DimensionHierarchyFolder");

		// Token: 0x04003276 RID: 12918
		public static readonly TextValue CubeViewKind = TextValue.New("CubeView");

		// Token: 0x04003277 RID: 12919
		public static readonly TextValue CubeViewFolderKind = TextValue.New("CubeViewFolder");

		// Token: 0x04003278 RID: 12920
		public static readonly TextValue FilterKind = TextValue.New("Filter");

		// Token: 0x04003279 RID: 12921
		public static readonly TextValue FolderKind = TextValue.New("Folder");

		// Token: 0x0400327A RID: 12922
		public static readonly TextValue MeasureKind = TextValue.New("Measure");

		// Token: 0x0400327B RID: 12923
		public static readonly TextValue MeasureGroupKind = TextValue.New("MeasureGroup");

		// Token: 0x0400327C RID: 12924
		public static readonly TextValue MeasureFolderKind = TextValue.New("MeasureFolder");

		// Token: 0x0400327D RID: 12925
		public static readonly TextValue SubcubeKind = TextValue.New("Subcube");

		// Token: 0x0400327E RID: 12926
		public static readonly TextValue KpiKind = TextValue.New("Kpi");

		// Token: 0x0400327F RID: 12927
		public static readonly TextValue LevelFolderKind = TextValue.New("LevelFolder");

		// Token: 0x04003280 RID: 12928
		private const string idKey = "Id";

		// Token: 0x04003281 RID: 12929
		private const string dimensionIdKey = "DimensionId";

		// Token: 0x04003282 RID: 12930
		private static readonly Keys IdKeys = Keys.New("Id");

		// Token: 0x04003283 RID: 12931
		private static readonly Keys DimensionIdKeys = Keys.New("DimensionId");

		// Token: 0x04003284 RID: 12932
		private static readonly Keys ObjectTableColumns = Keys.New(new string[] { "Id", "Name", "Description", "Kind", "Data" });

		// Token: 0x04003285 RID: 12933
		private static readonly RecordTypeValue ObjectTableRowType = RecordTypeValue.New(RecordValue.New(CubeObjectTableBuilder.ObjectTableColumns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Any), false)
		}));

		// Token: 0x04003286 RID: 12934
		private static readonly RecordTypeValue ObjectTableRowTypeWithoutLink = RecordTypeValue.New(RecordValue.New(CubeObjectTableBuilder.ObjectTableColumns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeServices.ConvertToInline(TypeValue.Any), false)
		}));

		// Token: 0x04003287 RID: 12935
		private readonly bool haveKey;

		// Token: 0x04003288 RID: 12936
		private readonly RecordTypeValue recordType;

		// Token: 0x04003289 RID: 12937
		private readonly List<IValueReference> objects = new List<IValueReference>();
	}
}
