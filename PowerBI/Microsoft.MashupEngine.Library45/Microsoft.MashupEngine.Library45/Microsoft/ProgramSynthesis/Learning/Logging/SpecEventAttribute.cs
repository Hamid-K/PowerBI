using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x02000759 RID: 1881
	internal class SpecEventAttribute : EventAttribute
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x00072036 File Offset: 0x00070236
		public SpecEventAttribute(string name, Spec value)
			: base(name, value)
		{
			this._spec = value;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00072048 File Offset: 0x00070248
		public override XObject ToXML(Dictionary<object, int> identityCache)
		{
			if (this._spec == null)
			{
				return new XElement(base.Name, "null");
			}
			return new XElement(base.Name, this._spec.ToXML(identityCache));
		}

		// Token: 0x0400138A RID: 5002
		private readonly Spec _spec;
	}
}
