using System;
using System.Globalization;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000108 RID: 264
	internal sealed class SqlFedAuthInfo
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x0005E449 File Offset: 0x0005C649
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "STSURL: {0}, SPN: {1}", this.stsurl ?? string.Empty, this.spn ?? string.Empty);
		}

		// Token: 0x0400087A RID: 2170
		internal string spn;

		// Token: 0x0400087B RID: 2171
		internal string stsurl;
	}
}
