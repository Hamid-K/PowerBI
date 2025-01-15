using System;
using System.Globalization;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E2 RID: 482
	internal sealed class ListTasksActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0003A287 File Offset: 0x00038487
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0003A28F File Offset: 0x0003848F
		public SecurityScopeEnum Scope
		{
			get
			{
				return this.m_scope;
			}
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0003A298 File Offset: 0x00038498
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0003A2A0 File Offset: 0x000384A0
		public Task[] Tasks
		{
			get
			{
				return this.m_tasks;
			}
			set
			{
				this.m_tasks = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0003A2A9 File Offset: 0x000384A9
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.Scope);
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000664 RID: 1636
		private SecurityScopeEnum m_scope;

		// Token: 0x04000665 RID: 1637
		private Task[] m_tasks;
	}
}
