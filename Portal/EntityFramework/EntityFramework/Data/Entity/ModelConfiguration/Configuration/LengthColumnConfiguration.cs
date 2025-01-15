using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001DF RID: 479
	public abstract class LengthColumnConfiguration : PrimitiveColumnConfiguration
	{
		// Token: 0x0600192C RID: 6444 RVA: 0x00043D53 File Offset: 0x00041F53
		internal LengthColumnConfiguration(LengthPropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x00043D5C File Offset: 0x00041F5C
		internal new LengthPropertyConfiguration Configuration
		{
			get
			{
				return (LengthPropertyConfiguration)base.Configuration;
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00043D6C File Offset: 0x00041F6C
		public LengthColumnConfiguration IsMaxLength()
		{
			this.Configuration.IsMaxLength = new bool?(true);
			this.Configuration.MaxLength = null;
			return this;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00043DA0 File Offset: 0x00041FA0
		public LengthColumnConfiguration HasMaxLength(int? value)
		{
			this.Configuration.MaxLength = value;
			this.Configuration.IsMaxLength = null;
			return this;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00043DCE File Offset: 0x00041FCE
		public LengthColumnConfiguration IsFixedLength()
		{
			this.Configuration.IsFixedLength = new bool?(true);
			return this;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00043DE2 File Offset: 0x00041FE2
		public LengthColumnConfiguration IsVariableLength()
		{
			this.Configuration.IsFixedLength = new bool?(false);
			return this;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00043DF6 File Offset: 0x00041FF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00043DFE File Offset: 0x00041FFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00043E07 File Offset: 0x00042007
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00043E0F File Offset: 0x0004200F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
