using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E2 RID: 482
	public class StringColumnConfiguration : LengthColumnConfiguration
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x00044031 File Offset: 0x00042231
		internal StringColumnConfiguration(StringPropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0004403A File Offset: 0x0004223A
		internal new StringPropertyConfiguration Configuration
		{
			get
			{
				return (StringPropertyConfiguration)base.Configuration;
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00044047 File Offset: 0x00042247
		public new StringColumnConfiguration IsMaxLength()
		{
			base.IsMaxLength();
			return this;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00044051 File Offset: 0x00042251
		public new StringColumnConfiguration HasMaxLength(int? value)
		{
			base.HasMaxLength(value);
			return this;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0004405C File Offset: 0x0004225C
		public new StringColumnConfiguration IsFixedLength()
		{
			base.IsFixedLength();
			return this;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00044066 File Offset: 0x00042266
		public new StringColumnConfiguration IsVariableLength()
		{
			base.IsVariableLength();
			return this;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00044070 File Offset: 0x00042270
		public new StringColumnConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0004407A File Offset: 0x0004227A
		public new StringColumnConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00044084 File Offset: 0x00042284
		public new StringColumnConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0004408F File Offset: 0x0004228F
		public new StringColumnConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0004409A File Offset: 0x0004229A
		public StringColumnConfiguration IsUnicode()
		{
			this.IsUnicode(new bool?(true));
			return this;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000440AA File Offset: 0x000422AA
		public StringColumnConfiguration IsUnicode(bool? unicode)
		{
			this.Configuration.IsUnicode = unicode;
			return this;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000440B9 File Offset: 0x000422B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000440C1 File Offset: 0x000422C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000440CA File Offset: 0x000422CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000440D2 File Offset: 0x000422D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
