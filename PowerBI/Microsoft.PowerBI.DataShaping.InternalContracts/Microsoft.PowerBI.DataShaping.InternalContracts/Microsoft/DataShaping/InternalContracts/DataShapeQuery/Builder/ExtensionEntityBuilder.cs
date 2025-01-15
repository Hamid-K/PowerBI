using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E6 RID: 230
	internal class ExtensionEntityBuilder<TParent> : BuilderBase<ExtensionEntity, TParent>
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0000DB54 File Offset: 0x0000BD54
		internal ExtensionEntityBuilder(TParent parent, ExtensionEntity activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000DB5E File Offset: 0x0000BD5E
		public ExtensionEntityBuilder<TParent> WithName(string name)
		{
			base.ActiveObject.Name = name;
			return this;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000DB6D File Offset: 0x0000BD6D
		public ExtensionEntityBuilder<TParent> WithExtends(string extends)
		{
			base.ActiveObject.Extends = extends;
			return this;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public ExtensionEntityBuilder<TParent> WithMeasure(string name, string daxText, Candidate<ConceptualPrimitiveType> dataType)
		{
			if (base.ActiveObject.Measures == null)
			{
				base.ActiveObject.Measures = new List<ExtensionMeasure>();
			}
			ExtensionMeasure extensionMeasure = new ExtensionMeasure
			{
				Name = name,
				Expression = daxText.DaxText(),
				DataType = dataType
			};
			base.ActiveObject.Measures.Add(extensionMeasure);
			return this;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public ExtensionEntityBuilder<TParent> WithMeasure(string name, Expression dsqExpression, Candidate<ConceptualPrimitiveType> dataType)
		{
			if (base.ActiveObject.Measures == null)
			{
				base.ActiveObject.Measures = new List<ExtensionMeasure>();
			}
			ExtensionMeasure extensionMeasure = new ExtensionMeasure
			{
				Name = name,
				Expression = dsqExpression,
				DataType = dataType
			};
			base.ActiveObject.Measures.Add(extensionMeasure);
			return this;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000DC38 File Offset: 0x0000BE38
		public ExtensionEntityBuilder<TParent> WithColumn(string name, string daxText, Candidate<ConceptualPrimitiveType> dataType)
		{
			if (base.ActiveObject.Columns == null)
			{
				base.ActiveObject.Columns = new List<ExtensionColumn>();
			}
			ExtensionColumn extensionColumn = new ExtensionColumn
			{
				Name = name,
				Expression = daxText.DaxText(),
				DataType = dataType
			};
			base.ActiveObject.Columns.Add(extensionColumn);
			return this;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public ExtensionEntityBuilder<TParent> WithColumn(string name, Expression dsqExpression, Candidate<ConceptualPrimitiveType> dataType)
		{
			if (base.ActiveObject.Columns == null)
			{
				base.ActiveObject.Columns = new List<ExtensionColumn>();
			}
			ExtensionColumn extensionColumn = new ExtensionColumn
			{
				Name = name,
				Expression = dsqExpression,
				DataType = dataType
			};
			base.ActiveObject.Columns.Add(extensionColumn);
			return this;
		}
	}
}
