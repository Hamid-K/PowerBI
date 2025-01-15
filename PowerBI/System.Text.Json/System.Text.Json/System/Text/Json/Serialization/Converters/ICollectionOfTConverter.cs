using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000CA RID: 202
	internal sealed class ICollectionOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : ICollection<TElement>
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002E75B File Offset: 0x0002C95B
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002E760 File Offset: 0x0002C960
		protected override void Add(in TElement value, ref ReadStack state)
		{
			TCollection tcollection = (TCollection)((object)state.Current.ReturnValue);
			tcollection.Add(value);
			if (base.IsValueType)
			{
				state.Current.ReturnValue = tcollection;
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002E7AC File Offset: 0x0002C9AC
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

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			if (jsonTypeInfo.CreateObject == null && this.Type.IsAssignableFrom(typeof(List<TElement>)))
			{
				jsonTypeInfo.CreateObject = () => new List<TElement>();
			}
		}
	}
}
