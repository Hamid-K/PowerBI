using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x0200075B RID: 1883
	internal class CollectionEventAttribute<T> : EventAttribute
	{
		// Token: 0x06002833 RID: 10291 RVA: 0x000720F4 File Offset: 0x000702F4
		public CollectionEventAttribute(string name, IEnumerable<T> value)
			: base(name, value)
		{
			this._collection = value;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00072105 File Offset: 0x00070305
		public override XObject ToXML(Dictionary<object, int> identityCache)
		{
			return this._collection.CollectionToXML(base.Name, "Item", ObjectFormatting.Literal, null, Array.Empty<Func<T, XAttribute>>());
		}

		// Token: 0x0400138C RID: 5004
		private readonly IEnumerable<T> _collection;
	}
}
