using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030DD RID: 12509
	internal static class UnionHelper
	{
		// Token: 0x0601B2D5 RID: 111317 RVA: 0x0036FD48 File Offset: 0x0036DF48
		internal static OpenXmlSimpleType[] CreatePossibleMembers(UnionValueRestriction unionValueRestriction, FileFormatVersions fileFormatVersion)
		{
			switch (fileFormatVersion)
			{
			case FileFormatVersions.Office2007:
				return O12UnionHelper.CreatePossibleMembers(unionValueRestriction);
			case FileFormatVersions.Office2010:
				return O14UnionHelper.CreatePossibleMembers(unionValueRestriction);
			default:
				return null;
			}
		}

		// Token: 0x0601B2D6 RID: 111318 RVA: 0x0036FD78 File Offset: 0x0036DF78
		internal static OpenXmlSimpleType CreateTargetValueObject(RedirectedRestriction redirectedRestriction, FileFormatVersions fileFormatVersion)
		{
			switch (fileFormatVersion)
			{
			case FileFormatVersions.Office2007:
				return O12UnionHelper.CreateTargetValueObject(redirectedRestriction);
			case FileFormatVersions.Office2010:
				return O14UnionHelper.CreateTargetValueObject(redirectedRestriction);
			default:
				return null;
			}
		}
	}
}
