using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200051D RID: 1309
	public struct UnitType : IEquatable<UnitType>
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00056C08 File Offset: 0x00054E08
		public static UnitType Instance
		{
			get
			{
				return default(UnitType);
			}
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00056C1E File Offset: 0x00054E1E
		public override int GetHashCode()
		{
			return 642217;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00056C25 File Offset: 0x00054E25
		public override bool Equals(object other)
		{
			return other is UnitType;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(UnitType other)
		{
			return true;
		}
	}
}
