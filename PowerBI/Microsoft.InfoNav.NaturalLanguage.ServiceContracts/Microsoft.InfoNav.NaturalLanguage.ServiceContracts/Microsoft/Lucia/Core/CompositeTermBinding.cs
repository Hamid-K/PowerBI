using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F8 RID: 248
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class CompositeTermBinding : TermBinding
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00008DAA File Offset: 0x00006FAA
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00008DB2 File Offset: 0x00006FB2
		[DataMember(IsRequired = true, Order = 10)]
		public IList<Term> Terms { get; set; }

		// Token: 0x060004D5 RID: 1237 RVA: 0x00008DBB File Offset: 0x00006FBB
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			if (this.Terms != null)
			{
				stringBuilder.Append("_");
				foreach (Term term in this.Terms)
				{
					stringBuilder.AppendFormatInvariant("[{0},{1}{2}", new object[]
					{
						term.StartIndex,
						term.Length,
						(term.Source != SpanBindingSource.System) ? StringUtil.FormatInvariant("({0})", term.Source) : null
					});
					if (term.Binding != null)
					{
						stringBuilder.AppendFormatInvariant(":{0}]", new object[] { term.Binding.Binding });
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
