﻿using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002372 File Offset: 0x00000572
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000237A File Offset: 0x0000057A
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002383 File Offset: 0x00000583
		public JsonArrayAttribute()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000238B File Offset: 0x0000058B
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239A File Offset: 0x0000059A
		[NullableContext(1)]
		public JsonArrayAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x04000027 RID: 39
		private bool _allowNullItems;
	}
}
