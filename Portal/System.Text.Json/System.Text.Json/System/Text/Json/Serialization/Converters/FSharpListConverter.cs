using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000DB RID: 219
	internal sealed class FSharpListConverter<TList, TElement> : IEnumerableDefaultConverter<TList, TElement> where TList : IEnumerable<TElement>
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0002FB12 File Offset: 0x0002DD12
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpListConverter()
		{
			this._listConstructor = FSharpCoreReflectionProxy.Instance.CreateFSharpListConstructor<TList, TElement>();
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002FB2A File Offset: 0x0002DD2A
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((List<TElement>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002FB47 File Offset: 0x0002DD47
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002FB4A File Offset: 0x0002DD4A
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new List<TElement>();
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002FB5C File Offset: 0x0002DD5C
		protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = this._listConstructor((List<TElement>)state.Current.ReturnValue);
		}

		// Token: 0x040003FA RID: 1018
		private readonly Func<IEnumerable<TElement>, TList> _listConstructor;
	}
}
