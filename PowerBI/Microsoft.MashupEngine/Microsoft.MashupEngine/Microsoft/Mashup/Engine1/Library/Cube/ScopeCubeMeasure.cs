using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D46 RID: 3398
	internal class ScopeCubeMeasure : ICubeMeasure, ICubeObject, IEquatable<ICubeObject>, ICubeObject2
	{
		// Token: 0x06005B64 RID: 23396 RVA: 0x0013EFCC File Offset: 0x0013D1CC
		public ScopeCubeMeasure(ICubeMeasure measure, string scope)
		{
			this.measure = measure;
			this.scope = scope;
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x000023C4 File Offset: 0x000005C4
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Measure;
			}
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06005B66 RID: 23398 RVA: 0x0013EFE2 File Offset: 0x0013D1E2
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return ((ICubeObject2)this.measure).Identifier.NewScope(this.scope);
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06005B67 RID: 23399 RVA: 0x0013EFFF File Offset: 0x0013D1FF
		public string Caption
		{
			get
			{
				return ((ICubeObject2)this.measure).Caption;
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x06005B68 RID: 23400 RVA: 0x0013F011 File Offset: 0x0013D211
		public TypeValue Type
		{
			get
			{
				return ((ICubeObject2)this.measure).Type;
			}
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x06005B69 RID: 23401 RVA: 0x0013F023 File Offset: 0x0013D223
		public ICubeMeasure Measure
		{
			get
			{
				return this.measure;
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x06005B6A RID: 23402 RVA: 0x0013F02B File Offset: 0x0013D22B
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0013F033 File Offset: 0x0013D233
		public override bool Equals(object other)
		{
			return this.Equals(other as ScopeCubeMeasure);
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x0013F033 File Offset: 0x0013D233
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as ScopeCubeMeasure);
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x0013F041 File Offset: 0x0013D241
		public bool Equals(ScopeCubeMeasure other)
		{
			return other != null && this.scope == other.scope && this.measure.Equals(other.measure);
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x0013F06C File Offset: 0x0013D26C
		public override int GetHashCode()
		{
			return this.scope.GetHashCode() * 37 + this.measure.GetHashCode();
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x0013F088 File Offset: 0x0013D288
		public override string ToString()
		{
			return this.Identifier.ToString();
		}

		// Token: 0x040032EA RID: 13034
		private readonly ICubeMeasure measure;

		// Token: 0x040032EB RID: 13035
		private readonly string scope;
	}
}
