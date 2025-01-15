using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Uris;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000508 RID: 1288
	internal abstract class FileSystemResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x060029E1 RID: 10721 RVA: 0x0007D490 File Offset: 0x0007B690
		protected FileSystemResourceKindInfo(string kind, string label, string absolutePathError, IDataSourceLocationFactory dslFactory)
			: base(kind, label, false, false, false, false, false, false, new AuthenticationInfo[]
			{
				new WindowsAuthenticationInfo
				{
					SupportsAlternateCredentials = true
				}
			}, null, null, null, new IDataSourceLocationFactory[] { dslFactory })
		{
			this.absolutePathError = absolutePathError;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x0007D4D5 File Offset: 0x0007B6D5
		public override IEnumerable<string> EnumerateKnownSupersets(string resourcePath)
		{
			return Resource.UriSubPaths(this, resourcePath, Path.DirectorySeparatorChar);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x0007D4E4 File Offset: 0x0007B6E4
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			bool flag;
			if (!Resource.TryNormalizeFileUri(resourcePath, out text, out flag))
			{
				errorMessage = (flag ? Strings.Resource_FileUrl_Absolute : this.absolutePathError);
				resource = null;
				return false;
			}
			errorMessage = null;
			resource = new Resource(base.Kind, text, resourcePath);
			return true;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x0007D52C File Offset: 0x0007B72C
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			Uri uri;
			if (UriHelper.TryCreateAbsoluteUri(resourcePath, out uri) && uri.IsUnc)
			{
				hostName = uri.Host;
			}
			else
			{
				hostName = "localhost";
			}
			return true;
		}

		// Token: 0x04001237 RID: 4663
		protected readonly string absolutePathError;
	}
}
