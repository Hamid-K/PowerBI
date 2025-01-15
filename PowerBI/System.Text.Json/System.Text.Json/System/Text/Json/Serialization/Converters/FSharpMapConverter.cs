using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000DC RID: 220
	internal sealed class FSharpMapConverter<TMap, TKey, TValue> : DictionaryDefaultConverter<TMap, TKey, TValue> where TMap : IEnumerable<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0002FB89 File Offset: 0x0002DD89
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpMapConverter()
		{
			this._mapConstructor = FSharpCoreReflectionProxy.Instance.CreateFSharpMapConstructor<TMap, TKey, TValue>();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002FBA1 File Offset: 0x0002DDA1
		protected override void Add(TKey key, in TValue value, JsonSerializerOptions options, ref ReadStack state)
		{
			((List<Tuple<TKey, TValue>>)state.Current.ReturnValue).Add(new Tuple<TKey, TValue>(key, value));
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002FBC5 File Offset: 0x0002DDC5
		internal override bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002FBCB File Offset: 0x0002DDCB
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			state.Current.ReturnValue = new List<Tuple<TKey, TValue>>();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002FBDD File Offset: 0x0002DDDD
		protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = this._mapConstructor((List<Tuple<TKey, TValue>>)state.Current.ReturnValue);
		}

		// Token: 0x040003FB RID: 1019
		private readonly Func<IEnumerable<Tuple<TKey, TValue>>, TMap> _mapConstructor;
	}
}
