using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x02000758 RID: 1880
	internal class StateEventAttribute : EventAttribute
	{
		// Token: 0x0600282D RID: 10285 RVA: 0x00072007 File Offset: 0x00070207
		public StateEventAttribute(string name, State value)
			: base(name, value)
		{
			this._state = value;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00072018 File Offset: 0x00070218
		public override XObject ToXML(Dictionary<object, int> identityCache)
		{
			return new XElement(base.Name, this._state.ToXML(identityCache));
		}

		// Token: 0x04001389 RID: 5001
		private readonly State _state;
	}
}
