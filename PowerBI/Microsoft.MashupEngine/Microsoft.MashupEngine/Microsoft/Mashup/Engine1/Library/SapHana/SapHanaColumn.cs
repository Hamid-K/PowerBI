using System;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000420 RID: 1056
	internal sealed class SapHanaColumn
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x00065569 File Offset: 0x00063769
		public SapHanaColumn(OdbcColumnInfo columnInfo)
		{
			this.columnInfo = columnInfo;
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00065578 File Offset: 0x00063778
		public string Name
		{
			get
			{
				return this.columnInfo.Name;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x00065585 File Offset: 0x00063785
		public int Ordinal
		{
			get
			{
				return this.columnInfo.Ordinal;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00065592 File Offset: 0x00063792
		public TypeValue Type
		{
			get
			{
				return this.columnInfo.TypeValue;
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x0006559F File Offset: 0x0006379F
		public bool Equals(SapHanaColumn other)
		{
			return other != null && this.Name == other.Name;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000655B7 File Offset: 0x000637B7
		public override bool Equals(object other)
		{
			return this.Equals(other as SapHanaColumn);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000655C5 File Offset: 0x000637C5
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x04000E80 RID: 3712
		private readonly OdbcColumnInfo columnInfo;
	}
}
