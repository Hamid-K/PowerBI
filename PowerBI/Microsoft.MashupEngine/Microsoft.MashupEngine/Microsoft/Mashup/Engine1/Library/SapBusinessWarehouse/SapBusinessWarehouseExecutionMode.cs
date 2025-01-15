using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x0200048A RID: 1162
	internal static class SapBusinessWarehouseExecutionMode
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x0006FFF0 File Offset: 0x0006E1F0
		public static string ExecutionModeAsString(SapBusinessWarehouseExecutionModeOption value)
		{
			string text;
			if (!SapBusinessWarehouseExecutionMode.ExecutionModeToText.TryGetValue(value, out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x04001010 RID: 4112
		public static readonly IntEnumTypeValue<SapBusinessWarehouseExecutionModeOption> Type = new IntEnumTypeValue<SapBusinessWarehouseExecutionModeOption>("SapBusinessWarehouseExecutionMode.Type");

		// Token: 0x04001011 RID: 4113
		public static readonly NumberValue BasXml = SapBusinessWarehouseExecutionMode.Type.NewEnumValue("SapBusinessWarehouseExecutionMode.BasXml", 64, SapBusinessWarehouseExecutionModeOption.BasXml, null);

		// Token: 0x04001012 RID: 4114
		public static readonly NumberValue BasXmlGzip = SapBusinessWarehouseExecutionMode.Type.NewEnumValue("SapBusinessWarehouseExecutionMode.BasXmlGzip", 65, SapBusinessWarehouseExecutionModeOption.BasXmlGzip, null);

		// Token: 0x04001013 RID: 4115
		public static readonly NumberValue DataStream = SapBusinessWarehouseExecutionMode.Type.NewEnumValue("SapBusinessWarehouseExecutionMode.DataStream", 66, SapBusinessWarehouseExecutionModeOption.DataStream, null);

		// Token: 0x04001014 RID: 4116
		private static readonly Dictionary<SapBusinessWarehouseExecutionModeOption, string> ExecutionModeToText = new Dictionary<SapBusinessWarehouseExecutionModeOption, string>
		{
			{
				SapBusinessWarehouseExecutionModeOption.BasXml,
				"BasXml"
			},
			{
				SapBusinessWarehouseExecutionModeOption.BasXmlGzip,
				"BasXmlGzip"
			},
			{
				SapBusinessWarehouseExecutionModeOption.DataStream,
				"DataStream"
			}
		};
	}
}
