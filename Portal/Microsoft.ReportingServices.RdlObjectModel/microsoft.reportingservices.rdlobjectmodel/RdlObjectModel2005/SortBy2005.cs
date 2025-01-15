using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000024 RID: 36
	internal class SortBy2005 : SortExpression
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00002F60 File Offset: 0x00001160
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00002F68 File Offset: 0x00001168
		public ReportExpression SortExpression
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002F71 File Offset: 0x00001171
		public SortBy2005()
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002F79 File Offset: 0x00001179
		public SortBy2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
