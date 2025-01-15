using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000430 RID: 1072
	internal sealed class Span
	{
		// Token: 0x0600341B RID: 13339 RVA: 0x000A82F7 File Offset: 0x000A64F7
		internal Span()
		{
			this._spanList = new List<Span.SpanPath>();
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000A830A File Offset: 0x000A650A
		internal List<Span.SpanPath> SpanList
		{
			get
			{
				return this._spanList;
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000A8312 File Offset: 0x000A6512
		internal static bool RequiresRelationshipSpan(MergeOption mergeOption)
		{
			return mergeOption != MergeOption.NoTracking;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000A831B File Offset: 0x000A651B
		internal static Span IncludeIn(Span spanToIncludeIn, string pathToInclude)
		{
			if (spanToIncludeIn == null)
			{
				spanToIncludeIn = new Span();
			}
			spanToIncludeIn.Include(pathToInclude);
			return spanToIncludeIn;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000A8330 File Offset: 0x000A6530
		internal static Span CopyUnion(Span span1, Span span2)
		{
			if (span1 == null)
			{
				return span2;
			}
			if (span2 == null)
			{
				return span1;
			}
			Span span3 = span1.Clone();
			foreach (Span.SpanPath spanPath in span2.SpanList)
			{
				span3.AddSpanPath(spanPath);
			}
			return span3;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000A8398 File Offset: 0x000A6598
		internal string GetCacheKey()
		{
			if (this._cacheKey == null && this._spanList.Count > 0)
			{
				if (this._spanList.Count == 1 && this._spanList[0].Navigations.Count == 1)
				{
					this._cacheKey = this._spanList[0].Navigations[0];
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < this._spanList.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(";");
						}
						Span.SpanPath spanPath = this._spanList[i];
						stringBuilder.Append(spanPath.Navigations[0]);
						for (int j = 1; j < spanPath.Navigations.Count; j++)
						{
							stringBuilder.Append(".");
							stringBuilder.Append(spanPath.Navigations[j]);
						}
					}
					this._cacheKey = stringBuilder.ToString();
				}
			}
			return this._cacheKey;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000A84A0 File Offset: 0x000A66A0
		public void Include(string path)
		{
			Check.NotEmpty(path, "path");
			Span.SpanPath spanPath = new Span.SpanPath(Span.ParsePath(path));
			this.AddSpanPath(spanPath);
			this._cacheKey = null;
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000A84D3 File Offset: 0x000A66D3
		internal Span Clone()
		{
			Span span = new Span();
			span.SpanList.AddRange(this._spanList);
			span._cacheKey = this._cacheKey;
			return span;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000A84F7 File Offset: 0x000A66F7
		internal void AddSpanPath(Span.SpanPath spanPath)
		{
			if (this.ValidateSpanPath(spanPath))
			{
				this.RemoveExistingSubPaths(spanPath);
				this._spanList.Add(spanPath);
			}
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000A8518 File Offset: 0x000A6718
		private bool ValidateSpanPath(Span.SpanPath spanPath)
		{
			for (int i = 0; i < this._spanList.Count; i++)
			{
				if (spanPath.IsSubPath(this._spanList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000A8554 File Offset: 0x000A6754
		private void RemoveExistingSubPaths(Span.SpanPath spanPath)
		{
			List<Span.SpanPath> list = new List<Span.SpanPath>();
			for (int i = 0; i < this._spanList.Count; i++)
			{
				if (this._spanList[i].IsSubPath(spanPath))
				{
					list.Add(this._spanList[i]);
				}
			}
			foreach (Span.SpanPath spanPath2 in list)
			{
				this._spanList.Remove(spanPath2);
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000A85EC File Offset: 0x000A67EC
		private static List<string> ParsePath(string path)
		{
			List<string> list = MultipartIdentifier.ParseMultipartIdentifier(path, "[", "]", '.');
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i] == null)
				{
					list.RemoveAt(i);
				}
				else if (list[i].Length == 0)
				{
					throw new ArgumentException(Strings.ObjectQuery_Span_SpanPathSyntaxError);
				}
			}
			return list;
		}

		// Token: 0x040010D3 RID: 4307
		private readonly List<Span.SpanPath> _spanList;

		// Token: 0x040010D4 RID: 4308
		private string _cacheKey;

		// Token: 0x02000A3D RID: 2621
		internal class SpanPath
		{
			// Token: 0x0600614F RID: 24911 RVA: 0x0014F073 File Offset: 0x0014D273
			public SpanPath(List<string> navigations)
			{
				this.Navigations = navigations;
			}

			// Token: 0x06006150 RID: 24912 RVA: 0x0014F084 File Offset: 0x0014D284
			public bool IsSubPath(Span.SpanPath rhs)
			{
				if (this.Navigations.Count > rhs.Navigations.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Navigations.Count; i++)
				{
					if (!this.Navigations[i].Equals(rhs.Navigations[i], StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04002A1B RID: 10779
			public readonly List<string> Navigations;
		}
	}
}
