using System;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Interop
{
	// Token: 0x02001FEC RID: 8172
	internal static class NodeExtensions
	{
		// Token: 0x060110EE RID: 69870 RVA: 0x003AD6CC File Offset: 0x003AB8CC
		public static TResult WithLogicalType<TResult>(this Node node, Func<LogicalType, TResult> func)
		{
			TResult tresult;
			using (LogicalType logicalType = node.LogicalType)
			{
				tresult = func(logicalType);
			}
			return tresult;
		}

		// Token: 0x060110EF RID: 69871 RVA: 0x003AD708 File Offset: 0x003AB908
		public static LogicalTypeEnum LogicalTypeType(this Node node)
		{
			return node.WithLogicalType((LogicalType logicalType) => logicalType.Type);
		}
	}
}
