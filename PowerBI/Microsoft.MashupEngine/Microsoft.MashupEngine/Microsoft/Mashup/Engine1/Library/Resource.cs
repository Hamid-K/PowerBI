using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Library.Uris.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x02000262 RID: 610
	public sealed class Resource : IResource
	{
		// Token: 0x060019F8 RID: 6648 RVA: 0x00034283 File Offset: 0x00032483
		internal Resource(string kind, string path, string nonNormalizedPath)
		{
			this.kind = kind;
			this.path = path;
			this.nonNormalizedPath = nonNormalizedPath;
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000342A0 File Offset: 0x000324A0
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000342A8 File Offset: 0x000324A8
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x000342B0 File Offset: 0x000324B0
		public string NonNormalizedPath
		{
			get
			{
				return this.nonNormalizedPath;
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000342B8 File Offset: 0x000324B8
		public override bool Equals(object obj)
		{
			Resource resource = obj as Resource;
			return resource != null && string.Equals(this.kind, resource.Kind, StringComparison.Ordinal) && string.Equals(this.path, resource.Path, StringComparison.Ordinal);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000342F7 File Offset: 0x000324F7
		public override int GetHashCode()
		{
			return StringComparer.Ordinal.GetHashCode(this.kind) ^ StringComparer.Ordinal.GetHashCode(this.path);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0003431A File Offset: 0x0003251A
		public override string ToString()
		{
			return this.kind + "/" + this.path;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00034334 File Offset: 0x00032534
		public static IResource New(string kind, string path)
		{
			IResource resource;
			ValueException ex;
			if (!Resource.TryNew(kind, path, out resource, out ex))
			{
				throw ex;
			}
			return resource;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00034354 File Offset: 0x00032554
		public static bool TryNew(string kind, string path, out IResource resource, out ValueException exception)
		{
			ResourceKindInfo resourceKindInfo;
			if (!ResourceKinds.Lookup(kind, out resourceKindInfo))
			{
				resource = null;
				exception = ValueException.NewDataFormatError<Message0>(Strings.Resource_Invalid, TextValue.New(kind), null);
				return false;
			}
			string text;
			if (!resourceKindInfo.Validate(path, out resource, out text))
			{
				exception = ValueException.NewDataFormatError(text, TextValue.New(path), null);
				return false;
			}
			exception = null;
			return true;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000343A4 File Offset: 0x000325A4
		private static bool IsAbsoluteFilePath(string path)
		{
			string[] array = path.Split(FileHelper.DirectorySeparatorChars, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Equals(array[i].Trim(), "..", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			if (path.StartsWith("\\\\", StringComparison.Ordinal))
			{
				int num = path.IndexOf('\\', 2);
				if (num == -1 || num == path.Length - 1)
				{
					return false;
				}
			}
			bool flag;
			try
			{
				flag = global::System.IO.Path.IsPathRooted(path);
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0003442C File Offset: 0x0003262C
		public static bool TryNormalizeFileUri(string resourcePath, out string normalizedResourcePath, out bool isOriginalPathUrl)
		{
			isOriginalPathUrl = resourcePath != null && resourcePath.StartsWith(Uri.UriSchemeFile + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase);
			resourcePath = UriExtensions.ProcessFilePathForUriCreate(resourcePath);
			UriBuilder uriBuilder;
			if (UriHelper.TryCreateAbsoluteUriBuilder(resourcePath, out uriBuilder))
			{
				Uri uriWithoutNormalizingBuilder = Resource.GetUriWithoutNormalizingBuilder(uriBuilder);
				if (UriHelper.IsFileUri(uriWithoutNormalizingBuilder) && Resource.IsAbsoluteFilePath(uriWithoutNormalizingBuilder.LocalPath))
				{
					normalizedResourcePath = Resource.FileNormalizer.GetNormalizer().NormalizePath(resourcePath);
					normalizedResourcePath = normalizedResourcePath.ToLowerInvariant();
					if (normalizedResourcePath.IndexOf(global::System.IO.Path.DirectorySeparatorChar) != normalizedResourcePath.Length - 1)
					{
						normalizedResourcePath = normalizedResourcePath.TrimEnd(new char[] { global::System.IO.Path.DirectorySeparatorChar });
					}
					return true;
				}
			}
			normalizedResourcePath = null;
			return false;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000344D0 File Offset: 0x000326D0
		public static bool TryNormalizeWebUri(string resourcePath, out string normalizedPath, out string nonNormalizedPath)
		{
			UriBuilder uriBuilder;
			if (UriHelper.TryCreateAbsoluteUriBuilder(resourcePath, out uriBuilder))
			{
				Uri uriWithoutNormalizingBuilder = Resource.GetUriWithoutNormalizingBuilder(uriBuilder);
				if (UriHelper.IsWebUri(uriWithoutNormalizingBuilder))
				{
					uriBuilder.Query = null;
					uriBuilder.Fragment = null;
					uriBuilder.Path = uriBuilder.Path.TrimEnd(new char[] { '/' });
					normalizedPath = Resource.RemoveDefaultPortIfIPv6(uriBuilder, uriWithoutNormalizingBuilder.HostNameType);
					char[] array = new char[normalizedPath.Length];
					for (int i = 0; i < normalizedPath.Length; i++)
					{
						char c = normalizedPath[i];
						array[i] = c;
						if (c == '%')
						{
							array[++i] = char.ToUpperInvariant(normalizedPath[i]);
							array[++i] = char.ToUpperInvariant(normalizedPath[i]);
						}
					}
					normalizedPath = new string(array);
					if (EngineFeatures.Instance.UriNormalizationVersion == 1)
					{
						try
						{
							normalizedPath = new InternalUri(normalizedPath).AbsoluteUri;
						}
						catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
						{
						}
					}
					nonNormalizedPath = uriWithoutNormalizingBuilder.AbsoluteUri;
					return true;
				}
			}
			normalizedPath = null;
			nonNormalizedPath = null;
			return false;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000345F0 File Offset: 0x000327F0
		public static bool TryNormalizeFtpUri(string resourcePath, out string normalizedResourcePath)
		{
			UriBuilder uriBuilder;
			if (UriHelper.TryCreateAbsoluteUriBuilder(resourcePath, out uriBuilder))
			{
				Uri uriWithoutNormalizingBuilder = Resource.GetUriWithoutNormalizingBuilder(uriBuilder);
				if (UriHelper.IsFtpUri(uriWithoutNormalizingBuilder))
				{
					normalizedResourcePath = Resource.RemoveDefaultPortIfIPv6(uriBuilder, uriWithoutNormalizingBuilder.HostNameType);
					return true;
				}
			}
			normalizedResourcePath = null;
			return false;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0003462A File Offset: 0x0003282A
		public static bool IsSubPath(string permittedResourcePath, string attemptedAccessResourcePath, char separatorChar)
		{
			attemptedAccessResourcePath = Resource.EnsureEndsWithSeparator(attemptedAccessResourcePath, separatorChar);
			permittedResourcePath = Resource.EnsureEndsWithSeparator(permittedResourcePath, separatorChar);
			return Resource.IsValidPathName(attemptedAccessResourcePath, separatorChar) && attemptedAccessResourcePath.StartsWith(permittedResourcePath, StringComparison.Ordinal);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00034651 File Offset: 0x00032851
		private static Uri GetUriWithoutNormalizingBuilder(UriBuilder uriBuilder)
		{
			return new Uri(uriBuilder.ToString());
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0003465E File Offset: 0x0003285E
		private static string RemoveDefaultPortIfIPv6(UriBuilder uriBuilder, UriHostNameType hostNameType)
		{
			if (hostNameType == UriHostNameType.IPv6)
			{
				return UriHelper.RemoveDefaultPort(uriBuilder);
			}
			return uriBuilder.Uri.AbsoluteUri;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00034678 File Offset: 0x00032878
		private static bool IsValidPathName(string path, char separatorChar)
		{
			string[] array = path.Split(new char[] { separatorChar }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim() == "..")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000346BC File Offset: 0x000328BC
		public static IEnumerable<string> UriSubPaths(ResourceKindInfo resourceKind, string path, char separatorChar)
		{
			HashSet<string> returnedPaths = new HashSet<string>();
			Uri uri;
			while (Uri.TryCreate(path, UriKind.Absolute, out uri))
			{
				IResource resource;
				string text;
				if (resourceKind.Validate(path, out resource, out text) && returnedPaths.Add(resource.Path))
				{
					yield return resource.Path;
				}
				int num = path.LastIndexOf(separatorChar);
				if (num == path.Length - 1)
				{
					path = path.Substring(0, num);
				}
				else
				{
					path = path.Substring(0, num + 1);
				}
			}
			yield break;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000346DA File Offset: 0x000328DA
		private static string EnsureEndsWithSeparator(string path, char separatorChar)
		{
			if (!path.EndsWith(separatorChar.ToString(), StringComparison.Ordinal))
			{
				return path + separatorChar.ToString();
			}
			return path;
		}

		// Token: 0x04000784 RID: 1924
		private readonly string kind;

		// Token: 0x04000785 RID: 1925
		private readonly string path;

		// Token: 0x04000786 RID: 1926
		private readonly string nonNormalizedPath;

		// Token: 0x02000263 RID: 611
		private enum NormalizationKind
		{
			// Token: 0x04000788 RID: 1928
			Bcl,
			// Token: 0x04000789 RID: 1929
			InternalV1,
			// Token: 0x0400078A RID: 1930
			CharacterSubstitutionOnly
		}

		// Token: 0x02000264 RID: 612
		public abstract class FileNormalizer
		{
			// Token: 0x06001A0B RID: 6667 RVA: 0x000346FC File Offset: 0x000328FC
			public static Resource.FileNormalizer GetNormalizer()
			{
				int fileNormalizationVersion = EngineFeatures.Instance.FileNormalizationVersion;
				if (fileNormalizationVersion == 1)
				{
					return Resource.FileNormalizer.InternalV1Normalizer.Instance;
				}
				if (fileNormalizationVersion != 2)
				{
					return Resource.FileNormalizer.BclNormalizer.Instance;
				}
				return Resource.FileNormalizer.CharacterSubstitutionNormalizer.Instance;
			}

			// Token: 0x06001A0C RID: 6668
			public abstract string ReplaceHost(string uri, string resolvedHostName);

			// Token: 0x06001A0D RID: 6669
			public abstract string GetLocalPath(string uri);

			// Token: 0x06001A0E RID: 6670
			public abstract string NormalizePath(string uri);

			// Token: 0x06001A0F RID: 6671 RVA: 0x00034730 File Offset: 0x00032930
			protected static string ReplaceFirstInstance(string s, string find, string replace)
			{
				int num = s.IndexOf(find, StringComparison.Ordinal);
				if (num >= 0)
				{
					s = s.Substring(0, num) + replace + s.Substring(num + find.Length);
				}
				return s;
			}

			// Token: 0x02000265 RID: 613
			private sealed class BclNormalizer : Resource.FileNormalizer
			{
				// Token: 0x06001A11 RID: 6673 RVA: 0x00034769 File Offset: 0x00032969
				public override string ReplaceHost(string uriText, string resolvedHostName)
				{
					return new UriBuilder(uriText)
					{
						Host = resolvedHostName
					}.Uri.LocalPath;
				}

				// Token: 0x06001A12 RID: 6674 RVA: 0x00034782 File Offset: 0x00032982
				public override string GetLocalPath(string uriText)
				{
					return new Uri(uriText).LocalPath;
				}

				// Token: 0x06001A13 RID: 6675 RVA: 0x00034790 File Offset: 0x00032990
				public override string NormalizePath(string uriText)
				{
					Uri uri = new Uri(UriHelper.FixUncWebDavPath(uriText));
					if (uri.HostNameType == UriHostNameType.IPv6)
					{
						return Resource.FileNormalizer.ReplaceFirstInstance(uri.LocalPath, uri.Host, "[" + UriHelper.NormalizeIPv6Host(uri.Host) + "]");
					}
					return uri.LocalPath;
				}

				// Token: 0x0400078B RID: 1931
				public static readonly Resource.FileNormalizer.BclNormalizer Instance = new Resource.FileNormalizer.BclNormalizer();
			}

			// Token: 0x02000266 RID: 614
			private sealed class InternalV1Normalizer : Resource.FileNormalizer
			{
				// Token: 0x06001A16 RID: 6678 RVA: 0x000347F8 File Offset: 0x000329F8
				public override string ReplaceHost(string uriText, string resolvedHostName)
				{
					return new InternalUriBuilder(uriText)
					{
						Host = resolvedHostName
					}.Uri.LocalPath;
				}

				// Token: 0x06001A17 RID: 6679 RVA: 0x00034811 File Offset: 0x00032A11
				public override string GetLocalPath(string uriText)
				{
					return new InternalUri(uriText).LocalPath;
				}

				// Token: 0x06001A18 RID: 6680 RVA: 0x00034820 File Offset: 0x00032A20
				public override string NormalizePath(string uriText)
				{
					InternalUri internalUri = new InternalUri(UriHelper.FixUncWebDavPath(uriText));
					if (internalUri.HostNameType == UriHostNameType.IPv6)
					{
						return Resource.FileNormalizer.ReplaceFirstInstance(internalUri.LocalPath, internalUri.Host, "[" + UriHelper.NormalizeIPv6Host(internalUri.Host) + "]");
					}
					return internalUri.LocalPath;
				}

				// Token: 0x0400078C RID: 1932
				public static readonly Resource.FileNormalizer.InternalV1Normalizer Instance = new Resource.FileNormalizer.InternalV1Normalizer();
			}

			// Token: 0x02000267 RID: 615
			private sealed class CharacterSubstitutionNormalizer : Resource.FileNormalizer
			{
				// Token: 0x06001A1B RID: 6683 RVA: 0x00034880 File Offset: 0x00032A80
				public override string ReplaceHost(string uriText, string resolvedHostName)
				{
					Resource.FileNormalizer.CharacterSubstitutionNormalizer.Substituter substituter = new Resource.FileNormalizer.CharacterSubstitutionNormalizer.Substituter(uriText);
					return substituter.Fixup(new UriBuilder(substituter.Replaced)
					{
						Host = resolvedHostName
					}.Uri.LocalPath);
				}

				// Token: 0x06001A1C RID: 6684 RVA: 0x000348BC File Offset: 0x00032ABC
				public override string GetLocalPath(string uriText)
				{
					Resource.FileNormalizer.CharacterSubstitutionNormalizer.Substituter substituter = new Resource.FileNormalizer.CharacterSubstitutionNormalizer.Substituter(uriText);
					return substituter.Fixup(new Uri(substituter.Replaced).LocalPath);
				}

				// Token: 0x06001A1D RID: 6685 RVA: 0x000348EC File Offset: 0x00032AEC
				public override string NormalizePath(string uriText)
				{
					Resource.FileNormalizer.CharacterSubstitutionNormalizer.Substituter substituter = new Resource.FileNormalizer.CharacterSubstitutionNormalizer.Substituter(uriText);
					Uri uri = new Uri(UriHelper.FixUncWebDavPath(substituter.Fixup(uriText)));
					string text = ((uri.HostNameType == UriHostNameType.IPv6) ? Resource.FileNormalizer.ReplaceFirstInstance(uri.LocalPath, uri.Host, "[" + UriHelper.NormalizeIPv6Host(uri.Host) + "]") : uri.LocalPath);
					return substituter.Fixup(text);
				}

				// Token: 0x0400078D RID: 1933
				public static readonly Resource.FileNormalizer.CharacterSubstitutionNormalizer Instance = new Resource.FileNormalizer.CharacterSubstitutionNormalizer();

				// Token: 0x02000268 RID: 616
				private struct Substituter
				{
					// Token: 0x06001A20 RID: 6688 RVA: 0x00034968 File Offset: 0x00032B68
					public Substituter(string originalText)
					{
						this.replaced = null;
						this.replacements = null;
						BitArray bitArray = new BitArray(65535);
						foreach (char c in originalText)
						{
							bitArray[(int)c] = true;
						}
						char c2 = 'က';
						for (char c3 = '\ue000'; c3 < '豈'; c3 += '\u0001')
						{
							if (bitArray[(int)c3])
							{
								if (this.replacements == null)
								{
									this.replacements = new Dictionary<char, char>();
								}
								while (c2 < '耀' && bitArray[(int)c2])
								{
									c2 += '\u0001';
								}
								if (c2 == '耀')
								{
									throw new InvalidOperationException();
								}
								this.replacements[c3] = c2;
								this.replacements[c2] = c3;
								c2 += '\u0001';
							}
						}
						this.replaced = this.Fixup(originalText);
					}

					// Token: 0x17000CD1 RID: 3281
					// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00034A47 File Offset: 0x00032C47
					public string Replaced
					{
						get
						{
							return this.replaced;
						}
					}

					// Token: 0x06001A22 RID: 6690 RVA: 0x00034A50 File Offset: 0x00032C50
					public string Fixup(string toBeFixed)
					{
						if (this.replacements == null)
						{
							return toBeFixed;
						}
						StringBuilder stringBuilder = new StringBuilder(toBeFixed);
						for (int i = 0; i < stringBuilder.Length; i++)
						{
							char c;
							if (this.replacements.TryGetValue(stringBuilder[i], out c))
							{
								stringBuilder[i] = c;
							}
						}
						return stringBuilder.ToString();
					}

					// Token: 0x0400078E RID: 1934
					private readonly string replaced;

					// Token: 0x0400078F RID: 1935
					private Dictionary<char, char> replacements;
				}
			}
		}
	}
}
