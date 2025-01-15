using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004B3 RID: 1203
	internal sealed class SapBwMdxCube : MdxCubeMetadataProviderCube
	{
		// Token: 0x06002795 RID: 10133 RVA: 0x000744A4 File Offset: 0x000726A4
		public SapBwMdxCube(ISapBwService service, string catalogName, string cubeName)
			: base((MdxCube cube) => new SapBwMdxCubeMetadataProvider(service, catalogName, (SapBwMdxCube)cube), cubeName)
		{
			this.catalogName = catalogName;
			this.variables = new SapBwVariablesCollection(service, catalogName, this);
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x000744FC File Offset: 0x000726FC
		public string CatalogName
		{
			get
			{
				return this.catalogName;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x00074504 File Offset: 0x00072704
		public SapBwVariablesCollection Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsProperties
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x0007450C File Offset: 0x0007270C
		public override bool TryGetLevelFromIdentifier(string identifier, out string levelUniqueName)
		{
			return SapBwIdentifier.TryExtractDimensionAndLevel(identifier, out levelUniqueName);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x0007451C File Offset: 0x0007271C
		public override MdxExpression CompileLevelMemberUserDefined(IdentifierCubeExpression identifier, MdxExpression mdx, MdxProperty property)
		{
			string text;
			MdxExpression mdxExpression = (SapBwIdentifier.TryExtractDimensionAndLevel(identifier.Identifier, out text) ? new InvocationMdxExpression(MdxFunction.CurrentMember, new MdxExpression[]
			{
				new IdentifierMdxExpression(text)
			}) : mdx);
			return base.CompileLevelMemberUserDefined(identifier, mdxExpression, property);
		}

		// Token: 0x040010A2 RID: 4258
		private readonly SapBwVariablesCollection variables;

		// Token: 0x040010A3 RID: 4259
		private readonly string catalogName;
	}
}
