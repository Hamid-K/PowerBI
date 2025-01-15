using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F83 RID: 3971
	internal sealed class AdobeAnalyticsSegment : AdobeAnalyticsCubeObject
	{
		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06006898 RID: 26776 RVA: 0x00167836 File Offset: 0x00165A36
		// (set) Token: 0x06006899 RID: 26777 RVA: 0x0016783E File Offset: 0x00165A3E
		public string Description { get; private set; }

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x0600689A RID: 26778 RVA: 0x000023C4 File Offset: 0x000005C4
		public override AdobeAnalyticsCubeObjectKind Kind
		{
			get
			{
				return AdobeAnalyticsCubeObjectKind.Segment;
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x00167847 File Offset: 0x00165A47
		private AdobeAnalyticsSegment(string name, string id, string description)
			: base(name, id)
		{
			this.Description = description;
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x00167858 File Offset: 0x00165A58
		public static AdobeAnalyticsSegment New(Value value)
		{
			string text = null;
			Value value2;
			if (value.TryGetValue("description", out value2) && !value2.IsNull)
			{
				text = value2.AsString;
			}
			return new AdobeAnalyticsSegment(value["name"].AsString, value["id"].AsString, text);
		}
	}
}
