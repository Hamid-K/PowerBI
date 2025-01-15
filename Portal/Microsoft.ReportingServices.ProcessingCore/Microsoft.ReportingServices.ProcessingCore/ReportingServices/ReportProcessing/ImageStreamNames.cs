using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200069B RID: 1691
	[Serializable]
	internal sealed class ImageStreamNames : Hashtable
	{
		// Token: 0x06005C5E RID: 23646 RVA: 0x001797C4 File Offset: 0x001779C4
		internal ImageStreamNames()
		{
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x001797CC File Offset: 0x001779CC
		internal ImageStreamNames(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002076 RID: 8310
		internal ImageInfo this[string url]
		{
			get
			{
				return (ImageInfo)base[url];
			}
			set
			{
				base[url] = value;
			}
		}
	}
}
