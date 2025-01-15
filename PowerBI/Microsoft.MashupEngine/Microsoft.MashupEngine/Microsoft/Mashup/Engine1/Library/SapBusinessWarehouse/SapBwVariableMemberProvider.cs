using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004FE RID: 1278
	internal class SapBwVariableMemberProvider : SapBwVariableValueProvider
	{
		// Token: 0x060029A9 RID: 10665 RVA: 0x0007C9C2 File Offset: 0x0007ABC2
		public SapBwVariableMemberProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, bool allowNonAssigned, bool predictablePaging, string hierarchyUniqueNameOverride = null)
			: base(service, mdxCube, variable, allowNonAssigned)
		{
			this.predictablePaging = predictablePaging;
			this.hierarchyUniqueNameOverride = hierarchyUniqueNameOverride;
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x0007C9DF File Offset: 0x0007ABDF
		private SapBwMemberProvider FirstPageProvider
		{
			get
			{
				if (this.firstPageProvider == null)
				{
					this.firstPageProvider = new SapBwMemberProvider(this, new long?(0L), null);
				}
				return this.firstPageProvider;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x0007CA03 File Offset: 0x0007AC03
		public override bool HasValues
		{
			get
			{
				return this.FirstPageProvider.HasValues;
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x0007CA10 File Offset: 0x0007AC10
		public override IEnumerable<IValueReference> GetValues()
		{
			long num = 0L;
			bool flag = true;
			while (flag)
			{
				SapBwMemberProvider memberProvider = this.GetMemberProvider(num);
				foreach (IValueReference valueReference in memberProvider.GetValues(0L))
				{
					yield return valueReference;
				}
				IEnumerator<IValueReference> enumerator = null;
				flag = memberProvider.HasMoreValues;
				num = memberProvider.EndRow + 1L;
				memberProvider = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x0007CA20 File Offset: 0x0007AC20
		public override IEnumerable<IValueReference> GetValues(long skip)
		{
			if (!this.predictablePaging)
			{
				return base.GetValues(skip);
			}
			return this.GetValuesWithFoldedSkip(skip);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x0007CA39 File Offset: 0x0007AC39
		private IEnumerable<IValueReference> GetValuesWithFoldedSkip(long skip)
		{
			bool flag = true;
			long num;
			SapBwMemberProvider memberProvider = this.GetFirstProvider(skip, out num);
			while (flag)
			{
				foreach (IValueReference valueReference in memberProvider.GetValues(num))
				{
					yield return valueReference;
				}
				IEnumerator<IValueReference> enumerator = null;
				num = 0L;
				long num2 = memberProvider.EndRow + 1L;
				flag = memberProvider.HasMoreValues;
				if (flag)
				{
					memberProvider = new SapBwMemberProvider(this, new long?(num2), this.hierarchyUniqueNameOverride);
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x0007CA50 File Offset: 0x0007AC50
		private SapBwMemberProvider GetFirstProvider(long skip, out long offset)
		{
			if (skip < 0L)
			{
				throw new InvalidOperationException("Invalid skip value.");
			}
			if (skip >= 0L && skip < 2499L)
			{
				offset = skip;
				return this.FirstPageProvider;
			}
			skip += (long)(2500 - this.FirstPageProvider.ValueCount);
			long num = skip - skip % 2500L;
			offset = skip - num;
			return new SapBwMemberProvider(this, new long?(num), this.hierarchyUniqueNameOverride);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x0007CABD File Offset: 0x0007ACBD
		private SapBwMemberProvider GetMemberProvider(long startRow)
		{
			if (startRow == 0L)
			{
				return this.FirstPageProvider;
			}
			return new SapBwMemberProvider(this, new long?(startRow), this.hierarchyUniqueNameOverride);
		}

		// Token: 0x04001217 RID: 4631
		private SapBwMemberProvider firstPageProvider;

		// Token: 0x04001218 RID: 4632
		private bool predictablePaging;

		// Token: 0x04001219 RID: 4633
		private string hierarchyUniqueNameOverride;
	}
}
