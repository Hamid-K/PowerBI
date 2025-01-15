using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C7 RID: 4551
	internal struct ConstantSqlString : IEquatable<ConstantSqlString>
	{
		// Token: 0x06007842 RID: 30786 RVA: 0x001A1048 File Offset: 0x0019F248
		public ConstantSqlString(string rawString)
		{
			this.rawString = rawString;
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06007843 RID: 30787 RVA: 0x001A1051 File Offset: 0x0019F251
		public string String
		{
			get
			{
				return this.rawString;
			}
		}

		// Token: 0x06007844 RID: 30788 RVA: 0x001A1059 File Offset: 0x0019F259
		public override int GetHashCode()
		{
			return this.rawString.GetHashCode();
		}

		// Token: 0x06007845 RID: 30789 RVA: 0x001A1066 File Offset: 0x0019F266
		public bool Equals(ConstantSqlString string2)
		{
			return this.String.Equals(string2.String);
		}

		// Token: 0x06007846 RID: 30790 RVA: 0x001A107A File Offset: 0x0019F27A
		public override bool Equals(object obj)
		{
			return obj != null && this.String.Equals(obj.ToString());
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x001A1092 File Offset: 0x0019F292
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x0400416A RID: 16746
		private readonly string rawString;
	}
}
