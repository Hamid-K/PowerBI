using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x020003A5 RID: 933
	internal class TableValueTransformer : TableValue
	{
		// Token: 0x0600206F RID: 8303 RVA: 0x000555FA File Offset: 0x000537FA
		public TableValueTransformer(TableValue table, TableTypeValue resultingTableType, Func<TypeValue, Value, Value> transformer)
		{
			this.table = table;
			this.type = resultingTableType;
			this.transformer = transformer;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x00055617 File Offset: 0x00053817
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x0005561F File Offset: 0x0005381F
		public override long LargeCount
		{
			get
			{
				return this.table.LargeCount;
			}
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0005562C File Offset: 0x0005382C
		public static TableValue Transform(TableValue table, TableTypeValue resultingTableType, Func<TypeValue, Value, Value> transformer)
		{
			return new TableValueTransformer(table, resultingTableType, transformer);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00055636 File Offset: 0x00053836
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			return this.table.TryGetProcessor(out processor);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00055644 File Offset: 0x00053844
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.table.Select(new Func<IValueReference, IValueReference>(this.TransformReference)).GetEnumerator();
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00055662 File Offset: 0x00053862
		private IValueReference TransformReference(IValueReference reference)
		{
			return new TransformValueReference(reference, new TransfromValue(this.type.ItemType, this.transformer));
		}

		// Token: 0x04000C67 RID: 3175
		private readonly TableValue table;

		// Token: 0x04000C68 RID: 3176
		private readonly TableTypeValue type;

		// Token: 0x04000C69 RID: 3177
		private readonly Func<TypeValue, Value, Value> transformer;
	}
}
