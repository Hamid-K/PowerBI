using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x0200001F RID: 31
	public sealed class ResponseCompressionMiddleware : OwinMiddleware
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00002E31 File Offset: 0x00001031
		public ResponseCompressionMiddleware(OwinMiddleware next)
			: base(next)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003690 File Offset: 0x00001890
		public override async Task Invoke(IOwinContext context)
		{
			bool flag = ResponseCompressionMiddleware.CanCompress(context);
			context.Set<bool>("CanCompress", flag);
			if (!flag || ResponseCompressionMiddleware.CouldBeStreamResponse(context))
			{
				await base.Next.Invoke(context);
			}
			else
			{
				Stream body = context.Response.Body;
				context.Response.Body = new MemoryStream();
				bool exceptionOccured = false;
				try
				{
					await base.Next.Invoke(context);
					using (ScopeMeter.Use(new string[]
					{
						"owin",
						base.GetType().Name
					}))
					{
						int num2;
						int num = num2 - 2;
						try
						{
							if (!context.Response.Body.CanSeek || !context.Response.Body.CanRead)
							{
								throw new InvalidOperationException("The response stream has been replaced.");
							}
							if (ResponseCompressionMiddleware.ShouldCompress(context))
							{
								context.Response.Headers["Transfer-Encoding"] = "chunked";
								context.Response.Headers["Content-Encoding"] = "gzip";
								using (GZipStream gzip = new GZipStream(body, CompressionMode.Compress, true))
								{
									context.Response.Body.Seek(0L, SeekOrigin.Begin);
									await context.Response.Body.CopyToAsync(gzip, 81920, context.Request.CallCancelled);
								}
								GZipStream gzip = null;
								return;
							}
							context.Response.Body.Seek(0L, SeekOrigin.Begin);
							context.Response.ContentLength = new long?(context.Response.Body.Length);
							await context.Response.Body.CopyToAsync(body, 81920, context.Request.CallCancelled);
						}
						catch (InvalidOperationException ex)
						{
							Logger.Warning("Unhandled exception in handling response compression {0}: ", new object[] { ex });
							exceptionOccured = true;
						}
					}
					IDisposable disposable = null;
				}
				finally
				{
					if (!exceptionOccured)
					{
						context.Response.Body = body;
					}
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036E0 File Offset: 0x000018E0
		private static bool CanCompress(IOwinContext context)
		{
			return string.Equals(context.Request.Protocol, "HTTP/1.1", StringComparison.Ordinal) && (context.Request.Headers["Accept-encoding"] ?? string.Empty).Contains("gzip");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003730 File Offset: 0x00001930
		private static bool ShouldCompress(IOwinContext context)
		{
			if (context.Response.Body.Length < 4096L)
			{
				return false;
			}
			if (context.Response.Headers.ContainsKey("Content-Encoding"))
			{
				return false;
			}
			string contentType = context.Response.Headers["Content-Type"];
			return !string.IsNullOrEmpty(contentType) && ResponseCompressionMiddleware.ContentTypesToCompress.Any((string t) => contentType.StartsWith(t, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037BC File Offset: 0x000019BC
		private static bool CouldBeStreamResponse(IOwinContext context)
		{
			return context.Request.Path.HasValue && context.Request.Path.Value.EndsWith(")/Content/$value", StringComparison.InvariantCultureIgnoreCase) && context.Request.Method == "GET";
		}

		// Token: 0x04000057 RID: 87
		private const int MinSize = 4096;

		// Token: 0x04000058 RID: 88
		private const int DefaultCopyBufferSize = 81920;

		// Token: 0x04000059 RID: 89
		private static readonly string[] ContentTypesToCompress = new string[] { "application/json", "application/javascript", "text/css", "text/html", "text/javascript" };
	}
}
