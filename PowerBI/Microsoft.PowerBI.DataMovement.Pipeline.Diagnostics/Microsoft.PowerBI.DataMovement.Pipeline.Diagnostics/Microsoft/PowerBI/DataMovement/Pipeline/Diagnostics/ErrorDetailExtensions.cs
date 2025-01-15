using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public static class ErrorDetailExtensions
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000023F8 File Offset: 0x000005F8
		public static PowerBIErrorDetail CreateForEmbeddedValue(string nameCode, string embeddedValue)
		{
			PowerBIErrorDetailValue powerBIErrorDetailValue = new PowerBIErrorDetailValue(PowerBIErrorResourceType.EmbeddedString, embeddedValue);
			return new PowerBIErrorDetail(nameCode, powerBIErrorDetailValue);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002414 File Offset: 0x00000614
		public static PowerBIErrorDetail CreateForReferencedValue(string nameCode, string valueCode)
		{
			PowerBIErrorDetailValue powerBIErrorDetailValue = new PowerBIErrorDetailValue(PowerBIErrorResourceType.ResourceCodeReference, valueCode);
			return new PowerBIErrorDetail(nameCode, powerBIErrorDetailValue);
		}
	}
}
