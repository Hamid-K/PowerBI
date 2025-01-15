using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030C3 RID: 12483
	internal static class EmbeddedControlPersistencePartTypeInfo
	{
		// Token: 0x0601B1DD RID: 111069 RVA: 0x0036C314 File Offset: 0x0036A514
		internal static string GetContentType(EmbeddedControlPersistencePartType controlType)
		{
			switch (controlType)
			{
			case EmbeddedControlPersistencePartType.ActiveX:
				return "application/vnd.ms-office.activeX+xml";
			case EmbeddedControlPersistencePartType.ActiveXBin:
				return "application/vnd.ms-office.activeX";
			default:
				throw new ArgumentOutOfRangeException("controlType");
			}
		}

		// Token: 0x0601B1DE RID: 111070 RVA: 0x0036C34C File Offset: 0x0036A54C
		internal static string GetTargetExtension(EmbeddedControlPersistencePartType controlType)
		{
			switch (controlType)
			{
			case EmbeddedControlPersistencePartType.ActiveX:
				return ".xml";
			case EmbeddedControlPersistencePartType.ActiveXBin:
				return ".bin";
			default:
				return ".bin";
			}
		}
	}
}
