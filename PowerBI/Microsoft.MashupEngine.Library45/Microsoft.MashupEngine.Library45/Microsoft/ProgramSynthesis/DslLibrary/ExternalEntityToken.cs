using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007ED RID: 2029
	public class ExternalEntityToken : Token, IEntityDetectorWrapper
	{
		// Token: 0x06002B3E RID: 11070 RVA: 0x00078D0A File Offset: 0x00076F0A
		public ExternalEntityToken(EntityDetector instance)
			: base(instance.Name, 500, -5.5, true, true, null, null)
		{
			this.EntityDetectorInstance = instance;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x00078D31 File Offset: 0x00076F31
		public EntityDetector EntityDetectorInstance { get; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x00078D39 File Offset: 0x00076F39
		IEnumerable<EntityDetector> IEntityDetectorWrapper.EntityDetectors
		{
			get
			{
				return this.EntityDetectorInstance.Yield<EntityDetector>();
			}
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00078D46 File Offset: 0x00076F46
		public override IEnumerable<PositionMatch> GetMatches(string content)
		{
			return this.EntityDetectorInstance.GetMatches(content) ?? Enumerable.Empty<PositionMatch>();
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x00078D60 File Offset: 0x00076F60
		internal override XElement ToXml()
		{
			XElement xelement = new XElement("ExternalEntityToken").WithAttribute("name", base.Name).WithAttribute("score", base.Score).WithAttribute("isSymbol", base.IsSymbol);
			xelement.Add(new XElement(this.EntityDetectorInstance.RenderXML()));
			return xelement;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x00078DCC File Offset: 0x00076FCC
		public override Token Clone()
		{
			return new ExternalEntityToken(this.EntityDetectorInstance);
		}

		// Token: 0x040014BA RID: 5306
		private const double _defaultLogPrior = -5.5;

		// Token: 0x040014BB RID: 5307
		private const int _defaultScore = 500;
	}
}
