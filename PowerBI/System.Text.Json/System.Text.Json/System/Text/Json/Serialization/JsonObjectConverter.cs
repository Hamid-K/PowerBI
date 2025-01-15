using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200008C RID: 140
	internal abstract class JsonObjectConverter<T> : JsonResumableConverter<T>
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x00025D38 File Offset: 0x00023F38
		private protected sealed override ConverterStrategy GetDefaultConverterStrategy()
		{
			return ConverterStrategy.Object;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00025D3B File Offset: 0x00023F3B
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00025D3E File Offset: 0x00023F3E
		internal sealed override Type ElementType
		{
			get
			{
				return null;
			}
		}
	}
}
