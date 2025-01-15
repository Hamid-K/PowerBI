using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.Storage;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000017 RID: 23
	public sealed class DataSource : IEquatable<DataSource>
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00005C5A File Offset: 0x00003E5A
		public static DataSource DefaultForKind(string kind)
		{
			return new DataSource(kind, null, false);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005C64 File Offset: 0x00003E64
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00005C6C File Offset: 0x00003E6C
		public string Kind { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005C75 File Offset: 0x00003E75
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005C7D File Offset: 0x00003E7D
		public string NormalizedPath
		{
			get
			{
				return this.normalizedResource.Path;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005C8A File Offset: 0x00003E8A
		public bool IsDefaultForKind
		{
			get
			{
				return this.Path == null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005C95 File Offset: 0x00003E95
		internal Resource NormalizedResource
		{
			get
			{
				return new Resource(this.Kind, this.NormalizedPath, this.Path);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005CB0 File Offset: 0x00003EB0
		private DataSource(string kind, string path, bool validatePath)
		{
			if (kind == null)
			{
				throw new ArgumentNullException("kind");
			}
			if (validatePath && path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.Kind = kind;
			this.path = path;
			ResourceKindInfo resourceKindInfo;
			if (!MashupEngines.Version1.TryLookupResourceKind(this.Kind, out resourceKindInfo))
			{
				throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(this.Kind));
			}
			if (path == null)
			{
				this.normalizedResource = new Resource(this.Kind, null, null);
				return;
			}
			string text;
			if (!resourceKindInfo.Validate(path, out this.normalizedResource, out text))
			{
				throw new MashupException(text);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005D44 File Offset: 0x00003F44
		internal DataSource(IResource resource)
			: this(resource.Kind, resource.NonNormalizedPath, false)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005D59 File Offset: 0x00003F59
		internal static DataSource New(IResource resource)
		{
			if (resource != null)
			{
				return new DataSource(resource);
			}
			return null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005D66 File Offset: 0x00003F66
		public DataSource(string kind, string path)
			: this(kind, path, true)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005D71 File Offset: 0x00003F71
		public bool Equals(DataSource other)
		{
			return other != null && this.Kind == other.Kind && this.NormalizedPath == other.NormalizedPath;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005D9C File Offset: 0x00003F9C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataSource);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005DAA File Offset: 0x00003FAA
		public override int GetHashCode()
		{
			if (this.IsDefaultForKind)
			{
				return this.Kind.GetHashCode();
			}
			return this.Kind.GetHashCode() ^ this.NormalizedPath.GetHashCode();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005DD7 File Offset: 0x00003FD7
		public override string ToString()
		{
			return this.NormalizedResource.ToString();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public bool TryFindBestMatch(IEnumerable<DataSource> candidates, out DataSource bestMatch)
		{
			foreach (DataSource dataSource in candidates.OrderByDescending((DataSource source) => ResourcePath.Length(source.NormalizedPath)))
			{
				if (ResourcePath.StartsWith(dataSource.Kind, dataSource.NormalizedPath, this.Kind, this.NormalizedPath))
				{
					bestMatch = dataSource;
					return true;
				}
			}
			bestMatch = null;
			return false;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005E78 File Offset: 0x00004078
		public IEnumerable<DataSource> GetMatchingDataSources()
		{
			if (this.IsDefaultForKind)
			{
				yield break;
			}
			string matchingKind;
			if (this.Kind == "File")
			{
				yield return this;
				matchingKind = "Folder";
			}
			else
			{
				matchingKind = this.Kind;
			}
			foreach (string text in ResourcePath.AllStartsFrom(matchingKind, this.NormalizedPath))
			{
				yield return new DataSource(matchingKind, text);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000076 RID: 118
		private readonly string path;

		// Token: 0x04000077 RID: 119
		private readonly IResource normalizedResource;
	}
}
