using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200006C RID: 108
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class JsonSerializableAttribute : JsonAttribute
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x000247F9 File Offset: 0x000229F9
		[NullableContext(1)]
		public JsonSerializableAttribute(Type type)
		{
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00024801 File Offset: 0x00022A01
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00024809 File Offset: 0x00022A09
		public string TypeInfoPropertyName { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00024812 File Offset: 0x00022A12
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0002481A File Offset: 0x00022A1A
		public JsonSourceGenerationMode GenerationMode { get; set; }
	}
}
