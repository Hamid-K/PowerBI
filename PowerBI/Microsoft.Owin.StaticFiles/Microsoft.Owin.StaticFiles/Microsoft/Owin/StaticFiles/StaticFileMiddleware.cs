using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000012 RID: 18
	public class StaticFileMiddleware
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003270 File Offset: 0x00001470
		public StaticFileMiddleware(Func<IDictionary<string, object>, Task> next, StaticFileOptions options)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (options.ContentTypeProvider == null)
			{
				throw new ArgumentException(Resources.Args_NoContentTypeProvider);
			}
			if (options.FileSystem == null)
			{
				options.FileSystem = new PhysicalFileSystem("." + options.RequestPath.Value);
			}
			this._next = next;
			this._options = options;
			this._matchUrl = options.RequestPath;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000032F8 File Offset: 0x000014F8
		public Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			StaticFileContext fileContext = new StaticFileContext(context, this._options, this._matchUrl);
			if (!fileContext.ValidateMethod() || !fileContext.ValidatePath() || !fileContext.LookupContentType() || !fileContext.LookupFileInfo())
			{
				return this._next(environment);
			}
			fileContext.ComprehendRequestHeaders();
			switch (fileContext.GetPreconditionState())
			{
			case StaticFileContext.PreconditionState.Unspecified:
			case StaticFileContext.PreconditionState.ShouldProcess:
				if (fileContext.IsHeadMethod)
				{
					return fileContext.SendStatusAsync(200);
				}
				if (fileContext.IsRangeRequest)
				{
					return fileContext.SendRangeAsync();
				}
				return fileContext.SendAsync();
			case StaticFileContext.PreconditionState.NotModified:
				return fileContext.SendStatusAsync(304);
			case StaticFileContext.PreconditionState.PreconditionFailed:
				return fileContext.SendStatusAsync(412);
			default:
				throw new NotImplementedException(fileContext.GetPreconditionState().ToString());
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly StaticFileOptions _options;

		// Token: 0x0400003C RID: 60
		private readonly PathString _matchUrl;

		// Token: 0x0400003D RID: 61
		private readonly Func<IDictionary<string, object>, Task> _next;
	}
}
