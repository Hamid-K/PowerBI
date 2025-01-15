using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006CD RID: 1741
	public abstract class DbModificationClause
	{
		// Token: 0x06005121 RID: 20769 RVA: 0x001232F2 File Offset: 0x001214F2
		internal DbModificationClause()
		{
		}

		// Token: 0x06005122 RID: 20770
		internal abstract void DumpStructure(ExpressionDumper dumper);

		// Token: 0x06005123 RID: 20771
		internal abstract TreeNode Print(DbExpressionVisitor<TreeNode> visitor);
	}
}
