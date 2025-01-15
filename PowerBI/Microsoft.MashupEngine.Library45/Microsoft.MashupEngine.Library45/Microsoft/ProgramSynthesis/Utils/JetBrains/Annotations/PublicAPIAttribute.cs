using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000558 RID: 1368
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x06001EC8 RID: 7880 RVA: 0x0000957E File Offset: 0x0000777E
		public PublicAPIAttribute()
		{
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00059966 File Offset: 0x00057B66
		public PublicAPIAttribute(string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x00059975 File Offset: 0x00057B75
		public string Comment { get; }
	}
}
