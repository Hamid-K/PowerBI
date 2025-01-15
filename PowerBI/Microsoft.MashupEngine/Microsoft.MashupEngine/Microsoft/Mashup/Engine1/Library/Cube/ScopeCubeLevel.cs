using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D43 RID: 3395
	internal class ScopeCubeLevel : ICubeLevel, ICubeObject, IEquatable<ICubeObject>, ICubeObject2
	{
		// Token: 0x06005B3E RID: 23358 RVA: 0x0013ED31 File Offset: 0x0013CF31
		public ScopeCubeLevel(ICubeLevel level, string scope)
		{
			this.level = level;
			this.scope = scope;
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x00002105 File Offset: 0x00000305
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.DimensionAttribute;
			}
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x0013ED47 File Offset: 0x0013CF47
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return ((ICubeObject2)this.level).Identifier.NewScope(this.scope);
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x0013ED64 File Offset: 0x0013CF64
		public string Caption
		{
			get
			{
				return ((ICubeObject2)this.level).Caption;
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x0013ED76 File Offset: 0x0013CF76
		public TypeValue Type
		{
			get
			{
				return ((ICubeObject2)this.level).Type;
			}
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x0013ED88 File Offset: 0x0013CF88
		public ICubeLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x0013ED90 File Offset: 0x0013CF90
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06005B45 RID: 23365 RVA: 0x0013ED98 File Offset: 0x0013CF98
		public ICubeHierarchy Hierarchy
		{
			get
			{
				return new ScopeCubeHierarchy(this.level.Hierarchy, this.scope);
			}
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0013EDB0 File Offset: 0x0013CFB0
		public int Number
		{
			get
			{
				return this.level.Number;
			}
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0013EDBD File Offset: 0x0013CFBD
		public override bool Equals(object other)
		{
			return this.Equals(other as ICubeLevel);
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x0013EDCB File Offset: 0x0013CFCB
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as ScopeCubeLevel);
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x0013EDD9 File Offset: 0x0013CFD9
		public bool Equals(ScopeCubeLevel other)
		{
			return other != null && this.scope == other.scope && this.level.Equals(other.level);
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x0013EE04 File Offset: 0x0013D004
		public override int GetHashCode()
		{
			return this.scope.GetHashCode() * 37 + this.level.GetHashCode();
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x0013EE20 File Offset: 0x0013D020
		public override string ToString()
		{
			return this.Identifier.ToString();
		}

		// Token: 0x040032E4 RID: 13028
		private readonly ICubeLevel level;

		// Token: 0x040032E5 RID: 13029
		private readonly string scope;
	}
}
