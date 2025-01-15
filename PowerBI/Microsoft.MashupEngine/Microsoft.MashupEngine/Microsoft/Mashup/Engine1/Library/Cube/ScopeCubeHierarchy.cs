using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D44 RID: 3396
	internal class ScopeCubeHierarchy : ICubeHierarchy, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06005B4C RID: 23372 RVA: 0x0013EE2D File Offset: 0x0013D02D
		public ScopeCubeHierarchy(ICubeHierarchy hierarchy, string scope)
		{
			this.hierarchy = hierarchy;
			this.scope = scope;
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0000244F File Offset: 0x0000064F
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Other;
			}
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x0013EE43 File Offset: 0x0013D043
		public ICubeHierarchy Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0013EE4B File Offset: 0x0013D04B
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0013EE53 File Offset: 0x0013D053
		public ICubeLevel GetLevel(int number)
		{
			return new ScopeCubeLevel(this.hierarchy.GetLevel(number), this.scope);
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x0013EE6C File Offset: 0x0013D06C
		public override bool Equals(object other)
		{
			return this.Equals(other as ScopeCubeHierarchy);
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x0013EE6C File Offset: 0x0013D06C
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as ScopeCubeHierarchy);
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0013EE7A File Offset: 0x0013D07A
		public bool Equals(ScopeCubeHierarchy other)
		{
			return other != null && this.scope == other.scope && this.hierarchy.Equals(other.hierarchy);
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x0013EEA5 File Offset: 0x0013D0A5
		public override int GetHashCode()
		{
			return this.scope.GetHashCode() * 37 + this.hierarchy.GetHashCode();
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x0013EEC1 File Offset: 0x0013D0C1
		public override string ToString()
		{
			return this.scope + "/" + this.hierarchy.ToString();
		}

		// Token: 0x040032E6 RID: 13030
		private readonly ICubeHierarchy hierarchy;

		// Token: 0x040032E7 RID: 13031
		private readonly string scope;
	}
}
