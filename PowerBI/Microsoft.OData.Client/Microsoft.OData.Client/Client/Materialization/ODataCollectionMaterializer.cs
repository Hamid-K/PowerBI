using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000104 RID: 260
	internal sealed class ODataCollectionMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x0002975F File Offset: 0x0002795F
		public ODataCollectionMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002976C File Offset: 0x0002796C
		internal override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00029774 File Offset: 0x00027974
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			if (!expectedClientType.IsCollection())
			{
				throw new DataServiceClientException(Strings.AtomMaterializer_TypeShouldBeCollectionError(expectedClientType.FullName()));
			}
			Type type = Nullable.GetUnderlyingType(base.ExpectedType) ?? base.ExpectedType;
			Type type2 = type;
			Type type3 = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>));
			if (type3 != null)
			{
				type2 = type3.GetGenericArguments()[0];
			}
			else
			{
				type3 = typeof(ICollection<>).MakeGenericType(new Type[] { type2 });
			}
			Type backingTypeForCollectionProperty = WebUtil.GetBackingTypeForCollectionProperty(type3);
			object obj = base.CollectionValueMaterializationPolicy.CreateCollectionInstance((IEdmCollectionTypeReference)expectedClientType, backingTypeForCollectionProperty);
			ODataCollectionReader odataCollectionReader = this.messageReader.CreateODataCollectionReader();
			ODataCollectionMaterializer.NonEntityItemsEnumerable nonEntityItemsEnumerable = new ODataCollectionMaterializer.NonEntityItemsEnumerable(odataCollectionReader);
			bool isNullable = expectedClientType.AsCollection().ElementType().IsNullable;
			base.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(nonEntityItemsEnumerable, null, obj, type2, ClientTypeUtil.GetAddToCollectionDelegate(type3), isNullable);
			this.currentValue = obj;
		}

		// Token: 0x04000628 RID: 1576
		private object currentValue;

		// Token: 0x020001D5 RID: 469
		private class NonEntityItemsEnumerable : IEnumerable, IEnumerator
		{
			// Token: 0x06000F44 RID: 3908 RVA: 0x00032933 File Offset: 0x00030B33
			internal NonEntityItemsEnumerable(ODataCollectionReader collectionReader)
			{
				this.collectionReader = collectionReader;
			}

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00032942 File Offset: 0x00030B42
			public object Current
			{
				get
				{
					return this.collectionReader.Item;
				}
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x00002DF3 File Offset: 0x00000FF3
			public IEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x00032950 File Offset: 0x00030B50
			public bool MoveNext()
			{
				bool flag;
				for (flag = this.collectionReader.Read(); flag && this.collectionReader.State != ODataCollectionReaderState.Value; flag = this.collectionReader.Read())
				{
				}
				return flag;
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x00032989 File Offset: 0x00030B89
			public void Reset()
			{
				throw new InvalidOperationException(Strings.AtomMaterializer_ResetAfterEnumeratorCreationError);
			}

			// Token: 0x0400082B RID: 2091
			private readonly ODataCollectionReader collectionReader;
		}
	}
}
