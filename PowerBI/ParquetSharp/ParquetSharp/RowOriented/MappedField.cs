using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ParquetSharp.RowOriented
{
	// Token: 0x0200009B RID: 155
	[NullableContext(1)]
	[Nullable(0)]
	internal struct MappedField
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x000100BC File Offset: 0x0000E2BC
		public MappedField(string name, [Nullable(2)] string mappedColumn, Type type, MemberInfo info)
		{
			this.Name = name;
			this.MappedColumn = mappedColumn;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x0400015B RID: 347
		public readonly string Name;

		// Token: 0x0400015C RID: 348
		[Nullable(2)]
		public readonly string MappedColumn;

		// Token: 0x0400015D RID: 349
		public readonly Type Type;

		// Token: 0x0400015E RID: 350
		public readonly MemberInfo Info;
	}
}
