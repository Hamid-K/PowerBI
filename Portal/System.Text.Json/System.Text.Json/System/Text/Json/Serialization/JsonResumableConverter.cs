using System;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000091 RID: 145
	internal abstract class JsonResumableConverter<T> : JsonConverter<T>
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00026E15 File Offset: 0x00025015
		public override bool HandleNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00026E18 File Offset: 0x00025018
		public sealed override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			ReadStack readStack = default(ReadStack);
			JsonTypeInfo typeInfoInternal = options.GetTypeInfoInternal(typeToConvert, true, new bool?(true), false, false);
			readStack.Initialize(typeInfoInternal, false);
			T t;
			bool flag;
			base.TryRead(ref reader, typeToConvert, options, ref readStack, out t, out flag);
			return t;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00026E68 File Offset: 0x00025068
		public sealed override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			WriteStack writeStack = default(WriteStack);
			JsonTypeInfo typeInfoInternal = options.GetTypeInfoInternal(typeof(T), true, new bool?(true), false, false);
			writeStack.Initialize(typeInfoInternal, null, false, false);
			try
			{
				base.TryWrite(writer, in value, options, ref writeStack);
			}
			catch
			{
				writeStack.DisposePendingDisposablesOnException();
				throw;
			}
		}
	}
}
