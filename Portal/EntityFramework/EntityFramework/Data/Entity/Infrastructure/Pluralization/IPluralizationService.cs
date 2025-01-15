using System;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200026C RID: 620
	public interface IPluralizationService
	{
		// Token: 0x06001F76 RID: 8054
		string Pluralize(string word);

		// Token: 0x06001F77 RID: 8055
		string Singularize(string word);
	}
}
