using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class InvalidDataShapeResultJsonFormat : Exception
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004253 File Offset: 0x00002453
		public InvalidDataShapeResultJsonFormat(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000425D File Offset: 0x0000245D
		public InvalidDataShapeResultJsonFormat()
		{
		}
	}
}
