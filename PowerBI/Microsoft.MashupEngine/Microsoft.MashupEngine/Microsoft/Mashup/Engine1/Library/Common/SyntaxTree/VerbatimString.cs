using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree
{
	// Token: 0x020011B3 RID: 4531
	public class VerbatimString : IEquatable<VerbatimString>
	{
		// Token: 0x060077CB RID: 30667 RVA: 0x001A0314 File Offset: 0x0019E514
		public VerbatimString(string verbatimString)
		{
			this.verbatimString = verbatimString;
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x060077CC RID: 30668 RVA: 0x001A0323 File Offset: 0x0019E523
		public string String
		{
			get
			{
				return this.verbatimString;
			}
		}

		// Token: 0x060077CD RID: 30669 RVA: 0x001A032B File Offset: 0x0019E52B
		public override int GetHashCode()
		{
			return this.verbatimString.GetHashCode();
		}

		// Token: 0x060077CE RID: 30670 RVA: 0x001A0338 File Offset: 0x0019E538
		public bool Equals(VerbatimString other)
		{
			return other != null && this.String == other.String;
		}

		// Token: 0x060077CF RID: 30671 RVA: 0x001A0350 File Offset: 0x0019E550
		public override bool Equals(object otherObject)
		{
			return this.Equals(otherObject as VerbatimString);
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x001A035E File Offset: 0x0019E55E
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x04004119 RID: 16665
		private readonly string verbatimString;
	}
}
