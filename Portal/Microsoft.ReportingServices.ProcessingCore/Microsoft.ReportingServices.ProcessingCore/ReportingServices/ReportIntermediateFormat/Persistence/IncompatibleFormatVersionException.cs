using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000535 RID: 1333
	[Serializable]
	internal sealed class IncompatibleFormatVersionException : Exception
	{
		// Token: 0x060048F5 RID: 18677 RVA: 0x0013470C File Offset: 0x0013290C
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

		// Token: 0x060048F6 RID: 18678 RVA: 0x00134761 File Offset: 0x00132961
		private IncompatibleFormatVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
