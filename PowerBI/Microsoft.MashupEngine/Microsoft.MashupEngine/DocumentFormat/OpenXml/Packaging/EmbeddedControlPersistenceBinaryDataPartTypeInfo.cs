using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030C5 RID: 12485
	internal static class EmbeddedControlPersistenceBinaryDataPartTypeInfo
	{
		// Token: 0x0601B1DF RID: 111071 RVA: 0x0036C37C File Offset: 0x0036A57C
		internal static string GetContentType(EmbeddedControlPersistenceBinaryDataPartType controlType)
		{
			if (controlType == EmbeddedControlPersistenceBinaryDataPartType.ActiveXBin)
			{
				return "application/vnd.ms-office.activeX";
			}
			throw new ArgumentOutOfRangeException("controlType");
		}

		// Token: 0x0601B1E0 RID: 111072 RVA: 0x0036C3A0 File Offset: 0x0036A5A0
		internal static string GetTargetExtension(EmbeddedControlPersistenceBinaryDataPartType controlType)
		{
			if (controlType == EmbeddedControlPersistenceBinaryDataPartType.ActiveXBin)
			{
				return ".bin";
			}
			return ".bin";
		}
	}
}
