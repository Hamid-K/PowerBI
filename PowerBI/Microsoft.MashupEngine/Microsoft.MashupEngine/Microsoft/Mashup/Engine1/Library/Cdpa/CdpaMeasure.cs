using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D91 RID: 3473
	internal abstract class CdpaMeasure : ICubeMeasure, ICubeObject, IEquatable<ICubeObject>, ICubeObject2
	{
		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x06005E87 RID: 24199 RVA: 0x000023C4 File Offset: 0x000005C4
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Measure;
			}
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x06005E88 RID: 24200 RVA: 0x00147228 File Offset: 0x00145428
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return this.QualifiedName.ToExpression();
			}
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x06005E89 RID: 24201
		public abstract CdpaCube Cube { get; }

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x06005E8A RID: 24202
		public abstract QualifiedName QualifiedName { get; }

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x06005E8B RID: 24203
		public abstract string Caption { get; }

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x06005E8C RID: 24204
		public abstract TypeValue Type { get; }

		// Token: 0x06005E8D RID: 24205 RVA: 0x00147235 File Offset: 0x00145435
		public override int GetHashCode()
		{
			return this.QualifiedName.GetHashCode();
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x00147242 File Offset: 0x00145442
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaMeasure);
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x00147242 File Offset: 0x00145442
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as CdpaMeasure);
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x00147250 File Offset: 0x00145450
		public bool Equals(CdpaMeasure other)
		{
			return other != null && this.QualifiedName.Equals(this.QualifiedName);
		}
	}
}
