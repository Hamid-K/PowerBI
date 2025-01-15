using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public sealed class TomInternalException : TomException
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x0007E92D File Offset: 0x0007CB2D
		internal TomInternalException()
			: base(TomSR.Exception_InternalErrorOccured)
		{
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0007E93A File Offset: 0x0007CB3A
		internal TomInternalException(string detailsMessage)
			: base(TomSR.Exception_InternalErrorOccured_Detailed(detailsMessage))
		{
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0007E948 File Offset: 0x0007CB48
		internal TomInternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0007E952 File Offset: 0x0007CB52
		internal static TomInternalException Create(string format, params object[] args)
		{
			return new TomInternalException((args != null && args.Length != 0) ? string.Format(format, args) : format);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0007E96C File Offset: 0x0007CB6C
		internal static TomInternalException CreateWithRestrictedInfo(string format, params KeyValuePair<InfoRestrictionType, object>[] args)
		{
			if (args == null || args.Length == 0)
			{
				return new TomInternalException(format);
			}
			object[] array = new object[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				array[i] = ((args[i].Value != null) ? ClientHostingManager.MarkAsRestrictedInformation(args[i].Value.ToString(), args[i].Key) : null);
			}
			return new TomInternalException(string.Format(format, array));
		}
	}
}
