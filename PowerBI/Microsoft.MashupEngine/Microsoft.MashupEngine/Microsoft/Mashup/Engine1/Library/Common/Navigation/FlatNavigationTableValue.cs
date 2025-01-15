using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001165 RID: 4453
	internal sealed class FlatNavigationTableValue : TableValue
	{
		// Token: 0x060074A9 RID: 29865 RVA: 0x00190709 File Offset: 0x0018E909
		public FlatNavigationTableValue(IMultiLevelNavigationProvider navigator)
		{
			this.navigator = navigator;
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x060074AA RID: 29866 RVA: 0x00190718 File Offset: 0x0018E918
		public override TypeValue Type
		{
			get
			{
				return FlatNavigationTableValue.type;
			}
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x0019071F File Offset: 0x0018E91F
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return (from item in this.navigator.GetTableItems(Restriction.Any, Restriction.Any)
				select RecordValue.New(FlatNavigationTableValue.keys, new IValueReference[]
				{
					TextValue.New(this.navigator.GetQualifiedTableName(item.CatalogName, item.SchemaName, item.Name)),
					TextValue.New(item.Name),
					TextValue.NewOrNull(item.SchemaName),
					TextValue.NewOrNull(item.CatalogName),
					item.Description,
					this.navigator.CreateDataTable(item.CatalogName, item.SchemaName, item.Name, item.TableType),
					item.Kind
				})).GetEnumerator();
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x0019074C File Offset: 0x0018E94C
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.navigator.NativeQuery(this, query, parameters, options, null);
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x0019075E File Offset: 0x0018E95E
		public override void TestConnection()
		{
			this.navigator.TestConnection();
		}

		// Token: 0x04004022 RID: 16418
		private const string nameKey = "Name";

		// Token: 0x04004023 RID: 16419
		private const string dataKey = "Data";

		// Token: 0x04004024 RID: 16420
		private const string kindKey = "Kind";

		// Token: 0x04004025 RID: 16421
		private static readonly Keys keys = Keys.New(new string[] { "Name", "Item", "Schema", "Catalog", "Description", "Data", "Kind" });

		// Token: 0x04004026 RID: 16422
		private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(FlatNavigationTableValue.keys, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NullableTypeValue.Text, false),
			RecordTypeAlgebra.NewField(NullableTypeValue.Text, false),
			RecordTypeAlgebra.NewField(NullableTypeValue.Text, false),
			RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Table", true), false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(new int[] { 1, 2, 3 }, true)
		}), TextValue.New("Name"), TextValue.New("Data"), TextValue.New("Kind"));

		// Token: 0x04004027 RID: 16423
		private readonly IMultiLevelNavigationProvider navigator;
	}
}
