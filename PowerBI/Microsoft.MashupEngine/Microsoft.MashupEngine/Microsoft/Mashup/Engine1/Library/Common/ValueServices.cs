using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001160 RID: 4448
	internal static class ValueServices
	{
		// Token: 0x0600748E RID: 29838 RVA: 0x001900A1 File Offset: 0x0018E2A1
		public static RecordValue AddFirstRowMayContainHeadersMeta(RecordValue oldMeta)
		{
			return oldMeta.Concatenate(ValueServices.FirstRowMayContainHeaders).AsRecord;
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x001900B3 File Offset: 0x0018E2B3
		public static TableValue AddFirstRowMayContainHeadersMeta(TableValue table)
		{
			return table.NewMeta(ValueServices.AddFirstRowMayContainHeadersMeta(table.MetaValue)).AsTable;
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x001900CB File Offset: 0x0018E2CB
		public static RecordValue AddShouldInferTableTypeMeta(RecordValue oldMeta)
		{
			return oldMeta.Concatenate(ValueServices.ShouldInferTableType).AsRecord;
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x001900DD File Offset: 0x0018E2DD
		public static TableValue AddShouldInferTableTypeMeta(TableValue table)
		{
			return table.NewMeta(ValueServices.AddShouldInferTableTypeMeta(table.MetaValue)).AsTable;
		}

		// Token: 0x04004015 RID: 16405
		public const string FirstRowMayContainHeadersMetaString = "FirstRowMayContainHeaders";

		// Token: 0x04004016 RID: 16406
		public const string ShouldInferTableTypeMetaString = "ShouldInferTableType";

		// Token: 0x04004017 RID: 16407
		private static readonly Keys FirstRowMayContainHeadersMetaKey = Keys.New("FirstRowMayContainHeaders");

		// Token: 0x04004018 RID: 16408
		private static readonly Keys ShouldInferTableTypeMetaKey = Keys.New("ShouldInferTableType");

		// Token: 0x04004019 RID: 16409
		private static readonly RecordValue FirstRowMayContainHeaders = RecordValue.New(ValueServices.FirstRowMayContainHeadersMetaKey, new Value[] { Value.Null });

		// Token: 0x0400401A RID: 16410
		private static readonly RecordValue ShouldInferTableType = RecordValue.New(ValueServices.ShouldInferTableTypeMetaKey, new Value[] { Value.Null });
	}
}
