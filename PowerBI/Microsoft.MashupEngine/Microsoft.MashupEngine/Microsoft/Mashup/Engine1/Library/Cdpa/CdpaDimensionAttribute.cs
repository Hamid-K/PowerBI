using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D9B RID: 3483
	internal abstract class CdpaDimensionAttribute : ICubeLevel, ICubeObject, IEquatable<ICubeObject>, ICubeObject2
	{
		// Token: 0x06005ED7 RID: 24279 RVA: 0x00147AD1 File Offset: 0x00145CD1
		public CdpaDimensionAttribute(CdpaDimension dimension)
		{
			this.dimension = dimension;
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x00002105 File Offset: 0x00000305
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.DimensionAttribute;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x06005ED9 RID: 24281 RVA: 0x00147AE0 File Offset: 0x00145CE0
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return this.QualifiedName.ToExpression();
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x06005EDA RID: 24282
		public abstract ICubeHierarchy Hierarchy { get; }

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x06005EDB RID: 24283
		public abstract int Number { get; }

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x00147AED File Offset: 0x00145CED
		public CdpaDimension Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06005EDD RID: 24285
		public abstract QualifiedName QualifiedName { get; }

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x06005EDE RID: 24286
		public abstract string PropertyName { get; }

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x06005EDF RID: 24287
		public abstract string Caption { get; }

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x06005EE0 RID: 24288
		public abstract TypeValue Type { get; }

		// Token: 0x06005EE1 RID: 24289 RVA: 0x00147AF5 File Offset: 0x00145CF5
		public override int GetHashCode()
		{
			return this.QualifiedName.GetHashCode();
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x00147B02 File Offset: 0x00145D02
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaDimensionAttribute);
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x00147B02 File Offset: 0x00145D02
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as CdpaDimensionAttribute);
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x00147B10 File Offset: 0x00145D10
		public bool Equals(CdpaDimensionAttribute other)
		{
			return other != null && this.QualifiedName.Equals(other.QualifiedName);
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x00147B28 File Offset: 0x00145D28
		public override string ToString()
		{
			string text = "attribute(";
			QualifiedName qualifiedName = this.QualifiedName;
			return text + ((qualifiedName != null) ? qualifiedName.ToString() : null) + ")";
		}

		// Token: 0x04003411 RID: 13329
		private readonly CdpaDimension dimension;
	}
}
