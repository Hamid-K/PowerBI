using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal sealed class IncompatibleFormatVersionException : Exception
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00005B38 File Offset: 0x00003D38
		internal IncompatibleFormatVersionException(ObjectType declaredType, long streamPos)
			: base(string.Concat(new string[]
			{
				"The Intermediate Format Version is incompatible. The ObjectType read, at stream position ",
				streamPos.ToString(CultureInfo.InvariantCulture),
				", is ",
				declaredType.ToString(),
				"."
			}))
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005B8D File Offset: 0x00003D8D
		private IncompatibleFormatVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
