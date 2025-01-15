using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF3 RID: 7155
	public class ExternalReferencesVisitor : ScopedReadOnlyAstVisitor<bool>
	{
		// Token: 0x0600B29E RID: 45726 RVA: 0x00245CC0 File Offset: 0x00243EC0
		public HashSet<string> GetExternalReferences(IExpression expression)
		{
			this.references = new HashSet<string>();
			HashSet<string> hashSet;
			try
			{
				this.VisitExpression(expression);
				hashSet = this.references;
			}
			finally
			{
				this.references = null;
			}
			return hashSet;
		}

		// Token: 0x0600B29F RID: 45727 RVA: 0x00245D04 File Offset: 0x00243F04
		protected override IList<bool> CreateBindings(IDeclarator declarator)
		{
			return new bool[declarator.Count];
		}

		// Token: 0x0600B2A0 RID: 45728 RVA: 0x00245D14 File Offset: 0x00243F14
		protected override void VisitIdentifier(IIdentifierExpression identifier)
		{
			bool flag;
			if (!base.TryGetValue(identifier.Name, identifier.IsInclusive, out flag))
			{
				this.references.Add(identifier.Name.Name);
			}
		}

		// Token: 0x0600B2A1 RID: 45729 RVA: 0x00245D4E File Offset: 0x00243F4E
		protected override void VisitImplicitIdentifier(IImplicitIdentifierExpression identifier)
		{
			this.VisitIdentifier(identifier);
		}

		// Token: 0x04005B40 RID: 23360
		private HashSet<string> references;
	}
}
