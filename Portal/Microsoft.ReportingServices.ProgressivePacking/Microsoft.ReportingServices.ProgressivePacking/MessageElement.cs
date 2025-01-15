using System;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x0200000B RID: 11
	internal class MessageElement
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002619 File Offset: 0x00000819
		internal MessageElement(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000262F File Offset: 0x0000082F
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002637 File Offset: 0x00000837
		internal object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000014 RID: 20
		private string m_name;

		// Token: 0x04000015 RID: 21
		private object m_value;
	}
}
