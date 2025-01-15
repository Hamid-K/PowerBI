using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000113 RID: 275
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class TextSegment : IRange
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0000A764 File Offset: 0x00008964
		public TextSegment()
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000A76C File Offset: 0x0000896C
		public TextSegment(int startIndex, int length, int tokenIndex = -1)
		{
			this.StartIndex = startIndex;
			this.Length = length;
			if (tokenIndex > -1)
			{
				this.TokenIndex = tokenIndex;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000A78D File Offset: 0x0000898D
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000A795 File Offset: 0x00008995
		[DataMember(IsRequired = true, Order = 10)]
		public int StartIndex { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000A79E File Offset: 0x0000899E
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000A7A6 File Offset: 0x000089A6
		[DataMember(IsRequired = true, Order = 20)]
		public int Length { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000A7AF File Offset: 0x000089AF
		public int FirstIndex
		{
			get
			{
				return this.StartIndex;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000A7B7 File Offset: 0x000089B7
		public int LastIndex
		{
			get
			{
				return this.StartIndex + this.Length - 1;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000A7C8 File Offset: 0x000089C8
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public int TokenIndex { get; set; }

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0},{1})", this.StartIndex, this.Length);
		}
	}
}
