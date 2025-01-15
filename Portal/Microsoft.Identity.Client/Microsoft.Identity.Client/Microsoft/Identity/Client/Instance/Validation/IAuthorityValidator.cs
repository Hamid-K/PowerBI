using System;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Instance.Validation
{
	// Token: 0x02000277 RID: 631
	internal interface IAuthorityValidator
	{
		// Token: 0x060018AA RID: 6314
		Task ValidateAuthorityAsync(AuthorityInfo authorityInfo);
	}
}
