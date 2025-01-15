using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles.DirectoryFormatters
{
	// Token: 0x02000019 RID: 25
	public class HtmlDirectoryFormatter : IDirectoryFormatter
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public virtual Task GenerateContentAsync(IOwinContext context, IEnumerable<IFileInfo> contents)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			context.Response.ContentType = "text/html; charset=utf-8";
			if (Helpers.IsHeadMethod(context.Request.Method))
			{
				return Constants.CompletedTask;
			}
			PathString requestPath = context.Request.PathBase + context.Request.Path;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("<!DOCTYPE html>\r\n<html lang=\"{0}\">", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
			builder.AppendFormat("\r\n<head>\r\n  <title>{0} {1}</title>", HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_IndexOf), HtmlDirectoryFormatter.HtmlEncode(requestPath.Value));
			builder.Append("\r\n  <style>\r\n    body {\r\n        font-family: \"Segoe UI\", \"Segoe WP\", \"Helvetica Neue\", 'RobotoRegular', sans-serif;\r\n        font-size: 14px;}\r\n    header h1 {\r\n        font-family: \"Segoe UI Light\", \"Helvetica Neue\", 'RobotoLight', \"Segoe UI\", \"Segoe WP\", sans-serif;\r\n        font-size: 28px;\r\n        font-weight: 100;\r\n        margin-top: 5px;\r\n        margin-bottom: 0px;}\r\n    #index {\r\n        border-collapse: separate; \r\n        border-spacing: 0; \r\n        margin: 0 0 20px; }\r\n    #index th {\r\n        vertical-align: bottom;\r\n        padding: 10px 5px 5px 5px;\r\n        font-weight: 400;\r\n        color: #a0a0a0;\r\n        text-align: center; }\r\n    #index td { padding: 3px 10px; }\r\n    #index th, #index td {\r\n        border-right: 1px #ddd solid;\r\n        border-bottom: 1px #ddd solid;\r\n        border-left: 1px transparent solid;\r\n        border-top: 1px transparent solid;\r\n        box-sizing: border-box; }\r\n    #index th:last-child, #index td:last-child {\r\n        border-right: 1px transparent solid; }\r\n    #index td.length, td.modified { text-align:right; }\r\n    a { color:#1ba1e2;text-decoration:none; }\r\n    a:hover { color:#13709e;text-decoration:underline; }\r\n  </style>\r\n</head>\r\n<body>\r\n  <section id=\"main\">");
			builder.AppendFormat("\r\n    <header><h1>{0} <a href=\"/\">/</a>", HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_IndexOf));
			string cumulativePath = "/";
			foreach (string segment in requestPath.Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
			{
				cumulativePath = cumulativePath + segment + "/";
				builder.AppendFormat("<a href=\"{0}\">{1}/</a>", HtmlDirectoryFormatter.HtmlEncode(cumulativePath), HtmlDirectoryFormatter.HtmlEncode(segment));
			}
			builder.AppendFormat(CultureInfo.CurrentUICulture, "</h1></header>\r\n    <table id=\"index\" summary=\"{0}\">\r\n    <thead>\r\n      <tr><th abbr=\"{1}\">{1}</th><th abbr=\"{2}\">{2}</th><th abbr=\"{3}\">{4}</th></tr>\r\n    </thead>\r\n    <tbody>", new object[]
			{
				HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_TableSummary),
				HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_Name),
				HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_Size),
				HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_Modified),
				HtmlDirectoryFormatter.HtmlEncode(Resources.HtmlDir_LastModified)
			});
			foreach (IFileInfo subdir in contents.Where((IFileInfo info) => info.IsDirectory))
			{
				builder.AppendFormat("\r\n      <tr class=\"directory\">\r\n        <td class=\"name\"><a href=\"{0}/\">{0}/</a></td>\r\n        <td></td>\r\n        <td class=\"modified\">{1}</td>\r\n      </tr>", HtmlDirectoryFormatter.HtmlEncode(subdir.Name), HtmlDirectoryFormatter.HtmlEncode(subdir.LastModified.ToString(CultureInfo.CurrentCulture)));
			}
			foreach (IFileInfo file in contents.Where((IFileInfo info) => !info.IsDirectory))
			{
				builder.AppendFormat("\r\n      <tr class=\"file\">\r\n        <td class=\"name\"><a href=\"{0}\">{0}</a></td>\r\n        <td class=\"length\">{1}</td>\r\n        <td class=\"modified\">{2}</td>\r\n      </tr>", HtmlDirectoryFormatter.HtmlEncode(file.Name), HtmlDirectoryFormatter.HtmlEncode(file.Length.ToString("n0", CultureInfo.CurrentCulture)), HtmlDirectoryFormatter.HtmlEncode(file.LastModified.ToString(CultureInfo.CurrentCulture)));
			}
			builder.Append("\r\n    </tbody>\r\n    </table>\r\n  </section>\r\n</body>\r\n</html>");
			string data = builder.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			context.Response.ContentLength = new long?((long)bytes.Length);
			return context.Response.WriteAsync(bytes);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DDC File Offset: 0x00001FDC
		private static string HtmlEncode(string body)
		{
			return WebUtility.HtmlEncode(body);
		}
	}
}
