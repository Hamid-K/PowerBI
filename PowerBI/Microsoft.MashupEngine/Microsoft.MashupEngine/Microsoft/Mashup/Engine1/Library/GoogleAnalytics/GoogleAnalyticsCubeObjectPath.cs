using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B05 RID: 2821
	internal class GoogleAnalyticsCubeObjectPath : IComparable<GoogleAnalyticsCubeObjectPath>
	{
		// Token: 0x06004E41 RID: 20033 RVA: 0x00103A7B File Offset: 0x00101C7B
		public GoogleAnalyticsCubeObjectPath(IList<string> path)
		{
			this.path = path;
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x00103A8A File Offset: 0x00101C8A
		public bool IsRoot
		{
			get
			{
				return this.path.Count == 0;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06004E43 RID: 20035 RVA: 0x00103A9A File Offset: 0x00101C9A
		public GoogleAnalyticsCubeObjectPath Parent
		{
			get
			{
				if (this.IsRoot)
				{
					throw new InvalidOperationException();
				}
				return new GoogleAnalyticsCubeObjectPath(this.path.Take(this.path.Count - 1).ToArray<string>());
			}
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00103ACC File Offset: 0x00101CCC
		public string Last()
		{
			return this.path.Last<string>();
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00103ADC File Offset: 0x00101CDC
		public int CompareTo(GoogleAnalyticsCubeObjectPath other)
		{
			for (int i = 0; i < Math.Min(this.path.Count, other.path.Count); i++)
			{
				int num = string.Compare(this.path[i], other.path[i], StringComparison.Ordinal);
				if (num != 0)
				{
					return num;
				}
			}
			if (this.path.Count < other.path.Count)
			{
				return -1;
			}
			if (this.path.Count > other.path.Count)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04002A08 RID: 10760
		public static readonly GoogleAnalyticsCubeObjectPath Root = new GoogleAnalyticsCubeObjectPath(EmptyArray<string>.Instance);

		// Token: 0x04002A09 RID: 10761
		private readonly IList<string> path;
	}
}
