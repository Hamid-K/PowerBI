using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D49 RID: 7497
	internal class Resource : IResource
	{
		// Token: 0x0600BA8D RID: 47757 RVA: 0x0025C58C File Offset: 0x0025A78C
		public Resource(string kind, string path, string nonNormalizedPath)
		{
			this.kind = kind;
			this.path = path;
			this.nonNormalizedPath = nonNormalizedPath;
		}

		// Token: 0x17002E16 RID: 11798
		// (get) Token: 0x0600BA8E RID: 47758 RVA: 0x0025C5A9 File Offset: 0x0025A7A9
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17002E17 RID: 11799
		// (get) Token: 0x0600BA8F RID: 47759 RVA: 0x0025C5B1 File Offset: 0x0025A7B1
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17002E18 RID: 11800
		// (get) Token: 0x0600BA90 RID: 47760 RVA: 0x0025C5B9 File Offset: 0x0025A7B9
		public string NonNormalizedPath
		{
			get
			{
				return this.nonNormalizedPath;
			}
		}

		// Token: 0x0600BA91 RID: 47761 RVA: 0x0025C5C1 File Offset: 0x0025A7C1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IResource);
		}

		// Token: 0x0600BA92 RID: 47762 RVA: 0x0025C5CF File Offset: 0x0025A7CF
		public override int GetHashCode()
		{
			if (this.path == null)
			{
				return this.kind.GetHashCode();
			}
			return this.kind.GetHashCode() ^ this.path.GetHashCode();
		}

		// Token: 0x0600BA93 RID: 47763 RVA: 0x0025C5FC File Offset: 0x0025A7FC
		public bool Equals(IResource other)
		{
			return other != null && this.kind == other.Kind && this.path == other.Path;
		}

		// Token: 0x0600BA94 RID: 47764 RVA: 0x0025C627 File Offset: 0x0025A827
		public override string ToString()
		{
			return this.kind + "/" + this.path;
		}

		// Token: 0x04005EF2 RID: 24306
		private string kind;

		// Token: 0x04005EF3 RID: 24307
		private string path;

		// Token: 0x04005EF4 RID: 24308
		private string nonNormalizedPath;
	}
}
