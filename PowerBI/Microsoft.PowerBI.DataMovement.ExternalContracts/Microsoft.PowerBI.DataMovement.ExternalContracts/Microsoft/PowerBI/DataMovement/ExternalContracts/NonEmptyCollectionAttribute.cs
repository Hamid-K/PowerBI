using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
	public class NonEmptyCollectionAttribute : ValidationAttribute
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000267C File Offset: 0x0000087C
		public override bool IsValid(object value)
		{
			ICollection collection = value as ICollection;
			return collection != null && collection.Count > 0;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000269E File Offset: 0x0000089E
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: Must be a non-empty collection", name);
		}
	}
}
