using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x0200075A RID: 1882
	internal class ProgramSetEventAttribute : EventAttribute
	{
		// Token: 0x06002831 RID: 10289 RVA: 0x00072095 File Offset: 0x00070295
		public ProgramSetEventAttribute(string name, ProgramSet value)
			: base(name, value)
		{
			this._set = value;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000720A8 File Offset: 0x000702A8
		public override XObject ToXML(Dictionary<object, int> identityCache)
		{
			if (this._set == null)
			{
				return new XElement(base.Name, "null");
			}
			return new XElement(base.Name, this._set.ToXML(identityCache, Array.Empty<IFeature>()));
		}

		// Token: 0x0400138B RID: 5003
		private readonly ProgramSet _set;
	}
}
