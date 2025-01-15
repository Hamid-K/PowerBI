using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D99 RID: 3481
	internal class CdpaSignalDimensionAttribute : CdpaDimensionAttribute, ICubeHierarchy, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06005EC1 RID: 24257 RVA: 0x00147980 File Offset: 0x00145B80
		public CdpaSignalDimensionAttribute(CdpaSignalDimension dimension, RecordValue property)
			: base(dimension)
		{
			this.propertyName = property["name"].AsString;
			this.qualifiedName = this.Dimension.QualifiedName.Qualify(this.propertyName);
			this.type = CdpaCube.GetMType(property["type"].AsString);
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x06005EC2 RID: 24258 RVA: 0x001479E1 File Offset: 0x00145BE1
		public new CdpaSignalDimension Dimension
		{
			get
			{
				return (CdpaSignalDimension)base.Dimension;
			}
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override ICubeHierarchy Hierarchy
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x06005EC4 RID: 24260 RVA: 0x00002105 File Offset: 0x00000305
		public override int Number
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ICubeLevel GetLevel(int number)
		{
			return this;
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x06005EC6 RID: 24262 RVA: 0x001479EE File Offset: 0x00145BEE
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x001479F6 File Offset: 0x00145BF6
		public override string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06005EC8 RID: 24264 RVA: 0x001479F6 File Offset: 0x00145BF6
		public override string Caption
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x001479FE File Offset: 0x00145BFE
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04003409 RID: 13321
		private readonly string propertyName;

		// Token: 0x0400340A RID: 13322
		private readonly QualifiedName qualifiedName;

		// Token: 0x0400340B RID: 13323
		private readonly TypeValue type;
	}
}
