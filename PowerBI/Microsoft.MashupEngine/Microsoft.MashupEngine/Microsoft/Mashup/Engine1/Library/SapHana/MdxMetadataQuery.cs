using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000418 RID: 1048
	internal sealed class MdxMetadataQuery : MetadataQueryBase
	{
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x00064F7D File Offset: 0x0006317D
		// (set) Token: 0x060023CA RID: 9162 RVA: 0x00064F85 File Offset: 0x00063185
		public bool OnlyUseMdxPrefixForV1SPS11AndBelow { get; set; }

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00064F8E File Offset: 0x0006318E
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x00064F96 File Offset: 0x00063196
		public string TableName { get; set; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x00064F9F File Offset: 0x0006319F
		// (set) Token: 0x060023CE RID: 9166 RVA: 0x00064FA7 File Offset: 0x000631A7
		public string WhereClause { get; set; }

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x00064FB0 File Offset: 0x000631B0
		// (set) Token: 0x060023D0 RID: 9168 RVA: 0x00064FB8 File Offset: 0x000631B8
		public string OrderByClause { get; set; }

		// Token: 0x060023D1 RID: 9169 RVA: 0x00064FC4 File Offset: 0x000631C4
		public override string GetQuery(SapHanaOdbcDataSource dataSource)
		{
			string text = this.GetFormat(dataSource);
			if (!string.IsNullOrEmpty(this.OrderByClause))
			{
				text = text + Environment.NewLine + "order by " + this.OrderByClause;
			}
			return string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				"{columns}",
				this.TableName,
				Environment.NewLine,
				this.WhereClause
			});
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00065030 File Offset: 0x00063230
		private string GetFormat(SapHanaOdbcDataSource dataSource)
		{
			Version version;
			if (dataSource.TryGetSapHanaVersion(out version))
			{
				if (version.Major >= 4)
				{
					return "select {0} from _SYS_BI.{1}{2}where {3}";
				}
				if (this.OnlyUseMdxPrefixForV1SPS11AndBelow)
				{
					if (version.Major == 1 && version.Minor == 0 && version.Build < 120)
					{
						return "MDX select {0} from {1}{2}where {3}";
					}
					return "select {0} from _SYS_BI.{1}{2}where {3}";
				}
			}
			return "MDX select {0} from {1}{2}where {3}";
		}

		// Token: 0x04000E5D RID: 3677
		private const string formatWithMdxPrefix = "MDX select {0} from {1}{2}where {3}";

		// Token: 0x04000E5E RID: 3678
		private const string formatWithoutMdxPrefix = "select {0} from _SYS_BI.{1}{2}where {3}";
	}
}
