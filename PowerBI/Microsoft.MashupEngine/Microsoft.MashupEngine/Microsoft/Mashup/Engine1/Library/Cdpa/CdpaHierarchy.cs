using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D9A RID: 3482
	internal class CdpaHierarchy : ICubeHierarchy, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06005ECA RID: 24266 RVA: 0x00147A06 File Offset: 0x00145C06
		public CdpaHierarchy(CdpaDimension dimension, string name, string caption)
		{
			this.dimension = dimension;
			this.name = name;
			this.caption = caption;
			this.qualifiedName = this.Dimension.QualifiedName.Qualify(name);
			this.levels = new List<CdpaHierarchyLevel>();
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x0000244F File Offset: 0x0000064F
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Other;
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x06005ECC RID: 24268 RVA: 0x00147A45 File Offset: 0x00145C45
		public CdpaDimension Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x00147A4D File Offset: 0x00145C4D
		public QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06005ECE RID: 24270 RVA: 0x00147A55 File Offset: 0x00145C55
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x00147A5D File Offset: 0x00145C5D
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x00147A65 File Offset: 0x00145C65
		public IList<CdpaHierarchyLevel> Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x00147A6D File Offset: 0x00145C6D
		public ICubeLevel GetLevel(int number)
		{
			return this.levels[number];
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x00147A7B File Offset: 0x00145C7B
		public override int GetHashCode()
		{
			return this.qualifiedName.GetHashCode();
		}

		// Token: 0x06005ED3 RID: 24275 RVA: 0x00147A88 File Offset: 0x00145C88
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaHierarchy);
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x00147A88 File Offset: 0x00145C88
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as CdpaHierarchy);
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x00147A96 File Offset: 0x00145C96
		public bool Equals(CdpaHierarchy other)
		{
			return other != null && this.qualifiedName.Equals(other.qualifiedName);
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x00147AAE File Offset: 0x00145CAE
		public override string ToString()
		{
			string text = "hierarchy(";
			QualifiedName qualifiedName = this.qualifiedName;
			return text + ((qualifiedName != null) ? qualifiedName.ToString() : null) + ")";
		}

		// Token: 0x0400340C RID: 13324
		private readonly CdpaDimension dimension;

		// Token: 0x0400340D RID: 13325
		private readonly string name;

		// Token: 0x0400340E RID: 13326
		private readonly string caption;

		// Token: 0x0400340F RID: 13327
		private readonly QualifiedName qualifiedName;

		// Token: 0x04003410 RID: 13328
		private readonly List<CdpaHierarchyLevel> levels;
	}
}
