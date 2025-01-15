using System;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200096C RID: 2412
	internal abstract class MdxCubeContextProvider : CubeContextProvider
	{
		// Token: 0x060044C9 RID: 17609 RVA: 0x000E74A3 File Offset: 0x000E56A3
		public MdxCubeContextProvider(MdxCubeMetadataProviderCube cube)
		{
			this.Cube = cube;
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x000E74B2 File Offset: 0x000E56B2
		public TableValue Measures
		{
			get
			{
				if (this.measuresTable == null)
				{
					this.measuresTable = MdxCubeMetadata.NewMeasuresTable(this.Cube);
				}
				return this.measuresTable;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x000E74D3 File Offset: 0x000E56D3
		public TableValue Dimensions
		{
			get
			{
				if (this.dimensionsTable == null)
				{
					this.dimensionsTable = MdxCubeMetadata.NewDimensionsTable(this, this.Cube, null);
				}
				return this.dimensionsTable;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x000E74F6 File Offset: 0x000E56F6
		public TableValue DisplayFolders
		{
			get
			{
				if (this.displayFolders == null)
				{
					this.displayFolders = this.GetDisplayFolders();
				}
				return this.displayFolders;
			}
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x000E7514 File Offset: 0x000E5714
		public override CubeObjectKind GetObjectKind(IdentifierCubeExpression identifier)
		{
			switch (this.Cube.GetObject(identifier.Identifier).Kind)
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

		// Token: 0x060044CE RID: 17614 RVA: 0x000E7561 File Offset: 0x000E5761
		public override string GetDisplayName(IdentifierCubeExpression identifier)
		{
			return this.Cube.GetObject(identifier.Identifier).Caption;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000E757C File Offset: 0x000E577C
		public override TypeValue GetType(IdentifierCubeExpression identifier)
		{
			MdxCubeObject @object = this.Cube.GetObject(identifier.Identifier);
			switch (@object.Kind)
			{
			case MdxCubeObjectKind.Measure:
				return ((MdxMeasure)@object).Type.GetTypeValue().Nullable;
			case MdxCubeObjectKind.Level:
				return MdxCubeContextProvider.AddHierarchyMetadata(CubeAttributeMemberIdMetadata.AddColumnMetadata(TypeValue.Text.Nullable), (MdxLevel)@object);
			case MdxCubeObjectKind.Property:
			{
				MdxProperty mdxProperty = (MdxProperty)@object;
				TypeValue nullable = mdxProperty.Type.GetTypeValue().Nullable;
				if (mdxProperty.Key == null)
				{
					return nullable;
				}
				return nullable.AddColumnMetadata();
			}
			case MdxCubeObjectKind.CellProperty:
				return ((MdxCellProperty)@object).Type.GetTypeValue().Nullable;
			}
			throw new InvalidOperationException("Unexpected MdxCubeObjectKind: " + @object.Kind.ToString());
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000E7654 File Offset: 0x000E5854
		public override IdentifierCubeExpression GetProperty(IdentifierCubeExpression dimensionAttribute, CubePropertyKind kind, string userDefinedIdentifier = null)
		{
			MdxLevel mdxLevel = (MdxLevel)this.Cube.GetObject(dimensionAttribute.Identifier);
			MdxProperty mdxProperty;
			switch (kind)
			{
			case CubePropertyKind.UniqueId:
				mdxProperty = mdxLevel.Properties.Single((MdxProperty p) => p.PropertyKind == MdxPropertyKind.MemberUniqueName);
				break;
			case CubePropertyKind.Caption:
				mdxProperty = mdxLevel.Properties.Single((MdxProperty p) => p.PropertyKind == MdxPropertyKind.MemberCaption);
				break;
			case CubePropertyKind.UserDefined:
				mdxProperty = this.GetPropertyByUserDefinedIdentifier(mdxLevel, userDefinedIdentifier);
				break;
			default:
				throw new InvalidOperationException("Unexpected MdxPropertyKind: " + kind.ToString());
			}
			return new IdentifierCubeExpression(mdxProperty.MdxIdentifier);
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000E771C File Offset: 0x000E591C
		protected virtual MdxProperty GetPropertyByUserDefinedIdentifier(MdxLevel level, string userDefinedIdentifier)
		{
			return level.Properties.Single((MdxProperty p) => p.MdxIdentifier.EndsWith("." + userDefinedIdentifier, StringComparison.Ordinal));
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000E774D File Offset: 0x000E594D
		public override IdentifierCubeExpression GetPropertyDimensionAttribute(IdentifierCubeExpression property)
		{
			return new IdentifierCubeExpression(((MdxProperty)this.Cube.GetObject(property.Identifier)).Level.MdxIdentifier);
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x000E7774 File Offset: 0x000E5974
		public override CubePropertyKind GetPropertyKind(IdentifierCubeExpression property)
		{
			MdxProperty mdxProperty = (MdxProperty)this.Cube.GetObject(property.Identifier);
			switch (mdxProperty.PropertyKind)
			{
			case MdxPropertyKind.MemberUniqueName:
				return CubePropertyKind.UniqueId;
			case MdxPropertyKind.MemberCaption:
				return CubePropertyKind.Caption;
			case MdxPropertyKind.UserDefined:
				return CubePropertyKind.UserDefined;
			default:
				throw new InvalidOperationException("Unexpected MdxPropertyKind: " + mdxProperty.PropertyKind.ToString());
			}
		}

		// Token: 0x060044D4 RID: 17620
		protected abstract TableValue GetDisplayFolders();

		// Token: 0x060044D5 RID: 17621 RVA: 0x000E77DC File Offset: 0x000E59DC
		protected static TypeValue AddHierarchyMetadata(TypeValue type, MdxLevel level)
		{
			return CubeHierarchiesMetadata.AddHierarchy(type, level.Hierarchy.MdxIdentifier, level.Hierarchy.Caption, level.Number, level.Caption);
		}

		// Token: 0x040024A7 RID: 9383
		public readonly MdxCubeMetadataProviderCube Cube;

		// Token: 0x040024A8 RID: 9384
		private TableValue displayFolders;

		// Token: 0x040024A9 RID: 9385
		private TableValue dimensionsTable;

		// Token: 0x040024AA RID: 9386
		private TableValue measuresTable;
	}
}
