using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200069D RID: 1693
	[Serializable]
	internal sealed class EmbeddedImageHashtable : Hashtable
	{
		// Token: 0x06005C66 RID: 23654 RVA: 0x00179816 File Offset: 0x00177A16
		internal EmbeddedImageHashtable()
		{
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x0017981E File Offset: 0x00177A1E
		internal EmbeddedImageHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x00179827 File Offset: 0x00177A27
		private EmbeddedImageHashtable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17002078 RID: 8312
		internal ImageInfo this[string index]
		{
			get
			{
				return (ImageInfo)base[index];
			}
		}
	}
}
