using System;
using System.Collections.Generic;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000054 RID: 84
	public interface IValueProviderParameterBinding
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600024C RID: 588
		IEnumerable<ValueProviderFactory> ValueProviderFactories { get; }
	}
}
