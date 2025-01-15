using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000676 RID: 1654
	internal sealed class BuiltInExpr : Node
	{
		// Token: 0x06004F01 RID: 20225 RVA: 0x0011F6AD File Offset: 0x0011D8AD
		private BuiltInExpr(BuiltInKind kind, string name)
		{
			this.Kind = kind;
			this.Name = name.ToUpperInvariant();
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0011F6C8 File Offset: 0x0011D8C8
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1)
			: this(kind, name)
		{
			this.ArgCount = 1;
			this.Arg1 = arg1;
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x0011F6E0 File Offset: 0x0011D8E0
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2)
			: this(kind, name)
		{
			this.ArgCount = 2;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x0011F700 File Offset: 0x0011D900
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2, Node arg3)
			: this(kind, name)
		{
			this.ArgCount = 3;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0011F728 File Offset: 0x0011D928
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2, Node arg3, Node arg4)
			: this(kind, name)
		{
			this.ArgCount = 4;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
			this.Arg4 = arg4;
		}

		// Token: 0x04001C91 RID: 7313
		internal readonly BuiltInKind Kind;

		// Token: 0x04001C92 RID: 7314
		internal readonly string Name;

		// Token: 0x04001C93 RID: 7315
		internal readonly int ArgCount;

		// Token: 0x04001C94 RID: 7316
		internal readonly Node Arg1;

		// Token: 0x04001C95 RID: 7317
		internal readonly Node Arg2;

		// Token: 0x04001C96 RID: 7318
		internal readonly Node Arg3;

		// Token: 0x04001C97 RID: 7319
		internal readonly Node Arg4;
	}
}
