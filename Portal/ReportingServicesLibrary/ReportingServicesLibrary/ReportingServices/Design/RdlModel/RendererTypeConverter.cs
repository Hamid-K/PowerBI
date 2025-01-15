using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003CC RID: 972
	internal sealed class RendererTypeConverter : ExclusiveStringListConverter
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0007E379 File Offset: 0x0007C579
		internal override string[] Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x04000D9D RID: 3485
		private string[] m_values = new string[] { "HTML" };
	}
}
