using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.ExcelTableInference
{
	// Token: 0x0200027A RID: 634
	public sealed class ExcelTableInferenceModule : Module45
	{
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00034E2D File Offset: 0x0003302D
		public override string Name
		{
			get
			{
				return "ExcelTableInference";
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00034E34 File Offset: 0x00033034
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "ExcelTableInference.InferTables";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040007BE RID: 1982
		public const string ExcelTableInferenceInferTablesName = "ExcelTableInference.InferTables";

		// Token: 0x040007BF RID: 1983
		private Keys exportKeys;

		// Token: 0x0200027B RID: 635
		private enum Exports
		{
			// Token: 0x040007C1 RID: 1985
			ExcelTableInference_InferTables,
			// Token: 0x040007C2 RID: 1986
			Count
		}
	}
}
