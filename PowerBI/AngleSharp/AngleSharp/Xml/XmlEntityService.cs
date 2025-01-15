using System;
using System.Collections.Generic;
using AngleSharp.Services;

namespace AngleSharp.Xml
{
	// Token: 0x02000021 RID: 33
	public sealed class XmlEntityService : IEntityProvider
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00006B10 File Offset: 0x00004D10
		private XmlEntityService()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006B80 File Offset: 0x00004D80
		public string GetSymbol(string name)
		{
			string empty = string.Empty;
			if (!string.IsNullOrEmpty(name))
			{
				this._entities.TryGetValue(name, out empty);
			}
			return empty;
		}

		// Token: 0x040001B4 RID: 436
		private readonly Dictionary<string, string> _entities = new Dictionary<string, string>
		{
			{ "amp", "&" },
			{ "lt", "<" },
			{ "gt", ">" },
			{ "apos", "'" },
			{ "quot", "\"" }
		};

		// Token: 0x040001B5 RID: 437
		public static readonly IEntityProvider Resolver = new XmlEntityService();
	}
}
