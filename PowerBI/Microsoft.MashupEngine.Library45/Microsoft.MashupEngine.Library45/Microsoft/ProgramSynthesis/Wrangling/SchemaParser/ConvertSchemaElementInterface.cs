using System;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200014A RID: 330
	public abstract class ConvertSchemaElementInterface
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00016FF3 File Offset: 0x000151F3
		public string ConverterName { get; }

		// Token: 0x06000757 RID: 1879 RVA: 0x00016FFB File Offset: 0x000151FB
		protected ConvertSchemaElementInterface(string converterName)
		{
			this.ConverterName = converterName;
		}
	}
}
