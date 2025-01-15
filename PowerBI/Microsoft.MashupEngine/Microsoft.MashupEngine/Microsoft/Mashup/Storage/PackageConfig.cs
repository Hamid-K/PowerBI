using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200207B RID: 8315
	[XmlRoot("Package")]
	public class PackageConfig : XmlRoot
	{
		// Token: 0x17003102 RID: 12546
		// (get) Token: 0x0600CB7D RID: 52093 RVA: 0x0028877C File Offset: 0x0028697C
		// (set) Token: 0x0600CB7E RID: 52094 RVA: 0x00288784 File Offset: 0x00286984
		[XmlElement]
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17003103 RID: 12547
		// (get) Token: 0x0600CB7F RID: 52095 RVA: 0x0028878D File Offset: 0x0028698D
		// (set) Token: 0x0600CB80 RID: 52096 RVA: 0x00288795 File Offset: 0x00286995
		[XmlElement]
		public string MinVersion
		{
			get
			{
				return this.minVersion;
			}
			set
			{
				this.minVersion = value;
			}
		}

		// Token: 0x17003104 RID: 12548
		// (get) Token: 0x0600CB81 RID: 52097 RVA: 0x0028879E File Offset: 0x0028699E
		// (set) Token: 0x0600CB82 RID: 52098 RVA: 0x002887A6 File Offset: 0x002869A6
		[XmlElement]
		public string Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x04006746 RID: 26438
		public const string contentType = "text/xml";

		// Token: 0x04006747 RID: 26439
		public const string fileName = "Package.xml";

		// Token: 0x04006748 RID: 26440
		private string version;

		// Token: 0x04006749 RID: 26441
		private string minVersion;

		// Token: 0x0400674A RID: 26442
		private string culture;
	}
}
