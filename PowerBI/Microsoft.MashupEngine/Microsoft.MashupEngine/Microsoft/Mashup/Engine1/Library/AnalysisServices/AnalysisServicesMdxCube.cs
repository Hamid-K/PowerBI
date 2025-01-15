using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F18 RID: 3864
	internal sealed class AnalysisServicesMdxCube : MdxCubeMetadataProviderCube
	{
		// Token: 0x06006644 RID: 26180 RVA: 0x0015FE78 File Offset: 0x0015E078
		public AnalysisServicesMdxCube(AnalysisServicesService service, string name, string baseCubeName, string caption)
			: base(new AnalysisServicesMdxCubeMetadataProvider(service, name, baseCubeName), name)
		{
			this.baseCubeName = baseCubeName;
			this.caption = caption;
			this.supportsProperties = service.SupportsProperties;
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06006645 RID: 26181 RVA: 0x0015FEA4 File Offset: 0x0015E0A4
		public string BaseCubeName
		{
			get
			{
				return this.baseCubeName;
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x0015FEAC File Offset: 0x0015E0AC
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x0015FEB4 File Offset: 0x0015E0B4
		public override bool SupportsProperties
		{
			get
			{
				return this.supportsProperties;
			}
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x0015FEBC File Offset: 0x0015E0BC
		protected override void AddProperty(MdxLevel level, MdxPropertyMetadata mdProperty, List<MdxProperty> properties, Dictionary<string, MdxProperty> propertyKeys)
		{
			if (level.MdxIdentifier == mdProperty.LevelUniqueName)
			{
				base.AddProperty(level, mdProperty, properties, propertyKeys);
			}
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x0015FEDC File Offset: 0x0015E0DC
		protected override string BuildPropertyMdxIdentifier(MdxLevel level, MdxPropertyMetadata mdProperty)
		{
			return mdProperty.UniqueName;
		}

		// Token: 0x04003827 RID: 14375
		private readonly string baseCubeName;

		// Token: 0x04003828 RID: 14376
		private readonly string caption;

		// Token: 0x04003829 RID: 14377
		private readonly bool supportsProperties;
	}
}
