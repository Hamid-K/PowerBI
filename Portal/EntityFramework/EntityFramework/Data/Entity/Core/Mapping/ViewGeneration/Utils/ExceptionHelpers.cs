using System;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Utils
{
	// Token: 0x02000572 RID: 1394
	internal static class ExceptionHelpers
	{
		// Token: 0x060043BF RID: 17343 RVA: 0x000EC3D8 File Offset: 0x000EA5D8
		internal static void ThrowMappingException(ErrorLog.Record errorRecord, ConfigViewGenerator config)
		{
			InternalMappingException ex = new InternalMappingException(errorRecord.ToUserString(), errorRecord);
			if (config.IsNormalTracing)
			{
				ex.ErrorLog.PrintTrace();
			}
			throw ex;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000EC408 File Offset: 0x000EA608
		internal static void ThrowMappingException(ErrorLog errorLog, ConfigViewGenerator config)
		{
			InternalMappingException ex = new InternalMappingException(errorLog.ToUserString(), errorLog);
			if (config.IsNormalTracing)
			{
				ex.ErrorLog.PrintTrace();
			}
			throw ex;
		}
	}
}
