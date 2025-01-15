using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000138 RID: 312
	[DebuggerDisplay("TmdlDocument - path=\"{Path}\"")]
	internal sealed class TmdlDocument
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x0008C889 File Offset: 0x0008AA89
		public TmdlDocument(TmdlProject project, string path)
		{
			this.Project = project;
			this.Path = path;
			this.objects = new List<TmdlObject>();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0008C8AA File Offset: 0x0008AAAA
		internal TmdlDocument(string path)
		{
			this.Path = path;
			this.objects = new List<TmdlObject>();
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0008C8C4 File Offset: 0x0008AAC4
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x0008C8CC File Offset: 0x0008AACC
		public TmdlProject Project { get; internal set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0008C8D5 File Offset: 0x0008AAD5
		public string Path { get; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0008C8DD File Offset: 0x0008AADD
		public ICollection<TmdlObject> Objects
		{
			get
			{
				return this.objects;
			}
		}

		// Token: 0x04000365 RID: 869
		private List<TmdlObject> objects;
	}
}
