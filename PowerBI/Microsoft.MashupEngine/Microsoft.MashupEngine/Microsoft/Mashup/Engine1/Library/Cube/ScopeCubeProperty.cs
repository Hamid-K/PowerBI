using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D45 RID: 3397
	internal class ScopeCubeProperty : ICubeProperty, ICubeObject, IEquatable<ICubeObject>, ICubeObject2
	{
		// Token: 0x06005B56 RID: 23382 RVA: 0x0013EEDE File Offset: 0x0013D0DE
		public ScopeCubeProperty(ICubeProperty property, string scope)
		{
			this.property = property;
			this.scope = scope;
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x00002139 File Offset: 0x00000339
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Property;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x0013EEF4 File Offset: 0x0013D0F4
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return ((ICubeObject2)this.property).Identifier.NewScope(this.scope);
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x0013EF11 File Offset: 0x0013D111
		public string Caption
		{
			get
			{
				return ((ICubeObject2)this.property).Caption;
			}
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0013EF23 File Offset: 0x0013D123
		public TypeValue Type
		{
			get
			{
				return ((ICubeObject2)this.property).Type;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x06005B5B RID: 23387 RVA: 0x0013EF35 File Offset: 0x0013D135
		public ICubeProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x0013EF3D File Offset: 0x0013D13D
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x06005B5D RID: 23389 RVA: 0x0013EF45 File Offset: 0x0013D145
		public CubePropertyKind PropertyKind
		{
			get
			{
				return this.property.PropertyKind;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x0013EF52 File Offset: 0x0013D152
		public ICubeLevel Level
		{
			get
			{
				return new ScopeCubeLevel(this.property.Level, this.scope);
			}
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x0013EF6A File Offset: 0x0013D16A
		public override bool Equals(object other)
		{
			return this.Equals(other as ScopeCubeProperty);
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0013EF6A File Offset: 0x0013D16A
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as ScopeCubeProperty);
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0013EF78 File Offset: 0x0013D178
		public bool Equals(ScopeCubeProperty other)
		{
			return other != null && this.scope == other.scope && this.property.Equals(other.property);
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x0013EFA3 File Offset: 0x0013D1A3
		public override int GetHashCode()
		{
			return this.scope.GetHashCode() * 37 + this.property.GetHashCode();
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x0013EFBF File Offset: 0x0013D1BF
		public override string ToString()
		{
			return this.Identifier.ToString();
		}

		// Token: 0x040032E8 RID: 13032
		private readonly ICubeProperty property;

		// Token: 0x040032E9 RID: 13033
		private readonly string scope;
	}
}
