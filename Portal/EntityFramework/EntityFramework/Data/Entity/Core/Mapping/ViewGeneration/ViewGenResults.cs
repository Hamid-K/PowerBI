using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000570 RID: 1392
	internal class ViewGenResults : InternalBase
	{
		// Token: 0x060043B8 RID: 17336 RVA: 0x000EC346 File Offset: 0x000EA546
		internal ViewGenResults()
		{
			this.m_views = new KeyToListMap<EntitySetBase, GeneratedView>(EqualityComparer<EntitySetBase>.Default);
			this.m_errorLog = new ErrorLog();
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x000EC369 File Offset: 0x000EA569
		internal KeyToListMap<EntitySetBase, GeneratedView> Views
		{
			get
			{
				return this.m_views;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000EC371 File Offset: 0x000EA571
		internal IEnumerable<EdmSchemaError> Errors
		{
			get
			{
				return this.m_errorLog.Errors;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x000EC37E File Offset: 0x000EA57E
		internal bool HasErrors
		{
			get
			{
				return this.m_errorLog.Count > 0;
			}
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x000EC38E File Offset: 0x000EA58E
		internal void AddErrors(ErrorLog errorLog)
		{
			this.m_errorLog.Merge(errorLog);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x000EC39C File Offset: 0x000EA59C
		internal string ErrorsToString()
		{
			return this.m_errorLog.ToString();
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x000EC3A9 File Offset: 0x000EA5A9
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_errorLog.Count);
			builder.Append(" ");
			this.m_errorLog.ToCompactString(builder);
		}

		// Token: 0x04001850 RID: 6224
		private readonly KeyToListMap<EntitySetBase, GeneratedView> m_views;

		// Token: 0x04001851 RID: 6225
		private readonly ErrorLog m_errorLog;
	}
}
