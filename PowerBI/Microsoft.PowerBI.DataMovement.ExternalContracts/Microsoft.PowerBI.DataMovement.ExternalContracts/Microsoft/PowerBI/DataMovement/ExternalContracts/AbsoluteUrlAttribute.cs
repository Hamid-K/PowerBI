using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
	public class AbsoluteUrlAttribute : ValidationAttribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		public override bool IsValid(object value)
		{
			string text = value as string;
			Uri uri;
			return text != null && Uri.TryCreate(text, UriKind.Absolute, out uri);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D6 File Offset: 0x000002D6
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: Must be a valid absolute url", name);
		}
	}
}
