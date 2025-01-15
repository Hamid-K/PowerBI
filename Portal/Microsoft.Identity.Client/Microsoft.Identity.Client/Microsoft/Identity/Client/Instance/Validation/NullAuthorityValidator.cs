using System;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Instance.Validation
{
	// Token: 0x02000278 RID: 632
	internal class NullAuthorityValidator : IAuthorityValidator
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x00051883 File Offset: 0x0004FA83
		public Task ValidateAuthorityAsync(AuthorityInfo authorityInfo)
		{
			return Task.FromResult<int>(0);
		}
	}
}
