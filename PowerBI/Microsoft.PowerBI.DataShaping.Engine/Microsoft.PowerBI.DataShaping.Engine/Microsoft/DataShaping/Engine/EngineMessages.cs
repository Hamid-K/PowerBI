using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200000F RID: 15
	internal static class EngineMessages
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002BB3 File Offset: 0x00000DB3
		public static EngineMessage UnsupportedTransformConfiguration(EngineMessageSeverity severity)
		{
			return EngineMessages.CreateMessage(EngineErrorCode.UnsupportedTransformConfiguration, severity, ErrorSource.PowerBI, Array.Empty<string>());
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public static EngineMessage AmbiguousTransformConfiguration(EngineMessageSeverity severity, string algorithmName)
		{
			return EngineMessages.CreateMessage(EngineErrorCode.AmbiguousTransformConfiguration, severity, ErrorSource.PowerBI, new string[] { algorithmName });
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public static EngineMessage MissingTransformConfiguration(EngineMessageSeverity severity, string algorithmName)
		{
			return EngineMessages.CreateMessage(EngineErrorCode.MissingTransformConfiguration, severity, ErrorSource.PowerBI, new string[] { algorithmName });
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BEA File Offset: 0x00000DEA
		private static EngineMessage CreateMessage(EngineErrorCode errorCode, EngineMessageSeverity severity, ErrorSource source, params string[] args)
		{
			return EngineMessage.Create(errorCode, severity, source, args);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BF8 File Offset: 0x00000DF8
		internal static string GetTemplate(EngineErrorCode errorCode)
		{
			string text;
			if (EngineMessages._engineErrorStrings.TryGetValue(errorCode.ToString(), out text))
			{
				return text;
			}
			return EngineMessages.GetTemplate(EngineErrorCode.InternalDataShapeEngineError);
		}

		// Token: 0x0400003A RID: 58
		private static Dictionary<string, string> _engineErrorStrings = new Dictionary<string, string>
		{
			{ "UnsupportedTransformConfiguration", "A single query can not contain transforms from both IDaxDataTransformMetadataFactory and IDataTransformPluginFactory" },
			{ "AmbiguousTransformConfiguration", "Ambiguous transform metadata: transform {0} found in both IDaxDataTransformMetadataFactory and IDataTransformPluginFactory" },
			{ "MissingTransformConfiguration", "Missing transform metadata: no metadata found for transform : {0}" },
			{ "InternalDataShapeEngineError", "Data Shape engine failed with error code: '{0}'. Check the report server logs for more information." }
		};
	}
}
