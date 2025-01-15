using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200207D RID: 8317
	public sealed class PackageConfigPackageSectionConfig : PackageConfig, IPackageSectionConfig
	{
		// Token: 0x0600CB84 RID: 52100 RVA: 0x002887EB File Offset: 0x002869EB
		public static PackageConfigPackageSectionConfig New(PackageConfig packageConfig, IEngine engine, SegmentedString sectionText)
		{
			return new PackageConfigPackageSectionConfig(engine, sectionText)
			{
				Version = packageConfig.Version,
				MinVersion = packageConfig.MinVersion,
				Culture = packageConfig.Culture
			};
		}

		// Token: 0x0600CB85 RID: 52101 RVA: 0x00288818 File Offset: 0x00286A18
		private PackageConfigPackageSectionConfig(IEngine engine, SegmentedString sectionText)
		{
			this.engine = engine;
			this.sectionText = sectionText;
		}

		// Token: 0x17003105 RID: 12549
		// (get) Token: 0x0600CB86 RID: 52102 RVA: 0x0028882E File Offset: 0x00286A2E
		string IPackageSectionConfig.TimeZone
		{
			get
			{
				if (this.parsedConfig == null)
				{
					this.parsedConfig = this.ParseConfiguration();
				}
				return this.parsedConfig.TimeZone;
			}
		}

		// Token: 0x17003106 RID: 12550
		// (get) Token: 0x0600CB87 RID: 52103 RVA: 0x0028884F File Offset: 0x00286A4F
		string IPackageSectionConfig.Version
		{
			get
			{
				if (this.parsedConfig == null)
				{
					this.parsedConfig = this.ParseConfiguration();
				}
				return this.parsedConfig.Version;
			}
		}

		// Token: 0x17003107 RID: 12551
		// (get) Token: 0x0600CB88 RID: 52104 RVA: 0x00288870 File Offset: 0x00286A70
		public KeyValuePair<string, VersionRange>[] Dependencies
		{
			get
			{
				if (this.parsedConfig == null)
				{
					this.parsedConfig = this.ParseConfiguration();
				}
				return this.parsedConfig.Dependencies;
			}
		}

		// Token: 0x0600CB89 RID: 52105 RVA: 0x00288894 File Offset: 0x00286A94
		private IPackageSectionConfig ParseConfiguration()
		{
			Version version;
			VersionRange versionRange = (Versioning.TryParseVersion(base.Version, out version) ? new VersionRange(version) : new VersionRange(new Version(1, 0)));
			if (this.sectionText != default(SegmentedString) && this.engine != null)
			{
				ISectionDocument sectionDocument = this.engine.Parse(this.engine.Tokenize(this.sectionText), new TextDocumentHost(this.sectionText), delegate(IError error)
				{
				}) as ISectionDocument;
				if (sectionDocument != null && sectionDocument.Section != null)
				{
					return sectionDocument.Section.GetSectionMetadata(versionRange);
				}
			}
			return new PackageSectionConfig(base.Culture, null, base.Version, base.MinVersion, new KeyValuePair<string, VersionRange>[]
			{
				new KeyValuePair<string, VersionRange>("Core", versionRange)
			});
		}

		// Token: 0x0400674B RID: 26443
		private readonly IEngine engine;

		// Token: 0x0400674C RID: 26444
		private readonly SegmentedString sectionText;

		// Token: 0x0400674D RID: 26445
		private IPackageSectionConfig parsedConfig;
	}
}
