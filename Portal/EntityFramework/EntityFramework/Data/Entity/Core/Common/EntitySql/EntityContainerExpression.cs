using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200064A RID: 1610
	internal sealed class EntityContainerExpression : ExpressionResolution
	{
		// Token: 0x06004DC8 RID: 19912 RVA: 0x00117E06 File Offset: 0x00116006
		internal EntityContainerExpression(EntityContainer entityContainer)
			: base(ExpressionResolutionClass.EntityContainer)
		{
			this.EntityContainer = entityContainer;
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x00117E16 File Offset: 0x00116016
		internal override string ExpressionClassName
		{
			get
			{
				return EntityContainerExpression.EntityContainerClassName;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x00117E1D File Offset: 0x0011601D
		internal static string EntityContainerClassName
		{
			get
			{
				return Strings.LocalizedEntityContainerExpression;
			}
		}

		// Token: 0x04001C20 RID: 7200
		internal readonly EntityContainer EntityContainer;
	}
}
