using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Http
{
	// Token: 0x0200001D RID: 29
	[TypeForwardedFrom("System.Web.Http.OData, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public sealed class SingleResult<T> : SingleResult
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00003C54 File Offset: 0x00001E54
		public SingleResult(IQueryable<T> queryable)
			: base(queryable)
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003C5D File Offset: 0x00001E5D
		public new IQueryable<T> Queryable
		{
			get
			{
				return base.Queryable as IQueryable<T>;
			}
		}
	}
}
