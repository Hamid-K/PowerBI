using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200016B RID: 363
	[DataContract]
	public abstract class ConcTreeElement<TRegion> : TreeElement<TRegion>, IComparable<ConcTreeElement<TRegion>> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x00019491 File Offset: 0x00017691
		static ConcTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(ConcTreeElement<TRegion>));
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000194A7 File Offset: 0x000176A7
		protected ConcTreeElement()
		{
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000194AF File Offset: 0x000176AF
		protected ConcTreeElement(string n, string type, TRegion reg)
			: base(n, type)
		{
			this.Region = reg;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000194C0 File Offset: 0x000176C0
		public override bool IsInside(TRegion s)
		{
			return s != null && s.Contains(this.Region);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000194DF File Offset: 0x000176DF
		public int CompareTo(ConcTreeElement<TRegion> other)
		{
			return this.Region.CompareTo(other.Region);
		}

		// Token: 0x04000384 RID: 900
		[DataMember]
		public TRegion Region;
	}
}
