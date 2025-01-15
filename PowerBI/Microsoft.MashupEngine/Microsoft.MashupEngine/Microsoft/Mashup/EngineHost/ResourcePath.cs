using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200197C RID: 6524
	public static class ResourcePath
	{
		// Token: 0x0600A586 RID: 42374 RVA: 0x002241BC File Offset: 0x002223BC
		public static bool StartsWith(IResource permittedResource, IResource attemptedAccessResource)
		{
			return ResourcePath.StartsWith(permittedResource.Kind, permittedResource.Path, attemptedAccessResource.Kind, attemptedAccessResource.Path);
		}

		// Token: 0x0600A587 RID: 42375 RVA: 0x002241DC File Offset: 0x002223DC
		public static bool StartsWith(string permittedResourceKind, string permittedResourcePath, string attemptedAccessResourceKind, string attemptedAccessResourcePath)
		{
			if (permittedResourceKind == "Folder" && attemptedAccessResourceKind == "File")
			{
				attemptedAccessResourceKind = "Folder";
			}
			ResourceKindInfo resourceKindInfo;
			return !(permittedResourceKind != attemptedAccessResourceKind) && (permittedResourcePath == null || (MashupEngines.Version1.TryLookupResourceKind(permittedResourceKind, out resourceKindInfo) && resourceKindInfo.IsSubset(permittedResourcePath, attemptedAccessResourcePath)));
		}

		// Token: 0x0600A588 RID: 42376 RVA: 0x00224234 File Offset: 0x00222434
		public static IEnumerable<string> AllStartsFrom(string kind, string path)
		{
			ResourceKindInfo resourceKindInfo;
			if (path == null || !MashupEngines.Version1.TryLookupResourceKind(kind, out resourceKindInfo))
			{
				return new string[0];
			}
			return resourceKindInfo.EnumerateKnownSupersets(path);
		}

		// Token: 0x0600A589 RID: 42377 RVA: 0x00224261 File Offset: 0x00222461
		public static int Length(string path)
		{
			if (path == null)
			{
				return -1;
			}
			return path.Length;
		}

		// Token: 0x0600A58A RID: 42378 RVA: 0x00224270 File Offset: 0x00222470
		public static Resource AdjustForNull(Resource permittedResource, string attemptedAccessResourcePath)
		{
			ResourceKindInfo resourceKindInfo;
			Uri uri;
			if (permittedResource.Path == null && MashupEngines.Version1.TryLookupResourceKind(permittedResource.Kind, out resourceKindInfo) && resourceKindInfo.IsUri && Uri.TryCreate(attemptedAccessResourcePath, UriKind.Absolute, out uri))
			{
				string absoluteUri = new UriBuilder(uri.Scheme, uri.Host, uri.Port).Uri.AbsoluteUri;
				return new Resource(permittedResource.Kind, absoluteUri, absoluteUri);
			}
			return permittedResource;
		}

		// Token: 0x04005631 RID: 22065
		private const char InstanceDatabaseSeparator = ';';
	}
}
