using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018BF RID: 6335
	public abstract class DataSourceLocationFactory : IDataSourceLocationFactory
	{
		// Token: 0x1700294C RID: 10572
		// (get) Token: 0x0600A18A RID: 41354
		public abstract string Protocol { get; }

		// Token: 0x0600A18B RID: 41355
		public abstract IDataSourceLocation New();

		// Token: 0x0600A18C RID: 41356 RVA: 0x0007D355 File Offset: 0x0007B555
		protected virtual bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
		{
			location = null;
			return false;
		}

		// Token: 0x0600A18D RID: 41357 RVA: 0x0021861A File Offset: 0x0021681A
		public virtual bool TryCreateFromResource(IResource resource, bool normalize, out IDataSourceLocation location)
		{
			return this.TryCreateFromResourcePath(normalize ? resource.Path : resource.NonNormalizedPath, out location);
		}

		// Token: 0x0600A18E RID: 41358 RVA: 0x00218634 File Offset: 0x00216834
		protected static bool TryCreateUrlLocation(string protocol, string url, out IDataSourceLocation location)
		{
			location = DataSourceLocationFactory.New(protocol);
			location.Address = new Dictionary<string, object> { { "url", url } };
			return true;
		}

		// Token: 0x0600A18F RID: 41359 RVA: 0x00218657 File Offset: 0x00216857
		protected static bool TryCreatePathLocation(string protocol, string path, out IDataSourceLocation location)
		{
			location = DataSourceLocationFactory.New(protocol);
			location.Address = new Dictionary<string, object> { { "path", path } };
			return true;
		}

		// Token: 0x0600A190 RID: 41360 RVA: 0x0021867C File Offset: 0x0021687C
		protected static bool TryCreateDatabaseLocation(string protocol, string resourcePath, out IDataSourceLocation location)
		{
			string text;
			string text2;
			if (DatabaseResource.TryParsePath(resourcePath, out text, out text2))
			{
				location = DataSourceLocationFactory.New(protocol);
				location.Address = new Dictionary<string, object>
				{
					{ "server", text },
					{ "database", text2 }
				};
				return true;
			}
			location = null;
			return false;
		}

		// Token: 0x0600A191 RID: 41361 RVA: 0x002186C8 File Offset: 0x002168C8
		protected static bool TryCreateAzureLocation(string protocol, string resourcePath, out IDataSourceLocation location)
		{
			location = DataSourceLocationFactory.New(protocol);
			Uri uri = new Uri(resourcePath);
			int num = uri.Host.IndexOf('.');
			if (num == -1)
			{
				location = null;
				return false;
			}
			location.Address["account"] = uri.Host.Substring(0, num);
			location.Address["domain"] = uri.Host.Substring(num + 1);
			if (!string.IsNullOrEmpty(uri.AbsolutePath) && !uri.AbsolutePath.Equals('/'.ToString()))
			{
				location.Address["container"] = uri.AbsolutePath.Trim(new char[] { '/' });
			}
			return true;
		}

		// Token: 0x0600A192 RID: 41362 RVA: 0x00218784 File Offset: 0x00216984
		public static IDataSourceLocation New(string protocol)
		{
			return ResourceKinds.GetDataSourceLocationFactory(protocol).New();
		}
	}
}
