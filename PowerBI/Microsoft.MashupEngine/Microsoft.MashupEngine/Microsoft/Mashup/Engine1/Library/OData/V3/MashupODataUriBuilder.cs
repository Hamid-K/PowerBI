using System;
using Microsoft.Data.Experimental.OData.Query;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008BB RID: 2235
	internal class MashupODataUriBuilder : ODataUriBuilder
	{
		// Token: 0x06003FD4 RID: 16340 RVA: 0x000D3E30 File Offset: 0x000D2030
		private MashupODataUriBuilder(QueryToken queryToken)
			: base(queryToken)
		{
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000D3E3C File Offset: 0x000D203C
		public new static Uri CreateUri(Uri baseUri, QueryDescriptorQueryToken queryDescriptor)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (queryDescriptor == null)
			{
				throw new ArgumentNullException("queryDescriptor");
			}
			string text = new MashupODataUriBuilder(queryDescriptor).Build();
			if (text.StartsWith("?", StringComparison.Ordinal))
			{
				return new UriBuilder(baseUri)
				{
					Query = text
				}.Uri;
			}
			return new Uri(baseUri, new Uri(text, UriKind.RelativeOrAbsolute));
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000D3EA8 File Offset: 0x000D20A8
		protected override void WriteSegment(SegmentQueryToken segment)
		{
			if (segment != null && !string.IsNullOrEmpty(segment.Name) && segment.NamedValues != null)
			{
				if (segment.Parent != null)
				{
					this.WriteSegment(segment.Parent);
					base.Builder.Append("/");
				}
				base.Builder.Append(segment.Name);
				base.Builder.Append("(");
				bool flag = false;
				foreach (NamedValue namedValue in segment.NamedValues)
				{
					if (flag)
					{
						base.Builder.Append(",");
					}
					if (!string.IsNullOrEmpty(namedValue.Name))
					{
						base.Builder.Append(namedValue.Name);
						base.Builder.Append("=");
					}
					this.WriteLiteral(namedValue.Value);
					flag = true;
				}
				base.Builder.Append(")");
				return;
			}
			base.WriteSegment(segment);
		}
	}
}
