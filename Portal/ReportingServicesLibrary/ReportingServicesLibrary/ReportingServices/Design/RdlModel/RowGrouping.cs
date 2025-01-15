using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003EF RID: 1007
	public sealed class RowGrouping : MatrixGrouping
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x0007F0E2 File Offset: 0x0007D2E2
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x0007F0EA File Offset: 0x0007D2EA
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0007F092 File Offset: 0x0007D292
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x0007F0F3 File Offset: 0x0007D2F3
		public DynamicColumnsRows DynamicRows
		{
			get
			{
				return base.DynamicElements;
			}
			set
			{
				if (this.m_staticElements != null)
				{
					throw new ArgumentException("Cannot have StaticRows and DynamicRows");
				}
				base.DynamicElements = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0007F0B6 File Offset: 0x0007D2B6
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x0007F10F File Offset: 0x0007D30F
		[XmlArrayItem("StaticRow", typeof(StaticColumnRow))]
		public List<StaticColumnRow> StaticRows
		{
			get
			{
				return base.StaticElements;
			}
			set
			{
				if (this.m_dynamicElements != null)
				{
					throw new ArgumentException("Cannot have StaticRows and DynamicRows");
				}
				base.StaticElements = value;
			}
		}

		// Token: 0x04000DF7 RID: 3575
		private Unit m_width;
	}
}
