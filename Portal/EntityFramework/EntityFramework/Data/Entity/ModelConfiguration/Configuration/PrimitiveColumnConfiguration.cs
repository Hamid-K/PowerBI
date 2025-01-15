using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E1 RID: 481
	public class PrimitiveColumnConfiguration
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x00043F95 File Offset: 0x00042195
		internal PrimitiveColumnConfiguration(PrimitivePropertyConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00043FA4 File Offset: 0x000421A4
		internal PrimitivePropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00043FAC File Offset: 0x000421AC
		public PrimitiveColumnConfiguration IsOptional()
		{
			this.Configuration.IsNullable = new bool?(true);
			return this;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00043FC0 File Offset: 0x000421C0
		public PrimitiveColumnConfiguration IsRequired()
		{
			this.Configuration.IsNullable = new bool?(false);
			return this;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00043FD4 File Offset: 0x000421D4
		public PrimitiveColumnConfiguration HasColumnType(string columnType)
		{
			this.Configuration.ColumnType = columnType;
			return this;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00043FE3 File Offset: 0x000421E3
		public PrimitiveColumnConfiguration HasColumnOrder(int? columnOrder)
		{
			if (columnOrder != null && columnOrder.Value < 0)
			{
				throw new ArgumentOutOfRangeException("columnOrder");
			}
			this.Configuration.ColumnOrder = columnOrder;
			return this;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00044010 File Offset: 0x00042210
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00044018 File Offset: 0x00042218
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00044021 File Offset: 0x00042221
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00044029 File Offset: 0x00042229
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A77 RID: 2679
		private readonly PrimitivePropertyConfiguration _configuration;
	}
}
