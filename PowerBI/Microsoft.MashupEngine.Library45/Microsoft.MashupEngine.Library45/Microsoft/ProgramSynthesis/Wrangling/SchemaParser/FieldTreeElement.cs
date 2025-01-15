using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000170 RID: 368
	[DataContract]
	public class FieldTreeElement<TRegion> : ConcTreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x00019612 File Offset: 0x00017812
		static FieldTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(FieldTreeElement<TRegion>));
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00019628 File Offset: 0x00017828
		public FieldTreeElement()
		{
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00019630 File Offset: 0x00017830
		public FieldTreeElement(string n, TRegion reg)
			: base(n, "Field", reg)
		{
		}
	}
}
