using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003108 RID: 12552
	internal static class XsdAnyPrefidefinedValue
	{
		// Token: 0x0601B387 RID: 111495 RVA: 0x00373048 File Offset: 0x00371248
		internal static string GetNamespaceString(ushort value)
		{
			switch (value)
			{
			case 0:
				return "##any";
			case 1:
				return "##other";
			case 2:
				return "##local";
			case 3:
				return "##targetNamespace";
			default:
				return string.Empty;
			}
		}

		// Token: 0x0400B499 RID: 46233
		public const ushort Any = 0;

		// Token: 0x0400B49A RID: 46234
		public const ushort Other = 1;

		// Token: 0x0400B49B RID: 46235
		public const ushort Local = 2;

		// Token: 0x0400B49C RID: 46236
		public const ushort TargetNamespace = 3;
	}
}
