using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000CD RID: 205
	public class EmbeddedImage : ReportObject, INamedObject
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001D909 File Offset: 0x0001BB09
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0001D91C File Offset: 0x0001BB1C
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001D92B File Offset: 0x0001BB2B
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0001D93E File Offset: 0x0001BB3E
		public string MIMEType
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001D94D File Offset: 0x0001BB4D
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0001D960 File Offset: 0x0001BB60
		public ImageData ImageData
		{
			get
			{
				return (ImageData)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001D96F File Offset: 0x0001BB6F
		public EmbeddedImage()
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001D977 File Offset: 0x0001BB77
		internal EmbeddedImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000376 RID: 886
		internal class Definition : DefinitionStore<EmbeddedImage, EmbeddedImage.Definition.Properties>
		{
			// Token: 0x0600180B RID: 6155 RVA: 0x0003B274 File Offset: 0x00039474
			private Definition()
			{
			}

			// Token: 0x02000491 RID: 1169
			internal enum Properties
			{
				// Token: 0x04000BB4 RID: 2996
				Name,
				// Token: 0x04000BB5 RID: 2997
				MIMEType,
				// Token: 0x04000BB6 RID: 2998
				ImageData
			}
		}
	}
}
