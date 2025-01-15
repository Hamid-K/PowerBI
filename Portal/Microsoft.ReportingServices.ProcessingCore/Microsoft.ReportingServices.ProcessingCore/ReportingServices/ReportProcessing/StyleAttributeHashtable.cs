using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200069C RID: 1692
	[Serializable]
	internal sealed class StyleAttributeHashtable : Hashtable
	{
		// Token: 0x06005C62 RID: 23650 RVA: 0x001797ED File Offset: 0x001779ED
		internal StyleAttributeHashtable()
		{
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x001797F5 File Offset: 0x001779F5
		internal StyleAttributeHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x001797FE File Offset: 0x001779FE
		private StyleAttributeHashtable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17002077 RID: 8311
		internal AttributeInfo this[string index]
		{
			get
			{
				return (AttributeInfo)base[index];
			}
		}
	}
}
