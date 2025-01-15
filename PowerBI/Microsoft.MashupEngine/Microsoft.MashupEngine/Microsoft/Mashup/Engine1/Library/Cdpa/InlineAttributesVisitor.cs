using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E4B RID: 3659
	internal sealed class InlineAttributesVisitor : CubeExpressionVisitor
	{
		// Token: 0x0600625C RID: 25180 RVA: 0x00151D18 File Offset: 0x0014FF18
		public InlineAttributesVisitor(ICube cube)
		{
			this.cube = cube;
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x00072ED2 File Offset: 0x000710D2
		public CubeExpression InlineAttributes(CubeExpression expression)
		{
			return this.Visit(expression);
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x00151D34 File Offset: 0x0014FF34
		protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
		{
			ICubeObject cubeObject;
			if (this.cube.TryGetObject(identifier, out cubeObject))
			{
				ScopePath scopePath = this.scopePath;
				try
				{
					ICubeObject unscoped = cubeObject.GetUnscoped(out this.scopePath);
					CdpaHierarchyLevel cdpaHierarchyLevel = unscoped as CdpaHierarchyLevel;
					if (cdpaHierarchyLevel != null)
					{
						return this.Visit(cdpaHierarchyLevel.Attribute.QualifiedName.ToExpression());
					}
					CdpaProjectedDimensionAttribute cdpaProjectedDimensionAttribute = unscoped as CdpaProjectedDimensionAttribute;
					if (cdpaProjectedDimensionAttribute != null)
					{
						return this.Visit(cdpaProjectedDimensionAttribute.Projection);
					}
				}
				finally
				{
					this.scopePath = scopePath;
				}
			}
			if (!this.scopePath.Equals(ScopePath.Default))
			{
				identifier = identifier.SetScopePath(this.scopePath);
			}
			return base.VisitIdentifier(identifier);
		}

		// Token: 0x040035A3 RID: 13731
		private readonly ICube cube;

		// Token: 0x040035A4 RID: 13732
		private ScopePath scopePath = ScopePath.Default;
	}
}
