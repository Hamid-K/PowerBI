using System;
using System.Linq;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200001E RID: 30
	internal static class ODataQueryContextExtensions
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000042F0 File Offset: 0x000024F0
		public static ODataQuerySettings UpdateQuerySettings(this ODataQueryContext context, ODataQuerySettings querySettings, IQueryable query)
		{
			ODataQuerySettings odataQuerySettings = ((context == null || context.RequestContainer == null) ? new ODataQuerySettings() : ServiceProviderServiceExtensions.GetRequiredService<ODataQuerySettings>(context.RequestContainer));
			odataQuerySettings.CopyFrom(querySettings);
			if (odataQuerySettings.HandleNullPropagation == HandleNullPropagationOption.Default)
			{
				odataQuerySettings.HandleNullPropagation = ((query != null) ? HandleNullPropagationOptionHelper.GetDefaultHandleNullPropagationOption(query) : HandleNullPropagationOption.True);
			}
			return odataQuerySettings;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000433D File Offset: 0x0000253D
		public static SkipTokenHandler GetSkipTokenHandler(this ODataQueryContext context)
		{
			if (context == null || context.RequestContainer == null)
			{
				return DefaultSkipTokenHandler.Instance;
			}
			return ServiceProviderServiceExtensions.GetRequiredService<SkipTokenHandler>(context.RequestContainer);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000435B File Offset: 0x0000255B
		public static SkipTokenQueryValidator GetSkipTokenQueryValidator(this ODataQueryContext context)
		{
			if (context == null || context.RequestContainer == null)
			{
				return new SkipTokenQueryValidator();
			}
			return ServiceProviderServiceExtensions.GetRequiredService<SkipTokenQueryValidator>(context.RequestContainer);
		}
	}
}
