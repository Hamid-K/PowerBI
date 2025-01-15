using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000154 RID: 340
	[ImmutableObject(true)]
	public sealed class DataIndexCacheKey : IEquatable<DataIndexCacheKey>
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0000B88E File Offset: 0x00009A8E
		private DataIndexCacheKey(string content)
		{
			this._content = content;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000B89D File Offset: 0x00009A9D
		public static DataIndexCacheKey Create(IEnumerable<DataIndexElement> indexElements, IReadOnlyDictionary<string, string> tags, CancellationToken cancellationToken)
		{
			List<DataIndexElement> list = new List<DataIndexElement>(indexElements);
			cancellationToken.ThrowIfCancellationRequested();
			list.Sort(DataIndexElement.StableComparer);
			cancellationToken.ThrowIfCancellationRequested();
			object obj = new DataIndexCacheKey.CacheKeyStructure(list, tags);
			cancellationToken.ThrowIfCancellationRequested();
			return new DataIndexCacheKey(JsonConvert.SerializeObject(obj));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000B8D5 File Offset: 0x00009AD5
		public static DataIndexCacheKey Create(string content)
		{
			return new DataIndexCacheKey(content);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000B8DD File Offset: 0x00009ADD
		public static DataIndexCacheKey Read(TextReader reader)
		{
			return new DataIndexCacheKey(reader.ReadToEnd());
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000B8EA File Offset: 0x00009AEA
		public bool Equals(DataIndexCacheKey other)
		{
			return other != null && DataIndexCacheKey._comparer.Equals(this._content, other._content);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000B907 File Offset: 0x00009B07
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataIndexCacheKey);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000B915 File Offset: 0x00009B15
		public override int GetHashCode()
		{
			return DataIndexCacheKey._comparer.GetHashCode(this._content);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000B927 File Offset: 0x00009B27
		public override string ToString()
		{
			return this._content;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000B92F File Offset: 0x00009B2F
		public void WriteTo(TextWriter writer)
		{
			writer.Write(this._content);
		}

		// Token: 0x0400067B RID: 1659
		private static readonly IEqualityComparer<string> _comparer = StringComparer.Ordinal;

		// Token: 0x0400067C RID: 1660
		private readonly string _content;

		// Token: 0x0200020A RID: 522
		private sealed class CacheKeyStructure
		{
			// Token: 0x06000B3C RID: 2876 RVA: 0x00014D68 File Offset: 0x00012F68
			public CacheKeyStructure(IReadOnlyList<DataIndexElement> indexElements, IReadOnlyDictionary<string, string> tags)
			{
				this.IndexElements = (from e in indexElements
					where e.Status == DataIndexElementStatus.None
					select e.WithStatus(DataIndexElementStatus.None, null)).ToList<DataIndexElement>();
				this.Tags = ((!tags.IsNullOrEmpty<KeyValuePair<string, string>>()) ? tags : null);
			}

			// Token: 0x17000333 RID: 819
			// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00014DE1 File Offset: 0x00012FE1
			public IReadOnlyList<DataIndexElement> IndexElements { get; }

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00014DE9 File Offset: 0x00012FE9
			public IReadOnlyDictionary<string, string> Tags { get; }
		}
	}
}
