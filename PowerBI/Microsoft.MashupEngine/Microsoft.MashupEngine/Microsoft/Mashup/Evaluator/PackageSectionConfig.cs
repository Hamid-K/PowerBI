using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D06 RID: 7430
	public sealed class PackageSectionConfig : IPackageSectionConfig
	{
		// Token: 0x0600B959 RID: 47449 RVA: 0x00258B5A File Offset: 0x00256D5A
		public PackageSectionConfig(string culture, string timeZone, string version, string minVersion, KeyValuePair<string, VersionRange>[] dependencies)
		{
			this.culture = culture;
			this.timeZone = timeZone;
			this.version = version;
			this.minVersion = minVersion;
			this.dependencies = dependencies;
		}

		// Token: 0x17002DDF RID: 11743
		// (get) Token: 0x0600B95A RID: 47450 RVA: 0x00258B87 File Offset: 0x00256D87
		public string Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x17002DE0 RID: 11744
		// (get) Token: 0x0600B95B RID: 47451 RVA: 0x00258B8F File Offset: 0x00256D8F
		public string TimeZone
		{
			get
			{
				return this.timeZone;
			}
		}

		// Token: 0x17002DE1 RID: 11745
		// (get) Token: 0x0600B95C RID: 47452 RVA: 0x00258B97 File Offset: 0x00256D97
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17002DE2 RID: 11746
		// (get) Token: 0x0600B95D RID: 47453 RVA: 0x00258B9F File Offset: 0x00256D9F
		public string MinVersion
		{
			get
			{
				return this.minVersion;
			}
		}

		// Token: 0x17002DE3 RID: 11747
		// (get) Token: 0x0600B95E RID: 47454 RVA: 0x00258BA7 File Offset: 0x00256DA7
		public KeyValuePair<string, VersionRange>[] Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x04005E54 RID: 24148
		private readonly string culture;

		// Token: 0x04005E55 RID: 24149
		private readonly string timeZone;

		// Token: 0x04005E56 RID: 24150
		private readonly string version;

		// Token: 0x04005E57 RID: 24151
		private readonly string minVersion;

		// Token: 0x04005E58 RID: 24152
		private readonly KeyValuePair<string, VersionRange>[] dependencies;
	}
}
