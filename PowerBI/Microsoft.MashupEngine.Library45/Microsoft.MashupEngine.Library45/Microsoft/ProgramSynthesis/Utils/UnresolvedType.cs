using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200051E RID: 1310
	public class UnresolvedType : GrammarType, IEquatable<UnresolvedType>
	{
		// Token: 0x06001D28 RID: 7464 RVA: 0x00056C30 File Offset: 0x00054E30
		public UnresolvedType(string name)
		{
			this.Name = name;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00056C3F File Offset: 0x00054E3F
		public override string CsName()
		{
			return this.Name;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x00002188 File Offset: 0x00000388
		public override Type Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x00056C47 File Offset: 0x00054E47
		public override string Name { get; }

		// Token: 0x06001D2C RID: 7468 RVA: 0x00056C3F File Offset: 0x00054E3F
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00056C4F File Offset: 0x00054E4F
		public bool Equals(UnresolvedType other)
		{
			return this.Name == ((other != null) ? other.Name : null);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00056C68 File Offset: 0x00054E68
		public override bool Equals(object obj)
		{
			UnresolvedType unresolvedType = obj as UnresolvedType;
			return unresolvedType != null && this.Equals(unresolvedType);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x00056C88 File Offset: 0x00054E88
		public override int GetHashCode()
		{
			return 2203 ^ this.Name.GetHashCode();
		}
	}
}
