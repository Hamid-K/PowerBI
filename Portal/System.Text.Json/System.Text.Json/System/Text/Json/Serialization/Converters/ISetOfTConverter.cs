using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D7 RID: 215
	internal sealed class ISetOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : ISet<TElement>
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002F78B File Offset: 0x0002D98B
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002F790 File Offset: 0x0002D990
		protected override void Add(in TElement value, ref ReadStack state)
		{
			TCollection tcollection = (TCollection)((object)state.Current.ReturnValue);
			tcollection.Add(value);
			if (base.IsValueType)
			{
				state.Current.ReturnValue = tcollection;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002F7DC File Offset: 0x0002D9DC
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			base.CreateCollection(ref reader, ref state, options);
			TCollection tcollection = (TCollection)((object)state.Current.ReturnValue);
			if (tcollection.IsReadOnly)
			{
				state.Current.ReturnValue = null;
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002F82C File Offset: 0x0002DA2C
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			if (jsonTypeInfo.CreateObject == null && this.Type.IsAssignableFrom(typeof(HashSet<TElement>)))
			{
				jsonTypeInfo.CreateObject = () => new HashSet<TElement>();
			}
		}
	}
}
