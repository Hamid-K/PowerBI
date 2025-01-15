using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D05 RID: 7429
	public sealed class PackageSection : IPackageSection, IDocumentHost, ICacheableDocumentHost
	{
		// Token: 0x0600B954 RID: 47444 RVA: 0x00258B18 File Offset: 0x00256D18
		public PackageSection(IPackageSectionConfig config, string uniqueID, SegmentedString text)
		{
			this.config = config;
			this.uniqueID = uniqueID;
			this.text = text;
		}

		// Token: 0x17002DDB RID: 11739
		// (get) Token: 0x0600B955 RID: 47445 RVA: 0x00258B35 File Offset: 0x00256D35
		public string UniqueID
		{
			get
			{
				return this.uniqueID;
			}
		}

		// Token: 0x17002DDC RID: 11740
		// (get) Token: 0x0600B956 RID: 47446 RVA: 0x00258B3D File Offset: 0x00256D3D
		public object CacheIdentity
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17002DDD RID: 11741
		// (get) Token: 0x0600B957 RID: 47447 RVA: 0x00258B4A File Offset: 0x00256D4A
		public IPackageSectionConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17002DDE RID: 11742
		// (get) Token: 0x0600B958 RID: 47448 RVA: 0x00258B52 File Offset: 0x00256D52
		public SegmentedString Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x04005E51 RID: 24145
		private readonly IPackageSectionConfig config;

		// Token: 0x04005E52 RID: 24146
		private readonly string uniqueID;

		// Token: 0x04005E53 RID: 24147
		private readonly SegmentedString text;
	}
}
