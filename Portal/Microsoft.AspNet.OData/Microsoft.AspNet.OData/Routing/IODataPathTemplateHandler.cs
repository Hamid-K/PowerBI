using System;
using Microsoft.AspNet.OData.Routing.Template;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000077 RID: 119
	public interface IODataPathTemplateHandler
	{
		// Token: 0x06000493 RID: 1171
		ODataPathTemplate ParseTemplate(string odataPathTemplate, IServiceProvider requestContainer);
	}
}
