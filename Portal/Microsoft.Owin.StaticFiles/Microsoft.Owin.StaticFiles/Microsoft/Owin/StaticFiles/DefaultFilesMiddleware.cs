using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000008 RID: 8
	public class DefaultFilesMiddleware
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002274 File Offset: 0x00000474
		public DefaultFilesMiddleware(Func<IDictionary<string, object>, Task> next, DefaultFilesOptions options)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (options.FileSystem == null)
			{
				options.FileSystem = new PhysicalFileSystem("." + options.RequestPath.Value);
			}
			this._next = next;
			this._options = options;
			this._matchUrl = options.RequestPath;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E8 File Offset: 0x000004E8
		public Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			PathString subpath;
			IEnumerable<IFileInfo> dirContents;
			if (Helpers.IsGetOrHeadMethod(context.Request.Method) && Helpers.TryMatchPath(context, this._matchUrl, true, out subpath) && this._options.FileSystem.TryGetDirectoryContents(subpath.Value, out dirContents))
			{
				int matchIndex = 0;
				while (matchIndex < this._options.DefaultFileNames.Count)
				{
					string defaultFile = this._options.DefaultFileNames[matchIndex];
					IFileInfo file;
					if (this._options.FileSystem.TryGetFileInfo(subpath.Value + defaultFile, out file))
					{
						if (!Helpers.PathEndsInSlash(context.Request.Path))
						{
							context.Response.StatusCode = 301;
							context.Response.Headers["Location"] = (context.Request.PathBase + context.Request.Path).ToString() + "/" + context.Request.QueryString.ToString();
							return Constants.CompletedTask;
						}
						context.Request.Path = new PathString(context.Request.Path.Value + defaultFile);
						break;
					}
					else
					{
						matchIndex++;
					}
				}
			}
			return this._next(environment);
		}

		// Token: 0x04000016 RID: 22
		private readonly DefaultFilesOptions _options;

		// Token: 0x04000017 RID: 23
		private readonly PathString _matchUrl;

		// Token: 0x04000018 RID: 24
		private readonly Func<IDictionary<string, object>, Task> _next;
	}
}
