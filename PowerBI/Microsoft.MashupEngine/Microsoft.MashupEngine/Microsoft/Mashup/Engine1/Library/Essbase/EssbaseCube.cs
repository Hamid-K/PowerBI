using System;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C63 RID: 3171
	internal class EssbaseCube : MdxCubeMetadataProviderCube
	{
		// Token: 0x06005617 RID: 22039 RVA: 0x0012A7F0 File Offset: 0x001289F0
		public EssbaseCube(EssbaseService service, string serverSourceInfo, string applicationName, string cubeName)
			: base((MdxCube cube) => new EssbaseMdxCubeMetadataProvider(service, serverSourceInfo, applicationName, (EssbaseCube)cube), cubeName)
		{
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06005618 RID: 22040 RVA: 0x0012A82C File Offset: 0x00128A2C
		public override string MeasuresDimensionName
		{
			get
			{
				return ((EssbaseMdxCubeMetadataProvider)base.Metadata).MeasuresDimensionName;
			}
		}
	}
}
