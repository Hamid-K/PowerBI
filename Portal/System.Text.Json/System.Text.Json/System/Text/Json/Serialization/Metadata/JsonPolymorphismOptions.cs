using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x0200009E RID: 158
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonPolymorphismOptions
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00028138 File Offset: 0x00026338
		public IList<JsonDerivedType> DerivedTypes
		{
			get
			{
				JsonPolymorphismOptions.DerivedTypeList derivedTypeList;
				if ((derivedTypeList = this._derivedTypes) == null)
				{
					derivedTypeList = (this._derivedTypes = new JsonPolymorphismOptions.DerivedTypeList(this));
				}
				return derivedTypeList;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0002815E File Offset: 0x0002635E
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00028166 File Offset: 0x00026366
		public bool IgnoreUnrecognizedTypeDiscriminators
		{
			get
			{
				return this._ignoreUnrecognizedTypeDiscriminators;
			}
			set
			{
				this.VerifyMutable();
				this._ignoreUnrecognizedTypeDiscriminators = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00028175 File Offset: 0x00026375
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0002817D File Offset: 0x0002637D
		public JsonUnknownDerivedTypeHandling UnknownDerivedTypeHandling
		{
			get
			{
				return this._unknownDerivedTypeHandling;
			}
			set
			{
				this.VerifyMutable();
				this._unknownDerivedTypeHandling = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0002818C File Offset: 0x0002638C
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0002819D File Offset: 0x0002639D
		public string TypeDiscriminatorPropertyName
		{
			get
			{
				return this._typeDiscriminatorPropertyName ?? "$type";
			}
			[param: AllowNull]
			set
			{
				this.VerifyMutable();
				this._typeDiscriminatorPropertyName = value;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000281AC File Offset: 0x000263AC
		private void VerifyMutable()
		{
			JsonTypeInfo declaringTypeInfo = this.DeclaringTypeInfo;
			if (declaringTypeInfo == null)
			{
				return;
			}
			declaringTypeInfo.VerifyMutable();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000281BE File Offset: 0x000263BE
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x000281C6 File Offset: 0x000263C6
		[Nullable(2)]
		internal JsonTypeInfo DeclaringTypeInfo { get; set; }

		// Token: 0x0600094B RID: 2379 RVA: 0x000281D0 File Offset: 0x000263D0
		internal static JsonPolymorphismOptions CreateFromAttributeDeclarations(Type baseType)
		{
			JsonPolymorphismOptions jsonPolymorphismOptions = null;
			JsonPolymorphicAttribute customAttribute = baseType.GetCustomAttribute(false);
			if (customAttribute != null)
			{
				jsonPolymorphismOptions = new JsonPolymorphismOptions
				{
					IgnoreUnrecognizedTypeDiscriminators = customAttribute.IgnoreUnrecognizedTypeDiscriminators,
					UnknownDerivedTypeHandling = customAttribute.UnknownDerivedTypeHandling,
					TypeDiscriminatorPropertyName = customAttribute.TypeDiscriminatorPropertyName
				};
			}
			foreach (JsonDerivedTypeAttribute jsonDerivedTypeAttribute in baseType.GetCustomAttributes(false))
			{
				JsonPolymorphismOptions jsonPolymorphismOptions2;
				if ((jsonPolymorphismOptions2 = jsonPolymorphismOptions) == null)
				{
					jsonPolymorphismOptions2 = (jsonPolymorphismOptions = new JsonPolymorphismOptions());
				}
				jsonPolymorphismOptions2.DerivedTypes.Add(new JsonDerivedType(jsonDerivedTypeAttribute.DerivedType, jsonDerivedTypeAttribute.TypeDiscriminator));
			}
			return jsonPolymorphismOptions;
		}

		// Token: 0x0400031A RID: 794
		private JsonPolymorphismOptions.DerivedTypeList _derivedTypes;

		// Token: 0x0400031B RID: 795
		private bool _ignoreUnrecognizedTypeDiscriminators;

		// Token: 0x0400031C RID: 796
		private JsonUnknownDerivedTypeHandling _unknownDerivedTypeHandling;

		// Token: 0x0400031D RID: 797
		private string _typeDiscriminatorPropertyName;

		// Token: 0x02000136 RID: 310
		private sealed class DerivedTypeList : ConfigurationList<JsonDerivedType>
		{
			// Token: 0x06000DDE RID: 3550 RVA: 0x00035EEA File Offset: 0x000340EA
			public DerivedTypeList(JsonPolymorphismOptions parent)
				: base(null)
			{
				this._parent = parent;
			}

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00035EFA File Offset: 0x000340FA
			public override bool IsReadOnly
			{
				get
				{
					JsonTypeInfo declaringTypeInfo = this._parent.DeclaringTypeInfo;
					return declaringTypeInfo != null && declaringTypeInfo.IsReadOnly;
				}
			}

			// Token: 0x06000DE0 RID: 3552 RVA: 0x00035F12 File Offset: 0x00034112
			protected override void OnCollectionModifying()
			{
				JsonTypeInfo declaringTypeInfo = this._parent.DeclaringTypeInfo;
				if (declaringTypeInfo == null)
				{
					return;
				}
				declaringTypeInfo.VerifyMutable();
			}

			// Token: 0x040004CE RID: 1230
			private readonly JsonPolymorphismOptions _parent;
		}
	}
}
