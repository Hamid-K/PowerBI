using System;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F4 RID: 500
	public abstract class LengthPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x06001A36 RID: 6710 RVA: 0x00046C56 File Offset: 0x00044E56
		internal LengthPropertyConfiguration(LengthPropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00046C60 File Offset: 0x00044E60
		public LengthPropertyConfiguration IsMaxLength()
		{
			this.Configuration.IsMaxLength = new bool?(true);
			this.Configuration.MaxLength = null;
			return this;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00046C94 File Offset: 0x00044E94
		public LengthPropertyConfiguration HasMaxLength(int? value)
		{
			this.Configuration.MaxLength = value;
			this.Configuration.IsMaxLength = null;
			this.Configuration.IsFixedLength = new bool?(this.Configuration.IsFixedLength.GetValueOrDefault());
			return this;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00046CE5 File Offset: 0x00044EE5
		public LengthPropertyConfiguration IsFixedLength()
		{
			this.Configuration.IsFixedLength = new bool?(true);
			return this;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00046CF9 File Offset: 0x00044EF9
		public LengthPropertyConfiguration IsVariableLength()
		{
			this.Configuration.IsFixedLength = new bool?(false);
			return this;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00046D0D File Offset: 0x00044F0D
		internal new LengthPropertyConfiguration Configuration
		{
			get
			{
				return (LengthPropertyConfiguration)base.Configuration;
			}
		}
	}
}
