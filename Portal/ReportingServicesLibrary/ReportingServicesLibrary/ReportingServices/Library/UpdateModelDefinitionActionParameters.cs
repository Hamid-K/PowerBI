using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000158 RID: 344
	internal abstract class UpdateModelDefinitionActionParameters : UpdateItemActionParameters
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00030383 File Offset: 0x0002E583
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0003038B File Offset: 0x0002E58B
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
			set
			{
				this.m_warnings = value;
			}
		}

		// Token: 0x0400053A RID: 1338
		private Warning[] m_warnings;
	}
}
