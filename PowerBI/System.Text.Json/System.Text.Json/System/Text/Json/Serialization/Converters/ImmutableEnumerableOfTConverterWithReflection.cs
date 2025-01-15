using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000BA RID: 186
	internal sealed class ImmutableEnumerableOfTConverterWithReflection<TCollection, TElement> : ImmutableEnumerableOfTConverter<TCollection, TElement> where TCollection : IEnumerable<TElement>
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x0002DBAF File Offset: 0x0002BDAF
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public ImmutableEnumerableOfTConverterWithReflection()
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002DBB7 File Offset: 0x0002BDB7
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		internal override void ConfigureJsonTypeInfoUsingReflection(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			jsonTypeInfo.CreateObjectWithArgs = DefaultJsonTypeInfoResolver.MemberAccessor.CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>();
		}
	}
}
