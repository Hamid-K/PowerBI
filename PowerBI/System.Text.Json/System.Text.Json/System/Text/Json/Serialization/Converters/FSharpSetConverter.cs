using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000DD RID: 221
	internal sealed class FSharpSetConverter<TSet, TElement> : IEnumerableDefaultConverter<TSet, TElement> where TSet : IEnumerable<TElement>
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x0002FC0A File Offset: 0x0002DE0A
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpSetConverter()
		{
			this._setConstructor = FSharpCoreReflectionProxy.Instance.CreateFSharpSetConstructor<TSet, TElement>();
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002FC22 File Offset: 0x0002DE22
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((List<TElement>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0002FC3F File Offset: 0x0002DE3F
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002FC42 File Offset: 0x0002DE42
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new List<TElement>();
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002FC54 File Offset: 0x0002DE54
		protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = this._setConstructor((List<TElement>)state.Current.ReturnValue);
		}

		// Token: 0x040003FC RID: 1020
		private readonly Func<IEnumerable<TElement>, TSet> _setConstructor;
	}
}
