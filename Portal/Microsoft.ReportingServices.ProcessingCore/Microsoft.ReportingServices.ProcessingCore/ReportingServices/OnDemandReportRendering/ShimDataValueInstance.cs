using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B1 RID: 689
	internal sealed class ShimDataValueInstance : DataValueInstance
	{
		// Token: 0x06001A60 RID: 6752 RVA: 0x0006A6AC File Offset: 0x000688AC
		internal ShimDataValueInstance(string name, object value)
			: base(null)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0006A6C3 File Offset: 0x000688C3
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0006A6CB File Offset: 0x000688CB
		public override object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0006A6D3 File Offset: 0x000688D3
		internal void Update(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0006A6E3 File Offset: 0x000688E3
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000D26 RID: 3366
		private string m_name;

		// Token: 0x04000D27 RID: 3367
		private object m_value;
	}
}
