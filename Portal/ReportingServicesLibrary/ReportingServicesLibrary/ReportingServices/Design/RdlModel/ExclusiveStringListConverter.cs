using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200040D RID: 1037
	public abstract class ExclusiveStringListConverter : StringListConverter
	{
		// Token: 0x060020FB RID: 8443 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
