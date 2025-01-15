using System;
using System.Collections.Generic;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F5 RID: 245
	internal class DbContextTypesInitializersPair : Tuple<Dictionary<Type, List<string>>, Action<DbContext>>
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x0002F1D2 File Offset: 0x0002D3D2
		public DbContextTypesInitializersPair(Dictionary<Type, List<string>> entityTypeToPropertyNameMap, Action<DbContext> setsInitializer)
			: base(entityTypeToPropertyNameMap, setsInitializer)
		{
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0002F1DC File Offset: 0x0002D3DC
		public Dictionary<Type, List<string>> EntityTypeToPropertyNameMap
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0002F1E4 File Offset: 0x0002D3E4
		public Action<DbContext> SetsInitializer
		{
			get
			{
				return base.Item2;
			}
		}
	}
}
