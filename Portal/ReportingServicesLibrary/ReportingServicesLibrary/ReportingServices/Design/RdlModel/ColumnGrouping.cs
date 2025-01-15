using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003EE RID: 1006
	public sealed class ColumnGrouping : MatrixGrouping
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0007F081 File Offset: 0x0007D281
		// (set) Token: 0x06001FFF RID: 8191 RVA: 0x0007F089 File Offset: 0x0007D289
		public Unit Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0007F092 File Offset: 0x0007D292
		// (set) Token: 0x06002001 RID: 8193 RVA: 0x0007F09A File Offset: 0x0007D29A
		public DynamicColumnsRows DynamicColumns
		{
			get
			{
				return base.DynamicElements;
			}
			set
			{
				if (this.m_staticElements != null)
				{
					throw new ArgumentException("Cannot have StaticColumns and DynamicColumns");
				}
				base.DynamicElements = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0007F0B6 File Offset: 0x0007D2B6
		// (set) Token: 0x06002003 RID: 8195 RVA: 0x0007F0BE File Offset: 0x0007D2BE
		[XmlArrayItem("StaticColumn", typeof(StaticColumnRow))]
		public List<StaticColumnRow> StaticColumns
		{
			get
			{
				return base.StaticElements;
			}
			set
			{
				if (this.m_dynamicElements != null)
				{
					throw new ArgumentException("Cannot have StaticColumns and DynamicColumns");
				}
				base.StaticElements = value;
			}
		}

		// Token: 0x04000DF6 RID: 3574
		private Unit m_height;
	}
}
