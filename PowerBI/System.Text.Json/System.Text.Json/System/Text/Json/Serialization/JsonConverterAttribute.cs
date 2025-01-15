using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000072 RID: 114
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = false)]
	public class JsonConverterAttribute : JsonAttribute
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x000249D0 File Offset: 0x00022BD0
		public JsonConverterAttribute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type converterType)
		{
			this.ConverterType = converterType;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000249DF File Offset: 0x00022BDF
		protected JsonConverterAttribute()
		{
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x000249E7 File Offset: 0x00022BE7
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x000249EF File Offset: 0x00022BEF
		[Nullable(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]
		public Type ConverterType
		{
			[NullableContext(2)]
			get;
			private set; }

		// Token: 0x0600080A RID: 2058 RVA: 0x000249F8 File Offset: 0x00022BF8
		[return: Nullable(2)]
		public virtual JsonConverter CreateConverter(Type typeToConvert)
		{
			return null;
		}
	}
}
