using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000BB RID: 187
	public class HistoryOperation : MigrationOperation
	{
		// Token: 0x06000F73 RID: 3955 RVA: 0x000208EA File Offset: 0x0001EAEA
		public HistoryOperation(IList<DbModificationCommandTree> commandTrees, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotNull<IList<DbModificationCommandTree>>(commandTrees, "commandTrees");
			if (!commandTrees.Any<DbModificationCommandTree>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("commandTrees", "HistoryOperation"));
			}
			this._commandTrees = commandTrees;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00020923 File Offset: 0x0001EB23
		public IList<DbModificationCommandTree> CommandTrees
		{
			get
			{
				return this._commandTrees;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0002092B File Offset: 0x0001EB2B
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400086A RID: 2154
		private readonly IList<DbModificationCommandTree> _commandTrees;
	}
}
