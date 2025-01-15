using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000AAE RID: 2734
	public static class SyntacticTypeOptionSet
	{
		// Token: 0x060044C2 RID: 17602 RVA: 0x000D7727 File Offset: 0x000D5927
		public static SyntacticTypeOptionSet<TSyntacticType> From<TSyntacticType>(TSyntacticType singleType) where TSyntacticType : SyntacticType
		{
			return new SyntacticTypeOptionSet<TSyntacticType>(singleType.Yield<TSyntacticType>());
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000D7734 File Offset: 0x000D5934
		public static SyntacticTypeOptionSet<TSyntacticType> From<TSyntacticType>(IEnumerable<TSyntacticType> types) where TSyntacticType : SyntacticType
		{
			return new SyntacticTypeOptionSet<TSyntacticType>(types);
		}
	}
}
