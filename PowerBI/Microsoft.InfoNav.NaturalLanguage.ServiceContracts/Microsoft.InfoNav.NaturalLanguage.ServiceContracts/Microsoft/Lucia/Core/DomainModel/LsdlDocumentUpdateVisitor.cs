using System;
using System.Collections.Generic;
using Microsoft.Lucia.Core.DomainModel.Serialization;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200017E RID: 382
	public abstract class LsdlDocumentUpdateVisitor
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		protected virtual void Visit(LsdlDocument document)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, Entity> keyValuePair in document.Entities)
			{
				if (!this.Visit(keyValuePair))
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string text in list)
			{
				document.Entities.Remove(text);
			}
			List<string> list2 = new List<string>();
			foreach (KeyValuePair<string, Relationship> keyValuePair2 in document.Relationships)
			{
				if (!this.Visit(keyValuePair2))
				{
					list2.Add(keyValuePair2.Key);
				}
			}
			foreach (string text2 in list2)
			{
				document.Relationships.Remove(text2);
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		protected virtual bool Visit(KeyValuePair<string, Entity> entity)
		{
			return true;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000E0FB File Offset: 0x0000C2FB
		protected virtual bool Visit(KeyValuePair<string, Relationship> relationship)
		{
			return true;
		}
	}
}
