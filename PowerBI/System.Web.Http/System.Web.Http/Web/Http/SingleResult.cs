using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Http
{
	// Token: 0x0200001C RID: 28
	[TypeForwardedFrom("System.Web.Http.OData, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class SingleResult
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003C1E File Offset: 0x00001E1E
		protected SingleResult(IQueryable queryable)
		{
			if (queryable == null)
			{
				throw Error.ArgumentNull("queryable");
			}
			this.Queryable = queryable;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003C3B File Offset: 0x00001E3B
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003C43 File Offset: 0x00001E43
		public IQueryable Queryable { get; private set; }

		// Token: 0x060000AB RID: 171 RVA: 0x00003C4C File Offset: 0x00001E4C
		public static SingleResult<T> Create<T>(IQueryable<T> queryable)
		{
			return new SingleResult<T>(queryable);
		}
	}
}
