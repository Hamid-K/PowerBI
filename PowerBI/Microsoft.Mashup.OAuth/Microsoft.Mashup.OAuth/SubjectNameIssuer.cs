using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000027 RID: 39
	public struct SubjectNameIssuer
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00005F70 File Offset: 0x00004170
		public SubjectNameIssuer(string subjectName, string issuer)
		{
			this.SubjectName = subjectName;
			this.Issuer = issuer;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005F80 File Offset: 0x00004180
		public string SubjectName { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005F88 File Offset: 0x00004188
		public string Issuer { get; }
	}
}
