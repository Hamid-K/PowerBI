using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x0200000A RID: 10
	public class DirectoryBrowserMiddleware
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000024C0 File Offset: 0x000006C0
		public DirectoryBrowserMiddleware(Func<IDictionary<string, object>, Task> next, DirectoryBrowserOptions options)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (options.Formatter == null)
			{
				throw new ArgumentException(Resources.Args_NoFormatter);
			}
			if (options.FileSystem == null)
			{
				options.FileSystem = new PhysicalFileSystem("." + options.RequestPath.Value);
			}
			this._next = next;
			this._options = options;
			this._matchUrl = options.RequestPath;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002548 File Offset: 0x00000748
		public Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			PathString subpath;
			IEnumerable<IFileInfo> contents;
			if (!Helpers.IsGetOrHeadMethod(context.Request.Method) || !Helpers.TryMatchPath(context, this._matchUrl, true, out subpath) || !this.TryGetDirectoryInfo(subpath, out contents))
			{
				return this._next(environment);
			}
			if (!Helpers.PathEndsInSlash(context.Request.Path))
			{
				context.Response.StatusCode = 301;
				context.Response.Headers["Location"] = (context.Request.PathBase + context.Request.Path).ToString() + "/" + context.Request.QueryString.ToString();
				return Constants.CompletedTask;
			}
			return this._options.Formatter.GenerateContentAsync(context, contents);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000263C File Offset: 0x0000083C
		private bool TryGetDirectoryInfo(PathString subpath, out IEnumerable<IFileInfo> contents)
		{
			return this._options.FileSystem.TryGetDirectoryContents(subpath.Value, out contents);
		}

		// Token: 0x0400001A RID: 26
		private readonly DirectoryBrowserOptions _options;

		// Token: 0x0400001B RID: 27
		private readonly PathString _matchUrl;

		// Token: 0x0400001C RID: 28
		private readonly Func<IDictionary<string, object>, Task> _next;
	}
}
