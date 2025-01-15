using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B4 RID: 180
	[DataContract]
	internal struct QueryLetReferenceContext
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x00018D5C File Offset: 0x00016F5C
		internal QueryLetReferenceContext(IImmutableDictionary<string, IIntermediateTableSchema> letsByName)
		{
			this._letsByName = letsByName;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00018D65 File Offset: 0x00016F65
		internal int Count
		{
			get
			{
				return this._letsByName.Count;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00018D72 File Offset: 0x00016F72
		[DataMember(Name = "Lets", Order = 1)]
		private IReadOnlyList<KeyValuePair<string, IIntermediateTableSchema>> LetsForSerialization
		{
			get
			{
				return this._letsByName.OrderBy((KeyValuePair<string, IIntermediateTableSchema> pair) => pair.Key).ToList<KeyValuePair<string, IIntermediateTableSchema>>();
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00018DA3 File Offset: 0x00016FA3
		internal bool TryGetLetSchema(string letName, out IIntermediateTableSchema schema)
		{
			return this._letsByName.TryGetValue(letName, out schema);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00018DB2 File Offset: 0x00016FB2
		internal QueryLetReferenceContext Add(string letName, IIntermediateTableSchema schema)
		{
			return new QueryLetReferenceContext(this._letsByName.Add(letName, schema));
		}

		// Token: 0x0400037C RID: 892
		internal static readonly QueryLetReferenceContext Empty = new QueryLetReferenceContext(ImmutableDictionary.Create<string, IIntermediateTableSchema>(QueryNameComparer.Instance));

		// Token: 0x0400037D RID: 893
		private readonly IImmutableDictionary<string, IIntermediateTableSchema> _letsByName;
	}
}
