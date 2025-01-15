using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000277 RID: 631
	internal sealed class NameOnlySecondaryRenderedStreamCacheBuilder : SecondaryRenderedStreamCacheBuilderBase
	{
		// Token: 0x06001675 RID: 5749 RVA: 0x00059758 File Offset: 0x00057958
		internal NameOnlySecondaryRenderedStreamCacheBuilder(CatalogItemContext context, string streamName)
			: base(context)
		{
			this.m_streamName = streamName;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00059768 File Offset: 0x00057968
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null);
			return base.BuildKey(key, this.m_streamName);
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool DetectHtmlRenderer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000832 RID: 2098
		private readonly string m_streamName;
	}
}
