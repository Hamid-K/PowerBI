using System;
using System.Globalization;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000204 RID: 516
	internal sealed class EpmAttributeNameBuilder
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00037083 File Offset: 0x00035283
		internal EpmAttributeNameBuilder()
		{
			this.suffix = string.Empty;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00037096 File Offset: 0x00035296
		internal string EpmKeepInContent
		{
			get
			{
				return "FC_KeepInContent" + this.suffix;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000370A8 File Offset: 0x000352A8
		internal string EpmSourcePath
		{
			get
			{
				return "FC_SourcePath" + this.suffix;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000370BA File Offset: 0x000352BA
		internal string EpmTargetPath
		{
			get
			{
				return "FC_TargetPath" + this.suffix;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x000370CC File Offset: 0x000352CC
		internal string EpmContentKind
		{
			get
			{
				return "FC_ContentKind" + this.suffix;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000370DE File Offset: 0x000352DE
		internal string EpmNsPrefix
		{
			get
			{
				return "FC_NsPrefix" + this.suffix;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x000370F0 File Offset: 0x000352F0
		internal string EpmNsUri
		{
			get
			{
				return "FC_NsUri" + this.suffix;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00037102 File Offset: 0x00035302
		internal void MoveNext()
		{
			this.index++;
			this.suffix = "_" + this.index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040005B8 RID: 1464
		private const string Separator = "_";

		// Token: 0x040005B9 RID: 1465
		private int index;

		// Token: 0x040005BA RID: 1466
		private string suffix;
	}
}
