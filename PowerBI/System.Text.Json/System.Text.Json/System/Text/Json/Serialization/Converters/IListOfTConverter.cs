using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D3 RID: 211
	internal sealed class IListOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : IList<TElement>
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0002F511 File Offset: 0x0002D711
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002F514 File Offset: 0x0002D714
		protected override void Add(in TElement value, ref ReadStack state)
		{
			TCollection tcollection = (TCollection)((object)state.Current.ReturnValue);
			tcollection.Add(value);
			if (base.IsValueType)
			{
				state.Current.ReturnValue = tcollection;
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002F560 File Offset: 0x0002D760
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

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			if (jsonTypeInfo.CreateObject == null && this.Type.IsAssignableFrom(typeof(List<TElement>)))
			{
				jsonTypeInfo.CreateObject = () => new List<TElement>();
			}
		}
	}
}
