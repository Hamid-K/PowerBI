using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200001C RID: 28
	internal abstract class SemanticQueryDataShapeCommandVisitor
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005B4C File Offset: 0x00003D4C
		protected virtual void Visit(QueryDefinition query)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005B4E File Offset: 0x00003D4E
		protected virtual void Visit(DataShapeBinding binding)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005B50 File Offset: 0x00003D50
		protected virtual void Visit(QueryExpressionContainer expression)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005B52 File Offset: 0x00003D52
		protected virtual void Visit(QueryExtensionSchema extensionSchema)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005B54 File Offset: 0x00003D54
		protected virtual void Visit(SemanticQueryDataShapeCommand command)
		{
			if (command.Query != null)
			{
				this.Visit(command.Query);
			}
			if (command.Binding != null)
			{
				this.Visit(command.Binding);
			}
			if (command.Extension != null)
			{
				this.Visit(command.Extension);
			}
		}
	}
}
