using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000108 RID: 264
	internal sealed class UnsupportedTypeConverter<T> : JsonConverter<T>
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x00033157 File Offset: 0x00031357
		public UnsupportedTypeConverter(string errorMessage = null)
		{
			this._errorMessage = errorMessage;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00033166 File Offset: 0x00031366
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage ?? SR.Format(SR.SerializeTypeInstanceNotSupported, typeof(T).FullName);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003318C File Offset: 0x0003138C
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotSupportedException(this.ErrorMessage);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000331A4 File Offset: 0x000313A4
		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			throw new NotSupportedException(this.ErrorMessage);
		}

		// Token: 0x0400041A RID: 1050
		private readonly string _errorMessage;
	}
}
