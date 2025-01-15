using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200098D RID: 2445
	internal abstract class MdxCubeObject : ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x0600462A RID: 17962 RVA: 0x000EBB20 File Offset: 0x000E9D20
		public MdxCubeObject(string mdxIdentifier, string caption)
		{
			this.mdxIdentifier = mdxIdentifier;
			this.caption = caption;
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x0600462B RID: 17963
		public abstract MdxCubeObjectKind Kind { get; }

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x000EBB38 File Offset: 0x000E9D38
		CubeObjectKind ICubeObject.Kind
		{
			get
			{
				switch (this.Kind)
				{
				case MdxCubeObjectKind.Measure:
					return CubeObjectKind.Measure;
				case MdxCubeObjectKind.Level:
					return CubeObjectKind.DimensionAttribute;
				case MdxCubeObjectKind.Property:
					return CubeObjectKind.Property;
				case MdxCubeObjectKind.CellProperty:
					return CubeObjectKind.MeasureProperty;
				}
				return CubeObjectKind.Other;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x000EBB75 File Offset: 0x000E9D75
		public string MdxIdentifier
		{
			get
			{
				return this.mdxIdentifier;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x000EBB7D File Offset: 0x000E9D7D
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000EBB85 File Offset: 0x000E9D85
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MdxCubeObject);
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000EBB93 File Offset: 0x000E9D93
		public override int GetHashCode()
		{
			return this.mdxIdentifier.GetHashCode();
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x000EBBA0 File Offset: 0x000E9DA0
		public bool Equals(MdxCubeObject obj)
		{
			return obj != null && obj.mdxIdentifier == this.mdxIdentifier;
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x000EBBB8 File Offset: 0x000E9DB8
		public bool Equals(ICubeObject obj)
		{
			return this.Equals((MdxCubeObject)obj);
		}

		// Token: 0x04002517 RID: 9495
		private readonly string mdxIdentifier;

		// Token: 0x04002518 RID: 9496
		private readonly string caption;
	}
}
