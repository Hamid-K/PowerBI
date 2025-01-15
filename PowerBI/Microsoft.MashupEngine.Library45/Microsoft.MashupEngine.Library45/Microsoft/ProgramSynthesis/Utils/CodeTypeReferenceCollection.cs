using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200040A RID: 1034
	public class CodeTypeReferenceCollection
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00047A68 File Offset: 0x00045C68
		public int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x1700045C RID: 1116
		public CodeTypeReference this[int index]
		{
			get
			{
				return this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00047A92 File Offset: 0x00045C92
		public int Add(CodeTypeReference value)
		{
			int count = this.List.Count;
			this.List.Add(value);
			return count;
		}

		// Token: 0x04000B44 RID: 2884
		private List<CodeTypeReference> List = new List<CodeTypeReference>();
	}
}
