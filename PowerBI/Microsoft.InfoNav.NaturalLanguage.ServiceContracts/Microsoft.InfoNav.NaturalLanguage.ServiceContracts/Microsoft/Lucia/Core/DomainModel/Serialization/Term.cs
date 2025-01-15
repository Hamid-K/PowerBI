using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001CE RID: 462
	public sealed class Term : SingletonMapping<TermProperties>, IScalarForm<string>, IStateItem
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x00012649 File Offset: 0x00010849
		public Term()
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00012651 File Offset: 0x00010851
		public Term(string value)
			: base(value)
		{
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001265A File Offset: 0x0001085A
		public Term(string value, TermProperties properties)
			: base(value, properties)
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00012664 File Offset: 0x00010864
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0001266C File Offset: 0x0001086C
		public string Value
		{
			get
			{
				return base.UnderlyingKey;
			}
			set
			{
				base.UnderlyingKey = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00012675 File Offset: 0x00010875
		public TermProperties Properties
		{
			get
			{
				return base.UnderlyingValue;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001267D File Offset: 0x0001087D
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			if (this.Properties.IsDefault())
			{
				value = this.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001269A File Offset: 0x0001089A
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			this.Value = value;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000126A3 File Offset: 0x000108A3
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x000126B0 File Offset: 0x000108B0
		State IStateItem.State
		{
			get
			{
				return this.Properties.State;
			}
			set
			{
				this.Properties.State = value;
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000126C0 File Offset: 0x000108C0
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}{1}|{2}{3}", new object[]
			{
				this.Value,
				(this.Properties.Type == null) ? null : ("|" + this.Properties.Type.ToString()),
				this.Properties.State,
				(this.Properties.Weight == 1.0) ? null : ("|" + this.Properties.Weight.ToString())
			});
		}

		// Token: 0x040007E1 RID: 2017
		internal static readonly IEqualityComparer<Term> ValueComparer = new Term.ValueComparerImpl();

		// Token: 0x02000245 RID: 581
		private sealed class ValueComparerImpl : IEqualityComparer<Term>
		{
			// Token: 0x06000C8C RID: 3212 RVA: 0x0001A024 File Offset: 0x00018224
			public bool Equals(Term x, Term y)
			{
				bool? flag = Util.AreEqual<Term>(x, y);
				if (flag != null)
				{
					return flag.Value;
				}
				return string.Equals(x.Value, y.Value, StringComparison.Ordinal);
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x0001A05C File Offset: 0x0001825C
			public int GetHashCode(Term obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.Value.GetHashCode();
			}
		}
	}
}
